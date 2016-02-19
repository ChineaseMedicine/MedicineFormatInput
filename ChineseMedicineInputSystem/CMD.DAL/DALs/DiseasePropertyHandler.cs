using CMD.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.DAL.DALs
{
    public class DiseasePropertyHandler
    {
        public void SaveRecord(DiseasePropertyBo bo)
        {
            BDiseasePropertyRecord record = new BDiseasePropertyRecord
            {
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                IsActive = bo.IsActive,
                Name = bo.Name,
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            CMDBasicEntities cmd = new CMDBasicEntities();
            cmd.BDiseasePropertyRecords.Add(record);
            cmd.SaveChanges();
        }

        public bool DuplicateQuery(string diseasePropertyName)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDiseasePropertyRecords.FirstOrDefault(o => o.Name == diseasePropertyName);

            if (null != record)
            {
                return true;
            }
            return false;
        }

        public List<BDiseasePropertyRecord> LoadRecords()
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            return cmd.BDiseasePropertyRecords.Where(o => 1 == 1).ToList();
        }

        public void DeleteBAgeRecord(long id)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDiseasePropertyRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                cmd.BDiseasePropertyRecords.Remove(record);
                cmd.SaveChanges();
            }
        }
    }
}
