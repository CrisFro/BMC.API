using BMC.API.Data;
using BMC.API.Models;
using BMC.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BMC.API.Repositories.Implementation
{
    public class PostItRepository : IPostItRepository
    {
        private readonly ApplicationDbContext _context;

        public PostItRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PostIt> GetPostItByIdAsync(int id)
        {
            return await _context.PostIts.FindAsync(id);
        }

        public async Task<IEnumerable<PostIt>> GetAllPostItsAsync()
        {
            return await _context.PostIts.ToListAsync();
        }

        public async Task<IEnumerable<PostIt>> GetPostItsByCanvasAsync(int canvasId)
        {
            return await _context.PostIts
                .Where(p => p.CanvasId == canvasId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PostIt>> GetPostItsByCanvasAndBlockAsync(int canvasId, BMCBlock block)
        {
            return await _context.PostIts
                .Where(p => p.CanvasId == canvasId && p.Block == block)
                .ToListAsync();
        }

        public async Task AddPostItAsync(PostIt postIt)
        {
            await _context.PostIts.AddAsync(postIt);
            await _context.SaveChangesAsync();
        }

        public async Task<PostIt> UpdatePostItAsync(PostIt postIt)
        {
            var existingPostIt = await GetPostItByIdAsync(postIt.Id) ?? throw new KeyNotFoundException("Post-It not found.");

            existingPostIt.Content = postIt.Content;
            existingPostIt.Color = postIt.Color;
            existingPostIt.Block = postIt.Block;
            existingPostIt.CanvasId = postIt.CanvasId;
            existingPostIt.LastModifiedBy = postIt.LastModifiedBy;
            existingPostIt.LastModifiedAt = DateTime.Now;
            existingPostIt.PositionX = postIt.PositionX; 
            existingPostIt.PositionY = postIt.PositionY; 

            _context.Entry(existingPostIt).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return existingPostIt;
        }

        public async Task DeletePostItAsync(int id)
        {
            var postIt = await GetPostItByIdAsync(id);
            if (postIt != null)
            {
                _context.PostIts.Remove(postIt);
                await _context.SaveChangesAsync();
            }
        }
    }
}
