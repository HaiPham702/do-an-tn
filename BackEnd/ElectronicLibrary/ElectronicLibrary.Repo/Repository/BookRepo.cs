using ElectronicLibrary.Core.Interface.Repository;
using ElectronicLibrary.Core.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace ElectronicLibrary.Repo.Repository
{
    public class BookRepo : BaseRepo<Book>, IBookRepo
    {
    }
}
