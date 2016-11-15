using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Enrollment.Api.Infrastructure.XsrfProtection
{
    /// <summary>
    /// Specifies that the class or method that this attribute is applied validates the anti-forgery token.
    /// If the anti-forgery token is not available, or if the token is invalid, the validation will fail
    /// and the action method will not execute.
    /// </summary>
    /// <remarks>
    /// This attribute helps defend against cross-site request forgery. It won't prevent other forgery or tampering
    /// attacks.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateCsrfTokenAttribute : Attribute, IFilterFactory, IFilterMetadata, IOrderedFilter
    {
        /// <inheritdoc />
        public int Order { get; set; }

        /// <inheritdoc />
        public bool IsReusable => true;

        /// <inheritdoc />
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<ValidateCsrfTokenFilter>();
        }
    }
}