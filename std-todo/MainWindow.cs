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

	private async void  loadItems() {
		var items = await itemsService.findAllItems (); 
		foreach( TodoItem item in items) {
			vbox1.Add (newItemWidget(item)); 
			vbox1.ShowAll (); 
		}
	}

	private void addActions() {
		Button newItemButton = new Button (); 
		newItemButton.Clicked +=  OnNewItem ;
		newItemButton.Label = "Add New Item"; 
		vbox1.PackEnd (newItemButton, false, false, 10); 

		Button refreshButton = new Button (); 
		refreshButton.Clicked +=  OnRefreshItems ;
		refreshButton.Label = "Refresh"; 
		vbox1.PackStart (refreshButton, false, false, 10); 
	}	

	private TodoItemWidget newItemWidget(TodoItem item) {
		TodoItemWidget listItemWidget = new TodoItemWidget (item); 
		listItemWidget.IsComplete = item.IsComplete; 
		listItemWidget.DeleteClicked += ItemAboutToDelete;
		listItemWidget.ActivateChanged += ItemActivateChanged;
		return listItemWidget; 
	}

	private void ItemAboutToDelete(object widget, TodoItem item) {
		deleteTodoItem((Widget)widget, item); 
		justDeletedMessage (item);
	}

	private void ItemActivateChanged(TodoItemWidget widget, Boolean IsComplete) {
		itemsService.updateItemStatus (widget.Item, IsComplete); 	
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

	private void OnRefreshItems(object sender, EventArgs e) {
		refreshItems (); 
	}


	private async void addNewItem(TodoItem item ) {
		if (await itemsService.addItem(item))
		{
			refreshItems (); 
			//If the server works fine we can do some Optimistic GUI here and shouldn't need to refresh all the items:
			/*vbox1.Add (newItemWidget(item)); 
			vbox1.ShowAll(); */
		}
	}

	private void refreshItems() {
		foreach (Widget item in vbox1.Children) {
			if (item is TodoItemWidget)
				vbox1.Remove(item); 
		}
		loadItems (); 

	}

	private async void deleteTodoItem(Widget widget, TodoItem item) {
		if (await itemsService.removeItem (item)) {
			vbox1.Remove (widget); 
			vbox1.ShowAll (); 
		}
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
