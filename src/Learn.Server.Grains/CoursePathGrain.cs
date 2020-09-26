using Learn.Server.Data.Exceptions;
using Learn.Server.Data.Repositories;
using Learn.Server.Shared;
using Orleans;
using System;
using System.Threading.Tasks;

namespace Learn.Server.Grains
{
    public class CoursePathGrain : Grain, ICoursePathGrain
    {
        private readonly ICoursePathRepository _repository;

        public CoursePathGrain(ICoursePathRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        private Guid _key;
        private CoursePath? _entity;

        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync().ConfigureAwait(true);

            _key = this.GetPrimaryKey();
            _entity = await _repository.GetAsync(_key).ConfigureAwait(true);
        }

        public Task<CoursePath?> GetAsync()
        {
            return Task.FromResult(_entity);
        }

        public Task<CoursePath> SetAsync(CoursePath entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            if (entity.Key != _key) throw new InvalidOperationException();

            return InnerSetAsync(entity);
        }

        private async Task<CoursePath> InnerSetAsync(CoursePath entity)
        {
            if (_entity is null)
            {
                // preempt the concurrency exception to avoid hitting the database
                if (entity.Version != Guid.Empty)
                {
                    throw new ConcurrencyException(null, entity.Version);
                }

                _entity = await _repository.AddAsync(entity).ConfigureAwait(true);
            }
            else
            {
                // preempt the concurrency exception to avoid hitting the database
                if (entity.Version != _entity.Version)
                {
                    throw new ConcurrencyException(_entity.Version, entity.Version);
                }

                _entity = await _repository.UpdateAsync(entity).ConfigureAwait(true);
            }

            return _entity;
        }

        public async Task ClearAsync(Guid version)
        {
            // ignore deletions of non-existing data
            if (_entity is null) return;

            // preempt the concurrency exception to avoid hitting the database
            if (_entity.Version != version)
            {
                throw new ConcurrencyException(_entity.Version, version);
            }

            // attempt to delete from the database
            await _repository.RemoveAsync(_entity.Key, version).ConfigureAwait(true);

            // also clear from cache
            _entity = null;
        }
    }
}