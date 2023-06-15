using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace money
{
    internal class in_file
    {
        public static void Serialize<T>(ObservableCollection<T> note, string name)
        {
            string json = JsonConvert.SerializeObject(note);
            File.WriteAllText($"{name}.json", json);
        }

        public static ObservableCollection<T> Mydeserializer<T>(string name)
        {
            string json = File.ReadAllText($"{name}.json");
            ObservableCollection<T> note = JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
            return note;
        }

        internal static void Serialize(object types, string v)
        {
            throw new NotImplementedException();
        }
    }
}
