using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicLibrary.Core.Atribute
{
    public class Atribute
    {
        /// <summary>
        /// Khởi tạo các Attribute
        /// </summary>
        /// CreateBy: Phạm Văn Hải (5/1/2022)
        [AttributeUsage(AttributeTargets.All)]
        public class PrimaryKey : Attribute { }

        /// <summary>
        /// Thuộc tính là khóa ngoại
        /// </summary>
        [AttributeUsage(AttributeTargets.All)]
        public class ForegenKey : Attribute { }

        /// <summary>
        /// Trường không được bỏ trống
        /// </summary>
        /// CreateBy: PVHai (5/1/2022)
        [AttributeUsage(AttributeTargets.All)]
        public class NotEmpty : Attribute { }

        /// <summary>
        /// Trường Được phép lặp
        /// </summary>
        /// CreateBy: PVHai (5/1/2022)
        [AttributeUsage(AttributeTargets.All)]
        public class NotDuplicate : Attribute { }
    }
}
