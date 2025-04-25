using DexianTest_back.Models;

namespace DexianTest_back.Interfaces
{
    public interface IEscolaService
    {
        Task<List<EscolaModel>> GetAsync();
        Task<EscolaModel?> GetByIdAsync(string id);
        Task CreateAsync(NewEscolaModel escola);
        Task<bool> UpdateAsync(int codEscola, NewEscolaModel escola);
        Task<bool> DeleteAsync(int codEscola);
        Task<List<EscolaModel>> GetByDescription(string desc);
    }
}
