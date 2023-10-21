using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExeedTask.Models
{
    public class Product
    {
        
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }

        public string? Image { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateOnly CreationDate { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateOnly StartDate { get; set; }

        public int DurationInDays { get; set; }
        
        public string UserId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public Product() 
        {
            this.Id = new Guid();
            DateTime currentDateTime = DateTime.Now;
            this.CreationDate = new DateOnly(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day);
        }    
    }
}
