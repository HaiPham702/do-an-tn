﻿using System;
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
        /// <summary>
        /// Tên sách
        /// </summary>
        [NotDuplicate]
        public string BookName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Tác giả
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Nhóm loại sách
        /// </summary>
        public int BookGroupID { get; set; }
        /// <summary>
        /// Nhà xuất bản
        /// </summary>
        public int PublisherId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Tên file sách
        /// </summary>
        public string FileBookID { get; set; }
        /// <summary>
        /// Ảnh bìa
        /// </summary>
        public string FileCoverBook { get; set; }
    }
}
