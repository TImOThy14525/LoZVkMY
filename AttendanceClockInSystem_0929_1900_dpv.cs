// 代码生成时间: 2025-09-29 19:00:57
// AttendanceClockInSystem.cs
// This file includes the main functionality for a simple attendance clock-in system.

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceClockInSystem
{
    public class AttendanceClockInSystem
    {
        // The clock-in records are stored in a list. In a production scenario, this should be replaced with a database.
        private List<ClockInRecord> clockInRecords = new List<ClockInRecord>();

        // Represents a clock-in record with a timestamp and employee ID.
        public class ClockInRecord
        {
            public DateTime Timestamp { get; set; }
            public string EmployeeId { get; set; }
        }

        // Events for notifying the UI of changes.
        public event Action OnClockIn;
        public event Action OnRemoveClockIn;

        // Method to simulate clocking in an employee.
        // The method takes an employee ID and the current time as parameters.
        public async Task ClockIn(string employeeId)
        {
            try
            {
                // Check if the employee has already clocked in today.
                if (clockInRecords.Any(record => record.EmployeeId == employeeId && record.Timestamp.Date == DateTime.Now.Date))
                {
                    throw new InvalidOperationException("Employee has already clocked in today.");
                }

                // Add the new clock-in record.
                clockInRecords.Add(new ClockInRecord { Timestamp = DateTime.Now, EmployeeId = employeeId });

                // Notify the UI that a clock-in has occurred.
                OnClockIn?.Invoke();

                // Await an empty task to simulate asynchronous behavior.
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Provide feedback to the user if there is an error.
                Console.WriteLine($"Error clocking in: {ex.Message}");
            }
        }

        // Method to remove a clock-in record.
        public void RemoveClockIn(string employeeId)
        {
            var recordToRemove = clockInRecords.FirstOrDefault(record => record.EmployeeId == employeeId && record.Timestamp.Date == DateTime.Now.Date);
            if (recordToRemove != null)
            {
                clockInRecords.Remove(recordToRemove);
                OnRemoveClockIn?.Invoke();
            }
            else
            {
                Console.WriteLine("No clock-in record found for today.");
            }
        }

        // Get all clock-in records.
        public IEnumerable<ClockInRecord> GetAllClockInRecords()
        {
            return clockInRecords;
        }

        // Get clock-in records for a specific employee.
        public IEnumerable<ClockInRecord> GetClockInRecordsForEmployee(string employeeId)
        {
            return clockInRecords.Where(record => record.EmployeeId == employeeId);
        }
    }
}
