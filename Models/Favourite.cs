using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3.Models
{
    
    public class Favourite
    {
        [Key]
        [Required]
        public int FavouriteID { get; set; }
        public int? UserID { get; set;}
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public int freelancerID {  get; set; }
      
        public DateTime? SelectedDate { get; set; }
        public string? Description { get; set;}

     
    }
}
