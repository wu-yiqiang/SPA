using spa.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace spa.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool OpenSerial {
            get;
            set
            {
                field = value;
                OnPropertyChanged(nameof(Enabled));
                OnPropertyChanged(nameof(OpenSerial));
            }
        } = false;

        public string SerialName { get; set {
                field = value;
                OnPropertyChanged(nameof(ButtonEnabled));
            }
        }
        public bool ShowTimeStamp { get; set; }
        public bool ButtonEnabled { get {
                if (SerialName != null)
                {
                    return true;
                }
                ;
                return false;
            }
        }
        public bool AutoScroll { get; set; }
        public bool AutoSend { get; set; }
        public int SendFrequency { get; set; } = 1000;
        public class SerialPortOption { public required string Id { get; set; } public required string Name { get; set; } }
        public List<SerialPortOption> SerialList { get; } = SerialPort.GetPortNames().Select((name, index) => new SerialPortOption { Name = name, Id = name }).ToList();

        public int SelectedSerialId { get; set; } = 1;
        public int BaudRate { get; set; } = 9600;
        public class BaudRateOption { public int Id { get; set; } public int Name { get; set; } }
        public List<BaudRateOption> BaudRateOptions { get; set; } =
        new()
        {
            new BaudRateOption { Id=1200, Name = 1200 },
            new BaudRateOption { Id=2400, Name = 2400 },
            new BaudRateOption { Id=9600, Name = 9600 },
            new BaudRateOption { Id=19200, Name = 19200 },
            new BaudRateOption { Id=38400, Name = 38400 },
            new BaudRateOption { Id=57600, Name = 57600 },
            new BaudRateOption { Id=115200, Name = 115200 },

        };
        public int SelectedReciveModeId { get; set; } = 1;

        public int SelectedReciveEncodeId { get; set; } = 1;
        public int SelectedSendModeId { get; set; } = 1;
        public int SelectedSendEncodeId { get; set; } = 1;
       

        public class EncodeModeOption { public int Id { get; set; } public string Name { get; set; } }

        public List<EncodeModeOption> EncodeModeOptions { get; set; } = new()
        {
            new EncodeModeOption { Id=1, Name = "HEX" },
            //new EncodeModeOption { Id=2, Name = "ASCII" },
            //new EncodeModeOption { Id=3, Name = "BASE64" },
            //new EncodeModeOption { Id=3, Name = "BINARY" },
        };

        public int SelectedEncodeId { get; set; } = 1;

        public Parity SelectedParityBitId { get; set; } = Parity.None;
        public class BaudParityBitOption { public Parity Id { get; set; } public string Name { get; set; } }

        public List<BaudParityBitOption> BaudParityBitOptions { get; set; } = new()
        {
            new BaudParityBitOption { Id=Parity.None, Name = "None" },
            new BaudParityBitOption { Id=Parity.Odd, Name = "Odd" },
            new BaudParityBitOption { Id=Parity.Even, Name = "Even" },
            new BaudParityBitOption { Id=Parity.Mark, Name = "Mark" },
            new BaudParityBitOption { Id=Parity.Space, Name = "Space" },
        };


        public int SelectedDataBitId { get; set; } = 8;
        public class BaudDataBitOption { public int Id { get; set; } public int Name { get; set; } }

        public List<BaudDataBitOption> BaudDataBitOptions { get; set; } = new()
        {
            new BaudDataBitOption { Id=5, Name = 5 },
            new BaudDataBitOption { Id=6, Name = 6 },
            new BaudDataBitOption { Id=7, Name = 7 },
            new BaudDataBitOption { Id=8, Name = 8 },
        };

        public StopBits SelectedStopBitId { get; set; } = StopBits.One;
        public class BaudStopBitOption { public StopBits Id { get; set; } public string Name { get; set; } }

        public List<BaudStopBitOption> BaudStopBitOptions { get; set; } = new()
        {
            new BaudStopBitOption { Id=StopBits.One, Name = "One" },
            new BaudStopBitOption { Id=StopBits.Two, Name = "Two" },
        };
        public Command BtnOpenCommand {  get; set; }
        public Command SendCommand { get; set; }
        public Command SendClear { get; set; }
        public Command ReceiveClear { get; set; }
        public bool Enabled
        {
            get
            {
                if (OpenSerial) {
                    return false;
                };
                return true;
            }
        }
        public string ReceiveText
        {
            get
            {
                if (ShowTimeStamp)
                {
                    var lines = RecordList.Select(static r => $"【{r.Timestamp:yyyy-MM-dd HH:mm:ss.fff}】 {r.Content}");
                    return string.Join(Environment.NewLine, lines);

                }
                else
                {
                    var lines = RecordList.Select(r => $"{r.Content}");
                    return string.Join(Environment.NewLine, lines);

                }

            }
        }

        public string SendText
        {
            get;
            set
            {
                field = value;
                OnPropertyChanged(nameof(SendText));
            }
        } = "";
        public ObservableCollection<SerialRecord> RecordList
        {
            get;
            set;
        } = new ObservableCollection<SerialRecord>();
        public class SerialRecord
        {
            public DateTime Timestamp { get; set; }
            public string Content { get; set; }
        }
        public MainViewModel()
        {
            BtnOpenCommand = new Command(Open);
            SendCommand = new Command(Send);
            SendClear = new Command(Clear);
            ReceiveClear = new Command(ClearReceived);
            serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] bytes = new byte[serialPort.BytesToRead];
            serialPort.Read(bytes,0, bytes.Length);
            string text = string.Join("", bytes.Select(b => b.ToString("X2")));
            Application.Current.Dispatcher.Invoke(() =>
            {
                RecordList.Add(new SerialRecord
                {
                    Timestamp = DateTime.Now,
                    Content = text
                });
                OnPropertyChanged(nameof(ReceiveText));
            });
        }

        SerialPort serialPort = new SerialPort();
        private void Open(Object? obj)
        {
            if (!OpenSerial) {
                serialPort.BaudRate = BaudRate;
                serialPort.DataBits = SelectedDataBitId;
                serialPort.Parity = SelectedParityBitId;
                serialPort.StopBits = SelectedStopBitId;
                serialPort.PortName = SerialName;
                try
                {
                    serialPort.Open();
                    OpenSerial = true;
                    MessageBox.Show("连接串口成功", "提示", MessageBoxButton.OK);
                }
                catch
                {
                    MessageBox.Show("连接串口失败", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }else
            {
                serialPort.Close();
                OpenSerial = false;
                MessageBox.Show("关闭串口成功", "提示", MessageBoxButton.OK);
            }
        }
        private void Send(object? obj)
        {
            if (String.IsNullOrEmpty(SendText.Trim()) != false || !OpenSerial)
            {
                return;
            }
            string send_text = Regex.Replace(SendText, @"\s", "");
            byte[] send_data = Convert.FromHexString(send_text);
            serialPort.Write(send_data, 0, send_data.Length); 
            RecordList.Add(new SerialRecord
            {
                Timestamp = DateTime.Now,
                Content = send_text
            });
            OnPropertyChanged(nameof(ReceiveText));
        }
        private void Clear(object? obj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                SendText = "";
            });
        }
        private void ClearReceived(object? obj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                RecordList.Clear();
                OnPropertyChanged(nameof(ReceiveText));
            });
        }
    }
}
