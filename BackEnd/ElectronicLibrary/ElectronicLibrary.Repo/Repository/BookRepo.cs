using ElectronicLibrary.Core.Interface.Repository;
using ElectronicLibrary.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicLibrary.Repo.Repository
{
    public class BookRepo: BaseRepo<Book>, IBookRepo
    {
    }
}
