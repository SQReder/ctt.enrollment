using System;

namespace Enrollment.Web.Infrastructure.Http.Responces
{
    public class GuidResult : SuccessResult
    {
        public GuidResult(Guid guid) : base()
        {
            Guid = guid;
        }

        public Guid Guid { get; set; }
    }
}