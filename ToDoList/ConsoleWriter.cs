using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
	public class ConsoleWriter
	{

		public static void WriteEx(string text)
		{
			Console.Clear();
			Console.Write(text);
		}

		public static void ConsoleColor(string text, ConsoleColor color)
		{
			Console.Clear();
			Console.ForegroundColor = color;
			Console.WriteLine(text);
			Console.ResetColor();
			Console.WriteLine();
		}

		public static void WriteHead(Dictionary<string, int> texts, Position position)
		{
			string template = "";
			int i = 0;
			foreach (KeyValuePair<string, int> text in texts)
			{
				i++;

				if (position == Position.Horizontal || position == Position.Mix && i % 2 != 0)
				{
					if (position == Position.Horizontal) template += text.Key.PadLeft(text.Value);
					if (position == Position.Mix) template += text.Key.PadRight(text.Value);
				}

				if (position == Position.Vertical || position == Position.Mix && i % 2 == 0)
				{
					template += (text.Key + "\n").PadLeft(text.Value);
				}
			}
			Console.WriteLine(template);
		}

	}
}
