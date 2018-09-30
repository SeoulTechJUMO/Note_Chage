using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace JUMO.UI
{
    class MusicStart
    { 
        private static Timer timer1;

        public delegate void tCallback(object state);

        public static void Test()
        {
            Pattern pattern = Song.Current.CurrentPattern;

            foreach (JUMO.Vst.Plugin plugin in JUMO.Vst.PluginManager.Instance.Plugins)
            {
                //Debug.WriteLine($"=== Plugin: {plugin.Name} ===");

                foreach (Note note in pattern[plugin])
                {
                    //Debug.WriteLine($"Start = {note.Start}, Length = {note.Length}, Value = {note.Value}, Velocity = {note.Velocity}");
                }
            }
        }

        private void MakeTimer(long NStart, long NLength, byte NValue, byte NVelocity)
        {
            int tempo = Song.Current.Tempo;
            int ppqn = Song.Current.TimeResolution;
            double QuterNotePerSecond = 1 / (tempo / 60);
            double NotePpqn = NLength / ppqn;
            double NoteTimedouble = QuterNotePerSecond * NotePpqn * 1000;
            int NoteTime = (int)NoteTimedouble;


        }
    }
}
