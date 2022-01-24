using AutoMapper;
using App.Application.Interfaces;
using MediatR;
using App.Application.Dtos;

namespace App.Application.Services
{
    public record GetAllUserQuery:IRequest<Response<IReadOnlyCollection<GetUserDto>>>;
    internal class GetAllUserHandler : IRequestHandler<GetAllUserQuery, Response<IReadOnlyCollection<GetUserDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyCollection<GetUserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var users = _mapper.Map<IReadOnlyCollection<GetUserDto>>(await _userRepository.GetAsync(cancellationToken));
            return new Response<IReadOnlyCollection<GetUserDto>>(users);
        }
    }
}
