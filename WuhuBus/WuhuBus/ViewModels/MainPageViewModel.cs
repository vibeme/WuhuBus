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
            var lines = App.Db.Table<BusLine>().ToListAsync().Result;
            Lines = new ObservableCollection<BusLine>(lines);
        }
    
    }
}
