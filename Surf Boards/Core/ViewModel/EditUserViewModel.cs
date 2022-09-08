using Microsoft.AspNetCore.Mvc.Rendering;
using Surf_Boards.Areas.Identity.Data;

namespace Surf_Boards.Core.ViewModel
{
    public class EditUserViewModel
    {

        public Surf_BoardsUser User { get; set; }

        public IList<SelectListItem> Roles { get; set; }
    }
}
