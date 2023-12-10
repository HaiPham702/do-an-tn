using System.ComponentModel.DataAnnotations;

namespace ElectronicLibrary.Core.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherID { get; set; }

        public string PublisherName { get; set; }
    }
}
