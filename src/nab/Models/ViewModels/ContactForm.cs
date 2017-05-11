namespace nab.Models.ViewModels
{
    public class ContactForm
    {
        private UserEmail userEmail;

        public UserEmail UserEmail
        {
            get
            {
                if (userEmail == null)
                {
                    return new UserEmail();
                }

                return userEmail;
            }
            set
            {
                userEmail = value;
            }
        }

        public bool IsSent { get; set; }

        public bool IsPosted { get; set; }
    }
}
