// 代码生成时间: 2025-09-18 14:07:10
// UserAuthentication.cs
// 这个类负责处理用户身份验证，包括登录和注册功能

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;

namespace BlazorApp
{
    public class UserAuthentication
    {
        [Inject]
        private NavigationManager Navigation { get; set; }

        [Inject]
        private HttpClient Http { get; set; }

        private string apiUrl = "https://api.example.com"; // API的基础URL

        // 登录方法
        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                // 模拟登录请求
                var response = await Http.PostAsync($