
using Application.Features.FeatureTarjeta.Commands.CrearTarjeta;
using Application.Features.FeatureTarjeta.Dto;
using Application.Features.FeatureTransaccion.Commands.NuevaCompra;
using Application.Features.FeatureTransaccion.Commands.NuevoPago;
using Application.Features.FeatureTransaccion.Dto;
using Application.Features.FeatureTransaccion.Vm;
using Application.Features.FeatureUsuario.Commands.CrearUsuario;
using Application.Features.FeatureUsuario.Commands.ModificarUsuario;
using AutoMapper;
using Domain;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tarjeta, TarjetaDto>();
            CreateMap<CrearTarjetaCommand, Tarjeta>();

            CreateMap<CrearUsuarioCommand, Usuario>();
            CreateMap<ModificarUsuarioCommand, Usuario>();

            CreateMap<Transaccion, TransaccionDto>();
            CreateMap<Transaccion, TransaccionVM>();
            CreateMap<NuevaCompraCommand, Transaccion>();
            CreateMap<NuevoPagoCommand, Transaccion>();

        }
    }
}
