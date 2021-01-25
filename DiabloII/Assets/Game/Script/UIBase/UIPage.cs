/*****************************************************************
    created:	2021/1/20
	author:		Roken24
	purpose:	UI页面，主界面
*****************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPage : UIBase{
    public UITypeDef UIType { get { return UITypeDef.Page; } }
    /// <summary>
    /// 返回按钮，大部分Page都会有返回按钮
    /// </summary>
    [SerializeField]
    private UIButton m_btnGoBack;

    /// <summary>
    /// 当UIPage被激活时调用  
    /// </summary>
    protected override void OnEnable()
    {
        AddUIClickListener(m_btnGoBack.name, OnBtnGoBack);
    }
    /// <summary>
    /// 关闭按钮，点击返回时调用
    /// 不是每个page都有返回按钮
    /// </summary>
    private void OnBtnGoBack()
    {
        UIManager.Instance().GoBackPage();
    }
    /// <summary>
    /// 当UI不可调用时
    /// </summary>
    protected override void OnDisable()
    {
        RemoveUIClickListeners(m_btnGoBack.name);
    }
}
