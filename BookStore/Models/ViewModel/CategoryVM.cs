using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.ViewModel
{
    public class CategoryVM
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "Plz Insert Name")]
        [MaxLength(30, ErrorMessage = "30")]
        [Remote("CheckName", null, ErrorMessage ="Exisets")]
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
