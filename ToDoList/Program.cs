using System;
using System.Collections.Generic;
using System.IO;

namespace ToDoList
{
	class Program
	{
		static void Main(string[] args)
		{
			string command;
			List<TaskModel> tasks = new List<TaskModel> {
				new TaskModel("wycieczka", "2018-09-12", true, false),
				new TaskModel("wyjazd służbowy", "2018-09-30", false, true, "2018-12-01")
			};

			do
			{
				Console.WriteLine("AddTask: add\n" + "RemoveTask: remove\n" + "ShowTask: show\n" +
								  "SaveTask: save\n" + "LoadTask: load\n\n");
				Console.Write("Write command: ");
				command = Console.ReadLine().Trim().ToLower();


				if (command == "exit")
				{
					break;
				}

				switch (command)
				{
					case "add":
						AddTask(tasks);
						break;
					case "remove":
						RemoveTask(tasks);
						break;
					case "show":
						Console.Clear();
						ShowTask(tasks);
						break;
					case "save":
						SaveTasks(tasks);
						break;
					case "load":
						LoadTasks(tasks);
						break;
					default:
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Cyan;
						Console.WriteLine("Select command from list:\n");
						Console.ResetColor();
						break;
				}
			} while (true);
		}

		private static void Error(string text)
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(text);
			Console.ResetColor();
			Console.WriteLine();
		}

		private static bool IsCorrect()
		{
			if (path == "")
			{
				Error("Wrong file name!");
				return;
			}

			if (path.Contains(".txt") == false)
			{
				if (path == ".txt")
				{
					Error("Wrong file name!");
					return;
				}
				Error("Wrong extension!");
				return;
			}
		}

		private static void SaveTasks(List<TaskModel> tasks)
		{
			Console.Clear();
			Console.Write("Set name/path file to save (e.g. Tasks.txt): ");
			string path = Console.ReadLine();
			if (path == "")
			{
				Error("Wrong file name!");
				return;
			}

			if (File.Exists(path) != true)
			{
				using (StreamWriter s = File.CreateText(path))
				{
					string[] taskString;
					foreach (TaskModel task in tasks)
					{
						taskString = new string[] { task.Description, task.StartDate, task.FinishDate, task.AllDay.ToString(), task.Important.ToString() };

						s.WriteLine(string.Join(",", taskString));
					}
				}
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"File was saved");
				Console.WriteLine();
				Console.ResetColor();
			}
			else
			{
				Error("File exist!");
			}
		}

		private static void LoadTasks(List<TaskModel> tasks)
		{
			Console.Clear();
			Console.Write("Set name/path file (List.txt): ");
			string path = Console.ReadLine();



			if(path == "")
			{
				Error("Wrong file name!");
				return;
			}

			if(path.Contains(".txt") == false)
			{
				if(path == ".txt")
				{
					Error("Wrong file name!");
					return;
				}
				Error("Wrong extension!");
				return;
			}

			string[] text = File.ReadAllLines(path);

			foreach (string task in text)
			{
				string[] parts = task.Split(',');
				tasks.Add(new TaskModel(parts[0], parts[1], bool.Parse(parts[3]),
										bool.Parse(parts[4]), parts[2]));
			}

			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"File was loaded");
			Console.WriteLine();
			Console.ResetColor();
			ShowTask(tasks);
		}

		private static void RemoveTask(List<TaskModel> tasks)
		{
			int i = 0;
			Console.Clear();
			ShowTask(tasks);
			Console.Write("Chose number task to delete : ");
			string decision = Console.ReadLine().Trim();
			bool result = int.TryParse(decision, out i);

			if (result == true && i <= tasks.Count)
			{
				tasks.RemoveAt(i - 1);
				Console.Clear();
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"Task was removed");
				Console.WriteLine();
				Console.ResetColor();
				ShowTask(tasks);
			}
			else
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Wrong Number");
				Console.ResetColor();
				Console.WriteLine();

			}
		}

		private static void ShowTask(List<TaskModel> tasks)
		{
			int i = 1;

			Console.WriteLine("Task |".PadLeft(10) + "Description |".PadLeft(20) + "Start day |".PadLeft(15) + "All day |".PadLeft(10) + "Finish day |".PadLeft(15) + "Important |".PadLeft(11));
			Console.WriteLine("".PadLeft(100, '.'));

			foreach (TaskModel task in tasks)
			{
				Console.WriteLine($"{i} |".PadLeft(10) + $"{task.Description} |".PadLeft(20) + $"{task.StartDate} |".PadLeft(15) +
								  $"{task.AllDay} |".PadLeft(10) + $"{task.FinishDate} |".PadLeft(15) + $"{task.Important} |".PadLeft(11));
				i++;
			}
			Console.WriteLine();
		}

		private static void AddTask(List<TaskModel> tasks)
		{
			bool allDay = true;
			bool important = false;
			string finishDate = "";

			Console.Clear();
			Console.Write("Description : ");
			string description = Console.ReadLine();
			if (description == "")
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Niepoprawne dane");
				Console.ResetColor();
				Console.WriteLine();
				return;
			}

			Console.Clear();
			Console.Write("Start day : ");
			string startDate = Console.ReadLine();
			if (startDate == "")
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Niepoprawne dane");
				Console.ResetColor();
				Console.WriteLine();
				return;
			}

			Console.Clear();
			Console.Write("All day task? y/n : ");
			string decision = Console.ReadLine();
			bool b = true;
			if (decision == "n")
			{
				
				allDay = false;
				Console.Clear();
				Console.Write("Finish day : ");
				finishDate = Console.ReadLine();
				if (description == "")
				{
					Console.Clear();
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Niepoprawne dane");
					Console.ResetColor();
					Console.WriteLine();
					b = false;
				}
				
			}
			if (b == false)
			{
				return;
			}

			Console.Clear();
			Console.Write("Important? y/n : ");
			decision = Console.ReadLine();
			if (decision == "y")
			{
				important = true;
			}
			Console.Clear();

			if (allDay == true)
			{
				tasks.Add(new TaskModel(description, startDate, allDay, important));
			}
			else
			{
				tasks.Add(new TaskModel(description, startDate, allDay, important, finishDate));
			}
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Task was added");
			Console.WriteLine();
			Console.ResetColor();
			ShowTask(tasks);
		}
	}
}
