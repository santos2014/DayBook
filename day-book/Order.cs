using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayBook
{
    public class Order : INotifyPropertyChanged
    {
        private int _AccountID;
        public int AccountID
        {
            get { return _AccountID; }
            set
            {
                if (value != _AccountID)
                {
                    _AccountID = value;
                    NotifyPropertyChanged("AccountID");
                }
            }
        }

        private int _HowMuch;
        public int HowMuch
        {
            get { return _HowMuch; }
            set
            {
                if (value != _HowMuch)
                {
                    _HowMuch = value;
                    NotifyPropertyChanged("HowMuch");
                }
            }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                if (value != _Description)
                {
                    _Description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        private SubCategory _Category = new SubCategory();
        public SubCategory Category
        {
            get { return _Category; }
            set
            {
                if (value != _Category)
                {
                    _Category = value;
                    NotifyPropertyChanged("Category");
                }
            }
        }

        //private byte _Category;
        //public byte Category
        //{
        //    get { return _Category; }
        //    set
        //    {
        //        if (value != _Category)
        //        {
        //            _AccountID = value;
        //            NotifyPropertyChanged("Category");
        //        }
        //    }
        //}

        public void Reset()
        {
            AccountID = 0;
            HowMuch = 0;
            Description = "";
            Category.Reset();
        }

        public static void AddOrder(Order order)
        {
            if (null == order)
            {
                PromptionMgr.Instance.Prompt("订单为空，插入无效", Promption.Level.eWarnning);
                return;
            }

            string sql = @"insert into detail(AccountID, HowMuch, Description, Date, Type)";
            sql += @" values(?AccountID, ?Howmuch, ?Description, ?Date, ?Type)";

            MySqlParameter[] parameters = new MySqlParameter[5];
            parameters[0] = new MySqlParameter("?AccountID", MySqlDbType.Int32);
            parameters[0].Value = order.AccountID;
            parameters[1] = new MySqlParameter("?HowMuch", MySqlDbType.Int16);
            parameters[1].Value = order.HowMuch;
            parameters[2] = new MySqlParameter("?Description", MySqlDbType.VarChar, 50);
            parameters[2].Value = order.Description;
            parameters[3] = new MySqlParameter("?Date", MySqlDbType.DateTime);
            parameters[3].Value = DateTime.Now;
            parameters[4] = new MySqlParameter("?Type", MySqlDbType.Byte);
            parameters[4].Value = order.Category.MainCategroy;

            if (db.MySqlHelper.ExecuteNonQuery(CommandType.Text, sql, parameters) > 0)
            {
                PromptionMgr.Instance.Prompt("添加订单成功", Promption.Level.eNormal);
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
