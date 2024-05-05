using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using WIDE.View.Layout;

namespace WIDE.View.Utility
{
    public class StatusView : LayoutView
    {
        private Label statusLabel;
        private Chart cpsChart;
        private ChartArea cpsChartArea;
        private int cpsCharCount = 50;
        private int cpsTimeout = 50;

        private List<double> cpsList = new();
        private List<double> currentCpsList = new();
        private DateTime lastCpsCapture = DateTime.Now;

        private System.Windows.Forms.Timer timer;

        public StatusView()
        {
            Text = "Status";

            statusLabel = new()
            {
                Font = Styles.FontMonospace(Font.Size),
                Location = new(12,12),
                AutoSize = true
            };
            Controls.Add(statusLabel);

            cpsChart = new()
            {
                Dock = DockStyle.Right,
                Width = 500,
                BackColor = Styles.ColorBackground,
            };
            Controls.Add(cpsChart);

            cpsChartArea = new();
            cpsChartArea.AxisX.LabelStyle.Enabled = false;
            cpsChartArea.AxisY.LabelStyle.Enabled = false;
            cpsChartArea.BackColor = Styles.ColorBackground;

            cpsChart.ChartAreas.Add(cpsChartArea);

            cpsChart.Series.Add(new Series());
            cpsChart.Series[0].ChartType = SeriesChartType.Line;
            cpsChart.Series[0].Color = Color.Red;
            cpsChart.Series[0].Name = "A";
            
            for(int i = 0; i < cpsCharCount; i++)
            {
                cpsChart.Series[0].Points.Add(new DataPoint(i, i));
                cpsList.Add(0);
            }

            timer = new()
            {
                Interval = 100,
                Enabled = true
            };
            timer.Tick += timer_Tick;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if(Width > Height)
            {
                cpsChart.Dock = DockStyle.Right;

                cpsChart.Width = (int)((Width > 4 * Height) ? Width * 0.2 : Width * 0.5);
            }
            else
            {
                cpsChart.Dock = DockStyle.Bottom;

                cpsChart.Height = (int)((Height > 4 * Width) ? Height * 0.2 : Height * 0.5);
            }
        }

        private void timer_Tick(object? sender, EventArgs e)
        {
            var cps = MainForm.EContainer.Emu.CPS;

            currentCpsList.Add(cps);

            if(DateTime.Now.Subtract(lastCpsCapture).TotalMilliseconds > cpsTimeout)
            {
                var avg = currentCpsList.Average();
                currentCpsList.Clear();

                cpsList.Add(avg);
                cpsList.RemoveAt(0);

                for(int i = 0; i < cpsCharCount; i++)
                {
                    cpsChart.Series[0].Points[i].YValues[0] = cpsList[i];
                }

                cpsChartArea.AxisY.Maximum = Math.Max(1, cpsList.Max());
                cpsChartArea.AxisY.Minimum = -0.001;

                cpsChart.Invalidate();
            }
            
            var unit = "";

            if(cps > 500)
            {
                cps /= 1000;
                unit = "K";

                if(cps > 500)
                {
                    cps /= 1000;
                    unit = "G";
                }
            }

            var clone = MainForm.EContainer.Emu.Arch.CommitedSignals.ToArray();

            var statusText = MainForm.EContainer.Emu.Paused ? Texts.Emulator.StatusPaused : Texts.Emulator.StatusUnpaused;
            var cpsText = Texts.Emulator.CPSLabel + string.Format(Texts.Emulator.CPSFormat, cps, unit);
            var signalText = Texts.Emulator.SignalsLabel + string.Join(", ", clone.Select(s => s.Name).ToArray());
            var instructionText = string.Format(
                Texts.Emulator.InstructionLabel,
                MainForm.EContainer.Emu.CurrentInstruction?.Name ?? "--",
                MainForm.EContainer.Emu.CycleIndex + (MainForm.EContainer.Emu.Paused ? -1 : 0)
            );

            statusLabel.Text = string.Join("\n", statusText, cpsText, signalText, instructionText);
        }
    }
}
