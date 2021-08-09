using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aiesec.Data.Context;
using AutoMapper;
using Aiesec.Repository;
using Aiesec.Repository.Repository;
using Aiesec.Data.Model.BusinessModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Linq.Dynamic.Core;   

namespace Aiesec.Web.Controllers
{
    public class OfficeController : BaseController<Data.Model.BusinessModel.Office,Data.DTO.Request.Office,Data.DTO.Response.Office,Data.DTO.Search.Office,int>
    {

        public OfficeController(IOfficeService crudRepository,AiesecDbContext context, IMapper mapper):base(crudRepository,context, mapper)
        {

        }
        [Authorize(Roles ="Admin, Super Admin")]
        public override IActionResult Create()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (City c in _context.Cities.ToList())
            {
                list.Add(new SelectListItem
                {
                    Text = c.Name +' ' + c.Postcode,
                    Value = c.Id.ToString()

                }) ;
            }
            ViewBag.CityID = list;
            ViewBag.LCs = _context.LocalCommittee.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.City.Name
            }).ToList();
            return View(new Data.DTO.Request.Office());
        }

        public override async Task<IActionResult> Insert([FromForm] Data.DTO.Request.Office model)
        {
            if (ModelState.IsValid)
            {
                await _crudRepository.InsertAsync(model);
                return RedirectToAction("Index");
            }
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (City c in _context.Cities.ToList())
            {
                list.Add(new SelectListItem
                {
                    Text = c.Name + ' ' + c.Postcode,
                    Value = c.Id.ToString()

                });
            }
            ViewBag.CityID = list;
            ViewBag.LCs = _context.LocalCommittee.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.City.Name
            }).ToList();
            return View("Create");
        }

        [HttpPost]
        public IActionResult GetFilteredItems()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][data]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var officeData = (from tempoffice in _context.Offices.Include(x => x.City) select tempoffice);
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                officeData = officeData.OrderBy<Office>(sortColumn + " " + sortColumnDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                officeData = officeData.Where(m => m.Address.Contains(searchValue)
                                            || m.Capacity.ToString().Contains(searchValue)
                                            || m.City.Name.Contains(searchValue));
            }
            recordsTotal = officeData.Count();
            var data = officeData.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);
        }

        [HttpGet]
        public override async Task<IActionResult> Edit(int id)
        {
            var o = await _crudRepository.GetByIdAsyncResponse(id);
            ViewBag.Cities = _context.Cities.Select(x=>new SelectListItem { Value=x.Id.ToString(), Text=x.Name}).ToList();
            ViewBag.LCs = _context.LocalCommittee.Include(x=>x.Offices).Select(x=>new SelectListItem { Value=x.Id.ToString(), Text=x.City.Name}).ToList();
            return View(o);
        }
        public async Task<IActionResult> PartialOfficesLC(int id)
        {
            var o= await _context.Offices.Where(x => x.LocalCommitteeId == id)
                .Include(x => x.City)
                .ToListAsync();
            
            return PartialView(_mapper.Map<IEnumerable<Data.DTO.Response.Office>>(o));
        }
        
    }
}
