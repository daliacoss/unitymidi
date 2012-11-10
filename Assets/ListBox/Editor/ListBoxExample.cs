using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic; //For List<T>.

public class ListBoxExample : EditorWindow
{
	public ListBox _myListBox = new ListBox();
	
	[MenuItem ("Window/List Box Example")]
	public static void Launch()
	{
		GetWindow (typeof (ListBoxExample)).Show ();

	}  
	void OnEnable ()
	{
		_myListBox.AddEntry("Item 1");
		_myListBox.AddEntry("Item 2");
		_myListBox.AddEntry("Item 3");
		_myListBox.AddEntry("Item 4");	
		_myListBox.AddEntry("Item 5");	
	}
	void Update ()
	{
		Repaint();	
	}
	public void OnGUI ()
	{
		_myListBox.Draw(new Rect(10, 10, 120, 320), 18, Color.white, Color.blue);
		if(_myListBox.selectedEntry != null)
		{
			GUI.Label(new Rect(140, 20, 300, 30), "Selected item: " + _myListBox.selectedEntry.name);
		}
	}
}
