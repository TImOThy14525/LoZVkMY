// 代码生成时间: 2025-08-29 13:55:08
 * Author: [Your Name]
 * Date: [Today's Date]
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FolderOrganizer
{
    public class FolderStructureOrganizer
    {
        private readonly string _rootDirectory;
        private readonly Dictionary<string, string> _folderRules;

        // Constructor
        public FolderStructureOrganizer(string rootDirectory)
        {
            _rootDirectory = rootDirectory;
            _folderRules = new Dictionary<string, string>();
            // Define rules for folder organization
            // e.g., _folderRules.Add("Documents", "Documents");
        }

        // Method to organize the folder structure
        public void OrganizeFolders()
        {
            if (!Directory.Exists(_rootDirectory))
            {
                throw new DirectoryNotFoundException($"The directory {_rootDirectory} does not exist.");
            }

            var files = Directory.GetFiles(_rootDirectory);
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var fileExtension = Path.GetExtension(file);

                // Determine the target folder based on file extension
                string targetFolder;
                if (_folderRules.TryGetValue(fileExtension, out targetFolder))
                {
                    var targetPath = Path.Combine(_rootDirectory, targetFolder);
                    if (!Directory.Exists(targetPath))
                    {
                        Directory.CreateDirectory(targetPath);
                    }

                    // Move the file to the target folder
                    var newFilePath = Path.Combine(targetPath, fileName);
                    File.Move(file, newFilePath);
                }
                else
                {
                    // If no rule is found, log the file or handle it as needed
                    Console.WriteLine($"No rule for file: {fileName}. Skipping...");
                }
            }
        }

        // Method to add a new rule for folder organization
        public void AddRule(string fileExtension, string folderName)
        {
            _folderRules[fileExtension] = folderName;
        }

        // Method to remove an existing rule
        public void RemoveRule(string fileExtension)
        {
            if (_folderRules.ContainsKey(fileExtension))
            {
                _folderRules.Remove(fileExtension);
            }
        }

        // Main method for demonstration purposes
        public static void Main(string[] args)
        {
            try
            {
                var organizer = new FolderStructureOrganizer("C:\PathToYourDirectory");
                organizer.AddRule(".txt", "TextFiles");
                organizer.AddRule(".jpg", "Images");
                organizer.OrganizeFolders();
                Console.WriteLine("Folder structure organized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}