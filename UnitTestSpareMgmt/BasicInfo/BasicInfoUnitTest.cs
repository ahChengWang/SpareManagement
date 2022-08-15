using ExpectedObjects;
using NSubstitute;
using NUnit.Framework;
using SpareManagement.DomainService;
using SpareManagement.DomainService.Entity;
using SpareManagement.Repository;
using SpareManagement.Repository.Dao;
using System.Collections.Generic;

namespace UnitTestSpareMgmt
{
    [TestFixture]
    public class BasicInfoUnitTest
    {
        private IBasicInformationRepository _basicInformationRepository;
        private ISerialNumberDomainService _serialNumberDomainService;
        private ICategoryRepository _categoryRepository;


        [SetUp]
        public void Setup()
        {
            _basicInformationRepository = Substitute.For<IBasicInformationRepository>();
            _serialNumberDomainService = Substitute.For<ISerialNumberDomainService>();
        }

        [Test(Author = "Towns", Description = "測試各備品建檔")]
        [TestCaseSource(typeof(BasicInfoDataSetting), nameof(BasicInfoDataSetting.CreateBasicInfo))]
        public void Create_BasicInfo_Success(BasicInfoEntity basicInfoEntity, string expectedResult)
        {
            _basicInformationRepository.GetBasicInfoCategoryIdCount(3).ReturnsForAnyArgs(0);
            _basicInformationRepository.Insert(new BasicInformationDao()).ReturnsForAnyArgs(1);
            _serialNumberDomainService.GeneratePartNo(1, 0).ReturnsForAnyArgs("C_0001");


            var _fakeBasicInformationDomainService = new BasicInformationDomainService(
                basicInformationRepository: _basicInformationRepository,
                serialNumberDomainService: _serialNumberDomainService,
                categoryRepository: _categoryRepository
                );

            var acturl = _fakeBasicInformationDomainService.Create(basicInfoEntity);

            expectedResult.ToExpectedObject().ShouldMatch(acturl);
        }

        [Test(Author = "Towns", Description = "測試「一般耗材」建檔失敗")]
        [TestCaseSource(typeof(BasicInfoDataSetting), nameof(BasicInfoDataSetting.CreateExpendables))]
        public void Create_BasicInfo_Expendables_Failed(BasicInfoEntity basicInfoEntity, string expectedResult)
        {
            _basicInformationRepository.GetBasicInfoCategoryIdCount(3).ReturnsForAnyArgs(0);
            _basicInformationRepository.Insert(new BasicInformationDao()).ReturnsForAnyArgs(1);
            _serialNumberDomainService.GeneratePartNo(1, 0).ReturnsForAnyArgs("C_0001");


            var _fakeBasicInformationDomainService = new BasicInformationDomainService(
                basicInformationRepository: _basicInformationRepository,
                serialNumberDomainService: _serialNumberDomainService,
                categoryRepository: _categoryRepository
                );

            var acturl = _fakeBasicInformationDomainService.Create(basicInfoEntity);

            expectedResult.ToExpectedObject().ShouldMatch(acturl);
        }

        [Test(Author = "Towns", Description = "測試「設備零件」建檔失敗")]
        [TestCaseSource(typeof(BasicInfoDataSetting), nameof(BasicInfoDataSetting.CreateComponents))]
        public void Create_BasicInfo_Components_Failed(BasicInfoEntity basicInfoEntity, string expectedResult)
        {
            _basicInformationRepository.GetBasicInfoCategoryIdCount(3).ReturnsForAnyArgs(0);
            _basicInformationRepository.Insert(new BasicInformationDao()).ReturnsForAnyArgs(1);
            _serialNumberDomainService.GeneratePartNo(1, 0).ReturnsForAnyArgs("E_0001");


            var _fakeBasicInformationDomainService = new BasicInformationDomainService(
                basicInformationRepository: _basicInformationRepository,
                serialNumberDomainService: _serialNumberDomainService,
                categoryRepository: _categoryRepository
                );

            var acturl = _fakeBasicInformationDomainService.Create(basicInfoEntity);

            expectedResult.ToExpectedObject().ShouldMatch(acturl);
        }

