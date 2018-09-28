using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;
using System.Diagnostics;

namespace midi_change
{
    class Make_note
    {
        int ppqn;
        int tempo;
        Sequence sq = new Sequence();
        List<Channel_Message> list_cm = new List<Channel_Message>();
        List<Note> list_note = new List<Note>();
        Note note = new Note();


        public struct Channel_Message
        {
            public string command;
            public long AbsoluteTick;
            public long deltatick;
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
            foreach(Track track in sq)
            {
                foreach(MidiEvent mev in track.Iterator())
                {
                    if(mev.MidiMessage is MetaMessage e)
                    {
                        if (e.MetaType is MetaType.Tempo)
                        {
                            tempo = e.GetHashCode();
                            byte[] data = e.GetBytes();
                            Debug.WriteLine(e.MetaType);
                            Debug.WriteLine(Convert.ToString(tempo, 16));
                            for (int i = 0; i < data.Length; i++)
                            {
                                Debug.WriteLine(Convert.ToString(data[i], 16));
                            }
                            Debug.WriteLine("");
                        }
                    }
                }
            }
        }

        private void note_make()
        {
            foreach(Track track in sq)
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
                                CM_struct.deltatick = ev.DeltaTicks;
                                CM_struct.Value = cm.Data1;
                                CM_struct.velocuty = cm.Data2;                               
                                list_cm.Add(CM_struct);
                                //Debug.Write($"{CM_struct.command}");
                                //Debug.WriteLine($"{CM_struct.Value}");
                                break;

                            case ChannelCommand.NoteOff:
                                Note note = new Note();
                                Channel_Message listcm = new Channel_Message();
                                listcm = list_cm.Find(x => x.Value.Equals(cm.Data1));
                                list_cm.Remove(new Channel_Message() { Value = cm.Data1, AbsoluteTick = listcm.AbsoluteTick, command = "NoteOn", velocuty = listcm.velocuty, deltatick = listcm.deltatick });
                                //Debug.WriteLine("{0} {1} {2} {3}", listcm.command, listcm.AbsoluteTick, listcm.Value, listcm.velocuty);

                                note.Value1 = (Byte)listcm.Value;
                                note.Start1 = listcm.AbsoluteTick;
                                note.DeltaTick1 = listcm.deltatick;
                                note.Length1 = ev.AbsoluteTicks - listcm.AbsoluteTick;
                                note.Velocity1 = (Byte)listcm.velocuty;
                                list_note.Add(note);

                                //list_note.Sort();
                                break;

                        }
                    }
                }
            }
            
            for (int i = 0; i < list_note.Count; i++) {
                //Debug.WriteLine("{0} {1} {2} {3}", list_cm[i].command, list_cm[i].AbsoluteTick, list_cm[i].Value, list_cm[i].velocuty);
                Debug.WriteLine("Value : {0}, Start : {1}, Length : {2}, Velocity : {3}, DeltaTick : {4}", list_note[i].Value1, list_note[i].Start1, list_note[i].Length1, list_note[i].Velocity1, list_note[i].DeltaTick1);
            }
            
        }

        public List<Note> return_note()
        {
            return list_note;
        }

    }
}
