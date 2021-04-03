using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CVSCovidLocator
{
    public partial class Ui : Form
    {
        ulong activityCounter = 0;
        public Ui()
        {
            InitializeComponent();
        }

        public void SetError(string message)
        {
            textBox1.BackColor = Color.LightCoral;
            textBox1.Text = message;
            textBox2.Text = "Stopped. Please fix error and restart application.";
        }

        public void Activity()
        {
            activityCounter++;
            string postfix = "";
            switch (activityCounter % 4)
            {
                case 0: postfix = "."; break;
                case 1: postfix = ".."; break;
                case 2: postfix = "..."; break;
                case 3: postfix = "...."; break;
            }
            label3.Text = $"Activity {postfix}";
        }

        public void UpdateAvailability()
        {
            var cur = new List<Availability>();
            lock (Globals.LOCK)
            {
                Activity();
                foreach (var zip in Globals.CurrentAvailability)
                {
                    cur.AddRange(zip.Value);
                }
                cur = cur.GroupBy(x => x.StoreNumber).Select(grp => grp.First()).ToList();
                var outString = "";
                foreach (var line in cur)
                {
                    foreach (var date in line.Dates)
                    {
                        var text = $"{line.City} - {date} - {line.Address}, {line.ZIP} --- {line.Distance} from {line.SearchZip}{Environment.NewLine}";
                        outString += text;
                        textBox2.AppendText($"{DateTime.Now.ToString("G")} - {text}");
                    }
                    
                }
                if (cur.Count > 0)
                {
                    textBox1.BackColor = Color.PaleGreen;
                }
                else
                {
                    textBox1.BackColor = Color.FromKnownColor(KnownColor.Control);
                }
                textBox1.Text = outString;
            }
        }
    }
    
}
