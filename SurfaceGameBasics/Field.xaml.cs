using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SurfaceGameBasics
{
	public partial class Field : UserControl
	{
		public interface IFieldObject
		{
			Point Position { get; }
		}

		List<IFieldObject> fieldObjects = new List<IFieldObject>();

		public Field()
		{
			InitializeComponent();
		}

		public void Enter(IFieldObject o)
		{
			fieldObjects.Add(o);
			UpdateState();
		}

		public void Leave(IFieldObject o)
		{
			fieldObjects.Remove(o);
			UpdateState();
		}

		private void UpdateState()
		{
			if (fieldObjects.Any())
			{
				Background = Brushes.Green;
			}
			else
			{
				Background = null;
			}
		}
	}
}
