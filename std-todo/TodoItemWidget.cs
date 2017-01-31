using System;
using Gtk;

namespace stdtodo
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class TodoItemWidget : Gtk.Bin
	{
		public TodoItemWidget ()
		{
			this.Build ();
			label1.SetAlignment (0, (float).5); 
		}

		public String Text {
			get{
				return label1.Text; 
			}
			set {
				label1.Text = value; 
			}
		}

	}
}

