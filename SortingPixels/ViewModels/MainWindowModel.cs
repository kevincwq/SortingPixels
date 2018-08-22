namespace SortingPixels.ViewModels
{
    class MainWindowModel : ViewModelBase
    {
        public ViewModelBase CurrentPage
        {
            get; set;
        }

        public MainWindowModel()
        {
            CurrentPage = new HueSortViewModel();
        }
    }
}
