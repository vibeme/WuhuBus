using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WuhuBus.Annotations;
using WuhuBus.Models;

namespace WuhuBus.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<BusLine> _lines;
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<BusLine> Lines
        {
            get => _lines;
            private set
            {
                if (Equals(value, _lines)) return;
                _lines = value;
                OnPropertyChanged(nameof(Lines));
            }
        }

        public void LoadLines()
        {
            var task = App.Db.Table<BusLine>().ToListAsync();
            task.Wait();
            var lines = task.Result;

            Lines = new ObservableCollection<BusLine>(lines);
        }
    
    }
}
