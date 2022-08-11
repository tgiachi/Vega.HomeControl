using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vega.HomeControl.Api.Data.Directories;
using Vega.HomeControl.Api.Interfaces.Base.Services;

namespace Vega.HomeControl.Api.Interfaces.Services
{
    public interface IFileSystemService
    {
        void InitializeSystemDirectories(SystemDirectories directories);
        bool CreateDirectory(string directory);
        Task<string> ReadFileAsStringAsync(string directory, string fileName);
        Task WriteFileAsStringAsync(string directory, string fileName, string content);
        Task WriteFileAsJsonAsync(string directory, string fileName, object content);
        Task WriteFileAsYamlAsync(string directory, string fileName, object content);
        Task<TData> ReadFileAsJsonAsync<TData>(string directory, string fileName);
        Task<TData> ReadFileAsYamlAsync<TData>(string directory, string fileName);
    }
}
