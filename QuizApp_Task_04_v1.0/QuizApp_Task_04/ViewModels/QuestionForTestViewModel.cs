namespace QuizApp_Task_04.ViewModel
{
    public class QuestionForTestViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string QuestionType { get; set; }
        public List<AnswerForTestViewModel> Answers { get; set; }
    }

}
