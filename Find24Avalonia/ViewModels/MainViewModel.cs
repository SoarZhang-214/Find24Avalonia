using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Find24Avalonia.Models;
using System.Linq;
using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Find24Avalonia.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public static string Title => "Calculator for 24";
    public string Input { get; set; } = string.Empty;
    [ObservableProperty]
    public string result = string.Empty;
    public ICommand Command { get; set; }

    public MainViewModel()
    {
        Solution solution = new Solution();
        RelayCommand relayCommand = new RelayCommand(() =>
        {
            var result = solution.Find24Solutions(Array.ConvertAll(Input.Split(' '),int.Parse));
            if (result.Count == 0)
            {
                Result = "No solution";
            }
            else
            {
                Result = "Solution Found:\n" + string.Join("\n", result.Distinct());
            }
        });
        Command = relayCommand;
    }
}
