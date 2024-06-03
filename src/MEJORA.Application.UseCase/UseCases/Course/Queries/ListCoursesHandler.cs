using MediatR;
using MEJORA.Application.Dtos.Course.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.Course.Queries
{
    public class ListCoursesHandler : IRequestHandler<ListCoursesQuery, Response<ListCoursesResponse>>
    {
        private readonly ICourseRepository _repository;
        public ListCoursesHandler(ICourseRepository repository)
            => _repository = repository;

        public async Task<Response<ListCoursesResponse>> Handle(ListCoursesQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.ListCourses();
            return new Response<ListCoursesResponse>(response);
        }
    }
}
