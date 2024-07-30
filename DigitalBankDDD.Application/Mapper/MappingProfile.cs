using AutoMapper;
using DigitalBankDDD.Application.Dtos;
using DigitalBankDDD.Domain.Entities;

namespace DigitalBankDDD.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Account, AccountResponseDto>();
        CreateMap<Transaction, TransactionResponseDto>()
            .ForMember(dest => dest.FromAccount, opt => opt.MapFrom(src => src.FromAccount))
            .ForMember(dest => dest.ToAccount, opt => opt.MapFrom(src => src.ToAccount));    }
}