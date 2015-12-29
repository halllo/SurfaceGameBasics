using Microsoft.Surface.Presentation.Controls;
using System.Windows;

namespace SurfaceGameBasics
{
	public class ScatterViewItemFieldObject : ScatterViewItem, Field.IFieldObject
	{
		public Point Position { get { return Center; } }
	}
}
