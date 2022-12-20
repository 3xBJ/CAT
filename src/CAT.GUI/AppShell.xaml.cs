using CAT.GUI.Functionalities.Analyzers.Pages;

namespace CAT.GUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AnalyzersMainPage), typeof(AnalyzersMainPage));
        }
    }
}