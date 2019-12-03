using AspNetCore3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore3.Domain.Contracts
{
    public interface IUsuarioRepository 
    {
        Usuario criarUsuario(Usuario usuario);
    }
}
