namespace LivrariaAPI.Models
{
    public class LivroAutor  //Entidade de Junção Livros x Autores(e vice-versa)
    {
        public int CodigoLivro { get; set; } //?????? duvida: como o entity framework vai saber q esse Codigo é o do livro? teria q ser o mesmo nome q está na classe Livro?
        public int CodigoAutor { get; set; }

        /*Propriedades de navegação colocadas de propósito para
          que o EF Core encontre os relacionamentos usando as regras de convenção.*/
        public Livro Livro { get; set; }
        public Autor Autor { get; set; }
    }
}
