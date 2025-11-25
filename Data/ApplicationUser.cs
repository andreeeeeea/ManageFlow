using Microsoft.AspNetCore.Identity;
using ManageFlow.Data.Models;

namespace ManageFlow.Data;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

}

