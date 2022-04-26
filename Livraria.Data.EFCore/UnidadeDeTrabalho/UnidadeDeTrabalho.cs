using System;
using System.Threading.Tasks;
using Livraria.Domain.Interfaces;

namespace Livraria.Data.EFCore.UnidadeDeTrabalho
{
    public class UnidadeDeTrabalho : IUnidadeDeTrabalho
    {
        private bool disposedValue;
        private readonly LivrariaDBContext _livrariaDBContext;

        public UnidadeDeTrabalho(LivrariaDBContext livrariaDBContext)
        {
            _livrariaDBContext = livrariaDBContext;
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                var sucesso = (await _livrariaDBContext.SaveChangesAsync()) > 0;

                //Aqui poderia disparar eventos de Dominio etc... eventos que chamam alteracoes em outras tabelas etc...

                return sucesso;
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.InnerException.Message);//posso ficar lançar excecao aqui?
            }
        }

        public Task RollBackAsync()
        {
            //nao faz nada pois no EF nao precisa
            return Task.CompletedTask;
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
        // ~UnidadeDeTrabalho()
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
