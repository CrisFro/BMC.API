using BMC.API.Data;
using BMC.API.Models;
using BMC.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BMC.API.Repositories.Implementation
{
    public class BusinessModelCanvasRepository : IBusinessModelCanvasRepository
    {
        private readonly ApplicationDbContext _context;

        public BusinessModelCanvasRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BusinessModelCanvas>> GetAllBusinessModelCanvases()
        {
            return await _context.BusinessModelCanvases
                .Include(bmc => bmc.UserCanvases)
                .ThenInclude(uc => uc.User)
                .ToListAsync();
        }

        public async Task<BusinessModelCanvas> GetBusinessModelCanvasById(int id)
        {
            return await _context.BusinessModelCanvases
                .Include(bmc => bmc.UserCanvases)
                .ThenInclude(uc => uc.User)
                .FirstOrDefaultAsync(bmc => bmc.Id == id);
        }

        public async Task<BusinessModelCanvas> GetBusinessModelCanvasByUserId(string userId)
        {
            return await _context.BusinessModelCanvases
                .FirstOrDefaultAsync(b => b.UserCanvases.Any(uc => uc.UserId == userId));
        }

        public async Task AddBusinessModelCanvas(BusinessModelCanvas businessModelCanvas)
        {
            // Adicionar BusinessModelCanvas
            _context.BusinessModelCanvases.Add(businessModelCanvas);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBusinessModelCanvas(BusinessModelCanvas businessModelCanvas)
        {
            _context.Entry(businessModelCanvas).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBusinessModelCanvas(int id)
        {
            var businessModelCanvas = await _context.BusinessModelCanvases.FindAsync(id);
            if (businessModelCanvas != null)
            {
                _context.BusinessModelCanvases.Remove(businessModelCanvas);
                await _context.SaveChangesAsync();
            }
        }    

             

        public async Task AddUserToBusinessModelCanvas(int canvasId, string userId)
        {
            var canvas = await _context.BusinessModelCanvases.FindAsync(canvasId);
            if (canvas == null)
                throw new ArgumentException($"Business Model Canvas with ID {canvasId} not found.");

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new ArgumentException($"User with ID {userId} not found.");

            var userCanvasAssociation = new UserCanvasAssociation
            {
                UserId = userId,
                CanvasId = canvasId,
                AssociatedAt = DateTime.UtcNow
            };

            _context.UserCanvasAssociations.Add(userCanvasAssociation);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserFromBusinessModelCanvas(int canvasId, string userId)
        {
            var userCanvasAssociation = await _context.UserCanvasAssociations
                .FirstOrDefaultAsync(uc => uc.CanvasId == canvasId && uc.UserId == userId);

            if (userCanvasAssociation != null)
            {
                _context.UserCanvasAssociations.Remove(userCanvasAssociation);
                await _context.SaveChangesAsync();
            }
        }
    }
}

