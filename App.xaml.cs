using System.Configuration;
using System.Data;
using System.Windows;
using System.IO.Ports;
namespace spa
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
           
        }

        private void  SerialPortApplication()
        {
            SerialPort serialPort = new SerialPort();
            serialPort.BaudRate = 9600;
            serialPort.DataBits = 8;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            serialPort.PortName = "COM1";
            try
            {
                serialPort.Open();
            }
            catch
            {

            }
            serialPort.Write(new byte[] { 0x12, 0x13 }, 0, 2);
            byte[] buffer = new byte[2];
            serialPort.Read(buffer, 0, 2);
            serialPort.DataReceived += (se, ev) =>
            {
                byte[] buffer = new byte[2];
                serialPort.Read(buffer, 0, 2);
            };
        }
    }

}
