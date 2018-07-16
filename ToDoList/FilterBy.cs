using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ToDoList
{
	public class FilterBy
	{
		public static void FilterMenu(List<TaskModel> tasks)
		{
			Dictionary<string, int> menu = new Dictionary<string, int>
			{
				["Description: "] = 15,
				["d"] = 10,
				["Finish day: "] = 15,
				["fd"] = 10,
				["Start day: "] = 15,
				["sd"] = 10,
				["Important: "] = 15,
				["i"] = 10,
				["All Day: "] = 15,
				["ad"] = 10,
				["Back to main menu"] = 15,
				["back"] = 10
			};
			string command;
			ConsoleWriter.ConsoleColor("Select command from list:", ConsoleColor.Cyan);
			do
			{
				ConsoleWriter.WriteHead(menu, Position.Mix);
				Console.Write("\nWrite command: ");

				command = Console.ReadLine().Trim().ToLower();

				switch (command)
				{
					case "d":
						Console.Clear();
						Description("description", tasks);
						break;
					case "sd":
						Console.Clear();
						Date(ToDoList.Date.Start, tasks);
						break;
					case "fd":
						Console.Clear();
						Date(ToDoList.Date.Finish, tasks);
						break;
					case "i":
						Console.Clear();
						DecisionFor(tasks, Decision.Important);
						break;
					case "ad":
						Console.Clear();
						DecisionFor(tasks, Decision.AllDay);
						break;
					case "back":
						Console.Clear();
						return;
					default:
						ConsoleWriter.ConsoleColor("Select command from list:", ConsoleColor.Cyan);
						break;
				}
			} while (true);
		}

		public static void Description(string category, List<TaskModel> tasks)
		{
			Program.ShowTask(tasks.OrderBy(x => x.Description).Select(x => x).ToList());
		}

		public static void Date(Date date, List<TaskModel> tasks)
		{

			if (date == ToDoList.Date.Finish)
			{
				Program.ShowTask(tasks.OrderBy(y => y.FinishDate).Select(y => y).ToList());
				return;
			}

			Program.ShowTask(tasks.OrderBy(y => y.StartDate).Select(y => y).ToList());
		}

		public static void DecisionFor(List<TaskModel> tasks, Decision decision)
		{
			if (decision == Decision.Important)
			{
				Program.ShowTask(tasks.OrderBy(x => x.Important).Select(x => x).ToList());
				return;
			}
			Program.ShowTask(tasks.OrderBy(x => x.AllDay).Select(x => x).ToList());
		}

	}

}
