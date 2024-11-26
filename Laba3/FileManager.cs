using Laba3;
using System.Text.Json;

namespace Laba3
{
    class FileManager
    {
        private FileReader fileReader = new FileReader();
        private FileWriter fileWriter = new FileWriter();
        private FileSerializer fileSerializer = new FileSerializer();

        public FileManager() { }

        private bool DeserializeFile(string content)
        {
            try
            {
                var file = Schelude.GetInstance();
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
            var file = Schelude.GetInstance();
            var json = fileSerializer.Serialize(file.Data);
            fileWriter.WriteFile(file.FilePath, json);
        }

        public bool OpenFile(string filePath)
        {
            var file = Schelude.GetInstance();
            file.FilePath = filePath;
            file.FileContent = fileReader.ReadFile(filePath);

            // Десеріалізація файлу
            var deserializedData = fileSerializer.Deserialize(file.FileContent);
            if (deserializedData != null && deserializedData.Count > 0)
            {
                file.Data = deserializedData;  // Оновлення даних у Schelude
                return true;
            }
            return false;
        }

    }
}