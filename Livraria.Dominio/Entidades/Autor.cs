using System;
using System.Collections.Generic;

namespace Livraria.Domain.Entidades
{
    public class Autor
    {
        public int Codigo { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public ICollection<LivroAutor> LivrosAutores { get; set; }
    }
}
