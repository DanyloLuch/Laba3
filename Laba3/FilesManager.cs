using Laba3;
using System.Text.Json;

namespace Laba3
{
    class FilesManager
    {
        private FileReader fileReader = new FileReader();
        private Writer fileWriter = new Writer();
        private Serializer fileSerializer = new Serializer();

        public FilesManager() { }

        private bool DeserializeFile(string content)
        {
            try
            {
                var file = StudentManager.GetInstance();
                file.Data = fileSerializer.Deserialize(content);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        public void SaveFile()
        {
            var file = StudentManager.GetInstance();
            var json = fileSerializer.Serialize(file.Data);
            fileWriter.WriteFile(file.FilePath, json);
        }

        public bool OpenFile(string filePath)
        {
            var file = StudentManager.GetInstance();
            file.FilePath = filePath;
            file.FileContent = fileReader.ReadFile(filePath);

            var deserializedData = fileSerializer.Deserialize(file.FileContent);
            if (deserializedData != null && deserializedData.Count > 0)
            {
                file.Data = deserializedData;  
                return true;
            }
            return false;
        }

    }
}