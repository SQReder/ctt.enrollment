using System;

namespace Enrollment.Web.Infrastructure.Http.Responces
{
    public class ErrorResult : GenericResult
    {   
        public string BaseMessage { get; set; }
        public string ErrorName { get; set; }

        public ErrorResult(Exception e): base(false)
        {
            Message = e.Message;
            if (e != e.GetBaseException())
                BaseMessage = e.GetBaseException().Message;
            Succeeded = false;
            ErrorName = e.GetType().Name;
        }
    }
}