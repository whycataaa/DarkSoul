using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolygonProject
{
    public class ShowItemPanel : BasePanel
    {
        #region 单例
        static ShowItemPanel instance;
        public static ShowItemPanel Instance
        {
            get
            {
                if(instance==null)
                {
                    instance=Activator.CreateInstance<ShowItemPanel>();
                }
                return instance;
            }
        }
        #endregion
        public bool IsOpen=false;
        static readonly string path="AssetPackage/GUI/Panel_ShowItem";
        public ShowItemPanel():base(new UIType(path)){}


        #region UI
        TextMeshProUGUI text_Name;
        TextMeshProUGUI text_Attribute;
        TextMeshProUGUI text_Class;
        TextMeshProUGUI text_Info;
        #endregion

        public override void OnEnter()
        {
            text_Name=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_Name");
            text_Attribute=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_Attribute");
            text_Class=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_Class");
            text_Info=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_Info");
            IsOpen=true;
        }

        public void RefreshUI(int itemID)
        {
            text_Name.text=DataBoard.Instance.ItemDic[itemID].Name;
            switch(DataBoard.Instance.BagItemDic[itemID].item.Quality)
            {
                case EQuality.Common:
                    text_Name.color=Color.white;
                    break;
                case EQuality.Rare:
                    text_Name.color=Color.green;
                    break;
                case EQuality.Epic:
                    ColorUtility.TryParseHtmlString("#A020F0", out Color nowColor);
                    text_Name.color=nowColor;
                    break;
                case EQuality.Legendary:
                    text_Name.color=Color.yellow;
                    break;
            }
            Debug.Log(DataBoard.Instance.BagItemDic[itemID].AttributeDic.Count);
            List<string> Strings=new List<string>();
            foreach(var item in DataBoard.Instance.BagItemDic[itemID].AttributeDic)
            {
                Debug.Log(item.Key);
                switch(item.Key)
                {
                    case EAttribute.AddHP:
                        Strings.Add("生命值 "+item.Value.ToString());
                        break;
                    case EAttribute.AddMagic:
                        Strings.Add("魔法值："+item.Value.ToString());
                        break;
                    case EAttribute.AddStamina:
                        Strings.Add("体力值："+item.Value.ToString());
                        break;
                    case EAttribute.AddStrength:
                        Strings.Add("力量："+item.Value.ToString());
                        break;
                    case EAttribute.AddVigor:
                        Strings.Add("活力："+item.Value.ToString());
                        break;
                    case EAttribute.AddAgility:
                        Strings.Add("敏捷："+item.Value.ToString());
                        break;
                    case EAttribute.AddDexterity:
                        Strings.Add("灵巧："+item.Value.ToString());
                        break;
                    case EAttribute.AddKnowledge:
                        Strings.Add("知识："+item.Value.ToString());
                        break;
                    case EAttribute.AddWill:
                        Strings.Add("意志："+item.Value.ToString());
                        break;
                    case EAttribute.AddMagicPenetration:
                        Strings.Add("魔法穿透："+item.Value.ToString());
                        break;
                    case EAttribute.AddMagicStrengthBonus:
                        Strings.Add("魔法伤害加成："+item.Value.ToString());
                        break;
                    case EAttribute.AddMoveSpeed:
                        Strings.Add("移动速度："+item.Value.ToString());
                        break;
                    case EAttribute.AddPhysicalPenetration:
                        Strings.Add("物理穿透："+item.Value.ToString());
                        break;
                    case EAttribute.AddPhysicalStrengthBonus:
                        Strings.Add("物理伤害加成："+item.Value.ToString());
                        break;
                    case EAttribute.AddWeaponDamage:
                        Strings.Add("武器伤害："+item.Value.ToString());
                        break;
                }
            }
            text_Attribute.text=string.Join("\n",Strings);
            text_Class.text="栏位类别："+"\n"+"护甲类别："+"\n"+"稀有度：";
            text_Info.text=DataBoard.Instance.ItemDic[itemID].Info;
        }
        public override void OnExit()
        {
            base.OnExit();
            IsOpen=false;
        }
    }
}
