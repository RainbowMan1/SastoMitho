using Dapper;
using SastoMithoClassLibrary.Models;
using SastoMithoMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SastoMithoMVC.DataAccess
{
    public static class DataAccess
    {
        public static async Task<bool> UpdateCart(Guid userId, int itemId, int quantity)
        {
            bool success = false;
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {

                try
                {
                    var item = new { ItemId = itemId, UserId = userId, Quantity = quantity };
                    if (!await DataAccess.CheckItemAlreadyInCart(itemId, userId))
                    {
                        await connection.ExecuteAsync("dbo.Carts_InsertItem @UserId, @ItemId, @Quantity", item);
                    }
                    else
                    {
                        await connection.ExecuteAsync("dbo.Carts_UpdateQuantity @UserId, @ItemId, @Quantity", item);
                    }
                    success = true;
                    return success;
                }
                catch (InvalidOperationException)
                {


                    return success;
                }
            }
        }
        public static async Task<bool> UpdateCookieCart(Guid cartId, int id, int quantity)
        {
            bool success = false;
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {

                try
                {
                    var item = new { ItemId = id, CartId = cartId, Quantity = quantity };
                    if (!await DataAccess.CheckItemAlreadyInCookieCart(id, cartId))
                    {
                        await connection.ExecuteAsync("dbo.CookieCarts_InsertItem @CartId, @ItemId, @Quantity", item);
                    }
                    else
                    {
                        await connection.ExecuteAsync("dbo.CookieCarts_UpdateQuantity @CartId, @ItemId, @Quantity", item);
                    }
                    success = true;
                    return success;
                }
                catch (InvalidOperationException)
                {


                    return success;
                }
            }
        }

        private static async Task<bool> CheckItemAlreadyInCookieCart(int id, Guid cartId)
        {
            bool itemincart = false;
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {

                try
                {
                    var item = new { ItemId = id, CartId = cartId };
                    var output = await connection.QuerySingleOrDefaultAsync("dbo.CookieCarts_CheckItem @CartId, @ItemId", item);
                    if (output != null)
                    {
                        itemincart = true;
                    }
                    return itemincart;
                }
                catch (InvalidOperationException)
                {


                    return itemincart;
                }
            }
        }
        private static async Task<bool> CheckItemAlreadyInCart(int id, Guid UserId)
        {
            bool itemincart = false;
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {

                try
                {
                    var item = new { ItemId = id, UserId = UserId };
                    var output = await connection.QuerySingleOrDefaultAsync("dbo.Carts_CheckItem @UserId, @ItemId", item);
                    if (output != null)
                    {
                        itemincart = true;
                    }
                    return itemincart;
                }
                catch (InvalidOperationException)
                {


                    return itemincart;
                }
            }
        }

        // TODO: Change SQL procedure to get all the properties of OrderedCartItemModel
        public static async Task<List<OrderedCartItemModel>> GetCartItems(Guid UserId)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                var user = new { UserId = UserId };
                List<OrderedCartItemModel> items = new List<OrderedCartItemModel>();
                var output = await connection.QueryAsync<OrderedCartItemModel>("dbo.Carts_GetItems @UserId", user);
                foreach (OrderedCartItemModel item in output)
                {
                    items.Add(item);
                }
                return items;
            }
        }
        public static async Task<List<OrderedCartItemModel>> GetCookieCartItems(Guid CartId)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                var cart = new { CartId = CartId };
                List<OrderedCartItemModel> items = new List<OrderedCartItemModel>();
                var output = await connection.QueryAsync<OrderedCartItemModel>("dbo.CookieCarts_GetItems @CartId", cart);
                foreach (OrderedCartItemModel item in output)
                {
                    items.Add(item);
                }
                return items;
            }
        }

        public static async Task<List<CategoryModel>> GetMenu()
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                try
                {
                    List<CategoryModel> categories = new List<CategoryModel>();
                    var output = await connection.QueryAsync<CategoryModel>("dbo.Menu_GetMenu");

                    foreach (CategoryModel category in output)
                    {
                        List<ItemModel> Items = new List<ItemModel>();
                        var items = await connection.QueryAsync<ItemModel>("dbo.Menu_GetItemsbyCategory @Id", new { Id = category.Id });
                        foreach (ItemModel item in items)
                        {

                            Items.Add(item);
                        }
                        category.Items = Items;
                        categories.Add(category);

                    }
                    return categories;
                }
                catch (InvalidOperationException)
                {


                    return null;
                }
            }

        }

        public static async Task<bool> CreateCookieCart(Guid CartId, DateTime ExpirationDate)
        {
            bool success = false;
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {

                try
                {
                    var datetoday = new { TodayDate = DateTime.Now };
                    var cart = new { Id = CartId, CartTerminationDate = ExpirationDate };
                    await connection.ExecuteAsync("dbo.CookieCarts_CleanCarts @TodayDate", datetoday);
                    await connection.ExecuteAsync("dbo.CookieCarts_CreateCart @Id, @CartTerminationDate", cart);
                    success = true;
                    return success;
                }
                catch (InvalidOperationException)
                {


                    return success;
                }
            }

        }
        public static async Task<bool> CreateCart(Guid UserId, Guid CartId)
        {
            bool success = false;
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {

                try
                {
                    var cart = new { Id = CartId, UserId = UserId };
                    await connection.ExecuteAsync("dbo.Carts_CreateCart @Id, @UserId", cart);
                    success = true;
                    return success;
                }
                catch (InvalidOperationException)
                {


                    return success;
                }
            }

        }
        public static async Task<bool> CheckCart(Guid UserId)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                try
                {

                    var user = new { UserId = UserId };


                    Guid output = await connection.QuerySingleOrDefaultAsync<Guid>("dbo.Carts_CheckCart @UserId", user);
                    if (output == Guid.Empty)
                    {
                        return false;

                    }
                    else
                    {
                        return true;
                    }
                }
                catch (InvalidOperationException)
                {


                    return true;
                }
            }

        }
        public static async Task<bool> CheckCookieCart(Guid CartId)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                

                    var cart = new { CartId = CartId };


                    Guid output = await connection.QuerySingleOrDefaultAsync<Guid>("dbo.CookieCarts_CheckCart @CartId", cart);
                    if (output == Guid.Empty)
                    {
                        return false;

                    }
                    else
                    {
                        return true;
                    }
                
               
            }

        }
        /// <summary>
        /// A method to pull all orders of a given user from the database
        /// </summary>
        /// <param name="UserID"> User ID of the user for whom the pending order is to be found</param>
        /// <returns>pending orders of the given user in a List</returns>
        public static async Task<List<OrderHistoryViewModel>> GetOrderHistory(Guid UserID)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                try
                {
                    var user = new { UserId = UserID };
                    List<OrderHistoryViewModel> orderhistory = new List<OrderHistoryViewModel>();
                    // might need to change order by clause in sql procedure in the future
                    var output = await connection.QueryAsync<OrderHistoryViewModel>("dbo.Orders_GetOrderHistory @UserId", user);

                    foreach (OrderHistoryViewModel order in output)
                    {
                        List<OrderedItemModel> orderedItems = new List<OrderedItemModel>();
                        var items = await connection.QueryAsync<OrderedItemModel>("dbo.Orders_GetOrderedItems @OrderId", new { OrderId = order.OrderId });
                        foreach (OrderedItemModel item in items)
                        {

                            orderedItems.Add(item);
                        }
                        order.OrderedItems = orderedItems;
                        orderhistory.Add(order);

                    }
                    return orderhistory;
                }
                catch (InvalidOperationException)
                {


                    return null;
                }
            }
        }
        /// <summary>
        /// A method to pull pending orders of a given user from the database
        /// </summary>
        /// <param name="UserID"> User ID of the user for whom the pending order is to be found</param>
        /// <returns>pending orders of the given user in a List</returns>
        public static async Task<List<PendingOrderViewModel>> GetPendingOrders(Guid UserID)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                try
                {
                    var user = new { UserId = UserID };
                    List<PendingOrderViewModel> pendingorders = new List<PendingOrderViewModel>();
                    var output = await connection.QueryAsync<PendingOrderViewModel>("dbo.Orders_GetPendingOrders @UserId", user);

                    foreach (PendingOrderViewModel order in output)
                    {
                        List<OrderedItemModel> orderedItems = new List<OrderedItemModel>();
                        var items = await connection.QueryAsync<OrderedItemModel>("dbo.Orders_GetOrderedItems @OrderId", new { OrderId = order.OrderId });
                        foreach (OrderedItemModel item in items)
                        {

                            orderedItems.Add(item);
                        }
                        order.OrderedItems = orderedItems;
                        pendingorders.Add(order);

                    }
                    return pendingorders;
                }
                catch (InvalidOperationException)
                {


                    return null;
                }
            }
        }
        public static async Task<ProfileInfoViewModel> GetProfileInfo(Guid UserID)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                try
                {
                    var user = new { UserId = UserID };
                    var output = await connection.QuerySingleOrDefaultAsync<ProfileInfoViewModel>("dbo.Orders_GetProfileInfo @UserId", user);
                    return output;
                    
                }
                catch (InvalidOperationException)
                {


                    return null;
                }
            }
        }
    }
}
