using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SurfaceGameBasics
{
	public class FieldsView : Grid
	{
		FrameworkElement _fieldsContainer;
		ConcurrentDictionary<IField, FieldPosition> _fields = new ConcurrentDictionary<IField, FieldPosition>();
		ConcurrentDictionary<IFieldOccupant, byte> _occupants = new ConcurrentDictionary<IFieldOccupant, byte>();

		public FieldsView()
		{
			Loaded += StartBackgroundPositioning;
		}

		public void Register(FrameworkElement fieldsContainer)
		{
			_fieldsContainer = fieldsContainer;
		}

		public void Register(params IField[] fields)
		{
			foreach (var field in fields)
			{
				var globalPosition = GetCenter(field);
				var globalPositionDifferenceTolerance = field.Size.X / 2.0;

				_fields.TryAdd(field, new FieldPosition());

				var fieldPositioning = _fields[field];
				fieldPositioning.GlobalPosition = globalPosition;
				fieldPositioning.GlobalPositionDifferenceToleranceSquared = globalPositionDifferenceTolerance * globalPositionDifferenceTolerance;
			}
		}

		public void Unregister(params IField[] fields)
		{
			foreach (var field in fields)
			{
				FieldPosition value;
				_fields.TryRemove(field, out value);
			}
		}

		public void Track(params IFieldOccupant[] occupants)
		{
			foreach (var occupant in occupants)
			{
				_occupants.TryAdd(occupant, byte.MinValue);
			}
		}

		public void Untrack(params IFieldOccupant[] occupants)
		{
			foreach (var occupant in occupants)
			{
				var value = byte.MinValue;
				_occupants.TryRemove(occupant, out value);
			}
		}

		private void StartBackgroundPositioning(object sender, RoutedEventArgs e)
		{
			var timer = new DispatcherTimer(DispatcherPriority.Background);
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += (s, e2) =>
			{
				timer.Stop();
				foreach (var occupant in _occupants.Keys)
				{
					foreach (var field in _fields)
					{
						if (field.Value.Contains(occupant.Position.AsVector()))
						{
							field.Key.Occupy(occupant);
						}
						else
						{
							field.Key.Yield(occupant);
						}
					}
				}
				timer.Start();
			};
			timer.Start();
		}

		private Vector GetCenter(IField field)
		{
			var containerSize = new Vector(ActualWidth, ActualHeight);
			var fieldsContainerSize = new Vector(_fieldsContainer.ActualWidth, _fieldsContainer.ActualHeight);
			var fieldsContainerTopRight = (containerSize - fieldsContainerSize) / 2.0;

			var fieldTopRight = fieldsContainerTopRight + field.Position.AsVector();
			var fieldCenter = fieldTopRight + field.Size / 2.0;

			return fieldCenter;
		}
	}

	public static class VectorConverter
	{
		public static Vector AsVector(this Point point)
		{
			return new Vector(point.X, point.Y);
		}
	}

	public class FieldPosition
	{
		public Vector GlobalPosition { get; set; }
		public double GlobalPositionDifferenceToleranceSquared { get; set; }

		public bool Contains(Vector position)
		{
			var centerDifference = GlobalPosition - position;
			return centerDifference.LengthSquared < GlobalPositionDifferenceToleranceSquared;
		}
	}

	public interface IField
	{
		string Tag { get; set; }
		Point Position { get; }
		Vector Size { get; }
		ReadOnlyCollection<IFieldOccupant> Occupants { get; }

		void Occupy(IFieldOccupant occupant);
		event Action<IFieldOccupant> Occupied;

		void Yield(IFieldOccupant occupant);
		event Action<IFieldOccupant> Yielded;
	}

	public interface IFieldOccupant
	{
		string Tag { get; set; }
		Point Position { get; }
	}
}
