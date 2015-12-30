using System.Windows;

namespace SurfaceGameBasics.Test
{
	public class TestTagVisual : TagVisual
	{
		public override Point Position
		{
			get
			{
				return ((Microsoft.Surface.Presentation.Controls.ScatterViewItem)Parent).Center;
			}
		}
	}
}
