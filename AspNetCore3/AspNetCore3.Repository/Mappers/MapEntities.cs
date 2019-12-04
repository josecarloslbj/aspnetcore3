using AspNetCore3.Domain.Entities;
using Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore3.Repository.Mappers
{
    public class TipoPessoaMap : DommelEntityMap<TipoPessoa>
    {
        public TipoPessoaMap()
        {
            ToTable("TipoPessoa");
            Map(x => x.Id).ToColumn("Id").IsIdentity();
            Map(x => x.Nome).ToColumn("Nome");
        }
    }

    public class PessoaMap : DommelEntityMap<Pessoa>
    {
        public PessoaMap()
        {
            ToTable("Pessoa");
            Map(x => x.Id).ToColumn("Id").IsIdentity();
            Map(x => x.TipoPessoa).Ignore();
        }
    }

    public class PessoaFisicaMap : DommelEntityMap<PessoaFisica>
    {
        public PessoaFisicaMap()
        {
            ToTable("PessoaFisica");
            Map(x => x.Id).ToColumn("Id").IsIdentity();
            Map(x => x.Pessoa).Ignore();
        }
    }

    public class UsuarioMap : DommelEntityMap<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("Usuario");
            Map(x => x.Id).ToColumn("Id").IsIdentity();
            Map(x => x.IdPessoaFisica).ToColumn("IdPessoa").IsIdentity();
            Map(x => x.PessoaFisica).Ignore();
            Map(x => x.Email).Ignore();
            Map(x => x.Nome).Ignore();

        }
    }
}
