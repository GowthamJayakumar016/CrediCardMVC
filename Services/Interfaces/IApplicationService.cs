using CreditCardAppMvc.DTOs;
using CreditCardAppMvc.Models;

namespace CreditCardAppMvc.Services.Interfaces
{
    public interface IApplicationService
    {
        Task Apply(int userId, CreditCardApplicationDto dto);

        Task<List<Application>> GetUserApplications(int userId);

        Task<List<Application>> GetAllApplications();
        Task<List<Application>> GetApplicationsByStatus(string status);

        Task ApproveApplication(int id);

        Task RejectApplication(int id);
    }
}