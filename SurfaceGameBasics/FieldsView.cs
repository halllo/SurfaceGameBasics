using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SurfaceGameBasics
{
	public class FieldsView : Grid
	{
		HashSet<IField> _fields = new HashSet<IField>();
		HashSet<IFieldOccupant> _occupants = new HashSet<IFieldOccupant>();
		FrameworkElement _fieldsContainer;

		public FieldsView()
		{
			Loaded += FieldsView_Loaded;
		}

		public void Register(FrameworkElement fieldsContainer)
		{
			_fieldsContainer = fieldsContainer;
		}

		public void Register(params IField[] fields)
		{
			foreach (var field in fields)
			{
				_fields.Add(field);
				field.Activate(globalPosition: GetCenter(field));
			}
		}

		public void Unregister(params IField[] fields)
		{
			foreach (var field in fields)
			{
				_fields.Remove(field);
			}
		}

		public void Track(params IFieldOccupant[] occupants)
		{
			foreach (var occupant in occupants)
			{
				_occupants.Add(occupant);
			}
		}

		public void Untrack(params IFieldOccupant[] occupants)
		{
			foreach (var occupant in occupants)
			{
				_occupants.Remove(occupant);
			}
		}

		private void FieldsView_Loaded(object sender, RoutedEventArgs e)
		{
			var timer = new DispatcherTimer(DispatcherPriority.Background);
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += (s, e2) =>
			{
				timer.Stop();
				foreach (var occupant in _occupants)
				{
					foreach (var field in _fields)
					{
						field.HandlePositioning(occupant);
					}
				}
				timer.Start();
			};
			timer.Start();
		}

		private Vector GetCenter(IField field)
		{
			var containerSize = new Vector(ActualWidth, ActualHeight);
			var mainfieldSize = new Vector(_fieldsContainer.ActualWidth, _fieldsContainer.ActualHeight);
			var mainfieldTopRight = (containerSize - mainfieldSize) / 2.0;

			var fieldTopRight = mainfieldTopRight + field.Position.AsVector();
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

	public interface IField
	{
		Point Position { get; }
		Vector Size { get; }
		void Activate(Vector globalPosition);
		void HandlePositioning(IFieldOccupant occupant);
	}

	public interface IFieldOccupant
	{
		Point Position { get; }
	}
}
