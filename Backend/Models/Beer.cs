using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Beer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BeerID { get; set; }
        public string Name { get; set; }
        public int BrandID { get; set; }

        [Column(TypeName ="decimal(18,2)")] //  [Column(TypeName ="SqlDataType")] -> Configura explicitamente el tipo de dato SQL para el campo creado. Es necesario para los decimales
        public decimal Alcohol {  get; set; }

        [ForeignKey("BrandID")]
        public virtual Brand Brand { get; set; }
    }
}
