using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ZapFarmaApi.Domain.Models;
using ZapFarmaApi.Domain.Enuns;

namespace ZapFarmaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private IConfiguration configuration;

        public RequestController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private string GerarToken()
        {
            Random random = new Random();
            string strarray = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            return new string(Enumerable.Repeat(strarray, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpGet]
        [Route("GetOpcao")]
        public string GetOpcao(string number, int opcao)
        {
            if (opcao == Opcaoes.Token)
            {
                var token = GerarToken();
                FinalizaStage(number, 1);
                return token;
            }
            else if (opcao == Opcaoes.Suporte)
            {
                FinalizaStage(number, 1);
                return string.Empty;
            }
            else if (opcao == Opcaoes.Comprar)
            {
                FinalizaStage(number, 3);
                return string.Empty;
            }
            //else if (opcao == Opcaoes.Promocao)
            //{

            //}
            else
            {
                return Opcaoes.Invalida.ToString();
            }
        }

        [HttpGet]
        [Route("GetStage")]
        public string GetStage(string number, string nome)
        {
            usuario user;
            using (var db = new LiteDatabase(configuration["PathDB"]))
            {                                
                var usuarios = db.GetCollection<usuario>("usuarios");

                var usuario = usuarios.Query().ToList().FirstOrDefault(x => x.Numero.Contains(number));
                if (usuario != null)
                    user = usuario;
                else
                {
                    user = new usuario()
                    {
                        Nome = nome,
                        Numero = number,
                        OpcaoAtual = 0,
                        Stage = Stages.PrimeiroAcesso
                    };
                    usuarios.Insert(user);
                }
            }            
            return user.Stage.ToString();
        }
        
        private void FinalizaStage(string number, int stage) {
            try
            {
                using (var db = new LiteDatabase(configuration["PathDB"]))
                {
                    var usuarios = db.GetCollection<usuario>("usuarios");

                    var usuario = usuarios.Query().ToList().FirstOrDefault(x => x.Numero.Contains(number));
                    usuario.Stage = stage;

                    usuarios.Update(usuario);
                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
        }

        [HttpPost]
        public void AtualizaStage(string number)
        {
            try
            {         
                using (var db = new LiteDatabase(configuration["PathDB"]))
                {
                    var usuarios = db.GetCollection<usuario>("usuarios");

                    var usuario = usuarios.Query().ToList().FirstOrDefault(x => x.Numero.Contains(number));
                    usuario.Stage += 1;

                    usuarios.Update(usuario);
                }
            }
            catch (Exception ex)
            {
               var a = ex.Message;    
            }
        }   
    }
}
