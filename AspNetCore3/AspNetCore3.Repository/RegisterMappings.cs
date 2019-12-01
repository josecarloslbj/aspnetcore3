using AspNetCore3.Repository.Mappers;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore3.Repository
{
    public class RegisterMappings
    {
        public static string conexao = "";
        public static void Register()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new PlayerMap());
                config.AddMap(new TeamMap());
                config.ForDommel();
            });
        }
    }
}
