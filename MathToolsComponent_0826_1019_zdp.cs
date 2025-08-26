// 代码生成时间: 2025-08-26 10:19:27
 * error handling, documentation, and maintainability.
 */

using Microsoft.AspNetCore.Components;
using System;

namespace MathTools.Blazor.Components
{
    public partial class MathToolsComponent
    {
        [Parameter]
        public int Operand1 { get; set; }

        [Parameter]
        public int Operand2 { get; set; }

        // Result of the last calculation
        private string Result { get; set; } = "";

        // Method to add two operands
        private void Add()
        {
            try
            {
                int sum = Operand1 + Operand2;
                Result = $"Result: {sum}";
            }
            catch (Exception ex)
            {
                Result = $"Error: {ex.Message}";
            }
        }

        // Method to subtract the second operand from the first
        private void Subtract()
        {
            try
            {
                int difference = Operand1 - Operand2;
                Result = $"Result: {difference}";
            }
            catch (Exception ex)
            {
                Result = $"Error: {ex.Message}";
            }
        }

        // Method to multiply two operands
        private void Multiply()
        {
            try
            {
                int product = Operand1 * Operand2;
                Result = $"Result: {product}";
            }
            catch (Exception ex)
            {
                Result = $"Error: {ex.Message}";
            }
        }

        // Method to divide the first operand by the second
        private void Divide()
        {
            try
            {
                if (Operand2 == 0)
                {
                    throw new DivideByZeroException();
                }

                int quotient = Operand1 / Operand2;
                Result = $"Result: {quotient}";
            }
            catch (DivideByZeroException)
            {
                Result = "Error: Division by zero is not allowed.";
            }
            catch (Exception ex)
            {
                Result = $"Error: {ex.Message}";
            }
        }

        // Render the component UI with buttons and result display
        private RenderFragment RenderUI()
        {
            return builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddContent(1, "Enter two numbers to perform mathematical operations");
                builder.CloseElement();

                builder.OpenElement(2, "div");
                builder.OpenElement(3, "input");
                builder.AddAttribute(4, "type", "number");
                builder.AddAttribute(5, "value", Operand1);
                builder.AddAttribute(6, "onchange", EventCallback.Factory.CreateBinder<int>(this, __value => Operand1, Operand1));
                builder.CloseElement();

                builder.OpenElement(7, "input");
                builder.AddAttribute(8, "type", "number");
                builder.AddAttribute(9, "value", Operand2);
                builder.AddAttribute(10, "onchange", EventCallback.Factory.CreateBinder<int>(this, __value => Operand2, Operand2));
                builder.CloseElement();

                builder.OpenElement(11, "button");
                builder.AddAttribute(12, "onclick", EventCallback.Factory.Create(this, Add));
                builder.AddContent(13, "Add");
                builder.CloseElement();

                builder.OpenElement(14, "button");
                builder.AddAttribute(15, "onclick", EventCallback.Factory.Create(this, Subtract));
                builder.AddContent(16, "Subtract");
                builder.CloseElement();

                builder.OpenElement(17, "button");
                builder.AddAttribute(18, "onclick", EventCallback.Factory.Create(this, Multiply));
                builder.AddContent(19, "Multiply");
                builder.CloseElement();

                builder.OpenElement(20, "button");
                builder.AddAttribute(21, "onclick", EventCallback.Factory.Create(this, Divide));
                builder.AddContent(22, "Divide");
                builder.CloseElement();

                builder.OpenElement(23, "div");
                builder.AddContent(24, $"Result: {Result}");
                builder.CloseElement();
            };
        }
    }
}
