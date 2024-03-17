using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxCalculator.DbModels
{
    public class TaxBand
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Range(0, int.MaxValue)]
        public int LowerBound { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int UpperBound { get; set; }

        [Required]
        [Range(0, 100)]
        public int RateInPercents { get; set; }

    }
}
