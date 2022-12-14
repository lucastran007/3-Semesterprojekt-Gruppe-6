using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Blazor.Shared
{
    public class SurfBoard
    {
        [Key]
        public Guid Id { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        [Column]
        [Required(ErrorMessage = "Feltet er ikke udfyldt")]
        [DisplayName("Navn")]
        public string BoardName { get; set; }

        [Required(ErrorMessage = "Feltet er ikke udfyldt")]
        [DisplayName("Længde i fod")]
        public double Length { get; set; }

        [Required(ErrorMessage = "Feltet er ikke udfyldt")]
        [DisplayName("Bredde i tommer")]
        public double Width { get; set; }

        [Required(ErrorMessage = "Feltet er ikke udfyldt")]
        [DisplayName("Tykkelse i tommer")]
        public double Thickness { get; set; }

        [Required(ErrorMessage = "Feltet er ikke udfyldt")]
        [DisplayName("Rumfang i liter")]
        public double Volume { get; set; }

        [Required(ErrorMessage = "Feltet er ikke udfyldt")]
        [DisplayName("Type")]
        public string Boardtype { get; set; }

        [Required(ErrorMessage = "Feltet er ikke udfyldt")]
        [DisplayName("Pris")]
        public double Price { get; set; }


        [DisplayName("Udstyr")]
        public string? Equipment { get; set; }

        [Column(TypeName = "nvarchar (100)")]
        [DisplayName("Billede navn")]
        public string? ImageName { get; set; }

        [NotMapped]
        [DisplayName("Tilføj billede")]
        public IFormFile? ImageFile { get; set; }
    }
}
