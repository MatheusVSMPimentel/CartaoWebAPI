using System;
using System.Collections.Generic;
using System.Linq; //LINQ
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; //Entity
using CartaoWebAPI.Data; //Context
using CartaoWebAPI.Models; //Models

namespace CartaoWebAPI.Controllers
{
    [Route("api/cartao")]
    [ApiController]
    public class CartaosController : Controller    {
        private readonly CartaoContext _context;

        public CartaosController(CartaoContext context)
        {
            //Construto inicializando o Dbcontext
            _context = context;
        }

        // GET: api/Cartao/email
        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<Cartao>>> GetCartaos(string email)
        {
            Usuario usuarioDb = new();
            usuarioDb =  _context.Usuarios.SingleOrDefault(bd => bd.UsuarioEmail == email);
            
            return await _context.Cartaos.Where(b => b.UsuarioId.Equals(usuarioDb.Id)).ToListAsync();
        }

        // POST: api/Cartaos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cartao>> PostCartao(Usuario usuario)
        {
            Cartao cartao = ConstrutoCartao(); //Chamada dos metodos para criar um cartão
            Usuario usuarioDb = new(); //instancia do usuario na função
            usuarioDb = _context.Usuarios.SingleOrDefault(bd => bd.UsuarioEmail == usuario.UsuarioEmail);
            if (usuarioDb == null) {
                //Se usuario não for encontrado no banco de dados ele será adicionado
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                usuarioDb = _context.Usuarios.SingleOrDefault(bd => bd.UsuarioEmail == usuario.UsuarioEmail);
                cartao.Usuario = usuarioDb;
                cartao.UsuarioId = usuarioDb.Id;
            }
            else { 
                //Se o usuario já existir associamos o cartão ao usuario
                cartao.Usuario = usuarioDb;
                cartao.UsuarioId = usuarioDb.Id;
            }
            //salvamos o cartão
            _context.Cartaos.Add(cartao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCartao), new { id = cartao.Id }, cartao);
        }

        private async Task<ActionResult<Cartao>> GetCartao(int id)
        {
            //função para encontrar cartão usada somente pela função post
            var cartao = await _context.Cartaos.FindAsync(id);

            if (cartao == null)
            {
                return NotFound();
            }

            return cartao;
        }

        private bool CartaoExists(int id)
        {
            return _context.Cartaos.Any(e => e.Id == id);
        }

        private Cartao ConstrutoCartao() {
            //Função Geradora de dados pro cartão
            DateTime now = DateTime.Now;
            Random randNum = new();
            Cartao cartao = new();
            for (int i = 0; i < 4; i++)
            {
                cartao.CartaoNumero += randNum.Next(1000, 9999).ToString();
            }
            for (int i = 0; i < 3; i++)
            {
                cartao.CartaoCVV += randNum.Next(0, 9).ToString();
            }
            for (int i = 0; i < 2; i++)
            {
                if (i == 1)
                {
                    cartao.CartaoValidade += "/";
                    cartao.CartaoValidade += randNum.Next(22, 99).ToString();
                }
                else
                {
                    cartao.CartaoValidade = randNum.Next(0, 12).ToString();
                }
            }
            cartao.CartaoCriacao = now;
            return cartao;
        }
    }
}
