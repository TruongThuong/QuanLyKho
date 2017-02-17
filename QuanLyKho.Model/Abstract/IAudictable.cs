using System;

namespace QuanLyKho.Model.Abstract
{
    public interface IAudictable
    {
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTime? UpdatedDated { get; set; }
        string UpdatedBy { get; set; }
        bool Status { get; set; }
        string MetaKeyWord { get; set; }
        string MetaDescription { get; set; }
    }
}