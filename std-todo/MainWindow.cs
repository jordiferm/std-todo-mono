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

	private void addActions() {
		Button newItemButton = new Button (); 
		newItemButton.Clicked +=  OnNewItem ;
		newItemButton.Label = "Add New Item"; 
		vbox1.PackEnd (newItemButton, false, false, 10); 
	}	

	private TodoItemWidget newItemWidget(TodoItem item) {
		TodoItemWidget listItemWidget = new TodoItemWidget (item); 
		listItemWidget.IsComplete = item.IsComplete; 
		listItemWidget.DeleteClicked += ItemAboutToDelete;
		return listItemWidget; 
	}

	private void ItemAboutToDelete(object widget, TodoItem item) {
		deleteTodoItem((Widget)widget, item); 
		justDeletedMessage (item);
	}

	private void justDeletedMessage(TodoItem item) {
		MessageDialog md = new MessageDialog(this, 
			DialogFlags.DestroyWithParent, MessageType.Info, 
			ButtonsType.Close, "ToDo item " + item.Name + " has been deleted correctly");
		md.Run();
		md.Destroy();		
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

	private void deleteTodoItem(Widget widget, TodoItem item) {
		vbox1.Remove (widget); 
		itemsService.removeItem (item);
		vbox1.ShowAll (); 
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
