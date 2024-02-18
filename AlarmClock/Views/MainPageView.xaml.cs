using AlarmClock.ViewModels;

namespace AlarmClock
{
    public partial class MainPageView : ContentPage
    {
        public MainPageView(MainPageViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
        }        
    }
}
