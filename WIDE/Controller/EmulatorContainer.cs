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

        public Architecture Arch { get; set; }
        public Emulator? Emu { get; set; }

        public EmulatorContainer(Architecture arch)
        {
            Arch = arch;
            Thread = new(EmulatorThreadSub);
        }

        public void Start()
        {
            Thread.Start();
        }

        // TODO
        public void CreateEmulator()
        {

        }

        public void EmulatorThreadSub()
        {

        }
    }
}
