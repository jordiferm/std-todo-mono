using System;
using Gtk;
using stdtodo; 

public partial class MainWindow: Gtk.Window
{
	TodoItemsService itemsService = new TodoItemsService(); 

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		loadItems (); 
		ShowAll (); 
	}

	private void loadItems() {
		var items = itemsService.findAllItems (); 
		foreach( TodoItem item in items) {
			vbox1.Add (newItemWidget(item)); 
		}
	}

	private TodoItemWidget newItemWidget(TodoItem item) {
		TodoItemWidget listItemWidget = new TodoItemWidget (item.Name); 
		listItemWidget.IsComplete = item.IsComplete; 
		return listItemWidget; 
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
