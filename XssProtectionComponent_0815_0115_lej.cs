// 代码生成时间: 2025-08-15 01:15:45
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Html;
using System.Text.RegularExpressions;

namespace BlazorXssProtection
{
    /// <summary>
    /// A Blazor component that protects against XSS attacks.
    /// It sanitizes input HTML to prevent cross-site scripting.
    /// </summary>
    public class XssProtectionComponent : ComponentBase
    {
        private const string ScriptPattern = "@""/<script[^>]*>([\s\S]*?)</script>@";";
        private const string OnEventPattern = "@""/on[a-z]+\s*=\s*\S+@";";
        private const string CommentPattern = "@""/<!--.*?-->@";";
        private const string StylePattern = "@""/<style[^>]*>([\s\S]*?)</style>@";";

        /// <summary>
        /// The input HTML string that needs to be sanitized.
        /// </summary>
        [Parameter]
        public string InputHtml { get; set; }

        /// <summary>
        /// The sanitized HTML output.
        /// </summary>
        public string SanitizedHtml { get; private set; } = string.Empty;

        /// <summary>
        /// Method invoked when the component is initialized.
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            SanitizeHtml();
        }

        /// <summary>
        /// Method invoked when the input HTML is changed.
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            SanitizeHtml();
        }

        /// <summary>
        /// Sanitizes the input HTML by removing script tags and other potentially dangerous attributes.
        /// </summary>
        private void SanitizeHtml()
        {
            if (string.IsNullOrWhiteSpace(InputHtml))
            {
                SanitizedHtml = InputHtml;
                return;
            }

            string sanitized = InputHtml;

            // Remove script tags
            sanitized = Regex.Replace(sanitized, ScriptPattern, "", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            // Remove on-event attributes (e.g., onclick)
            sanitized = Regex.Replace(sanitized, OnEventPattern, "", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            // Remove HTML comments
            sanitized = Regex.Replace(sanitized, CommentPattern, "", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            // Remove style tags
            sanitized = Regex.Replace(sanitized, StylePattern, "", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            // Set the sanitized HTML
            SanitizedHtml = sanitized;
        }

        /// <summary>
        /// Renders the sanitized HTML.
        /// </summary>
        /// <param name="builder">The rendering builder.</param>
        protected void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "innerHTML", new HtmlString(SanitizedHtml));
            builder.CloseElement();
        }
    }
}