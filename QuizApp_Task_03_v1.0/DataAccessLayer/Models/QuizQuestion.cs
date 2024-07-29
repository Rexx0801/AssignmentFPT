
namespace NWEC.P.L001_Task3.DataAccessLayer.Models
{
    public class QuizQuestion
    {
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}