using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
	public class Check
	{
		public static bool IsCorrectPath(string path)
		{
			if (path == "")
			{
				ConsoleWriter.ConsoleColor("Wrong file name!", ConsoleColor.Red);
				return false;
			}

			if (path.Contains(".txt") == false && path.Contains(".csv") == false)
			{
				ConsoleWriter.ConsoleColor("Wrong extension!", ConsoleColor.Red);
				return false;
			}

			if (path == ".txt" || path == ".csv")
			{
				ConsoleWriter.ConsoleColor("Wrong file name!", ConsoleColor.Red);
				return false;
			}
			return true;
		}

		public static bool GetCorrectDescription(out string description)
		{
			Console.Write("Description : ");
			description = Console.ReadLine();

			if (description == "")
			{
				ConsoleWriter.ConsoleColor("Incorrect data", ConsoleColor.Red);
				return false;
			}
			return true;
		}

		public static bool GetCorrectDate(string query, out DateTime? date)
		{
			Console.Write(query);
			if (!DateTime.TryParse(Console.ReadLine(), out DateTime sDate))
			{
				ConsoleWriter.ConsoleColor("Incorrect format date", ConsoleColor.Red);
				date = DateTime.Now;
				return false;
			}
			date = sDate;

			return true;
		}

		public static bool DecisionInSwitch(string query, out bool decision)
		{
			Console.Write(query);
			switch (Console.ReadLine())
			{
				case "n":
					decision = false;
					return true;
				case "y":
					decision = true;
					return true;
				default:
					ConsoleWriter.ConsoleColor("Incorrect data", ConsoleColor.Red);
					decision = false;
					return false;
			}
		}
	}
}
