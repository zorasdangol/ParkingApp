using Newtonsoft.Json;
using ParkingCommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ParkingStandardLibrary.Models
{
    class MembershipScheme : BaseModel
    {
        private int _SchemeId;
        private string _SchemeName;
        private bool _ValidOnWeekends;
        private bool _ValidOnHolidays;
        private int _ValidityPeriod;
        private decimal _Discount;
        private int _Limit;
        private ObservableCollection<ValidHour> _ValidHoursList;

        public int SchemeId { get { return _SchemeId; } set { _SchemeId = value; OnPropertyChanged("SchemeId"); } }
        public string SchemeName { get { return _SchemeName; } set { _SchemeName = value; OnPropertyChanged("SchemeName"); } }
        public bool ValidOnWeekends { get { return _ValidOnWeekends; } set { _ValidOnWeekends = value; OnPropertyChanged("ValidOnWeekends"); } }
        public bool ValidOnHolidays { get { return _ValidOnHolidays; } set { _ValidOnHolidays = value; OnPropertyChanged("ValidOnHolidays"); } }
        public int ValidityPeriod { get { return _ValidityPeriod; } set { _ValidityPeriod = value; OnPropertyChanged("ValidityPeriod"); } }
        public decimal Discount { get { return _Discount; } set { _Discount = value; OnPropertyChanged("Discount"); } }
        public int Limit { get { return _Limit; } set { _Limit = value; OnPropertyChanged("Limit"); } }
        public string ValidHours { get { return JsonConvert.SerializeObject(ValidHoursList); } set { ValidHoursList = JsonConvert.DeserializeObject<ObservableCollection<ValidHour>>(value); } }
        public ObservableCollection<ValidHour> ValidHoursList { get { return _ValidHoursList; } set { _ValidHoursList = value; OnPropertyChanged("ValidHoursList"); } }
               
    }

    class ValidHour : BaseModel
    {
        private TimeSpan _Start;
        private TimeSpan _End;
        private bool _SkipValidityPeriod;
        private bool _IgnoreLimit;
        public TimeSpan Start { get { return _Start; } set { _Start = value; OnPropertyChanged("Start"); } }
        public TimeSpan End { get { return _End; } set { _End = value; OnPropertyChanged("End"); } }
        public bool SkipValidityPeriod { get { return _SkipValidityPeriod; } set { _SkipValidityPeriod = value; OnPropertyChanged("SkipValidityPeriod"); } }
        public bool IgnoreLimit { get { return _IgnoreLimit; } set { _IgnoreLimit = value; OnPropertyChanged("IgnoreLimit"); } }
    }
}


