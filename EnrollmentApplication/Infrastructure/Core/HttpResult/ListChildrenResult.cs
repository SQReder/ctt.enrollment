using EnrollmentApplication.Infrastructure.ViewModels;

namespace EnrollmentApplication.Infrastructure.Core.HttpResult
{
    public class ListChildrenResult : GenericResult
    {
        public ChildViewModel[] Children { get; set; }
    }
}