using CMD.Contract.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CMD.DAL.DALs
{
    public class AgeHandler
    {
        public void SaveRecord(AgeBo bo)
        {
            BAgeRecord record = new BAgeRecord
            {
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                IsActive = bo.IsActive,
                Name = bo.Name,
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            CMDBasicEntities cmd = new CMDBasicEntities();
            cmd.BAgeRecords.Add(record);
            cmd.SaveChanges();
        }

        public bool DuplicateQuery(string ageName)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BAgeRecords.FirstOrDefault(o => o.Name == ageName);

            if (null != record)
            {
                return true;
            }
            return false;
        }

        public List<BAgeRecord> LoadBAgeRecords()
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            return cmd.BAgeRecords.Where(o => 1 == 1).ToList();
        }

        public void DeleteBAgeRecord(long id)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BAgeRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                cmd.BAgeRecords.Remove(record);
                cmd.SaveChanges();
            }
        }
    }
}
