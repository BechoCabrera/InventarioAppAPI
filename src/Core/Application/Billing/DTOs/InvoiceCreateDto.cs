using System;
using System.Collections.Generic;

namespace InventarioBackend.Core.Application.Billing.DTOs
{
    public class InvoiceCreateDto
    {
        public string InvoiceNumber { get; set; } = string.Empty;
        public Guid ClientId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal SubtotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? PaymentMethod { get; set; }
        public List<InvoiceDetailCreateDto> Details { get; set; } = new();
    }
}
