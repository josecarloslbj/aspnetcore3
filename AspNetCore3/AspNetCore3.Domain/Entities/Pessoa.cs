using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore3.Domain.Entities
{
    public class Pessoa : BaseEntity
    {
        public int IdTipoPessoa { get; set; }

        public int Status { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public TipoPessoa  TipoPessoa { get; set; }
    }
}
