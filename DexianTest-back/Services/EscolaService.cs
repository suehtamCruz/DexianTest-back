using DexianTest_back.Interfaces;
using DexianTest_back.Models;
using System.Collections.Generic;

namespace DexianTest_back.Services
{ 
    public class EscolaService : IEscolaService
    { 
        private static readonly List<EscolaModel> _escolasStatic = new List<EscolaModel>();
         
        private readonly List<EscolaModel> _escolas;

        public EscolaService()
        { 
            _escolas = _escolasStatic;
        }

        public async Task CreateAsync(NewEscolaModel escola)
        {
            if (_escolas.Any(x => x.ICodEscola == escola.Code))
            {
                throw new InvalidOperationException($"Escola com id {escola.Code} já existe!");
            }
            
            var newSchool = new EscolaModel
            {
                ICodEscola = escola.Code,
                SDescricao = escola.Description,
                Id = Guid.NewGuid().ToString()
            };

            _escolas.Add(newSchool);
            
            await Task.CompletedTask;
        }

        public async Task<bool> DeleteAsync(int codEscola)
        {
            var escola = _escolas.Find(x => x.ICodEscola == codEscola);
            if (escola == null)
            {
                throw new KeyNotFoundException($"Escola com código {codEscola} não encontrada!");
            }

            _escolas.Remove(escola);
            
            return true;
        }

        public async Task<List<EscolaModel>> GetAsync()
        {
            return await Task.FromResult(_escolas);
        }

        public Task<bool> UpdateAsync(int codEscola, NewEscolaModel escola)
        {
            var existingEscola = _escolas.Find(x => x.ICodEscola == codEscola);
            if (existingEscola == null)
            {
                throw new KeyNotFoundException($"Escola com código {codEscola} não encontrada!");
            }

            if (escola.Code != 0 && escola.Code != codEscola)
            { 
                if (_escolas.Any(x => x.ICodEscola == escola.Code))
                {
                    throw new InvalidOperationException($"Escola com código {escola.Code} já existe!");
                }
                
                existingEscola.ICodEscola = escola.Code;
            }
            
            if (!string.IsNullOrWhiteSpace(escola.Description))
            {
                existingEscola.SDescricao = escola.Description;
            }
            
            return Task.FromResult(true);
        }

        public async Task<List<EscolaModel>> GetByDescription(string desc)
        {
            var escolas = _escolas
                .Where(x => x.SDescricao.Contains(desc))
                .ToList();
                
            if (escolas.Count == 0)
            {
                throw new KeyNotFoundException($"Escola não encontrada!");
            }
            
            return await Task.FromResult(escolas);
        }
    }
}
