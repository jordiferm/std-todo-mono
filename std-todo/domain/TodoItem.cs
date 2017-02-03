using System;

namespace stdtodo
{
	public class TodoItem
	{
		String key; 
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

		public String Key {
			get { return key; }
			set { key = value; }

		}

	}
}

