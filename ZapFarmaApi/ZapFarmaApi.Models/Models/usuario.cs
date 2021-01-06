using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;
using ZapFarmaApi.Domain.Enuns;

namespace ZapFarmaApi.Domain.Models
{
    public class usuario
    {
        public ObjectId Id { get; set; }
        public string Nome { get; set; }
        public string Numero { get; set; }
        public int Stage { get; set; }
        public string  Mensagem { get; set; }
        public int OpcaoAtual { get; set; }
    }
}
