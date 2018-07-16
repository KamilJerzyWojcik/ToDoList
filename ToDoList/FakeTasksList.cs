using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
	class FakeTasksList
	{

		public static List<TaskModel> GetFakeList()
		{

			List<TaskModel> tasks = new List<TaskModel> {
			//	new TaskModel("wycieczka", DateTime.Parse("2018-09-12"), false, false, null),
			//	new TaskModel("wyjazd służbowy", DateTime.Parse("2018-09-30"), true, true, DateTime.Parse("2018-12-01")),
			//	new TaskModel("wycieczka", DateTime.Parse("2019-11-12"), true, false, null),
			//	new TaskModel("wycieczka", DateTime.Parse("2020-09-12"), true, false, null),
			//	new TaskModel("wyjazd służbowy", DateTime.Parse("2019-11-30"), false, true, DateTime.Parse("2019-12-01")),
			//	new TaskModel("wycieczka", DateTime.Parse("2022-09-12"), true, false, null),
			//	new TaskModel("wyjazd służbowy", DateTime.Parse("2016-09-30"), false, true, DateTime.Parse("2017-12-01"))
			};

			return tasks;
		}
	}
}
