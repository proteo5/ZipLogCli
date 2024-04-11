using Cocona;
using System.IO.Compression;
namespace ZipLogCli.cli;


internal class Program
{
    static void Main(string[] args)
    {
        var builder = CoconaApp.CreateBuilder();
        var app = builder.Build();

        app.AddCommand((
            [Argument(Description = "Name or pattern of the files to Zip")]
            string files,
            [Option('d', Description = "Delete files after being ziped")]
            bool delete,
            [Option('y', Description = "Skip zip confirmation")]
            bool skipZipConfirmation,
            [Option('z', Description = "Skip delete files confirmation")]
            bool skipDeleteConfirmation,
            [Option('s', Description = "Skip all confirmation")]
            bool skipAllConfirmations,
            [Option('a', Description = "Add files to an existing zip file, otherwise delete existing zip file")]
            bool addToExistingZipFile,
            [Option('o', Description = "Output file name")]
            string? outputFileName
            ) =>
        {
            #region Validations
            //check if the string files is empty
            if (string.IsNullOrEmpty(files))
            {
                Console.WriteLine("No files to zip");
                return;
            }
            //check if the string files has less than 200 characters
            if (files.Length > 200)
            {
                Console.WriteLine($"The maximum number of characters for a file is 200 ({files.Length})");
                return;
            }

            //check if the string files is a valid file or pattern
            Console.WriteLine($"Looking for files with patern: {files}");
            Console.WriteLine();
            if (!files.Contains("*") && !files.Contains("?"))
            {
                if (!File.Exists(files))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"No files found with the patern: {files}");
                    Console.ResetColor();
                    return;
                }
            }

            //verify if the output file name is valid and if it is a zip file
            if (!string.IsNullOrEmpty(outputFileName))
            {
                if (!outputFileName.EndsWith(".zip"))
                {
                    Console.WriteLine("The output file name must have a .zip extension");
                    return;
                }
            }

            //if output file name was not provided, create a zip file with the pattern name
            var zipFileName = "";
            if (string.IsNullOrEmpty(outputFileName))
            {
                //create a zip file with the pattern name
                zipFileName = files.Replace("*", "").Replace("?", "").Replace(".", "").Replace("\\", "").Replace("/", "") + ".zip";
            }
            else
            {
                zipFileName = outputFileName;
            }

            //if exist the zip file and no addToExistingZipFile flag provided then delete it to create a new one
            bool createNewZipFile = !addToExistingZipFile;
            if (!addToExistingZipFile)
            {
                var zipFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), zipFileName);
                foreach (var file in zipFiles)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Deleting file: {file}");
                    Console.ResetColor();
                    File.Delete(file);
                }
            }
            #endregion



            //list all files that match the pattern
            var filesToZip = Directory.GetFiles(Directory.GetCurrentDirectory(), files);
            if (filesToZip.Length == 0)
            {
                Console.WriteLine($"No files found with the pattern {files}");
                return;
            }
            if (!skipZipConfirmation)
            {
                foreach (var file in filesToZip)
                {
                    Console.WriteLine($"Found file: {file}");
                }
            }

            //ask for confirmation
            if (!skipZipConfirmation && !skipAllConfirmations)
            {
                Console.WriteLine();
                Console.WriteLine($"Found {filesToZip.Length} files,do you want to zip these files? (Y/N)");
                var key = Console.ReadKey();
                Console.WriteLine();
                if (key.Key != ConsoleKey.Y)
                {
                    Console.WriteLine("Operation canceled by user");
                    return;
                }
            }

            //confirm if the user wants to delete the files after zipping
            if ((delete && !skipDeleteConfirmation) && !skipAllConfirmations)
            {
                Console.WriteLine();
                Console.WriteLine($"Do you want to delete the files after zipping? (Y/N)");
                var key = Console.ReadKey();
                Console.WriteLine();
                if (key.Key != ConsoleKey.Y)
                {
                    Console.WriteLine("Operation canceled by user");
                    return;
                }
            }


            var message = File.Exists(zipFileName) ? "Creating" : "Updating";
            Console.WriteLine();
            Console.WriteLine($"{message} zip file: {zipFileName}");
            Console.WriteLine();

            //zip files
            //create the zip file
            var zipArchiveMode = createNewZipFile ? ZipArchiveMode.Create : ZipArchiveMode.Update;
            using (var zip = ZipFile.Open(zipFileName, zipArchiveMode))
            {
                var count = filesToZip.Length;
                var i = 0;
                //add files to the zip file
                foreach (var file in filesToZip)
                {
                    // Calculate percentage
                    i++;
                    double percentage = (double)i / count * 100;

                    zip.CreateEntryFromFile(file, Path.GetFileName(file));
                    Console.Write($"\rProgress: {percentage:F2}%");
                }
                Console.WriteLine();

                //if the delete flag is set, delete the files
                if (delete)
                {
                    i = 0;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    foreach (var file in filesToZip)
                    {
                        // Calculate percentage
                        i++;
                        double percentage = (double)i / count * 100;

                        Console.Write($"\rDeleting files progress: {percentage:F2}%");

                        File.Delete(file);
                    }
                    Console.ResetColor();
                    Console.WriteLine();
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{filesToZip.Length} file(s) are being zipped into {zipFileName}.");

                if (delete)
                {
                    Console.WriteLine($"The file(s) are also being deleted.");
                }
                Console.ResetColor();
            }
        });

        app.Run();
    }
}
