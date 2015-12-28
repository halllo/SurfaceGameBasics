using Microsoft.Surface.Presentation.Controls;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SurfaceGameBasics
{
	public partial class FieldScatterView
	{
		public FieldScatterView()
		{
			InitializeComponent();

			var centerDp = DependencyPropertyDescriptor.FromProperty(ScatterViewItem.CenterProperty, typeof(ScatterViewItem));
			centerDp.AddValueChanged(scatterViewItem1, delegate
			{
				var target = GetTargetCenter();

				var itemCenter = scatterViewItem1.Center;
				var offset = target - new Vector(itemCenter.X, itemCenter.Y);
				if (offset.Length < 20)
				{
					rectTarget.Fill = Brushes.Magenta;
				}
				else
				{
					rectTarget.Fill = null;
				}
			}); 
			centerDp.AddValueChanged(scatterViewItem2, delegate
			{
				var target = GetTargetCenter();

				var itemCenter = scatterViewItem2.Center;
				var offset = target - new Vector(itemCenter.X, itemCenter.Y);
				if (offset.Length < 20)
				{
					rectTarget.Fill = Brushes.Magenta;
				}
				else
				{
					rectTarget.Fill = null;
				}
			});
		}

		private Vector GetTargetCenter()
		{
			var ch = ActualHeight;
			var cw = ActualWidth;

			var rh = rect.ActualHeight;
			var rw = rect.ActualWidth;

			var absTopRight = new Vector((cw - rw) / 2.0, (ch - rh) / 2.0);

			var top = (double)rectTarget.GetValue(Canvas.TopProperty);
			var left = (double)rectTarget.GetValue(Canvas.LeftProperty);

			var targetTopRight = absTopRight + new Vector(left, top);
			var targetCenter = targetTopRight + new Vector(rectTarget.ActualWidth / 2.0, rectTarget.ActualHeight / 2.0);

			return targetCenter;
		}
	}
}
