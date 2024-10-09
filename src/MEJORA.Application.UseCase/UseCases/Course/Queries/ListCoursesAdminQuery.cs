using MediatR;
using MEJORA.Application.Dtos.Course.Response;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.Course.Queries
{
    public class ListCoursesAdminQuery : IRequest<Response<ListCoursesAdminResponse>>
    {
    }
}
