using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDE.View
{
    public class Translation
    {
        public class LayoutTranslation
        {
            public string Empty { get; } = "Empty";

            public string ButtonPick { get; } = "Select windows for this view.";
            public string ButtonCollapse { get; } = "(Un)collapse this view.";

            public string PickerFormTitle { get; } = "Select windows for this view...";
        }

        public class GlobalTranslation
        {
            public string OK { get; } = "OK";
            public string Cancel { get; } = "Cancel";

            public string WindowTitle { get; } = "WIDE (alpha)";
        }

        public class EmulatorTranslation
        {
            public string BlockDescNoLive { get; } = "View will be available when an emulator is created.";
            public string BlockDescUndefined { get; } = "This block does not have a view available.";

            public string BlockDescALU { get; } = "Arithmetic logic unit";

            public string CPUViewEmpty { get; } = "Architecture does not contain\nany visible block.";

            public string MemoryCannotAdd { get; } = "This view only allows editing memory.\nTo add more bytes to memory consider editing its width in the properties menu.";
            public string MemoryCannotRemove { get; } = "This view only allows editing memory.\nTo remove bytes from memory consider editing its width in the properties menu.";

            public string StatusPaused { get; } = "Status: PAUSED";
            public string StatusUnpaused { get; } = "Status: RUNNING";
            public string CPSLabel { get; } = "CPS: ";
            public string CPSFormat { get; } = "{0:0.00} {1}cps";
            public string SignalsLabel { get; } = "Signals: ";

            public string InstructionLabel { get; } = "Instruction: {0}[{1}]";
        }


        public class ScriptTranslation
        {
            public string EmulatorAlreadyExists { get; } = "Cannot create emulator as an emulator has been already created.";

            public string EmulatorPause { get; } = "Pause emulator";
            public string EmulatorUnpause { get; } = "Unpause emulator";
            public string EmulatorStepCycle { get; } = "Step cycle";
            public string EmulatorStepInstruction { get; } = "Step instruction";

            public string ErrorEmulatorPaused { get; } = "Emulator is already paused.";
            public string ErrorEmulatorNotPaused { get; } = "Emulator is not paused yet.";
        }

        public LayoutTranslation Layout { get; } = new();
        public GlobalTranslation Global { get; } = new();
        public EmulatorTranslation Emulator { get; } = new();
        public ScriptTranslation Script { get; } = new();
    }
}
