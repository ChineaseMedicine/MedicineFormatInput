using CMD.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.DAL.DALs
{
    public class DynastyHandler
    {
        public void SaveRecord(DynastyBo bo)
        {
            BDynastyRecord record = new BDynastyRecord
            {
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                IsActive = bo.IsActive,
                IsAncient = bo.IsAncient,
                Name = bo.Name,
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            CMDBasicEntities cmd = new CMDBasicEntities();
            cmd.BDynastyRecords.Add(record);
            cmd.SaveChanges();
        }

        public bool DuplicateQuery(string dynastyName)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDynastyRecords.FirstOrDefault(o => o.Name == dynastyName);

            if (null != record)
            {
                return true;
            }
            return false;
        }

        public List<BDynastyRecord> LoadBDynastyRecords()
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            return cmd.BDynastyRecords.Where(o => 1 == 1).ToList();
        }

        public bool DeleteBDynastyRecord(long id)
        {
            bool result = true;
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDynastyRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                var mRecord = cmd.MRelationDynastyRecords.FirstOrDefault(o => o.DynastyName == record.Name);
                if (null == mRecord)
                {
                    cmd.BDynastyRecords.Remove(record);
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
