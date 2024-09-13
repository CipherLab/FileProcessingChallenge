// See https://aka.ms/new-console-template for more information
using FileProcessingApp;
using System.Diagnostics;

var inputFilePath = "input.txt";
var outputFilePath = "output.txt";

// Start the stopwatch for the entire process
var totalStopwatch = Stopwatch.StartNew();

var lines = await File.ReadAllLinesAsync(inputFilePath);
var processedLines = new List<string>();

// Start the stopwatch for processing lines
var processingStopwatch = Stopwatch.StartNew();

foreach (var line in lines)
{
    var processedLine = await Processor.ProcessLineAsync(line);
    processedLines.Add(processedLine);
}

// Stop the stopwatch for processing lines
processingStopwatch.Stop();

await File.WriteAllLinesAsync(outputFilePath, processedLines);

// Stop the stopwatch for the entire process
totalStopwatch.Stop();

// Validate output
var outputFileHash = Utilities.ComputeFileHash(outputFilePath);
Console.WriteLine($"Output File Hash: {outputFileHash}");

// Output the run-time
Console.WriteLine($"Total run-time: {totalStopwatch.ElapsedMilliseconds} ms");
Console.WriteLine($"Processing run-time: {processingStopwatch.ElapsedMilliseconds} ms");
// Compare with the expected hash
