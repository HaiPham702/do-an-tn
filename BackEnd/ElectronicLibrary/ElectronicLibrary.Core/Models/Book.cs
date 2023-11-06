using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ElectronicLibrary.Core.Atribute.Atribute;

namespace ElectronicLibrary.Core.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [NotDuplicate]
        public string BookName { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public int Booktype { get; set; }
    }
}
