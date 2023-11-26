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

    public async Task<List<Book>> GetBookPaging(int limt, int skip, string sort, string filter)
    {  
        using (var connection = new MySqlConnection(this.connectionString))
        {
            MySqlCommand command = new MySqlCommand("InsertIntotblStudent", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new MySqlParameter("PStudentId", TextBox1.Text));
            command.Parameters.Add(new MySqlParameter("PStudentName", TextBox2.Text));
            command.Connection.Open();
            var result = command.ExecuteNonQuery();
            command.Connection.Close();
        }
    }
}
