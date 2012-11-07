using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompassNet.BusinessObjects
{
    [Serializable]
    public class Quote
    {
        public int ID { get; set; }
        private string _quotenumber = string.Empty;
        public string QuoteNumber { get; set; }

        private string _customer = string.Empty;
        public string Customer { get { return _customer; } set { _customer = value; } }
        private string _CustomerCode = string.Empty;
        public string CustomerCode { get { return _CustomerCode; } set { _CustomerCode = value; } }
        private string _CustomerContact = string.Empty;
        public string CustomerContact { get { return _CustomerContact; } set { _CustomerContact = value; } }
        private string _CustomerPhone = string.Empty;
        public string CustomerPhone { get { return _CustomerPhone; } set { _CustomerPhone = value; } }
        private string _CustomerFax = string.Empty;
        public string CustomerFax { get { return _CustomerFax; } set { _CustomerFax = value; } }
        private string _CustomerReferenceNo = string.Empty;
        public string CustomerReferenceNo { get { return _CustomerReferenceNo; } set { _CustomerReferenceNo = value; } }
        private string _CustomerState = string.Empty;
        public string CustomerState { get { return _CustomerState; } set { _CustomerState = value; } }
        private string _CustomerZip = string.Empty;
        public string CustomerZip { get { return _CustomerZip; } set { _CustomerZip = value; } }

        private string _PickupCity = string.Empty;
        public string PickupCity { get { return _PickupCity; } set { _PickupCity = value; } }
        private string _PortOfLoad = string.Empty;
        public string PortOfLoad { get { return _PortOfLoad; } set { _PortOfLoad = value; } }
        private string _PortOfDispatch = string.Empty;
        public string PortOfDispatch { get { return _PortOfDispatch; } set { _PortOfDispatch = value; } }
        private string _FinalDestination = string.Empty;
        public string FinalDestination { get { return _FinalDestination; } set { _FinalDestination = value; } }
        private string _DestinationZip = string.Empty;
        public string DestinationZip { get { return _DestinationZip; } set { _DestinationZip = value; } }
        private string _DestinationCountry = string.Empty;
        public string DestinationCountry { get { return _DestinationCountry; } set { _DestinationCountry = value; } }
        private string _TypeOfMove = string.Empty;
        public string TypeOfMove { get { return _TypeOfMove; } set { _TypeOfMove = value; } }
        private string _Division = string.Empty;
        public string Division { get { return _Division; } set { _Division = value; } }
        private string _Businessline = string.Empty;
        public string Businessline { get { return _Businessline; } set { _Businessline = value; } }
        private string _INCOTerms = string.Empty;
        public string INCOTerms { get { return _INCOTerms; } set { _INCOTerms = value; } }
        private string _INCOTermsLocation = string.Empty;
        public string INCOTermsLocation { get { return _INCOTermsLocation; } set { _INCOTermsLocation = value; } }
        private DateTime _Effectivdate = new DateTime();
        public DateTime Effectivdate { get { return _Effectivdate; } set { _Effectivdate = value; } }
        private DateTime _ExpirationDate = new DateTime();
        public DateTime ExpirationDate { get { return _ExpirationDate; } set { _ExpirationDate = value; } }
        private string _QuotedBy = string.Empty;
        public string QuotedBy { get { return _QuotedBy; } set { _QuotedBy = value; } }
        private string _SalesRep = string.Empty;
        public string SalesRep { get { return _SalesRep; } set { _SalesRep = value; } }
        private string _PreferredCarrier = string.Empty;
        public string PreferredCarrier { get { return _PreferredCarrier; } set { _PreferredCarrier = value; } }
        private string _Voyage = string.Empty;
        public string Voyage { get { return _Voyage; } set { _Voyage = value; } }
        private string _Vessel = string.Empty;
        public string Vessel { get { return _Vessel; } set { _Vessel = value; } }
        private string _ContainerHandling = string.Empty;
        public string ContainerHandling { get { return _ContainerHandling; } set { _ContainerHandling = value; } }
        private List<BusinessObjects.Container> _Containers = new List<Container>();
        public List<BusinessObjects.Container> Containers { get { return _Containers; } set { _Containers = value; } }


    }
}