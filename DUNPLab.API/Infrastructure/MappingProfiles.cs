using AutoMapper;
using DUNPLab.API.DTOs;
using DUNPLab.API.Models;

namespace DUNPLab.API.Infrastructure
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Supstanca, SupstancaFileDTO>();
            CreateMap<Uzorak, UzorakFileDTO>()
                .ForMember(dest => dest.ZahtevId, opt => opt.MapFrom(src => src.Testiranje.ZahtevId))
                .ForMember(dest => dest.ImePacijenta, opt => opt.MapFrom(src => src.Testiranje.Zahtev.Pacijent.Ime))
                .ForMember(dest => dest.PrezimePacijenta, opt => opt.MapFrom(src => src.Testiranje.Zahtev.Pacijent.Prezime))
                .ForMember(dest => dest.KodEpruvete, opt => opt.MapFrom(src => src.KodEpruvete))
                .ForMember(dest => dest.DatumKreiranjaFajla, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.JeLiObradjen, opt => opt.MapFrom(src => src.Testiranje.Zahtev.JeLiObradjen))
                .ForMember(dest => dest.Supstance, opt => opt.MapFrom(src => src.Testiranje.Zahtev.ZahtevSupstance.Select(zs => zs.Supstanca)));
        }
    }
}
