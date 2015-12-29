using Microsoft.Surface.Presentation.Controls;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SurfaceGameBasics
{
	public partial class FieldsView
	{
		public FieldsView()
		{
			InitializeComponent();

			var centerDp = DependencyPropertyDescriptor.FromProperty(ScatterViewItem.CenterProperty, typeof(ScatterViewItem));
			centerDp.AddValueChanged(scatterViewItem1, delegate
			{
				CheckPositioning(rectTarget1, scatterViewItem1);
				CheckPositioning(rectTarget2, scatterViewItem1);
				CheckPositioning(rectTarget3, scatterViewItem1);
				CheckPositioning(rectTarget4, scatterViewItem1);
			});
			centerDp.AddValueChanged(scatterViewItem2, delegate
			{
				CheckPositioning(rectTarget1, scatterViewItem2);
				CheckPositioning(rectTarget2, scatterViewItem2);
				CheckPositioning(rectTarget3, scatterViewItem2);
				CheckPositioning(rectTarget4, scatterViewItem2);
			});
		}

		private void CheckPositioning(Field targetRectangle, Field.IFieldObject item)
		{
			var target = GetTargetCenter(targetRectangle);

			var itemCenter = item.Position;
			var centerDifference = target - new Vector(itemCenter.X, itemCenter.Y);
			var centerDifferenceTolerance = targetRectangle.ActualWidth / 2.0;
			if (centerDifference.LengthSquared < centerDifferenceTolerance * centerDifferenceTolerance)
			{
				targetRectangle.Enter(item);
			}
			else
			{
				targetRectangle.Leave(item);
			}
		}

		private Vector GetTargetCenter(Field target)
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
				var tagPosition = tag.View.Position;
				infoText += "\n" + tag.Id + ": position(" + tagPosition.X + ", " + tagPosition.Y + ")";
				CheckPositioning(rectTarget1, tag.View);
				CheckPositioning(rectTarget2, tag.View);
				CheckPositioning(rectTarget3, tag.View);
				CheckPositioning(rectTarget4, tag.View);
			}

			info.Content = infoText;
		}
	}
}
