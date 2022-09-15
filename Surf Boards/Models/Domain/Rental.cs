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
        public Guid RentalId { get; set; }

        [Required]
        public Guid SurfboardId { get; set; }

        [Required]
        public string UserId { get; set; }
       
        [Required]
        public int RentalDuration { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        public SurfBoard Surfboard { get; set; }
        public Surf_BoardsUser User { get; set; }


        public Rental(Guid rentalId, DateTime startTime, int rentalDuration, Guid surfboardId, string userId)
        {
            RentalId = rentalId;
            StartTime = startTime;
            RentalDuration = rentalDuration;
            SurfboardId = surfboardId;
            UserId = userId;
        }
    }
}
