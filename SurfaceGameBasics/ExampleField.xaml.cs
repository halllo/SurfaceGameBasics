using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SurfaceGameBasics
{
	public partial class ExampleField : UserControl, IField
	{
		HashSet<IFieldOccupant> _fieldOccupants = new HashSet<IFieldOccupant>();
		Vector _globalPosition;
		double _globalPositionDifferenceToleranceSquared;

		public ExampleField()
		{
			InitializeComponent();
		}

		public Point Position { get { return new Point((double)GetValue(Canvas.LeftProperty), (double)GetValue(Canvas.TopProperty)); } }
		public Vector Size { get { return new Vector(ActualWidth, ActualHeight); } }

		public void Activate(Vector globalPosition)
		{
			_globalPosition = globalPosition;

			var globalPositionDifferenceTolerance = base.ActualWidth / 2.0;
			_globalPositionDifferenceToleranceSquared = globalPositionDifferenceTolerance * globalPositionDifferenceTolerance;
		}

		public void HandlePositioning(IFieldOccupant occupant)
		{
			var itemCenter = occupant.Position;
			var centerDifference = _globalPosition - new Vector(itemCenter.X, itemCenter.Y);
			var centerDifferenceLengthSquared = centerDifference.LengthSquared;

			if (centerDifferenceLengthSquared < _globalPositionDifferenceToleranceSquared)
			{
				Occupy(occupant);
			}
			else
			{
				Leave(occupant);
			}
		}

		private void Occupy(IFieldOccupant occupant)
		{
			_fieldOccupants.Add(occupant);
			UpdateState();
		}

		private void Leave(IFieldOccupant occupant)
		{
			_fieldOccupants.Remove(occupant);
			UpdateState();
		}

		private void UpdateState()
		{
			if (_fieldOccupants.Any())
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
