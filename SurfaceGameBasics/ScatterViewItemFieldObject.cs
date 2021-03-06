﻿using Microsoft.Surface.Presentation.Controls;
using System.Windows;

namespace SurfaceGameBasics
{
	public class ScatterViewItemFieldObject : ScatterViewItem, IFieldOccupant
	{
		public Point Position { get { return Center; } }
		public double OrientationAngle { get { return Orientation; } }

		string IFieldOccupant.Tag
		{
			get { return Tag.ToString(); }
			set { Tag = value; }
		}
	}
}
