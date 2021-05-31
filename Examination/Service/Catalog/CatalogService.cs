using Examination.Areas.TestsSchools.Models;
using Examination.Data;
using Examination.Service.Catalog;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Service
{
    public class CatalogService : ICatalogService
    {
      //  private readonly ILogger<CatalogService> _logger;
        private readonly IRepository<Test> _itemRepository;
        private readonly IAsyncRepository<Part> _brandRepository;
        private readonly IAsyncRepository<Predmet> _typeRepository;
        

        public CatalogService(
             IRepository<Test> itemRepository,
            IAsyncRepository<Part> brandRepository,
            IAsyncRepository<Predmet> typeRepository
          )
        {
             _itemRepository = itemRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
           
        }

        public async Task<CatalogTestIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? PartId, int? PredID)
        {
           

            var filterSpecification = new CatalogFilterSpecification(PartId, PredID);
            var root = _itemRepository.List(filterSpecification);

            var totalItems = root.Count();

            var itemsOnPage = root
                .Skip(itemsPage * pageIndex)
                .Take(itemsPage)
                .ToList();



            var vm = new CatalogTestIndexViewModel()
            {
                CatalogItems = itemsOnPage.Select(i => new CatalogTestitemViewModel()
                {

                    ID = i.ID,
                    Nomi = i.Nomi,
                    Predmet=i.Part.Predmet.Nomi,
                    Soni=i.Questions?.Count()
                    

                })
            ,
                
                Parts = await GetParts(PredID),

                PredmetsFilterApplied = PredID,
                PartFilterApplied = PartId,
              Predmet= PredID.HasValue ? (await _typeRepository.GetByIdAsync(PredID.Value)).Nomi :"Hamma fanlar",

              
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = itemsOnPage.Count,
                    TotalItems = totalItems,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return vm;
        }

        public async Task<IEnumerable<SelectListItem>> GetParts(int? Pred)
        {
            // _logger.LogInformation("GetBrands called.");
            var filterSpecification = new PartFilterSpecification(Pred);
            var brands = await _brandRepository.ListAsync(filterSpecification);

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






    }
}
