using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Inputs;
using Unity.VisualScripting;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Data.Common;

namespace Assets.Scripts.GamePlay2D
{
    public abstract class CharacterAttr_Vector2
    {
        public Vector2 base_value;
        public List<AttrDecorator_Vector2> attrDecorators = new List<AttrDecorator_Vector2>();
        public void AddDect(AttrDecorator_Vector2 target)
        {
            attrDecorators.Add(target);
        }
        public abstract Vector2 GetValue();
    }

    public abstract class AttrDecorator_Vector2
    {
        public const string NAME = "";
        public abstract CharacterAttr_Vector2 GetAttr(CharacterAttr_Vector2 oriAttr);// where RetAttrT : CharacterAttr;
    }

    public class GetXYInputToVectorAttr : AttrDecorator_Vector2
    {
        public override CharacterAttr_Vector2 GetAttr(CharacterAttr_Vector2 oriAttr)
        {
            var ret_v = new PlusMultiAttr_Vector2();
            var ori_v = (PlusMultiAttr_Vector2)oriAttr;
            ret_v.base_value = ori_v.base_value;
            ret_v.multiP = ori_v.multiP;
            ret_v.plusP = ori_v.plusP;

            Vector2 a = InputActionsHandler.Instance.DirInput;
            ret_v.plusP = ret_v.plusP + a;
            Debug.Log(a);
            return ret_v;
        }
    }

    public class PlusMultiAttr_Vector2 : CharacterAttr_Vector2
    {
        public float multiP = 1;
        public Vector2 plusP;
        public override Vector2 GetValue()
        {
            PlusMultiAttr_Vector2 resultV = this;
            foreach (var st in attrDecorators)
            {
                resultV = (PlusMultiAttr_Vector2)st.GetAttr(resultV);
            }
            
            return (resultV.base_value + resultV.plusP) * resultV.multiP;
        }
    }




}