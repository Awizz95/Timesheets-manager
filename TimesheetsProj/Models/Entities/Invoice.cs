using System.ComponentModel.DataAnnotations.Schema;
using TimesheetsProj.Domain.ValueObjects;

namespace TimesheetsProj.Models.Entities
{
    public class Invoice : Entity
    {
        public Invoice()
        {
            Sheets = new();
            Sum = Money.FromDecimal(0);
        }

        public Guid Id { get; set; }
        public required Guid ContractId { get; set; }
        public required DateTime DateStart { get; set; }
        public required DateTime DateEnd { get; set; }

        [NotMapped] //доделать
        public Money Sum { get; set; }
        public Contract Contract { get; set; } //когда он заполняется
        public List<Sheet> Sheets { get; set; }
    }
}
