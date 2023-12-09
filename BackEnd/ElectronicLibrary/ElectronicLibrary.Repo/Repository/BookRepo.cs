using ElectronicLibrary.Core.Interface.Repository;
using ElectronicLibrary.Core.Models;

namespace ElectronicLibrary.Repo.Repository
{
    public class BookRepo : BaseRepo<Book>, IBookRepo
    {
        public override string GetTableName()
        {
            return "view_book";
        }
    }

}
