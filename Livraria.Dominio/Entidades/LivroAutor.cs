namespace Livraria.Domain.Entidades
{
    public class LivroAutor 
    {
        public int CodigoLivro { get; set; } 
        public int CodigoAutor { get; set; }
        public Livro Livro { get; set; }
        public Autor Autor { get; set; }
    }
}
