using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace Keygen
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      var addressBytes = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault().GetPhysicalAddress().GetAddressBytes();
      var dateBytes = BitConverter.GetBytes(DateTime.Now.Date.ToBinary());

      var transformedValues = addressBytes.Select((x, i) => (x ^ dateBytes[i]) * 10);

      var key = string.Join("-", transformedValues);

      textBox1.Text = key;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Clipboard.SetText(textBox1.Text);
    }
  }
}
