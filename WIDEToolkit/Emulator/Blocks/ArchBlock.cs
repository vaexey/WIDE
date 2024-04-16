using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Emulator.Blocks.Live;

namespace WIDEToolkit.Emulator.Blocks
{
    public abstract class ArchBlock
    {
        public BlockMetadata Meta { get; set; } = new();

        public abstract LiveBlock? GetLive();
        public abstract void SetLive(LiveBlock? live);

        public virtual void CreateLive(Architecture arch) { }
        public virtual void Clean() 
        {
            SetLive(null);
        }
    }

    public abstract class ArchBlock<T> : ArchBlock where T : LiveBlock
    {
        public override T? GetLive()
        {
            return Live;
        }

        public override void SetLive(LiveBlock? live)
        {
            if (live == null)
                Live = null;
            else
                Live = (T)live;
        }

        public virtual T? Live { get; set; } = null;
    }
}
