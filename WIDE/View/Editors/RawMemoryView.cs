using Be.Windows.Forms;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDE.View.Controls;
using WIDE.View.Layout;
using WIDEToolkit.Data.Binary;
using Timer = System.Windows.Forms.Timer;

namespace WIDE.View.Editors
{
    public class RawMemoryView : LayoutView
    {
        private ComboBox memoryCombo;
        private HexBox hexEditor;
        private Timer timer;
        private SimpleByteProvider memoryProvider;

        public RawMemoryView()
        {
            // TODO: translate
            Text = "Memory (hex)";
            TabImage = Resources.Database;

            memoryCombo = new()
            {
                Dock = DockStyle.Top,
                DropDownStyle = ComboBoxStyle.DropDownList,
            };

            memoryCombo.Items.Add("(not selected)");
            memoryCombo.SelectedIndex = 0;

            Controls.Add(memoryCombo);

            hexEditor = new()
            {
                Top = memoryCombo.Bottom,
                Width = Width,
                Height = Height - memoryCombo.Top,

                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom,
            };
            Controls.Add(hexEditor);

            timer = new()
            {
                Enabled = true,
                Interval = 100
            };
            timer.Tick += timer_Tick;

            HexEditorInitializer.Init(hexEditor);

            memoryProvider = new( new byte[] { });
            memoryProvider.Write += memoryProvider_Write;

            hexEditor.ByteProvider = memoryProvider;
        }

        protected override void OnMainFormReady(EventArgs e)
        {
            base.OnMainFormReady(e);
        }

        private void EContainer_LiveCreated(object? sender, EventArgs e)
        {
            memoryCombo.Items.Clear();
            memoryCombo.Items.Add("(not selected)");

            memoryCombo.Items.AddRange(
                MainForm.EContainer.Arch.MemoryBlock.Live.Divisions
                .Select(m => m.DivisionName).ToArray());

            memoryCombo.SelectedIndex = memoryCombo.Items.Count > 1 ? 1 : 0;
        }

        private void timer_Tick(object? sender, EventArgs e)
        {
            UpdateMemoryView();
        }

        private void UpdateMemoryView()
        {
            var emu = MainForm.EContainer.Emu;
            var memIdx = memoryCombo.SelectedIndex - 1;

            if (memIdx > -1)
            {
                var mem = (SingleMemory)emu.Arch.MemoryBlock.Live.Divisions[memIdx];

                var bytes = mem.RawMemory.ToBytes();

                var oldSize = memoryProvider.Length;

                memoryProvider.Bytes = bytes.ToList();

                if (oldSize != bytes.Length)
                {
                    memoryProvider.OnLengthChanged();
                }

                memoryProvider.OnChanged();

                hexEditor.Invalidate();
            }
            else
            {
                memoryProvider.Bytes.Clear();

                memoryProvider.OnLengthChanged();
                memoryProvider.OnChanged();

                hexEditor.Invalidate();
            }
        }

        private void memoryProvider_Write(object? sender, Tuple<long, byte> e)
        {
            var emu = MainForm.EContainer.Emu;
            var memIdx = memoryCombo.SelectedIndex - 1;

            if (memIdx > -1)
            {
                var mem = (SingleMemory)emu.Arch.MemoryBlock.Live.Divisions[memIdx];

                MainForm.EContainer.Invoke(() =>
                {
                    mem.RawMemory.Write(WORD.FromUInt64(e.Item2, 8), 8 * (int)e.Item1);
                });

                MainForm.EContainer.WaitUntilInvokeCompleted();

                UpdateMemoryView();
            }
        }
    }
}
