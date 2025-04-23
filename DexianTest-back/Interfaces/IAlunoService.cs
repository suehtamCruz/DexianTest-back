using DexianTest_back.Models;

namespace DexianTest_back.Interfaces
{
    public interface IAlunoService
    {
        Task<List<AlunoModel>> GetAsync();
        Task<AlunoModel?> GetByIdAsync(string id);
        Task CreateAsync(NewAlunoModel aluno);
        Task<bool> UpdateAsync(int codAluno, NewAlunoModel aluno);
        Task<bool> DeleteAsync(int codAluno);
    }
}
