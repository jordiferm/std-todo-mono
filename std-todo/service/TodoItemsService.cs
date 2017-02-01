using System;
using System.Collections.Generic;

namespace stdtodo
{
	public class TodoItemsService
	{
		List<TodoItem> todoItemList = new List<TodoItem>(); 

		public TodoItemsService ()
		{
			todoItemList.Add(new TodoItem("First TODO Item")); 
			todoItemList.Add(new TodoItem("Second TODO Item")); 
			todoItemList.Add(new TodoItem("Third TODO Item")); 
			todoItemList.Add(new TodoItem("Forth TODO Item")); 
			todoItemList.Add(new TodoItem("Fifth TODO Item")); 
		}

		public List<TodoItem>  findAllItems() {
			//TODO: Get from RESTFUL; 
			return todoItemList; 
		}

		public void addItem(TodoItem item) {
			todoItemList.Add (item); 
		}
	}
}

