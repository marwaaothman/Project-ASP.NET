using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI1.Models.Repositories
{
    public interface IBatimentRepository
    {
        Task<IEnumerable<Batiment>> GetBatiments();
        Task<Batiment> GetBatiment(int BatimentId);
        Task<Batiment> AddBatiment(Batiment batiment);
        Task<Batiment> UpdateBatiment(Batiment batiment);
        Task<Batiment> DeleteBatiment(int BatimentId);
        Task<IEnumerable<Batiment>> GetBatimentsByClient(int ClientId);
        
    }
}
