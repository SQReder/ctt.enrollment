using System;

namespace Enrollment.Api.Infrastructure.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(Guid id, int alternateId)
            : base($"Entry with id {id} or alternateId {alternateId} already exists")
        {
            
        }
    }
}