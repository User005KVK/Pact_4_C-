using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using budget;

namespace money
{
    public partial class small_win : Window
    {
        private MainWindow mainWindow;

        public small_win(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            string name = new_type.Text;
            mainWindow.types.Add(name);
            in_file.Serialize(mainWindow.types, "types");
            new_type.Text = "";
            mainWindow.type_list.ItemsSource = mainWindow.types; // Обновление списка типов в главном окне
            Close();
        }
    }
}