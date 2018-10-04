using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingCommonLibrary.Models
{
    public class VoucherType : BaseModel
    {
        private int _VoucherId;
        private string _VoucherName;
        byte _VehicleType;
        private decimal _Rate;
        private decimal _Value;
        private int _Validity;
        private TimeSpan _ValidStart;
        private TimeSpan _ValidEnd;
        private string _VoucherInfo;
        private bool _SkipVoucherGeneration;
        private string _RateStr;

        public int VoucherId { get { return _VoucherId; } set { _VoucherId = value; OnPropertyChanged("VoucherId"); } }
        public string VoucherName { get { return _VoucherName; } set { _VoucherName = value; OnPropertyChanged("VoucherName"); } }
        public byte VehicleType { get { return _VehicleType; } set { _VehicleType = value; OnPropertyChanged("VehicleType"); } }
        public decimal Rate { get { return _Rate; } set { _Rate = value; _RateStr = value.ToString("#0.00"); OnPropertyChanged("Rate"); OnPropertyChanged("RateStr"); } }
        public decimal Value { get { return _Value; } set { _Value = value; OnPropertyChanged("Value"); } }
        public int Validity { get { return _Validity; } set { _Validity = value; OnPropertyChanged("Validity"); } }
        public TimeSpan ValidStart { get { return _ValidStart; } set { _ValidStart = value; OnPropertyChanged("ValidStart"); } }
        public TimeSpan ValidEnd { get { return _ValidEnd; } set { _ValidEnd = value; OnPropertyChanged("ValidEnd"); } }
        public DateTime Start { get { return new DateTime().Add(ValidStart); } set { ValidStart = value.TimeOfDay; OnPropertyChanged("Start"); } }
        public DateTime End { get { return new DateTime().Add(ValidEnd); } set { ValidEnd = value.TimeOfDay; OnPropertyChanged("End"); } }
        public string VoucherInfo { get { return _VoucherInfo; } set { _VoucherInfo = value; OnPropertyChanged("VoucherInfo"); } }
        public bool SkipVoucherGeneration { get { return _SkipVoucherGeneration; } set { _SkipVoucherGeneration = value; OnPropertyChanged("Skip Voucher Generation"); } }
        public string RateStr
        {
            get { return _RateStr; }
            set
            {
                _RateStr = value;
                Rate = Convert.ToDecimal(value);
                OnPropertyChanged("RateStr");
            }
        }
        public VoucherType()
        {
            //ValidStart = new TimeSpan(0, 0, 0);
            //ValidEnd = new TimeSpan(0, 0, 0);
        }

    }

    public class Voucher : BaseModel
    {

        public int VoucherNo { get; set; }
        public string BillNo { get; set; }
        public int Sno { get; set; }
        public string VoucherName { get; set; }
        public string Barcode { get; set; }
        public int VoucherId { get; set; }
        public decimal Value { get; set; }
        public DateTime ExpDate { get; set; }
        public TimeSpan ValidStart { get; set; }
        public TimeSpan ValidEnd { get; set; }
        public DateTime ScannedTime { get; set; }
        public byte FYID { get; set; }
               
    }
}
