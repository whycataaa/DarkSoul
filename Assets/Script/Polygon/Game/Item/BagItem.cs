using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class BagItem
    {
        public BagItem(IItem _item,int _Num)
        {
            item=_item;
            Num=_Num;
            BaseAttribute=new BaseAttribute();

            AttributeDic=new Dictionary<EAttribute,float>();
            foreach(var item in AttributeDic)
            {
                switch(item.Key)
                {
                    case EAttribute.AddHP:
                        BaseAttribute.OtherHp= (int)item.Value;
                        break;
                    case EAttribute.AddMagic:
                        BaseAttribute.OtherMp= (int)item.Value;
                        break;
                    case EAttribute.AddStamina:
                        BaseAttribute.OtherStamina= (int)item.Value;
                        break;
                    case EAttribute.AddStrength:
                        BaseAttribute.Strength= (int)item.Value;
                        break;
                    case EAttribute.AddDexterity:
                        BaseAttribute.Dexterity= (int)item.Value;
                        break;
                    case EAttribute.AddAgility:
                        BaseAttribute.Agility= (int)item.Value;
                        break;
                    case EAttribute.AddKnowledge:
                        BaseAttribute.Knowledge= (int)item.Value;
                        break;
                    case EAttribute.AddVigor:
                        BaseAttribute.Vigor= (int)item.Value;
                        break;
                    case EAttribute.AddWill:
                        BaseAttribute.Will= (int)item.Value;
                        break;
                    case EAttribute.AddMagicPenetration:
                        BaseAttribute.MagicPenetration= (int)item.Value;
                        break;
                    case EAttribute.AddPhysicalPenetration:
                        BaseAttribute.PhysicalPenetration= (int)item.Value;
                        break;
                    case EAttribute.AddMoveSpeed:
                        BaseAttribute.MoveSpeed= (int)item.Value;
                        break;
                    case EAttribute.AddPhysicalStrengthBonus:
                        BaseAttribute.PhysicalPowerBonus= (int)item.Value;
                        break;
                    case EAttribute.AddMagicStrengthBonus:
                        BaseAttribute.MagicPowerBonus= (int)item.Value;
                        break;
                    case EAttribute.AddWeaponDamage:
                        BaseAttribute.WeaponDamage= (int)item.Value;
                        break;

                }
            }
            //默认没有被装备
            ItemEquipState=EItemEquipState.Unequipped;
        }
        public IItem item;
        public int num;
        public int Num
        {
            get
            {
                return num;
            }
            set
            {
                num=value;

                Debug.Log("add"+value);

            }
        }
        public BaseAttribute BaseAttribute;

        //属性->属性值
        public Dictionary<EAttribute,float> AttributeDic;

        public EItemEquipState ItemEquipState;
    }



}
