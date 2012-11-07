using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompassNet.BusinessObjects
{
    public class SalesRep
    {
        private int _id;
	    public int ID
	    {
		    get { return _id;}
		    set { _id = value;}
	    }
	    private string _code;
	    public string Code
	    {
		    get { return _code;}
		    set { _code = value;}
	    }
	    private string _name;
	    public string Name
	    {
		    get { return _name;}
		    set { _name = value;}
	    }
	    private bool _active;
	    public bool Active
	    {
		    get { return _active;}
		    set { _active = value;}
	    }
	

    }
}