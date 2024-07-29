namespace QuizApp_Task_04.ViewModel
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
    }

}
