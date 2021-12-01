using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebAPI1.Models.Repositories
{
    public class clientRepository : IClientRepository
    {

        private readonly AppDbContext appDbContext;

        public clientRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Client>> GetClients()
        {
            return await appDbContext.Clients.ToListAsync();
        }

        public async Task<Client> GetClient(int ClientId)
        {
            return await appDbContext.Clients
                .FirstOrDefaultAsync(d => d.ClientId == ClientId);
        }

        public async Task<Client> AddClient(Client client)
        {
            var result = await appDbContext.Clients.AddAsync(client);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Client> DeleteClient(int clientId)
        {
            var result = await appDbContext.Clients
                .FirstOrDefaultAsync(e => e.ClientId == clientId);
            if (result != null)
            {
                appDbContext.Clients.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }


    }
}
