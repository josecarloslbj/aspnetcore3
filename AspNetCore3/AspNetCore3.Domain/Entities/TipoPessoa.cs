using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore3.Domain.Entities
{
    public class TipoPessoa : BaseEntity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public int Status { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public int? IdUsuario { get; set; }
    }
}
