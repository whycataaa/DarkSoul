using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class PlayerAttributePanelManager : MonoBehaviour
    {
        PlayerData playerData;
        PlayerAttributePanel playerAttributePanel;
        void Start()
        {
            playerData.Load();
            playerAttributePanel.Init();
            playerAttributePanel.RefreshUI();
        }


    }
}
