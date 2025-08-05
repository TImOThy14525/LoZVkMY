// 代码生成时间: 2025-08-06 04:55:15
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProcessManager
{
    /// <summary>
    /// The <c>ProcessManager</c> component provides functionality to
    /// manage and display running processes.
    /// </summary>
    public partial class ProcessManager
    {
        private List<Process> processes = new List<Process>();

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        /// <summary>
        /// Fetches all running processes and displays them.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            try
            {
                processes = Process.GetProcesses().ToList();
            }
            catch (Exception ex)
            {
                await JSRuntime.InvokeVoidAsync("console.error", ex.Message);
            }

            StateHasChanged();
        }

        /// <summary>
        /// Kills a process by its process ID.
        /// </summary>
        /// <param name="processId">The ID of the process to kill.</param>
        public async Task KillProcess(int processId)
        {
            try
            {
                var process = processes.FirstOrDefault(p => p.Id == processId);
                if (process != null)
                {
                    process.Kill();
                    process.WaitForExit();
                    // Refresh the list of processes after killing one
                    OnInitializedAsync();
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("alert", $"Process with ID {processId} not found.");
                }
            }
            catch (Exception ex)
            {
                await JSRuntime.InvokeVoidAsync("console.error", ex.Message);
            }
        }
    }
}