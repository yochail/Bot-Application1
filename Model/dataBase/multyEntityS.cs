﻿using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.dataBase
{
    public partial class multyEntity : IMultyEntity
    {
        public IentityBase clone()
        {
            var newMulEnt = new multyEntity();
            newMulEnt.entityID = entityID;
             newMulEnt.entityValue = entityValue;
              newMulEnt.entityType = entityType;
               newMulEnt.parts = parts;
                newMulEnt.singleValue = singleValue;
            return newMulEnt;
        }


        public override bool Equals(Object obj)
        {
            var ent = obj as Ientity;
            if (ent == null) return false;

            if (ent.entityValue == singleValue || ent.entityValue == entityValue)
                return true;

            return false;
        }
    }
}