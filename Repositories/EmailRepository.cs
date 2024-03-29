using api.Data;
using api.Models;
using api.Repositories.Contracts;
using api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DataContext _dataContext;

        public EmailRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task Send(string _, string __)
        {
            // simulates random errors that occur with external services
            // leave this to emulate real life
            ChaosUtility.RollTheDice();

            // simulates sending an email
            // leave this delay as 10s to emulate real life
            await Task.Delay(10000);
        }
    }
}

