using System.ComponentModel;

namespace CarProject.ViewModel
{
    public class UserModelViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DefaultValue("")]
        public string Image { get; set; }

        public string ImageString { get; set; }
        public string Email { get; set; }
        public int? IsAdmin { get; set; }
    }
}
