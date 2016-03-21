using CMD.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.DAL.DALs
{
    public class DrugEffectCategroyHandler
    {
        public void SaveRecord(DrugEffectCategroyBo bo)
        {
            BDrugEffectCategroyRecord record = new BDrugEffectCategroyRecord
            {
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                IsActive = bo.IsActive,
                Name = bo.Name,
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            CMDBasicEntities cmd = new CMDBasicEntities();
            cmd.BDrugEffectCategroyRecords.Add(record);
            cmd.SaveChanges();
        }

        public bool DuplicateQuery(string ageName)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDrugEffectCategroyRecords.FirstOrDefault(o => o.Name == ageName);

            if (null != record)
            {
                return true;
            }
            return false;
        }

        public List<BDrugEffectCategroyRecord> LoadBDrugEffectCategroyRecords()
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            return cmd.BDrugEffectCategroyRecords.Where(o => 1 == 1).ToList();
        }

        public bool DeleteBAgeRecord(long id)
        {
            bool result = true;
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDrugEffectCategroyRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                cmd.BDrugEffectCategroyRecords.Remove(record);
                cmd.SaveChanges();
            }
            else
            {
                result = false;
            }

            return result;
        }
    }
}
