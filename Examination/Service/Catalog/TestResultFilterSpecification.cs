using Examination.Data;

namespace Examination.Service.Catalog
{
    internal class TestResultFilterSpecification : BaseSpecification<UserExams>
    {
        public TestResultFilterSpecification(int? PredmetsID, int? PartID, int UserID=0)  :
            base(i => (!PartID.HasValue || i.Test.PartID == PartID) && (!PredmetsID.HasValue || i.Test.Part.PredmetID == PredmetsID)&&( !(i.TestID == null)))

        {
            AddInclude("AnsweredQuesttions.Question.Answers");
            AddInclude("Test.Part.Predmet");
            AddInclude("AnsweredQuesttions.Answer");
            AddInclude(i => i.Test.Questions);
           
        }
        public TestResultFilterSpecification(int? PredmetsID, int? PartID, int? TestID, int UserID=0 ) :
          base(i => (!PartID.HasValue || i.Test.PartID == PartID) && (!TestID.HasValue || i.TestID == TestID) &&(!PredmetsID.HasValue || i.Test.Part.PredmetID == PredmetsID) && (!(i.TestID == null)))

        {
            
            AddInclude("Test.Part.Predmet");
            AddInclude("AnsweredQuesttions.Answer");
           
            
        }

      
       
    }

}