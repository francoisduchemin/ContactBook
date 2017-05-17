namespace ContactBook.Model
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public bool IsBlocked { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
