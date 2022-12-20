using CAT.GUI.Functionalities.Analyzers.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CAT.GUI;

public partial class MainVM : ObservableObject
{
    [RelayCommand]
    public async Task OpenAnalyzer() => await Shell.Current.GoToAsync(nameof(AnalyzersMainPage));
}
