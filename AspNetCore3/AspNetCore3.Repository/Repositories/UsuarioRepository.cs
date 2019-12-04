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
        public Usuario CriarUsuario(Usuario usuario)
        {
            try
            {
                using (var db = new SqlConnection(ConnectionString))
                {
                    var user = db.Select<Usuario>(q => q.Login == usuario.Login).FirstOrDefault();
                    if (user == null)
                    {
                        TipoPessoa tipoPessoa = db.Select<TipoPessoa>(q => q.Nome == "USUARIO").FirstOrDefault();
                        if (tipoPessoa == null)
                        {
                            tipoPessoa = new TipoPessoa();
                            tipoPessoa.Nome = "USUARIO";
                            tipoPessoa.Descricao = "Usuario do sistema";
                            tipoPessoa.DataInicio = DateTime.Now;
                            tipoPessoa.IdUsuario = null;
                            tipoPessoa.Status = 1;

                            var idTipoPessoa = db.Insert<TipoPessoa>(tipoPessoa);
                            tipoPessoa.Id = (int)(idTipoPessoa);
                        }

                        Pessoa pessoa = new Pessoa();
                        pessoa.Status = 1;
                        pessoa.DataInicio = DateTime.Now;
                        pessoa.IdTipoPessoa = tipoPessoa.Id;

                        var idPessoa = db.Insert<Pessoa>(pessoa);
                        pessoa.Id = (int)(idPessoa);


                        PessoaFisica pessoaFisica = db.Select<PessoaFisica>(q => q.IdPessoa == pessoa.Id).FirstOrDefault();
                        if (pessoaFisica == null)
                        {
                            pessoaFisica = new PessoaFisica();
                            pessoaFisica.Nome = usuario.Nome;
                            pessoaFisica.Idade = 31;
                            pessoaFisica.IdPessoa = pessoa.Id;

                            var idPessoaFisica = db.Insert<PessoaFisica>(pessoaFisica);
                            pessoaFisica.Id = (int)(idPessoaFisica);
                        }

                        user = new Usuario();
                        user.Login = usuario.Login;
                        user.Senha = usuario.Senha;
                        user.IdPessoaFisica = pessoaFisica.Id;

                        var idUsuario = db.Insert<Usuario>(user);
                        user.Id = (int)(idUsuario);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return usuario;
        }

    }
}
