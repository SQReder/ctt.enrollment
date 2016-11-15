using System;
using System.Linq;
using System.Threading.Tasks;
using Enrollment.Api.Infrastructure.ViewModels;
using Microsoft.Extensions.Logging;

namespace Enrollment.Api.Controllers
{
    public class UnityController : CrudControllerBase<UnityViewModel>
    {
        public UnityController(ILogger logger) : base(logger)
        {
        }

        #region Overrides of CrudControllerBase<UnityViewModel>

        protected override Task<Guid> CreateInternal(UnityViewModel model)
        {
            throw new NotImplementedException();
        }

        protected override Task<UnityViewModel> ReadInternal(Guid id, ILookup<string, string> param)
        {
            throw new NotImplementedException();
        }

        protected override Task<UnityViewModel[]> ListInternal(ILookup<string, string> param)
        {
            throw new NotImplementedException();
        }

        protected override Task UpdateInternal(Guid id, UnityViewModel model)
        {
            throw new NotImplementedException();
        }

        protected override Task DeleteInternal(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}