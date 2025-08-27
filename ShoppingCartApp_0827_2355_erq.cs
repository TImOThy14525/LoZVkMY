// 代码生成时间: 2025-08-27 23:55:13
using Microsoft.AspNetCore.Components;
# 改进用户体验
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Shopping cart component that allows users to add and remove items.
/// </summary>
public partial class ShoppingCart
# 增强安全性
{
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private List<CartItem> cartItems = new List<CartItem>();
    private int itemCount = 0;

    /// <summary>
    /// Represents a shopping cart item with product details and quantity.
    /// </summary>
# NOTE: 重要实现细节
    public class CartItem
    {
        public string ProductName { get; set; }
# FIXME: 处理边界情况
        public double Price { get; set; }
        public int Quantity { get; set; }

        public CartItem(string productName, double price, int quantity)
        {
            ProductName = productName;
            Price = price;
# 增强安全性
            Quantity = quantity;
# 扩展功能模块
        }
    }

    /// <summary>
    /// Adds an item to the shopping cart.
# 添加错误处理
    /// </summary>
# 扩展功能模块
    /// <param name="productName">The name of the product to add.</param>
# 扩展功能模块
    /// <param name="price">The price of the product.</param>
    public void AddItem(string productName, double price)
    {
        cartItems.RemoveAll(item => item.ProductName == productName); // Remove existing item to update quantity
        cartItems.Add(new CartItem(productName, price, 1));
        itemCount = cartItems.Sum(item => item.Quantity);
    }

    /// <summary>
    /// Removes an item from the shopping cart.
    /// </summary>
    /// <param name="productName">The name of the product to remove.</param>
    public void RemoveItem(string productName)
    {
        var itemToRemove = cartItems.FirstOrDefault(item => item.ProductName == productName);
        if (itemToRemove != null)
        {
            cartItems.Remove(itemToRemove);
            itemCount = cartItems.Sum(item => item.Quantity);
# 添加错误处理
        }
        else
        {
            // Handle error: item not found in cart
            Console.WriteLine("Item not found in cart.");
        }
    }

    /// <summary>
    /// Clears all items from the shopping cart.
    /// </summary>
    public void ClearCart()
    {
        cartItems.Clear();
        itemCount = 0;
    }

    /// <summary>
    /// Returns the total price of all items in the cart.
    /// </summary>
    /// <returns>Total price of the cart items.</returns>
    public double GetTotalPrice()
# NOTE: 重要实现细节
    {
# 添加错误处理
        return cartItems.Sum(item => item.Price * item.Quantity);
    }

    /// <summary>
    /// Returns the number of items in the cart.
    /// </summary>
    /// <returns>Number of items in the cart.</returns>
    public int GetItemCount()
    {
        return itemCount;
    }

    /// <summary>
    /// Renders the shopping cart component UI.
    /// </summary>
    /// <returns>A rendering of the shopping cart component.</returns>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
# 优化算法效率
        builder.OpenElement(0, "div");
        builder.AddContent(1, "Shopping Cart");
        builder.CloseElement();

        foreach (var item in cartItems)
        {
# 增强安全性
            builder.OpenElement(2, "div");
# NOTE: 重要实现细节
            builder.AddContent(3, $"{item.ProductName} - Quantity: {item.Quantity}, Price: ${item.Price}");
            builder.CloseElement();
        }
    }
}
