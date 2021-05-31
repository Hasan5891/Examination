using Examination.Areas.TestsSchools.Models;
using Examination.Data;
using Examination.Service.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Service.Catalog
{
    public class TestResultService : ITestRerulService
    {
        //  private readonly ILogger<CatalogService> _logger;
        private readonly IAsyncRepository<UserExams> _itemRepository;
        private readonly IAsyncRepository<Test> _TestRepository;
       
        private readonly IAsyncRepository<Part> _PartRepository;
        private readonly IAsyncRepository<Predmet> _PredmetRepository;
       


        public TestResultService(
             IAsyncRepository<UserExams> itemRepository, IAsyncRepository<Test> TestRepository, IAsyncRepository<Part> gradedRepository, IAsyncRepository<Predmet> PredmetRepository)
        {
            _itemRepository = itemRepository;
            _TestRepository = TestRepository;
            _PartRepository = gradedRepository;
            _PredmetRepository = PredmetRepository;
           




        }

        public async Task<TestResulGroupModel> GetCatalogItems(int pageIndex, int itemsPage,  int? PredmetsID, int? PartID)
        {

            


            var filterSpecification = new TestResultFilterSpecification(PredmetsID, PartID);
            

            var root = await _itemRepository.ListAsync(filterSpecification);
             var totalItems = root.Count();
            var vm = new TestResulGroupModel()
            {
                Users = root.Select(i => new Useritem()
                {
                    ExamID = i.ID,
                    TestNomi = i.Test.Nomi,
                    PredmetNomi = i.Test.Part.Predmet.Nomi,
                    FISH = i.UserName,
                    T = i.AnsweredQuesttions.Where(p => p.Answer != null ? (p.Answer.isTrue) : false).Count(),
                    Soni = i.Test.Questions.Count(),
                    Answered = i.AnsweredQuesttions.Select(n => (n.Answer != null) ? (n.Answer.isTrue ? "btn - success" : "btn-danger") : "btn - warning").ToList()

                }).Skip(itemsPage * pageIndex)
                .Take(itemsPage)
                .ToList(),

                Predmets = await GetPredmets(),
                Parts = await GetParts(PredmetsID),
                Predmet =  PredmetsID.HasValue ? (await _PredmetRepository.GetByIdAsync(PredmetsID.Value)).Nomi : "Hamma fanlar",
                PredmetsFilterApplied = PredmetsID,
                PartsFilterApplied = PartID,
               
             
            };
            vm.PaginationInfo = new PaginationInfoViewModel()
            {
                ActualPage = pageIndex,
                ItemsPerPage = itemsPage,
                TotalItems = totalItems,
                TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
            };
            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "disabled" : "";
            
            return vm;
        }

        public async Task<TestResulGroupModel> GetCatalogItemsAdmin(int pageIndex, int itemsPage, int? PredmetsID, int? PartID, int? TestID)
        {




            var filterSpecification = new TestResultFilterSpecification(PredmetsID, PartID, TestID);
            var root = await _itemRepository.ListAsync(filterSpecification);
            var totalItems = root.Count();
            var vm = new TestResulGroupModel()
            {
                Users = root.Select(i => new Useritem()
                {
                    ExamID = i.ID,
                    TestNomi = i.Test.Nomi,
                    PredmetNomi = i.Test.Part.Predmet.Nomi,
                    FISH = i.UserName,
                    T = i.AnsweredQuesttions.Where(p => p.AnswerID != null ? (p.Answer.isTrue) : false).Count(),
                    Soni = i.AnsweredQuesttions.Count(),
                    Answered = i.AnsweredQuesttions.Select(n => (n.AnswerID != null) ? (n.Answer.isTrue ? "btn-success" : "btn-danger") : "btn-warning").ToList()

                }).OrderByDescending(p => p.T).Skip(itemsPage * pageIndex)
                .Take(itemsPage)
                .ToList(),

                Predmets = await GetPredmets(),
                Parts = await GetParts(PredmetsID),
           
               Tests = await GetTests(PartID),
                PredmetsFilterApplied = PredmetsID,
                PartsFilterApplied = PartID,
                TestsFilterApplied = TestID,

            };
            vm.PaginationInfo = new PaginationInfoViewModel()
            {
                ActualPage = pageIndex,
                ItemsPerPage = itemsPage,
                TotalItems = totalItems,
                TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
            };
            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "disabled" : "";

            return vm;
        }
      

        private async Task<IEnumerable<SelectListItem>> GetTests(int? PartID)
        {
            var filterSpecification = new TestFilterSpecification(PartID);
            var brands = await _TestRepository.ListAsync(filterSpecification);

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "Barchasi", Selected = true }
            };
            foreach (Test brand in brands)
            {
                items.Add(new SelectListItem() { Value = brand.ID.ToString(), Text = brand.Nomi });
            }

            return items;
        }
       


        public async Task<IEnumerable<SelectListItem>> GetParts(int? PredmetsID)
        {
            
            var filterSpecification = new PartFilterSpecification(PredmetsID);
            var brands = await _PartRepository.ListAsync(filterSpecification);

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "Barchasi", Selected = true }
            };
            foreach (Part brand in brands)
            {
                items.Add(new SelectListItem() { Value = brand.ID.ToString(), Text = brand.Nomi });
            }

            return items;
        }
       

        public async Task<IEnumerable<SelectListItem>> GetPredmets()
        {

            var types = await _PredmetRepository.ListAllAsync();
            var items = new List<SelectListItem>(){
                new SelectListItem() { Value = null, Text = "Barchasi", Selected = true }
            };

            foreach (Predmet type in types)
            {
                items.Add(new SelectListItem() { Value = type.ID.ToString(), Text = type.Nomi });
            }

            return items;
        }




    }
}
