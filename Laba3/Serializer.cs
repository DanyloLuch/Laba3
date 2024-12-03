using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Laba3
{
    class Serializer
    {
        public ObservableCollection<Student> Deserialize(string json)
        {
            return JsonSerializer.Deserialize<ObservableCollection<Student>>(json);
        }

        public string Serialize(ObservableCollection<Student> articles)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            return JsonSerializer.Serialize(articles, options);
        }
    }
}