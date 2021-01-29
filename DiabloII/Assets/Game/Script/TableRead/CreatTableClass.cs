using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Data;
using Excel;
//using Microsoft.CSharp;
public class CreatTableClass
{
    /// <summary>
    /// 生成“Tab_XXX”的类，每个表对应一个cs文件
    /// </summary>
    /// <param name="fileName">表格名称</param>
    /// <param name="TableData">表格数据</param>
    /// <param name="columnNum">表格列数</param>
    public static void CreatNewClass(string fileName, DataRowCollection TableData, int columnNum)
    {
        string FileName = "Tab_" + fileName;
        string FilePath = "Assets/Game/Script/TableScript/" + FileName + ".cs";
        StringBuilder sbCode = new StringBuilder();
        sbCode.AppendLine("using System.Collections;");
        sbCode.AppendLine("using System.Collections.Generic;");
        sbCode.AppendLine("using UnityEngine;");
        sbCode.AppendLine(" ");
        sbCode.AppendLine("public class Tab_"+fileName);
        sbCode.AppendLine("{");
        for (int i = 0; i < columnNum; i++)
        {
            string VariableName = TableData[0][i].ToString();
            string VariableType = TableData[1][i].ToString();
            sbCode.AppendLine("    public " + VariableType + " " + VariableName + ";");
        }
        sbCode.AppendLine("}");
        File.WriteAllText(FilePath, sbCode.ToString());
        Debug.Log(FileName + ".cs文件生成完毕。");
    }
    /// <summary> 
    /// 开始生成表格的类
    /// </summary>
    public static void BeginCreatTableClass()
    {
        List<string> filePathList = GetFilePathList(ExcelConfig.excelsFolderPath);//拿到所有文件路径，里面包括".xlsx"和".xlsx.meta"结尾的路径
        for (int i = 0; i < filePathList.Count; i++)
        {
            string selectionExt = System.IO.Path.GetExtension(filePathList[i]);//拿到文件后缀".xlsx"和".xlsx.meta"
            if (selectionExt != ".xlsx")
            {
                continue;
            }
            else
            {
                //获得表数据
                int columnNum = 0, rowNum = 0;
                DataRowCollection collect = ReadExcel(filePathList[i], ref columnNum, ref rowNum);
                string FileName = filePathList[i].Remove(filePathList[i].Length - selectionExt.Length);
                FileName = FileName.Substring(ExcelConfig.excelsFolderPath.Length);
                //开始生成类
                CreatNewClass(FileName, collect, columnNum);
            }
        }
    }
    /// <summary>
    /// 生成TableManager.cs
    /// </summary>
    /// <param name="sbCode">单列</param>
    /// <param name="fileName"></param>
    /// <param name="TableData"></param>
    /// <param name="columnNum"></param>
    public static void CreatTableManager(StringBuilder sbCode, string fileName, DataRowCollection TableData, int columnNum)
    {
        string FileName = "Tab_" + fileName;
        sbCode.AppendLine("    static Dictionary<int, "+FileName+"> "+fileName+"Dic = null;");
        sbCode.AppendLine("    public static Dictionary<int, "+ FileName + "> Creat"+ fileName + "ArrayWithExcel()");
        sbCode.AppendLine("    {");
        sbCode.AppendLine("        if ("+ fileName + "Dic != null) return "+ fileName + "Dic;");
        sbCode.AppendLine("        "+ fileName + "Dic = new Dictionary<int, "+ FileName + ">();");
        sbCode.AppendLine("        string filePath = ExcelConfig.excelsFolderPath + "+"\"" + fileName + ".xlsx"+"\";");
        sbCode.AppendLine("        int columnNum = 0, rowNum = 0;");
        sbCode.AppendLine("        DataRowCollection collect = CreatTableClass.ReadExcel(filePath, ref columnNum, ref rowNum);");
        sbCode.AppendLine("        " + FileName + "[] array = new " + FileName + "[rowNum - 4];");
        sbCode.AppendLine("        for (int i = 4; i < rowNum; i++)");
        sbCode.AppendLine("        {");
        sbCode.AppendLine("            " + FileName + " " + fileName + " = new " + FileName + "();");
        for (int j = 0; j < columnNum; j++)
        {
            string VariableName = TableData[0][j].ToString();
            string VariableType = TableData[1][j].ToString();
            if (VariableType == "int")
            {
                sbCode.AppendLine("            " + fileName + "." + VariableName + " = int.Parse(collect[i][" + j + "].ToString());");
            }
            else
            {
                sbCode.AppendLine("            " + fileName + "." + VariableName + " = collect[i][" + j + "].ToString();");
            }
        }
        sbCode.AppendLine("            "+ fileName + "Dic.Add("+ fileName + ".Id, "+ fileName + ");");
        sbCode.AppendLine("        }");
        sbCode.AppendLine("        return "+ fileName + "Dic;");
        sbCode.AppendLine("    }");
        //生成拿到表格单列方法
        sbCode.AppendLine("  ");
        sbCode.AppendLine("    public static " + FileName + " Get" + fileName + "ById(int id)");
        sbCode.AppendLine("    {");
        sbCode.AppendLine("        " + FileName + " ret = null;");
        sbCode.AppendLine("        var " + fileName + "Dic = Creat"+ fileName + "ArrayWithExcel();");
        sbCode.AppendLine("        " + fileName + "Dic.TryGetValue(id, out ret);");
        sbCode.AppendLine("        return ret;");
        sbCode.AppendLine("    }");
        sbCode.AppendLine(" ");
    }
    /// <summary> 
    /// 开始生成TableManager.cs
    /// </summary>
    public static void BeginCreatTableManager()
    {
        List<string> filePathList = GetFilePathList(ExcelConfig.excelsFolderPath);//拿到所有文件路径，里面包括".xlsx"和".xlsx.meta"结尾的路径
        StringBuilder sbCode = new StringBuilder();
        sbCode.AppendLine("using System.Collections;");
        sbCode.AppendLine("using System.Collections.Generic;");
        sbCode.AppendLine("using UnityEngine;");
        sbCode.AppendLine("using System.Data;");
        sbCode.AppendLine(" ");
        sbCode.AppendLine("public class TableManager");
        sbCode.AppendLine("{");
        for (int i = 0; i < filePathList.Count; i++)
        {
            string selectionExt = System.IO.Path.GetExtension(filePathList[i]);//拿到文件后缀".xlsx"和".xlsx.meta"
            if (selectionExt != ".xlsx")
            {
                continue;
            }
            else
            {
                //获得表数据
                int columnNum = 0, rowNum = 0;
                DataRowCollection collect = ReadExcel(filePathList[i], ref columnNum, ref rowNum);
                string FileName = filePathList[i].Remove(filePathList[i].Length - selectionExt.Length);
                FileName = FileName.Substring(ExcelConfig.excelsFolderPath.Length);
                //开始生成TableManager
                CreatTableManager(sbCode, FileName, collect, columnNum);
            }
        }
        sbCode.AppendLine("}");
        File.WriteAllText(ExcelConfig.TableManagerPath+"TableManager.cs", sbCode.ToString());
        Debug.Log("TableManager.cs文件生成完毕。");
    }
    /// <summary>
    /// 读取excel文件内容
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="columnNum">列数</param>
    /// <param name="rowNum">行数</param>
    /// <returns></returns>
    public static DataRowCollection ReadExcel(string filePath, ref int columnNum, ref int rowNum)
    {
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelReader.AsDataSet();
        //Tables[0] 下标0表示excel文件中第一张表的数据
        columnNum = result.Tables[0].Columns.Count;
        rowNum = result.Tables[0].Rows.Count;
        return result.Tables[0].Rows;
    }

    /// <summary>
    /// 获取文件夹下的所有table路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static List<string> GetFilePathList(string path)
    {
        List<string> list = new List<string>();
        string[] arr = Directory.GetFileSystemEntries(path);
        for (int i = 0; i < arr.Length; i++)
        {
            if (File.GetAttributes(arr[i]).CompareTo(FileAttributes.Directory) > 0)
            {
                list.Add(arr[i]);
                Debug.Log(arr[i]);
            }
        }
        return list;
    }
}
