using ShoppingWeb.Filters;
using ShoppingWeb.Repository;
using ShoppingWeb.Response;
using System;
using System.Data;
using System.Web.Http;

namespace ShoppingWeb.Controller
{
    [RoutePrefix("/api/Controller/order")]
    [RolesFilter((int)Roles.Member)]
    public class OrderController : BaseController
    {
        private IOrderRepository _orderRepo;

        private IOrderRepository OrderRepo
        {
            get
            {
                if (this._orderRepo == null)
                {
                    this._orderRepo = new OrderRepository();
                }

                return this._orderRepo;
            }
        }

        /// <summary>
        /// 更改訂單資料
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderStatusNum"></param>
        /// <param name="deliveryStatusNum"></param>
        /// <param name="deliveryMethodNum"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditOrder")]
        public BaseResponse EditOrder([FromBody] EditOrderDto dto)
        {
            try
            {
                if (!(dto.OrderId >= 1 && dto.OrderId <= int.MaxValue) || !(dto.OrderStatusNum >= 1 && dto.OrderStatusNum <= 4) || !(dto.DeliveryStatusNum >= 1 && dto.DeliveryStatusNum <= 6) || !(dto.DeliveryMethodNum >= 1 && dto.DeliveryMethodNum <= 3))
                {
                    return new BaseResponse
                    {
                        Status = ActionResult.InputError
                    };
                }

                (Exception exc, int? result) = this.OrderRepo.EditOrder(dto);

                if (exc != null)
                {
                    throw exc;
                }

                return new BaseResponse
                {
                    Status = (result == 1) ? ActionResult.Success : ActionResult.Failure
                };
            }
            catch (Exception ex)
            {
                this.OrderRepo.SetNLog(ex);
                return new BaseResponse
                {
                    Status = ActionResult.Error
                };
            }
        }

        /// <summary>
        /// /更改退貨訂單資訊
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditReturnOrder")]
        public BaseResponse EditReturnOrder([FromBody] EditReturnOrderDto dto)
        {
            try
            {
                if (!(dto.OrderId >= 1 && dto.OrderId <= int.MaxValue))
                {
                    return new BaseResponse
                    {
                        Status = ActionResult.InputError
                    };
                }

                (Exception exc, int? result) = this.OrderRepo.EditReturnOrder(dto);

                if (exc != null)
                {
                    throw exc;
                }

                return new BaseResponse
                {
                    Status = (result == 1) ? ActionResult.Success : ActionResult.Failure
                };
            }
            catch (Exception ex)
            {
                this.OrderRepo.SetNLog(ex);
                return new BaseResponse
                {
                    Status = ActionResult.Error
                };
            }
        }

        /// <summary>
        /// 顯示訂單詳細資訊
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderDetailsData")]
        public GetOrderDetailsDataResponse GetOrderDetailsData([FromBody] GetOrderDetailsDataDto dto)
        {
            try
            {
                if (!(dto.OrderId >= 1 && dto.OrderId <= int.MaxValue))
                {
                    return new GetOrderDetailsDataResponse
                    {
                        Status = ActionResult.InputError
                    };
                }

                (Exception exc, DataTable dt) = this.OrderRepo.GetOrderDetailsData(dto);

                if (exc != null)
                {
                    throw exc;
                }

                if (dt != null)
                {
                    GetOrderDetailsDataResponse result = GetOrderDetailsDataResponse.GetInstance(dt);
                    result.Status = ActionResult.Success;

                    return result;
                }
                else
                {
                    return new GetOrderDetailsDataResponse
                    {
                        Status = ActionResult.Failure
                    };
                }
            }
            catch (Exception ex)
            {
                this.OrderRepo.SetNLog(ex);
                return new GetOrderDetailsDataResponse
                {
                    Status = ActionResult.Error
                };
            }
        }

