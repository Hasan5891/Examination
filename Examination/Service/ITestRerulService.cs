using Examination.Areas.TestsSchools.Models;
using Examination.Service.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Service.Core
{
    public interface ITestRerulService
    {
        Task<TestResulGroupModel> GetCatalogItems(int pageIndex, int itemsPage, int?PredmetsID, int?PartID);
       Task<TestResulGroupModel> GetCatalogItemsAdmin(int pageIndex, int itemsPage, int? PredmetsID, int? PartID, int? TestID);
        
    }
}
