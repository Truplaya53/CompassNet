using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompassNet.Functions
{
    public class Tools
    {
        public static System.Web.UI.Control FindConrolRecursive(System.Web.UI.Control root, string id)
        {
            if (root.ID == id)
            {
                return root;
            }
            foreach (System.Web.UI.Control c in root.Controls)
            {
                System.Web.UI.Control t = FindConrolRecursive(c, id);
                if (t != null)
                { return t; }
            }
            return null;
        }
        public static void ExecutePageMethod(string methodname, object[] parametrss, System.Web.UI.Page p)
        {
            System.Reflection.MethodInfo myMethod = p.GetType().GetMethod(methodname);
            myMethod.Invoke(p, parametrss);
        }
        public static void ExecuteControlMethod(string methodname, object[] parametrss, System.Web.UI.Control p)
        {
            System.Reflection.MethodInfo myMethod = p.GetType().GetMethod(methodname);
            myMethod.Invoke(p, parametrss);
        }

        public static string FormatShortDate(object o)
        {
            DateTime d;
            if (o!=null&&DateTime.TryParse(o.ToString(), out d))
            {
                if (d > DateTime.MinValue) return d.ToShortDateString();
            }
            return string.Empty;
        }

    }
}