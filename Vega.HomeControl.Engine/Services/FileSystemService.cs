using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Serilog;
using Vega.HomeControl.Api.Data.Config.Root;
using Vega.HomeControl.Api.Data.Directories;
using Vega.HomeControl.Api.Impl.Services;
using Vega.HomeControl.Api.Interfaces.Services;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Vega.HomeControl.Engine.Services
{
    public class FileSystemService : IFileSystemService
    {
        private readonly string _rootDirectory;


        private readonly ISerializer _yamlSerializer = new SerializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();

        private readonly IDeserializer _yamlDeserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();

        public FileSystemService(string rootDirectory)
        {
            _rootDirectory = rootDirectory;
            CreateDirectory("");
        }


        public bool CreateDirectory(string directory)
        {
            if (Directory.Exists(Path.Join(_rootDirectory, directory))) return false;
            Directory.CreateDirectory(Path.Combine(_rootDirectory, directory));
            return true;
        }

        public Task<string> ReadFileAsStringAsync(string directory, string fileName)
        {
            if (Directory.Exists(Path.Join(_rootDirectory, directory))) return null;

            return File.ReadAllTextAsync(Path.Join(_rootDirectory, directory, fileName));
        }

        public Task WriteFileAsStringAsync(string directory, string fileName, string content)
        {
            CreateDirectory(directory);
            return File.WriteAllTextAsync(Path.Combine(_rootDirectory, directory, fileName), content);
        }

        public Task WriteFileAsJsonAsync(string directory, string fileName, object content)
        {
            return WriteFileAsStringAsync(directory, fileName, JsonSerializer.Serialize(content));
        }

        public Task WriteFileAsYamlAsync(string directory, string fileName, object content)
        {
            return WriteFileAsStringAsync(directory, fileName, _yamlSerializer.Serialize(content));
        }

        public async Task<TData> ReadFileAsJsonAsync<TData>(string directory, string fileName)
        {
            var content = await ReadFileAsStringAsync(directory, fileName);

            return JsonSerializer.Deserialize<TData>(content);
        }

        public async Task<TData> ReadFileAsYamlAsync<TData>(string directory, string fileName)
        {
            var content = await ReadFileAsStringAsync(directory, fileName);

            return _yamlDeserializer.Deserialize<TData>(content);
        }

        public void InitializeSystemDirectories(SystemDirectories directories)
        {
            foreach (var d in directories.GetKeys())
            {
                if (d != SystemDirectoryType.Root)
                {
                    CreateDirectory(directories[d]);
                }
            }
            
        }
    }
}
