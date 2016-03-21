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

        public List<BDiseaseRecord> LoadBos()
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            return cmd.BDiseaseRecords.Where(o => 1 == 1).ToList();
        }

        public bool DeleteBAgeRecord(long id)
        {
            bool result = true;
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDiseaseRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                var mRecord = cmd.MMainSourceRecords.FirstOrDefault(o => o.DiseaseName == record.Name);
                if (null == mRecord)
                {
                    cmd.BDiseaseRecords.Remove(record);
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
