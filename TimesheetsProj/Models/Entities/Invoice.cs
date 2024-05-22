using System.ComponentModel.DataAnnotations.Schema;
using TimesheetsProj.Domain.ValueObjects;

namespace TimesheetsProj.Models.Entities
{
    public class Invoice : Entity
    {
        public Guid Id { get; protected set; }
        public Guid ContractId { get; protected set; }
        public DateTime DateStart { get; protected set; }
        public DateTime DateEnd { get; protected set; }

        [NotMapped] //доделать
        public Money Sum { get; protected set; }
        public Contract Contract { get; protected set; }
        public List<Sheet> Sheets { get; set; } = new List<Sheet>();
        protected Invoice() { }
    }
}
