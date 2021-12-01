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
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository clientRepository;
        public ClientsController(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetClients()
        {
            try
            {
                return Ok(await clientRepository.GetClients());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            try
            {
                var result = await clientRepository.GetClient(id);

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
        public async Task<ActionResult<Client>> CreatedClient([FromBody] Client client)
        {
            try
            {
                if (client == null)
                    return BadRequest();

                var createdClient = await clientRepository.AddClient(client);

                return CreatedAtAction(nameof(GetClient),
                    new { id = createdClient.ClientId }, createdClient);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new client record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Client>> DeleteClient(int id)
        {
            try
            {
                var clientToDelete = await clientRepository.GetClient(id);

                if (clientToDelete == null)
                {
                    return NotFound($"Client with Id = {id} not found");
                }

                return await clientRepository.DeleteClient(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }





    }

}

