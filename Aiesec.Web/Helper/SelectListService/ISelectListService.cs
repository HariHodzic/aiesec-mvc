using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aiesec.Web.Helper.SelectListService
{
    public interface ISelectListService
    {
        ValueTask<IEnumerable<SelectListItem>> Cities(bool includeChooseText = true);
        IEnumerable<SelectListItem> Roles(bool includeChooseText = true);
        ValueTask<IEnumerable<SelectListItem>> Genders(bool includeChooseText = true);
    }
}