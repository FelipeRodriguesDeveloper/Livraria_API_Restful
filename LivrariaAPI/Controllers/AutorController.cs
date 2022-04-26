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
    [Route("api/v1/autores")]   
    public class AutorController : ControllerBase
    {
        /// <summary>
        /// Consulta todos os autores.
        /// </summary>
        /// <returns>Uma lista de autores</returns>
        /// <response code="200">Sucesso - Retorna uma lista de autores.</response>
        [HttpGet("")]  //GET api/v1/autores
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAutoresAsync([FromServices] IRepositorioAutor repositorioAutor)
        {
            var listaAutores = await repositorioAutor.GetAutoresAsync();

            return Ok(listaAutores);
        }

        /// <summary>
        /// Consulta um autor específico.
        /// </summary>
        /// <response code="404">Not Found - Autor não encontrado.</response>
        /// <response code="200">Sucesso - Retorna o autor encontrado.</response>
        [HttpGet("{id:int}")] //GET api/v1/autores/3
        [ProducesResponseType(StatusCodes.Status404NotFound)]  
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Autor))] 
        public async Task<IActionResult> GetAutorByIdAsync([FromServices] IRepositorioAutor repositorioAutor,
                                                           [FromRoute] int id)
        {
            var autor = await repositorioAutor.GetAutorByIdAsync(id);

            return autor == null ? NotFound("Autor não encontrado!") : Ok(autor);
        }

        /// <summary>
        /// Cria um novo autor.
        /// </summary>
        /// <response code="400">Bad Request - Erro na requisição.</response>
        /// <response code="201">Sucesso - Retorna o autor criado.</response>
        /// <response code="500">Server Error.</response>
        [HttpPost("")] //POST api/v1/autores
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarAutorAsync([FromServices] IRepositorioAutor repositorioAutor,
                                                         [FromServices] IUnidadeDeTrabalho unidadeDeTrabalho,
                                                         [FromBody] CreateAutorInputModel autorInputModel)
        {
            var novoAutor = new Autor
            {
                Nome = autorInputModel.Nome,
                CPF = autorInputModel.CPF,
                DataNascimento = autorInputModel.DataNascimento,
                LivrosAutores = null
            };

            try
            {
                //No caso de ORM(EF) esse codigo vai receber sempre zero, porem se trocar a fonte de dados será necessario.
                novoAutor.Codigo = await repositorioAutor.CriarAutorAsync(novoAutor);
                await unidadeDeTrabalho.CommitAsync();

                return Created($"api/v1/autores/{novoAutor.Codigo}", novoAutor);
            }
            catch (Exception ex)
            {
                await unidadeDeTrabalho.RollBackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um autor específico.
        /// </summary>
        /// <response code="400">Bad Request - Erro na requisição.</response>
        /// <response code="404">Not Found - Autor não encontrado.</response>
        /// <response code="200">Sucesso - Retorna o autor alterado.</response>
        /// <response code="500">Server Error.</response>
        [HttpPut("{id:int}")] //PUT api/v1/autores
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Autor))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarAutorAsync([FromServices] IRepositorioAutor repositorioAutor,
                                                             [FromServices] IUnidadeDeTrabalho unidadeDeTrabalho,
                                                             [FromBody] UpdateAutorInputModel autorInputModel,
                                                             [FromRoute] int id)
        {
            var autor = await repositorioAutor.GetAutorByIdAsync(id);

            if (autor == null)
                return NotFound("Autor não encontrado!");

            autor.Nome = autorInputModel.Nome;
            autor.CPF = autorInputModel.CPF;
            autor.DataNascimento = autorInputModel.DataNascimento;

            try
            {
                repositorioAutor.AtualizarAutor(autor);
                await unidadeDeTrabalho.CommitAsync();
             
                return Ok(autor);
            }
            catch (Exception ex)
            {
                await unidadeDeTrabalho.RollBackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deleta um autor especíico.
        /// </summary>
        /// <response code="404">Not Found - Autor não encontrado.</response>
        /// <response code="200">Sucesso</response>
        /// <response code="500">Server Error.</response>
        [HttpDelete("{id:int}")] //DELETE api/v1/autores/3
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarAutorAsync([FromServices] IRepositorioAutor repositorioAutor,
                                                           [FromServices] IUnidadeDeTrabalho unidadeDeTrabalho,
                                                           [FromRoute] int id)
        {
            var autor = await repositorioAutor.GetAutorByIdAsync(id);

            if (autor == null)
                return NotFound("Autor não encontrado!");

            try
            {
                repositorioAutor.DeletarAutor(autor);
                await unidadeDeTrabalho.CommitAsync();

                return Ok("Autor deletado com Sucesso!");
            }
            catch (Exception ex)
            {
                await unidadeDeTrabalho.RollBackAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
