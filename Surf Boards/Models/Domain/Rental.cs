using Microsoft.AspNetCore.Mvc;
using Surf_Boards.Areas.Identity.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Surf_Boards.Models.Domain;

namespace Surf_Boards.Models.Domain
{
    public class Rental
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public SurfBoard Surfboard { get; set; }

        [Required]
        [BindProperty]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }
        
        [Required]
        [BindProperty]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        [Required]
        public Surf_BoardsUser User { get; set; }

    }
}
