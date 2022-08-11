using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vega.HomeControl.Api.Data.Fs
{
    public class FileSystemObject
    {
        public string FileName { get; set; }

        public long FileSize { get; set; }

        public string FullFileName { get; set; }

        public string HumanizedFileSize { get; set; }

        public DateTime LastModificationDateTime { get; set; }
    }
}
