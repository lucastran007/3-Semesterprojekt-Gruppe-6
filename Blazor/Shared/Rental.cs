using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Shared
{
    public class Rental
    {
        [Key]
        public Guid RentalId { get; set; }

        public string RentalName { get; set; }

        [AllowNull]
        public string? RentalPhone { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Required]
        public Guid SurfboardId { get; set; }

        //[Required]
        public string? UserId { get; set; }

        [Required]
        public string UserIp { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RentalDate { get; set; }

        public SurfBoard Surfboard { get; set; }
        //public Appli User { get; set; }


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
