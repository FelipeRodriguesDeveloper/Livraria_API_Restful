using System;
using System.Threading.Tasks;

namespace Livraria.Domain.Interfaces
{
    public interface IUnidadeDeTrabalho : IDisposable
    {
        public Task<bool> CommitAsync();

        public Task RollBackAsync();
    }
}
