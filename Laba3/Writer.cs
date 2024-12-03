
namespace Laba3
{
    class Writer
    {
        public void WriteFile(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }
    }
}