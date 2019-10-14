using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Akavache;

using DynamicData;
using DynamicData.Binding;

using MovieList.Preferences;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Splat;

using static MovieList.Constants;

namespace MovieList.ViewModels
{
    public sealed class HomePageViewModel : ReactiveObject
    {
        private readonly IBlobCache store;
        private readonly ReadOnlyObservableCollection<RecentFileViewModel> recentFiles;
        private readonly SourceList<RecentFileViewModel> recentFilesSource = new SourceList<RecentFileViewModel>();

        public HomePageViewModel(IBlobCache? store = null)
        {
            this.store = store ?? Locator.Current.GetService<IBlobCache>(StoreKey);

            this.store.GetObject<UserPreferences>(PreferencesKey)
                .SelectMany(preferences => preferences.File.RecentFiles)
                .Select(file => new RecentFileViewModel(file))
                .Subscribe(recentFilesSource.Add);

            this.recentFilesSource.Connect()
                .Sort(SortExpressionComparer<RecentFileViewModel>.Descending(vm => vm.File.Closed))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out this.recentFiles)
                .DisposeMany()
                .Subscribe();

            this.WhenAnyValue(vm => vm.RecentFiles.Count)
                .Select(count => count != 0)
                .ToPropertyEx(this, vm => vm.RecentFilesPresent);

            this.CreateFile = ReactiveCommand.CreateFromTask(this.OnCreateFile);
            this.OpenFile = ReactiveCommand.CreateFromTask<string?, string?>(this.OnOpenFile);

            var canRemoveSelectedRecentFiles = recentFilesSource.Connect()
                .AutoRefresh(file => file.IsSelected)
                .ToCollection()
                .Select(files => files.Any(file => file.IsSelected));

            this.RemoveSelectedRecentFiles = ReactiveCommand.CreateFromTask(
                this.OnRemoveSelectedRecentFilesAsync, canRemoveSelectedRecentFiles);
        }

        public ReadOnlyObservableCollection<RecentFileViewModel> RecentFiles
            => this.recentFiles;

        public bool RecentFilesPresent { [ObservableAsProperty] get; }

        public ReactiveCommand<Unit, string?> CreateFile { get; }
        public ReactiveCommand<string?, string?> OpenFile { get; }
        public ReactiveCommand<Unit, Unit> RemoveSelectedRecentFiles { get; }

        private async Task<string?> OnCreateFile()
        {
            this.Log().Debug("Creating a new list.");
            string? listName = await Dialog.CreateList.Handle(Unit.Default);

            return listName is null
                ? null
                : await Dialog.SaveFile.Handle($"{listName}.{ListFileExtension}");
        }

        private async Task<string?> OnOpenFile(string? fileName)
        {
            this.Log().Debug(fileName is null ? "Opening a list." : $"Opening a list: ${fileName}.");
            return fileName ?? await Dialog.OpenFile.Handle(Unit.Default);
        }

        private async Task OnRemoveSelectedRecentFilesAsync()
        {
            var preferences = await this.store.GetObject<UserPreferences>(PreferencesKey);

            var filesToRemove = this.recentFiles
                .Where(file => file.IsSelected)
                .ToList();

            string fileNames = filesToRemove
                .Select(file => file.File.Name)
                .Aggregate((acc, file) => $"{acc}, {file}");

            this.Log().Debug($"Removing recent files: {fileNames}.");

            this.recentFilesSource.RemoveMany(filesToRemove);

            preferences.File.RecentFiles.RemoveMany(filesToRemove.Select(file => file.File));

            await this.store.InsertObject(PreferencesKey, preferences);
        }
    }
}