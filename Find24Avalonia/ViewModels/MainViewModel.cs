using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Find24Avalonia.Models;
using System.Linq;
using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Ursa.Controls;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Find24Avalonia.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public static string Title => "Calculator for 24";
    [ObservableProperty]
    public string _input = string.Empty;
    [ObservableProperty]
    public string _result = string.Empty;

    public TopLevel? _topLevel;

    public MainViewModel(TopLevel? topLevel)
    {
        _topLevel = topLevel;
    }

    [RelayCommand]
    public async Task Calculate()
    {
        Result = string.Empty;

        string? stinger = Helper.GetStinger(Input);

        var result = new Solution().Find24Solutions(Array.ConvertAll(Input.Split(' '), int.Parse));
        if (result.Count == 0)
        {
            Result += "No solution";
        }
        else
        {
            Result += string.Join("\n", result.Distinct());
        }

        
        await MessageBox.ShowOverlayAsync(
            Result, 
            stinger is null ? "Solution Found:" : stinger);

        
    }

    [RelayCommand]
    public void Enter(string number)
    {
        if (number != "back")
        {
            Input += number;
        }
        else if (Input.Length > 0)
        {
            Input = Input[..^1];
        }
    }

    [RelayCommand]
    public async Task OpenSourceAsync()
    {
        if (_topLevel is null) return;
        var launcher = _topLevel.Launcher;
        await launcher.LaunchUriAsync(new Uri("https://github.com/SoarZhang-214/Find24Avalonia"));
    }
}
