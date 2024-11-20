﻿using CadastroFuncional.Models;
using CadastroFuncional.Services.LivroService;
using Microsoft.AspNetCore.Mvc;

namespace CadastroFuncional.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroInterface _livroInterface;
        public LivroController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetAllLivros()
        {
            IEnumerable<Livro> Livros = await _livroInterface.GetAllLivros();

            if(!Livros.Any())
                return NotFound("nenhum registro localizado!");

            return Ok(Livros);
        }

        [HttpGet("{livroId}")]
        public async Task<ActionResult<Livro>> GetLivroById(int livroId)
        {
            Livro livro = await _livroInterface.GetLivroById(livroId);

            if (livro == null)
                return NotFound("nenhum registro localizado!");

            return Ok(livro);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Livro>>> CreateLivro(Livro livro)
        {
            IEnumerable<Livro> livros = await _livroInterface.CreateLivro(livro);

            return Ok(livros);
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<Livro>>> UpdateLivro(Livro livro)
        {
            Livro registro = await _livroInterface.GetLivroById(livro.Id);

            if (registro == null)
                return NotFound("nenhum registro localizado!");

            IEnumerable<Livro> livros = await _livroInterface.UpdateLivro(livro);

            return Ok(livros);
        }

        [HttpDelete("{livroId}")]
        public async Task<ActionResult<IEnumerable<Livro>>> DeleteLivro(int livroId)
        {
            Livro registro = await _livroInterface.GetLivroById(livroId);

            if (registro == null)
                return NotFound("nenhum registro localizado!");

            IEnumerable<Livro> livros = await _livroInterface.DeleteLivro(livroId);

            return Ok(livros);
        }

    }
}
