using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class ChosePanelManager : MonoBehaviour
    {
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            PanelManager.Instance.PanelPush(ChosePanel.Instance);
            ChosePanel.Instance.Init();
            PanelManager.Instance.PanelPop();
        }
    }
}
