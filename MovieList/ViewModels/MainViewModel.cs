using System.Windows;

using Microsoft.Extensions.DependencyInjection;

using MovieList.Config;
using MovieList.Options;
using MovieList.Views;

namespace MovieList.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly App app;
        private readonly IWritableOptions<UIConfig> configOptions;

        public MainViewModel(App app, IWritableOptions<UIConfig> configOptions)
        {
            this.app = app;
            this.configOptions = configOptions;
        }

        public MainWindow MainWindow { get; set; }

        public void SetControlViewModels()
        {
            var movieListViewModel = this.app.ServiceProvider.GetRequiredService<MovieListViewModel>();
            movieListViewModel.MovieListControl = this.MainWindow.ListControl;

            this.MainWindow.ListControl.DataContext = this.MainWindow.ListControl.ViewModel = movieListViewModel;

            this.MainWindow.SettingsControl.DataContext = this.MainWindow.SettingsControl.ViewModel =
                this.app.ServiceProvider.GetRequiredService<SettingsViewModel>();

            var sidePanelViewModel = this.app.ServiceProvider.GetRequiredService<SidePanelViewModel>();
            sidePanelViewModel.SidePanelControl = this.MainWindow.SidePanelControl;

            this.MainWindow.SidePanelControl.DataContext = this.MainWindow.SidePanelControl.ViewModel =
                sidePanelViewModel;

            if (this.MainWindow.SidePanelControl.ContentContainer.Content is AddNewControl control)
            {
                control.DataContext = control.ViewModel =
                    this.app.ServiceProvider.GetRequiredService<AddNewViewModel>();
            }
        }

        public void RestoreWindowState()
        {
            this.MainWindow.Width = this.configOptions.Value.Width;
            this.MainWindow.Height = this.configOptions.Value.Height;
            this.MainWindow.Left = this.configOptions.Value.Left;
            this.MainWindow.Top = this.configOptions.Value.Top;
            this.MainWindow.WindowState = this.configOptions.Value.IsMaximized
                ? WindowState.Maximized
                : WindowState.Normal;
        }

        public void SaveWindowState()
            => this.configOptions.Update(config =>
            {
                config.Width = this.MainWindow.Width;
                config.Height = this.MainWindow.Height;
                config.Top = this.MainWindow.Top;
                config.Left = this.MainWindow.Left;
                config.IsMaximized = this.MainWindow.WindowState == WindowState.Maximized;
            });
    }
}
