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

            config.NewConfig<InvoiceDetail, InvoiceDetailDto>()
                .Map(dest => dest.ProductName, src => src.Product.Name);
            config.NewConfig<InvoiceDetailDto, InvoiceDetail>();

            config.NewConfig<InvoiceCreateDto, Invoice>();
            config.NewConfig<InvoiceDetailCreateDto, InvoiceDetail>();

            TypeAdapterConfig<InvoicesCancelled, InvoiceCancellationDto>.NewConfig()
                .Map(dest => dest.InvoiceId, src => src.InvoiceId)
                .Map(dest => dest.Reason, src => src.Reason)
                .Map(dest => dest.CancellationDate, src => src.CancellationDate)
                .Map(dest => dest.CancelledByUserId, src => src.CancelledByUserId)
                .Map(dest => dest.InvoiceNumber, src => src.Invoice != null ? src.Invoice.InvoiceNumber : null)  // Evitar NullReferenceException
                .Map(dest => dest.CancelledByUser, src => src.User != null ? src.User.Name : null)  // Evitar NullReferenceException
                .Map(dest => dest.EntitiConfigId, src => src.EntitiConfigId);
        }
    }
}
