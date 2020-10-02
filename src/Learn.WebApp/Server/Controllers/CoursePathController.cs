using AutoMapper;
using Learn.Server.Data.Exceptions;
using Learn.Server.Data.Repositories;
using Learn.Server.Shared;
using Learn.WebApp.Shared.Conflict;
using Learn.WebApp.Shared.CoursePath;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orleans;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public async Task<ActionResult<IEnumerable<CoursePathModel>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync().ConfigureAwait(false);

            return Ok(_mapper.Map<IEnumerable<CoursePathModel>>(result));
        }

        [HttpGet("{key}")]
        public async Task<ActionResult<CoursePathModel>> GetAsync([FromRoute, Required] Guid key)
        {
            // get the cached entity
            var result = await _factory.GetCoursePathGrain(key).GetAsync().ConfigureAwait(false);

            // the entity does not exist
            if (result is null) return NotFound();

            // the entity exists
            return Ok(_mapper.Map<CoursePathModel>(result));
        }

        [HttpPut("{key}")]
        public async Task<ActionResult<CoursePathModel>> PostAsync([Required] CoursePathModel model)
        {
            // to keep the compiler happy
            if (model is null) return BadRequest();

            // attempt to set the entry
            CoursePath result;
            try
            {
                result = await _factory
                    .GetCoursePathGrain(model.Key)
                    .SetAsync(_mapper.Map<CoursePath>(model))
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
                        Key = model.Key,
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
                        Key = model.Key,
                        Slug = ex.Slug
                    }
                });
            }

            return Ok(_mapper.Map<CoursePathModel>(result));
        }

        [HttpDelete]
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