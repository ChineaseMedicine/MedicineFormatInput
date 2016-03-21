﻿using CMD.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.DAL.DALs
{
    public class DrugHandler
    {
        public void SaveRecord(DrugBo bo)
        {
            BDrugRecord record = new BDrugRecord
            {
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                IsActive = bo.IsActive,
                Name = bo.Name,
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            CMDBasicEntities cmd = new CMDBasicEntities();
            cmd.BDrugRecords.Add(record);
            cmd.SaveChanges();
        }

        public bool DuplicateQuery(string drugName)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDrugRecords.FirstOrDefault(o => o.Name == drugName);

            if (null != record)
            {
                return true;
            }
            return false;
        }

        public List<BDrugRecord> LoadBDrugRecords()
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            return cmd.BDrugRecords.Where(o => 1 == 1).ToList();
        }

        public bool DeleteBDrugRecord(long id)
        {
            bool result = true;
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BDrugRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                var mRecord = cmd.MRelationDrugRecords.FirstOrDefault(o => o.DrugName == record.Name);
                if (null == mRecord)
                {
                    cmd.BDrugRecords.Remove(record);
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
