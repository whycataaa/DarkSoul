using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class PlayerBasePanelManager : MonoBehaviour
    {
        void Start()
        {
            PanelManager.Instance.PanelPush(new PlayerBasePanel());
        }
    }
}
