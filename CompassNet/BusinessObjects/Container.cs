using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompassNet.BusinessObjects
{
    [Serializable]
    public class Container
    {
        public int ID { get; set; }
        private string _containernumber = string.Empty;
        public string ContainerNumber { get { return _containernumber; } set { _containernumber = value; } }
        private string _containertype = string.Empty;
        public string ContainerType { get { return _containertype; } set{_containertype =value;} }
        private string _seal1 = string.Empty;
        public string Seal1 {  get { return _seal1; } set{_seal1 =value;} }
        private string _seal2 = string.Empty;
        public string Seal2 {  get { return _seal2; } set{_seal2 =value;} }
        private string _seal3 = string.Empty;
        public string Seal3 {  get { return _seal3; } set{_seal3 =value;} }
        private string _tareweight = string.Empty;
        public string TareWeight {  get { return _tareweight; } set{_tareweight =value;} }
        private Units.WeightUnits _tareweightunit=Units.WeightUnits.Lbs;
        public Units.WeightUnits TareWeightUnit { get { return _tareweightunit; } set { _tareweightunit = value; } }
        private List<ContainerCargo> _cargo=new List<ContainerCargo>();
        public List<ContainerCargo> Cargo
        {
            get { return _cargo; }
            set { _cargo = value; }
        }
        private DateTime _dischargedate;
        public DateTime DischargeDate
        {
            get { return _dischargedate; }
            set { _dischargedate = value; }
        }
        private DateTime _pickupdate;
        public DateTime PickupDate
        {
            get { return _pickupdate; }
            set { _pickupdate = value; }
        }
        private DateTime _customerpickuplastdate;
        public DateTime CustomerPickupLastDate
        {
            get { return _customerpickuplastdate; }
            set { _customerpickuplastdate = value; }
        }
        private DateTime _customerreturnlastdate;
        public DateTime CustomerReturnLastDate
        {
            get { return _customerreturnlastdate; }
            set { _customerreturnlastdate = value; }
        }
        private DateTime _carrierrpickuplastdate;
        public DateTime CarrierPickUpLastDate
        {
            get { return _carrierrpickuplastdate; }
            set { _carrierrpickuplastdate = value; }
        }
        private DateTime _carrierreturnlastdate;
        public DateTime CarrierReturnLastDate
        {
            get { return _carrierreturnlastdate; }
            set { _carrierreturnlastdate = value; }
        }
        private DateTime _returndate;
        public DateTime ReturnDate
        {
            get { return _returndate; }
            set { _returndate = value; }
        }
        private string _rate;
        public string Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }
         
        
    }
}