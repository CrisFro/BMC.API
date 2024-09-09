using Microsoft.AspNetCore.Identity;

namespace BMC.API.Models
{
    public class ApplicationUser : IdentityUser
    {
       public ICollection<UserCanvasAssociation> UserCanvases { get; set; } = new List<UserCanvasAssociation>();
    }
}
