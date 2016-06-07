using EnrollmentApplication.Infrastructure.ViewModels;

namespace EnrollmentApplication.Infrastructure.Http.Responces
{
    public class ListChildrenResult : GenericResult
    {
        public ChildViewModel[] Children { get; set; }
    }
}