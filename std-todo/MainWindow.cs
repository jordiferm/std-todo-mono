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
		addActions (); 
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

	private void addActions() {
		Button newItemButton = new Button (); 
		newItemButton.Clicked +=  OnNewItem ;
		newItemButton.Label = "Add New Item"; 
		vbox1.PackEnd (newItemButton, false, false, 10); 
	}	

	private void OnNewItem(object sender, EventArgs e) {

		NewItemDialog dialog = new NewItemDialog (); 
		dialog.Response += delegate(object o, ResponseArgs args) {
			if (args.ResponseId == ResponseType.Ok) {
				addNewItem(new TodoItem(dialog.ItemText)); 
			}		
		};
		dialog.Run (); 
		dialog.Destroy (); 
	}

	private void addNewItem(TodoItem item ) {
		vbox1.Add (newItemWidget(item)); 
		itemsService.addItem(item);
		vbox1.ShowAll(); 
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
