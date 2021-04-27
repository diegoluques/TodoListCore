using Microsoft.AspNetCore.Mvc;
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
	}
}