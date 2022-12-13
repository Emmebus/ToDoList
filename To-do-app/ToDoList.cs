using System.ComponentModel;

namespace ToDoApp
{
    public class ToDoList
    {
        public string Title { get; set; }
        public List<Task> Tasks { get; set; }
        public ToDoList(string title)
        {
            Title= title;
            Tasks = new List<Task>();
        }

        public static void Create()
        {
            Console.Clear();
            Console.WriteLine("CREATING NEW LIST");
            Console.WriteLine("Enter list title");
            var titleInput = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(titleInput))
            {
                Console.WriteLine("Title cannot be empty");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                Create();
                return;
            }
            Console.Clear();
            Console.WriteLine($"Title set to: {titleInput}");
            if (Tool.Confirm())
            {
                ToDoList list = new ToDoList(titleInput);
                FileManager.ToDoLists.Add(list);
                FileManager.WriteUpdatedJson();
                Console.WriteLine("New list created!");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Creation of new list cancelled");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
        }

        public static void View()
        {
            if (FileManager.ToDoLists.Count == 0)
                Console.WriteLine("No lists currently avalible");
            else { 
                for (int i = 0; i < FileManager.ToDoLists.Count; i++)
                {
                    Console.WriteLine($"[{i}] {FileManager.ToDoLists[i].Title}");
                }
            }
        }

        public static void Edit(int index)
        {
            Console.WriteLine("Enter a new title:");
            Console.WriteLine("Enter nothing to cancel");
            string titleInput = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(titleInput))
            {
                Console.WriteLine("Edit of list cancelled");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                return;
            }
            FileManager.ToDoLists[index].Title = titleInput;
            FileManager.WriteUpdatedJson();
            Console.Clear();
            Console.WriteLine($"List updated to {FileManager.ToDoLists[index].Title}");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }

        public static void Delete(int index)
        {
            Console.WriteLine($"Are you sure you wanna delete list {index} : {FileManager.ToDoLists[index].Title}?");
            if (Tool.Confirm())
            {
                FileManager.ToDoLists.RemoveAt(index);
                FileManager.WriteUpdatedJson();
                return;
            }
        }
    }
}