namespace Examination.Data
{
    public class Answer
    {
        public int? ID { get; set; }
        public string Nomi { get; set; }
        public bool isTrue { get; set; }
        public int QuestionID { get; set; }
        public Question Question { get; set; }
       
    }
}