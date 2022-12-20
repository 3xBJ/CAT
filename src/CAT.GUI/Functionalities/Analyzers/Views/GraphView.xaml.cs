using CAT.Analysers.Methods;

namespace CAT.GUI.Functionalities.Analyzers.Views;

public partial class GraphView : ContentView
{
	public GraphView()
	{
		InitializeComponent();
	}

	public static readonly BindableProperty NodesProperty = BindableProperty.Create(nameof(Nodes), typeof(List<MethodInformation>), typeof(GraphView), new List<MethodInformation>(), 
		propertyChanged: NodesChanged);

	public List<MethodInformation> Nodes
	{
		get => (List<MethodInformation>)GetValue(NodesProperty);
		set => SetValue(NodesProperty, value);
    }

	private static void NodesChanged(BindableObject bindable, object oldValue, object newValue)
	{
		if (bindable is GraphView graphView)
		{
            graphView.drawable.methodsInformation = newValue as List<MethodInformation>;
            graphView.graphCanvas.Invalidate();
        }
    }
}