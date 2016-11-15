using System;

namespace Enrollment.Web.Infrastructure.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(Guid id, int alternateId)
            : base($"Entry with id {id} or alternateId {alternateId} already exists")
        {
            
        }
    }
}