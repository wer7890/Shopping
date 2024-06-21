CREATE PROCEDURE [dbo].[pro_sw_editOrderData]
	@orderId INT,
	@orderStatus INT,
	@deliveryStatus INT,
	@deliveryMethod INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @newStock INT
	BEGIN TRANSACTION; -- 開始交易

    BEGIN TRY
		
		--訂單狀態為已付款，配送狀態為發貨中
        IF (@orderStatus = 1 AND @deliveryStatus = 2)
        BEGIN
            -- 檢查庫存是否足夠
            IF EXISTS (
                SELECT 1
                FROM t_product p WITH(NOLOCK)
                INNER JOIN t_orderDetails od ON p.f_id = od.f_productId
                WHERE od.f_orderId = @orderId
                GROUP BY p.f_id
                HAVING SUM(p.f_stock) - SUM(od.f_quantity) < 0
            )
            BEGIN
                -- 库存不足，配送狀態為發貨中，回傳0
                UPDATE t_order WITH(ROWLOCK)
                SET f_orderStatus = @orderStatus, f_deliveryStatus = 1, f_deliveryMethod = @deliveryMethod
                WHERE f_Id = @orderId;

                COMMIT TRANSACTION; -- 提交交易
                SELECT 0; -- 操作成功，回傳0
                RETURN;
            END;

            -- 更新庫存
            UPDATE p WITH(ROWLOCK)
            SET p.f_stock = p.f_stock - od.f_quantity
            FROM t_product p
            INNER JOIN t_orderDetails od ON p.f_id = od.f_productId
            WHERE od.f_orderId = @orderId;

            -- 更新訂單資料
            UPDATE t_order WITH(ROWLOCK)
            SET f_orderStatus = @orderStatus, f_deliveryStatus = 2, f_deliveryMethod = @deliveryMethod
            WHERE f_Id = @orderId;

			-- 如果庫存小於等於0，就關閉商品
			SELECT @newStock = p.f_stock
			FROM t_product p WITH(NOLOCK)
            INNER JOIN t_orderDetails od ON p.f_id = od.f_productId
            WHERE od.f_orderId = @orderId;

			IF (@newStock <= 0)
			BEGIN
				UPDATE p WITH(ROWLOCK)
				SET p.f_isOpen = 0
				FROM t_product p
				INNER JOIN t_orderDetails od ON p.f_id = od.f_productId
				WHERE od.f_orderId = @orderId;
			END

        END

		--訂單狀態為已退款，配送狀態為已退貨
		ELSE IF (@orderStatus = 4 AND @deliveryStatus = 6)
		BEGIN
			DECLARE @totalAmount INT;
			DECLARE @memberId INT;
			DECLARE @walletAmount INT;

			-- 鎖定 t_order 和 t_wallet 表
			SELECT @totalAmount = f_total, @memberId = f_memberId
			FROM t_order WITH(ROWLOCK)
			WHERE f_id = @orderId;

			SELECT @walletAmount = f_amount
			FROM t_wallet WITH(ROWLOCK)
			WHERE f_memberId = @memberId;

			-- 更新會員總花費
			UPDATE t_totalSpent WITH(ROWLOCK)
			SET f_totalSpent = f_totalSpent - @totalAmount
			WHERE f_memberId = @memberId;

			-- 退款給會員錢包中的金額
			UPDATE t_wallet WITH(ROWLOCK)
			SET f_amount = f_amount + @totalAmount
			WHERE f_memberId = @memberId;

			-- 記錄會員錢包異動
			INSERT INTO t_walletChanges (f_memberId, f_previousAmount, f_finalAmount, f_changeAmount)
			VALUES (@memberId, @walletAmount, @walletAmount + @totalAmount, @totalAmount);

			-- 更新訂單資料
			UPDATE t_order WITH(ROWLOCK)
			SET f_orderStatus = 4, f_deliveryStatus = 6
			WHERE f_Id = @orderId;
		END

        ELSE
        BEGIN
            -- 更新訂單資料
            UPDATE t_order WITH(ROWLOCK)
            SET f_orderStatus = @orderStatus, f_deliveryStatus = @deliveryStatus, f_deliveryMethod = @deliveryMethod
            WHERE f_Id = @orderId;
        END

        COMMIT TRANSACTION; -- 提交交易
        SELECT 1; -- 操作成功，回傳1

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION; -- 回滾交易
        -- 在這裡處理錯誤
        SELECT 0; -- 錯誤，回傳0
    END CATCH

END