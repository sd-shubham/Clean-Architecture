using AutoMapper;
using App.Application.Interfaces;
using MediatR;
using System.Net;
using App.Application.Dtos;
using App.Application.Exceptions;

namespace App.Application.Services
{
    public record GetUserById(int Id):IRequest<Response<GetUserDto>>;
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
            // if (user is null) return Error<GetUserDto>.NotFound();
            user.IsNull($"User does not exists");
            return new Response<GetUserDto>(_mapper.Map<GetUserDto>(user));
        }
    }
}
