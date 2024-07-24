using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PolygonProject
{
    public class PlayerAttributePanel : BasePanel
    {
        static readonly string path="AssetPackage/GUI/Panel_PlayerAttribute";

        public PlayerAttributePanel():base(new UIType(path))
        {

        }

        TextMeshProUGUI text_Strength;
        TextMeshProUGUI text_Agility;
        TextMeshProUGUI text_Vigor;
        TextMeshProUGUI text_Dexterity;
        TextMeshProUGUI text_Will;
        TextMeshProUGUI text_Knowledge;
        TextMeshProUGUI text_HP;
        TextMeshProUGUI text_PhysicsHpRecovery;
        TextMeshProUGUI text_MagicHpRecovery;
        TextMeshProUGUI text_MagicCapacity;
        TextMeshProUGUI text_MoveSpeed;
        TextMeshProUGUI text_ActionSpeed;
        TextMeshProUGUI text_MagicSpeed;
        TextMeshProUGUI text_BuffDuration;
        TextMeshProUGUI text_DeBuffDuration;
        TextMeshProUGUI text_ArmorPenetration;
        TextMeshProUGUI text_MagicPenetration;
        TextMeshProUGUI text_HeadHitReduction;
        TextMeshProUGUI text_FlyingHitReduction;
        TextMeshProUGUI text_PhysicalHitReduction;
        TextMeshProUGUI text_MagicHitReduction;
        TextMeshProUGUI text_PhysicalStrengthBonus;
        TextMeshProUGUI text_MagicStrengthBonus;

        public override void Init()
        {
            text_Strength=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_Strength");
            text_Agility=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_Agility");
            text_Vigor=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_Vigor");
            text_Dexterity=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_Dexterity");
            text_Will=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_Will");
            text_Knowledge=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_Knowledge");
            text_HP=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_HP");
            text_PhysicsHpRecovery=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_PhysicsHpRecovery");
            text_MagicHpRecovery=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_MagicHpRecovery");
            text_MagicCapacity=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_MagicCapacity");
            text_MoveSpeed=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_MoveSpeed");
            text_ActionSpeed=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_ActionSpeed");
            text_MagicSpeed=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_MagicSpeed");
            text_BuffDuration=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_BuffDuration");
            text_DeBuffDuration=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_DeBuffDuration");
            text_ArmorPenetration=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_ArmorPenetration");
            text_MagicPenetration=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_MagicPenetration");
            text_HeadHitReduction=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_HeadHitReduction");
            text_FlyingHitReduction=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_FlyingHitReduction");
            text_PhysicalHitReduction=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_PhysicalHitReduction");
            text_MagicHitReduction=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_MagicHitReduction");
            text_PhysicalStrengthBonus=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_PhysicalStrengthBonus");
            text_MagicStrengthBonus=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_MagicStrengthBonus");
        }

        public void RefreshUI()
        {

        }
    }
}
