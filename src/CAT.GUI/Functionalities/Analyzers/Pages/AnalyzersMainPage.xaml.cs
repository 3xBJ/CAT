using CAT.GUI.Functionalities.Analyzers.ViewModels;

namespace CAT.GUI.Functionalities.Analyzers.Pages;

public partial class AnalyzersMainPage : ContentPage
{
	public AnalyzersMainPage(AnalyzersMainVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}