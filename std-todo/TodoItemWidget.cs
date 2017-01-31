using System;
using Gtk;

namespace stdtodo
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class TodoItemWidget : Gtk.Bin
	{
		public TodoItemWidget (String name)
		{
			this.Build ();
			label1.SetAlignment (0, (float).5); 

			ItemName = name ; 
		}

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

