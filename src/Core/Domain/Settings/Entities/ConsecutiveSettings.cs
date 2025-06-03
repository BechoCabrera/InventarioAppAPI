namespace InventarioBackend.src.Core.Domain.Settings.Entities
{
    public class ConsecutiveSettings
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string YearPrefix { get; set; } = string.Empty;
        public string Prefix { get; set; } = string.Empty;
        public int StartNumber { get; set; }
        public int IncrementStep { get; set; }
        public int LastUsedNumber { get; set; }
        public int NumberLength { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public string GenerateNextNumber()
        {
            int nextNumber = LastUsedNumber == 0 ? StartNumber : LastUsedNumber + IncrementStep;
            string formattedNumber = nextNumber.ToString($"D{NumberLength}");
            return $"{YearPrefix}{Prefix}{formattedNumber}";
        }
    }
}
