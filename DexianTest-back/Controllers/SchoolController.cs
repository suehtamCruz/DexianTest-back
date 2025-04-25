using DexianTest_back.Interfaces;
using DexianTest_back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DexianTest_back.Controllers
{
    [ApiController]
    [Route("school")]
    [Authorize]
    public class SchoolController
    {

        private readonly IEscolaService _escolaService;

        public SchoolController(IEscolaService escolaService)
        {
            _escolaService = escolaService;
        }


        [HttpGet()]
        public async Task<List<EscolaModel>>  GetAllSchools()
        {
            return await _escolaService.GetAsync();
        }

        [HttpPost]
        public async Task CreateSchool([FromBody] NewEscolaModel escola)
        {
           await _escolaService.CreateAsync(escola); 
        }

        [HttpDelete("{codEscola}")]
        public async Task DeleteSchool(int codEscola)
        {
            await _escolaService.DeleteAsync(codEscola);
        }

        [HttpPut("{codEscola}")]
        public async Task UpdateSchool(int codEscola, [FromBody] NewEscolaModel escola)
        {
            await _escolaService.UpdateAsync(codEscola, escola);
        }

        [HttpGet("find-by-desc")]
        public async Task<List<EscolaModel>> GetByDescription([FromQuery] string desc)
        {
            return await _escolaService.GetByDescription(desc);
        }
    }
}
