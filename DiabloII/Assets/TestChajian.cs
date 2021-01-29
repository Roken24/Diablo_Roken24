using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestChajian : MonoBehaviour
{
    public void onclick()
    {
        int id = 1;
        Tab_Item item = TableManager.GetItemById(id);
        if (item == null)
        {
            return;
        }
        Debug.Log("===" + item.Id + "===" + item.Name + "===" + item.Price);
    }
}
