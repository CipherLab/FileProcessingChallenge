using FileProcessingApp;
using System.Diagnostics;

var inputFilePath = "input.txt";
var outputFilePath = "output.txt";

// Start the stopwatch for the entire process
var totalStopwatch = Stopwatch.StartNew();


var lines = await File.ReadAllLinesAsync(inputFilePath);

ILineProcessor processor = new CPUBoundLineProcessor();
// Process lines using the current method (LineProcessor)
var processedLines = await processor.ProcessLinesAsync(lines);

// Stop the stopwatch for the entire process
totalStopwatch.Stop();

await File.WriteAllLinesAsync(outputFilePath, processedLines);

// Validate output
var outputFileHash = Utilities.ComputeFileHash(outputFilePath);
Console.WriteLine($"Output File Hash: {outputFileHash}");

// Output the run-time
Console.WriteLine($"Total run-time: {totalStopwatch.ElapsedMilliseconds} ms");

// Method to process lines using the current method
