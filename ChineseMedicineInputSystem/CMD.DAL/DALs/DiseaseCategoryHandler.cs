using CMD.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.DAL.DALs
{
    public class DiseaseCategoryHandler
    {
        public void SaveRecord(DiseaseCategoryBo bo)
        {
            BDiseaseCategoryRecord record = new BDiseaseCategoryRecord
            {
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                IsActive = bo.IsActive,
                Name = bo.Name,
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            CMDBasicEntities cmd = new CMDBasicEntities();
            cmd.BDiseaseCategoryRecords.Add(record);
            cmd.SaveChanges();
        }

        public bool DuplicateQuery(string diseaseCategoryName)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDiseaseCategoryRecords.FirstOrDefault(o => o.Name == diseaseCategoryName);

            if (null != record)
            {
                return true;
            }
            return false;
        }

        public List<BDiseaseCategoryRecord> LoadBos()
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            return cmd.BDiseaseCategoryRecords.Where(o => 1 == 1).ToList();
        }

        public void DeleteBAgeRecord(long id)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDiseaseCategoryRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                cmd.BDiseaseCategoryRecords.Remove(record);
                cmd.SaveChanges();
            }
        }
    }
}
