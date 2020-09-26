using AutoMapper;
using Learn.Server.Data.Exceptions;
using Learn.Server.Data.Repositories;
using Learn.Server.Shared;
using Learn.WebApp.Shared;
using Learn.WebApp.Shared.Conflict;
using Learn.WebApp.Shared.CoursePath;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orleans;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Learn.WebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursePathController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICoursePathRepository _repository;
        private readonly IGrainFactory _factory;
        private readonly IMapper _mapper;

        public CoursePathController(ILogger<CoursePathController> logger, ICoursePathRepository repository, IGrainFactory factory, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoursePathApiModel>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(cancellationToken).ConfigureAwait(false);

            return Ok(_mapper.Map<IEnumerable<CoursePathApiModel>>(result));
        }

        [HttpGet("{key}")]
        public async Task<ActionResult<CoursePathApiModel>> GetAsync([FromRoute, Required] Guid key)
        {
            // get the cached entity
            var result = await _factory.GetCoursePathGrain(key).GetAsync().ConfigureAwait(false);

            // the entity does not exist
            if (result is null) return NotFound();

            // the entity exists
            return Ok(_mapper.Map<CoursePathApiModel>(result));
        }

        [HttpPost]
        [SwaggerResponse(409, Type = typeof(ConflictApiResponseModel))]
        public async Task<ActionResult<CoursePathApiModel>> PostAsync([FromBody, Required, DisallowNull] CoursePathApiModel input)
        {
            // to keep the compiler happy
            if (input is null) return BadRequest();

            // generate a new key for a new entry
            if (!input.Key.HasValue)
            {
                input.Key = Guid.NewGuid();
            }

            // attempt to set the entry
            CoursePath result;
            try
            {
                result = await _factory
                    .GetCoursePathGrain(input.Key.Value)
                    .SetAsync(_mapper.Map<CoursePath>(input))
                    .ConfigureAwait(false);
            }
            catch (ConcurrencyException ex)
            {
                // handle version conflict
                return Conflict(new ConflictApiResponseModel
                {
                    VersionConflict = new VersionConflictApiResponseModel
                    {
                        StoredVersion = ex.StoredVersion,
                        CurrentVersion = ex.CurrentVersion
                    }
                });
            }
            catch (NameAlreadyExistsException ex)
            {
                // handle name conflict
                return Conflict(new ConflictApiResponseModel
                {
                    NameConflict = new NameConflictApiResponseModel
                    {
                        Key = input.Key.Value,
                        Name = ex.Name
                    }
                });
            }
            catch (SlugAlreadyExistsException ex)
            {
                // handle slug conflic
                return Conflict(new ConflictApiResponseModel
                {
                    SlugConflict = new SlugConflictApiResponseModel
                    {
                        Key = input.Key.Value,
                        Slug = ex.Slug
                    }
                });
            }

            return Ok(_mapper.Map<CoursePathApiModel>(result));
        }

        [HttpDelete]
        [SwaggerResponse(409, Type = typeof(ConflictApiResponseModel))]
        public async Task<ActionResult> DeleteAsync([FromBody, Required] CoursePathDeleteRequestModel model)
        {
            // just to keep the compiler happy
            if (model is null) return BadRequest();

            // attempt deletion via the grain
            try
            {
                await _factory
                    .GetCoursePathGrain(model.Key).ClearAsync(model.Version)
                    .ConfigureAwait(false);
            }
            catch (ConcurrencyException ex)
            {
                return Conflict(new ConflictApiResponseModel
                {
                    VersionConflict = new VersionConflictApiResponseModel
                    {
                        StoredVersion = ex.StoredVersion,
                        CurrentVersion = ex.CurrentVersion
                    }
                });
            }

            return Ok();
        }
    }
}