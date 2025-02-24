using System.ComponentModel.DataAnnotations;

namespace GL.Company.DataAccess.Models
{
    public class TlCompany
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Exchange { get; set; } = string.Empty;

        [Required]
        public string Ticker { get; set; } = string.Empty;

        [Required, StringLength(12, MinimumLength = 2)]
        public string Isin { get; set; } = string.Empty;

        public string? Website { get; set; }
    }
}
