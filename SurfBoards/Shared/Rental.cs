using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfBoards.Shared
{
    public class Rental
    {
        [Key]
        public Guid RentalId { get; set; }

        [Required]
        public Guid SurfboardId { get; set; }

        //[Required]
        public string UserId { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RentalDate { get; set; }



        public Rental(Guid rentalId, DateTime rentalDate, Guid surfboardId, string userId)
        {
            RentalId = rentalId;
            RentalDate = rentalDate;
            SurfboardId = surfboardId;
            UserId = userId;

        }
    }
}
