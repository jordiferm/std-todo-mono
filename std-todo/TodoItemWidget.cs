using System;
using Gtk;

namespace stdtodo
{
	public delegate void DeleteClickedEventHandler(object sender, TodoItem item);
	public delegate void ActivateChangedEventHandler(TodoItemWidget sender, Boolean Active); 

	[System.ComponentModel.ToolboxItem (true)]
	public partial class TodoItemWidget : Gtk.Bin
	{
		public event DeleteClickedEventHandler DeleteClicked;
		public event ActivateChangedEventHandler ActivateChanged; 
		private TodoItem myItem; 

		public void OnDeleteClicked(object sender, EventArgs e) {
			if (DeleteClicked != null)
				DeleteClicked (this, myItem); 
		}

		public void OnActivateClicked(object sender, EventArgs e) {
			if (ActivateChanged != null)
				ActivateChanged (this, this.IsComplete); 
		}

		public TodoItemWidget (TodoItem item)
		{
			this.Build ();
			label1.SetAlignment (0, (float).5); 
			deleteButton.Clicked += OnDeleteClicked; 
			checkbutton.Clicked += OnActivateClicked; 
			deleteButton.BorderWidth = 0; 
			myItem = item; 
			ItemName = item.Name ; 
		}

		public TodoItem Item { get { return myItem; } } 

		public String ItemName {
			get{
				return label1.Text; 
			}
			set {
				label1.Text = value; 
			}
		}

		public Boolean IsComplete {
			get { 
				return checkbutton.Active; 
			}
			set {
				checkbutton.Active = value;
			}
		}


	}
}

