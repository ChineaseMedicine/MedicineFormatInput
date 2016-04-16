using System;
using System.Collections.Generic;

namespace CMD.Contract.Models
{
    public class MeteDataBo
    {
        public virtual object Id { get; set; }
        public virtual long LineNumber { get; set; }
        public virtual object DiseaseCategoryName { get; set; }
        public virtual object DynastyName { get; set; }
        public virtual object DiseaseName { get; set; }
        public virtual object DiseasePropertyName { get; set; }
        public virtual object AreaName { get; set; }
        public virtual object SeasonName { get; set; }
        public virtual object EnvironmentName { get; set; }
        public virtual object AgeName { get; set; }
        public virtual object SexName { get; set; }
        public virtual List<object> Symptoms { get; set; }
        public string SymptomNameString
        {
            get
            {
                return string.Join(",", Symptoms.ToArray());
            }
        }
        public virtual List<object> Syndromes { get; set; }
        public string SyndromesNameString
        {
            get
            {
                return string.Join(",", Syndromes.ToArray());
            }
        }
        public virtual List<object> Prescriptions { get; set; }
        public string PrescriptionNameString
        {
            get
            {
                return string.Join(",", Prescriptions.ToArray());
            }
        }
        public virtual List<object> Dosageformses { get; set; }
        public string DosageformsNameString
        {
            get
            {
                return string.Join(",", Dosageformses.ToArray());
            }
        }
        public virtual List<object> DrugNames { get; set; }
        public string DrugNameString
        {
            get { return string.Join(",", DrugNames.ToArray()); }
        }
        public virtual int CaseNumber { get; set; }
        public virtual string CreateBy { get; set; }
        public virtual string UpdateBy { get; set; }
        public virtual DateTime CreateTime { get; set; }
        public virtual DateTime UpdateTime { get; set; }
    }
}
