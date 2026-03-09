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

        public int GenerateCreditScore()
        {
            Random random = new Random();
            return random.Next(600, 981);
        }

        public decimal CalculateCreditLimit(decimal income)
        {
            if (income <= 200000)
                return 50000;

            if (income > 200000 && income < 300000)
                return 75000;

            return 100000;
        }

        public async Task Apply(int userId, CreditCardApplicationDto dto)
        {
            int score = GenerateCreditScore();

            decimal limit = CalculateCreditLimit(dto.AnnualIncome);

            var application = new Application
            {
                UserId = userId,
                PAN = dto.PAN,
                DOB = dto.DOB,
                AnnualIncome = dto.AnnualIncome,
                CreditScore = score,
                CreditLimit = limit,
                Status = "Pending"
            };

            _context.Applications.Add(application);

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

        public async Task Approve(int applicationId)
        {
            var app = await _context.Applications.FindAsync(applicationId);

            if (app == null)
                throw new Exception("Application not found");

            app.Status = "Approved";

            var card = new CreditCard
            {
                ApplicationId = app.Id,
                CardNumber = Guid.NewGuid().ToString().Substring(0, 16),
                DispatchNumber = Guid.NewGuid().ToString().Substring(0, 10),
                IssuedDate = DateTime.Now
            };

            _context.CreditCards.Add(card);

            await _context.SaveChangesAsync();
        }

        public async Task Reject(int applicationId)
        {
            var app = await _context.Applications.FindAsync(applicationId);

            if (app == null)
                throw new Exception("Application not found");

            app.Status = "Rejected";

            await _context.SaveChangesAsync();
        }
    }
}