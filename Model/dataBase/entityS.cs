﻿using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.dataBase
{
    public partial class entity : Ientity
    {

        public override bool Equals(Object obj)
        {

            var ent = obj as Ientity;
            if (ent == null) return false;
            if (!(entitySynonimus.Contains(ent.entityValue) && entityType == ent.entityType))
                return false;


            return true;
        }

        public IentityBase clone()
        {
            var newMulEnt = new entity();
            newMulEnt.entityID = entityID;
            newMulEnt.entityValue = entityValue;
            newMulEnt.entityType = entityType;
            newMulEnt.entitySynonimus = entitySynonimus;

            return newMulEnt;
        }


    }
}