using Excel;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class ExcelConfig
{
    /// <summary>
    /// 存放excel表文件夹的的路径，本例excel表放在了"Assets/Excel/"当中
    /// </summary>
    public static readonly string excelsFolderPath = Application.dataPath + "/Excel/";

    /// <summary>
    /// 存放Excel转化CS文件的文件夹路径
    /// </summary>
    public static readonly string assetPath = "Assets/Game/Resources/DataAssets/";

    /// <summary>
    /// 存放TableManager的文件夹路径
    /// </summary>
    public static readonly string TableManagerPath = "Assets/Game/Script/TableRead/";
}
