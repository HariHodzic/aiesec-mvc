using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Aiesec.Data.Context;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aiesec.Repository.IRepository;

namespace Aiesec.Web.Controllers
{
    public class BaseController<TEntity, TRequest, TResponse, TSearch, TKey> : Controller where TRequest : class, new()
    {
        protected readonly ICRUDRepository<TEntity, TResponse, TRequest, TSearch, TKey> _crudRepository;
        protected readonly IMapper _mapper;
        protected readonly AiesecDbContext _context;

        public BaseController(ICRUDRepository<TEntity, TResponse, TRequest, TSearch, TKey> crudRepository, AiesecDbContext context, IMapper mapper)
        {
            _crudRepository = crudRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _crudRepository.GetAllAsync());
        }
        public virtual IActionResult Create() {
            return View(new TRequest());
        }
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Insert([FromForm] TRequest model)
        {
            if (ModelState.IsValid)
            {
                await _crudRepository.InsertAsync(model);
                return RedirectToAction("Index");
            }
            return View("Create");
        }
        [HttpGet]
        public virtual async Task<IActionResult> Edit(TKey id)
        {
            var o = await _crudRepository.GetByIdAsyncResponse(id);

            return View(o);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [ActionName("Update")]
        public virtual async Task<IActionResult> Update([FromForm] TKey id,TResponse model)
        {
            if (ModelState.IsValid)
            {
                await _crudRepository.UpdateAsync(id, model);
                return RedirectToAction(nameof(Index));
            }
                return View("Edit");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(TKey id)
        {
            var o = await _crudRepository.GetByIdAsyncResponse(id);

            return View(o);
        }
        [HttpDelete]
        public virtual async Task<IActionResult> Remove(TKey Id)
        {
            await _crudRepository.DeleteAsync(Id);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> DetailsPartial(TKey id)
        {
            var o = await _crudRepository.GetByIdAsyncResponse(id);
            return PartialView(o);
        }
    }
}
