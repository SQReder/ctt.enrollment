using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enrollment.Api.Infrastructure.Http.Responces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Enrollment.Api.Controllers
{
    public abstract class CrudControllerBase<T> : Controller
    {
        private readonly ILogger _logger;

        protected abstract Task<Guid> CreateInternal(T model);
        protected abstract Task<T> ReadInternal(Guid id, ILookup<string, string> param);
        protected abstract Task<T[]> ListInternal(ILookup<string, string> param);
        protected abstract Task UpdateInternal(Guid id, T model);
        protected abstract Task DeleteInternal(Guid id);

        protected CrudControllerBase(
            ILogger logger
        )
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<GenericResult> Create([FromBody] T model)
        {
            GenericResult result;

            try
            {
                var data = await CreateInternal(model);

                result = new GenericDataResult<Guid>(data);
            }
            catch (Exception e)
            {
                _logger.LogError(default(EventId), e, e.Message);
                result = new ErrorResult(e);
            }

            return result;
        }

        [HttpGet("{id}")]
        public async Task<GenericResult> Read(Guid id)
        {
            GenericResult result;

            try
            {
                var param = GetQueryLookup();

                var data = await ReadInternal(id, param);

                result = new GenericDataResult<T>(data);
            }
            catch (Exception e)
            {
                _logger.LogError(default(EventId), e, e.Message);
                result = new ErrorResult(e);
            }

            return result;
        }

        private ILookup<string, string> GetQueryLookup()
        {
            var param = Request.Query
                .SelectMany(x => x.Value.Select(y => new KeyValuePair<string, string>(x.Key, y)))
                .ToLookup(x => x.Key, x => x.Value);
            return param;
        }

        [HttpGet]
        public async Task<GenericResult> List()
        {
            GenericResult result;

            try
            {
                var queryLookup = GetQueryLookup();

                var data = await ListInternal(queryLookup);

                result = new GenericDataResult<T[]>(data);
            }
            catch (Exception e)
            {
                _logger.LogError(default(EventId), e, e.Message);
                result = new ErrorResult(e);
            }

            return result;
        }


        [HttpPut("{id}")]
        public async Task<GenericResult> Update(Guid id, [FromBody] T model)
        {
            GenericResult result;

            try
            {
                await UpdateInternal(id, model);

                result = new SuccessResult();
            }
            catch (Exception e)
            {
                _logger.LogError(default(EventId), e, e.Message);
                result = new ErrorResult(e);
            }

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<GenericResult> Delete(Guid id)
        {
            GenericResult result;

            try
            {
                await DeleteInternal(id);

                result = new SuccessResult();
            }
            catch (Exception e)
            {
                _logger.LogError(default(EventId), e, e.Message);
                result = new ErrorResult(e);
            }

            return result;
        }
    }
}