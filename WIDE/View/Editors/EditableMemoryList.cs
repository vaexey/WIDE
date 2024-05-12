using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDEToolkit.Data.Binary;

namespace WIDE.View.Editors
{
    public class EditableMemoryList : IReadOnlyList<EditableMemoryWORD>
    {
        private Memory mem;

        public EditableMemoryList(Memory mem)
        {
            this.mem = mem;
        }

        public EditableMemoryWORD this[int index] => new EditableMemoryWORD(mem, index);

        public int Count => mem.GetSize();

        public IEnumerator<EditableMemoryWORD> GetEnumerator()
        {
            return new EditableMemoryListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new EditableMemoryListEnumerator(this);
        }
    }
}
