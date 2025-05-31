using Asana.Library.Models;
using System;

namespace Asana
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var toDos = new List<ToDo>();
            int choiceInt;
            var itemCount = 0;
            var toDoChoice = 0;
            var projects = new List<Project>();
            var projectCount = 0;

            do
            {
                Console.WriteLine("Choose a menu option:");
                Console.WriteLine("1. Create a ToDo");
                Console.WriteLine("2. List all ToDos");
                Console.WriteLine("3. List all outstanding ToDos");
                Console.WriteLine("4. Delete a ToDo");
                Console.WriteLine("5. Update a ToDo");
                Console.WriteLine("6. Create a Project");
                Console.WriteLine("7. Delete a Project");
                Console.WriteLine("8. Update a Project");
                Console.WriteLine("9. List all Projects");
                Console.WriteLine("10. List ToDos in a Project");
                Console.WriteLine("11. Exit");

                var choice = Console.ReadLine() ?? "11";

                if (int.TryParse(choice, out choiceInt))
                {
                    switch (choiceInt)
                    {
                        case 1:
                            Console.Write("Name:");
                            var name = Console.ReadLine();
                            Console.Write("Description:");
                            var description = Console.ReadLine();

                            var newToDo = new ToDo
                            {
                                Name = name,
                                Description = description,
                                IsCompleted = false,
                                Id = ++itemCount
                            };

                            // checks if any projects exist
                            if (projects.Any())
                            {
                                Console.Write("Would you like to assign this ToDo to a project? (y/n): ");
                                var assign = Console.ReadLine()?.ToLower();

                                if (assign == "y")
                                {
                                    Console.WriteLine("Available Projects:");
                                    projects.ForEach(p => Console.WriteLine($"[{p.Id}] {p.Name}"));
                                    Console.WriteLine();

                                    Console.Write("Enter the project ID: ");
                                    if (int.TryParse(Console.ReadLine(), out int projId))
                                    {
                                        var selectedProject = projects.FirstOrDefault(p => p.Id == projId);
                                        if (selectedProject != null)
                                        {
                                            newToDo.ProjectId = selectedProject.Id;
                                            selectedProject.ToDos.Add(newToDo);
                                            Console.WriteLine("ToDo added to project.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid project ID. ToDo will not be assigned to any project.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input. ToDo will not be assigned to any project.");
                                    }
                                }
                            }

                            toDos.Add(newToDo);
                            break;
                        case 2:
                            toDos.ForEach(Console.WriteLine);
                            Console.WriteLine();
                            break;
                        case 3:
                            toDos.Where(t => (t != null) && !(t?.IsCompleted ?? false))
                                .ToList()
                                .ForEach(Console.WriteLine);
                                Console.WriteLine();
                            break;
                        case 4:

                            toDos.ForEach(Console.WriteLine);
                            Console.WriteLine();
                            Console.Write("ToDo to Delete: ");
                            toDoChoice = int.Parse(Console.ReadLine() ?? "0");

                            var reference = toDos.FirstOrDefault(t => t.Id == toDoChoice);
                            if (reference != null)
                            {
                                toDos.Remove(reference);
                            }

                            break;
                        case 5:

                            toDos.ForEach(Console.WriteLine);
                            Console.WriteLine();
                            Console.Write("ToDo to Update: ");
                            toDoChoice = int.Parse(Console.ReadLine() ?? "0");
                            var updateReference = toDos.FirstOrDefault(t => t.Id == toDoChoice);

                            if (updateReference != null)
                            {
                                Console.WriteLine("What do you want to update?");
                                Console.WriteLine("1. Name");
                                Console.WriteLine("2. Description");
                                Console.WriteLine("3. Completion Status");
                                Console.WriteLine("4. Cancel");
                                Console.Write("Choose an option: ");
                                var updateOption = int.Parse(Console.ReadLine() ?? "0");

                                switch (updateOption)
                                {
                                    case 1:
                                        Console.Write("New Name: ");
                                        updateReference.Name = Console.ReadLine();
                                        break;
                                    case 2:
                                        Console.Write("New Description: ");
                                        updateReference.Description = Console.ReadLine();
                                        break;
                                    case 3:
                                        Console.Write("Mark as completed? (y/n): ");
                                        var completed = Console.ReadLine();
                                        updateReference.IsCompleted = completed?.ToLower() == "y";
                                        break;
                                    case 4:
                                        break;
                                    default:
                                        Console.WriteLine("Invalid option.");
                                        break;
                                }
                            }
                            break;
                        case 6: // create project
                            Console.Write("Project Name: ");
                            var projName = Console.ReadLine();
                            Console.Write("Project Description: ");
                            var projDesc = Console.ReadLine();

                            projects.Add(new Project
                            {
                                Id = ++projectCount,
                                Name = projName,
                                Description = projDesc
                            });
                            break;
                        case 7: // delete projects
                            projects.ForEach(Console.WriteLine);
                            Console.WriteLine();
                            Console.Write("Project to Delete: ");
                            var deleteId = int.Parse(Console.ReadLine() ?? "0");
                            var projToDelete = projects.FirstOrDefault(p => p.Id == deleteId);

                            if (projToDelete != null)
                            {
                                projects.Remove(projToDelete);
                            }
                            break;
                        case 8: // update project
                                projects.ForEach(Console.WriteLine);
                                Console.WriteLine();
                                Console.Write("Project to Update: ");
                                var updateId = int.Parse(Console.ReadLine() ?? "0");

                                var pToUpdate = projects.FirstOrDefault(p => p.Id == updateId);
                                if (pToUpdate != null)
                                {
                                    Console.WriteLine("What do you want to update?");
                                    Console.WriteLine("1. Name");
                                    Console.WriteLine("2. Description");
                                    Console.WriteLine("3. Cancel");
                                    Console.Write("Choose an option: ");
                                    var projectUpdateChoice = int.Parse(Console.ReadLine() ?? "0");

                                    switch (projectUpdateChoice)
                                    {
                                        case 1:
                                            Console.Write("New Name: ");
                                            pToUpdate.Name = Console.ReadLine();
                                            break;
                                        case 2:
                                            Console.Write("New Description: ");
                                            pToUpdate.Description = Console.ReadLine();
                                            break;
                                        case 3:
                                            break;
                                        default:
                                            Console.WriteLine("Invalid option.");
                                            break;
                                    }
                                }
                            break;
                        case 9: // list projects
                            projects.ForEach(Console.WriteLine);
                            Console.WriteLine();
                            break;
                        case 10: // list ToDos in a project
                            projects.ForEach(Console.WriteLine);
                            Console.WriteLine();
                            Console.Write("Project to view ToDos: ");
                            var pid = int.Parse(Console.ReadLine() ?? "0");

                            var p = projects.FirstOrDefault(x => x.Id == pid);
                            if (p != null)
                            {
                                p.ToDos.ForEach(Console.WriteLine);
                                Console.WriteLine(); //
                            }
                            break;
                        case 11:
                            break;
                        default:
                            Console.WriteLine("ERROR: Unknown menu selection");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"ERROR: {choice} is not a valid menu selection");
                }

            } while (choiceInt != 11);

        }
    }
}