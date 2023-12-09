using Dapper;
using ElectronicLibrary.Core.Enum;
using ElectronicLibrary.Core.Interface.Repository;
using ElectronicLibrary.Core.Models;
using ElectronicLibrary.Core.Parameters;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using static Dapper.SqlMapper;
using static ElectronicLibrary.Core.Atribute.Atribute;

namespace ElectronicLibrary.Repo.Repository
{
    public class BaseRepo<Entity> : IBaseRepo<Entity>
    {
        #region fields
        protected string connectionString = "Server= localhost;Port=3306;Database=qlthuvien; User Id = root; Password=123456789;Character Set = utf8";

        /// <summary>
        /// Khởi tạo connection
        /// </summary>
        public MySqlConnection connection;
        /// <summary>
        /// Tên của bảng
        /// </summary>
        string tableName = typeof(Entity).Name;

        #endregion
        #region methods 

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <param name="limt"></param>
        /// <param name="skip"></param>
        /// <param name="sort"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<PagingResult<object>> Get(int limt, int skip, string sort, string filter)
        {
            using (connection = new MySqlConnection(connectionString))
            {
                var result = new PagingResult<object>();
                // lấy dữ liệu trong database
                var slqCommand = PartWhere(limt, skip, sort, filter);
                var data = await connection.QueryAsync<object>(slqCommand);
                var total = await connection.QueryAsync<object>(SqlCommandCount(filter));
                result.Data = data.ToList();
                result.Total = total.ToList().Count();
                return result;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="limt"></param>
        /// <param name="skip"></param>
        /// <param name="sort"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private string PartWhere(int limt, int skip, string sort, string filter)
        {
            var result = string.Empty;

            string nameTabel = GetTableName();

            result = $"SELECT * FROM {nameTabel}";

            if (!string.IsNullOrWhiteSpace(filter))
            {
                var filters = JsonConvert.DeserializeObject<List<Filter>>(filter);

                result += " Where ";

                for (int i = 0; i < filters.Count; i++)
                {
                    var op = filters[i].Operator;
                    //switch (filters[i].Operator)
                    //{
                    //    case "=":
                    //    case ">":
                    //    case "<":
                    //    case "=":

                    //        break;
                    //}

                    if (i > 0)
                    {
                        result += " AND ";
                    }

                    result += $"{filters[i].ColName} {op} '{filters[i].Value}'";
                }
            }

            if (string.IsNullOrWhiteSpace(sort))
            {
                result += $" ORDER BY {sort}";
            }

            if (limt != 0)
            {
                result += $" LIMIT {limt} OFFSET {skip}";
            }

            return result;
        }

        public virtual string GetTableName()
        {
            var result = tableName;
            switch (tableName)
            {
                case "Book":
                    result = "view_book";
                    break;
            }
            return result;
        }

        private string SqlCommandCount(string filter)
        {
            var result = string.Empty;

            result = $"SELECT * FROM {tableName}";

            if (!string.IsNullOrWhiteSpace(filter))
            {
                var filters = JsonConvert.DeserializeObject<List<Filter>>(filter);

                result += " Where ";

                for (int i = 0; i < filters.Count; i++)
                {
                    var op = filters[i].Operator;
                    //switch (filters[i].Operator)
                    //{
                    //    case "=":
                    //    case ">":
                    //    case "<":
                    //    case "=":

                    //        break;
                    //}

                    if (i > 0)
                    {
                        result += " AND ";
                    }

                    result += $"{filters[i].ColName} {op} '{filters[i].Value}'";
                }
            }

            return result;
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<Entity>> GetAll()
        {
            using (connection = new MySqlConnection(connectionString))
            {
                // lấy dữ liệu trong database
                var slqCommand = $"SELECT * FROM {GetUserName()}";
                var result = await connection.QueryAsync<Entity>(slqCommand);
                return result.ToList();
            }
        }

        /// <summary>
        /// Thêm mới bản ghi vào DB
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Entity Insert(Entity entity)
        {
            using (connection = new MySqlConnection(connectionString))
            {
                // lấy dữ liệu trong database
                var slqCommand = $"INSERT {tableName}({BuildSqlCommand(entity, "", ActionType.Insert)} ) "
                               + $"VALUES({BuildSqlCommand(entity, "@", ActionType.Insert)})";

                var parameter = ParamCoustome(entity, ActionType.Insert);

                var result = connection.Execute(slqCommand, parameter);

                return entity;
            }
        }

        /// <summary>
        /// Cập nhật bản ghi vào DB
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Entity Update(Entity entity)
        {
            using (connection = new MySqlConnection(connectionString))
            {
                // lấy dữ liệu trong database
                var slqCommand = $"UPDATE {tableName} SET {BuildSqlCommand(entity, "", ActionType.Update)}  "
                               + $"WHERE {BuilKeyWhere(entity)}";

                var parameter = ParamCoustome(entity, ActionType.Insert);

                var result = connection.Execute(slqCommand, parameter);

                return entity;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(Entity entity)
        {
            using (connection = new MySqlConnection(connectionString))
            {
                // lấy dữ liệu trong database
                var slqCommand = $"DELETE FROM {tableName}"
                + $" WHERE {BuilKeyWhere(entity)}";

                var result = connection.Execute(slqCommand);

                return result;
            }
        }

        /// <summary>
        /// Coustome param chuyền vào
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected DynamicParameters ParamCoustome(Entity entity, ActionType actionType)
        {
            // Khởi tạo dynamicparameters
            var parameters = new DynamicParameters();

            // add các giá trị trong enty ti tương ứng vào parameter


            var props = typeof(Entity).GetProperties();
            foreach (var prop in props)
            {
                // Kiểm tra có phải là khóa chính
                var isPrimaryKey = System.Attribute.IsDefined(prop, typeof(PrimaryKey));
                // lấy giá trị của trường
                var propValue = prop.GetValue(entity, null);
                switch (actionType)
                {
                    // Insert bản ghi
                    case ActionType.Insert:
                        // Tạo khóa chính cho bản ghi mới
                        if (isPrimaryKey == true)
                        {
                            propValue = Guid.NewGuid();
                        }
                        break;
                    // Cập nhật bản ghi
                    case ActionType.Update:
                        break;
                    case ActionType.Delete:

                        break;
                }

                parameters.Add($"@{prop.Name}", propValue);
            }
            return parameters;
        }

        /// <summary>
        /// Build câu command text
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="subString"></param>
        /// <returns></returns>
        protected string BuildSqlCommand(Entity entity, string subString, ActionType actionType)
        {
            var result = String.Empty;

            var listProperti = entity.GetType().GetProperties().ToList();
            var count = 1;

            switch (actionType)
            {
                case ActionType.Insert:
                    break;
                case ActionType.Update:
                    // Cập nhật các thuôc tính không phải là khóa chính
                    listProperti = entity.GetType().GetProperties().ToList().Where(p => !System.Attribute.IsDefined(p, typeof(PrimaryKey))).ToList();
                    break;

            }

            foreach (var prop in listProperti)
            {
                if (count == listProperti.Count())
                {
                    result += $"{subString}{prop.Name}";
                }
                else
                {
                    result += $"{subString}{prop.Name},";
                }
                count++;
            }

            return result;
        }



        public bool CheckDuplicateV2(Entity entity)
        {
            using (connection = new MySqlConnection(connectionString))
            {
                var isDuplicate = false;
                var propertys = entity.GetType();
                var propNotDuplicate = propertys.GetProperties().Where(prop => prop.GetCustomAttribute<NotDuplicate>() != null).ToList();

                var slqCommand = $"SELECT * FROM {tableName}";

                if (propNotDuplicate.Count() > 0)
                {
                    slqCommand += " WHERE ";
                    for (int i = 0; i < propNotDuplicate.Count(); i++)
                    {
                        if (i > 0)
                        {
                            slqCommand += " OR ";
                        }
                        var key = propNotDuplicate[i];
                        slqCommand += $" {key.Name} = '{propertys.GetProperty(key.Name).GetValue(entity)}'";
                    }
                }
                else
                {
                    return false;
                }

                var entities = connection.QueryFirstOrDefault<Entity>(slqCommand);
                if (entities != null)
                {
                    isDuplicate = true;
                }
                return isDuplicate;
            }
        }

        /// <summary>
        /// Kiểm tra dữ kiệu trùng
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        public bool CheckDuplicate(string propName, object propValue)
        {
            using (connection = new MySqlConnection(connectionString))
            {
                var isDuplicate = false;
                // lấy dữ liệu trong database
                var slqCommand = $"SELECT {propName} FROM {tableName} WHERE {propName} = @{propName} ";
                var parameter = new DynamicParameters();
                parameter.Add($"@{propName}", propValue);
                var entities = connection.QueryFirstOrDefault<Entity>(slqCommand, parameter);
                if (entities != null)
                {
                    isDuplicate = true;
                }
                return isDuplicate;
            }
        }

        private string GetUserName()
        {
            return tableName.ToLower();
        }

        private string BuilKeyWhere(Entity entity)
        {
            var result = "";
            var propertys = entity.GetType();
            var key = propertys.GetProperties().FirstOrDefault(prop => prop.GetCustomAttribute<KeyAttribute>() != null);

            if (key != null)
            {
                result = $"{key.Name} = {propertys.GetProperty(key.Name).GetValue(entity)}";
            }
            return result;
        }
        #endregion
    }
}
