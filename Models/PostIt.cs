using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMC.API.Models
{
    public class PostIt
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Color { get; set; }

        [Required]
        public BMCBlock Block { get; set; }

        [ForeignKey("BusinessModelCanvas")]
        public int CanvasId { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedAt { get; set; }

        public double PositionX { get; set; }

        public double PositionY { get; set; }
    }

}
