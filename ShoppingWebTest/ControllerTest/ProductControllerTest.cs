using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShoppingWeb;
using ShoppingWeb.Controller;
using ShoppingWeb.Repository;
using System;
using System.Collections.Generic;

namespace ShoppingWebTest.ControllerTest
{
    [TestClass]
    public class ProductControllerTest
    {
        private readonly ProductController _productController;
        private readonly Mock<IProductRepository> _repo;
        private readonly PrivateObject _privateObject;

        public ProductControllerTest()
        {
            _productController = new ProductController();
            _repo = new Mock<IProductRepository>();
            _privateObject = new PrivateObject(_productController);
        }

        /// <summary>
        /// ID資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> IdData()
        {
            for (int i = -10; i <= 10; i++)
            {
                int id = i;
                ActionResult res = (i >= 1 && i <= int.MaxValue) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { id, res };
            }
        }



        /// <summary>
        /// DelProduct成功
        /// </summary>
        [TestMethod]
        public void DelProductSuccess()
        {
            _repo.Setup(x => x.DelProduct(It.IsAny<DelProductDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_productRepo", _repo.Object);

            DelProductDto delProductDto = new DelProductDto
            {
                ProductId = 1
            };
            var result = _productController.DelProduct(delProductDto);
            _repo.Verify(x => x.DelProduct(It.IsAny<DelProductDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// DelProduct失敗
        /// </summary>
        [TestMethod]
        public void DelProductFailure()
        {
            _repo.Setup(x => x.DelProduct(It.IsAny<DelProductDto>())).Returns((null, 0));

            _privateObject.SetFieldOrProperty("_productRepo", _repo.Object);

            DelProductDto delProductDto = new DelProductDto
            {
                ProductId = 1
            };
            var result = _productController.DelProduct(delProductDto);
            _repo.Verify(x => x.DelProduct(It.IsAny<DelProductDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// DelProduct例外
        /// </summary>
        [TestMethod]
        public void DelProductException()
        {
            _repo.Setup(x => x.DelProduct(It.IsAny<DelProductDto>())).Returns((new Exception("DelProduct單元測試"), null));
            _repo.Setup(x => x.SetNLog(It.IsAny<Exception>()));
            _privateObject.SetFieldOrProperty("_productRepo", _repo.Object);

            DelProductDto delProductDto = new DelProductDto
            {
                ProductId = 1
            };
            var result = _productController.DelProduct(delProductDto);
            _repo.Verify(x => x.DelProduct(It.IsAny<DelProductDto>()), Times.Once);
            _repo.Verify(x => x.SetNLog(It.IsAny<Exception>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Error);
        }

        /// <summary>
        /// DelProduct id判斷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DynamicData(nameof(IdData), DynamicDataSourceType.Method)]
        public void DelProductInputId(int id, ActionResult res)
        {
            _repo.Setup(x => x.DelProduct(It.IsAny<DelProductDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_productRepo", _repo.Object);

            DelProductDto delProductDto = new DelProductDto
            {
                ProductId = id
            };
            var result = _productController.DelProduct(delProductDto);
            Assert.AreEqual(result.Status, res);
        }



        /// <summary>
        /// EditProductStatus成功
        /// </summary>
        [TestMethod]
        public void EditProductStatusSuccess()
        {
            _repo.Setup(x => x.EditProductStatus(It.IsAny<EditProductStatusDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_productRepo", _repo.Object);

            EditProductStatusDto editProductStatusDto = new EditProductStatusDto
            {
                ProductId = 1
            };
            var result = _productController.EditProductStatus(editProductStatusDto);
            _repo.Verify(x => x.EditProductStatus(It.IsAny<EditProductStatusDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// EditProductStatus失敗
        /// </summary>
        [TestMethod]
        public void EditProductStatusFailure()
        {
            _repo.Setup(x => x.EditProductStatus(It.IsAny<EditProductStatusDto>())).Returns((null, 0));

            _privateObject.SetFieldOrProperty("_productRepo", _repo.Object);

            EditProductStatusDto editProductStatusDto = new EditProductStatusDto
            {
                ProductId = 1
            };
            var result = _productController.EditProductStatus(editProductStatusDto);
            _repo.Verify(x => x.EditProductStatus(It.IsAny<EditProductStatusDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// EditProductStatus例外
        /// </summary>
        [TestMethod]
        public void EditProductStatusException()
        {
            _repo.Setup(x => x.EditProductStatus(It.IsAny<EditProductStatusDto>())).Returns((new Exception("DelProduct單元測試"), null));
            _repo.Setup(x => x.SetNLog(It.IsAny<Exception>()));
            _privateObject.SetFieldOrProperty("_productRepo", _repo.Object);

            EditProductStatusDto editProductStatusDto = new EditProductStatusDto
            {
                ProductId = 1
            };
            var result = _productController.EditProductStatus(editProductStatusDto);
            _repo.Verify(x => x.EditProductStatus(It.IsAny<EditProductStatusDto>()), Times.Once);
            _repo.Verify(x => x.SetNLog(It.IsAny<Exception>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Error);
        }

        /// <summary>
        /// EditProductStatus id判斷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DynamicData(nameof(IdData), DynamicDataSourceType.Method)]
        public void EditProductStatusInputId(int id, ActionResult res)
        {
            _repo.Setup(x => x.EditProductStatus(It.IsAny<EditProductStatusDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_productRepo", _repo.Object);

            EditProductStatusDto editProductStatusDto = new EditProductStatusDto
            {
                ProductId = id
            };
            var result = _productController.EditProductStatus(editProductStatusDto);
            Assert.AreEqual(result.Status, res);
        }



        /// <summary>
        /// EditProduct成功
        /// </summary>
        [TestMethod]
        public void EditProductSuccess()
        {
            _repo.Setup(x => x.EditProduct(It.IsAny<EditProductDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_productRepo", _repo.Object);

            EditProductDto editProductDto = new EditProductDto
            {
                ProductId = 1,
                ProductPrice = 10,
                ProductStock = 10,
                ProductIntroduce = "中文詳情",
                ProductIntroduceEN = "英文詳情",
                ProductCheckStock = true,
                ProductStockWarning = 100
            };
            var result = _productController.EditProduct(editProductDto);
            _repo.Verify(x => x.EditProduct(It.IsAny<EditProductDto>()), Times.Once);  
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// EditProduct失敗
        /// </summary>
        [TestMethod]
        public void EditProductFailure()
        {
            _repo.Setup(x => x.EditProduct(It.IsAny<EditProductDto>())).Returns((null, 0));

            _privateObject.SetFieldOrProperty("_productRepo", _repo.Object);

            EditProductDto editProductDto = new EditProductDto
            {
                ProductId = 1,
                ProductPrice = 10,
                ProductStock = 10,
                ProductIntroduce = "中文詳情",
                ProductIntroduceEN = "英文詳情",
                ProductCheckStock = true,
                ProductStockWarning = 100
            };
            var result = _productController.EditProduct(editProductDto);
            _repo.Verify(x => x.EditProduct(It.IsAny<EditProductDto>()), Times.Once); 
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// EditProduct例外
        /// </summary>
        [TestMethod]
        public void EditProductException()
        {
            _repo.Setup(x => x.EditProduct(It.IsAny<EditProductDto>())).Returns((new Exception("EditProduct單元測試"), null));
            _repo.Setup(x => x.SetNLog(It.IsAny<Exception>()));
            _privateObject.SetFieldOrProperty("_productRepo", _repo.Object);

            EditProductDto editProductDto = new EditProductDto
            {
                ProductId = 1,
                ProductPrice = 10,
                ProductStock = 10,
                ProductIntroduce = "中文詳情",
                ProductIntroduceEN = "英文詳情",
                ProductCheckStock = true,
                ProductStockWarning = 100
            };
            var result = _productController.EditProduct(editProductDto);
            _repo.Verify(x => x.EditProduct(It.IsAny<EditProductDto>()), Times.Once);
            _repo.Verify(x => x.SetNLog(It.IsAny<Exception>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Error);
        }

    }
}
