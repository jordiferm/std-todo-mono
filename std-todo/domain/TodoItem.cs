using System;

namespace stdtodo
{
	public class TodoItem
	{
		String name; 
		Boolean isComplete; 


		public TodoItem ()
		{
			IsComplete = false; 
		}

		public TodoItem(String itemName) : this() {
			Name = itemName;
		}

		public String Name {
			get { return name; }
			set { name = value;  }
		}

		public Boolean IsComplete {
			get { return isComplete; }
			set { isComplete = value; }
		}

	}
}

