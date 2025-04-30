using DexianTest_back.Interfaces;
using DexianTest_back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DexianTest_back.Controllers
{
    [ApiController]
    [Authorize]
    [Route("students")]
    public class StudentsController
    {
        private readonly IAlunoService _alunoService;

        public StudentsController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet()]
        public async Task<List<AlunoModel>> GetAllStudents()
        {
            return await _alunoService.GetAsync();
        }

        [HttpPost]
        public async Task CreateStudent([FromBody] NewAlunoModel aluno)
        {
            await _alunoService.CreateAsync(aluno);
        }

        [HttpPut("{idAluno}")]
        public async Task UpdateStudent(string idAluno, [FromBody] NewAlunoModel aluno)
        {
            await _alunoService.UpdateAsync(idAluno, aluno);
        }

        [HttpDelete("{codAluno}")]
        public async Task DeleteStudent(int codAluno)
        {
            await _alunoService.DeleteAsync(codAluno);
        }

        [HttpGet("find-by-name")]
        public async Task<List<AlunoModel>> GetStudentByName([FromQuery] string name)
        {
            return await _alunoService.GetByName(name);
        }
         
    }
}
