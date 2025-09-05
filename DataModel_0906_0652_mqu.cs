// 代码生成时间: 2025-09-06 06:52:47
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorApp.Models
{
    // 数据模型基类
    public class BaseModel
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    // 用户数据模型
    public class User : BaseModel
    {
        // 用户ID
        [Key]
        public int UserId { get; set; }

        // 用户名
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "用户名长度必须在3到50之间")]
        public string UserName { get; set; }

        // 邮箱
        [Required(ErrorMessage = "邮箱不能为空")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }

        // 密码
        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "密码长度必须在6到100之间")]
        public string Password { get; set; }
    }

    // 产品数据模型
    public class Product : BaseModel
    {
        // 产品ID
        [Key]
        public int ProductId { get; set; }

        // 产品名称
        [Required(ErrorMessage = "产品名称不能为空")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "产品名称长度必须在3到100之间")]
        public string ProductName { get; set; }

        // 价格
        [Required(ErrorMessage = "价格不能为空")]
        public decimal Price { get; set; }

        // 库存数量
        [Required(ErrorMessage = "库存数量不能为空")]
        public int Stock { get; set; }
    }
}
