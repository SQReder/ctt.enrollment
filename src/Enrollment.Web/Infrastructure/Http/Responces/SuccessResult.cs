﻿namespace Enrollment.Web.Infrastructure.Http.Responces
{
    public class SuccessResult : GenericResult
    {
        public SuccessResult(string message = null): base(true)
        {
            Message = message;
        }
    }
}