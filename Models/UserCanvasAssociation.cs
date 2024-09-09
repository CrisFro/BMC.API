namespace BMC.API.Models
{
    public class UserCanvasAssociation
    {
        public string UserId { get; set; }
        public int CanvasId { get; set; }
        // Outros campos opcionais, como data de associação
        public DateTime AssociatedAt { get; set; }

        public ApplicationUser User { get; set; } // Propriedade de navegação
        public BusinessModelCanvas Canvas { get; set; } // Propriedade de navegação
    }
}
