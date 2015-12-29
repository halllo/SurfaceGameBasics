﻿using Microsoft.Surface.Presentation.Input;
using System.Windows;

namespace SurfaceGameBasics
{
	public partial class TagVisual : Field.IFieldObject
	{
		public TagVisualModel ViewModel { get; private set; }

		public Point Position { get { return Center; } }

		public TagVisual()
		{
			InitializeComponent();

			DataContext = ViewModel = new TagVisualModel { View = this };

			Loaded += (s, e) => ViewModel.TagAvailable(VisualizedTag);
			Unloaded += (s, e) => ViewModel.TagUnavailable(VisualizedTag);
		}
	}

	public class TagVisualModel : ViewModel
	{
		public void TagAvailable(TagData tag)
		{
			TagManagement.Instance.Value.Register(tag.Value, this);
		}

		internal void TagUnavailable(TagData tag)
		{
			TagManagement.Instance.Value.Unregister(tag.Value, this);
		}

		public TagVisual View { get; set; }

		public long Id { get; set; }
	}
}
