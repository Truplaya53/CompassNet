using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompassNet.BusinessObjects
{
    [Serializable]
    public class ContainerCargo
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }       
        private string _description=string.Empty;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private int _Pieces;
        public int Pieces
        {
            get { return _Pieces; }
            set { _Pieces = value; }
        }
        private string _uom = string.Empty;
        public string UOM
        {
            get { return _uom; }
            set { _uom = value; }
        }
        private double _grossweight;
        public double GrossWeight
        {
            get { return _grossweight; }
            set { _grossweight = value; }
        }
        private Units.WeightUnits _grossweightunit;
        public Units.WeightUnits GrossWeightUnit
        {
            get { return _grossweightunit; }
            set { _grossweightunit = value; }
        }
        private double _netweight;
        public double NetWeight
        {
            get { return _netweight; }
            set { _netweight = value; }
        }
        private Units.WeightUnits _netweightunit;
        public Units.WeightUnits NetWeightUnit
        {
            get { return _netweightunit; }
            set { _netweightunit = value; }
        }
        private bool _hazardous;
        public bool Hazardous
        {
            get { return _hazardous; }
            set { _hazardous = value; }
        }
        
        
    }
}