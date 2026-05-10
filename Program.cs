using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

// 1. Directory Setup
var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");
var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");

// Create the output directory if it doesn't exist
Directory.CreateDirectory(salesTotalDir);

// 2. Data Aggregation
var salesFiles = FindFiles(storesDirectory);
var salesData = new List<(string FileName, decimal Total)>();
decimal grandTotal = 0;

// 3. JSON Parsing & Calculation
foreach (var file in salesFiles)
{
    var jsonText = File.ReadAllText(file);

    // Deserialize the JSON. (Assumes your JSON looks like: { "Total": 200.50 })
    var data = JsonSerializer.Deserialize<SalesData>(jsonText);

    if (data != null)
    {
        string fileName = Path.GetFileName(file);
        salesData.Add((fileName, data.Total));
        grandTotal += data.Total;
    }
}

// 4. Generate the string using your custom requirement
string reportContent = GenerateSalesSummary(salesData, grandTotal);

// 5. Write to Disk
var reportPath = Path.Combine(salesTotalDir, "salesSummary.txt");
File.WriteAllText(reportPath, reportContent);

Console.WriteLine($"Sales summary successfully generated at: {reportPath}");


// ==========================================
// LOCAL FUNCTIONS & CLASSES
// ==========================================

// Finds all files ending in 'sales.json' recursively
IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();
    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        if (file.EndsWith("sales.json"))
        {
            salesFiles.Add(file);
        }
    }
    return salesFiles;
}

// Builds the final report formatting
string GenerateSalesSummary(IEnumerable<(string FileName, decimal Total)> fileDetails, decimal aggregateTotal)
{
    StringBuilder sb = new StringBuilder();

    sb.AppendLine("Sales Summary");
    sb.AppendLine("----------------------------");

    // :C formats the decimal into standard currency (e.g., $1,000.00)
    sb.AppendLine($" Total Sales: {aggregateTotal:C}");
    sb.AppendLine();
    sb.AppendLine(" Details:");

    foreach (var file in fileDetails)
    {
        sb.AppendLine($"  {file.FileName}: {file.Total:C}");
    }

    return sb.ToString();
}

// DTO to map the JSON structure
class SalesData
{
    public decimal Total { get; set; }
}