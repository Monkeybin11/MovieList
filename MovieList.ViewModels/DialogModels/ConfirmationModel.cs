using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MovieList.DialogModels
{
    public sealed class ConfirmationModel : ReactiveObject
    {
        public ConfirmationModel(string message, string confirmButtonText, string cancelButtonText)
        {
            this.Message = message;
            this.ConfirmButtonText = confirmButtonText;
            this.CancelButtonText = cancelButtonText;
        }

        [Reactive]
        public string Message { get; set; }

        [Reactive]
        public string ConfirmButtonText { get; set; }

        [Reactive]
        public string CancelButtonText { get; set; }
    }
}
