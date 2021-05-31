using Examination.Data;

namespace Examination.Service.Catalog
{
    public class TestFilterSpecification : BaseSpecification<Test>
    {
        public TestFilterSpecification(int? ID)
            : base(i => (i.PartID == ID) )
        {
           

        }
    }

}