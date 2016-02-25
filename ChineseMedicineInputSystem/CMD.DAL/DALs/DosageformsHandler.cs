using CMD.Contract.Models;
using System.Collections.Generic;
using System.Linq;

namespace CMD.DAL.DALs
{
    public class DosageformsHandler : BaseHandler<DosageformsBo>
    {
        public override void SaveRecord(DosageformsBo bo)
        {
            var record = new BDosageFormsRecord
            {
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                IsActive = bo.IsActive,
                Name = bo.Name,
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            var cmd = new CMDBasicEntities();
            cmd.BDosageFormsRecords.Add(record);
            cmd.SaveChanges();
        }

        public override bool DuplicateQuery(string name)
        {
            var cmd = new CMDBasicEntities();
            var record = cmd.BDosageFormsRecords.FirstOrDefault(o => o.Name == name);

            if (null != record)
            {
                return true;
            }
            return false;
        }

        public override List<DosageformsBo> LoadBos()
        {
            var cmd = new CMDBasicEntities();
            return (from record in cmd.BDosageFormsRecords.Where(o => 1 == 1)
                   select new DosageformsBo
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
            var cmd = new CMDBasicEntities();
            var record = cmd.BDosageFormsRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                cmd.BDosageFormsRecords.Remove(record);
                cmd.SaveChanges();
            }
        }
    }
}
