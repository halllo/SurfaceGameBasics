using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SurfaceGameBasics
{
	public partial class ExampleField : UserControl, IField
	{
		ConcurrentDictionary<IFieldOccupant, byte> _fieldOccupants = new ConcurrentDictionary<IFieldOccupant, byte>();

		public ExampleField()
		{
			InitializeComponent();
		}

		public Point Position { get { return new Point((double)GetValue(Canvas.LeftProperty), (double)GetValue(Canvas.TopProperty)); } }
		public Vector Size { get { return new Vector(ActualWidth, ActualHeight); } }
		public ReadOnlyCollection<IFieldOccupant> Occupants { get { return _fieldOccupants.Keys.ToList().AsReadOnly(); } }

		public event Action<IFieldOccupant> Occupied;
		private void RaiseOccupied(IFieldOccupant occupant)
		{
			var h = Occupied;
			if (h != null) h(occupant);
		}

		public event Action<IFieldOccupant> Yielded;
		private void RaiseYielded(IFieldOccupant occupant)
		{
			var h = Yielded;
			if (h != null) h(occupant);
		}

		public void Occupy(IFieldOccupant occupant)
		{
			_fieldOccupants.TryAdd(occupant, byte.MinValue);
			UpdateState();
			RaiseOccupied(occupant);
		}

		public void Yield(IFieldOccupant occupant)
		{
			var value = byte.MinValue;
			_fieldOccupants.TryRemove(occupant, out value);
			UpdateState();
			RaiseYielded(occupant);
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
