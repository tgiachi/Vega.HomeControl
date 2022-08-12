using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vega.HomeControl.Api.Data.Directories
{
    public class SystemDirectories
    {
        private readonly Dictionary<SystemDirectoryType, string> _directories = new();

        public string this[SystemDirectoryType directoryType]
        {
            get => _directories[directoryType];
            set => _directories[directoryType] = value;
        }

        public List<string> GetAllDirectories()
        {
            return _directories.Values.ToList();
        }

        public List<SystemDirectoryType> GetKeys()
        {
            return _directories.Keys.ToList();
        }

        public string GetFullPath(SystemDirectoryType directoryType)
        {
            return Path.Join(this[SystemDirectoryType.Root], this[directoryType]);
        }
    }

    public enum SystemDirectoryType
    {
        Root,
        Configs,
        Database,
        Plugins,
        Scripts,
        Packages,
        Logs
    }
}
