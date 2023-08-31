using Application.DTO;
using AutoMapper;
using Domain;

namespace Application.Mapper
{
    public class CustomMapper : Profile
    {
        public CustomMapper()
        {
            CreateMap<Benificiary, EscrowBeneficiaryDTO>().ReverseMap();
            CreateMap<Escrow_Disbursement_Schedule, EscrowDisbursementDTO>()
            .ForMember(dest => dest.escrow_payment_amount, source=> source.MapFrom(source =>"$"+ source.escrow_payment_amount))
            .ForMember(dest => dest.escrow_disbursement, source=> source.MapFrom(source =>"$"+ source.escrow_disbursement))
            .ForMember(dest => dest.Escrow_Balance, source=> source.MapFrom(source =>"$"+ source.Escrow_Balance)).ReverseMap();

            CreateMap<Payment_Hierarchy, TransactionDTO>()
            .ForMember(dest => dest.SheduledAmount, source=> source.MapFrom(source =>"$"+ source.Monthly_Payment_Amount.ToString()))
            .ForMember(dest => dest.ReceivedAmount, source=> source.MapFrom(source =>"$"+ source.actual_receive))
            .ForMember(dest => dest.InterestAmount, source=> source.MapFrom(source =>"$"+ source.interest))
            .ForMember(dest => dest.PrincipalAmount, source=> source.MapFrom(source =>"$"+ source.principal))
            .ForMember(dest => dest.EscrowAmount, source=> source.MapFrom(source =>"$"+ source.escrow))
            .ForMember(dest => dest.LateCharges, source=> source.MapFrom(source =>"$"+ source.late_charge))
            .ForMember(dest => dest.OtherFees, source=> source.MapFrom(source =>"$"+ source.other_fee))
            .ForMember(dest => dest.Suspense, source=> source.MapFrom(source =>"$"+ source.suspence))
            .ForMember(dest => dest.UPBAmount, source=> source.MapFrom(source =>"$"+ source.UPB_Amount)).ReverseMap();

        }
        
    }
}