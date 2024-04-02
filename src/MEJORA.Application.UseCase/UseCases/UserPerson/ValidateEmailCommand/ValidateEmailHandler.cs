using MediatR;
using MEJORA.Application.Dtos.Wrappers.Response;
using MEJORA.Application.Interface;

namespace MEJORA.Application.UseCase.UseCases.UserPerson.ValidateEmailCommand
{
    public class ValidateEmailHandler : IRequestHandler<ValidateEmailCommand, Response<bool>>
    {
        private readonly IUserPersonRespository _userPersonRespository;
        public ValidateEmailHandler(IUserPersonRespository userPersonRespository)
            => _userPersonRespository = userPersonRespository;
        public async Task<Response<bool>> Handle(ValidateEmailCommand request, CancellationToken cancellationToken)
        {

            var validate = await _userPersonRespository.ValidateEmail(request.Identifier);

            if (!validate){
                return new Response<bool>(false, "Los datos ingresados no son correctos.");
            }

            return new Response<bool>(true, "Cuenta confirmada correctamente.");
        }
    }
}
