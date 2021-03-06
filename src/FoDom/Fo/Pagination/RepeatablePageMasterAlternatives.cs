﻿//Apache2, 2017, WinterDev
//Apache2, 2009, griffm, FO.NET
using System;
using System.Collections;

namespace Fonet.Fo.Pagination
{
    internal class RepeatablePageMasterAlternatives : FObj, SubSequenceSpecifier
    {
        private const int INFINITE = -1;

        public static FObjMaker<RepeatablePageMasterAlternatives> GetMaker()
        {
            return new FObjMaker<RepeatablePageMasterAlternatives>((parent, propertyList) => new RepeatablePageMasterAlternatives(parent, propertyList));
        }




        private PageSequenceMaster pageSequenceMaster;

        private int maximumRepeats;

        private int numberConsumed = 0;

        private ArrayList conditionalPageMasterRefs;

        public RepeatablePageMasterAlternatives(FObj parent, PropertyList propertyList)
            : base(parent, propertyList)
        {
     
            conditionalPageMasterRefs = new ArrayList();

            if (parent.ElementName.Equals("fo:page-sequence-master"))
            {
                this.pageSequenceMaster = (PageSequenceMaster)parent;
                this.pageSequenceMaster.AddSubsequenceSpecifier(this);
            }
            else
            {
                throw new FonetException("fo:repeatable-page-master-alternatives"
                    + "must be child of fo:page-sequence-master, not "
                    + parent.ElementName);
            }

            string mr = GetProperty("maximum-repeats").GetString();
            if (mr.Equals("no-limit"))
            {
                setMaximumRepeats(INFINITE);
            }
            else
            {
                try
                {
                    setMaximumRepeats(Int32.Parse(mr));
                }
                catch (FormatException)
                {
                    throw new FonetException("Invalid number for 'maximum-repeats' property");
                }
            } 
        }
        public override string ElementName { get { return "fo:repeatable-page-master-alternatives"; } }
        public string GetNextPageMaster(
            int currentPageNumber, bool thisIsFirstPage, bool isEmptyPage)
        {
            string pm = null;
            if (getMaximumRepeats() != INFINITE)
            {
                if (numberConsumed < getMaximumRepeats())
                {
                    numberConsumed++;
                }
                else
                {
                    return null;
                }
            }

            foreach (ConditionalPageMasterReference cpmr in conditionalPageMasterRefs)
            {
                if (cpmr.isValid(currentPageNumber + 1, thisIsFirstPage, isEmptyPage))
                {
                    pm = cpmr.GetMasterName();
                    break;
                }
            }
            return pm;
        }

        private void setMaximumRepeats(int maximumRepeats)
        {
            if (maximumRepeats == INFINITE)
            {
                this.maximumRepeats = maximumRepeats;
            }
            else
            {
                this.maximumRepeats = (maximumRepeats < 0) ? 0 : maximumRepeats;
            }

        }

        private int getMaximumRepeats()
        {
            return this.maximumRepeats;
        }

        public void addConditionalPageMasterReference(ConditionalPageMasterReference cpmr)
        {
            this.conditionalPageMasterRefs.Add(cpmr);
        }

        public void Reset()
        {
            this.numberConsumed = 0;
        }

        protected PageSequenceMaster getPageSequenceMaster()
        {
            return pageSequenceMaster;
        }
    }
}