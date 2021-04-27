using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TodoList.ListaLeitura.Modelos;
using TodoList.ListaLeitura.Persistencia;

namespace TodoList.WebAPI.WebApp.Api
{
	public class LivrosController : Controller
	{
		private readonly IRepository<Livro> _repository;

		public LivrosController(IRepository<Livro> repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public IActionResult Recuperar(int id)
		{
			var model = _repository.Find(id);
			if (model == null)
				return NotFound();

			return Json(model.ToModel());
		}

		[HttpPost]
		public IActionResult Incluir(LivroUpload livroUpload)
		{
			if (ModelState.IsValid)
			{
				var livro = livroUpload.ToLivro();
				_repository.Incluir(livro);

				var uri = Url.Action("Recuperar", new { id = livro.Id });

				return Created(uri, livro);
			}

			return BadRequest();
		}

		[HttpPost]
		public IActionResult Detalhes(LivroUpload model)
		{
			if (ModelState.IsValid)
			{
				var livro = model.ToLivro();
				if (model.Capa == null)
				{
					livro.ImagemCapa = _repository.All
						.Where(l => l.Id == livro.Id)
						.Select(l => l.ImagemCapa)
						.FirstOrDefault();
				}
				_repository.Alterar(livro);
				return Ok();//200
			}
			return BadRequest();
		}

		[HttpPost]
		public IActionResult Remover(int id)
		{
			var model = _repository.Find(id);
			if (model == null)
			{
				return NotFound();
			}
			_repository.Excluir(model);
			return NoContent();//203
		}
	}
}