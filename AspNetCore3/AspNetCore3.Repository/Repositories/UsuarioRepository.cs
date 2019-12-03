using AspNetCore3.Domain.Contracts;
using AspNetCore3.Domain.Entities;
using Dommel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AspNetCore3.Repository.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public Usuario criarUsuario(Usuario usuario)
        {
            try
            {
                using (var db = new SqlConnection(ConnectionString))
                {
                    var usuarios = db.Select<Usuario>(q => q.Login == usuario.Login).ToList();
                    //var id = db.Insert(usuario);

                    //                    entity = GetById((int)id);
                }
            }
            catch (Exception ex)
            {

            }

            return usuario;
        }

    }
}
