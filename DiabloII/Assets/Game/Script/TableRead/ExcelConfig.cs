using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelConfig
{
    /// <summary>
    /// 存放excel表文件夹的的路径，本例excel表放在了"Assets/Excel/"当中
    /// </summary>
    public static readonly string excelsFolderPath = Application.dataPath + "/Excel/";
    /// <summary>
    /// 存放表格对应类的cs文件路径
    /// </summary>
    public static readonly string TableClassPath = "Assets/Game/Script/TableScript/";
    /// <summary>
    /// 存放TableManager的文件夹路径
    /// </summary>
    public static readonly string TableManagerPath = "Assets/Game/Script/TableRead/";
}
