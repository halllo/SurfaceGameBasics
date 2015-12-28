using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace SurfaceGameBasics
{
	public class TagManagement
	{
		public class Data
		{
			public string Name { get; set; }
			public Brush Color { get; set; }
		}


		public readonly static Lazy<TagManagement> Instance = new Lazy<TagManagement>(() => new TagManagement());

		Dictionary<long, TagVisualModel> viewModels = new Dictionary<long, TagVisualModel>();
				

		public void Register(long tag, TagVisualModel viewModel)
		{
			viewModels[tag] = viewModel;
		}
	}
}
