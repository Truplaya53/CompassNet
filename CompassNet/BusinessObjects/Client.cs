using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompassNet.BusinessObjects
{
    public class Client
    {
        private string  _clintID;
        public string ClientID
        {
            get { return _clintID; }
            set { _clintID = value; }
        }
        private string _type;
        public string Type
        {
            get { return _type;}
            set { _type = value;}
        }
        private string _name;
	    public string Name
	    {
		    get { return _name;}
		    set { _name = value;}
	    }
    	private string _contact;
	    public string Contact
	    {
		    get { return _contact;}
		    set { _contact = value;}
	    }
        private string _phone;
	    public string Phone
	    {
		    get { return _phone;}
		    set { _phone = value;}
	    }
	    private string _fax;
	    public string Fax
	    {
		    get { return _fax;}
		    set { _fax = value;}
	    }
        private string _refno;
	    public string ReferenceNo
	    {
		    get { return _refno;}
		    set { _refno = value;}
	    }
			

    }
}