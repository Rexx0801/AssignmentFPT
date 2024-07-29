namespace NWEC.P.L001_Task3.DataAccessLayer.Models
{
    public class UserQuiz
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public DateTime TakenDate { get; set; }
        public int Score { get; set; }
    }
}