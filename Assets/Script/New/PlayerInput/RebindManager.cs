using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using TMPro;

public class RebindManager : Singleton<RebindManager>
{
    public static PlayerInputAsset playerInputAsset;

    public static event Action rebindComplete;
    public static event Action rebindCanceled;
    public static event Action<InputAction, int> rebindStarted;

    /// <summary>
    /// 准备开始按键绑定（要先关闭输入集）
    /// </summary>
    /// <param name="actionName">动作名称</param>
    /// <param name="bindingIndex">绑定序号</param>
    /// <param name="excludeMouse">绑定按键是否包含鼠标</param>
    public static void StartRebind(string actionName, int bindingIndex, bool excludeMouse)
    {
        InputAction action = playerInputAsset.asset.FindAction(actionName);
        if (action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.Log("Couldn't find action or binding");
            return;
        }

        if (action.bindings[bindingIndex].isPartOfComposite)
        {
            var firstPartIndex = bindingIndex + 1;
            if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isPartOfComposite)
            DoRebind(action, bindingIndex, true, excludeMouse);
        }
        else
        {
            DoRebind(action, bindingIndex, false, excludeMouse);
        }
    }

    /// <summary>
    /// 开始绑定
    /// </summary>
    /// <param name="actionToRebind">要绑定的动作</param>
    /// <param name="bindingIndex">绑定序号</param>
    /// <param name="allCompositeParts">是否为组合类型</param>
    /// <param name="excludeMouse">是否包含鼠标</param>
    private static void DoRebind(InputAction actionToRebind, int bindingIndex, bool allCompositeParts, bool excludeMouse)
    {
        if (actionToRebind == null || bindingIndex < 0)
        {
            return;
        }

        //statusText.text = $"Press a Button";

        actionToRebind.Disable();

        var rebind = actionToRebind.PerformInteractiveRebinding(bindingIndex);

        rebind.OnComplete(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            if (allCompositeParts)
            {
                var nextBindingIndex = bindingIndex + 1;
               if (nextBindingIndex < actionToRebind.bindings.Count && actionToRebind.bindings[nextBindingIndex].isComposite)
                    DoRebind(actionToRebind, nextBindingIndex, allCompositeParts, excludeMouse);
            }

            //SaveBindingOverride(actionToRebind);
            rebindComplete?.Invoke();
        });

        rebind.OnCancel(operation =>
        {
            actionToRebind.Enable();
            operation.Dispose();

            rebindCanceled?.Invoke();
        });

        if (excludeMouse)
            rebind.WithControlsExcluding("Mouse");

        rebindStarted?.Invoke(actionToRebind, bindingIndex);
        //真正开始绑定
        rebind.Start();
    }

    /// <summary>
    /// 根据绑定动作的名称和绑定序号获取动作
    /// </summary>
    /// <param name="actionName"></param>
    /// <param name="bindingIndex"></param>
    /// <returns></returns>
    public static string GetBindingName(string actionName, int bindingIndex)
    {
        if (playerInputAsset == null)
            playerInputAsset = new PlayerInputAsset();

        InputAction action = playerInputAsset.asset.FindAction(actionName);
        return action.GetBindingDisplayString(bindingIndex);
    }


    
}
