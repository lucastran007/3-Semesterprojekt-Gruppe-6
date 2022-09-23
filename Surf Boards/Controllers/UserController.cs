using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Surf_Boards.Areas.Identity.Data;
using Surf_Boards.Core;
using Surf_Boards.Core.Repository;
using Surf_Boards.Core.ViewModel;

namespace Surf_Boards.Controllers
{
    [Authorize(Roles = $"{ConstantsRole.Roles.Administrator}")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<Surf_BoardsUser> _signInManager;

        public UserController(IUnitOfWork unitOfWork, SignInManager<Surf_BoardsUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var users = _unitOfWork.User.GetUsers();
            return View(users);
        }



        public async Task<IActionResult> Edit(string id)
        {
            var user = _unitOfWork.User.GetUser(id);
            var roles = _unitOfWork.Role.GetRoles();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            var roleItems = roles.Select(role => new SelectListItem(role.Name, role.Id, userRoles.Any(ur => ur.Contains(role.Name)))).ToList();

            var vm = new EditUserViewModel
            {
                User = user,
                Roles = roleItems

            };
            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> OnPostAsync(EditUserViewModel data)
        {
            var user = _unitOfWork.User.GetUser(data.User.Id);

            if (user == null)
            {
                return NotFound();
            }

            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);
            //Loop through the roles in ViewModel
            //check if the role is assigned in DB
            //if assgned -> do nothing
            //if not assigned -> add role

            var rolesToAdd = new List<string>();
            var rolesToDelete = new List<string>();


            if (rolesToAdd.Any())
            {
                await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);
            }

            if (rolesToDelete.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToDelete);
            }

            user.FirstName = data.User.FirstName;
            user.LastName = data.User.LastName;
            user.Email = data.User.Email;

            _unitOfWork.User.UpdateUser(user);

            return RedirectToAction("Edit", new { id = user.Id });
        }
    }
}
