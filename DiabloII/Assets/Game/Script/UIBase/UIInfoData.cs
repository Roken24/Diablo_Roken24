/********************************************************************
	created:	2021/1/19
	author:		Roken24
	purpose:	UI注册数据类，所有UI信息需要在此文件中添加才能被使用
*********************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum UITypeDef
{
    Unkown = 0,
    Page = 1,
    Window = 2,
    Widgets = 3,
    Loading = 4
}


public class UILayerDef
{
    public const int Background = 0;
    public const int Page = 1000;//-1999
    public const int NormalWindow = 2000;//-2999
    public const int TopWindow = 3000;//-3999
    public const int Widgets = 4000;//-4999
    public const int Loading = 5000;
    public const int Unkown = 9999;

    public static int GetDefaultLayer(UITypeDef type)
    {
        switch (type)
        {
            case UITypeDef.Loading: return Loading;
            case UITypeDef.Widgets: return Widgets;
            case UITypeDef.Window: return NormalWindow;
            case UITypeDef.Page: return Page;
            case UITypeDef.Unkown: return Unkown;
            default: return Unkown;
        }
    }
}
//public class UIPathData
//{
//    public enum UIType
//    {
//        TYPE_ITEM,          // 只是用资源路径
//        TYPE_BASE,          // 常驻场景的UI，Close不销毁 一级UI
//        TYPE_POP,           // 弹出式UI，互斥，当前只能有一个弹出界面 二级弹出 在一级之上 阻止移动 无法操作后面UI,隐藏Base
//        TYPE_POPCENTER,     // 弹出式UI，与POP及POPCENTER互斥，当前只能有一个弹出界面 二级弹出 在一级之上 阻止移动 无法操作后面UI不隐藏Base
//        TYPE_STORY,         // 故事界面，故事界面出现，所有UI消失，故事界面关闭，其他界面恢复
//        TYPE_TIP,           // 三级弹出 在二级之上 不互斥 阻止移动 无法操作后面UI
//        TYPE_MESSAGE,       // 消息提示UI 在三级之上 一般是最高层级 不互斥 不阻止移动 可操作后面UI
//        TYPE_DEATH,         // 死亡UI 目前只有复活界面 用于添加复活特殊规则
//        TYPE_MESSAGEBOX,    // MessageBox专属UI类型 类型内互斥 其他规则和Message完全一致
//        TYPE_FLOAT,         // 悬浮层
//        TYPE_ROOT,          // 在UI节点下,特殊UI
//        TYPE_STORYMESSAGE,  // 规则同TYPE_MESSAGE，在TYPE_STORY打开的时候不会被关闭
//    };

//    public enum E_UIDestroyOnUnloadType
//    { // 这个ui关闭时应该怎么处理
//        e_DonotDestroyOnUnlad = 0, // 不销毁, 只失效
//        e_DestroyOnUnload = 1, // 销毁
//        e_DesrtoyOnOpenOtherPop = 2, // 在打开另外一个popUI的时候销毁（只能PopUI使用）
//        /// <summary>
//        /// 缓存到容量的池子里(薛定谔的销毁，注意初始化逻辑)
//        /// </summary>
//    }

//    public static Dictionary<string, UIPathData> m_DicUIName = new Dictionary<string, UIPathData>();

//    // bDestroyOnUnload 只对PopUI起作用
//    public UIPathData(string _bundleName, UIType _uiType, string _groupSubName = "", E_UIDestroyOnUnloadType eDestroyOnUnload = E_UIDestroyOnUnloadType.e_DestroyOnUnload, bool _isCloseChangeScene = true)
//    {
//        path = _bundleName;
//        if(!string.IsNullOrEmpty(_groupSubName))
//        {
//            path += "/" + _groupSubName;
//        }
//        uiType = _uiType;

//        groupSubName = _groupSubName;
//        bundleName = _bundleName.ToLower();
//        isDestroyOnUnload = eDestroyOnUnload;
//        isCloseChangeScene = _isCloseChangeScene;
//        m_DicUIName.Add(path, this);
//    }



//    public string GetUIName()
//    {
//        string name = string.IsNullOrEmpty(groupSubName) ? bundleName : groupSubName;
//        return name.Replace('/', '.');
//    }

//    public string path;
//    public string bundleName;
//    public string groupSubName;
//    public UIType uiType;
//    public E_UIDestroyOnUnloadType isDestroyOnUnload;
//    public bool isCloseChangeScene;
//    public string forceParentSubPath;
//}

public class UIInfo
{
    public static string UIResRoot = "ui/";

    /// <summary>
    /// 加载UI的Prefab
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject LoadPrefab(string name)
    {
        GameObject asset = (GameObject)Resources.Load(UIResRoot + name);
        return asset;
    }
    //root
#region
    //public static UIPathData SceneLoading = new UIPathData("SceneLoading", UIPathData.UIType.TYPE_ROOT);
    //public static UIPathData LoginRoot = new UIPathData("LoginRoot", UIPathData.UIType.TYPE_ROOT);
    //public static UIPathData ServerChoose = new UIPathData("Login/ServerChooseWindow", UIPathData.UIType.TYPE_ROOT);
    //public static UIPathData LaunchLoading = new UIPathData("loadgameatlas/LaunchLoadingWindow", UIPathData.UIType.TYPE_ROOT);
    //public static UIPathData RoleChoose = new UIPathData("Login/RoleChooseWindow", UIPathData.UIType.TYPE_ROOT);
    //public static UIPathData RoleCreate = new UIPathData("Login/RoleCreateWindow", UIPathData.UIType.TYPE_ROOT);
    //public static UIPathData RaceSelect = new UIPathData("Login/RaceSelectWindow", UIPathData.UIType.TYPE_ROOT);
    //public static UIPathData AppearanceCreate = new UIPathData("Login/AppearanceCreateWindow", UIPathData.UIType.TYPE_ROOT);
    //public static UIPathData NamePlayer = new UIPathData("Login/NamePlayer", UIPathData.UIType.TYPE_ROOT);
    //public static UIPathData WaitUpdate = new UIPathData("Login/WaitUpdateWindow", UIPathData.UIType.TYPE_ROOT);
#endregion
}