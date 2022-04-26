using System;
using Livraria.Domain.Entidades;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Livraria.Domain.Interfaces.Repositorios
{
    public interface IRepositorioLivro : IDisposable
    {
        public Task<List<Livro>> GetLivrosAsync();

        public Task<Livro> GetLivroByIdAsync(int id);

        public Task<int> CriarLivroAsync(Livro novoLivro);

        public bool AtualizarLivro(Livro livroAlterado);

        public bool DeletarLivro(Livro livro);
    }
}
