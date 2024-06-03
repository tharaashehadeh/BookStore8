using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModel
{
    public class AuthorFormVM
    {
        public int Id { get; set; }
        [MaxLength(50,ErrorMessage ="the name filed can't exceed 50 characters")]
        public string Name { get; set; }

        
    }
}