        [Test(Author = "Towns", Description = "測試「治具」建檔失敗")]
        [TestCaseSource(typeof(BasicInfoDataSetting), nameof(BasicInfoDataSetting.CreateJigs))]
        public void Create_BasicInfo_Jigs_Failed(BasicInfoEntity basicInfoEntity, string expectedResult)
        {
            _basicInformationRepository.GetBasicInfoCategoryIdCount(3).ReturnsForAnyArgs(0);
            _basicInformationRepository.Insert(new BasicInformationDao()).ReturnsForAnyArgs(1);
            _serialNumberDomainService.GeneratePartNo(1, 0).ReturnsForAnyArgs("E_0001");


            var _fakeBasicInformationDomainService = new BasicInformationDomainService(
                basicInformationRepository: _basicInformationRepository,
                serialNumberDomainService: _serialNumberDomainService,
                categoryRepository: _categoryRepository
                );

            var acturl = _fakeBasicInformationDomainService.Create(basicInfoEntity);

            expectedResult.ToExpectedObject().ShouldMatch(acturl);
        }

        [Test(Author = "Towns", Description = "測試「線板材」建檔失敗")]
        [TestCaseSource(typeof(BasicInfoDataSetting), nameof(BasicInfoDataSetting.CreateWirePanel))]
        public void Create_BasicInfo_WirePanel_Failed(BasicInfoEntity basicInfoEntity, string expectedResult)
        {
            _basicInformationRepository.GetBasicInfoCategoryIdCount(3).ReturnsForAnyArgs(0);
            _basicInformationRepository.Insert(new BasicInformationDao()).ReturnsForAnyArgs(1);
            _serialNumberDomainService.GeneratePartNo(1, 0).ReturnsForAnyArgs("E_0001");


            var _fakeBasicInformationDomainService = new BasicInformationDomainService(
                basicInformationRepository: _basicInformationRepository,
                serialNumberDomainService: _serialNumberDomainService,
                categoryRepository: _categoryRepository
                );

            var acturl = _fakeBasicInformationDomainService.Create(basicInfoEntity);

            expectedResult.ToExpectedObject().ShouldMatch(acturl);
        }
    }

    public class BasicInfoDataSetting
    {
        public static object[] CreateBasicInfo
        {
            get
            {
                var cases = new List<object>()
                {
                    new object[]
                    {
                        new BasicInfoEntity
                        {
                            CategoryId = 1,
                            Name = "螺絲",
                            Spec = "13吋",
                            PurchaseId = "ASD123",
                            IsSpecial = false,
                            IsKeySpare = true,
                            IsCommercial = false,
                            UseLocation = "1,2,3",
                            PurchaseDelivery = 60,
                            SafetyCount = 100,
                            Placement = "A-1-1",
                            CreateUser = "test",
                            Manager = "test",
                            Memo = "123"
                        },
                        ""
                    },
                    new object[]
                    {
                        new BasicInfoEntity
                        {
                            CategoryId = 2,
                            Name = "噴嘴",
                            Spec = "30",
                            PurchaseId = "ASD123",
                            IsSpecial = false,
                            IsKeySpare = true,
                            IsCommercial = false,
                            Equipment = "all",
                            UseLocation = "1,2,3",
                            PurchaseDelivery = 60,
                            SafetyCount = 100,
                            Placement = "A-1-1",
                            CreateUser = "test",
                            Manager = "test",
                            Memo = "123"
                        },
                        ""
                    },
                    new object[]
                    {
                        new BasicInfoEntity
                        {
                            CategoryId = 3,
                            Name = "電腦",
                            Spec = "16吋",
                            PurchaseId = "ASD123",
                            IsSpecial = false,
                            IsKeySpare = true,
                            IsCommercial = false,
                            Equipment = "all",
                            UseLocation = "1,2,3",
                            PurchaseDelivery = 60,
                            SafetyCount = 100,
                            IsNeedInspect = true,
                            InspectCycle = 180,
                            Placement = "A-1-1",
                            CreateUser = "test",
                            Manager = "test",
                            Memo = "123"
                        },
                        ""
                    },
                    new object[]
                    {
                        new BasicInfoEntity
                        {
                            CategoryId = 4,
                            Name = "線材",
                            Spec = "1尺",
                            PurchaseId = "ASD123",
                            IsSpecial = false,
                            IsKeySpare = true,
                            IsCommercial = false,
                            Equipment = "all",
                            UseLocation = "1,2,3",
                            PurchaseDelivery = 60,
                            SafetyCount = 100,
                            IsNeedInspect = true,
                            InspectCycle = 90,
                            Placement = "A-1-1",
                            CreateUser = "test",
                            Manager = "test",
                            Memo = "123"
                        },
                        ""
                    }
                };

                return cases.ToArray();
            }
        }

