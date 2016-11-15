using System;

namespace Enrollment.Model.Contracts
{
    internal interface IHaveIdentifier
    {
        Guid Id { get; set; }
    }
}
