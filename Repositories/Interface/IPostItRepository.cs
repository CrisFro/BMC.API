using BMC.API.Models;

namespace BMC.API.Repositories.Interface
{
    public interface IPostItRepository
    {
        Task<PostIt> GetPostItByIdAsync(int id);
        Task<IEnumerable<PostIt>> GetAllPostItsAsync();
        Task<IEnumerable<PostIt>> GetPostItsByCanvasAndBlockAsync(int canvasId, BMCBlock block);
        Task<IEnumerable<PostIt>> GetPostItsByCanvasAsync(int canvasId);
        Task AddPostItAsync(PostIt postIt);
        Task<PostIt> UpdatePostItAsync(PostIt postIt);
        Task DeletePostItAsync(int id);
    }
}
