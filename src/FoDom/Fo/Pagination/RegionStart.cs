﻿//Apache2, 2017, WinterDev
//Apache2, 2009, griffm, FO.NET
using Fonet.Layout;

namespace Fonet.Fo.Pagination
{
    internal class RegionStart : Region
    {

        public static FObjMaker<RegionStart> GetMaker()
        {
            return new FObjMaker<RegionStart>((parent, propertyList) => new RegionStart(parent, propertyList));
        }

        public const string REGION_CLASS = "start";

        protected RegionStart(FObj parent, PropertyList propertyList)
            : base(parent, propertyList) { }

        internal RegionArea MakeRegionArea(
            int allocationRectangleXPosition,
            int allocationRectangleYPosition,
            int allocationRectangleWidth,
            int allocationRectangleHeight,
            bool beforePrecedence,
            bool afterPrecedence,
            int beforeHeight,
            int afterHeight)
        {
            int extent = this.properties.GetProperty("extent").GetLength().MValue();
            int startY = allocationRectangleYPosition;
            int startH = allocationRectangleHeight;
            if (beforePrecedence)
            {
                startY -= beforeHeight;
                startH -= beforeHeight;
            }
            if (afterPrecedence)
            {
                startH -= afterHeight;
            }

            RegionArea area = new RegionArea(
                allocationRectangleXPosition, startY, extent, startH);
            area.setBackground(propMgr.GetBackgroundProps());

            return area;
        }

        public override RegionArea MakeRegionArea(int allocationRectangleXPosition,
                                                  int allocationRectangleYPosition,
                                                  int allocationRectangleWidth,
                                                  int allocationRectangleHeight)
        {
            BorderAndPadding bap = propMgr.GetBorderAndPadding();
            BackgroundProps bProps = propMgr.GetBackgroundProps();
            int extent = this.properties.GetProperty("extent").GetLength().MValue();

            return MakeRegionArea(allocationRectangleXPosition,
                                  allocationRectangleYPosition,
                                  allocationRectangleWidth, extent, false, false,
                                  0, 0);
        }

        protected override string GetDefaultRegionName()
        {
            return "xsl-region-start";
        } 
        public override string ElementName { get { return "fo:region-start"; } }
        public override string GetRegionClass()
        {
            return REGION_CLASS;
        }
    }
}