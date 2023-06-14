using MdFilesMerger.App.Concrete;
using MdFilesMerger.Controller.Common;
using MdFilesMerger.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdFilesMerger.Controller.Concrete
{
    public class IgnoredFileManager : RelativeFileMenager<IgnoredFile, IgnoredFileService>
    {
        public IgnoredFileManager(MainDirectoryService mainDirectoryService) : base(new IgnoredFileService(mainDirectoryService))
        {
        }

        public override void DisplayTitle()
        {
            DisplayTitle("Lista ignorowanych plików");
        }
    }
}