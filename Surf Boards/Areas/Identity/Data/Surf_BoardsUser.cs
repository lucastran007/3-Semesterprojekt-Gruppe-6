using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Surf_Boards.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Surf_BoardsUser class
public class Surf_BoardsUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class Surf_BoardRole : IdentityRole
{

}

