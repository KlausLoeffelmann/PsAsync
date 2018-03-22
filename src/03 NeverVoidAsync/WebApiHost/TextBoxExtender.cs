using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiHost
{
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public static class TextBoxExtender
    {
        public static void Write(this TextBoxBase control, string Text, bool DontBlock = false)
        {
            Action writer = () => {
                control.AppendText(Text);
            };

            if (control.InvokeRequired)
            {
                if (DontBlock)
                {
                    control.BeginInvoke(writer);
                }
                else
                {
                    control.Invoke(writer);
                }
            }
            else
            {
                writer.Invoke();
            }
        }

        public static void WriteLine(this TextBoxBase control, string Text, bool DontBlock = false)
        {
            Action writer = () => {
                control.AppendText(Text + Environment.NewLine);
                control.SelectionStart = control.Text.Length - 1;
                control.SelectionLength = 0;
                control.ScrollToCaret();
            };

            if (control.InvokeRequired)
            {
                if (DontBlock)
                {
                    control.BeginInvoke(writer);
                }
                else
                {
                    control.Invoke(writer);
                }
            }
            else
            {
                writer.Invoke();
            }
        }
    }
}
