using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CAT;
using CAT.Analysers.Methods;

namespace CAT.GUI.Functionalities.Analyzers.ViewModels;

public partial class AnalyzersMainVM : ObservableObject
{
    [ObservableProperty]
    private string filePath;

    [ObservableProperty]
    private string jsonAnalised;

    [ObservableProperty]
    private List<MethodInformation> methodList;

    [RelayCommand]
    private async Task PickFiles()
    {
        PickOptions options = new()
        {
            PickerTitle = "Please select a dll to be analysed"
        };

        try
        {
            FileResult result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                FilePath = result.FullPath;
            }
        }
        catch(Exception ex) { /* do something*/}
    }

    [RelayCommand]
    private /*async Task */ void Analyze()
    {
        MethodList = Processor.Analise(filePath);
    }
}
