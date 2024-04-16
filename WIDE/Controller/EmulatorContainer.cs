using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator;

namespace WIDE.Controller
{
    public class EmulatorContainer
    {
        public Thread Thread { get; }

        public Emulator Emu { get; set; }

        public EmulatorContainer(Emulator emu)
        {
            Emu = emu;
            Thread = new(EmulatorThreadSub);
        }

        public void Start()
        {
            Thread.Start();
        }

        public void EmulatorThreadSub()
        {

        }
    }
}
