﻿namespace NLP.WorldObj
{
    internal class PrepRelObject : RelationObject
    {
        private PrepType type;
        private int v;


        public override IWorldObject Clone()
        {
            PrepRelObject res = new PrepRelObject(Objective);
            res.Copy(this);
            return res;
        }
        public PrepRelObject(IWorldObject objective, PrepType type) : base(objective)
        {

            this.Type = type;
            
        }

        public PrepRelObject(IWorldObject objective) : base(objective)
        {
   
                this.Type = PrepType.unknownPrep;
            
        }

        internal PrepType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public override string ToString()
        {
            return type.ToString();
        }
    }

    enum PrepType
    {
        likePrep,
        onPrep,
        beforePrep,
        afterPrep,
        inPrep,
        atPrep,
        sincePrep,
        forPrep,
        toPrep,
        rangePrep,
        unknownPrep,

    }


    enum PrepContext
    {
        timePrepContext,
        placePrepContext,
        otherPrepContext,
    }


   
}

