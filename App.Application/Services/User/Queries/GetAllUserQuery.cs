using AutoMapper;
using App.Application.Interfaces;
using MediatR;
using App.Application.Dtos;

namespace App.Application.Services
{
    public record GetAllUserQuery:IRequest<IReadOnlyCollection<GetUserDto>>;
    internal class GetAllUserHandler : IRequestHandler<GetAllUserQuery,IReadOnlyCollection<GetUserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyCollection<GetUserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IReadOnlyCollection<GetUserDto>>(await _unitOfWork.UserRepository.GetAsync(cancellationToken));
        }
    }
}
