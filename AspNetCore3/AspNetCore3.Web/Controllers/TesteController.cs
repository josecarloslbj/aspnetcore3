using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore3.Domain.Entities;
using AspNetCore3.Repository;
using AspNetCore3.Repository.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore3.Web.Controllers
{
    [Route("api/[controller]")]
    [Route("teste")]
    public class TesteController : Controller
    {
        private readonly PlayerRepository _playerRepository;

        public TesteController()
        {
            _playerRepository = new PlayerRepository();
           // RegisterMappings.Register();
        }

        [AllowAnonymous]
        public IActionResult Index()
        {

            var player_novo = new Player
            {
                Age = 38,
                Name = "NOVO JOGADOR",
                TeamId = 2
            };
            _playerRepository.Insert(ref player_novo);

            var tst = _playerRepository.GetList(q => q.Name == "RICARDO").ToList();

            var player = _playerRepository.GetById(2);




            var player_edit = _playerRepository.GetById(4);
            if (player_edit != null)
            {
                player_edit.Name = "FILIPE ALTERADO";
                _playerRepository.Update(player_edit);
            } 
           


            _playerRepository.Delete(5);

            var result = _playerRepository.GetAll().ToList();

            return Ok(new { retorno = result });
        }
    }
}