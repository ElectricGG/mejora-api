using MediatR;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Queries
{
    public class ListSurveysResolveQuery : IRequest<Response<ListSurveysResolveResponse>>
    {
    }
}
