using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI1.Models.Repositories
{
    public class BatimentRepository : IBatimentRepository
    {
        private readonly AppDbContext appDbContext;

        public BatimentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Batiment>> GetBatiments()
        {
            return await appDbContext.Batiments
                .Include(e => e.Client)
                .ToListAsync();
        }

        public async Task<Batiment> GetBatiment(int batimentId)
        {
            return await appDbContext.Batiments
                .Include(e => e.Client)
                .FirstOrDefaultAsync(e => e.BatimentId == batimentId);
        }

        public async Task<Batiment> AddBatiment(Batiment batiment)
        {
            var result = await appDbContext.Batiments.AddAsync(batiment);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Batiment>> GetBatimentsByClient(int clientId)
        {
            IQueryable<Batiment> query = appDbContext.Batiments;

                query = query.Where(e => e.ClientId == clientId);
           
            return await query.ToListAsync();
        }



        public async Task<Batiment> UpdateBatiment(Batiment  batiment)
        {
            var result = await appDbContext.Batiments
                .FirstOrDefaultAsync(e => e.BatimentId == batiment.BatimentId);

            if (result != null)
            {
                result.Address = batiment.Address;
                result.City = batiment.City;
                result.PostalCode = batiment.PostalCode;
                result.ClientId = batiment.ClientId;
           

                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Batiment> DeleteBatiment(int batimentId)
        {
            var result = await appDbContext.Batiments
                .FirstOrDefaultAsync(e => e.BatimentId == batimentId);
            if (result != null)
            {
                appDbContext.Batiments.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }



        

    }
}
