using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKho.Model.Abstract
{
    public abstract class Auditable : IAudictable
    {
        public DateTime CreatedDate { get; set; }

        [MaxLength(250)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDated { get; set; }

        [MaxLength(250)]
        public string UpdatedBy { get; set; }
        public bool Status { get; set; }
        [MaxLength(250)]
        public string MetaKeyWord { get; set; }
        [MaxLength(250)]
        public string MetaDescription { get; set; }
    }
}