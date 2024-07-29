namespace NWEC.P.L001_Task3.DataAccessLayer.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
