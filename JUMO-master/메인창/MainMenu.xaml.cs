﻿using System;
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
using System.Windows.Threading;
using System.Windows.Diagnostics;
using System.Timers;
using Microsoft.Win32;
using Sanford.Multimedia.Midi;
using midi_change;
using System.ComponentModel;

namespace 메인창
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainMenu : Window
    {      
        double orginalWidth, originalHeight;

        ScaleTransform scale = new ScaleTransform();

        OpenFileDialog openFile = new OpenFileDialog();
        Sequence sequence1 = new Sequence();

        string fileName;

        public MainMenu()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Window1_Loaded);
        }

        void Window1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChangeSize(e.NewSize.Width, e.NewSize.Height);
        }

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            orginalWidth = this.Width;
            originalHeight = this.Height;

            if (this.WindowState == WindowState.Maximized)
            {
                ChangeSize(this.ActualWidth, this.ActualHeight);
            }
            this.SizeChanged += new SizeChangedEventHandler(Window1_SizeChanged);
            
        }

        private void ChangeSize(double width, double height)
        {
            scale.ScaleX = width / orginalWidth;
            scale.ScaleY = height / originalHeight;

            FrameworkElement rootElement = this.Content as FrameworkElement;

            rootElement.LayoutTransform = scale;
        }

        private void PlayListOpen_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void Mixser_Click(object sender, RoutedEventArgs e)
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

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow winFileOpen = new SettingWindow();
            winFileOpen.Owner = this;
            if (winFileOpen.ShowDialog() == true)
            {

            }
        }
    }
}
