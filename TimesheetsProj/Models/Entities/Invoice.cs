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
        public Guid ContractId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        [NotMapped] //доделать
        public Money Sum { get; set; }
        public Contract Contract { get; set; }
        public List<Sheet> Sheets { get; set; }
    }
}
