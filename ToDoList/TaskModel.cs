using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
	public class TaskModel
	{
		public string Description { get; private set; }
		public DateTime? StartDate { get; private set; }
		public DateTime? FinishDate { get; private set; }
		public bool AllDay { get; private set; }
		public bool Important { get; private set; }

		public TaskModel(string description, DateTime? startDate, bool allDay, bool important, DateTime? finishDate)
		{
			Description = description;
			StartDate = startDate;
			FinishDate = finishDate;
			AllDay = allDay;
			Important = important;
		}
	}
}
