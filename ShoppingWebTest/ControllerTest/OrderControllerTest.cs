using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShoppingWeb;
using ShoppingWeb.Controller;
using ShoppingWeb.Repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace ShoppingWebTest.ControllerTest
{
    [TestClass]
    public class OrderControllerTest
    {
        private readonly OrderController _orderController;
        private readonly Mock<IOrderRepository> _repo;
        private readonly PrivateObject _privateObject;

        public OrderControllerTest()
        {
            _orderController = new OrderController();
            _repo = new Mock<IOrderRepository>();
            _privateObject = new PrivateObject(_orderController);
        }

        /// <summary>
        /// ID資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> OrderIdData()
        {
            for (int i = -10; i <= 10; i++)
            {
                int id = i;
                ActionResult res = (i >= 1 && i <= int.MaxValue) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { id, res };
            }
        }

        /// <summary>
        /// OrderStatusNum資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> OrderStatusNumData()
        {
            for (int i = -10; i <= 10; i++)
            {
                int orderStatusNum = i;
                ActionResult res = (i >= 1 && i <= 4) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { orderStatusNum, res };
            }
        }

        /// <summary>
        /// DeliveryStatusNum資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> DeliveryStatusNumData()
        {
            for (int i = -10; i <= 10; i++)
            {
                int deliveryStatusNum = i;
                ActionResult res = (i >= 1 && i <= 6) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { deliveryStatusNum, res };
            }
        }

        /// <summary>
        /// DeliveryMethodNum資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> DeliveryMethodNumData()
        {
            for (int i = -10; i <= 10; i++)
            {
                int deliveryMethodNum = i;
                ActionResult res = (i >= 1 && i <= 3) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { deliveryMethodNum, res };
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
        /// EditOrder成功
        /// </summary>
        [TestMethod]
        public void EditOrderSuccess()
        {
            _repo.Setup(x => x.EditOrder(It.IsAny<EditOrderDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            EditOrderDto editOrderDto = new EditOrderDto
            {
                OrderId = 1,
                OrderStatusNum = 1,
                DeliveryStatusNum = 1,
                DeliveryMethodNum = 1
            };
            var result = _orderController.EditOrder(editOrderDto);
            _repo.Verify(x => x.EditOrder(It.IsAny<EditOrderDto>()), Times.Once);  
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// EditOrder失敗
        /// </summary>
        [TestMethod]
        public void EditOrderFailure()
        {
            _repo.Setup(x => x.EditOrder(It.IsAny<EditOrderDto>())).Returns((null, 0));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            EditOrderDto editOrderDto = new EditOrderDto
            {
                OrderId = 1,
                OrderStatusNum = 1,
                DeliveryStatusNum = 1,
                DeliveryMethodNum = 1
            };
            var result = _orderController.EditOrder(editOrderDto);
            _repo.Verify(x => x.EditOrder(It.IsAny<EditOrderDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// EditOrder例外
        /// </summary>
        [TestMethod]
        public void EditOrderException()
        {
            _repo.Setup(x => x.EditOrder(It.IsAny<EditOrderDto>())).Returns((new Exception("EditOrder單元測試"), null));
            _repo.Setup(x => x.SetNLog(It.IsAny<Exception>()));
            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            EditOrderDto editOrderDto = new EditOrderDto
            {
                OrderId = 1,
                OrderStatusNum = 1,
                DeliveryStatusNum = 1,
                DeliveryMethodNum = 1
            };
            var result = _orderController.EditOrder(editOrderDto);
            _repo.Verify(x => x.EditOrder(It.IsAny<EditOrderDto>()), Times.Once);
            _repo.Verify(x => x.SetNLog(It.IsAny<Exception>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Error);
        }

        /// <summary>
        /// EditOrder id判斷
        /// </summary>
        [DataTestMethod]
        [DynamicData(nameof(OrderIdData), DynamicDataSourceType.Method)]
        public void EditOrderInputId(int id, ActionResult res)
        {
            _repo.Setup(x => x.EditOrder(It.IsAny<EditOrderDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            EditOrderDto editOrderDto = new EditOrderDto
            {
                OrderId = id,
                OrderStatusNum = 1,
                DeliveryStatusNum = 1,
                DeliveryMethodNum = 1
            };
            var result = _orderController.EditOrder(editOrderDto);

            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// EditOrder OrderStatusNum判斷
        /// </summary>
        [DataTestMethod]
        [DynamicData(nameof(OrderStatusNumData), DynamicDataSourceType.Method)]
        public void EditOrderInputOrderStatusNum(int orderStatusNum, ActionResult res)
        {
            _repo.Setup(x => x.EditOrder(It.IsAny<EditOrderDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            EditOrderDto editOrderDto = new EditOrderDto
            {
                OrderId = 1,
                OrderStatusNum = orderStatusNum,
                DeliveryStatusNum = 1,
                DeliveryMethodNum = 1
            };
            var result = _orderController.EditOrder(editOrderDto);

            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// EditOrder DeliveryStatusNum判斷
        /// </summary>
        [DataTestMethod]
        [DynamicData(nameof(DeliveryStatusNumData), DynamicDataSourceType.Method)]
        public void EditOrderInputDeliveryStatusNum(int deliveryStatusNum, ActionResult res)
        {
            _repo.Setup(x => x.EditOrder(It.IsAny<EditOrderDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            EditOrderDto editOrderDto = new EditOrderDto
            {
                OrderId = 1,
                OrderStatusNum = 1,
                DeliveryStatusNum = deliveryStatusNum,
                DeliveryMethodNum = 1
            };
            var result = _orderController.EditOrder(editOrderDto);

            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// EditOrder DeliveryMethodNum判斷
        /// </summary>
        [DataTestMethod]
        [DynamicData(nameof(DeliveryMethodNumData), DynamicDataSourceType.Method)]
        public void EditOrderInputDeliveryMethodNum(int deliveryMethodNum, ActionResult res)
        {
            _repo.Setup(x => x.EditOrder(It.IsAny<EditOrderDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            EditOrderDto editOrderDto = new EditOrderDto
            {
                OrderId = 1,
                OrderStatusNum = 1,
                DeliveryStatusNum = 1,
                DeliveryMethodNum = deliveryMethodNum
            };
            var result = _orderController.EditOrder(editOrderDto);

            Assert.AreEqual(result.Status, res);
        }



        /// <summary>
        /// EditReturnOrder成功
        /// </summary>
        [TestMethod]
        public void EditReturnOrderSuccess()
        {
            _repo.Setup(x => x.EditReturnOrder(It.IsAny<EditReturnOrderDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            EditReturnOrderDto editReturnOrderDto = new EditReturnOrderDto
            {
                OrderId = 1,
                BoolReturn = true
            };
            var result = _orderController.EditReturnOrder(editReturnOrderDto);
            _repo.Verify(x => x.EditReturnOrder(It.IsAny<EditReturnOrderDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// EditReturnOrder失敗
        /// </summary>
        [TestMethod]
        public void EditReturnOrderFailure()
        {
            _repo.Setup(x => x.EditReturnOrder(It.IsAny<EditReturnOrderDto>())).Returns((null, 0));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            EditReturnOrderDto editReturnOrderDto = new EditReturnOrderDto
            {
                OrderId = 1,
                BoolReturn = true
            };
            var result = _orderController.EditReturnOrder(editReturnOrderDto);
            _repo.Verify(x => x.EditReturnOrder(It.IsAny<EditReturnOrderDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// EditReturnOrder例外
        /// </summary>
        [TestMethod]
        public void EditReturnOrderException()
        {
            _repo.Setup(x => x.EditReturnOrder(It.IsAny<EditReturnOrderDto>())).Returns((new Exception("EditReturnOrder單元測試"), null));
            _repo.Setup(x => x.SetNLog(It.IsAny<Exception>()));
            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            EditReturnOrderDto editReturnOrderDto = new EditReturnOrderDto
            {
                OrderId = 1,
                BoolReturn = true
            };
            var result = _orderController.EditReturnOrder(editReturnOrderDto);
            _repo.Verify(x => x.EditReturnOrder(It.IsAny<EditReturnOrderDto>()), Times.Once);
            _repo.Verify(x => x.SetNLog(It.IsAny<Exception>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Error);
        }

        /// <summary>
        /// EditReturnOrder id判斷
        /// </summary>
        [DataTestMethod]
        [DynamicData(nameof(OrderIdData), DynamicDataSourceType.Method)]
        public void EditReturnOrderInputId(int id, ActionResult res)
        {
            _repo.Setup(x => x.EditReturnOrder(It.IsAny<EditReturnOrderDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            EditReturnOrderDto editReturnOrderDto = new EditReturnOrderDto
            {
                OrderId = id,
                BoolReturn = true
            };
            var result = _orderController.EditReturnOrder(editReturnOrderDto);

            Assert.AreEqual(result.Status, res);
        }



        /// <summary>
        /// GetOrderDetailsData成功
        /// </summary>
        [TestMethod]
        public void GetOrderDetailsDataSuccess()
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_productName", typeof(string));
            dt.Columns.Add("f_productPrice", typeof(int));
            dt.Columns.Add("f_productCategory", typeof(int));
            dt.Columns.Add("f_quantity", typeof(int));
            dt.Columns.Add("f_subtotal", typeof(int));
            DataRow row = dt.NewRow();
            row["f_productName"] = "名稱";
            row["f_productPrice"] = 100;
            row["f_productCategory"] = 100302;
            row["f_quantity"] = 1;
            row["f_subtotal"] = 1006;
            dt.Rows.Add(row);
            _repo.Setup(x => x.GetOrderDetailsData(It.IsAny<GetOrderDetailsDataDto>())).Returns((null, dt));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetOrderDetailsDataDto getOrderDetailsDataDto = new GetOrderDetailsDataDto
            {
                OrderId = 1
            };
            var result = _orderController.GetOrderDetailsData(getOrderDetailsDataDto);
            _repo.Verify(x => x.GetOrderDetailsData(It.IsAny<GetOrderDetailsDataDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// GetOrderDetailsData失敗
        /// </summary>
        [TestMethod]
        public void GetOrderDetailsDataFailure()
        {
            _repo.Setup(x => x.GetOrderDetailsData(It.IsAny<GetOrderDetailsDataDto>())).Returns((null, null));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetOrderDetailsDataDto getOrderDetailsDataDto = new GetOrderDetailsDataDto
            {
                OrderId = 1
            };
            var result = _orderController.GetOrderDetailsData(getOrderDetailsDataDto);
            _repo.Verify(x => x.GetOrderDetailsData(It.IsAny<GetOrderDetailsDataDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// GetOrderDetailsData例外
        /// </summary>
        [TestMethod]
        public void GetOrderDetailsDataException()
        {
            _repo.Setup(x => x.GetOrderDetailsData(It.IsAny<GetOrderDetailsDataDto>())).Returns((new Exception("GetOrderDetailsData單元測試"), null));
            _repo.Setup(x => x.SetNLog(It.IsAny<Exception>()));
            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetOrderDetailsDataDto getOrderDetailsDataDto = new GetOrderDetailsDataDto
            {
                OrderId = 1
            };
            var result = _orderController.GetOrderDetailsData(getOrderDetailsDataDto);
            _repo.Verify(x => x.GetOrderDetailsData(It.IsAny<GetOrderDetailsDataDto>()), Times.Once);
            _repo.Verify(x => x.SetNLog(It.IsAny<Exception>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Error);
        }

        /// <summary>
        /// GetOrderDetailsData id判斷
        /// </summary>
        [DataTestMethod]
        [DynamicData(nameof(OrderIdData), DynamicDataSourceType.Method)]
        public void GetOrderDetailsDataInputId(int id, ActionResult res)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_productName", typeof(string));
            dt.Columns.Add("f_productPrice", typeof(int));
            dt.Columns.Add("f_productCategory", typeof(int));
            dt.Columns.Add("f_quantity", typeof(int));
            dt.Columns.Add("f_subtotal", typeof(int));
            DataRow row = dt.NewRow();
            row["f_productName"] = "名稱";
            row["f_productPrice"] = 100;
            row["f_productCategory"] = 100302;
            row["f_quantity"] = 1;
            row["f_subtotal"] = 1006;
            dt.Rows.Add(row);

            _repo.Setup(x => x.GetOrderDetailsData(It.IsAny<GetOrderDetailsDataDto>())).Returns((null, dt));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetOrderDetailsDataDto getOrderDetailsDataDto = new GetOrderDetailsDataDto
            {
                OrderId = id
            };
            var result = _orderController.GetOrderDetailsData(getOrderDetailsDataDto);

            Assert.AreEqual(result.Status, res);
        }



        /// <summary>
        /// GetAllOrderData成功
        /// </summary>
        [TestMethod]
        public void GetAllOrderDataSuccess()
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_productName", typeof(string));
            dt.Columns.Add("f_productPrice", typeof(int));
            dt.Columns.Add("f_productCategory", typeof(int));
            dt.Columns.Add("f_quantity", typeof(int));
            dt.Columns.Add("f_subtotal", typeof(int));
            DataRow row = dt.NewRow();
            row["f_productName"] = "名稱";
            row["f_productPrice"] = 100;
            row["f_productCategory"] = 100302;
            row["f_quantity"] = 1;
            row["f_subtotal"] = 1006;
            dt.Rows.Add(row);

            DataTable dt2 = new DataTable("Test2");
            dt2.Columns.Add("statusAll", typeof(int));
            dt2.Columns.Add("status1", typeof(int));
            dt2.Columns.Add("status2", typeof(int));
            dt2.Columns.Add("status3", typeof(int));
            dt2.Columns.Add("status4", typeof(int));
            dt2.Columns.Add("status5", typeof(int));
            dt2.Columns.Add("status6", typeof(int));
            dt2.Columns.Add("orderStatus2", typeof(int));
            DataRow row2 = dt2.NewRow();
            row2["statusAll"] = 1;
            row2["status1"] = 1;
            row2["status2"] = 1;
            row2["status3"] = 1;
            row2["status4"] = 1;
            row2["status5"] = 1;
            row2["status6"] = 1;
            row2["orderStatus2"] = 1;
            dt2.Rows.Add(row2);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);

            _repo.Setup(x => x.GetAllOrderData(It.IsAny<GetAllOrderDataDto>())).Returns((null, 1, ds));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetAllOrderDataDto getAllOrderDataDto = new GetAllOrderDataDto
            {
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _orderController.GetAllOrderData(getAllOrderDataDto);
            _repo.Verify(x => x.GetAllOrderData(It.IsAny<GetAllOrderDataDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// GetAllOrderData沒有資料
        /// </summary>
        [TestMethod]
        public void GetAllOrderDataFailure()
        {
            _repo.Setup(x => x.GetAllOrderData(It.IsAny<GetAllOrderDataDto>())).Returns((null, 0, null));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetAllOrderDataDto getAllOrderDataDto = new GetAllOrderDataDto
            {
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _orderController.GetAllOrderData(getAllOrderDataDto);
            _repo.Verify(x => x.GetAllOrderData(It.IsAny<GetAllOrderDataDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// GetAllOrderData例外
        /// </summary>
        [TestMethod]
        public void GetAllOrderDataException()
        {
            _repo.Setup(x => x.GetAllOrderData(It.IsAny<GetAllOrderDataDto>())).Returns((new Exception("GetAllOrderData單元測試"), null, null));
            _repo.Setup(x => x.SetNLog(It.IsAny<Exception>()));
            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetAllOrderDataDto getAllOrderDataDto = new GetAllOrderDataDto
            {
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _orderController.GetAllOrderData(getAllOrderDataDto);
            _repo.Verify(x => x.GetAllOrderData(It.IsAny<GetAllOrderDataDto>()), Times.Once);
            _repo.Verify(x => x.SetNLog(It.IsAny<Exception>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Error);
        }

        /// <summary>
        /// GetAllOrderData的PageNumber參數判斷
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(PageNumberData), DynamicDataSourceType.Method)]
        public void GetAllOrderDataInputPageNumber(int pageNumber, ActionResult res)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_productName", typeof(string));
            dt.Columns.Add("f_productPrice", typeof(int));
            dt.Columns.Add("f_productCategory", typeof(int));
            dt.Columns.Add("f_quantity", typeof(int));
            dt.Columns.Add("f_subtotal", typeof(int));
            DataRow row = dt.NewRow();
            row["f_productName"] = "名稱";
            row["f_productPrice"] = 100;
            row["f_productCategory"] = 100302;
            row["f_quantity"] = 1;
            row["f_subtotal"] = 1006;
            dt.Rows.Add(row);

            DataTable dt2 = new DataTable("Test2");
            dt2.Columns.Add("statusAll", typeof(int));
            dt2.Columns.Add("status1", typeof(int));
            dt2.Columns.Add("status2", typeof(int));
            dt2.Columns.Add("status3", typeof(int));
            dt2.Columns.Add("status4", typeof(int));
            dt2.Columns.Add("status5", typeof(int));
            dt2.Columns.Add("status6", typeof(int));
            dt2.Columns.Add("orderStatus2", typeof(int));
            DataRow row2 = dt2.NewRow();
            row2["statusAll"] = 1;
            row2["status1"] = 1;
            row2["status2"] = 1;
            row2["status3"] = 1;
            row2["status4"] = 1;
            row2["status5"] = 1;
            row2["status6"] = 1;
            row2["orderStatus2"] = 1;
            dt2.Rows.Add(row2);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);

            _repo.Setup(x => x.GetAllOrderData(It.IsAny<GetAllOrderDataDto>())).Returns((null, 1, ds));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetAllOrderDataDto getAllOrderDataDto = new GetAllOrderDataDto
            {
                PageNumber = pageNumber,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _orderController.GetAllOrderData(getAllOrderDataDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// GetAllOrderData的PageSize參數判斷
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(PageSizeData), DynamicDataSourceType.Method)]
        public void GetAllOrderDataInputPageSize(int pageSize, ActionResult res)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_productName", typeof(string));
            dt.Columns.Add("f_productPrice", typeof(int));
            dt.Columns.Add("f_productCategory", typeof(int));
            dt.Columns.Add("f_quantity", typeof(int));
            dt.Columns.Add("f_subtotal", typeof(int));
            DataRow row = dt.NewRow();
            row["f_productName"] = "名稱";
            row["f_productPrice"] = 100;
            row["f_productCategory"] = 100302;
            row["f_quantity"] = 1;
            row["f_subtotal"] = 1006;
            dt.Rows.Add(row);

            DataTable dt2 = new DataTable("Test2");
            dt2.Columns.Add("statusAll", typeof(int));
            dt2.Columns.Add("status1", typeof(int));
            dt2.Columns.Add("status2", typeof(int));
            dt2.Columns.Add("status3", typeof(int));
            dt2.Columns.Add("status4", typeof(int));
            dt2.Columns.Add("status5", typeof(int));
            dt2.Columns.Add("status6", typeof(int));
            dt2.Columns.Add("orderStatus2", typeof(int));
            DataRow row2 = dt2.NewRow();
            row2["statusAll"] = 1;
            row2["status1"] = 1;
            row2["status2"] = 1;
            row2["status3"] = 1;
            row2["status4"] = 1;
            row2["status5"] = 1;
            row2["status6"] = 1;
            row2["orderStatus2"] = 1;
            dt2.Rows.Add(row2);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);

            _repo.Setup(x => x.GetAllOrderData(It.IsAny<GetAllOrderDataDto>())).Returns((null, 1, ds));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetAllOrderDataDto getAllOrderDataDto = new GetAllOrderDataDto
            {
                PageNumber = 1,
                PageSize = pageSize,
                BeforePagesTotal = 1
            };
            var result = _orderController.GetAllOrderData(getAllOrderDataDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// GetAllOrderData的BeforePagesTotal參數判斷
        /// </summary>
        /// <param name="beforePagesTotal"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(BeforePagesTotalData), DynamicDataSourceType.Method)]
        public void GetAllOrderDataInputBeforePagesTotal(int beforePagesTotal, ActionResult res)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_productName", typeof(string));
            dt.Columns.Add("f_productPrice", typeof(int));
            dt.Columns.Add("f_productCategory", typeof(int));
            dt.Columns.Add("f_quantity", typeof(int));
            dt.Columns.Add("f_subtotal", typeof(int));
            DataRow row = dt.NewRow();
            row["f_productName"] = "名稱";
            row["f_productPrice"] = 100;
            row["f_productCategory"] = 100302;
            row["f_quantity"] = 1;
            row["f_subtotal"] = 1006;
            dt.Rows.Add(row);

            DataTable dt2 = new DataTable("Test2");
            dt2.Columns.Add("statusAll", typeof(int));
            dt2.Columns.Add("status1", typeof(int));
            dt2.Columns.Add("status2", typeof(int));
            dt2.Columns.Add("status3", typeof(int));
            dt2.Columns.Add("status4", typeof(int));
            dt2.Columns.Add("status5", typeof(int));
            dt2.Columns.Add("status6", typeof(int));
            dt2.Columns.Add("orderStatus2", typeof(int));
            DataRow row2 = dt2.NewRow();
            row2["statusAll"] = 1;
            row2["status1"] = 1;
            row2["status2"] = 1;
            row2["status3"] = 1;
            row2["status4"] = 1;
            row2["status5"] = 1;
            row2["status6"] = 1;
            row2["orderStatus2"] = 1;
            dt2.Rows.Add(row2);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);

            _repo.Setup(x => x.GetAllOrderData(It.IsAny<GetAllOrderDataDto>())).Returns((null, 1, ds));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetAllOrderDataDto getAllOrderDataDto = new GetAllOrderDataDto
            {
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = beforePagesTotal
            };
            var result = _orderController.GetAllOrderData(getAllOrderDataDto);
            Assert.AreEqual(result.Status, res);
        }



        /// <summary>
        /// GetReturnOrderData成功
        /// </summary>
        [TestMethod]
        public void GetReturnOrderDataSuccess()
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_productName", typeof(string));
            dt.Columns.Add("f_productPrice", typeof(int));
            dt.Columns.Add("f_productCategory", typeof(int));
            dt.Columns.Add("f_quantity", typeof(int));
            dt.Columns.Add("f_subtotal", typeof(int));
            DataRow row = dt.NewRow();
            row["f_productName"] = "名稱";
            row["f_productPrice"] = 100;
            row["f_productCategory"] = 100302;
            row["f_quantity"] = 1;
            row["f_subtotal"] = 1006;
            dt.Rows.Add(row);

            DataTable dt2 = new DataTable("Test2");
            dt2.Columns.Add("statusAll", typeof(int));
            dt2.Columns.Add("status1", typeof(int));
            dt2.Columns.Add("status2", typeof(int));
            dt2.Columns.Add("status3", typeof(int));
            dt2.Columns.Add("status4", typeof(int));
            dt2.Columns.Add("status5", typeof(int));
            dt2.Columns.Add("status6", typeof(int));
            dt2.Columns.Add("orderStatus2", typeof(int));
            DataRow row2 = dt2.NewRow();
            row2["statusAll"] = 1;
            row2["status1"] = 1;
            row2["status2"] = 1;
            row2["status3"] = 1;
            row2["status4"] = 1;
            row2["status5"] = 1;
            row2["status6"] = 1;
            row2["orderStatus2"] = 1;
            dt2.Rows.Add(row2);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);

            _repo.Setup(x => x.GetReturnOrderData(It.IsAny<GetReturnOrderDataDto>())).Returns((null, 1, ds));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetReturnOrderDataDto getReturnOrderDataDto = new GetReturnOrderDataDto
            {
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _orderController.GetReturnOrderData(getReturnOrderDataDto);
            _repo.Verify(x => x.GetReturnOrderData(It.IsAny<GetReturnOrderDataDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// GetReturnOrderData沒有資料
        /// </summary>
        [TestMethod]
        public void GetReturnOrderDataFailure()
        {
            _repo.Setup(x => x.GetReturnOrderData(It.IsAny<GetReturnOrderDataDto>())).Returns((null, 0, null));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetReturnOrderDataDto getReturnOrderDataDto = new GetReturnOrderDataDto
            {
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _orderController.GetReturnOrderData(getReturnOrderDataDto);
            _repo.Verify(x => x.GetReturnOrderData(It.IsAny<GetReturnOrderDataDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// GetReturnOrderData例外
        /// </summary>
        [TestMethod]
        public void GetReturnOrderDataException()
        {
            _repo.Setup(x => x.GetReturnOrderData(It.IsAny<GetReturnOrderDataDto>())).Returns((new Exception("GetReturnOrderData單元測試"), null, null));
            _repo.Setup(x => x.SetNLog(It.IsAny<Exception>()));
            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetReturnOrderDataDto getReturnOrderDataDto = new GetReturnOrderDataDto
            {
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _orderController.GetReturnOrderData(getReturnOrderDataDto);
            _repo.Verify(x => x.GetReturnOrderData(It.IsAny<GetReturnOrderDataDto>()), Times.Once);
            _repo.Verify(x => x.SetNLog(It.IsAny<Exception>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Error);
        }

        /// <summary>
        /// GetReturnOrderData的PageNumber參數判斷
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(PageNumberData), DynamicDataSourceType.Method)]
        public void GetReturnOrderDataInputPageNumber(int pageNumber, ActionResult res)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_productName", typeof(string));
            dt.Columns.Add("f_productPrice", typeof(int));
            dt.Columns.Add("f_productCategory", typeof(int));
            dt.Columns.Add("f_quantity", typeof(int));
            dt.Columns.Add("f_subtotal", typeof(int));
            DataRow row = dt.NewRow();
            row["f_productName"] = "名稱";
            row["f_productPrice"] = 100;
            row["f_productCategory"] = 100302;
            row["f_quantity"] = 1;
            row["f_subtotal"] = 1006;
            dt.Rows.Add(row);

            DataTable dt2 = new DataTable("Test2");
            dt2.Columns.Add("statusAll", typeof(int));
            dt2.Columns.Add("status1", typeof(int));
            dt2.Columns.Add("status2", typeof(int));
            dt2.Columns.Add("status3", typeof(int));
            dt2.Columns.Add("status4", typeof(int));
            dt2.Columns.Add("status5", typeof(int));
            dt2.Columns.Add("status6", typeof(int));
            dt2.Columns.Add("orderStatus2", typeof(int));
            DataRow row2 = dt2.NewRow();
            row2["statusAll"] = 1;
            row2["status1"] = 1;
            row2["status2"] = 1;
            row2["status3"] = 1;
            row2["status4"] = 1;
            row2["status5"] = 1;
            row2["status6"] = 1;
            row2["orderStatus2"] = 1;
            dt2.Rows.Add(row2);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);

            _repo.Setup(x => x.GetReturnOrderData(It.IsAny<GetReturnOrderDataDto>())).Returns((null, 1, ds));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetReturnOrderDataDto getReturnOrderDataDto = new GetReturnOrderDataDto
            {
                PageNumber = pageNumber,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _orderController.GetReturnOrderData(getReturnOrderDataDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// GetReturnOrderData的PageSize參數判斷
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(PageSizeData), DynamicDataSourceType.Method)]
        public void GetReturnOrderDataInputPageSize(int pageSize, ActionResult res)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_productName", typeof(string));
            dt.Columns.Add("f_productPrice", typeof(int));
            dt.Columns.Add("f_productCategory", typeof(int));
            dt.Columns.Add("f_quantity", typeof(int));
            dt.Columns.Add("f_subtotal", typeof(int));
            DataRow row = dt.NewRow();
            row["f_productName"] = "名稱";
            row["f_productPrice"] = 100;
            row["f_productCategory"] = 100302;
            row["f_quantity"] = 1;
            row["f_subtotal"] = 1006;
            dt.Rows.Add(row);

            DataTable dt2 = new DataTable("Test2");
            dt2.Columns.Add("statusAll", typeof(int));
            dt2.Columns.Add("status1", typeof(int));
            dt2.Columns.Add("status2", typeof(int));
            dt2.Columns.Add("status3", typeof(int));
            dt2.Columns.Add("status4", typeof(int));
            dt2.Columns.Add("status5", typeof(int));
            dt2.Columns.Add("status6", typeof(int));
            dt2.Columns.Add("orderStatus2", typeof(int));
            DataRow row2 = dt2.NewRow();
            row2["statusAll"] = 1;
            row2["status1"] = 1;
            row2["status2"] = 1;
            row2["status3"] = 1;
            row2["status4"] = 1;
            row2["status5"] = 1;
            row2["status6"] = 1;
            row2["orderStatus2"] = 1;
            dt2.Rows.Add(row2);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);

            _repo.Setup(x => x.GetReturnOrderData(It.IsAny<GetReturnOrderDataDto>())).Returns((null, 1, ds));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetReturnOrderDataDto getReturnOrderDataDto = new GetReturnOrderDataDto
            {
                PageNumber = 1,
                PageSize = pageSize,
                BeforePagesTotal = 1
            };
            var result = _orderController.GetReturnOrderData(getReturnOrderDataDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// GetReturnOrderData的BeforePagesTotal參數判斷
        /// </summary>
        /// <param name="beforePagesTotal"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(BeforePagesTotalData), DynamicDataSourceType.Method)]
        public void GetReturnOrderDataInputBeforePagesTotal(int beforePagesTotal, ActionResult res)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_productName", typeof(string));
            dt.Columns.Add("f_productPrice", typeof(int));
            dt.Columns.Add("f_productCategory", typeof(int));
            dt.Columns.Add("f_quantity", typeof(int));
            dt.Columns.Add("f_subtotal", typeof(int));
            DataRow row = dt.NewRow();
            row["f_productName"] = "名稱";
            row["f_productPrice"] = 100;
            row["f_productCategory"] = 100302;
            row["f_quantity"] = 1;
            row["f_subtotal"] = 1006;
            dt.Rows.Add(row);

            DataTable dt2 = new DataTable("Test2");
            dt2.Columns.Add("statusAll", typeof(int));
            dt2.Columns.Add("status1", typeof(int));
            dt2.Columns.Add("status2", typeof(int));
            dt2.Columns.Add("status3", typeof(int));
            dt2.Columns.Add("status4", typeof(int));
            dt2.Columns.Add("status5", typeof(int));
            dt2.Columns.Add("status6", typeof(int));
            dt2.Columns.Add("orderStatus2", typeof(int));
            DataRow row2 = dt2.NewRow();
            row2["statusAll"] = 1;
            row2["status1"] = 1;
            row2["status2"] = 1;
            row2["status3"] = 1;
            row2["status4"] = 1;
            row2["status5"] = 1;
            row2["status6"] = 1;
            row2["orderStatus2"] = 1;
            dt2.Rows.Add(row2);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);

            _repo.Setup(x => x.GetReturnOrderData(It.IsAny<GetReturnOrderDataDto>())).Returns((null, 1, ds));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetReturnOrderDataDto getReturnOrderDataDto = new GetReturnOrderDataDto
            {
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = beforePagesTotal
            };
            var result = _orderController.GetReturnOrderData(getReturnOrderDataDto);
            Assert.AreEqual(result.Status, res);
        }



        /// <summary>
        /// GetOrderData成功
        /// </summary>
        [TestMethod]
        public void GetOrderDataSuccess()
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_productName", typeof(string));
            dt.Columns.Add("f_productPrice", typeof(int));
            dt.Columns.Add("f_productCategory", typeof(int));
            dt.Columns.Add("f_quantity", typeof(int));
            dt.Columns.Add("f_subtotal", typeof(int));
            DataRow row = dt.NewRow();
            row["f_productName"] = "名稱";
            row["f_productPrice"] = 100;
            row["f_productCategory"] = 100302;
            row["f_quantity"] = 1;
            row["f_subtotal"] = 1006;
            dt.Rows.Add(row);

            DataTable dt2 = new DataTable("Test2");
            dt2.Columns.Add("statusAll", typeof(int));
            dt2.Columns.Add("status1", typeof(int));
            dt2.Columns.Add("status2", typeof(int));
            dt2.Columns.Add("status3", typeof(int));
            dt2.Columns.Add("status4", typeof(int));
            dt2.Columns.Add("status5", typeof(int));
            dt2.Columns.Add("status6", typeof(int));
            dt2.Columns.Add("orderStatus2", typeof(int));
            DataRow row2 = dt2.NewRow();
            row2["statusAll"] = 1;
            row2["status1"] = 1;
            row2["status2"] = 1;
            row2["status3"] = 1;
            row2["status4"] = 1;
            row2["status5"] = 1;
            row2["status6"] = 1;
            row2["orderStatus2"] = 1;
            dt2.Rows.Add(row2);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);

            _repo.Setup(x => x.GetOrderData(It.IsAny<GetOrderDataDto>())).Returns((null, 1, ds));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetOrderDataDto getOrderDataDto = new GetOrderDataDto
            {
                DeliveryStatusNum = 1,
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _orderController.GetOrderData(getOrderDataDto);
            _repo.Verify(x => x.GetOrderData(It.IsAny<GetOrderDataDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// GetOrderData沒有資料
        /// </summary>
        [TestMethod]
        public void GetOrderDataFailure()
        {
            _repo.Setup(x => x.GetOrderData(It.IsAny<GetOrderDataDto>())).Returns((null, 0, null));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetOrderDataDto getOrderDataDto = new GetOrderDataDto
            {
                DeliveryStatusNum = 1,
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _orderController.GetOrderData(getOrderDataDto);
            _repo.Verify(x => x.GetOrderData(It.IsAny<GetOrderDataDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// GetOrderData例外
        /// </summary>
        [TestMethod]
        public void GetOrderDataException()
        {
            _repo.Setup(x => x.GetOrderData(It.IsAny<GetOrderDataDto>())).Returns((new Exception("GetOrderData單元測試"), null, null));
            _repo.Setup(x => x.SetNLog(It.IsAny<Exception>()));
            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetOrderDataDto getOrderDataDto = new GetOrderDataDto
            {
                DeliveryStatusNum = 1,
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _orderController.GetOrderData(getOrderDataDto);
            _repo.Verify(x => x.GetOrderData(It.IsAny<GetOrderDataDto>()), Times.Once);
            _repo.Verify(x => x.SetNLog(It.IsAny<Exception>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Error);
        }

        /// <summary>
        /// GetOrderData的PageNumber參數判斷
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(PageNumberData), DynamicDataSourceType.Method)]
        public void GetOrderDataInputPageNumber(int pageNumber, ActionResult res)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_productName", typeof(string));
            dt.Columns.Add("f_productPrice", typeof(int));
            dt.Columns.Add("f_productCategory", typeof(int));
            dt.Columns.Add("f_quantity", typeof(int));
            dt.Columns.Add("f_subtotal", typeof(int));
            DataRow row = dt.NewRow();
            row["f_productName"] = "名稱";
            row["f_productPrice"] = 100;
            row["f_productCategory"] = 100302;
            row["f_quantity"] = 1;
            row["f_subtotal"] = 1006;
            dt.Rows.Add(row);

            DataTable dt2 = new DataTable("Test2");
            dt2.Columns.Add("statusAll", typeof(int));
            dt2.Columns.Add("status1", typeof(int));
            dt2.Columns.Add("status2", typeof(int));
            dt2.Columns.Add("status3", typeof(int));
            dt2.Columns.Add("status4", typeof(int));
            dt2.Columns.Add("status5", typeof(int));
            dt2.Columns.Add("status6", typeof(int));
            dt2.Columns.Add("orderStatus2", typeof(int));
            DataRow row2 = dt2.NewRow();
            row2["statusAll"] = 1;
            row2["status1"] = 1;
            row2["status2"] = 1;
            row2["status3"] = 1;
            row2["status4"] = 1;
            row2["status5"] = 1;
            row2["status6"] = 1;
            row2["orderStatus2"] = 1;
            dt2.Rows.Add(row2);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);

            _repo.Setup(x => x.GetOrderData(It.IsAny<GetOrderDataDto>())).Returns((null, 1, ds));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetOrderDataDto getOrderDataDto = new GetOrderDataDto
            {
                DeliveryStatusNum = 1,
                PageNumber = pageNumber,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _orderController.GetOrderData(getOrderDataDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// GetOrderData的PageSize參數判斷
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(PageSizeData), DynamicDataSourceType.Method)]
        public void GetOrderDataInputPageSize(int pageSize, ActionResult res)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_productName", typeof(string));
            dt.Columns.Add("f_productPrice", typeof(int));
            dt.Columns.Add("f_productCategory", typeof(int));
            dt.Columns.Add("f_quantity", typeof(int));
            dt.Columns.Add("f_subtotal", typeof(int));
            DataRow row = dt.NewRow();
            row["f_productName"] = "名稱";
            row["f_productPrice"] = 100;
            row["f_productCategory"] = 100302;
            row["f_quantity"] = 1;
            row["f_subtotal"] = 1006;
            dt.Rows.Add(row);

            DataTable dt2 = new DataTable("Test2");
            dt2.Columns.Add("statusAll", typeof(int));
            dt2.Columns.Add("status1", typeof(int));
            dt2.Columns.Add("status2", typeof(int));
            dt2.Columns.Add("status3", typeof(int));
            dt2.Columns.Add("status4", typeof(int));
            dt2.Columns.Add("status5", typeof(int));
            dt2.Columns.Add("status6", typeof(int));
            dt2.Columns.Add("orderStatus2", typeof(int));
            DataRow row2 = dt2.NewRow();
            row2["statusAll"] = 1;
            row2["status1"] = 1;
            row2["status2"] = 1;
            row2["status3"] = 1;
            row2["status4"] = 1;
            row2["status5"] = 1;
            row2["status6"] = 1;
            row2["orderStatus2"] = 1;
            dt2.Rows.Add(row2);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);

            _repo.Setup(x => x.GetOrderData(It.IsAny<GetOrderDataDto>())).Returns((null, 1, ds));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetOrderDataDto getOrderDataDto = new GetOrderDataDto
            {
                DeliveryStatusNum = 1,
                PageNumber = 1,
                PageSize = pageSize,
                BeforePagesTotal = 1
            };
            var result = _orderController.GetOrderData(getOrderDataDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// GetOrderData的BeforePagesTotal參數判斷
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(BeforePagesTotalData), DynamicDataSourceType.Method)]
        public void GetOrderDataInputBeforePagesTotal(int beforePagesTotal, ActionResult res)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_productName", typeof(string));
            dt.Columns.Add("f_productPrice", typeof(int));
            dt.Columns.Add("f_productCategory", typeof(int));
            dt.Columns.Add("f_quantity", typeof(int));
            dt.Columns.Add("f_subtotal", typeof(int));
            DataRow row = dt.NewRow();
            row["f_productName"] = "名稱";
            row["f_productPrice"] = 100;
            row["f_productCategory"] = 100302;
            row["f_quantity"] = 1;
            row["f_subtotal"] = 1006;
            dt.Rows.Add(row);

            DataTable dt2 = new DataTable("Test2");
            dt2.Columns.Add("statusAll", typeof(int));
            dt2.Columns.Add("status1", typeof(int));
            dt2.Columns.Add("status2", typeof(int));
            dt2.Columns.Add("status3", typeof(int));
            dt2.Columns.Add("status4", typeof(int));
            dt2.Columns.Add("status5", typeof(int));
            dt2.Columns.Add("status6", typeof(int));
            dt2.Columns.Add("orderStatus2", typeof(int));
            DataRow row2 = dt2.NewRow();
            row2["statusAll"] = 1;
            row2["status1"] = 1;
            row2["status2"] = 1;
            row2["status3"] = 1;
            row2["status4"] = 1;
            row2["status5"] = 1;
            row2["status6"] = 1;
            row2["orderStatus2"] = 1;
            dt2.Rows.Add(row2);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);

            _repo.Setup(x => x.GetOrderData(It.IsAny<GetOrderDataDto>())).Returns((null, 1, ds));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetOrderDataDto getOrderDataDto = new GetOrderDataDto
            {
                DeliveryStatusNum = 1,
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = beforePagesTotal
            };
            var result = _orderController.GetOrderData(getOrderDataDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// GetOrderData的DeliveryStatusNum參數判斷
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(DeliveryStatusNumData), DynamicDataSourceType.Method)]
        public void GetOrderDataInputDeliveryStatusNum(int deliveryStatusNum, ActionResult res)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_productName", typeof(string));
            dt.Columns.Add("f_productPrice", typeof(int));
            dt.Columns.Add("f_productCategory", typeof(int));
            dt.Columns.Add("f_quantity", typeof(int));
            dt.Columns.Add("f_subtotal", typeof(int));
            DataRow row = dt.NewRow();
            row["f_productName"] = "名稱";
            row["f_productPrice"] = 100;
            row["f_productCategory"] = 100302;
            row["f_quantity"] = 1;
            row["f_subtotal"] = 1006;
            dt.Rows.Add(row);

            DataTable dt2 = new DataTable("Test2");
            dt2.Columns.Add("statusAll", typeof(int));
            dt2.Columns.Add("status1", typeof(int));
            dt2.Columns.Add("status2", typeof(int));
            dt2.Columns.Add("status3", typeof(int));
            dt2.Columns.Add("status4", typeof(int));
            dt2.Columns.Add("status5", typeof(int));
            dt2.Columns.Add("status6", typeof(int));
            dt2.Columns.Add("orderStatus2", typeof(int));
            DataRow row2 = dt2.NewRow();
            row2["statusAll"] = 1;
            row2["status1"] = 1;
            row2["status2"] = 1;
            row2["status3"] = 1;
            row2["status4"] = 1;
            row2["status5"] = 1;
            row2["status6"] = 1;
            row2["orderStatus2"] = 1;
            dt2.Rows.Add(row2);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);

            _repo.Setup(x => x.GetOrderData(It.IsAny<GetOrderDataDto>())).Returns((null, 1, ds));

            _privateObject.SetFieldOrProperty("_orderRepo", _repo.Object);

            GetOrderDataDto getOrderDataDto = new GetOrderDataDto
            {
                DeliveryStatusNum = deliveryStatusNum,
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _orderController.GetOrderData(getOrderDataDto);
            Assert.AreEqual(result.Status, res);
        }
    }
}
