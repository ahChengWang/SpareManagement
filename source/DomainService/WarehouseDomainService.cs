using SpareManagement.DomainService.Entity;
using SpareManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareManagement.DomainService
{
    public class WarehouseDomainService : IWarehouseDomainService
    {
        private readonly IBasicInformationRepository _basicInformationRepository;
        private readonly IExpendablesDomainService _expendablesDomainService;
        private readonly IComponentsDomainService _componentsDomainService;
        private readonly IJigsDomainService _jigsDomainService;
        private readonly IWirePanelDomainService _wirePanelDomainService;
        private readonly ISampleDomainService _sampleDomainService;

        public WarehouseDomainService(IBasicInformationRepository basicInformationRepository,
            IExpendablesDomainService expendablesDomainService,
            IComponentsDomainService componentsDomainService,
            IJigsDomainService jigsDomainService,
            IWirePanelDomainService wirePanelDomainService,
            ISampleDomainService sampleDomainService)
        {
            _basicInformationRepository = basicInformationRepository;
            _expendablesDomainService = expendablesDomainService;
            _componentsDomainService = componentsDomainService;
            _jigsDomainService = jigsDomainService;
            _wirePanelDomainService = wirePanelDomainService;
            _sampleDomainService = sampleDomainService;
        }

        public string Insert(WarehouseInsertEntity entity)
        {
            try
            {
                var _result = Validate(entity);

                if (_result.Item1 != "")
                    return _result.Item1;

                var basicDataList = _basicInformationRepository.SelectByConditions(partNoList: _result.Item2.Select(s => s.PartNo).ToList());

                var _nonExistPartNo = string.Join(',', _result.Item2.Select(s => s.PartNo).Where(w => !basicDataList.Select(s => s.PartNo).Contains(w)));

                if (basicDataList.Count() == 0 || _nonExistPartNo != "")
                    return $"{_nonExistPartNo} 物料編號不存在";

                Task<string> _expendablesInsTask = null;
                Task<string> _componentsInsTask = null;
                Task<string> _jigsInsTask = null;
                Task<string> _wirePanelInsTask = null;
                Task<string> _sampleInsTask = null;

                if (basicDataList.Where(w => w.CategoryId == 5).Any())
                {
                    if (_result.Item2.Where(w => basicDataList.Where(w => w.CategoryId == 5).ToList().Select(s => s.PartNo).Contains(w.PartNo)).Any(a => a.Count > 1))
                        return "Sample 入庫數量不得 > 1";

                    _sampleInsTask = Task.Run(() =>
                        _sampleDomainService.ProcessWarehouse(basicDataList.Where(w => w.CategoryId == 5), _result.Item2, entity.CreateUser, (DateTime)entity.CreateDate, entity.Memo));
                }

                if (basicDataList.Where(w => w.CategoryId == 1).Any())
                {
                    _expendablesInsTask = Task.Run(() =>
                        _expendablesDomainService.ProcessWarehouse(basicDataList.Where(w => w.CategoryId == 1), _result.Item2, entity.CreateUser, (DateTime)entity.CreateDate, entity.Memo));
                }

                if (basicDataList.Where(w => w.CategoryId == 2).Any())
                {
                    _componentsInsTask = Task.Run(() => 
                        _componentsDomainService.ProcessWarehouse(basicDataList.Where(w => w.CategoryId == 2), _result.Item2, entity.CreateUser, (DateTime)entity.CreateDate, entity.Memo));
                }

                if (basicDataList.Where(w => w.CategoryId == 3).Any())
                {
                    _jigsInsTask = Task.Run(() => 
                        _jigsDomainService.ProcessWarehouse(basicDataList.Where(w => w.CategoryId == 3), _result.Item2, entity.CreateUser, (DateTime)entity.CreateDate, entity.Memo));
                }

                if (basicDataList.Where(w => w.CategoryId == 4).Any())
                {
                    _wirePanelInsTask = Task.Run(() =>
                        _wirePanelDomainService.ProcessWarehouse(basicDataList.Where(w => w.CategoryId == 4), _result.Item2, entity.CreateUser, (DateTime)entity.CreateDate, entity.Memo));
                }

                _result.Item1 += _expendablesInsTask?.GetAwaiter().GetResult() ?? "";
                _result.Item1 += _componentsInsTask?.GetAwaiter().GetResult() ?? "";
                _result.Item1 += _jigsInsTask?.GetAwaiter().GetResult() ?? "";
                _result.Item1 += _wirePanelInsTask?.GetAwaiter().GetResult() ?? "";
                _result.Item1 += _sampleInsTask?.GetAwaiter().GetResult() ?? "";

                return _result.Item1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private (string, List<WarehouseGoodsEntity>) Validate(WarehouseInsertEntity entity)
        {
            var _errorInfo = "";

            //if (entity.Count1 != 0 && string.IsNullOrEmpty(entity.PartNo1))
            //    _errorInfo += "編號 1. \n";
            //if (entity.Count2 != 0 && string.IsNullOrEmpty(entity.PartNo2))
            //    _errorInfo += "編號 2. \n";
            //if (entity.Count3 != 0 && string.IsNullOrEmpty(entity.PartNo3))
            //    _errorInfo += "編號 3. \n";
            //if (entity.Count4 != 0 && string.IsNullOrEmpty(entity.PartNo4))
            //    _errorInfo += "編號 4. \n";
            //if (entity.Count5 != 0 && string.IsNullOrEmpty(entity.PartNo5))
            //    _errorInfo += "編號 5. \n";

            var _tempList = new List<WarehouseGoodsEntity> {
                new WarehouseGoodsEntity {ColName = "編號 1.", PartNo = entity.PartNo1, Count = entity.Count1 },
                new WarehouseGoodsEntity {ColName = "編號 2.", PartNo = entity.PartNo2, Count = entity.Count2 },
                new WarehouseGoodsEntity {ColName = "編號 3.", PartNo = entity.PartNo3, Count = entity.Count3 },
                new WarehouseGoodsEntity {ColName = "編號 4.", PartNo = entity.PartNo4, Count = entity.Count4 },
                new WarehouseGoodsEntity {ColName = "編號 5.", PartNo = entity.PartNo5, Count = entity.Count5 }
            };

            if (_tempList.Where(a => string.IsNullOrEmpty(a.PartNo)).Count() == 5)
                return ("無須入庫物料", null);


            if (entity.CreateUser == "" || entity.CreateDate == null)
                _errorInfo += "入庫人員 & 日期 必填 \n";

            var _noPartNo = _tempList.Where(a => string.IsNullOrEmpty(a.PartNo) && a.Count != 0);
            var _noCnt = _tempList.Where(a => !string.IsNullOrEmpty(a.PartNo) && a.Count == 0);

            if (_noPartNo.Any())
                _noPartNo.ToList().ForEach(fe => _errorInfo += $"{fe.ColName} 物料編號必填\n");
            if (_noCnt.Any())
                _noCnt.ToList().ForEach(fe => _errorInfo += $"{fe.ColName} 數量必填\n");

            return (_errorInfo, _tempList.Where(w => !string.IsNullOrEmpty(w.PartNo) && w.Count > 0).ToList());
        }
    }
}
