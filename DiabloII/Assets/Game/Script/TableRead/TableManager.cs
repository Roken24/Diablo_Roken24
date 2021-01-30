using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
 
public class TableManager
{
    static Dictionary<int, Tab_Tasks> TasksDic = null;
    public static Dictionary<int, Tab_Tasks> CreatTasksArrayWithExcel()
    {
        if (TasksDic != null) return TasksDic;
        TasksDic = new Dictionary<int, Tab_Tasks>();
        string filePath = ExcelConfig.excelsFolderPath + "Tasks.xlsx";
        int columnNum = 0, rowNum = 0;
        DataRowCollection collect = CreatTableClass.ReadExcel(filePath, ref columnNum, ref rowNum);
        Tab_Tasks[] array = new Tab_Tasks[rowNum - 4];
        for (int i = 4; i < rowNum; i++)
        {
            Tab_Tasks Tasks = new Tab_Tasks();
            Tasks.Id = int.Parse(collect[i][0].ToString());
            Tasks.Name = collect[i][1].ToString();
            Tasks.Act = int.Parse(collect[i][2].ToString());
            Tasks.Scene = int.Parse(collect[i][3].ToString());
            Tasks.Text = collect[i][4].ToString();
            Tasks.Passed = collect[i][5].ToString();
            TasksDic.Add(Tasks.Id, Tasks);
        }
        return TasksDic;
    }
  
    public static Tab_Tasks GetTasksById(int id)
    {
        Tab_Tasks ret = null;
        var TasksDic = CreatTasksArrayWithExcel();
        TasksDic.TryGetValue(id, out ret);
        return ret;
    }
 
    static Dictionary<int, Tab_TestItem> TestItemDic = null;
    public static Dictionary<int, Tab_TestItem> CreatTestItemArrayWithExcel()
    {
        if (TestItemDic != null) return TestItemDic;
        TestItemDic = new Dictionary<int, Tab_TestItem>();
        string filePath = ExcelConfig.excelsFolderPath + "TestItem.xlsx";
        int columnNum = 0, rowNum = 0;
        DataRowCollection collect = CreatTableClass.ReadExcel(filePath, ref columnNum, ref rowNum);
        Tab_TestItem[] array = new Tab_TestItem[rowNum - 4];
        for (int i = 4; i < rowNum; i++)
        {
            Tab_TestItem TestItem = new Tab_TestItem();
            TestItem.Id = int.Parse(collect[i][0].ToString());
            TestItem.Name = collect[i][1].ToString();
            TestItem.Price = int.Parse(collect[i][2].ToString());
            TestItemDic.Add(TestItem.Id, TestItem);
        }
        return TestItemDic;
    }
  
    public static Tab_TestItem GetTestItemById(int id)
    {
        Tab_TestItem ret = null;
        var TestItemDic = CreatTestItemArrayWithExcel();
        TestItemDic.TryGetValue(id, out ret);
        return ret;
    }
 
    static Dictionary<int, Tab_Scenes> ScenesDic = null;
    public static Dictionary<int, Tab_Scenes> CreatScenesArrayWithExcel()
    {
        if (ScenesDic != null) return ScenesDic;
        ScenesDic = new Dictionary<int, Tab_Scenes>();
        string filePath = ExcelConfig.excelsFolderPath + "Scenes.xlsx";
        int columnNum = 0, rowNum = 0;
        DataRowCollection collect = CreatTableClass.ReadExcel(filePath, ref columnNum, ref rowNum);
        Tab_Scenes[] array = new Tab_Scenes[rowNum - 4];
        for (int i = 4; i < rowNum; i++)
        {
            Tab_Scenes Scenes = new Tab_Scenes();
            Scenes.Id = int.Parse(collect[i][0].ToString());
            Scenes.Name = collect[i][1].ToString();
            Scenes.Icon = collect[i][2].ToString();
            Scenes.LvL1 = int.Parse(collect[i][3].ToString());
            Scenes.LvL2 = int.Parse(collect[i][4].ToString());
            Scenes.LvL3 = int.Parse(collect[i][5].ToString());
            ScenesDic.Add(Scenes.Id, Scenes);
        }
        return ScenesDic;
    }
  
    public static Tab_Scenes GetScenesById(int id)
    {
        Tab_Scenes ret = null;
        var ScenesDic = CreatScenesArrayWithExcel();
        ScenesDic.TryGetValue(id, out ret);
        return ret;
    }
 
}
