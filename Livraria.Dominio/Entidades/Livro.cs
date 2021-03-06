using System;
using System.Collections.Generic;

namespace Livraria.Domain.Entidades
{
    public class Livro
    {
        public int Codigo { get; set; }
        public string Titulo { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public decimal Preco { get; set; }
        public ICollection<LivroAutor> LivrosAutores { get; set; }
    }
}
