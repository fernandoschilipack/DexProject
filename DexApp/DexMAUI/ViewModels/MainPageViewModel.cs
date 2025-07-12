using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DexMAUI.Models;
using DexMAUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexMAUI.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly DexUploadService _dexUploadService;

        [ObservableProperty]
        private string? statusMessage;

        public MainPageViewModel(DexUploadService dexUploadService)
        {
            _dexUploadService = dexUploadService;
        }

        [RelayCommand]
        public async Task SendDexAAsync()
        {
            StatusMessage = "Sending DEX A...";
            var result = await _dexUploadService.SendDexAsync("A", DexReports.DexA);
            StatusMessage = result;
        }

        [RelayCommand]
        public async Task SendDexBAsync()
        {
            StatusMessage = "Sending DEX B...";
            var result = await _dexUploadService.SendDexAsync("B", DexReports.DexB);
            StatusMessage = result;
        }
    }
}
