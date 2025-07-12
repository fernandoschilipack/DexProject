namespace DexMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (App.Current is { })
                App.Current.UserAppTheme = AppTheme.Light;
            MainPage = new AppShell();
        }
    }
}
