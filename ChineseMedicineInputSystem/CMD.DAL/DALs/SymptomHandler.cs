using CMD.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.DAL.DALs
{
    public class SymptomHandler : BaseHandler<SymptomBo>
    {
        public override void SaveRecord(SymptomBo bo)
        {
            var record = new BSymptomRecord
            {
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                //IsActive = bo.IsActive,
                Name = bo.Name,
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            CMDBasicEntities cmd = new CMDBasicEntities();
            cmd.BSymptomRecords.Add(record);
            cmd.SaveChanges();
        }
        public override bool DuplicateQuery(string name)
        {
            var cmd = new CMDBasicEntities();
            var record = cmd.BSymptomRecords.FirstOrDefault(o => o.Name == name);

            if (null != record)
            {
                return true;
            }
            return false;
        }

        public override List<SymptomBo> LoadBos()
        {
            var cmd = new CMDBasicEntities();
            return (from record in cmd.BSymptomRecords.Where(o => 1 == 1)
                    select new SymptomBo
                    {
                        Id = record.Id,
                        CreateBy = record.CreateBy,
                        CreateTime = record.CreateTime,
                        IsActive = record.IsActive,
                        Name = record.Name,
                        UpdateBy = record.UpdateBy,
                        UpdateTime = record.UpdateTime,
                    }).ToList();
        }

        public override void DeleteRecord(long id)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BSymptomRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                cmd.BSymptomRecords.Remove(record);
                cmd.SaveChanges();
            }
        }
    }
}
