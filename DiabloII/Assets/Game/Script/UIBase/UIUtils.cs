/********************************************************************
	created:	2021/1/20
	author:		Roken24
	purpose:	UI使用，方便操作
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 为UI操作提供基础封装，使UI操作更方便
/// </summary>
public static class UIUtils
{
    /// <summary>
    /// 设置一个UI元素是否可见
    /// </summary>
    /// <param name="ui"></param>
    /// <param name="value"></param>
    public static void SetActive(UIBehaviour ui, bool value)
    {
        if (ui != null && ui.gameObject != null)
        {
            ui.gameObject.SetActive(value);
        }
    }


    public static void SetButtonText(Button btn, string text)
    {
        Text objText = btn.transform.GetComponentInChildren<Text>();
        if (objText != null)
        {
            objText.text = text;
        }
    }

    public static string GetButtonText(Button btn)
    {
        Text objText = btn.transform.GetComponentInChildren<Text>();
        if (objText != null)
        {
            return objText.text;
        }
        return "";
    }

    public static void SetChildText(UIBehaviour ui, string text)
    {
        Text objText = ui.transform.GetComponentInChildren<Text>();
        if (objText != null)
        {
            objText.text = text;
        }
    }


    /// <summary>
    /// 方便寻找Panel上的UI控件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controlName"></param>
    /// <returns></returns>
    public static T Find<T>(MonoBehaviour parent, string controlName) where T : MonoBehaviour
    {
        Transform target = parent.transform.Find(controlName);
        if (target != null)
        {
            return target.GetComponent<T>();
        }
        else
        {
            Debug.LogError("未找到UI控件：{0}"+ controlName);
            return default(T);
        }
    }
}
