using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces;
using Livraria.Domain.Interfaces.Repositorios;
using LivrariaAPI.InputModels;

namespace LivrariaAPI.Controllers
{
    /*
    (Esta API segue em fase de desenvolvimento...)
    Ainda faltam middleware, etc...

    Esta API Restful foi desenvolvida com:
    
    - ASP.NET Core 5.0
    - Entity Framework Core 5.0.16
    - SQL Server 
    - Fluent API + Data Annotation 
    - Swagger + OpenAPI
    - Padrões Repository e Unit of Work

    Desenvolvedor: Felipe Rodrigues
    GitHub:https://github.com/FelipeRodriguesDeveloper
    Linkedin:https://br.linkedin.com/in/felipe-rodrigues-programador
    Email: feliperodriguesdeveloper@hotmail.com
    */

    [ApiController]
    [Route("api/v1/livros")]
    public class LivroController : ControllerBase
    {
        /// <summary>
        /// Consulta todos os livros.
        /// </summary>
        /// <returns>Uma lista de livros</returns>
        /// <response code="200">Sucesso - Retorna uma lista de livros.</response>
        [HttpGet("")] //GET api/v1/livros
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLivrosAsync([FromServices] IRepositorioLivro repositorioLivro)
        {
            var listaLivros = await repositorioLivro.GetLivrosAsync();

            return Ok(listaLivros);
        }

        /// <summary>
        /// Consulta um livro específico.
        /// </summary>
        /// <response code="404">Not Found - Livro não encontrado.</response>
        /// <response code="200">Sucesso - Retorna o livro encontrado.</response>
        [HttpGet("{id:int}")] //GET api/v1/livros/3
        [ProducesResponseType(StatusCodes.Status404NotFound)]  
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Livro))]
        public async Task<IActionResult> GetLivroByIdAsync([FromServices] IRepositorioLivro repositorioLivro,
                                                           [FromRoute] int id)
        {
            var livro = await repositorioLivro.GetLivroByIdAsync(id);

            return livro == null ? NotFound("Livro não encontrado!") : Ok(livro);
        }

        /// <summary>
        /// Cria um novo livro.
        /// </summary>
        /// <response code="400">Bad Request - Erro na requisição.</response>
        /// <response code="201">Sucesso - Retorna o livro criado.</response>
        /// <response code="500">Server Error.</response>
        [HttpPost("")]  //POST api/v1/livros
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarLivroAsync([FromServices] IRepositorioLivro repositorioLivro,
                                                         [FromServices] IUnidadeDeTrabalho unidadeDeTrabalho,
                                                         [FromBody] CreateLivroInputModel livroInputModel)
        {
            var novoLivro = new Livro
            {
                Titulo = livroInputModel.Titulo,
                Preco = livroInputModel.Preco,
                LivrosAutores = null
            };

            try
            {
                //No caso de ORM(EF) esse codigo vai receber sempre zero, porem se trocar a fonte de dados será necessario.
                novoLivro.Codigo = await repositorioLivro.CriarLivroAsync(novoLivro); 
                await unidadeDeTrabalho.CommitAsync();

                return Created($"api/v1/livros/{novoLivro.Codigo}", novoLivro);
            }
            catch (Exception ex)
            {
                await unidadeDeTrabalho.RollBackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um livro específico.
        /// </summary>
        /// <response code="400">Bad Request - Erro na requisição.</response>
        /// <response code="404">Not Found - Livro não encontrado.</response>
        /// <response code="200">Sucesso - Retorna o livro alterado.</response>
        /// <response code="500">Server Error.</response>
        [HttpPut("{id:int}")] //PUT api/v1/livros/3
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Livro))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarLivroAsync([FromServices] IRepositorioLivro repositorioLivro,
                                                             [FromServices] IUnidadeDeTrabalho unidadeDeTrabalho,
                                                             [FromBody] UpdateLivroInputModel livroInputModel,
                                                             [FromRoute] int id)
        {
            var livro = await repositorioLivro.GetLivroByIdAsync(id);

            if (livro == null)
                return NotFound("Livro não encontrado!");

            livro.Titulo = livroInputModel.Titulo;
            livro.Preco = livroInputModel.Preco;

            try
            {
                repositorioLivro.AtualizarLivro(livro);
                await unidadeDeTrabalho.CommitAsync();

                return Ok(livro);
            }
            catch (Exception ex)
            {
                await unidadeDeTrabalho.RollBackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deleta um livro especíico.
        /// </summary>
        /// <response code="404">Not Found - Livro não encontrado.</response>
        /// <response code="200">Sucesso</response>
        /// <response code="500">Server Error.</response>
        [HttpDelete("{id:int}")] //DELETE api/v1/livros/3
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarLivroAsync([FromServices] IRepositorioLivro repositorioLivro,
                                                           [FromServices] IUnidadeDeTrabalho unidadeDeTrabalho, 
                                                           [FromRoute] int id)
        {
            var livro = await repositorioLivro.GetLivroByIdAsync(id);

            if (livro == null)
                return NotFound("Livro não encontrado!");

            try
            {
                repositorioLivro.DeletarLivro(livro);
                await unidadeDeTrabalho.CommitAsync();

                return Ok("Livro deletado com Sucesso!");
            }
            catch (Exception ex)
            {
                await unidadeDeTrabalho.RollBackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
