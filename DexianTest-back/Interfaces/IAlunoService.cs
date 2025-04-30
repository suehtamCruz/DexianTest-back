using DexianTest_back.Models;

namespace DexianTest_back.Interfaces
{
    public interface IAlunoService
    {
        Task<List<AlunoModel>> GetAsync(); 
        Task CreateAsync(NewAlunoModel aluno);
        Task<bool> UpdateAsync(string id, NewAlunoModel aluno);
        Task<bool> DeleteAsync(int codAluno);
        Task<List<AlunoModel>> GetByName(string name);
    }
}
