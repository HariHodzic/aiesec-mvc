using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aiesec.Data.Context;
using Aiesec.Repository.IRepository;
using AutoMapper;
using Aiesec.Repository.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aiesec.Web.Controllers
{
    public class LocalCommitteeController : BaseController<Data.Model.BusinessModel.LocalCommittee, Data.DTO.Request.LocalCommittee, Data.DTO.Response.LocalCommittee, Data.DTO.Search.LocalCommittee, int>
    {
        public LocalCommitteeController(ILocalCommitteeService service, AiesecDbContext context, IMapper mapper):base(service, context,mapper)
        {

        }
        public override IActionResult Create()
        {
            ViewBag.Cities = _context.Cities.Select(x => new SelectListItem
            {
                Text = x.Name + ' ' + x.Postcode,
                Value = x.Id.ToString()
            }).ToList();
            return View(new Data.DTO.Request.LocalCommittee());
        }
        public override async Task<IActionResult> Insert([FromForm] Data.DTO.Request.LocalCommittee model)
        {
            if (ModelState.IsValid)
            {
                await _crudRepository.InsertAsync(model);
                return RedirectToAction("Index");
            }
            else { 
            ViewBag.Cities = _context.Cities.Select(x => new SelectListItem
            {
                Text = x.Name + ' ' + x.Postcode,
                Value = x.Id.ToString()
            }).ToList();
            return View("Create");
            }
        }
        [HttpGet]
        public override async Task<IActionResult> Edit(int id)
        {
            var o = await _crudRepository.GetByIdAsyncResponse(id);
            ViewBag.Cities = _context.Cities.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            return View(o);
        }
    }
}
