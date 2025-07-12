namespace DexApi.Models
{
    public class DexMeter
    {
        public DexMeter(string machine, DateTime dEXDateTime, string machineSerialNumber, decimal valueOfPaidVends)
        {
            Machine = machine;
            DEXDateTime = dEXDateTime;
            MachineSerialNumber = machineSerialNumber;
            ValueOfPaidVends = valueOfPaidVends;
        }

        public string Machine { get; set; } = string.Empty;
        public DateTime DEXDateTime { get; set; }
        public string MachineSerialNumber { get; set; } = string.Empty;
        public decimal ValueOfPaidVends { get; set; }
    }
}
