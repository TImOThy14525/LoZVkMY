// 代码生成时间: 2025-08-23 10:17:59
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace TestReportGenerator
{
    public class TestReportGeneratorService
    {
        private readonly IJSRuntime jsRuntime;

        public TestReportGeneratorService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        // Generate a simple test report as a PDF
        public async Task<string> GenerateTestReportAsync(List<TestResult> results)
        {
            try
            {
                if (results == null || results.Count == 0)
                {
                    throw new ArgumentException("Test results cannot be null or empty.");
                }

                // Construct the report content
                string reportContent = BuildReportContent(results);

                // Use JavaScript interop to generate the PDF
                string pdfData = await jsRuntime.InvokeAsync<string>("jsFunctions.createPdf", reportContent);

                return pdfData;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error generating report: {ex.Message}");
                throw;
            }
        }

        // Build the content of the report
        private string BuildReportContent(List<TestResult> results)
        {
            var contentBuilder = new StringBuilder();
            foreach (var result in results)
            {
                contentBuilder.AppendLine($"Test Case: {result.TestCaseName}");
                contentBuilder.AppendLine($"Status: {result.Status}