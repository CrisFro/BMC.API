using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMC.API.Models
{
    public class BusinessModelCanvas
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<PostIt> PostIts { get; set; } = new List<PostIt>();

        public ICollection<UserCanvasAssociation> UserCanvases { get; set; } = new List<UserCanvasAssociation>();

    }
}
