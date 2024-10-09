using MediatR;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.VideoQuestion.Response;
using MEJORA.Application.Dtos.Wrappers.Response;

namespace MEJORA.Application.UseCase.UseCases.VideoQuestion.Queries
{
    public class ListVideoQuestionQuery: IRequest<Response<List<ListVideoQuestionResponse>>>
    {
    }
}
