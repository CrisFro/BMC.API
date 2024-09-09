using BMC.API.Models;

namespace BMC.API.Repositories.Interface
{
    public interface IBusinessModelCanvasRepository
    {
        Task<IEnumerable<BusinessModelCanvas>> GetAllBusinessModelCanvases();
        Task<BusinessModelCanvas> GetBusinessModelCanvasById(int id);
        Task<BusinessModelCanvas> GetBusinessModelCanvasByUserId(string userId);
        Task AddBusinessModelCanvas(BusinessModelCanvas businessModelCanvas);
        Task UpdateBusinessModelCanvas(BusinessModelCanvas businessModelCanvas);
        Task DeleteBusinessModelCanvas(int id);
        Task AddUserToBusinessModelCanvas(int canvasId, string userId);
        Task RemoveUserFromBusinessModelCanvas(int canvasId, string userId);
    }

}

