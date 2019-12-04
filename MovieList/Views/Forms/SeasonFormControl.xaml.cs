using MovieList.ViewModels.Forms;

using ReactiveUI;

namespace MovieList.Views.Forms
{
    public abstract class SeasonFormControlBase : ReactiveUserControl<SeasonFormViewModel> { }

    public partial class SeasonFormControl : SeasonFormControlBase
    {
        public SeasonFormControl()
        {
            this.InitializeComponent();
        }
    }
}