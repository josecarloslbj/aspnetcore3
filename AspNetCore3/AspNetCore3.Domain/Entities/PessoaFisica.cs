using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore3.Domain.Entities
{
    public class PessoaFisica : BaseEntity
    {
        public string Nome { get; set; }
        public int Idade { get; set; }

        public int IdPessoa { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}
