using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingWeb.Repository;
using ShoppingWeb.Controller;
using ShoppingWeb;
using Moq;
using System.Collections.Generic;
using System.Data;
using System;

namespace ShoppingWebTest.ControllerTest
{
    [TestClass]
    public class MemberControllerTest
    {
        private readonly MemberController _memberController;
        private readonly Mock<IMemberRepository> _repo;
        private readonly PrivateObject _privateObject;

        public MemberControllerTest()
        {
            _memberController = new MemberController();
            _repo = new Mock<IMemberRepository>();
            _privateObject = new PrivateObject(_memberController);
        }

        /// <summary>
        /// ID資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> MemberIdData()
        {
            for (int i = -10; i <= 10; i++)
            {
                int id = i;
                ActionResult res = (i >= 1 && i <= int.MaxValue) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { id, res };
            }
        }

        /// <summary>
        /// Level資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> MemberLevelData()
        {
            for (int i = -10; i <= 10; i++)
            {
                int level = i;
                ActionResult res = (i >= 0 && i <= 3) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { level, res };
            }
        }

        /// <summary>
        /// Account資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> MemberAccountData()
        {
            for (int i = 1; i <= 20; i++)
            {
                string account = new string('a', i);
                ActionResult res = (i >= 6 && i <= 16) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { account, res };
            }
        }

        /// <summary>
        /// Pwd資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> MemberPwdData()
        {
            for (int i = 1; i <= 20; i++)
            {
                string pwd = new string('a', i);
                ActionResult res = (i >= 6 && i <= 16) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { pwd, res };
            }
        }

        /// <summary>
        /// Name資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> MemberNameData()
        {
            for (int i = 1; i <= 20; i++)
            {
                string name = new string('我', i);
                ActionResult res = (i >= 1 && i <= 15) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { name, res };
            }
        }

        /// <summary>
        /// Birthday資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> MemberBirthdayData()
        {
            for (int i = 1; i <= 20; i++)
            {
                string birthday = new string('1', i);
                ActionResult res = (i >= 8 && i <= 10) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { birthday, res };
            }
        }

        /// <summary>
        /// Phone資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> MemberPhoneData()
        {
            for (int i = 1; i <= 20; i++)
            {
                string phone = new string('1', i);
                ActionResult res = (i == 10) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { phone, res };
            }
        }

        /// <summary>
        /// Address資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> MemberAddressData()
        {
            for (int i = 40; i <= 60; i++)
            {
                string address = new string('1', i);
                ActionResult res = (i >= 2 && i <= 50) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { address, res };
            }
        }

        /// <summary>
        /// PageNumber資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> PageNumberData()
        {
            for (int i = -10; i <= 10; i++)
            {
                int pageNumber = i;
                ActionResult res = (i >= 1 && i <= int.MaxValue) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { pageNumber, res };
            }
        }

        /// <summary>
        /// PageSize資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> PageSizeData()
        {
            for (int i = -10; i <= 10; i++)
            {
                int pageSize = i;
                ActionResult res = (i >= 1 && i <= int.MaxValue) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { pageSize, res };
            }
        }

        /// <summary>
        /// BeforePagesTotal資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> BeforePagesTotalData()
        {
            for (int i = -10; i <= 10; i++)
            {
                int beforePagesTotal = i;
                ActionResult res = (i >= 1 && i <= int.MaxValue) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { beforePagesTotal, res };
            }
        }




