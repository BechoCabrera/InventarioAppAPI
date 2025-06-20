using InventarioBackend.Core.Application.Billing.DTOs;
using InventarioBackend.Core.Domain.Billing;
using InventarioBackend.src.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Domain.Billing.Entities;
using InventarioBackend.src.Core.Domain.Clients.Entities;
using Mapster;

namespace InventarioBackend.Core.Application._Common.Mappings
{
    public class InvoiceMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Mapeo para Invoice con sus detalles
            config.NewConfig<Invoice, InvoiceDto>()
                .Map(dest => dest.ClientName, src => src.Client != null ? src.Client.Name : src.NameClientDraft)
                .Map(dest => dest.EntitiName, src => src.EntitiConfigs.EntitiName); 
            


            // Mapeo para InvoiceCreateDto -> Invoice
            // (si tienes DTOs para creación y actualización, configúralos aquí)

            // Mapeo para InvoiceDetail <-> InvoiceDetailDto
            config.NewConfig<InvoiceDetail, InvoiceDetailDto>()
                .Map(dest => dest.ProductName, src => src.Product.Name);
            config.NewConfig<InvoiceDetailDto, InvoiceDetail>();

            config.NewConfig<InvoiceCreateDto, Invoice>();
            config.NewConfig<InvoiceDetailCreateDto, InvoiceDetail>();
        }
    }
}
