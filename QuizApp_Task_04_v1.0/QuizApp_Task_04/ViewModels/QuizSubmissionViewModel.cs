namespace QuizApp_Task_04.ViewModel
{
    public class QuizSubmissionViewModel
    {
        public Guid QuizId { get; set; }
        public Guid UserId { get; set; }
        public List<UserAnswerSubmissionViewModel> UserAnswers { get; set; }
    }

}
