using System.ComponentModel.DataAnnotations;

namespace TimesheetsProj.Models.Dto.Requests
{
    public class ContractRequest
    {
        public string Title { get; set; }
        public string?  Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
