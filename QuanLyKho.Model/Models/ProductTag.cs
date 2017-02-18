﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKho.Model.Models
{
    [Table("ProductTags")]
    public class ProductTag
    {
        [Key]
        [Column(Order = 1)]
        public int ProductID { set; get; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(50)]
        public string TagID { set; get; }

        [ForeignKey("ProductID")]
        public virtual Product Product { set; get; }
    }
}