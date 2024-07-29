

namespace NWEC.P.L001_Task3.DataAccessLayer.Models
{
    public class UserAnswer
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public Guid AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}