using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using RDSDataObjects;

namespace DemoApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            try
            {
                RDSDataObject dataObject = new RDSDataObject(Clipboard.GetDataObject());

                if (Clipboard.ContainsImage())
                {
                    picBox1.Image = Clipboard.GetImage();
                }
                else if (dataObject.GetData("FileGroupDescriptorW") != null)
                {
                    var fileNames = dataObject.GetData("FileGroupDescriptorW") as string[];
                    var fileStreams = dataObject.GetData("FileContents") as MemoryStream[];

                    if (fileNames.Length > 0)
                    {
                        string fileName = fileNames[0];
                        MemoryStream fileStream = fileStreams[0];

                        picBox1.Image = Image.FromStream(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:  {{{ex.Message}}}");
            }
        }
    }
}
