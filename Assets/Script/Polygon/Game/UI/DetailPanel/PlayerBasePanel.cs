using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class PlayerBasePanel : BasePanel
    {
        static readonly string path="AssetPackage/GUI/Panel_PlayerBase";
        public PlayerBasePanel():base(new UIType(path)){}
    }
}
