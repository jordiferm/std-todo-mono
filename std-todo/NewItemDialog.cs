using System;

namespace stdtodo
{
	public partial class NewItemDialog : Gtk.Dialog
	{
		public NewItemDialog ()
		{
			this.Build ();
		}

		public String ItemText {
			get { return entry1.Text; }
		}
	}
}

