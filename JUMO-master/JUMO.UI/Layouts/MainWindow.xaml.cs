using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Microsoft.Win32;
using Sanford.Multimedia.Midi;
using midi_change;


namespace JUMO.UI.Layouts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fileName;
        OpenFileDialog openFile = new OpenFileDialog();
        Sequence sequence1 = new Sequence();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Nullable<bool> result = openFile.ShowDialog();

            if (result == true)
            {
                fileName = openFile.FileName;

                EventHandler<AsyncCompletedEventArgs> sequence1_LoadCompleted = Sequence1_LoadCompleted;
                sequence1.LoadCompleted += sequence1_LoadCompleted;

                sequence1.LoadAsync(fileName);
            }
        }

        private void Sequence1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Set_Sequence(sequence1);
        }

        private void Set_Sequence(Sequence sq)
        {
            Make_note mk = new Make_note();            
            mk.Get_Sequence(sq);        
        }
    }
}
