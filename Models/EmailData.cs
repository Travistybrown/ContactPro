using System.ComponentModel.DataAnnotations;

namespace ContactPro.Models
{
    public class EmailData
    {
        // ToDO: Revisit 
        public int? MyProerty { get; set; }

        [Required]
        public string? EmailAddress { get; set; }
        [Required]
        public string? EmailSubject { get; set; }
        [Required]
        public string? EmailBody { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? GroupName { get; set; }
    }
}
