using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIDE.View;
using WIDE.View.Utility;

namespace WIDE.Controller
{
    public class ScriptEngine
    {
        protected Dictionary<string, Action<string>> commands = new();

        public void Execute(string line)
        {
            if (line.Contains(";"))
            {
                foreach (var cmd in line.Split(';'))
                    Execute(cmd);

                return;
            }

            var command = line.Split(" ")[0];

            if(!commands.ContainsKey(command))
            {
                return;
            }

            try
            {
                commands[command](line);
            }
            catch(ScriptException ex)
            {
                //MessageBox.Show(ex.Message, Texts.Global.WindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                var msf = new MessageForm("Scripts", ex.Message, ex.Error);

                msf.ShowDialog();

                msf.Dispose();
            }
        }

        public void Register(string command, Action<string> action)
        {
            commands[command] = action;
        }
        public void Register(string command, Action action)
        {
            commands[command] = (string s) =>
            {
                action();
            };
        }
    }
}
