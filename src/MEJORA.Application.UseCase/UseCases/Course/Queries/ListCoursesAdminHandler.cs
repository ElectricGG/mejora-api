using MediatR;
using MEJORA.Application.Dtos.Course.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.Course.Queries
{
    public class ListCoursesAdminHandler : IRequestHandler<ListCoursesAdminQuery, Response<ListCoursesAdminResponse>>
    {
        private readonly ICourseRepository _repository;
        public ListCoursesAdminHandler(ICourseRepository repository)
            => _repository = repository;
        public async Task<Response<ListCoursesAdminResponse>> Handle(ListCoursesAdminQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.ListCoursesAdmin();
            return new Response<ListCoursesAdminResponse>(response);
        }
    }
}
