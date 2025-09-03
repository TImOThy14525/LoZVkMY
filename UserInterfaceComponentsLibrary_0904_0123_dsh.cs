// 代码生成时间: 2025-09-04 01:23:24
 * UserInterfaceComponentsLibrary.cs
 * This file represents a library of user interface components for a Blazor application.
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

// ComponentBase is the base class for Blazor components
public class UserInterfaceComponentsLibrary : ComponentBase
{
    // This dictionary will hold our components
    [Parameter]
    public Dictionary<string, RenderFragment> Components { get; set; } = new();

    // Method to add a component to the library
    public async Task AddComponent(string componentName, RenderFragment component)
    {
        if (string.IsNullOrWhiteSpace(componentName))
        {
            throw new ArgumentException("Component name cannot be null or whitespace.", nameof(componentName));
        }

        if (component == null)
        {
            throw new ArgumentNullException(nameof(component), "Component cannot be null.");
        }

        // Add the component to the dictionary
        Components.Add(componentName, component);

        // Invalidate the layout so the component is rendered
        await InvokeAsync(StateHasChanged);
    }

    // Method to remove a component from the library
    public async Task RemoveComponent(string componentName)
    {
        if (string.IsNullOrWhiteSpace(componentName))
        {
            throw new ArgumentException("Component name cannot be null or whitespace.", nameof(componentName));
        }

        if (!Components.ContainsKey(componentName))
        {
            throw new KeyNotFoundException($"Component with name {componentName} not found.");
        }

        // Remove the component from the dictionary
        Components.Remove(componentName);

        // Invalidate the layout so the changes are reflected
        await InvokeAsync(StateHasChanged);
    }

    // This is the component's rendering logic
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        foreach (var (componentName, component) in Components)
        {
            // Render each component using its name as a key
            builder.OpenComponent(1, typeof(GenericComponent));
            builder.AddAttribute(2, "ComponentName", componentName);
            builder.AddAttribute(3, "ChildContent", component);
            builder.CloseComponent();
        }
    }
}

// Generic component that can render any Blazor component
public class GenericComponent : ComponentBase
{
    [Parameter]
    public string ComponentName { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        // Render the child content, which is the actual component
        builder.AddContent(0, ChildContent);
    }
}
