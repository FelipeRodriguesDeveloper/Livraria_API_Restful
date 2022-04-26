using System;
using Livraria.Domain.Entidades;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Livraria.Domain.Interfaces.Repositorios
{
    public interface IRepositorioAutor : IDisposable
    {
        public Task<List<Autor>> GetAutoresAsync();

        public Task<Autor> GetAutorByIdAsync(int id);

        public Task<int> CriarAutorAsync(Autor novoAutor);

        public bool AtualizarAutor(Autor autorAlterado);

        public bool DeletarAutor(Autor autor);
    }
}
