﻿using System;
using System.Collections.Generic;
using InventarioBackend.src.Core.Application.Billing.DTOs;

namespace InventarioBackend.Core.Application.Billing.DTOs
{
    public class InvoiceDto
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public Guid? ClientId { get; set; }
        public string ClientName { get; set; } = default!;
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal SubtotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? PaymentMethod { get; set; }
        public string? EntitiName { get; set; }
        public string? NameClientDraft { get; set; }
        public string? NitClientDraft { get; set; }
        public Guid? EntitiId { get; set; }
        public bool isCancelled { get; set; }
        public List<InvoiceDetailDto?> Details { get; set; } = new();
    }
}
