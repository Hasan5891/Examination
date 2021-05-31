using Examination.Data;

namespace Examination.Service
{
    public class CatalogFilterSpecification : BaseSpecification<Test>
    {
        public CatalogFilterSpecification(int? PartId, int? PredID)
             : base(i => (!PartId.HasValue || i.PartID == PartId)&& (!PredID.HasValue || i.Part.PredmetID == PredID)&&i.isactive)
        {
            AddInclude(i => i.Questions);
            AddInclude(i => i.Part);
            AddInclude("Part.Predmet");
        }
    }

}