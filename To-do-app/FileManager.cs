using System.Text.Json;

namespace ToDoApp
{
    public class FileManager
    {
        private static string _currentDir = Environment.CurrentDirectory;
        private static string _path = Directory.GetParent(_currentDir).Parent.Parent.FullName + @"\ToDoList.json";
        public static List<ToDoList> ToDoLists { get; set; }

        public static void UpdateJson()
        {
            FileManager.CreateFiles();
            FileManager.ToDoLists = FileManager.GetFiles();
        }

        public static void CreateFiles()
        {
            if (!File.Exists(_path) || String.IsNullOrEmpty(File.ReadAllText(_path)))
            {
                using (FileStream fs = File.Create(_path)) { }
                File.WriteAllText(_path, "[]");
            }
        }

        public static void WriteUpdatedJson() //Writes the current state of ToDoLists (List of ToDoList objects) to Jsonfile
        {
            string JsonData = JsonSerializer.Serialize(ToDoLists);
            File.WriteAllText(_path, JsonData);
        }

        public static List<ToDoList> GetFiles()
        {
            string JsonData = File.ReadAllText(_path);
            var ToDoLists = JsonSerializer.Deserialize<List<ToDoList>>(JsonData,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, WriteIndented = true });
            return ToDoLists;
        }
    }
}