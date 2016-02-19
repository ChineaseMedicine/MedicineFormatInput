using System.Collections.Generic;
using System.Linq;

using CMD.Contract.Models;

namespace CMD.DAL.DALs
{
    public class SyndromesHandler : BaseHandler<SyndromesBo>
    {
        public override void SaveRecord(SyndromesBo bo)
        {
            var record = new BSyndromesRecord
            {
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                IsActive = bo.IsActive,
                Name = bo.Name,
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            CMDBasicEntities cmd = new CMDBasicEntities();
            cmd.BSyndromesRecords.Add(record);
            cmd.SaveChanges();
        }
        public override bool DuplicateQuery(string name)
        {
            var cmd = new CMDBasicEntities();
            var record = cmd.BSyndromesRecords.FirstOrDefault(o => o.Name == name);

            if (null != record)
            {
                return true;
            }
            return false;
        }

        public override List<SyndromesBo> LoadBos()
        {
            var cmd = new CMDBasicEntities();
            return (from record in cmd.BSyndromesRecords.Where(o => 1 == 1)
                    select new SyndromesBo
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
            var record = cmd.BSyndromesRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                cmd.BSyndromesRecords.Remove(record);
                cmd.SaveChanges();
            }
        }
    }
}
