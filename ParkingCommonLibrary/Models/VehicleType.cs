﻿using ParkingCommonLibrary.Helpers;
using ParkingCommonLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ParkingCommonLibrary.Models
{
    public class TVehicleType : BaseModel
    {
        byte _VehicleTypeID;
        string _Description;
        int _Capacity;
        int _UID;
        private byte[] _ButtonImage;


        public byte VTypeID { get { return _VehicleTypeID; } set { _VehicleTypeID = value; OnPropertyChanged("VTypeID"); } }
        public string Description { get { return _Description; } set { _Description = value; OnPropertyChanged("Description"); } }
        public int Capacity { get { return _Capacity; } set { _Capacity = value; OnPropertyChanged("Capacity"); } }
        public int UID { get { return _UID; } set { _UID = value; OnPropertyChanged("UID"); } }
        public byte[] ButtonImage { get { return _ButtonImage; } set { _ButtonImage = value; OnPropertyChanged("ButtonImage"); } }
                
    }

    public class VehicleType : TVehicleType
    {
        private int _Occupency;
        private ObservableCollection<ParkingArea> _PAOccupencyList;
        private string _PlateNo;

        public int Occupency { get { return _Occupency; } set { _Occupency = value; OnPropertyChanged("Occupency"); OnPropertyChanged("Available"); OnPropertyChanged("IsOverMaxLimit"); OnPropertyChanged("IsOverHalf"); } }
        public int Available { get { return Capacity - Occupency; } }
        public bool IsOverMaxLimit { get { return Available < 10; } }
        public bool IsOverHalf { get { return Occupency > Available; } }
        public string PlateNo { get { return _PlateNo; } set { _PlateNo = value; OnPropertyChanged("PlateNo"); } }

        public ObservableCollection<ParkingArea> PAOccupencyList { get { if (_PAOccupencyList == null) _PAOccupencyList = new ObservableCollection<ParkingArea>(); return _PAOccupencyList; } set { _PAOccupencyList = value; OnPropertyChanged("PAOccupencyList"); } }
        public VehicleType()
        {
            UID = GlobalClass.User.UID;
        }
    }
}
