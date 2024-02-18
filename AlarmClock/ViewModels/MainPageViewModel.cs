using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AlarmClock.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        int count = 0;
        int count1 = 0;

        [ObservableProperty]
        public string? _counterBtn;

        [ObservableProperty]
        public string? _counterBtn1;

        [ObservableProperty]
        private TimeSpan _timeWakeUp;

        [ObservableProperty]
        private DateTime _currentTime;

        [ObservableProperty]
        private string? _clockStatus;

        [ObservableProperty]
        private bool _startButtonEnabled = true;

        [ObservableProperty]
        private bool _stopButtonEnabled = false;

        [ObservableProperty]
        private bool _timePickerVisible = false;

        [ObservableProperty]
        private bool _switchIsToggled = false;

        private IDispatcherTimer? _timer = Application.Current!.Dispatcher.CreateTimer();
        private IDispatcherTimer? _timer1;

        public MainPageViewModel()
        {
            var wakeUp = DateTime.Now;
            TimeWakeUp = wakeUp.TimeOfDay;

            CurrentTime = wakeUp;

            _timer1 = Application.Current.Dispatcher.CreateTimer();
            _timer1.Interval = TimeSpan.FromSeconds(1);
            _timer1.Tick += AlarmStart1!;
            _timer1.Start();

        }

        private void TimerCallback(object state)
        {
            // Этот метод будет вызываться при каждом тике таймера.
            // Здесь вы можете обновлять значение времени или выполнять другие действия.
            // Например:
             //YourTimeProperty = DateTime.Now;
        }



        private void AlarmStart(object sender, EventArgs e)
        {
            var wakeUpTime = TimeWakeUp;
            var currentTime = DateTime.Now;
            CurrentTime = DateTime.Now;

            if (currentTime.Hour == wakeUpTime.Hours && currentTime.Minute == wakeUpTime.Minutes && currentTime.Second == wakeUpTime.Seconds)
            {
                //StopAlarmClock();
                //ToggledCommand();

                SwitchIsToggled = false;
            }
        }


        private void AlarmStart1(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now;
        }



        [RelayCommand]
        private void ToggledCommand()
        {
            if (_timer!.IsRunning)
            {
                ClockStatus = $"Stape {CurrentTime}";
                StartButtonEnabled = true;
                StopButtonEnabled = false;

                _timer.Stop();

                return;
            }

            ClockStatus = $"Pognali {TimeWakeUp}";
            StartButtonEnabled = false;
            StopButtonEnabled = true;
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += AlarmStart!;
            _timer.Start();
        }


        [RelayCommand]
        private void StartAlarmClock()
        {
            ClockStatus = $"Pognali {TimeWakeUp}";
            StartButtonEnabled = false;
            StopButtonEnabled = true;

            //_timer = Application.Current.Dispatcher.CreateTimer();
            _timer!.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += AlarmStart!;
            _timer.Start();
        }

        [RelayCommand]
        private void StopAlarmClock()
        {
            ClockStatus = $"Stape {CurrentTime}";
            StartButtonEnabled = true;
            StopButtonEnabled = false;

            _timer!.Stop();
        }


        [RelayCommand]
        void OpenTimePickerClicked()
        {
            if (!TimePickerVisible)
            {
                var currentTime = DateTime.Now;
                TimeWakeUp = currentTime.TimeOfDay;
                TimePickerVisible = true;
                return;
            }

            TimePickerVisible = false;
        }



        [RelayCommand]
        private void OnCounterClicked()
        {
            count++;

            if (count == 1)
                CounterBtn = $"Clicked {count} time";
            else
                CounterBtn = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn);
        }

        [RelayCommand]
        private void OnCounterClicked1()
        {
            count1++;

            if (count1 == 1)
                CounterBtn1 = $"Clicked {count1} time";
            else
                CounterBtn1 = $"Clicked {count1} times";

            SemanticScreenReader.Announce(CounterBtn1);
        }

    }
}
