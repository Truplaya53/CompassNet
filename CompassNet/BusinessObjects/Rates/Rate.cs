using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompassNet.BusinessObjects.Rates
{
    public class Rate
    {
        private RateType _type;
        public RateType Type
        {
            get { return _type; }
            set { _type = value; }
        }        
        private string _contractnumber;
        public string ContractNumber
        {
            get { return _contractnumber; }
            set { _contractnumber = value; }
        }
        private string _shipper;
        public string Shipper
        {
            get { return _shipper; }
            set { _shipper = value; }
        }        
        private string _origin;
        public string Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }
        private string _destination;
        public string Destination
        {
            get { return _destination; }
            set { _destination = value; }
        }
        private string _commodity;
        public string Commodity
        {
            get { return _commodity; }
            set { _commodity = value; }
        }
        private string _containerType;
        public string ContainerType
        {
            get { return _containerType; }
            set { _containerType = value; }
        }
        private double _dollarvalue;
        public double DollarValue
        {
            get { return _dollarvalue; }
            set { _dollarvalue = value; }
        }

        private List<Rate> _subrates;
        public List<Rate> Subrates
        {
            get { return _subrates; }
            set { _subrates = value; }
        }
        
        private List<Surcharge> _surcharges;
        public List<Surcharge> Surcharges
        {
            get { return _surcharges; }
            set { _surcharges = value; }
        }

        public enum RateType
        {
            InlandFromOrigin,
            PortToPortSea,
            PortToPortAir,
            InlandAtDestination
        }
    }
}