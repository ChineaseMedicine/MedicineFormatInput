using System;

namespace CMD.Contract.Models
{
    public class DurgTasteBo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
