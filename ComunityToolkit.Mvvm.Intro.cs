#r "nuget: CommunityToolkit.Mvvm, 8.2.2"

using CommunityToolkit.Mvvm.ComponentModel;

ViewModel viewModel = new();
viewModel.FullTitle.Dump("Before");

viewModel.Title = "Change";
viewModel.FullTitle.Dump("After");

partial class ViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullTitle))]
    private string title = "default title";

    public string FullTitle => $"{Title}";
}