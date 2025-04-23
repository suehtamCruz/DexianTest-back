using DexianTest_back.Interfaces;
using DexianTest_back.Models;
using Microsoft.AspNetCore.Mvc;

namespace DexianTest_back.Controllers
{
    [ApiController]
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

        [HttpPut("{codAluno}")]
        public async Task UpdateStudent(int codAluno, [FromBody] NewAlunoModel aluno)
        {
            await _alunoService.UpdateAsync(codAluno, aluno);
        }

        [HttpDelete("{codAluno}")]
        public async Task DeleteStudent(int codAluno)
        {
            await _alunoService.DeleteAsync(codAluno);
        }
    }
}
