using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartaoWebAPI.Models
{
    public class Cartao
    {
        public int Id { get; set; }
        public string CartaoNumero { get; set; }
        public string CartaoCVV { get; set; }
        public DateTime CartaoCriacao { get; set; }
        public DateTime CartaoExclusao { get; set; }
        public string CartaoValidade { get; set; }
        /*UsuarioId e Usuario fazendo referencia a tabela usuarios como FK
         * onde um cartão só pode ter um usuario e um usuario pode ter N cartões*/
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
