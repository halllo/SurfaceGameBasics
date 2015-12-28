using Microsoft.Surface.Presentation.Input;
using System.Windows;
using System.Windows.Media;

namespace SurfaceGameBasics
{
	public partial class TagVisual
	{
		public TagVisualModel ViewModel { get; private set; }

		public TagVisual()
		{
			InitializeComponent();

			DataContext = ViewModel = new TagVisualModel();

			Loaded += (s, e) => ViewModel.TagAvailable(VisualizedTag);
		}
	}

	public class TagVisualModel : ViewModel
	{
		public void TagAvailable(TagData tag)
		{
			TagManagement.Instance.Value.Register(tag.Value, this);
		}
	}
}
