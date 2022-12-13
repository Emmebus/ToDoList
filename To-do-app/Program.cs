using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace ToDoApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input;
            FileManager.CreateFiles(); //Initializes JSON file
            FileManager.ToDoLists = FileManager.GetFiles(); //gets data from JSON file

            startMenu(); //Start of app

            void startMenu()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("START MENU");
                    Console.WriteLine("[1] View latest list");
                    Console.WriteLine("[2] View all current lists");
                    Console.WriteLine("[3] Create new list");
                    Console.WriteLine("[X] Exit program");
                    Console.WriteLine(); //line break
                    input = Console.ReadLine()
                        .ToLower()
                        .Trim();

                    if (input == "x" || input == "exit" || string.IsNullOrWhiteSpace(input))
                    {
                        Tool.Exit();
                    }
                    else if (input == "1")
                    {
                        if (FileManager.ToDoLists.Count <= 0){
                            Console.WriteLine("No lists avalible");
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                        }
                        else
                        {
                            listMenu(FileManager.ToDoLists.Count - 1);
                        }
                    }
                    else if (input == "2")
                    {
                        listView();
                    }
                    else if (input == "3")
                    {
                        ToDoList.Create();
                    }
                    else
                    {
                        Console.WriteLine("Invalid input: " + input + ". Try Again");
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                    }
                }
            }

            void listView()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("LIST VIEW");
                    ToDoList.View();
                    Console.WriteLine();//line break
                    Console.WriteLine("Select a list by entering its index number");
                    Console.WriteLine("[S] To start menu");
                    Console.WriteLine("[X] Exit program");
                    Console.WriteLine(); //line break
                    input = Console.ReadLine()
                        .ToLower()
                        .Trim();

                    for (int i = 0; i < FileManager.ToDoLists.Count; i++)
                    {
                        if (input == i.ToString())
                        {
                            listMenu(i);
                            listView();
                            return; // To startMenu
                        }
                    }

                    if (input == "x" || input == "exit" || string.IsNullOrWhiteSpace(input))
                    {
                        Tool.Exit();
                    }
                    else if (input == "s" || input == "start")
                    {
                        return; // To startMenu
                    }
                    else
                    {
                        Console.WriteLine("Invalid input: " + input + ". Try Again");
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                    }
                }
            }

            void listMenu(int index)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("LIST MENU");
                    Console.WriteLine($"List {index} : {FileManager.ToDoLists[index].Title}");
                    Console.WriteLine();// Line break
                    Task.TaskView(index);
                    Console.WriteLine();// Line break
                    Console.WriteLine("[1] Edit list");
                    Console.WriteLine("[2] Delete list");
                    Console.WriteLine("[3] Create task");
                    Console.WriteLine("[4] Select a Task");
                    Console.WriteLine("[B] Back to previous page");
                    Console.WriteLine("[S] To start menu");
                    Console.WriteLine("[X] Exit program");
                    input = Console.ReadLine()
                        .ToLower()
                        .Trim();

                    if (input == "x" || input == "exit" || string.IsNullOrWhiteSpace(input))
                    {
                        Tool.Exit();
                    }
                    else if (input == "1")
                    {
                        ToDoList.Edit(index);
                    }
                    else if (input == "2")
                    {
                        ToDoList.Delete(index);
                        return; //Returns to listView
                    }
                    else if (input == "3")
                    {
                        Task.CreateTask(index);
                    }
                    else if (input == "4")
                    {
                        taskView(index);
                    }
                    else if (input == "b" || input == "back")
                    {
                        return; //Returns to listView
                    }
                    else if (input == "s" || input == "start")
                    {
                        startMenu();
                        return; //Returns to startMenu
                    }
                    else
                    {
                        Console.WriteLine("Invalid input: " + input + ". Try Again");
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                    }
                }
            }

            void taskView(int listIndex){
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"TASK VIEW FOR: {FileManager.ToDoLists[listIndex].Title}.");
                    Task.TaskView(listIndex);
                    Console.WriteLine();//line break
                    Console.WriteLine("Select a task by entering its index number");
                    Console.WriteLine("[B] Back to previous page");
                    Console.WriteLine("[S] To start menu");
                    Console.WriteLine("[X] Exit program");
                    string input = Console.ReadLine()
                        .ToLower()
                        .Trim();

                    for (int i = 0; i < FileManager.ToDoLists[listIndex].Tasks.Count; i++)
                    {
                        if (input == i.ToString())
                        {
                            Console.WriteLine($"Task {i} selected, choose action:");
                            Console.WriteLine("[1] Edit");
                            Console.WriteLine("[2] Delete");
                            Console.WriteLine("[3] Change complete status");
                            input = Console.ReadLine();
                            if (input == "1")
                            {
                                Task.EditTask(listIndex, i);
                                taskView(listIndex);
                                return; //return to task views
                            }
                            else if (input == "2")
                            {
                                Task.DeleteTask(listIndex, i);
                                taskView(listIndex);
                                return;//return to task views
                            }
                            else if (input == "3")
                            {
                                Task.ChangeComplete(listIndex, i);
                                taskView(listIndex);
                                return; //return to task views
                            }
                            else
                            {
                                break; //stops loop. Continues to next if/else check
                            }
                        }
                    }

                    if (input == "x" || input == "exit" || string.IsNullOrWhiteSpace(input))
                    {
                        Tool.Exit();
                    }
                    else if (input == "b" || input == "back")
                    {
                        return; //returns to listMenu
                    }
                    else if (input == "s" || input == "start")
                    {
                        startMenu();
                        return; //Returns to startMenu
                    }
                    else
                    {
                        Console.WriteLine("outside loop");
                        Console.WriteLine("Invalid input: " + input + ". Try Again");
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}


