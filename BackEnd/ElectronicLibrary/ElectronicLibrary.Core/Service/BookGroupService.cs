using ElectronicLibrary.Core.Interface.Repository;
using ElectronicLibrary.Core.Interface.Service;
using ElectronicLibrary.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicLibrary.Core.Service
{
    public class BookGroupService : BaseService<BookGroup>, IBookGroupService
    {
        public BookGroupService(IBaseRepo<BookGroup> repo) : base(repo)
        {
        }
    }
}
