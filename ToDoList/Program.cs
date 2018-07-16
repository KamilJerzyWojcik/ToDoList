using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ToDoList
{
	class Program
	{
		static void Main(string[] args)
		{
			string command;
			List<TaskModel> tasks = FakeTasksList.GetFakeList();

			Dictionary<string, int> menu = new Dictionary<string, int>
			{
				["Add task: "] = 15,
				["add"] = 10,
				["Remove task: "] = 15,
				["remove"] = 10,
				["Show task: "] = 15,
				["show"] = 10,
				["Save task: "] = 15,
				["save"] = 10,
				["Load task: "] = 15,
				["load"] = 10,
				["Filter task"] = 15,
				["filter"] = 10,
				["Exit program"] = 15,
				["exit"] = 10
			};
			ConsoleWriter.ConsoleColor("Select command from list:", ConsoleColor.Cyan);
			do
			{


				ConsoleWriter.WriteHead(menu, Position.Mix);
				CurrentTask(tasks);
				Console.Write("\nWrite command: ");
				command = Console.ReadLine().Trim().ToLower();

				if (command == "exit") break;

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
					case "filter":
						Console.Clear();
						FilterBy.FilterMenu(tasks);
						break;
					default:
						ConsoleWriter.ConsoleColor("Select command from list:", ConsoleColor.Cyan);
						break;
				}
			} while (true);
		}

		private static void CurrentTask(List<TaskModel> tasks)
		{
			string fDay = "All day";
			TaskModel currentTask = tasks.Where(x => x.StartDate > DateTime.Now).OrderBy(x => x.StartDate).FirstOrDefault();

			if (currentTask == null)
			{
				Console.WriteLine("no upcoming events");
				return;
			}

			if (currentTask.FinishDate != null)
			{
				fDay = currentTask.FinishDate.GetValueOrDefault().ToString("d");
			}

			Dictionary<string, int> current = new Dictionary<string, int>
			{
				["Current task: "] = 15,
				[""] = 10,
				["Start day: "] = 15,
				[currentTask.StartDate.GetValueOrDefault().ToString("d")] = 10,
				["Finish day: "] = 15,
				[fDay] = 10,
				["Description: "] = 15,
				[currentTask.Description] = 10,
				["Important: "] = 15,
				[currentTask.Important.ToString()] = 10,
			};

			Console.WriteLine("".PadLeft(25, '.'));
			ConsoleWriter.WriteHead(current, Position.Mix);
			Console.WriteLine("".PadLeft(25, '.'));
		}

		private static void SaveTasks(List<TaskModel> tasks)
		{
			ConsoleWriter.WriteEx("Set name/path file to save (e.g. List.txt): ");

			string path = Console.ReadLine();
			if (!Check.IsCorrectPath(path)) return;

			if (File.Exists(path) != true)
			{
				using (StreamWriter s = File.CreateText(path))
				{
					string[] taskString;
					string fDay = "";
					foreach (TaskModel task in tasks)
					{
						if (task.FinishDate != null)
						{
							fDay = ((DateTime)task.FinishDate).ToString("d");
						}

						taskString = new string[] { task.Description, ((DateTime)task.StartDate).ToString("d"), fDay, task.AllDay.ToString(), task.Important.ToString() };

						s.WriteLine(string.Join(",", taskString));
					}
				}

				ConsoleWriter.ConsoleColor("File was saved", ConsoleColor.Green);
			}
			else
			{
				ConsoleWriter.ConsoleColor("File exist!", ConsoleColor.Red);
				return;
			}
		}

		private static void LoadTasks(List<TaskModel> tasks)
		{
			ConsoleWriter.WriteEx("Set name/path file (List.txt): ");
			string path = Console.ReadLine();

			if (!Check.IsCorrectPath(path)) return;

			string[] text = File.ReadAllLines(path);
			DateTime? finishDate;
			DateTime fDate;

			foreach (string task in text)
			{
				string[] parts = new string[] { };

				if (task.Contains(','))
				{
					parts = task.Split(',');
				}

				if (task.Contains(';'))
				{
					parts = task.Split(';');
				}
				if (!DateTime.TryParse(parts[2], out fDate))
				{
					finishDate = null;
				}
				else
				{
					finishDate = fDate;
				}

				tasks.Add(new TaskModel(parts[0], DateTime.Parse(parts[1]), bool.Parse(parts[3]),
										bool.Parse(parts[4]), finishDate));
			}

			ConsoleWriter.ConsoleColor("File was loaded", ConsoleColor.Green);
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
				ConsoleWriter.ConsoleColor("Task was removed", ConsoleColor.Green);
				ShowTask(tasks);
			}
			else
			{
				ConsoleWriter.ConsoleColor("Wrong Index", ConsoleColor.Red);
				return;
			}
		}

		internal static void ShowTask(List<TaskModel> tasks)
		{
			string fDay = "";
			Console.WriteLine("Task |".PadLeft(10) + "Description |".PadLeft(20) + "Start day |".PadLeft(15) + "All day |".PadLeft(10) + "Finish day |".PadLeft(15) + "Important |".PadLeft(11));
			Console.WriteLine("".PadLeft(100, '.'));

			for (int i = 0; i <= tasks.Count - 1; i++)
			{

				if (tasks[i].FinishDate != null)
				{
					fDay = tasks[i].FinishDate.GetValueOrDefault().ToString("d");
				}

				Console.WriteLine($"{i + 1} |".PadLeft(10) +
				$"{tasks[i].Description} |".PadLeft(20) +
				$"{tasks[i].StartDate.GetValueOrDefault().ToString("d")} |".PadLeft(15) +
				$"{tasks[i].AllDay} |".PadLeft(10) +
				$"{fDay} |".PadLeft(15) +
				$"{tasks[i].Important} |".PadLeft(11));

				fDay = "";
			}
			Console.WriteLine();
		}

		private static void AddTask(List<TaskModel> tasks)
		{
			Console.Clear();
			DateTime? finishDate = null;
			if (!Check.GetCorrectDescription(out string description)) return;
			if (!Check.GetCorrectDate("Start day (yyyy-mm-dd): ", out DateTime? startDate)) return;
			if (!Check.DecisionInSwitch("All day task? y/n : ", out bool allDay)) return;

			if (allDay == false)
				if (!Check.GetCorrectDate("Finish day (yyyy-mm-dd) : ", out finishDate)) return;

			if (!Check.DecisionInSwitch("Important? y/n : ", out bool important)) return;

			tasks.Add(new TaskModel(description, startDate, allDay, important, finishDate));

			Console.Clear();
			ConsoleWriter.ConsoleColor("Task was added", ConsoleColor.Green);
			ShowTask(tasks);
		}
	}
}
