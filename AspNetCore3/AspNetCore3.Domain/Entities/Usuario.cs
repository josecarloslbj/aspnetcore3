﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore3.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public int IdPessoaFisica { get; set; }

        public PessoaFisica PessoaFisica { get; set; }
    }
}
