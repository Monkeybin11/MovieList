namespace MovieList.ViewModels.FormItems
{
    public abstract class MovieSeriesComponentFormItemBase : TitledFormItemBase
    {
        private int ordinalNumber;
        private int? displayNumber;
        private bool shouldDisplayNumber;

        public int OrdinalNumber
        {
            get => this.ordinalNumber;
            set
            {
                this.ordinalNumber = value;
                this.OnPropertyChanged();
            }
        }

        public int? DisplayNumber
        {
            get => this.displayNumber;
            set
            {
                this.displayNumber = value;
                this.OnPropertyChanged();
            }
        }

        public bool ShouldDisplayNumber
        {
            get => this.shouldDisplayNumber;
            set
            {
                this.shouldDisplayNumber = value;
                this.OnPropertyChanged();
            }
        }

        public abstract string Title { get; }
        public abstract string Years { get; }
        public abstract string NumberToDisplay { get; }

        public abstract void OpenForm(SidePanelViewModel sidePanel);
    }
}