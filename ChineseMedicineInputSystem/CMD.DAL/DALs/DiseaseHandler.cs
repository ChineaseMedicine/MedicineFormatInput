using CMD.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.DAL.DALs
{
    public class DiseaseHandler
    {
        public void SaveRecord(DiseaseBo bo)
        {
            BDiseaseRecord record = new BDiseaseRecord
            {
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                IsActive = bo.IsActive,
                Name = bo.Name,
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            CMDBasicEntities cmd = new CMDBasicEntities();
            cmd.BDiseaseRecords.Add(record);
            cmd.SaveChanges();
        }

        public bool DuplicateQuery(string ageName)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDiseaseRecords.FirstOrDefault(o => o.Name == ageName);

            if (null != record)
            {
                return true;
            }
            return false;
        }

        public List<BDiseaseRecord> LoadBAgeRecords()
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            return cmd.BDiseaseRecords.Where(o => 1 == 1).ToList();
        }

        public void DeleteBAgeRecord(long id)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDiseaseRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                cmd.BDiseaseRecords.Remove(record);
                cmd.SaveChanges();
            }
        }
    }
}
