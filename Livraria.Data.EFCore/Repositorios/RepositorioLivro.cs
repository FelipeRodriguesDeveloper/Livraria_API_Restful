using System;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Data.EFCore.Repositorios
{
    public class RepositorioLivro : IRepositorioLivro
    {
        private bool disposedValue;
        private readonly LivrariaDBContext _livrariaDBContext;
        
        public RepositorioLivro(LivrariaDBContext livrariaDBContext)
        {
            _livrariaDBContext = livrariaDBContext;
        }

        public async Task<List<Livro>> GetLivrosAsync()
        {
            return await _livrariaDBContext.Livros.AsNoTracking().ToListAsync();
        }

        public async Task<Livro> GetLivroByIdAsync(int id)
        {
            return await _livrariaDBContext.Livros.AsNoTracking().SingleOrDefaultAsync(l => l.Codigo == id);
        }

        public async Task<int> CriarLivroAsync(Livro novoLivro)
        {
            try
            {
                await _livrariaDBContext.Livros.AddAsync(novoLivro);
                return novoLivro.Codigo;//vai ser sempre 0 no caso do ORM. Mas como tem q ter um retorno pra dar suporte a outras fontes de dados...
            }
            catch(Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public bool AtualizarLivro(Livro livroAlterado)
        {
            try
            {
                _livrariaDBContext.Livros.Update(livroAlterado);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public bool DeletarLivro(Livro livro)
        {
            try
            {
                _livrariaDBContext.Livros.Remove(livro);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~RepositorioLivro()
        // {
        //     // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
