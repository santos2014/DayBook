using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayBook
{
    public class DatePicker : INotifyPropertyChanged
    {
        public DatePicker()
        {
            Head = DateTime.Now;
            Tail = DateTime.Now;
        }

        private DateTime _Head;
        public DateTime Head
        {
            get { return _Head; }
            set
            {
                if (value != _Head)
                {
                    _Head = value;
                    NotifyPropertyChanged("Head");
                }
            }
        }

        private DateTime _Tail;
        public DateTime Tail
        {
            get { return _Tail; }
            set
            {
                if (value != _Tail)
                {
                    _Tail = value;
                    NotifyPropertyChanged("Tail");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
