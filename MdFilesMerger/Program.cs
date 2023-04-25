namespace MdFilesMerger
{
    internal class Program
    {
        public const string MAIN_DIRECTORY_PATH = @"C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET";
        static void Main(string[] args)
        {
            string mainDirectoryPath = SetMainDirectoryPath();
            var listOfFiles = Sort(GetListOfMdFiles(mainDirectoryPath));
            DisplayListOfFiles(listOfFiles, mainDirectoryPath);
        }
        private static List<FileCustomInfo> GetListOfMdFiles(string mainDirectoryPath)
        {
            var listOfFiles = new List<FileCustomInfo>();
            var mainDirectory = Directory.CreateDirectory(mainDirectoryPath);
            foreach(FileInfo file in mainDirectory.EnumerateFiles("*.md", SearchOption.AllDirectories))
            {
                listOfFiles.Add(new FileCustomInfo(file, mainDirectoryPath));
            }
            return listOfFiles;
        }
        private static string SetMainDirectoryPath()
        {
            string? mainDirectoryPath = MAIN_DIRECTORY_PATH;
            Console.WriteLine("-----SET DIRECTORY WHERE YOU WANT TO SEARCH FOR .MD FILES-----");
            string? change;
            if (Directory.Exists(MAIN_DIRECTORY_PATH))
            {
                Console.WriteLine("Default directory path is: {0}", MAIN_DIRECTORY_PATH);
                Console.Write("Do you want to change it? (y/n) ");
                change = Console.ReadLine();
                if (change != null) change = change.ToLower();
                Console.WriteLine();
                while (!(change == "y" || change == "n"))
                {
                    Console.WriteLine("I didn't understand your answer.\nDo you want to change default directory path?");
                    Console.Write("Write \"y\" if yes or \"n\" if no, to answer the question: ");
                    change = Console.ReadLine();
                    if (change != null) change = change.ToLower();
                    Console.WriteLine();
                }
            }
            else change = "y";
            if (change == "y")
            {
                Console.Write("Enter desired directory path: ");
                mainDirectoryPath = Console.ReadLine();
                while (mainDirectoryPath == null || mainDirectoryPath.Trim().Length <= 0 || !Directory.Exists(mainDirectoryPath))
                {
                    Console.WriteLine("Cannot find directory of given path. Make sure that it exist and enter the path again.");

                    Console.Write("Enter desired directory path: ");
                    mainDirectoryPath = Console.ReadLine();
                }
            }
            return mainDirectoryPath;
        }
        private static void DisplayListOfFiles(List<FileCustomInfo> list, string mainDirectoryPath)
        {
            foreach (FileCustomInfo file in list)
            {
                Console.WriteLine(file.FileInfo.FullName.Replace(mainDirectoryPath, ""));
            }
        }
        private static List<FileCustomInfo> Sort(List<FileCustomInfo>? list)
        {
            if (list == null || list.Count == 0) return new List<FileCustomInfo>();
            List<FileCustomInfo> result = new List<FileCustomInfo>() { list.First() };
            for(int i = 1; i < list.Count; i++)
            {
                FileCustomInfo file = list[i];
                int index = result.Count;
                while(index > 0)
                {
                    FileCustomInfo previousFile = result[index - 1];
                    if (file.SubDirectories.Length == 0)
                    {
                        //If no subdirectories order by file name
                        if (previousFile.SubDirectories.Length > 0)
                            index--;
                        else
                        {
                            if (CompareFileNames(previousFile.FileInfo.Name, file.FileInfo.Name) > 0) index--;
                            else break;
                        }
                    }
                    else
                    {
                        //if subdirectories order by subdirectories names and file names
                        int previousDivNumber = previousFile.SubDirectories.Length;
                        int currentDivNumber = file.SubDirectories.Length;
                        int smallerDirectoriesNumber = previousDivNumber;
                        if (smallerDirectoriesNumber == 0) break;
                        if (smallerDirectoriesNumber > currentDivNumber)
                            smallerDirectoriesNumber = currentDivNumber;
                        //order by subdirectories names
                        bool? changeIndex = null;
                        for (int j = 0; j < smallerDirectoriesNumber; j++)
                        {
                            int compare = CompareFileNames(previousFile.SubDirectories[j], file.SubDirectories[j]);
                            //if the same directory check lower directory
                            if (compare == 0) continue;
                            //if previous file has subdirectory next in order change order
                            else if (compare > 0)
                            {
                                changeIndex = true;
                                break;
                            }
                            //if previous file has subdirectory previous in order, preserve order
                            else
                            {
                                changeIndex = false;
                                break;
                            }
                        }
                        //if file has the same subdirectories as previous file
                        if (changeIndex == null)
                        {
                            //if previous file has more subdirectories then file it lays lower in directory tree. Change order
                            if (previousDivNumber > currentDivNumber)
                                index--;
                            //if file and previous file lay directly in the same subdirectory, order by file name
                            else if (previousDivNumber == currentDivNumber)
                            {
                                if (CompareFileNames(previousFile.FileInfo.Name, file.FileInfo.Name) > 0) index--;
                                else break;
                            }
                            //if file has more subdirectories then previous file, order is correct
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            if ((bool)changeIndex) index--;
                            else break;
                        }
                    }
                }
                //place file in the right place
                result.Insert(index, file);
            }
            return result;
        }
        /***
         * Compare file names. If names have numbers ends with number check if names are the same except numeric part. 
         * If yes extract numeric part and order names by numeric order.
         * Returns:
         * 0 - if names are the same
         * -1 - if name1 is before name2
         * 1 - if name2 is before name1
         ***/
        private static int CompareFileNames(string name1, string name2)
        {
            int compare = string.Compare(name1, name2);
            if(compare == 0) return 0;
            else
            {
                int index1 = GetStartOfNumberSequence(name1);
                int index2 = GetStartOfNumberSequence(name2);
                if(index1 >= 0 && index2 >= 0)
                {
                    int number1 = int.Parse(name1.Substring(index1));
                    int number2 = int.Parse(name2.Substring(index2));
                    name1 = name1.Substring(0, index1);
                    name2 = name2.Substring(0, index2);
                    if(string.Compare(name1, name2) == 0)
                    {
                        if(number1 < number2)
                            return -1;
                        else if(number1 > number2)
                            return 1;
                        else
                            return 0;
                    }
                }
                return compare;
            }
        }
        private static int GetStartOfNumberSequence(string name)
        {
            int index = name.Length - 1;
            return GetStartOfNumberSequence(name, index);
        }
        private static int GetStartOfNumberSequence(string name, int index)
        {
            if (char.IsNumber(name, index))
                if (index > 0 && char.IsNumber(name, index - 1))
                    return GetStartOfNumberSequence(name, index - 1);
                else
                    return index;
            else return -1;
        }
    }
}