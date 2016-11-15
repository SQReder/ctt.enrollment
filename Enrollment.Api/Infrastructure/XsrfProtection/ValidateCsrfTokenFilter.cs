using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enrollment.Api.Infrastructure.Http.Responces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Logging;

namespace Enrollment.Api.Infrastructure.XsrfProtection
{
    public class ValidateCsrfTokenFilter : IAsyncAuthorizationFilter, IFilterMetadata, IAntiforgeryPolicy
    {
        private readonly IAntiforgery _antiforgery;
        private readonly ILogger _logger;

        public ValidateCsrfTokenFilter(IAntiforgery antiforgery, ILoggerFactory loggerFactory)
        {
            if (antiforgery == null)
            {
                throw new ArgumentNullException(nameof(antiforgery));                
            }

            _antiforgery = antiforgery;
            _logger = loggerFactory.CreateLogger<ValidateAntiforgeryTokenAuthorizationFilter>();
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));                
            }
            if (!ShouldValidate(context))
            {
                return;
            }
            if (!IsClosestAntiforgeryPolicy(context.Filters))
            {
                return;
            }

            try
            {
                await _antiforgery.ValidateRequestAsync(context.HttpContext);
            }
            catch (AntiforgeryValidationException e)
            {
                _logger.LogInformation(e.Message, e);
                context.Result = new JsonResult(new BadCsrfTokenResult());
            }
            catch (InvalidOperationException e)
            {
                _logger.LogInformation(e.Message, e);
                context.Result = new JsonResult(new BadCsrfTokenResult());
            }
        }

        protected virtual bool ShouldValidate(AuthorizationFilterContext context)
        {
            return true;
        }

        private bool IsClosestAntiforgeryPolicy(IList<IFilterMetadata> filters)
        {
            return filters.OfType<IAntiforgeryPolicy>().LastOrDefault() == this;
        }
    }
}