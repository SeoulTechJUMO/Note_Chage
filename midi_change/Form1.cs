﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sanford.Multimedia.Midi;


namespace midi_change
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string fileName;
        Sequence sequence1 = new Sequence();
        Sequencer sequencer1 = new Sequencer();
        int outDeviceID = 0;
        OutputDevice outDevice;        
        //OutputStream outputStream = new OutputStream();
        

        private void button1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                sequence1.LoadCompleted += Sequence1_LoadCompleted;

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

        private void button2_Click(object sender, EventArgs e)
        {
            outDevice = new OutputDevice(outDeviceID);
            sequencer1.Position = 0;
            sequencer1.Sequence = sequence1;
            sequencer1.ChannelMessagePlayed += new EventHandler<ChannelMessageEventArgs>(HandleCnannelMessagePlayed);
            sequencer1.Start();           
        }

        private void HandleSysExMessagePlayed(object sender, SysExMessageEventArgs e)
        {
        }

        private void HandleCnannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        {
            outDevice.Send(e.Message);
        }
    }
}
