using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI1.Models;
using WebAPI1.Models.Repositories;

namespace WebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatimentsController : ControllerBase
    {
        private readonly IBatimentRepository batimentRepository;

        public BatimentsController(IBatimentRepository batimentRepository)
        {
            this.batimentRepository = batimentRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetBatiments()
        {
            try
            {
                return Ok(await batimentRepository.GetBatiments());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Batiment>> GetBatiment(int id)
        {
            try
            {
                var result = await batimentRepository.GetBatiment(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }



        [HttpPost]
        public async Task<ActionResult<Batiment>> CreateBatiment([FromBody] Batiment batiment)
        {
            try
          {
            if (batiment == null)
                 return BadRequest();

             var createdBatiment = await batimentRepository.AddBatiment(batiment);

             return CreatedAtAction(nameof(GetBatiment),
                 new { id = createdBatiment.BatimentId }, createdBatiment);
         }
          catch (Exception)
          {
              return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error creating new batiment record");
           }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Batiment>> UpdateBatiment(int id, Batiment batiment)
        {
            try
            {
                if (id != batiment.BatimentId)
                    return BadRequest("Batiment ID mismatch");

                var batimentToUpdate = await batimentRepository.GetBatiment(id);

                if (batimentToUpdate == null)
                    return NotFound($"Batiment with Id = {id} not found");

                return await batimentRepository.UpdateBatiment(batiment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Batiment>> DeleteBatiment(int id)
        {
            try
            {
                var batimentToDelete = await batimentRepository.GetBatiment(id);

                if (batimentToDelete == null)
                {
                    return NotFound($"Batiment with Id = {id} not found");
                }

                return await batimentRepository.DeleteBatiment(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }



        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Batiment>>> GetBatimentsByClient(int clientId)
        {
            try
            {
                var result = await batimentRepository.GetBatimentsByClient(clientId);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }





    }


}
