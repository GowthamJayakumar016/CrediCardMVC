using CreditCardAppMvc.Data;
using CreditCardAppMvc.DTOs;
using CreditCardAppMvc.Models;
using CreditCardAppMvc.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CreditCardAppMvc.Services.Implementations
{
    public class ApplicationService : IApplicationService
    {
        private readonly AppDbContext _context;

        public ApplicationService(AppDbContext context)
        {
            _context = context;
        }

        private int GenerateScore()
        {
            Random r = new Random();
            return r.Next(600, 981);
        }

        private decimal CalculateLimit(decimal income)
        {
            if (income <= 200000)
                return 50000;

            if (income < 300000)
                return 75000;

            return 100000;
        }

        public async Task Apply(int userId, CreditCardApplicationDto dto)
        {
            var score = GenerateScore();
            var limit = CalculateLimit(dto.AnnualIncome);

            var app = new Application
            {
                UserId = userId,
                PAN = dto.PAN,
                DOB = dto.DOB,
                AnnualIncome = dto.AnnualIncome,
                CreditScore = score,
                CreditLimit = limit
            };

            _context.Applications.Add(app);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Application>> GetUserApplications(int userId)
        {
            return await _context.Applications
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Application>> GetAllApplications()
        {
            return await _context.Applications
                .Include(x => x.User)
                .ToListAsync();
        }
        public async Task<List<Application>> GetApplicationsByStatus(string status)
        {
            return await _context.Applications
                .Include(x => x.User)
                .Where(x => x.Status == status)
                .ToListAsync();
        }

        public async Task ApproveApplication(int id)
        {
            var app = await _context.Applications.FindAsync(id);

            if (app == null)
                throw new Exception("Application not found");

            app.Status = "Approved";

            await _context.SaveChangesAsync();
        }

        public async Task RejectApplication(int id)
        {
            var app = await _context.Applications.FindAsync(id);

            if (app == null)
                throw new Exception("Application not found");

            app.Status = "Rejected";

            await _context.SaveChangesAsync();
        }
    }
}