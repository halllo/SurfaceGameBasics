namespace SurfaceGameBasics
{
	public partial class ExampleFieldsView
	{
		public ExampleFieldsView() : base()
		{
			InitializeComponent();

			Loaded += (s, e) =>
			{
				Register(fieldsContainer);
				Register(field1, field2, field3, field4);

				Track(scatterViewItem1, scatterViewItem2);
			};
		}
	}
}