        public static object[] CreateExpendables
        {
            get
            {
                var cases = new List<object>()
                {
                    new object[]
                    {
                        new BasicInfoEntity
                        {
                            CategoryId = 1,
                            Name = "螺絲",
                            Spec = "13吋",
                            PurchaseId = "ASD123",
                            IsSpecial = false,
                            IsKeySpare = true,
                            IsCommercial = false,
                            UseLocation = "2,4",
                            PurchaseDelivery = 60,
                            SafetyCount = 0,
                            Placement = "A-1-1",
                            CreateUser = "",
                            Manager = "test",
                            Memo = "123"
                        },
                        "安全庫存 \n儲位、建檔人員、管理人員 \n**必填**"
                    }
                };

                return cases.ToArray();
            }
        }

        public static object[] CreateComponents
        {
            get
            {
                var cases = new List<object>()
                {
                    new object[]
                    {
                        new BasicInfoEntity
                        {
                            CategoryId = 2,
                            Name = "吸嘴",
                            Spec = "30",
                            PurchaseId = "",
                            IsSpecial = false,
                            IsKeySpare = true,
                            Equipment = "",
                            IsCommercial = false,
                            UseLocation = "2,4",
                            PurchaseDelivery = 60,
                            SafetyCount = 100,
                            Placement = "A-1-1",
                            CreateUser = "test",
                            Manager = "test",
                            Memo = "123"
                        },
                        "適用機種 \n採購料號 \n**必填**"
                    }
                };

                return cases.ToArray();
            }
        }

        public static object[] CreateJigs
        {
            get
            {
                var cases = new List<object>()
                {
                    new object[]
                    {
                        new BasicInfoEntity
                        {
                            CategoryId = 3,
                            Name = "電腦",
                            Spec = "24吋",
                            PurchaseId = "QWE123",
                            IsSpecial = false,
                            IsKeySpare = true,
                            Equipment = "",
                            IsCommercial = false,
                            UseLocation = "2,4",
                            PurchaseDelivery = 60,
                            IsNeedInspect = true,
                            InspectCycle = 0,
                            SafetyCount = 100,
                            Placement = "A-1-1",
                            CreateUser = "test",
                            Manager = "test",
                            Memo = "123"
                        },
                        "適用機種 \n檢驗週期不能為 0 \n**必填**"
                    }
                };

                return cases.ToArray();
            }
        }

        public static object[] CreateWirePanel
        {
            get
            {
                var cases = new List<object>()
                {
                    new object[]
                    {
                        new BasicInfoEntity
                        {
                            CategoryId = 4,
                            Name = "線材",
                            Spec = "1尺",
                            PurchaseId = "QWE123",
                            IsSpecial = false,
                            IsKeySpare = true,
                            Equipment = "",
                            IsCommercial = false,
                            UseLocation = "2,4",
                            PurchaseDelivery = 60,
                            IsNeedInspect = true,
                            InspectCycle = 0,
                            SafetyCount = 100,
                            Placement = "A-1-1",
                            CreateUser = "test",
                            Manager = "test",
                            Memo = "123"
                        },
                        "適用機種 \n檢驗週期不能為 0 \n**必填**"
                    }
                };

                return cases.ToArray();
            }
        }
    }

}