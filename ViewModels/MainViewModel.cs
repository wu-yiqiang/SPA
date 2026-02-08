using System;
using System.Collections.Generic;
using System.Text;

namespace spa.ViewModels
{
    public class MainViewModel
    {
        public int SelectedReciveModeId { get; set; } = 1;
        public int SelectedReciveEncodeId { get; set; } = 1;

        public int SelectedSendModeId { get; set; } = 1;
        public int SelectedSendEncodeId { get; set; } = 1;

        public class ModeOption { public int Id { get; set; } public string Name { get; set; } }

        public List<ModeOption> ModeOptions { get; set; } = new()
        {
            new ModeOption { Id=1, Name = "HEX" },
            new ModeOption { Id=2, Name = "ASCII" },
            new ModeOption { Id=3, Name = "BASE64" },
            new ModeOption { Id=3, Name = "BINARY" },
        };

        public int SelectedEncodeId { get; set; } = 1;
        public class EncodeOption { public int Id { get; set; } public string Name { get; set; } }

        public List<EncodeOption> EncodeOptions { get; set; } = new()
        {
            new EncodeOption { Id=1, Name = "UTF-8" },
            new EncodeOption { Id=2, Name = "UTF-16" },
            new EncodeOption { Id=3, Name = "UTF-32" },
        };


        public int SelectedSerialId { get; set; } = 1;
        public class SerialOption { public int Id { get; set; } public string Name { get; set; } }

        public List<SerialOption> SerialOptions { get; set; } = new()
        {
            new SerialOption { Id=1, Name = "COM1" },
            new SerialOption { Id=2, Name = "COM2" },
            new SerialOption { Id=3, Name = "COM3" },
            new SerialOption { Id=4, Name = "COM4" },
        };


        public int SelectedBaudRateId { get; set; } = 9600;
        public class BaudRateOption { public int Id { get; set; } public string Name { get; set; } }

        public List<BaudRateOption> BaudRateOptions { get; set; } = new()
        {
            new BaudRateOption { Id=300, Name = "300" },
            new BaudRateOption { Id=1200, Name = "1200" },
            new BaudRateOption { Id=2400, Name = "2400" },
            new BaudRateOption { Id=9600, Name = "9600" },
        };

        public int SelectedParityBitId { get; set; } = 1;
        public class BaudParityBitOption { public int Id { get; set; } public string Name { get; set; } }

        public List<BaudParityBitOption> BaudParityBitOptions { get; set; } = new()
        {
            new BaudParityBitOption { Id=1, Name = "NONE" },
            new BaudParityBitOption { Id=2, Name = "ODD" },
            new BaudParityBitOption { Id=3, Name = "EVEN" },
            new BaudParityBitOption { Id=4, Name = "MARK" },
            new BaudParityBitOption { Id=5, Name = "SPACE" },
        };


        public int SelectedDataBitId { get; set; } = 1;
        public class BaudDataBitOption { public int Id { get; set; } public string Name { get; set; } }

        public List<BaudDataBitOption> BaudDataBitOptions { get; set; } = new()
        {
            new BaudDataBitOption { Id=1, Name = "8" },
        };

        public int SelectedStopBitId { get; set; } = 1;
        public class BaudStopBitOption { public int Id { get; set; } public string Name { get; set; } }

        public List<BaudStopBitOption> BaudStopBitOptions { get; set; } = new()
        {
            new BaudStopBitOption { Id=1, Name = "1" },
            new BaudStopBitOption { Id=2, Name = "2" },
        };
    }
}
