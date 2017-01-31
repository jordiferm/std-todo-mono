using System;
using Gtk;
using stdtodo; 

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();

		TodoItemWidget listItem = new TodoItemWidget (); 
		listItem.Text = "This is a test widget"; 
		vbox1.Add (listItem); 
		ShowAll (); 
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
