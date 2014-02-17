using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DayBook
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            CategoriesLoader.Load();
            SubCategoriesLoader.Load();
            DataContext = this;

            PromptionMgr.Instance.HandlePromption += HandlePromption;
            InitializeComponent();
        }

        public List<Category> Categories
        {
            get
            {
                return CategoriesLoader.Load();
            }
        }

        public List<SubCategory> SubCategories
        {
            get
            {
                return SubCategoriesLoader.Load();
            }
        }

        private OrderValidation _OrderValidation = new OrderValidation();
        public OrderValidation OrderValidation
        {
            get { return _OrderValidation; }
        }

        private Promptions _Promptsions = new Promptions();
        public Promptions Promptions
        {
            get { return _Promptsions; }
        }

        private Order _CurOrder = new Order();
        public Order CurOrder
        {
            get { return _CurOrder; }
        }

        private DatePicker _DatePicker = new DatePicker();
        public DatePicker DatePicker
        {
            get { return _DatePicker; }
        }
        
        private void TB_howmuch_ValidationError(object sender, ValidationErrorEventArgs e)
        {
            OrderValidation.Message = e.Error.ErrorContent.ToString();
        }

        private void CB_SubCategroy_ValidationError(object sender, ValidationErrorEventArgs e)
        {
            OrderValidation.Message = e.Error.ErrorContent.ToString();
        } 

        private void HandlePromption(object sender, PromptionEventArgs e)
        {
            Promptions.Add(e.Item);
        }

        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var selected = (sender as ComboBox).SelectedItem as SubCategory;
        //    TB_IncomeOrExpand.Text = Category.GetDescription(selected.MainCategroy);

        //    CurOrder.Category = selected.ID;
        //}

        private void Btn_search_Click(object sender, RoutedEventArgs e)
        {
            // query
            string sql = "select * from detail";
            var dt = db.MySqlHelper.GetDataTable(sql);
            if (dt != null)
            {
                DG_detail.ItemsSource = dt.DefaultView;
            }
        }

        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (OrderValidation.Valid)
                Order.AddOrder(CurOrder);
            else
                CurOrder.Reset();

            OrderValidation.Reset();
        }

    }
}
