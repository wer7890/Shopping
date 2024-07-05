using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingWeb.Repository;
using ShoppingWeb.Controller;
using ShoppingWeb;
using Moq;
using System.Collections.Generic;
using System.Data;

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
                ActionResult expected = (i >= 1 && i <= int.MaxValue) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { id, expected };
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
                ActionResult expected = (i >= 0 && i <= 3) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { level, expected };
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
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// EditMemberStatus ID判斷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DynamicData(nameof(MemberIdData), DynamicDataSourceType.Method)]
        public void EditMemberStatusInputId(int id, ActionResult expected)
        {
            _repo.Setup(x => x.EditMemberStatus(It.IsAny<EditMemberStatusDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            EditMemberStatusDto editMemberStatusDto = new EditMemberStatusDto
            {
                MemberId = id
            };
            var result = _memberController.EditMemberStatus(editMemberStatusDto);
            Assert.AreEqual(result.Status, expected);
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
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// EditMemberLevel ID判斷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DynamicData(nameof(MemberIdData), DynamicDataSourceType.Method)]
        public void EditMemberLevelInputId(int id, ActionResult expected)
        {
            _repo.Setup(x => x.EditMemberLevel(It.IsAny<EditMemberLevelDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            EditMemberLevelDto editMemberLevelDto = new EditMemberLevelDto
            {
                MemberId = id,
                Level = 1
            };
            var result = _memberController.EditMemberLevel(editMemberLevelDto);
            Assert.AreEqual(result.Status, expected);
        }

        /// <summary>
        /// EditMemberLevel Level判斷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DynamicData(nameof(MemberLevelData), DynamicDataSourceType.Method)]
        public void EditMemberLevelInputLevel(int level, ActionResult expected)
        {
            _repo.Setup(x => x.EditMemberLevel(It.IsAny<EditMemberLevelDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_memberRepo", _repo.Object);

            EditMemberLevelDto editMemberLevelDto = new EditMemberLevelDto
            {
                MemberId = 1,
                Level = level
            };
            var result = _memberController.EditMemberLevel(editMemberLevelDto);
            Assert.AreEqual(result.Status, expected);
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
                Name = "東南西",
                Birthday = "2019-01-27",
                Phone = "0934413574",
                Email = "J6LO27Dnn3@yahoo.com",
                Address = "台中市"
            };
            var result = _memberController.AddMember(addMemberDto);
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
                Name = "東南西",
                Birthday = "2019-01-27",
                Phone = "0934413574",
                Email = "J6LO27Dnn3@yahoo.com",
                Address = "台中市"
            };
            var result = _memberController.AddMember(addMemberDto);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }
    }
}
