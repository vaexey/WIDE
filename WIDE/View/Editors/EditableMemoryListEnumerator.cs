using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIDE.View.Editors
{
    public class EditableMemoryListEnumerator : IEnumerator<EditableMemoryWORD>
    {
        int address = -1;
        EditableMemoryList eml;

        public EditableMemoryListEnumerator(EditableMemoryList eml)
        {
            this.eml = eml;
        }

        public EditableMemoryWORD Current => eml[address];

        object IEnumerator.Current => eml[address];

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            address++;

            return address < eml.Count;
        }

        public void Reset()
        {
            address = -1;
        }
    }
}
