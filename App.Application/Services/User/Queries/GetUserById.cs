using AutoMapper;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Services
{
    public class GetUserById:IRequest<Response<GetUserDto>>
    {
        public int Id { get; init; }
    }
    internal class GetUserByIdHandler : IRequestHandler<GetUserById, Response<GetUserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Response<GetUserDto>> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.SingleOrDefaultAsync(x => x.Id == request.Id,cancellationToken);
            if (user is null) return new Response<GetUserDto>("user not found",HttpStatusCode.NotFound);
           return new Response<GetUserDto>(_mapper.Map<GetUserDto>(user));
        }
    }
}
