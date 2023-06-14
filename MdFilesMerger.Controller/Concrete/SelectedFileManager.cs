using MdFilesMerger.App.Abstract;
using MdFilesMerger.App.Concrete;
using MdFilesMerger.Controller.Abstract;
using MdFilesMerger.Controller.Common;
using MdFilesMerger.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdFilesMerger.Controller.Concrete
{
    public class SelectedFileManager : RelativeFileMenager<SelectedFile, SelectedFileService>
    {
        public SelectedFileManager(MainDirectoryService mainDirectoryService) : base(new SelectedFileService(mainDirectoryService))
        {
        }

        public override void DisplayTitle()
        {
            DisplayTitle("Lista plików do scalenia");
        }

        public bool UpdateTitle()
        {
            if (SelectedItem != -1)
            {
                Console.Write("Podaj tytuł pliku, który chcesz użyć jako odnośnik do niego w spisie treści: ");
                return Service.UpdateTitle(SelectedItem, Console.ReadLine()) == SelectedItem;
            }

            return false;
        }
    }
}