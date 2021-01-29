using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
 
public class TableManager
{
    static Dictionary<int, Tab_Item> ItemDic = null;
    public static Dictionary<int, Tab_Item> CreatItemArrayWithExcel()
    {
        if (ItemDic != null) return ItemDic;
        ItemDic = new Dictionary<int, Tab_Item>();
        string filePath = ExcelConfig.excelsFolderPath + "Item.xlsx";
        int columnNum = 0, rowNum = 0;
        DataRowCollection collect = CreatTableClass.ReadExcel(filePath, ref columnNum, ref rowNum);
        Tab_Item[] array = new Tab_Item[rowNum - 4];
        for (int i = 4; i < rowNum; i++)
        {
            Tab_Item Item = new Tab_Item();
            Item.Id = int.Parse(collect[i][0].ToString());
            Item.Name = collect[i][1].ToString();
            Item.Price = int.Parse(collect[i][2].ToString());
            ItemDic.Add(Item.Id, Item);
        }
        return ItemDic;
    }
  
    public static Tab_Item GetItemById(int id)
    {
        Tab_Item ret = null;
        var ItemDic = CreatItemArrayWithExcel();
        ItemDic.TryGetValue(id, out ret);
        return ret;
    }
 
    static Dictionary<int, Tab_Itemp> ItempDic = null;
    public static Dictionary<int, Tab_Itemp> CreatItempArrayWithExcel()
    {
        if (ItempDic != null) return ItempDic;
        ItempDic = new Dictionary<int, Tab_Itemp>();
        string filePath = ExcelConfig.excelsFolderPath + "Itemp.xlsx";
        int columnNum = 0, rowNum = 0;
        DataRowCollection collect = CreatTableClass.ReadExcel(filePath, ref columnNum, ref rowNum);
        Tab_Itemp[] array = new Tab_Itemp[rowNum - 4];
        for (int i = 4; i < rowNum; i++)
        {
            Tab_Itemp Itemp = new Tab_Itemp();
            Itemp.Id = int.Parse(collect[i][0].ToString());
            Itemp.Name = collect[i][1].ToString();
            Itemp.Price = int.Parse(collect[i][2].ToString());
            Itemp.Count = int.Parse(collect[i][3].ToString());
            ItempDic.Add(Itemp.Id, Itemp);
        }
        return ItempDic;
    }
  
    public static Tab_Itemp GetItempById(int id)
    {
        Tab_Itemp ret = null;
        var ItempDic = CreatItempArrayWithExcel();
        ItempDic.TryGetValue(id, out ret);
        return ret;
    }
 
}
