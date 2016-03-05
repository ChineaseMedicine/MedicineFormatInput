using CMD.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMD.DAL.DALs
{
    public class HerbsHandler: BaseHandler<HerbsBo>
    {
        public override void SaveRecord(HerbsBo bo)
        {
            var record = new BHerbsRecord
            {
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                IsActive = bo.IsActive,
                Name = bo.Name,
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            CMDBasicEntities cmd = new CMDBasicEntities();
            cmd.BHerbsRecords.Add(record);
            cmd.SaveChanges();
        }
        public override bool DuplicateQuery(string name)
        {
            var cmd = new CMDBasicEntities();
            var record = cmd.BHerbsRecords.FirstOrDefault(o => o.Name == name);

            if (null != record)
            {
                return true;
            }
            return false;
        }

        public override List<HerbsBo> LoadBos()
        {
            var cmd = new CMDBasicEntities();
            return (from record in cmd.BHerbsRecords.Where(o => 1 == 1)
                    select new HerbsBo
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
            var record = cmd.BHerbsRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                cmd.BHerbsRecords.Remove(record);
                cmd.SaveChanges();
            }
        }
    }
}
