using JetBrains.Annotations;

namespace Enrollment.Web.Infrastructure.Http.Responces
{
    public abstract class GenericResult
    {
        public bool Succeeded { [UsedImplicitly] get; set; }
        public string Message { [UsedImplicitly] get; set; }

        protected GenericResult(bool succeeded)
        {
            Succeeded = succeeded;
        }
    }
}
