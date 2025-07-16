using InventarioBackend.src.Core.Domain.EntitiConfigs.Entities;
using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Domain.CashClosings.Entities
{
    public class CashClosing
    {
        public Guid CashClosingId { get; set; } // Identificador único
        public DateTime Date { get; set; } // Fecha del cierre de caja
        public decimal TotalCash { get; set; } // Total en efectivo
        public decimal TotalCredit { get; set; } // Total en crédito
        public decimal TotalCard { get; set; } // Total en tarjeta
        public decimal TotalTransfer { get; set; } // Total en transferencias
        public decimal TotalAmount { get; set; } // Total general (suma de todos los métodos)
        public Guid? CreatedBy { get; set; } // Usuario que creó el cierre
        public DateTime CreatedAt { get; set; } // Fecha de creación
        public Guid? EntitiId { get; set; } // ID de la entidad (opcional)
        public EntitiConfig? EntitiConfigs { get; set; } 
        public User? User { get; set; } 
    }
}
