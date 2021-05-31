using Examination.Areas.TestsSchools.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examination.Service
{
    public interface ICatalogService
    {
        Task<CatalogTestIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId);
     
    }
}