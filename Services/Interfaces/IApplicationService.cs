using CreditCardAppMvc.DTOs;

using CreditCardAppMvc.Models;

namespace CreditCardAppMvc.Services.Interfaces
{
    public interface IApplicationService
    {
        int GenerateCreditScore();

        decimal CalculateCreditLimit(decimal income);

        Task Apply(int userId, CreditCardApplicationDto dto);

        Task<List<Application>> GetUserApplications(int userId);

        Task<List<Application>> GetAllApplications();

        Task Approve(int applicationId);

        Task Reject(int applicationId);
    }
}