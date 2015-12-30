namespace SurfaceGameBasics
{
	public partial class ExampleFieldsView
	{
		public ExampleFieldsView()
		{
			InitializeComponent();

			Loaded += (s, e) =>
			{
				Register(fieldsContainer);
				Register(field1, field2, field3, field4);

				//((IField)field1).Occupied += occupant => { ((IField)field1). };


				Track(scatterViewItem1, scatterViewItem2);

				TagManagement.Instance.Value.TagRegistered += tag => Track(tag.Visual);
				TagManagement.Instance.Value.TagUnregistered += tag => Untrack(tag.Visual);
			};
		}
	}
}
