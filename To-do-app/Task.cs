using System;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace ToDoApp
{
    public class Task
    {
        public Task(string taskTitle, string taskDescription) //constructor
        {
            TaskTitle= taskTitle;
            TaskDescription = taskDescription;
        }

        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public bool IsCompleted { get; set; }

        public static void CreateTask(int listIndex)
        {
            Console.Clear();
            Console.WriteLine("CREATING NEW TASK");
            Console.WriteLine("Enter task title:");
            var titleInput = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(titleInput))
            {
                Console.WriteLine("Title cannot be empty");
                CreateTask(listIndex);
                return;
            }
            Console.WriteLine(); //line break
            Console.WriteLine("Enter task description:");
            var descriptionInput = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(descriptionInput))
            {
                Console.WriteLine("title cannot be empty");
                CreateTask(listIndex);
                return;
            }
            Console.Clear();
            Console.WriteLine($"Task title set to: {titleInput}.");
            Console.WriteLine($"Task description title set to: {descriptionInput}.");
            if (Tool.Confirm())
            { 
                Task newTask = new Task(titleInput, descriptionInput);
                FileManager.ToDoLists[listIndex].Tasks.Add(newTask);
                FileManager.WriteUpdatedJson();
                Console.WriteLine("New task created");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Creating task cancelled");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
        }
        public static void TaskView(int ListIndex)
        {
            if (FileManager.ToDoLists[ListIndex].Tasks.Count() == 0) { 
                Console.WriteLine("No tasks currently avalible for this list");
                return;
            }
            else
            {
                for (int i = 0; i < FileManager.ToDoLists[ListIndex].Tasks.Count; i++)
                {
                    Console.WriteLine($"Task {i} : {FileManager.ToDoLists[ListIndex].Tasks[i].TaskTitle} {CheckMark(FileManager.ToDoLists[ListIndex].Tasks[i].IsCompleted)}");
                    Console.WriteLine($"{FileManager.ToDoLists[ListIndex].Tasks[i].TaskDescription}");
                }
            }
        }

        public static void EditTask(int ListIndex, int TaskIndex)
        {
            Console.WriteLine("Enter a new title:");
            Console.WriteLine("Enter nothing to continue without editing");
            string titleInput = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(titleInput))
            {
                Console.WriteLine("Title not edited");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
            else
            {
                FileManager.ToDoLists[ListIndex].Tasks[TaskIndex].TaskTitle = titleInput;
            }
            Console.WriteLine("Enter a new description:");
            Console.WriteLine("Enter nothing to continue without editing");
            string descriptionInput = Console.ReadLine().Trim();
            if (string.IsNullOrWhiteSpace(titleInput))
            {
                Console.WriteLine("Description not edited");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
            else
            {
                FileManager.ToDoLists[ListIndex].Tasks[TaskIndex].TaskDescription = descriptionInput;
            }
            FileManager.WriteUpdatedJson();
            Console.Clear();
            Console.WriteLine("Current task");
            Console.WriteLine($"{FileManager.ToDoLists[ListIndex].Tasks[TaskIndex].TaskTitle}");
            Console.WriteLine(FileManager.ToDoLists[ListIndex].Tasks[TaskIndex].TaskDescription);
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }

        public static void DeleteTask(int ListIndex, int TaskIndex)
        {
            Console.WriteLine($"Are you sure you wanna delete task {TaskIndex} : {FileManager.ToDoLists[ListIndex].Tasks[TaskIndex].TaskTitle}?");
            if (Tool.Confirm())
            {
                FileManager.ToDoLists[ListIndex].Tasks.RemoveAt(TaskIndex);
                FileManager.WriteUpdatedJson();
                return;
            }
        }

        public static void ChangeComplete(int ListIndex, int TaskIndex)
        {
            FileManager.ToDoLists[ListIndex].Tasks[TaskIndex].IsCompleted = !FileManager.ToDoLists[ListIndex].Tasks[TaskIndex].IsCompleted;
        }

        private static string CheckMark(bool taskIsComplete)
        {
            if (taskIsComplete)
                return "[X]";
            return "[]";
        }
    }
}