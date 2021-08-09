using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aiesec.Data.Model.IdentityModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aiesec.Web.Helper.SelectListService
{
    public class SelectListService : ISelectListService
    {
        public IEnumerable<SelectListItem> BaseSelectListItem(bool includeChooseText, string chooseText,
            IEnumerable<SelectListItem> selectListItems)
        {
            var items = new List<SelectListItem>();
            if (includeChooseText)
                items.Add(new SelectListItem {Text = chooseText, Value = string.Empty});
            if (selectListItems != null)
                items.AddRange(selectListItems);
            return items;
        }

        public ValueTask<IEnumerable<SelectListItem>> Cities(bool includeChooseText = true)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<SelectListItem> Roles(bool includeChooseText = true)
        {
            return BaseSelectListItem(true, "Roles",
                SystemRoles.Roles.Select((role, i) => new SelectListItem(role, role)));
        }

        public ValueTask<IEnumerable<SelectListItem>> Genders(bool includeChooseText = true)
        {
            throw new System.NotImplementedException();
        }
    }
}