using CMD.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.DAL.DALs
{
    public class AreaHandler : BaseHandler<AreaBo>
    {
        public override void SaveRecord(AreaBo bo)
        {
            var record = new BAreaRecord
            {
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                IsActive = bo.IsActive,
                Name = bo.Name,
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            CMDBasicEntities cmd = new CMDBasicEntities();
            cmd.BAreaRecords.Add(record);
            cmd.SaveChanges();
        }
        public override bool DuplicateQuery(string name)
        {
            var cmd = new CMDBasicEntities();
            var record = cmd.BAreaRecords.FirstOrDefault(o => o.Name == name);

            if (null != record)
            {
                return true;
            }
            return false;
        }

        public override List<AreaBo> LoadBos()
        {
            var cmd = new CMDBasicEntities();
            return (from record in cmd.BAreaRecords.Where(o => 1 == 1)
                    select new AreaBo
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
            var record = cmd.BAreaRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                cmd.BAreaRecords.Remove(record);
                cmd.SaveChanges();
            }
        }
    }
}
