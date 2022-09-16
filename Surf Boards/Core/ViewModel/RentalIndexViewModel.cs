using Surf_Boards.Areas.Identity.Data;
using Surf_Boards.Models;
using Surf_Boards.Models.Domain;

namespace Surf_Boards.Core.ViewModel {
    public class RentalIndexViewModel {

        public Surf_BoardsUser User { get; set; }
        public ICollection<SurfBoard> Surfboards { get; set; }
        public ICollection<Rental> Rentals { get; set; }

    }
}
