using SpareManagement.DomainService.Entity;
using SpareManagement.Helper;
using SpareManagement.Repository;
using SpareManagement.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace SpareManagement.DomainService
{
    public class BasicInformationDomainService
    {
        private readonly BasicInformationRepository _basicInformationRepository;
        private readonly SerialNumberDomainService _serialNumberDomainService;
        private readonly CategoryRepository _categoryRepository;


        public BasicInformationDomainService(BasicInformationRepository basicInformationRepository,
            SerialNumberDomainService serialNumberDomainService,
            CategoryRepository categoryRepository)
        {
            _basicInformationRepository = basicInformationRepository;
            _serialNumberDomainService = serialNumberDomainService;
            _categoryRepository = categoryRepository;
        }

        public List<BasicInfoEntity> Select(string partNo, string name, string purchaseId, string placement, string createTime)
        {
            try
            {
                DateTime? _dateStart = createTime != null ? DateTime.Parse(createTime) : (DateTime?)null;
                DateTime? _dateEnd = createTime != null ? DateTime.Parse(createTime).Date.AddDays(1).AddSeconds(-1) : (DateTime?)null;

                var _te = _basicInformationRepository.SelectByConditions(null, partNo ?? "", name ?? "", purchaseId ?? "", placement ?? "", _dateStart, _dateEnd);

                return _te.CopyAToB<BasicInfoEntity>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BasicInfoEntity GetInspectInfo(BasicInfoEntity basicEntity)
            => _basicInformationRepository.SelectByConditions(partNoList: new List<string> { basicEntity.PartNo }).CopyAToB<BasicInfoEntity>().FirstOrDefault();

        public BasicInfoEntity GetEditBasicInfo(BasicInfoEntity basicEntity)
        {
            BasicInfoEntity _basicInfo = GetInspectInfo(basicEntity);
            var _category = _categoryRepository.SelectByConditions(_basicInfo.CategoryId);
            _basicInfo.Category = _category.FirstOrDefault().Name;
            _basicInfo.SpareDesc = _category.FirstOrDefault().Description;

            return _basicInfo;
        }

        public string Create(BasicInfoEntity basicEntity)
        {

            try
            {
                string _validateInfo = Validate(basicEntity);

                if (_validateInfo != "")
                    return _validateInfo;

                // 查詢類別數量
                var _categoryCnt = _basicInformationRepository.GetBasicInfoCategoryIdCount(basicEntity.CategoryId);

                // 計算類別料號
                var _newPartNo = _serialNumberDomainService.GeneratePartNo(basicEntity.CategoryId, _categoryCnt);

                BasicInformationDao _basicDao = new BasicInformationDao
                {
                    CategoryId = basicEntity.CategoryId,
                    PartNo = _newPartNo,
                    Name = basicEntity.Name,
                    Spec = basicEntity.Spec,
                    PurchaseId = basicEntity.PurchaseId,
                    IsSpecial = basicEntity.IsSpecial,
                    IsKeySpare = basicEntity.IsKeySpare,
                    IsCommercial = basicEntity.IsCommercial,
                    Equipment = basicEntity.Equipment,
                    UseLocation = basicEntity.UseLocation,
                    PurchaseDelivery = basicEntity.PurchaseDelivery,
                    SafetyCount = basicEntity.SafetyCount,
                    IsNeedInspect = basicEntity.IsNeedInspect,
                    //InspectDate = basicEntity.InspectDate.ToString("yyyyMMdd") == "00010101" ? (DateTime?)null : basicEntity.InspectDate,
                    InspectCycle = basicEntity.InspectCycle,
                    Placement = basicEntity.Placement,
                    CreateUser = basicEntity.CreateUser,
                    CreateDate = DateTime.Now,
                    Manager = basicEntity.Manager,
                    Memo = basicEntity.Memo ?? ""
                };

                using (var scope = new TransactionScope())
                {
                    var _insResult = _basicInformationRepository.Insert(_basicDao) == 1;

                    if (_insResult)
                    {
                        scope.Complete();
                    }
                    else
                    {
                        return "物料建檔失敗";
                    }
                }

                //_serialNumberDomainService.UpdateSerialNo(basicEntity.CategoryId, 0, false);

                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Update(BasicInfoEntity basicEntity)
        {
            try
            {
                var _oldData = _basicInformationRepository.SelectByConditions(partNo:basicEntity.PartNo);

                if (_oldData.Count != 1)
                    return "資料錯誤";

                using (var scope = new TransactionScope())
                {
                    var _updResult = _basicInformationRepository.Update(new BasicInformationDao
                    {
                        PartNo = basicEntity.PartNo,
                        PurchaseId = basicEntity.PurchaseId,
                        Placement = basicEntity.Placement
                    }) == 1;

                    if (_updResult)
                    {
                        scope.Complete();
                    }
                    else
                    {
                        return "物料資訊更新失敗";
                    }
                }
                

                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Validate(BasicInfoEntity basicEntity)
        {
            var _errorInfo = "";

            if ((new int[3] { 2, 3, 4 }).Contains(basicEntity.CategoryId) && string.IsNullOrEmpty(basicEntity.Equipment.Trim()))
                _errorInfo += "適用機種必填 ";

            if ((new int[2] { 3, 4 }).Contains(basicEntity.CategoryId) && basicEntity.InspectCycle == 0)
                _errorInfo += "檢驗週期為 0 ";

            return _errorInfo;
        }
    }
}
