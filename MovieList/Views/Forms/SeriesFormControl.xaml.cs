using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

using Akavache;

using MovieList.ViewModels.Forms;

using ReactiveUI;

namespace MovieList.Views.Forms
{
    public abstract class SeriesFormControlBase : ReactiveUserControl<SeriesFormViewModel> { }

    public partial class SeriesFormControl : SeriesFormControlBase
    {
        public SeriesFormControl()
        {
            this.InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.WhenAnyValue(v => v.ViewModel)
                    .BindTo(this, v => v.DataContext)
                    .DisposeWith(disposables);

                this.ViewModel.FormTitle
                    .BindTo(this, v => v.FormTitleTextBlock.Text)
                    .DisposeWith(disposables);

                this.LoadPoster();

                this.BindCommands(disposables);
                this.BindCheckboxes(disposables);
                this.BindLink(disposables);
                this.BindFields(disposables);

                this.AddValidation(disposables);
            });
        }

        private void BindCommands(CompositeDisposable disposables)
        {
            this.BindCommand(this.ViewModel, vm => vm.Save, v => v.SaveButton)
                .DisposeWith(disposables);

            this.BindCommand(this.ViewModel, vm => vm.Cancel, v => v.CancelButton)
                .DisposeWith(disposables);

            this.BindCommand(this.ViewModel, vm => vm.Close, v => v.CloseButton)
                .DisposeWith(disposables);

            this.BindCommand(this.ViewModel, vm => vm.Delete, v => v.DeleteButton)
                .DisposeWith(disposables);

            this.ViewModel.Delete.CanExecute
                .Select(canDelete => canDelete.ToVisibility())
                .BindTo(this, v => v.DeleteButton.Visibility)
                .DisposeWith(disposables);

            this.BindCommand(this.ViewModel, vm => vm.AddTitle, v => v.AddTitleButton)
                .DisposeWith(disposables);

            this.ViewModel.AddTitle.CanExecute
                .Select(canAddTitle => canAddTitle.ToVisibility())
                .BindTo(this, v => v.AddTitleButton.Visibility);

            this.BindCommand(this.ViewModel, vm => vm.AddOriginalTitle, v => v.AddOriginalTitleButton)
                .DisposeWith(disposables);

            this.ViewModel.AddOriginalTitle.CanExecute
                .Select(canAddOriginalTitle => canAddOriginalTitle.ToVisibility())
                .BindTo(this, v => v.AddOriginalTitleButton.Visibility);

            Observable.CombineLatest(this.ViewModel.Save.CanExecute, this.ViewModel.Cancel.CanExecute)
                .AnyTrue()
                .Select(canSaveOrCancel => canSaveOrCancel.ToVisibility())
                .BindTo(this, v => v.ActionPanel.Visibility);

            this.ViewModel.Save
                .Subscribe(_ => this.LoadPoster())
                .DisposeWith(disposables);
        }

        private void BindCheckboxes(CompositeDisposable disposables)
        {
            this.Bind(this.ViewModel, vm => vm.IsWatched, v => v.IsWatchedCheckBox.IsChecked)
                .DisposeWith(disposables);

            this.Bind(this.ViewModel, vm => vm.IsAnthology, v => v.IsAnthologyCheckBox.IsChecked)
                .DisposeWith(disposables);
        }

        private void BindLink(CompositeDisposable disposables)
        {
            this.OneWayBind(this.ViewModel, vm => vm.ImdbLink, v => v.ImdbLink.NavigateUri)
                .DisposeWith(disposables);

            this.OneWayBind(this.ViewModel, vm => vm.ImdbLink, v => v.ImdbLinkRun.Text)
                .DisposeWith(disposables);

            this.WhenAnyValue(v => v.ViewModel.ImdbLink)
                .Select(link => !String.IsNullOrWhiteSpace(link))
                .Select(isEmpty => isEmpty.ToVisibility())
                .BindTo(this, v => v.ImdbLinkTextBlock.Visibility)
                .DisposeWith(disposables);
        }

        private void BindFields(CompositeDisposable disposables)
        {
            this.OneWayBind(this.ViewModel, vm => vm.Titles, v => v.Titles.ItemsSource)
                .DisposeWith(disposables);

            this.OneWayBind(this.ViewModel, vm => vm.OriginalTitles, v => v.OriginalTitles.ItemsSource)
                .DisposeWith(disposables);

            this.Bind(this.ViewModel, vm => vm.Status, v => v.StatusComboBox.SelectedItem)
                .DisposeWith(disposables);

            this.Bind(this.ViewModel, vm => vm.Kind, v => v.KindComboBox.SelectedItem)
                .DisposeWith(disposables);

            this.Bind(this.ViewModel, vm => vm.ImdbLink, v => v.ImdbLinkTextBox.Text)
                .DisposeWith(disposables);

            this.Bind(this.ViewModel, vm => vm.PosterUrl, v => v.PosterUrlTextBox.Text)
                .DisposeWith(disposables);

            this.OneWayBind(this.ViewModel, vm => vm.Kinds, v => v.KindComboBox.ItemsSource)
                .DisposeWith(disposables);
        }

        private void AddValidation(CompositeDisposable disposables)
        {
            this.ImdbLinkTextBox.ValidateWith(this.ViewModel.ImdbLinkRule)
                .DisposeWith(disposables);

            this.PosterUrlTextBox.ValidateWith(this.ViewModel.PosterUrlRule)
                .DisposeWith(disposables);
        }

        private void LoadPoster()
        {
            if (!String.IsNullOrEmpty(this.ViewModel.PosterUrl))
            {
                BlobCache.UserAccount.DownloadUrl(this.ViewModel.PosterUrl, TimeSpan.FromMinutes(5))
                    .Select(data => data.AsImage())
                    .WhereNotNull()
                    .ObserveOnDispatcher()
                    .BindTo(this, v => v.Poster.Source);
            }
        }
    }
}
