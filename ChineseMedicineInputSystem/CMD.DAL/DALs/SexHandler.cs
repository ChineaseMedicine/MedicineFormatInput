using CMD.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.DAL.DALs
{
    public class SexHandler:BaseHandler<SexBo>
    {
        public override void SaveRecord(SexBo bo)
        {
            var record = new BSexRecord
            {
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                //IsActive = bo.IsActive,
                Name = bo.Name,
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            CMDBasicEntities cmd = new CMDBasicEntities();
            cmd.BSexRecords.Add(record);
            cmd.SaveChanges();
        }
        public override bool DuplicateQuery(string name)
        {
            var cmd = new CMDBasicEntities();
            var record = cmd.BSexRecords.FirstOrDefault(o => o.Name == name);

            if (null != record)
            {
                return true;
            }
            return false;
        }

        public override List<SexBo> LoadBos()
        {
            var cmd = new CMDBasicEntities();
            return (from record in cmd.BSexRecords.Where(o => 1 == 1)
                    select new SexBo
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

        public override bool DeleteRecord(long id)
        {
            bool result = true;
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BSexRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                var mRecord = cmd.MRelationSexRecords.FirstOrDefault(o => o.SexName == record.Name);
                if (null == mRecord)
                {
                    cmd.BSexRecords.Remove(record);
                    cmd.SaveChanges();
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }
    }
}
