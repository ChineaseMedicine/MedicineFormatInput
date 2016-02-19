using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.Contract.Models
{
    public class DrugBo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long IndicationsRecord_Id { get; set; }
        public long MeridianRecord_Id { get; set; }
        public long DrugEffectRecord_Id { get; set; }
        public long DrugEffectCategoryRecord_Id { get; set; }
        public long HerbsRecord_Id { get; set; }
        public long DurgTasteRecord_Id { get; set; }
        public bool IsActive { get; set; }
        public string CreateBy { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string UpdateBy { get; set; }
        public System.DateTime UpdateTime { get; set; }
    }
}
