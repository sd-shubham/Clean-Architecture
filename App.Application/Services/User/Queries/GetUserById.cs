using AutoMapper;
using App.Application.Interfaces;
using MediatR;
using System.Net;
using App.Application.Dtos;
using App.Application.Exceptions;

namespace App.Application.Services
{
    public record GetUserById(int Id):IRequest<GetUserDto>;
    internal class GetUserByIdHandler : IRequestHandler<GetUserById,GetUserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetUserDto> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(x => x.Id == request.Id,cancellationToken);
            (user is null).IsTrue($"User does not exists");
            return _mapper.Map<GetUserDto>(user);
        }
    }
}
