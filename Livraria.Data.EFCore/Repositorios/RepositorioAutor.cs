using System;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Data.EFCore.Repositorios
{
    public class RepositorioAutor : IRepositorioAutor
    {
        private bool disposedValue;
        private readonly LivrariaDBContext _livrariaDBContext;

        public RepositorioAutor(LivrariaDBContext livrariaDBContext)
        {
            _livrariaDBContext = livrariaDBContext;  
        }
     
        public async Task<List<Autor>> GetAutoresAsync()
        {
            return await _livrariaDBContext.Autores.AsNoTracking().ToListAsync();
        }

        public async Task<Autor> GetAutorByIdAsync(int id)
        {
            return await _livrariaDBContext.Autores.AsNoTracking().SingleOrDefaultAsync(l => l.Codigo == id);
        }

        public async Task<int> CriarAutorAsync(Autor novoAutor)
        {
            try
            {
                await _livrariaDBContext.Autores.AddAsync(novoAutor);
                return novoAutor.Codigo;//vai ser sempre 0 no caso do ORM. Mas como tem q ter um retorno pra dar suporte a outras fontes de dados...
            }
            catch(Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public bool AtualizarAutor(Autor autorAlterado)
        {
            try
            {
                _livrariaDBContext.Autores.Update(autorAlterado);

                return true; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public bool DeletarAutor(Autor autor)
        {
            try
            {
                _livrariaDBContext.Autores.Remove(autor);

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
        // ~RepositorioAutor()
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