        /// <summary>
        /// EditMemberStatus成功
        /// </summary>
        [TestMethod]
        public void EditMemberStatusSuccess()
        {
            _repo.Setup(x => x.EditMemberStatus(It.IsAny<EditMemberStatusDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            EditMemberStatusDto editMemberStatusDto = new EditMemberStatusDto
            {
                MemberId = 1
            };
            var result = _memberController.EditMemberStatus(editMemberStatusDto);
            _repo.Verify(x => x.EditMemberStatus(It.IsAny<EditMemberStatusDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// EditMemberStatus失敗
        /// </summary>
        [TestMethod]
        public void EditMemberStatusFailure()
        {
            _repo.Setup(x => x.EditMemberStatus(It.IsAny<EditMemberStatusDto>())).Returns((null, 0));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            EditMemberStatusDto editMemberStatusDto = new EditMemberStatusDto
            {
                MemberId = 1
            };
            var result = _memberController.EditMemberStatus(editMemberStatusDto);
            _repo.Verify(x => x.EditMemberStatus(It.IsAny<EditMemberStatusDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// EditMemberStatus例外
        /// </summary>
        [TestMethod]
        public void EditMemberStatusException()
        {
            _repo.Setup(x => x.EditMemberStatus(It.IsAny<EditMemberStatusDto>())).Returns((new Exception("EditMemberStatus單元測試"), null));
            _repo.Setup(x => x.SetNLog(It.IsAny<Exception>()));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            EditMemberStatusDto editMemberStatusDto = new EditMemberStatusDto
            {
                MemberId = 1
            };
            var result = _memberController.EditMemberStatus(editMemberStatusDto);
            _repo.Verify(x => x.EditMemberStatus(It.IsAny<EditMemberStatusDto>()), Times.Once);
            _repo.Verify(x => x.SetNLog(It.IsAny<Exception>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Error);
        }

        /// <summary>
        /// EditMemberStatus ID判斷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(MemberIdData), DynamicDataSourceType.Method)]
        public void EditMemberStatusInputId(int id, ActionResult res)
        {
            _repo.Setup(x => x.EditMemberStatus(It.IsAny<EditMemberStatusDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            EditMemberStatusDto editMemberStatusDto = new EditMemberStatusDto
            {
                MemberId = id
            };
            var result = _memberController.EditMemberStatus(editMemberStatusDto);
            Assert.AreEqual(result.Status, res);
        }



        /// <summary>
        /// EditMemberLevel成功
        /// </summary>
        [TestMethod]
        public void EditMemberLevelSuccess()
        {
            _repo.Setup(x => x.EditMemberLevel(It.IsAny<EditMemberLevelDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            EditMemberLevelDto editMemberLevelDto = new EditMemberLevelDto
            {
                MemberId = 1,
                Level = 1
            };
            var result = _memberController.EditMemberLevel(editMemberLevelDto);
            _repo.Verify(x => x.EditMemberLevel(It.IsAny<EditMemberLevelDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// EditMemberLevel失敗
        /// </summary>
        [TestMethod]
        public void EditMemberLevelFailure()
        {
            _repo.Setup(x => x.EditMemberLevel(It.IsAny<EditMemberLevelDto>())).Returns((null, 0));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            EditMemberLevelDto editMemberLevelDto = new EditMemberLevelDto
            {
                MemberId = 1,
                Level = 1
            };
            var result = _memberController.EditMemberLevel(editMemberLevelDto);
            _repo.Verify(x => x.EditMemberLevel(It.IsAny<EditMemberLevelDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// EditMemberLevel例外
        /// </summary>
        [TestMethod]
        public void EditMemberLevelException()
        {
            _repo.Setup(x => x.EditMemberLevel(It.IsAny<EditMemberLevelDto>())).Returns((new Exception("EditMemberLevel單元測試"), null));
            _repo.Setup(x => x.SetNLog(It.IsAny<Exception>()));
            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            EditMemberLevelDto editMemberLevelDto = new EditMemberLevelDto
            {
                MemberId = 1,
                Level = 1
            };
            var result = _memberController.EditMemberLevel(editMemberLevelDto);
            _repo.Verify(x => x.EditMemberLevel(It.IsAny<EditMemberLevelDto>()), Times.Once);
            _repo.Verify(x => x.SetNLog(It.IsAny<Exception>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Error);
        }

        /// <summary>
        /// EditMemberLevel ID判斷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(MemberIdData), DynamicDataSourceType.Method)]
        public void EditMemberLevelInputId(int id, ActionResult res)
        {
            _repo.Setup(x => x.EditMemberLevel(It.IsAny<EditMemberLevelDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            EditMemberLevelDto editMemberLevelDto = new EditMemberLevelDto
            {
                MemberId = id,
                Level = 1
            };
            var result = _memberController.EditMemberLevel(editMemberLevelDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// EditMemberLevel Level判斷
        /// </summary>
        /// <param name="level"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(MemberLevelData), DynamicDataSourceType.Method)]
        public void EditMemberLevelInputLevel(int level, ActionResult res)
        {
            _repo.Setup(x => x.EditMemberLevel(It.IsAny<EditMemberLevelDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            EditMemberLevelDto editMemberLevelDto = new EditMemberLevelDto
            {
                MemberId = 1,
                Level = level
            };
            var result = _memberController.EditMemberLevel(editMemberLevelDto);
            Assert.AreEqual(result.Status, res);
        }



        /// <summary>
        /// AddMember成功
        /// </summary>
        [TestMethod]
        public void AddMemberSuccess()
        {
            _repo.Setup(x => x.AddMember(It.IsAny<AddMemberDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            AddMemberDto addMemberDto = new AddMemberDto
            {
                Account = "test11",
                Pwd = "123456",
                Name = "東南西",
                Birthday = "2019-01-27",
                Phone = "0934413574",
                Email = "J6LO27Dnn3@yahoo.com",
                Address = "台中市"
            };
            var result = _memberController.AddMember(addMemberDto);
            _repo.Verify(x => x.AddMember(It.IsAny<AddMemberDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// AddMember失敗
        /// </summary>
        [TestMethod]
        public void AddMemberFailure()
        {
            _repo.Setup(x => x.AddMember(It.IsAny<AddMemberDto>())).Returns((null, 0));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            AddMemberDto addMemberDto = new AddMemberDto
            {
                Account = "test11",
                Pwd = "123456",
                Name = "東南西",
                Birthday = "2019-01-27",
                Phone = "0934413574",
                Email = "J6LO27Dnn3@yahoo.com",
                Address = "台中市"
            };
            var result = _memberController.AddMember(addMemberDto);
            _repo.Verify(x => x.AddMember(It.IsAny<AddMemberDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// AddMember例外
        /// </summary>
        [TestMethod]
        public void AddMemberException()
        {
            _repo.Setup(x => x.AddMember(It.IsAny<AddMemberDto>())).Returns((new Exception("AddMember單元測試"), null));
            _repo.Setup(x => x.SetNLog(It.IsAny<Exception>()));
            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            AddMemberDto addMemberDto = new AddMemberDto
            {
                Account = "test11",
                Pwd = "123456",
                Name = "東南西",
                Birthday = "2019-01-27",
                Phone = "0934413574",
                Email = "J6LO27Dnn3@yahoo.com",
                Address = "台中市"
            };
            var result = _memberController.AddMember(addMemberDto);
            _repo.Verify(x => x.AddMember(It.IsAny<AddMemberDto>()), Times.Once);
            _repo.Verify(x => x.SetNLog(It.IsAny<Exception>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Error);
        }

        /// <summary>
        /// AddMember帳號長度判斷
        /// </summary>
        /// <param name="account"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(MemberAccountData), DynamicDataSourceType.Method)]
        public void AddMemberAccountLength(string account, ActionResult res)
        {
            _repo.Setup(x => x.AddMember(It.IsAny<AddMemberDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            AddMemberDto addMemberDto = new AddMemberDto
            {
                Account = account,
                Pwd = "123456",
                Name = "東南西",
                Birthday = "2019-01-27",
                Phone = "0934413574",
                Email = "J6LO27Dnn3@yahoo.com",
                Address = "台中市"
            };
            var result = _memberController.AddMember(addMemberDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// AddMember帳號特殊符號判斷
        /// </summary>
        /// <param name="account"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DataRow("test11-", ActionResult.InputError)]
        public void AddMemberAccountSpecial(string account, ActionResult res)
        {
            _repo.Setup(x => x.AddMember(It.IsAny<AddMemberDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            AddMemberDto addMemberDto = new AddMemberDto
            {
                Account = account,
                Pwd = "123456",
                Name = "東南西",
                Birthday = "2019-01-27",
                Phone = "0934413574",
                Email = "J6LO27Dnn3@yahoo.com",
                Address = "台中市"
            };
            var result = _memberController.AddMember(addMemberDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// AddMember密碼長度判斷
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(MemberPwdData), DynamicDataSourceType.Method)]
        public void AddMemberPwdLength(string pwd, ActionResult res)
        {
            _repo.Setup(x => x.AddMember(It.IsAny<AddMemberDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            AddMemberDto addMemberDto = new AddMemberDto
            {
                Account = "test11",
                Pwd = pwd,
                Name = "東南西",
                Birthday = "2019-01-27",
                Phone = "0934413574",
                Email = "J6LO27Dnn3@yahoo.com",
                Address = "台中市"
            };
            var result = _memberController.AddMember(addMemberDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// AddMember密碼特殊符號判斷
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DataRow("123456-", ActionResult.InputError)]
        public void AddMemberPwdSpecial(string pwd, ActionResult res)
        {
            _repo.Setup(x => x.AddMember(It.IsAny<AddMemberDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            AddMemberDto addMemberDto = new AddMemberDto
            {
                Account = "test11",
                Pwd = pwd,
                Name = "東南西",
                Birthday = "2019-01-27",
                Phone = "0934413574",
                Email = "J6LO27Dnn3@yahoo.com",
                Address = "台中市"
            };
            var result = _memberController.AddMember(addMemberDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// AddMember名稱長度判斷
        /// </summary>
        /// <param name="name"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(MemberNameData), DynamicDataSourceType.Method)]
        public void AddMemberNameLength(string name, ActionResult res)
        {
            _repo.Setup(x => x.AddMember(It.IsAny<AddMemberDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            AddMemberDto addMemberDto = new AddMemberDto
            {
                Account = "test11",
                Pwd = "123456",
                Name = name,
                Birthday = "2019-01-27",
                Phone = "0934413574",
                Email = "J6LO27Dnn3@yahoo.com",
                Address = "台中市"
            };
            var result = _memberController.AddMember(addMemberDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// AddMember名稱特殊符號判斷
        /// </summary>
        /// <param name="name"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DataRow("我我-", ActionResult.InputError)]
        public void AddMemberNameSpecial(string name, ActionResult res)
        {
            _repo.Setup(x => x.AddMember(It.IsAny<AddMemberDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            AddMemberDto addMemberDto = new AddMemberDto
            {
                Account = "test11",
                Pwd = "123456",
                Name = name,
                Birthday = "2019-01-27",
                Phone = "0934413574",
                Email = "J6LO27Dnn3@yahoo.com",
                Address = "台中市"
            };
            var result = _memberController.AddMember(addMemberDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// AddMember日期長度判斷
        /// </summary>
        /// <param name="birthday"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(MemberBirthdayData), DynamicDataSourceType.Method)]
        public void AddMemberBirthdayLength(string birthday, ActionResult res)
        {
            _repo.Setup(x => x.AddMember(It.IsAny<AddMemberDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            AddMemberDto addMemberDto = new AddMemberDto
            {
                Account = "test11",
                Pwd = "123456",
                Name = "東南西",
                Birthday = birthday,
                Phone = "0934413574",
                Email = "J6LO27Dnn3@yahoo.com",
                Address = "台中市"
            };
            var result = _memberController.AddMember(addMemberDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// AddMember日期特殊符號判斷
        /// </summary>
        /// <param name="birthday"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DataRow("1989-08-ad", ActionResult.InputError)]
        public void AddMemberBirthdaySpecial(string birthday, ActionResult res)
        {
            _repo.Setup(x => x.AddMember(It.IsAny<AddMemberDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            AddMemberDto addMemberDto = new AddMemberDto
            {
                Account = "test11",
                Pwd = "123456",
                Name = "東南西",
                Birthday = birthday,
                Phone = "0934413574",
                Email = "J6LO27Dnn3@yahoo.com",
                Address = "台中市"
            };
            var result = _memberController.AddMember(addMemberDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// AddMember手機長度判斷
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(MemberPhoneData), DynamicDataSourceType.Method)]
        public void AddMemberPhoneLength(string phone, ActionResult res)
        {
            _repo.Setup(x => x.AddMember(It.IsAny<AddMemberDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            AddMemberDto addMemberDto = new AddMemberDto
            {
                Account = "test11",
                Pwd = "123456",
                Name = "東南西",
                Birthday = "2019-01-27",
                Phone = phone,
                Email = "J6LO27Dnn3@yahoo.com",
                Address = "台中市"
            };
            var result = _memberController.AddMember(addMemberDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// AddMember手機特殊符號判斷
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DataRow("091234569-", ActionResult.InputError)]
        public void AddMemberPhoneSpecial(string phone, ActionResult res)
        {
            _repo.Setup(x => x.AddMember(It.IsAny<AddMemberDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            AddMemberDto addMemberDto = new AddMemberDto
            {
                Account = "test11",
                Pwd = "123456",
                Name = "東南西",
                Birthday = "2019-01-27",
                Phone = phone,
                Email = "J6LO27Dnn3@yahoo.com",
                Address = "台中市"
            };
            var result = _memberController.AddMember(addMemberDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// AddMember地址長度判斷
        /// </summary>
        /// <param name="address"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(MemberAddressData), DynamicDataSourceType.Method)]
        public void AddMemberAddressLength(string address, ActionResult res)
        {
            _repo.Setup(x => x.AddMember(It.IsAny<AddMemberDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            AddMemberDto addMemberDto = new AddMemberDto
            {
                Account = "test11",
                Pwd = "123456",
                Name = "東南西",
                Birthday = "2019-01-27",
                Phone = "0934413574",
                Email = "J6LO27Dnn3@yahoo.com",
                Address = address
            };
            var result = _memberController.AddMember(addMemberDto);
            Assert.AreEqual(result.Status, res);
        }



        /// <summary>
        /// GetAllMemberData成功
        /// </summary>
        [TestMethod]
        public void GetAllMemberDataSuccess()
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_id", typeof(int));
            dt.Columns.Add("f_account", typeof(string));
            dt.Columns.Add("f_pwd", typeof(string));
            dt.Columns.Add("f_name", typeof(string));
            dt.Columns.Add("f_level", typeof(byte));
            dt.Columns.Add("f_phoneNumber", typeof(string));
            dt.Columns.Add("f_email", typeof(string));
            dt.Columns.Add("f_accountStatus", typeof(bool));
            dt.Columns.Add("f_amount", typeof(int));
            dt.Columns.Add("f_totalSpent", typeof(int));
            DataRow row = dt.NewRow();
            row["f_id"] = 1;
            row["f_account"] = "test11";
            row["f_pwd"] = "123456";
            row["f_name"] = "東南西";
            row["f_level"] = 1;
            row["f_phoneNumber"] = "0934413574";
            row["f_email"] = "J6LO27Dnn3@yahoo.com";
            row["f_accountStatus"] = true;
            row["f_amount"] = 0;
            row["f_totalSpent"] = 0;
            dt.Rows.Add(row);

            _repo.Setup(x => x.GetAllMemberData(It.IsAny<GetAllMemberDataDto>())).Returns((null, 1, dt));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            GetAllMemberDataDto getAllMemberDataDto = new GetAllMemberDataDto
            {
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _memberController.GetAllMemberData(getAllMemberDataDto);
            _repo.Verify(x => x.GetAllMemberData(It.IsAny<GetAllMemberDataDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// GetAllMemberData沒有資料
        /// </summary>
        [TestMethod]
        public void GetAllMemberDataFailure()
        {
            _repo.Setup(x => x.GetAllMemberData(It.IsAny<GetAllMemberDataDto>())).Returns((null, 0, null));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            GetAllMemberDataDto getAllMemberDataDto = new GetAllMemberDataDto
            {
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _memberController.GetAllMemberData(getAllMemberDataDto);
            _repo.Verify(x => x.GetAllMemberData(It.IsAny<GetAllMemberDataDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// GetAllMemberData例外
        /// </summary>
        [TestMethod]
        public void GetAllMemberDataException()
        {
            _repo.Setup(x => x.GetAllMemberData(It.IsAny<GetAllMemberDataDto>())).Returns((new Exception("GetAllMemberData單元測試"), null, null));
            _repo.Setup(x => x.SetNLog(It.IsAny<Exception>()));
            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            GetAllMemberDataDto getAllMemberDataDto = new GetAllMemberDataDto
            {
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _memberController.GetAllMemberData(getAllMemberDataDto);
            _repo.Verify(x => x.GetAllMemberData(It.IsAny<GetAllMemberDataDto>()), Times.Once);
            _repo.Verify(x => x.SetNLog(It.IsAny<Exception>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Error);
        }

        /// <summary>
        /// GetAllMemberData的PageNumber參數判斷
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(PageNumberData), DynamicDataSourceType.Method)]
        public void GetAllMemberDataInputPageNumber(int pageNumber, ActionResult res)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_id", typeof(int));
            dt.Columns.Add("f_account", typeof(string));
            dt.Columns.Add("f_pwd", typeof(string));
            dt.Columns.Add("f_name", typeof(string));
            dt.Columns.Add("f_level", typeof(byte));
            dt.Columns.Add("f_phoneNumber", typeof(string));
            dt.Columns.Add("f_email", typeof(string));
            dt.Columns.Add("f_accountStatus", typeof(bool));
            dt.Columns.Add("f_amount", typeof(int));
            dt.Columns.Add("f_totalSpent", typeof(int));
            DataRow row = dt.NewRow();
            row["f_id"] = 1;
            row["f_account"] = "test11";
            row["f_pwd"] = "123456";
            row["f_name"] = "東南西";
            row["f_level"] = 1;
            row["f_phoneNumber"] = "0934413574";
            row["f_email"] = "J6LO27Dnn3@yahoo.com";
            row["f_accountStatus"] = true;
            row["f_amount"] = 0;
            row["f_totalSpent"] = 0;
            dt.Rows.Add(row);

            _repo.Setup(x => x.GetAllMemberData(It.IsAny<GetAllMemberDataDto>())).Returns((null, 1, dt));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            GetAllMemberDataDto getAllMemberDataDto = new GetAllMemberDataDto
            {
                PageNumber = pageNumber,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _memberController.GetAllMemberData(getAllMemberDataDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// GetAllMemberData的PageSize參數判斷
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(PageSizeData), DynamicDataSourceType.Method)]
        public void GetAllMemberDataInputPageSize(int pageSize, ActionResult res)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_id", typeof(int));
            dt.Columns.Add("f_account", typeof(string));
            dt.Columns.Add("f_pwd", typeof(string));
            dt.Columns.Add("f_name", typeof(string));
            dt.Columns.Add("f_level", typeof(byte));
            dt.Columns.Add("f_phoneNumber", typeof(string));
            dt.Columns.Add("f_email", typeof(string));
            dt.Columns.Add("f_accountStatus", typeof(bool));
            dt.Columns.Add("f_amount", typeof(int));
            dt.Columns.Add("f_totalSpent", typeof(int));
            DataRow row = dt.NewRow();
            row["f_id"] = 1;
            row["f_account"] = "test11";
            row["f_pwd"] = "123456";
            row["f_name"] = "東南西";
            row["f_level"] = 1;
            row["f_phoneNumber"] = "0934413574";
            row["f_email"] = "J6LO27Dnn3@yahoo.com";
            row["f_accountStatus"] = true;
            row["f_amount"] = 0;
            row["f_totalSpent"] = 0;
            dt.Rows.Add(row);

            _repo.Setup(x => x.GetAllMemberData(It.IsAny<GetAllMemberDataDto>())).Returns((null, 1, dt));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            GetAllMemberDataDto getAllMemberDataDto = new GetAllMemberDataDto
            {
                PageNumber = 1,
                PageSize = pageSize,
                BeforePagesTotal = 1
            };
            var result = _memberController.GetAllMemberData(getAllMemberDataDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// GetAllMemberData的BeforePagesTotalData參數判斷
        /// </summary>
        /// <param name="beforePagesTotal"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(BeforePagesTotalData), DynamicDataSourceType.Method)]
        public void GetAllMemberDataInputBeforePagesTotal(int beforePagesTotal, ActionResult res)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_id", typeof(int));
            dt.Columns.Add("f_account", typeof(string));
            dt.Columns.Add("f_pwd", typeof(string));
            dt.Columns.Add("f_name", typeof(string));
            dt.Columns.Add("f_level", typeof(byte));
            dt.Columns.Add("f_phoneNumber", typeof(string));
            dt.Columns.Add("f_email", typeof(string));
            dt.Columns.Add("f_accountStatus", typeof(bool));
            dt.Columns.Add("f_amount", typeof(int));
            dt.Columns.Add("f_totalSpent", typeof(int));
            DataRow row = dt.NewRow();
            row["f_id"] = 1;
            row["f_account"] = "test11";
            row["f_pwd"] = "123456";
            row["f_name"] = "東南西";
            row["f_level"] = 1;
            row["f_phoneNumber"] = "0934413574";
            row["f_email"] = "J6LO27Dnn3@yahoo.com";
            row["f_accountStatus"] = true;
            row["f_amount"] = 0;
            row["f_totalSpent"] = 0;
            dt.Rows.Add(row);

            _repo.Setup(x => x.GetAllMemberData(It.IsAny<GetAllMemberDataDto>())).Returns((null, 1, dt));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            GetAllMemberDataDto getAllMemberDataDto = new GetAllMemberDataDto
            {
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = beforePagesTotal
            };
            var result = _memberController.GetAllMemberData(getAllMemberDataDto);
            Assert.AreEqual(result.Status, res);
        }
    }
}
