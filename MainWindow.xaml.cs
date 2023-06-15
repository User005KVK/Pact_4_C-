using Microsoft.VisualBasic;
using money;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Printing;
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
using System.Xml.Linq;

namespace budget
{
    public partial class MainWindow : Window
    {
        public static ObservableCollection<string> Types = new();
        int index;
        int selectedIndexNoteInNotes;
        int price;
        Note selectedNote;
        static ObservableCollection<Note> notes = new ObservableCollection<Note>();
        static ObservableCollection<Note> filteredNotes = new ObservableCollection<Note>();
        DateTime date;
        internal readonly ObservableCollection<object> types;

        public MainWindow()
        {
            InitializeComponent();
            date = DateTime.Today;
            Calendar.SelectedDate = date;
            notes = new ObservableCollection<Note>();
            notes = in_file.Mydeserializer<Note>("notes");
            data_grid.ItemsSource = notes;
            Types = new ObservableCollection<string>();
            Types = in_file.Mydeserializer<string>("types");
            type_list.ItemsSource = Types;
            sum();
        }

        private void create_type_Click(object sender, RoutedEventArgs e)
        {
            selectedNote = data_grid.SelectedItem as Note;
            index = data_grid.SelectedIndex;
            FindSelectedIndex();
            if (index >= 0)
            {
                index = 0;
                name.Text = notes[selectedIndexNoteInNotes].Name;
                type_list.Text = notes[selectedIndexNoteInNotes].Type;
                money_product.Text = $"{notes[selectedIndexNoteInNotes].Price}";
            }
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            Note note = new Note(name.Text, type_list.Text, Convert.ToInt32(money_product.Text), (DateTime)Calendar.SelectedDate);
            notes.Add(note);
            in_file.Serialize(notes, "notes");
            name.Text = "";
            money_product.Text = "";
            sum();
        }

        private void Calendar_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            create.IsEnabled = true;
            delete.IsEnabled = true;
            update.IsEnabled = true;
            if (notes.Count > 0)
            {
                filteredNotes = new ObservableCollection<Note>(notes.Where(note => note.Date == Calendar.SelectedDate));
                data_grid.ItemsSource = filteredNotes;
            }
        }

        private void data_grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedNote = data_grid.SelectedItem as Note;
            index = data_grid.SelectedIndex;
            FindSelectedIndex();
            if (index >= 0)
            {
                index = 0;
                name.Text = notes[selectedIndexNoteInNotes].Name;
                type_list.Text = notes[selectedIndexNoteInNotes].Type;
                money_product.Text = $"{notes[selectedIndexNoteInNotes].Price}";
            }
        }

        private void FindSelectedIndex()
        {
            selectedIndexNoteInNotes = notes.IndexOf(selectedNote);
        }

        private void sum()
        {
            price = notes.Sum(note => note.Price);
            end.Content = price;
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            index = data_grid.SelectedIndex;
            FindSelectedIndex();
            notes.RemoveAt(selectedIndexNoteInNotes);

            Note note = new Note(name.Text, type_list.Text, Convert.ToInt32(money_product.Text), (DateTime)Calendar.SelectedDate);
            notes.Add(note);
            in_file.Serialize(notes, "notes");
            name.Text = "";
            money_product.Text = "";
            sum();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            selectedNote = data_grid.SelectedItem as Note;
            index = data_grid.SelectedIndex;
            FindSelectedIndex();
            notes.RemoveAt(selectedIndexNoteInNotes);
            in_file.Serialize<Note>(notes, "notes");
        }
    }
}