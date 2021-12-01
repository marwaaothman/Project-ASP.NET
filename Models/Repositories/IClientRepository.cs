using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI1.Models.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetClients();
        Task<Client> GetClient(int ClientId);
        Task<Client> AddClient(Client client);
        Task<Client> DeleteClient(int ClientId);
    }
}
