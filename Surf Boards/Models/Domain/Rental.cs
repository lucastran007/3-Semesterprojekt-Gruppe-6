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

        public string RentalName { get; set; }
        public string RentalPhone { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Required]
        public Guid SurfboardId { get; set; }

        //[Required]
        public string? UserId { get; set; }

        [Required]
        public string UserIp { get; set; }
       
        [BindProperty]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RentalDate { get; set; }

        public SurfBoard Surfboard { get; set; }
        public Surf_BoardsUser User { get; set; }


        public Rental(Guid rentalId, string rentalName, string rentalPhone, DateTime rentalDate, Guid surfboardId, string userId, string userIp)
        {
            RentalId = rentalId;
            RentalName = rentalName;
            RentalPhone = rentalPhone;
            RentalDate = rentalDate;
            SurfboardId = surfboardId;
            UserId = userId;
            UserIp = userIp;
        }

    }
}
