using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore3.Domain.Entities
{
    public class Player : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }

        // declarar as colunas de chave estrangeira da tabela
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
