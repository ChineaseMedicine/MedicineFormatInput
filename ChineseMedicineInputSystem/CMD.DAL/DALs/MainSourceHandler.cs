using CMD.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMD.DAL.DALs
{
    public class MainSourceHandler
    {
        public void DeleteRecord(long id)
        {
            var cmd = new CMDBasicEntities();
            var mainSourceEntity = cmd.MMainSourceRecords.FirstOrDefault(o => o.MainSourceId == id);

            var relationAgeEntity = cmd.MRelationAgeRecords.Where(o => o.MainSourceId == mainSourceEntity.MainSourceId);
            foreach (var item in relationAgeEntity)
            {
                cmd.MRelationAgeRecords.Remove(item);
            }
            var relationDiseasePropertyCategory = cmd.MRelationDiseasePropertyRecords.Where(o => o.MainSourceId == mainSourceEntity.MainSourceId);
            foreach (var item in relationDiseasePropertyCategory)
            {
                cmd.MRelationDiseasePropertyRecords.Remove(item);
            }
            var relatioinDosage = cmd.MRelationDosageRecords.Where(o => o.MainSourceId == mainSourceEntity.MainSourceId);
            foreach (var item in relatioinDosage)
            {
                cmd.MRelationDosageRecords.Remove(item);
            }
            var relationDrug = cmd.MRelationDrugRecords.Where(o => o.MainSourceId == mainSourceEntity.MainSourceId);
            foreach (var item in relationDrug)
            {
                cmd.MRelationDrugRecords.Remove(item);
            }
            var relatioinDynasty = cmd.MRelationDynastyRecords.Where(o => o.MainSourceId == mainSourceEntity.MainSourceId);
            foreach (var item in relatioinDynasty)
            {
                cmd.MRelationDynastyRecords.Remove(item);
            }
            var relationFamousPrescription = cmd.MRelationFamousPrescriptionRecords.Where(o => o.MainSourceId == mainSourceEntity.MainSourceId);
            foreach (var item in relationFamousPrescription)
            {
                cmd.MRelationFamousPrescriptionRecords.Remove(item);
            }
            var relationPathogenesis = cmd.MRelationPathogenesisRecords.Where(o => o.MainSourceId == mainSourceEntity.MainSourceId);
            foreach (var item in relationPathogenesis)
            {
                cmd.MRelationPathogenesisRecords.Remove(item);
            }
            var relationPhysique = cmd.MRelationPhysiqueRecords.Where(o => o.MainSourceId == mainSourceEntity.MainSourceId);
            foreach (var item in relationPhysique)
            {
                cmd.MRelationPhysiqueRecords.Remove(item);
            }
            var relationSex = cmd.MRelationSexRecords.Where(o => o.MainSourceId == mainSourceEntity.MainSourceId);
            foreach (var item in relationSex)
            {
                cmd.MRelationSexRecords.Remove(item);
            }
            var relationSymptom = cmd.MRelationSymptomRecords.Where(o => o.MainSourceId == mainSourceEntity.MainSourceId);
            foreach (var item in relationSymptom)
            {
                cmd.MRelationSymptomRecords.Remove(item);
            }
            var relationSyndromes = cmd.MRelationSyndromesRecords.Where(o => o.MainSourceId == mainSourceEntity.MainSourceId);
            foreach (var item in relationSyndromes)
            {
                cmd.MRelationSyndromesRecords.Remove(item);
            }
            var relationTherapy = cmd.MRelationTherapyRecords.Where(o => o.MainSourceId == mainSourceEntity.MainSourceId);
            foreach (var item in relationTherapy)
            {
                cmd.MRelationTherapyRecords.Remove(item);
            }
            var environment = cmd.MRelationEnvironmentRecords.Where(o => o.MainSourceId == mainSourceEntity.MainSourceId);
            foreach (var item in environment)
            {
                cmd.MRelationEnvironmentRecords.Remove(item);
            }
            var season = cmd.MRelationSeasonRecords.Where(o => o.MainSourceId == mainSourceEntity.MainSourceId);
            foreach (var item in season)
            {
                cmd.MRelationSeasonRecords.Remove(item);
            }

            cmd.MMainSourceRecords.Remove(mainSourceEntity);

            cmd.SaveChanges();
        }

        public bool DuplicateQuery(string name)
        {
            throw new NotImplementedException();
        }

        public List<MeteDataBo> LoadBos()
        {
            var result = new List<MeteDataBo>();
            var cmd = new CMDBasicEntities();

            var mainSourceEntities = cmd.MMainSourceRecords.Where(o => 1 == 1);
            foreach (var itemEntity in mainSourceEntities)
            {
                var age = cmd.MRelationAgeRecords.FirstOrDefault(o => o.MainSourceId == itemEntity.MainSourceId);
                var diseaseProperty = cmd.MRelationDiseasePropertyRecords.FirstOrDefault(o => o.MainSourceId == itemEntity.MainSourceId);
                var dosageformses = cmd.MRelationDosageRecords.Where(o => o.MainSourceId == itemEntity.MainSourceId).ToList();
                var drugs = cmd.MRelationDrugRecords.Where(o => o.MainSourceId == itemEntity.MainSourceId).ToList();
                var prescriptions = cmd.MRelationFamousPrescriptionRecords.Where(o => o.MainSourceId == itemEntity.MainSourceId).ToList();
                var symptoms = cmd.MRelationSymptomRecords.Where(o => o.MainSourceId == itemEntity.MainSourceId).ToList();
                var syndromes = cmd.MRelationSyndromesRecords.Where(o => o.MainSourceId == itemEntity.MainSourceId).ToList();
                var dynasty = cmd.MRelationDynastyRecords.FirstOrDefault(o => o.MainSourceId == itemEntity.MainSourceId);
                var sex = cmd.MRelationSexRecords.FirstOrDefault(o => o.MainSourceId == itemEntity.MainSourceId);
                var season = cmd.MRelationSeasonRecords.FirstOrDefault(o => o.MainSourceId == itemEntity.MainSourceId);
                var environment = cmd.MRelationEnvironmentRecords.FirstOrDefault(o => o.MainSourceId == itemEntity.MainSourceId);

                result.Add(new MeteDataBo()
                {
                    Id = itemEntity.MainSourceId,
                    AgeName = age.AgeName,
                    AreaName = itemEntity.Area,
                    CreateBy = itemEntity.CreateBy,
                    CreateTime = itemEntity.CreateTime,
                    DiseaseCategoryName = itemEntity.DiseaseCategory,
                    DiseaseName = itemEntity.DiseaseName,
                    DiseasePropertyName = diseaseProperty.DiseasePropertyCategoryName,
                    Dosageformses = dosageformses.Select(o => o.DosageName as object).ToList(),
                    DrugNames = drugs.Select(o => o.DrugName as object).ToList(),
                    DynastyName = dynasty.DynastyName,
                    EnvironmentName = null == season ? string.Empty : environment.EnvironmentName,
                    Prescriptions = prescriptions.Select(o => o.FamousPrescriptionName as object).ToList(),
                    Symptoms = symptoms.Select(o => o.SymptomName as object).ToList(),
                    Syndromes = syndromes.Select(o => o.SyndromesName as object).ToList(),
                    SexName = sex.SexName,
                    SeasonName = null == season ? string.Empty : season.SeasonName,
                    UpdateBy = itemEntity.UpdateBy,
                    UpdateTime = itemEntity.UpdateTime,
                    CaseNumber = itemEntity.CaseNumber,

                });
            }

            return result;
        }

        public void SaveRecord(MeteDataBo bo)
        {
            var cmd = new CMDBasicEntities();
            var mainSourceEntity = new MMainSourceRecord()
            {
                Area = bo.AreaName.ToString(),
                CaseNumber = 1,
                CreateBy = bo.CreateBy,
                CreateTime = bo.CreateTime,
                DiseaseCategory = bo.DiseaseCategoryName.ToString(),
                DiseaseName = bo.DiseaseName.ToString(),
                UpdateBy = bo.UpdateBy,
                UpdateTime = bo.UpdateTime,
            };
            cmd.MMainSourceRecords.Add(mainSourceEntity);
            cmd.SaveChanges();

            #region Age
            string ageName = bo.AgeName.ToString();
            var bAgeRecord = cmd.BAgeRecords.FirstOrDefault(o => o.Name == ageName);
            var relationAgeEntity = new MRelationAgeRecord()
            {
                AgeId = bAgeRecord.Id,
                AgeName = bAgeRecord.Name,
                MainSourceId = mainSourceEntity.MainSourceId,
                CreateBy = mainSourceEntity.CreateBy,
                CreateTime = mainSourceEntity.CreateTime,
                UpdateBy = mainSourceEntity.UpdateBy,
                UpdateTime = mainSourceEntity.UpdateTime,
            };
            cmd.MRelationAgeRecords.Add(relationAgeEntity);
            #endregion
            #region DiseaseProperty
            string diseasePropertyName = bo.DiseaseCategoryName.ToString();
            var bDiseasePropertyRecord = cmd.BDiseaseCategoryRecords.FirstOrDefault(o => o.Name == diseasePropertyName);
            var relationDiseasePropertyCategory = new MRelationDiseasePropertyRecord()
            {
                DiseasePropertyCategoryId = bDiseasePropertyRecord.Id,
                DiseasePropertyCategoryName = bDiseasePropertyRecord.Name,
                MainSourceId = mainSourceEntity.MainSourceId,
                CreateBy = mainSourceEntity.CreateBy,
                CreateTime = mainSourceEntity.CreateTime,
                UpdateBy = mainSourceEntity.UpdateBy,
                UpdateTime = mainSourceEntity.UpdateTime,
            };
            cmd.MRelationDiseasePropertyRecords.Add(relationDiseasePropertyCategory);
            #endregion
            #region Dosageformses
            foreach (var item in bo.Dosageformses.Distinct())
            {
                string name = item.ToString();
                var bDosage = cmd.BDosageFormsRecords.FirstOrDefault(o => o.Name == name);
                var relationDosageRecord = new MRelationDosageRecord()
                {
                    DosageId = bDosage.Id,
                    DosageName = bDosage.Name,
                    MainSourceId = mainSourceEntity.MainSourceId,
                    CreateBy = mainSourceEntity.CreateBy,
                    CreateTime = mainSourceEntity.CreateTime,
                    UpdateBy = mainSourceEntity.UpdateBy,
                    UpdateTime = mainSourceEntity.UpdateTime,
                };
                cmd.MRelationDosageRecords.Add(relationDosageRecord);
            }
            #endregion
            #region DrugNames
            foreach (var item in bo.DrugNames.Distinct())
            {
                string name = item.ToString();
                var bDrugRecord = cmd.BDrugRecords.FirstOrDefault(o => o.Name == name);
                var relationDrugRecord = new MRelationDrugRecord()
                {
                    DrugId = bDrugRecord.Id,
                    DrugName = bDrugRecord.Name,
                    MainSourceId = mainSourceEntity.MainSourceId,
                    CreateBy = mainSourceEntity.CreateBy,
                    CreateTime = mainSourceEntity.CreateTime,
                    UpdateBy = mainSourceEntity.UpdateBy,
                    UpdateTime = mainSourceEntity.UpdateTime,
                };
                cmd.MRelationDrugRecords.Add(relationDrugRecord);
            }
            #endregion
            #region Dynasty
            string dynastyName = bo.DynastyName.ToString();
            var bDynasty = cmd.BDynastyRecords.FirstOrDefault(o => o.Name == dynastyName);
            var relationDynasty = new MRelationDynastyRecord()
            {
                DynastyId = bDynasty.Id,
                DynastyName = bDynasty.Name,
                MainSourceId = mainSourceEntity.MainSourceId,
                CreateBy = mainSourceEntity.CreateBy,
                CreateTime = mainSourceEntity.CreateTime,
                UpdateBy = mainSourceEntity.UpdateBy,
                UpdateTime = mainSourceEntity.UpdateTime,
            };
            cmd.MRelationDynastyRecords.Add(relationDynasty);
            #endregion
            #region Prescriptions
            foreach (var item in bo.Prescriptions.Distinct())
            {
                string name = item.ToString();
                var bFamousPrescription = cmd.BFamousPrescriptionRecords.FirstOrDefault(o => o.Name == name);
                var relationFamousPrescription = new MRelationFamousPrescriptionRecord()
                {
                    FamousPrescriptionId = bFamousPrescription.Id,
                    FamousPrescriptionName = bFamousPrescription.Name,
                    MainSourceId = mainSourceEntity.MainSourceId,
                    CreateBy = mainSourceEntity.CreateBy,
                    CreateTime = mainSourceEntity.CreateTime,
                    UpdateBy = mainSourceEntity.UpdateBy,
                    UpdateTime = mainSourceEntity.UpdateTime,
                };
                cmd.MRelationFamousPrescriptionRecords.Add(relationFamousPrescription);
            }
            #endregion
            #region Sex
            string sexName = bo.SexName.ToString();
            var bSex = cmd.BSexRecords.FirstOrDefault(o => o.Name == sexName);
            var relationSex = new MRelationSexRecord()
            {
                SexId = bSex.Id,
                SexName = bSex.Name,
                MainSourceId = mainSourceEntity.MainSourceId,
                CreateBy = mainSourceEntity.CreateBy,
                CreateTime = mainSourceEntity.CreateTime,
                UpdateBy = mainSourceEntity.UpdateBy,
                UpdateTime = mainSourceEntity.UpdateTime
            };
            cmd.MRelationSexRecords.Add(relationSex);
            #endregion
            #region Symptoms
            foreach (var item in bo.Symptoms.Distinct())
            {
                string name = item.ToString();
                var bSymptom = cmd.BSymptomRecords.FirstOrDefault(o => o.Name == name);
                var relationSymptom = new MRelationSymptomRecord()
                {
                    SymptomId = bSymptom.Id,
                    SymptomName = bSymptom.Name,
                    MainSourceId = mainSourceEntity.MainSourceId,
                    CreateBy = mainSourceEntity.CreateBy,
                    CreateTime = mainSourceEntity.CreateTime,
                    UpdateBy = mainSourceEntity.UpdateBy,
                    UpdateTime = mainSourceEntity.UpdateTime
                };
                cmd.MRelationSymptomRecords.Add(relationSymptom);
            }
            #endregion
            #region Syndromes
            foreach (var item in bo.Syndromes.Distinct())
            {
                string name = item.ToString();
                var bSyndromes = cmd.BSyndromesRecords.FirstOrDefault(o => o.Name == name);
                var relationSyndrome = new MRelationSyndromesRecord()
                {
                    SyndromesId = bSyndromes.Id,
                    SyndromesName = bSyndromes.Name,
                    MainSourceId = mainSourceEntity.MainSourceId,
                    CreateBy = mainSourceEntity.CreateBy,
                    CreateTime = mainSourceEntity.CreateTime,
                    UpdateBy = mainSourceEntity.UpdateBy,
                    UpdateTime = mainSourceEntity.UpdateTime
                };
                cmd.MRelationSyndromesRecords.Add(relationSyndrome);
            }
            #endregion
            #region Environment
            string environmentName = bo.EnvironmentName.ToString();
            var bEnvironmentName = cmd.BEnvironmentRecords.FirstOrDefault(o => o.Name == environmentName);
            var relationEnvironment = new MRelationEnvironmentRecord()
            {
                EnvironmentId = bEnvironmentName.Id,
                EnvironmentName = bEnvironmentName.Name,
                MainSourceId = mainSourceEntity.MainSourceId,
                CreateBy = mainSourceEntity.CreateBy,
                CreateTime = mainSourceEntity.CreateTime,
                UpdateBy = mainSourceEntity.UpdateBy,
                UpdateTime = mainSourceEntity.UpdateTime
            };
            cmd.MRelationEnvironmentRecords.Add(relationEnvironment);
            #endregion
            #region Season
            string seasonName = bo.SeasonName.ToString();
            var bSeason = cmd.BSeasonRecords.FirstOrDefault(o => o.Name == seasonName);
            var relationbSeason = new MRelationSeasonRecord()
            {
                SeasonId = bSeason.Id,
                SeasonName = bSeason.Name,
                MainSourceId = mainSourceEntity.MainSourceId,
                CreateBy = mainSourceEntity.CreateBy,
                CreateTime = mainSourceEntity.CreateTime,
                UpdateBy = mainSourceEntity.UpdateBy,
                UpdateTime = mainSourceEntity.UpdateTime
            };
            cmd.MRelationSeasonRecords.Add(relationbSeason);
            #endregion
            //cmd.MMainSourceRecords.Remove(mainSourceEntity);

            cmd.SaveChanges();
        }
    }
}
