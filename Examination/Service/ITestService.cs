using Examination.Areas.TestsSchools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Service
{
    public interface ITestService
    {
        TestAnswerModel GetCatalogItems(int pageIndex, int itemsPage, TestAnswerModel items);
        
    }
}
