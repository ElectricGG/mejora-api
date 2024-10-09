using MediatR;
using MEJORA.Application.Dtos.VideoQuestion.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.VideoQuestion.Queries
{
    public class ListVideoQuestionHandler : IRequestHandler<ListVideoQuestionQuery, Response<List<ListVideoQuestionResponse>>>
    {
        private readonly IVideoQuestionRepository _videoQuestionRepository;
        public ListVideoQuestionHandler(IVideoQuestionRepository videoQuestionRepository)
        {
            _videoQuestionRepository = videoQuestionRepository;
        }

        public async Task<Response<List<ListVideoQuestionResponse>>> Handle(ListVideoQuestionQuery request, CancellationToken cancellationToken)
        {
            var response = await _videoQuestionRepository.ListQuestionVideo();
            return new Response<List<ListVideoQuestionResponse>>(response);
        }

    }
}
