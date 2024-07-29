using MediatR;
using MEJORA.Application.Dtos.Lesson.Request;
using MEJORA.Application.Dtos.Lesson.Response;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.Lesson.Queries
{
    public class ListLessonDetailsHandler : IRequestHandler<ListLessonDetailsQuery, Response<ListLessonsDetailsResponse>>
    {
        private readonly ILessonRepository _lessonRepository;
        public ListLessonDetailsHandler(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<Response<ListLessonsDetailsResponse>> Handle(ListLessonDetailsQuery query, CancellationToken cancellationToken)
        {
            var rq = new ListLessonsDetailsRequest { CourseId = query.CourseId };
            var response = await _lessonRepository.ListLessonsDetails(rq);
            return new Response<ListLessonsDetailsResponse>(response);
        }
    }
}
