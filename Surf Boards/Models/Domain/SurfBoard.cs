using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surf_Boards.Models
{
    public class SurfBoard
    {
        [Key]
        public Guid Id { get; set; }

        [Column]
        public string BoardName { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Thickness { get; set; }
        public double Volume { get; set; }
        public string Boardtype { get; set; }
        public double Price { get; set; }
        public string Equipment { get; set; }

        [Column(TypeName ="nvarchar (100)")]
        [DisplayName("Billede navn")]
        public string ImageName { get; set; }

        [NotMapped]
        [DisplayName("Tilføj billede")]
        public IFormFile ImageFile { get; set; }

    }
}
