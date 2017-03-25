﻿//Apache2, 2017, WinterDev
//Apache2, 2009, griffm, FO.NET
using Fonet.Fo.Expr;

namespace Fonet.DataTypes
{
    public class TableColLength : Length
    {
        private double tcolUnits;

        public TableColLength(double tcolUnits)
        {
            this.tcolUnits = tcolUnits;
        }

        public override double GetTableUnits()
        {
            return tcolUnits;
        }

        public override void ResolveTableUnit(double mpointsPerUnit)
        {
            SetComputedValue((int)(tcolUnits * mpointsPerUnit));
        }

        public override string ToString()
        {
            return (tcolUnits.ToString() + " table-column-units");
        }

        public override Numeric AsNumeric()
        {
            return new Numeric(this);
        }
    }
}