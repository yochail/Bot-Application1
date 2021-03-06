﻿
using Model.dataBase;
using Model.Models;
using NLP.HebWords;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;


namespace NLP.WorldObj
{


    public class WorldObject : IWorldObject
    {
       
        List<RelationObject> relations = new List<RelationObject>();
        private string word;
        private bool negat;

        public WorldObject(){}

        public WorldObject(string word)
        {
            this.Word = word; 
            this.Entity =  new entity();
            Entity.entityValue = word;
            Entity.entityType = "unknownWord";
        }



        public WorldObject(WordObject word) 
        {
            this.Word = word.Text;
            this.Entity = new entity();
            Entity.entityValue = word.Text;
            Entity.entityType = word.TypefromEnum(word.WordT);
        }

        public List<RelationObject> Relations
        {
            get
            {
                return relations;
            }

            set
            {
                relations = value;
            }
        }

        public bool Negat
        {
            get
            {
                return negat;
            }

            set
            {
                negat = value;
            }
        }

        public bool DefiniteArticle { get;  set; }

        public string Word
        {
            get
            {
                return word;
            }

            set
            {
                word = value;
            }
        }

        public IentityBase Entity { get;  set; }

        public void addRelation(RelationObject relation)
        {if (relation != null)
            {
                relations.Add(relation);
            }
        }


        public override string ToString()
        {
            var n = negat ? "not" : "";
                return Word + "(" + GetType() + ")" + n ;
        }

        internal void Copy(WorldObject obj)
        {
            word = obj.word;
            negat = negat || obj.negat;
            DefiniteArticle = DefiniteArticle || obj.DefiniteArticle;
            foreach (var r in obj.Relations)
            {
                relations.Add((RelationObject)r.Clone());
            }
        }

        public int ObjectType()
        {
            return 1;
        }

        public virtual IWorldObject Clone()
        {
            WorldObject res = new WorldObject(word);
            res.Copy(this);
            return res;
        }

        //internal void cloneBase(WorldObject origin)
        //{
        //    foreach (var r in origin.Relations)
        //    {
        //        relations.Add((RelationObject)r.Clone());
        //    }
        //    negat = origin.negat;
        //    DefiniteArticle = origin.DefiniteArticle;
        //}

         public virtual void CopyFromTemplate(ITemplate[] objects)
        {
            try
            {
                var index = int.Parse(Word);
                var obj = objects[index];

                if (obj is WorldObject)
                {
                    Copy(obj as WorldObject);
                    foreach (var r in relations)
                    {
                        r.CopyFromTemplate(objects);
                    }
                }
                else
                {
                    var word = obj as WordObject;
                    this.word = word.Lemma == null || word.Lemma.Length < 2 ? word.Text : word.Lemma ;
                    this.DefiniteArticle = word.IsDefinite;
                    this.Entity = word.WorldObject.Entity;
                }
            }catch(Exception ex)
            {

            }

        }

        public bool haveTypeOf(ITemplate template)
        {
            if (template.ObjectType() != ObjectType()) return false;
            if (instanceOf(this.GetType(), template.GetType())) return true;
            return false;
        }


        //recursivly search for type;
        private bool instanceOf(Type type, Type templateType)
        {
            if (templateType == null) return false;
            else if (type == templateType) return true;
            else return instanceOf(type.BaseType, templateType);
        }


        public override bool Equals(Object obj)
        {

            if (this.GetType() != obj.GetType()) return false;

            var objres = obj as WorldObject;

            if (!(this.Word == objres.Word &&
            this.Negat == objres.Negat
            //  && this.DefiniteArticle == objres.DefiniteArticle
            )) return false;

            foreach(var r in Relations)
            {
                if (!objres.Relations.Contains(r)) return false;
            }
          

            return true;
        }
    }




}
