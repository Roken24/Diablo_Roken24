/********************************************************************
	created:	2021/1/20
	author:		Roken24
	purpose:	UIWindow，共用窗口
*********************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindow : UIBase{
    public UITypeDef UIType { get { return UITypeDef.Window; } }

    [SerializeField]
    private UIButton m_btnClose;

    /// <summary>
    /// 当UI可用时调用
    /// </summary>
    protected override void OnEnable()
    {
        AddUIClickListener(m_btnClose.name, OnBtnClose);
    }

    /// <summary>
    /// 当UI不可用时调用
    /// </summary>
    protected override void OnDisable()
    {
        RemoveUIClickListeners(m_btnClose.name);
    }

    private void OnBtnClose()
    {
        Close(0);
    }
}
