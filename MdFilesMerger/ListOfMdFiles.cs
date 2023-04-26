namespace MdFilesMerger
{
    internal class ListOfMdFiles : List<MdFile>
    {
        public new void Sort()
        {
            if (this == null || this.Count == 0) return;
            List<MdFile> result = new List<MdFile>() { this.First() };
            MdFile[] list = new MdFile[this.Count];
            this.CopyTo(list);
            this.Clear();
            for (int i = 1; i < list.Length; i++)
            {
                MdFile file = list[i];
                int index = result.Count;
                while (index > 0)
                {
                    MdFile previousFile = result[index - 1];
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
            this.AddRange(result);
            return;
        }
        public void DisplayListOfFiles()
        {
            if (this != null && this.Count > 0)
            {
                DisplayListOfFiles(this.First().GetMainDirectoryPath());
            }
        }
        private void DisplayListOfFiles(string? mainDirectoryPath)
        {
            if (mainDirectoryPath == null) mainDirectoryPath = "";
            foreach (MdFile file in this)
            {
                Console.WriteLine(file.FileInfo.FullName.Replace(mainDirectoryPath, ""));
            }
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
            if (compare == 0) return 0;
            else
            {
                int index1 = GetStartOfNumberSequence(name1);
                int index2 = GetStartOfNumberSequence(name2);
                if (index1 >= 0 && index2 >= 0)
                {
                    int number1 = int.Parse(name1.Substring(index1));
                    int number2 = int.Parse(name2.Substring(index2));
                    name1 = name1.Substring(0, index1);
                    name2 = name2.Substring(0, index2);
                    if (string.Compare(name1, name2) == 0)
                    {
                        if (number1 < number2)
                            return -1;
                        else if (number1 > number2)
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
