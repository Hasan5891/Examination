using Examination.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.Service.Catalog
{
    public class PartFilterSpecification : BaseSpecification<Part>
    {
        public PartFilterSpecification(int? PredId)
             : base(i => (!PredId.HasValue || i.PredmetID == PredId))
        {
        }
    }
}
    