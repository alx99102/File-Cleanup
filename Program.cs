namespace FileCleanup
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path to the directory to clean up
            string path = @"C:\Users\alx99\Downloads";
            Console.WriteLine("Commencing file cleanup for \"" + path+"\"");

            // Get all directories in the path
            string[] directories = Directory.GetDirectories(path);

            // Create a .folders directory if it doesn't exist
            if(!Directory.Exists(path + @"\.folders\")) {
                Console.WriteLine("Creating .folders directory");
                Directory.CreateDirectory(path + @"\.folders\");
            }

            // Loop through each directory
            foreach (string directory in directories)
            {
                // Get the name of the directory
                string dirName = directory.Substring(directory.LastIndexOf('\\') + 1);
                
                // If the directory isn't a file type folder, move it to the .folders directory
                if(!dirName.StartsWith(".")) {
                    Console.WriteLine("Moving directory \"" + directory + "\" to .folders directory");
                    try {
                    Directory.Move(directory, path + @"\.folders\" + dirName);

                    }
                    catch {
                        Console.WriteLine("Error moving directory \"" + directory + "\" to .folders directory");
                    }

                }
            }
            
            // Get all loose files in the directory
            string[] files = Directory.GetFiles(path);
            int fileCount = files.Length;
            Console.WriteLine("Found " + fileCount + " files");

            // Loop through each file
            foreach (string file in files)
            {
                // Get the file name and file type
                string fileName = file.Substring(file.LastIndexOf('\\') + 1);
                string fileType = fileName.Substring(fileName.LastIndexOf('.'));
                string fileTypeFolder = path + @"\" + fileType;

                // Create a folder for the file type if it doesn't exist
                if(!Directory.Exists(fileTypeFolder)) {
                    Console.WriteLine("Creating folder for file type \"" + fileType + "\"");
                    Directory.CreateDirectory(fileTypeFolder);
                }
                // Move the file to the file type folder
                Console.WriteLine("Moving file \"" + file + "\" to \"" + fileTypeFolder + "\"");
                try {
                    File.Move(file,fileTypeFolder + @"\" + fileName);
                }
                // If the file already exists, ask the user if they want to overwrite it
                catch (IOException) {
                    Console.WriteLine("Error moving file \"" + file + "\" to \"" + fileTypeFolder + "\", file already exists");
                    Console.WriteLine("Do you want to overwrite the file? (y/n)");
                    string input = Console.ReadLine();
                    if(input == "y") {
                        File.Move(file, fileTypeFolder + @"\" + fileName, true);
                    }
                }
                // If there is an unknown error, print it to the console
                catch (Exception e) {
                    Console.WriteLine("Error moving file \"" + file + "\" to \"" + fileTypeFolder + "\"");
                    Console.WriteLine(e);
                }
            }
        }
    }
}
