using CMD.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.DAL.DALs
{
    public class DrugEffectHandler
    {
        public void SaveRecord(DrugEffectBo bo)
        {
            BDrugEffectRecord record = new BDrugEffectRecord
            {
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                IsActive = bo.IsActive,
                Name = bo.Name,
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            CMDBasicEntities cmd = new CMDBasicEntities();
            cmd.BDrugEffectRecords.Add(record);
            cmd.SaveChanges();
        }

        public bool DuplicateQuery(string drugEffectName)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDrugEffectRecords.FirstOrDefault(o => o.Name == drugEffectName);

            if (null != record)
            {
                return true;
            }
            return false;
        }

        public List<BDrugEffectRecord> LoadBDrugEffectRecords()
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            return cmd.BDrugEffectRecords.Where(o => 1 == 1).ToList();
        }

        public void DeleteBDrugEffectRecord(long id)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDrugEffectRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                cmd.BDrugEffectRecords.Remove(record);
                cmd.SaveChanges();
            }
        }
    }
}
