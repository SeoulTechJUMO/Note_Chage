using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midi_change
{
    class Note
    {
        private byte Value;
        private long Start;
        private long Length;
        private byte Velocity;
        private long DeltaTick;

        public byte Value1 { get => Value; set => Value = value; }
        public long Start1 { get => Start; set => Start = value; }
        public long Length1 { get => Length; set => Length = value; }
        public byte Velocity1 { get => Velocity; set => Velocity = value; }
        public long DeltaTick1 { get => DeltaTick; set => DeltaTick = value; }
    }
}
