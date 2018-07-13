using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
	public class TaskModel
	{
		public string Description { get; private set; }
		public string StartDate { get; private set; }
		public string FinishDate { get; private set; } = "";
		public bool AllDay { get; private set; }
		public bool Important { get; private set; }

		public TaskModel(string description, string startDate, bool allDay, bool important, string finishDate)
		{
			Description = description;
			StartDate = startDate;
			FinishDate = finishDate;
			AllDay = allDay;
			Important = important;
		}

		public TaskModel(string description, string startDate, bool allDay, bool important)
		{
			Description = description;
			StartDate = startDate;
			AllDay = allDay;
			Important = important;
		}
	}
}
