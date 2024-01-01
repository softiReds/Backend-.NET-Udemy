using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   //  [DatabaseGenerated(DatabaseGeneratedOption.Identity)] -> Configura el autoincremento de la primary key
        public int BrandID { get; set; }
        public string Name { get; set; }
    }
}