        /// <summary>
        /// 一開始顯示所有訂單資訊
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAllOrderData")]
        public GetAllOrderDataResponse GetAllOrderData([FromBody] GetAllOrderDataDto dto)
        {
            try
            {
                if (!(dto.PageNumber >= 1 && dto.PageNumber <= int.MaxValue) || !(dto.PageSize >= 1 && dto.PageSize <= int.MaxValue) || !(dto.BeforePagesTotal >= 1 && dto.BeforePagesTotal <= int.MaxValue))
                {
                    return new GetAllOrderDataResponse
                    {
                        Status = ActionResult.InputError
                    };
                }

                (Exception exc, int? totalCount, DataSet ds) = this.OrderRepo.GetAllOrderData(dto);

                if (exc != null)
                {
                    throw exc;
                }

                if (totalCount > 0)
                {
                    int totalPages = (int)Math.Ceiling((double)totalCount / dto.PageSize);  
                    GetAllOrderDataResponse result = GetAllOrderDataResponse.GetInstance(ds);
                    result.TotalPages = totalPages;
                    result.Status = ActionResult.Success;

                    return result;
                }
                else
                {
                    return new GetAllOrderDataResponse
                    {
                        Status = ActionResult.Failure
                    };
                }
            }
            catch (Exception ex)
            {
                this.OrderRepo.SetNLog(ex);
                return new GetAllOrderDataResponse
                {
                    Status = ActionResult.Error
                };
            }
        }

        /// <summary>
        /// 顯示申請退貨訂單資訊
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetReturnOrderData")]
        public GetReturnOrderDataResponse GetReturnOrderData([FromBody] GetReturnOrderDataDto dto)
        {
            try
            {
                if (!(dto.PageNumber >= 1 && dto.PageNumber <= int.MaxValue) || !(dto.PageSize >= 1 && dto.PageSize <= int.MaxValue) || !(dto.BeforePagesTotal >= 1 && dto.BeforePagesTotal <= int.MaxValue))
                {
                    return new GetReturnOrderDataResponse
                    {
                        Status = ActionResult.InputError
                    };
                }

                (Exception exc, int? totalCount, DataSet ds) = this.OrderRepo.GetReturnOrderData(dto);

                if (exc != null)
                {
                    throw exc;
                }

                if (totalCount > 0)
                {
                    int totalPages = (int)Math.Ceiling((double)totalCount / dto.PageSize);
                    GetReturnOrderDataResponse result = GetReturnOrderDataResponse.GetInstance(ds);
                    result.TotalPages = totalPages;
                    result.Status = ActionResult.Success;

                    return result;
                }
                else
                {
                    return new GetReturnOrderDataResponse
                    {
                        Status = ActionResult.Failure
                    };
                }
            }
            catch (Exception ex)
            {
                this.OrderRepo.SetNLog(ex);
                return new GetReturnOrderDataResponse
                {
                    Status = ActionResult.Error
                };
            }
        }

        /// <summary>
        /// 顯示相關訂單資訊
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderData")]
        public GetOrderDataResponse GetOrderData([FromBody] GetOrderDataDto dto)
        {
            try
            {
                if (!(dto.DeliveryStatusNum >= 1 && dto.DeliveryStatusNum <= 6) || !(dto.PageNumber >= 1 && dto.PageNumber <= int.MaxValue) || !(dto.PageSize >= 1 && dto.PageSize <= int.MaxValue) || !(dto.BeforePagesTotal >= 1 && dto.BeforePagesTotal <= int.MaxValue))
                {
                    return new GetOrderDataResponse
                    {
                        Status = ActionResult.InputError
                    };
                }

                (Exception exc, int? totalCount, DataSet ds) = this.OrderRepo.GetOrderData(dto);

                if (exc != null)
                {
                    throw exc;
                }

                if (totalCount > 0)
                {
                    int totalPages = (int)Math.Ceiling((double)totalCount / dto.PageSize);
                    GetOrderDataResponse result = GetOrderDataResponse.GetInstance(ds);
                    result.TotalPages = totalPages;
                    result.Status = ActionResult.Success;

                    return result;
                }
                else
                {
                    return new GetOrderDataResponse
                    {
                        Status = ActionResult.Failure
                    };
                }
            }
            catch (Exception ex)
            {
                this.OrderRepo.SetNLog(ex);
                return new GetOrderDataResponse
                {
                    Status = ActionResult.Error
                };
            }
        }

    }
}