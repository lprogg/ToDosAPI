using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

        public ToDo()
        {
            Active = true;
        }
    }
}