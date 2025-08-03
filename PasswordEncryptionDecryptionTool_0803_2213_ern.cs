// 代码生成时间: 2025-08-03 22:13:41
using Microsoft.AspNetCore.Components;
using System.Security.Cryptography;
using System.Text;

// 密码加密解密工具组件
public class PasswordEncryptionDecryptionTool : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    // 用于存储加密后的密码
    private string encryptedPassword = string.Empty;
    // 用于存储解密后的密码
    private string decryptedPassword = string.Empty;
    // 用户输入的原始密码
    private string rawPassword = string.Empty;

    // 加密密码方法
    private async Task EncryptPassword()
    {
        try
        {
            using (var aes = Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();

                // 将原始密码转换为字节数组
                var key = Encoding.UTF8.GetBytes(rawPassword);
                var iv = aes.IV;
                var encryptor = aes.CreateEncryptor(aes.Key, iv);
                var encrypted = new byte[aes.BlockSize + rawPassword.Length];
                // 加密原始密码
                int length = encryptor.TransformBlock(Encoding.UTF8.GetBytes(rawPassword), 0, rawPassword.Length, encrypted, 0);
                length += encryptor.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
                Array.Resize(ref encrypted, length);

                // 将加密后的数据转换为Base64字符串
                encryptedPassword = Convert.ToBase64String(encrypted);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while encrypting password: {ex.Message}");
        }
    }

    // 解密密码方法
    private async Task DecryptPassword()
    {
        try
        {
            var encryptedBytes = Convert.FromBase64String(encryptedPassword);
            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(rawPassword);
                aes.IV = aes.IV;
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                var decrypted = new byte[aes.BlockSize + encryptedBytes.Length];
                // 解密密码
                int length = decryptor.TransformBlock(encryptedBytes, 0, encryptedBytes.Length, decrypted, 0);
                length += decryptor.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
                Array.Resize(ref decrypted, length);

                // 将解密后的数据转换为字符串
                decryptedPassword = Encoding.UTF8.GetString(decrypted);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while decrypting password: {ex.Message}");
        }
    }

    // 渲染组件的UI
    public override void Render(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", "password-tool-container");

        builder.OpenElement(2, "input");
        builder.AddAttribute(3, "type", "text");
        builder.AddAttribute(4, "value", rawPassword);
        builder.AddAttribute(5, "oninput", EventCallback.Factory.CreateBinder<string>(this, __value => rawPassword, () => rawPassword));
        builder.CloseElement();

        builder.OpenElement(6, "button");
        builder.AddAttribute(7, "onclick", EventCallback.Factory.Create(this, EncryptPassword));
        builder.AddAttribute(8, "style", "margin-right: 10px;");
        builder.AddContent(9, "Encrypt");
        builder.CloseElement();

        builder.OpenElement(10, "button");
        builder.AddAttribute(11, "onclick", EventCallback.Factory.Create(this, DecryptPassword));
        builder.AddContent(12, "Decrypt");
        builder.CloseElement();

        builder.OpenElement(13, "div");
        builder.AddAttribute(14, "class", "password-result");
        builder.AddContent(15, encryptedPassword);
        builder.CloseElement();

        builder.OpenElement(16, "div");
        builder.AddAttribute(17, "class", "password-result");
        builder.AddContent(18, decryptedPassword);
        builder.CloseElement();

        builder.CloseElement();
    }
}
