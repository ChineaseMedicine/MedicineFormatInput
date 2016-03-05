using CMD.Contract.Models;
using System.Collections.Generic;
using System.Linq;

namespace CMD.DAL.DALs
{
    public class DrugTasteHandler
    {
        public void SaveRecord(DurgTasteBo bo)
        {
            BDurgTasteRecord record = new BDurgTasteRecord
            {
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                IsActive = bo.IsActive,
                Name = bo.Name,
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            CMDBasicEntities cmd = new CMDBasicEntities();
            cmd.BDurgTasteRecords.Add(record);
            cmd.SaveChanges();
        }

        public bool DuplicateQuery(string drugTasteName)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDurgTasteRecords.FirstOrDefault(o => o.Name == drugTasteName);

            if (null != record)
            {
                return true;
            }
            return false;
        }

        public List<BDurgTasteRecord> LoadBDurgTasteRecords()
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            return cmd.BDurgTasteRecords.Where(o => 1 == 1).ToList();
        }

        public void DeleteBDurgTasteRecord(long id)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDurgTasteRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                cmd.BDurgTasteRecords.Remove(record);
                cmd.SaveChanges();
            }
        }
    }
}
