using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace TestApplicationButtonsItemsControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeButtonModel();
            InitializeComponent();

            DataContext = this;
        }

        public ObservableCollection<Button> Buttons { get; set; }

        public ObservableCollection<Button> Buttons2 { get; set; }

        private void InitializeButtonModel()
        {
            Buttons = new ObservableCollection<Button>();
            Buttons2 = new ObservableCollection<Button>();

            Buttons.Add(CreateTestButton("B1"));
            Buttons.Add(CreateTestButton("B2"));

            Buttons2.Add(CreateTestButton("B3"));
            Buttons2.Add(CreateTestButton("B4"));
        }

        private static Button CreateTestButton(string buttonName)
        {
            Button button = new Button();
            button.Height = 40;
            button.Content = buttonName;
            return button;
        }

        private int _i;
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Buttons.Add(CreateTestButton(_i.ToString()));
            _i++;
            DoPropertyChanged("Buttons");
        }

        private int _i2;
        private void ButtonAdd2_Click(object sender, RoutedEventArgs e)
        {
            Buttons2.Add(CreateTestButton(_i2.ToString()));
            _i2++;
            DoPropertyChanged("Buttons2");
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            Buttons.RemoveAt(Buttons.Count - 1);
        }

        private void ButtonRemove2_Click(object sender, RoutedEventArgs e)
        {
            Buttons2.RemoveAt(Buttons.Count - 1);
        }

        #region INotifyPropertyChanged Members

        public void DoPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
