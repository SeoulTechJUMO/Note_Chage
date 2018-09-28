using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;
using System.Diagnostics;
using JUMO;


namespace midi_change
{
    class Make_note
    {
        string S_tempo = "";
        int length_counter = 0;
        int ppqn;
        int tempo;
        Sequence sq = new Sequence();
        List<Channel_Message> list_cm = new List<Channel_Message>();
        List<Note> list_note = new List<Note>();

        public struct Channel_Message
        {
            public string command;
            public long AbsoluteTick;
            public long Value;
            public long velocuty;
        }
        
        public void Get_Sequence(Sequence squence)
        {
            sq = squence;
            note_make();
            PPQN_TEMPO();
        }

        private void PPQN_TEMPO()
        {
            foreach(Sanford.Multimedia.Midi.Track track in sq)
            {
                foreach(MidiEvent mev in track.Iterator())
                {
                    if(mev.MidiMessage is MetaMessage e)
                    {
                        if (e.MetaType is MetaType.Tempo)
                        {
                            if (length_counter >= 1)
                            {
                                break;
                            }
                            else
                            {
                                byte[] data = e.GetBytes();
                                int data_length = data.Length;

                                for (int i = 0; i < data.Length; i++)
                                {
                                    if (data[i] > 15)
                                    {
                                        S_tempo += Convert.ToString(data[i], 16);
                                    }
                                    else
                                    {
                                        S_tempo = S_tempo + "0" + Convert.ToString(data[i], 16);
                                    }
                                }

                                length_counter++;
                            }
                            
                        }
                    }
                }
            }
            Debug.WriteLine(S_tempo);
        }

        private void note_make()
        {
            foreach(Sanford.Multimedia.Midi.Track track in sq)
            {
                foreach(MidiEvent ev in track.Iterator())
                {
                    //Debug.WriteLine($"{ev.AbsoluteTicks}");
                    if (ev.MidiMessage is ChannelMessage cm)
                    {   
                        /*
                        Debug.Write($"{cm.Command},");
                        Debug.Write($"{ev.AbsoluteTicks},");
                        Debug.Write($"{cm.Data1},");
                        Debug.WriteLine($"{cm.Data2}");                       
                        */
                        switch (cm.Command)
                        {
                            case ChannelCommand.NoteOn:
                                Channel_Message CM_struct = new Channel_Message();
                                CM_struct.command = "NoteOn";
                                CM_struct.AbsoluteTick = ev.AbsoluteTicks;                                
                                CM_struct.Value = cm.Data1;
                                CM_struct.velocuty = cm.Data2;                               
                                list_cm.Add(CM_struct);
                                //Debug.Write($"{CM_struct.command}");
                                //Debug.WriteLine($"{CM_struct.Value}");
                                break;

                            case ChannelCommand.NoteOff:
                                Channel_Message listcm = new Channel_Message();
                                listcm = list_cm.Find(x => x.Value.Equals(cm.Data1));
                                list_cm.Remove(new Channel_Message() { Value = cm.Data1, AbsoluteTick = listcm.AbsoluteTick, command = "NoteOn", velocuty = listcm.velocuty});
                                //Debug.WriteLine("{0} {1} {2} {3}", listcm.command, listcm.AbsoluteTick, listcm.Value, listcm.velocuty);

                                Note note = 
                                    new Note((Byte)listcm.Value, (Byte)listcm.velocuty, listcm.AbsoluteTick, ev.AbsoluteTicks - listcm.AbsoluteTick);                               
                                list_note.Add(note);

                                //list_note.Sort();
                                break;

                        }
                    }
                }
            }
            
            for (int i = 0; i < list_note.Count; i++) {
                //Debug.WriteLine("{0} {1} {2} {3}", list_cm[i].command, list_cm[i].AbsoluteTick, list_cm[i].Value, list_cm[i].velocuty);
                Debug.WriteLine("Value : {0}, Start : {1}, Length : {2}, Velocity : {3}", list_note[i].Value, list_note[i].Start, list_note[i].Length, list_note[i].Velocity);
            }
            
        }

        public List<Note> return_note()
        {
            return list_note;
        }

        public static void Test()
        {
            Pattern pattern = Song.Current.CurrentPattern;

            foreach (JUMO.Vst.Plugin plugin in JUMO.Vst.PluginManager.Instance.Plugins)
            {
                Debug.WriteLine($"=== Plugin: {plugin.Name} ===");

                foreach (Note note in pattern[plugin])
                {
                    Debug.WriteLine($"Start = {note.Start}, Length = {note.Length}, Value = {note.Value}, Velocity = {note.Velocity}");
                }
            }
        }
    }
}
