using CMD.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.DAL.DALs
{
    public class PrescriptionHandler : BaseHandler<PrescriptionBo>
    {
        public override void SaveRecord(PrescriptionBo bo)
        {
            var record = new BFamousPrescriptionRecord
            {
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                IsActive = bo.IsActive,
                Name = bo.Name,
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            CMDBasicEntities cmd = new CMDBasicEntities();
            cmd.BFamousPrescriptionRecords.Add(record);
            cmd.SaveChanges();
        }
        public override bool DuplicateQuery(string name)
        {
            var cmd = new CMDBasicEntities();
            var record = cmd.BFamousPrescriptionRecords.FirstOrDefault(o => o.Name == name);

            if (null != record)
            {
                return true;
            }
            return false;
        }

        public override List<PrescriptionBo> LoadBos()
        {
            var cmd = new CMDBasicEntities();
            return (from record in cmd.BFamousPrescriptionRecords.Where(o => 1 == 1)
                   select new PrescriptionBo
                   {
                       Id = record.Id,
                       CreateBy = record.CreateBy,
                       CreateTime = record.CreateTime,
                       IsActive = record.IsActive,
                       Name = record.Name,
                       UpdateBy = record.UpdateBy,
                       UpdateTime = record.UpdateTime,
                   }).ToList();
        }
        
        public override void DeleteRecord(long id)
        {
            CMDBasicEntities cmd = new CMDBasicEntities();
            var record = cmd.BFamousPrescriptionRecords.FirstOrDefault(o => o.Id == id);
            if (null != record)
            {
                cmd.BFamousPrescriptionRecords.Remove(record);
                cmd.SaveChanges();
            }
        }
    }
}
