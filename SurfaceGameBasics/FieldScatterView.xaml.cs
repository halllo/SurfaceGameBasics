using Microsoft.Surface.Presentation.Controls;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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
				CheckPositioning(rectTarget1, scatterViewItem1.Center);
				CheckPositioning(rectTarget2, scatterViewItem1.Center);
				CheckPositioning(rectTarget3, scatterViewItem1.Center);
				CheckPositioning(rectTarget4, scatterViewItem1.Center);
			});
			centerDp.AddValueChanged(scatterViewItem2, delegate
			{
				CheckPositioning(rectTarget1, scatterViewItem2.Center);
				CheckPositioning(rectTarget2, scatterViewItem2.Center);
				CheckPositioning(rectTarget3, scatterViewItem2.Center);
				CheckPositioning(rectTarget4, scatterViewItem2.Center);
			});
		}

		private void CheckPositioning(Rectangle targetRectangle, Point itemCenter)
		{
			var target = GetTargetCenter(targetRectangle);

			var centerDifference = target - new Vector(itemCenter.X, itemCenter.Y);
			var centerDifferenceTolerance = targetRectangle.ActualWidth / 2.0;
			if (centerDifference.LengthSquared < centerDifferenceTolerance * centerDifferenceTolerance)
			{
				targetRectangle.Fill = Brushes.Blue;
			}
			else
			{
				targetRectangle.Fill = null;
			}
		}

		private Vector GetTargetCenter(Rectangle target)
		{
			var containerSize = new Vector(ActualWidth, ActualHeight);
			var borderSize = new Vector(rect.ActualWidth, rect.ActualHeight);
			var borderTopRight = (containerSize - borderSize) / 2.0;

			var targetRelativePosition = new Vector((double)target.GetValue(Canvas.LeftProperty), (double)target.GetValue(Canvas.TopProperty));
			var targetSize = new Vector(target.ActualWidth, target.ActualHeight);

			var targetTopRight = borderTopRight + targetRelativePosition;
			var targetCenter = targetTopRight + targetSize / 2.0;

			return targetCenter;
		}

		private void TagPosition_Click(object sender, RoutedEventArgs e)
		{
			var tags = TagManagement.Instance.Value.All;
			var infoText = tags.Count() + " tags";

			foreach (var tag in tags)
			{
				var tagPosition = tag.Position;
				infoText += "\n" + tag.Id + ": position(" + tagPosition.X + ", " + tagPosition.Y + ")";
				CheckPositioning(rectTarget1, tagPosition);
				CheckPositioning(rectTarget2, tagPosition);
				CheckPositioning(rectTarget3, tagPosition);
				CheckPositioning(rectTarget4, tagPosition);
			}

			info.Content = infoText;
		}
	}
}
