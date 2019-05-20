using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MovieList.ViewModels.FormItems
{
    public abstract class FormItemBase : ViewModelBase
    {
        private bool areChangesPresent;

        public bool AreChangesPresent
        {
            get => this.areChangesPresent;
            protected set
            {
                this.areChangesPresent = value;
                this.OnPropertyChanged();
            }
        }

        protected bool IsInitialized { get; set; }

        protected abstract IEnumerable<(Func<object?> CurrentValueProvider, Func<object?> OriginalValueProvider)> Values { get; }

        public abstract void WriteChanges();
        public abstract void RevertChanges();

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (this.IsInitialized)
            {
                base.OnPropertyChanged(propertyName);

                if (propertyName != nameof(AreChangesPresent))
                {
                    this.CheckIfValuesChanged();
                }
            }
        }

        private void CheckIfValuesChanged()
            => this.AreChangesPresent = this.Values.Any(v =>
                !this.AreValuesEqual(v.CurrentValueProvider(), v.OriginalValueProvider()));

        private bool AreValuesEqual(object? a, object? b)
            => !(a == null && b != null) &&
                !(a != null && b == null) &&
                ((a == null && b == null) ||
                 (a is IEnumerable<object> e1 && b is IEnumerable<object> e2 && e1.SequenceEqual(e2)) ||
                 a!.Equals(b));
    }
}