namespace QuizApp_Task_04.ViewModel
{
    public class QuizPrepareInfoViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string ThumbnailUrl { get; set; }
        public string QuizCode { get; set; }
        public UserViewModel User { get; set; }
    }

}
