using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoList.ListaLeitura.Modelos;
using TodoList.ListaLeitura.Persistencia;
using Lista = TodoList.ListaLeitura.Modelos.ListaLeitura;

namespace TodoList.WebAPI.WebApp.Api
{
	[ApiController]
	[Route("[controller]")]
	public class ListasLeituraController : ControllerBase
	{
		private readonly IRepository<Livro> _repository;

		public ListasLeituraController(IRepository<Livro> repository)
		{
			_repository = repository;
		}

		private Lista CriarLista(TipoListaLeitura tipoLista)
		{
			return new Lista
			{
				Tipo = tipoLista.ParaString(),
				Livros = (IEnumerable<LivroApi>)_repository.All.Where(l => l.Lista == tipoLista).ToList()
			};
		}

		[HttpGet]
		public IActionResult TodasListas()
		{
			Lista paraLer = CriarLista(TipoListaLeitura.ParaLer);
			Lista lendo = CriarLista(TipoListaLeitura.Lendo);
			Lista lidos = CriarLista(TipoListaLeitura.Lidos);

			var colecao = new List<Lista> { paraLer, lendo, lidos };
			return Ok(colecao);
		}

		[HttpGet("{tipo}")]
		public IActionResult Recuperar(TipoListaLeitura tipo)
		{
			var lista = CriarLista(tipo);
			return Ok(lista);
		}

	}
}