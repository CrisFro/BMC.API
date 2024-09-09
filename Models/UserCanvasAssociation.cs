namespace BMC.API.Models
{
    public class UserCanvasAssociation
    {
        public string UserId { get; set; }
        public int CanvasId { get; set; }
        public DateTime AssociatedAt { get; set; }

        public ApplicationUser User { get; set; } 
        public BusinessModelCanvas Canvas { get; set; } 
    }
}
