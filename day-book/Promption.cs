using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayBook
{
    public class Promption : INotifyPropertyChanged
    {
        public enum Level
        {
            eNormal,
            eWarnning,
            eError,
            eFatal,
        }

        public void SetPromption(string text, Level lvl)
        {
            Text = text;
            Lvl = lvl;
        }

        public Level Lvl
        {
            get;
            set;
        }

        public DateTime Time
        {
            get { return DateTime.Now; }
        }

        private string _Text;
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                if (value != _Text)
                {
                    _Text = value;
                    NotifyPropertyChanged("Text");
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

    public class Promptions : ObservableCollection<Promption>
    {
    }


    public class PromptionEventArgs : EventArgs
    {
        public PromptionEventArgs(Promption p)
        {
            Item = p;
        }
        public Promption Item
        {
            get;
            set;
        }
    }
    public class PromptionMgr
    {
        public delegate void PromptionHandler(object sender, PromptionEventArgs e);
        public event PromptionHandler HandlePromption;

        private static PromptionMgr _Instance = new PromptionMgr();
        public static PromptionMgr Instance
        {
            get
            {
                return _Instance;
            }
        }

        public void Prompt(string promption, Promption.Level lvl)
        {
            var _promption = new Promption();
            _promption.SetPromption(promption, lvl);

            if (HandlePromption != null)
            {
                HandlePromption(this, new PromptionEventArgs(_promption));
            }
        }
    }
}
