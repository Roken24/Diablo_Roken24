using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChajian : MonoBehaviour
{
    public void onclick()
    {
        int id = 1;
        Tab_Tasks tasks = TableManager.GetTasksById(id);
        if (tasks == null)
        {
            return;
        }
        Debug.Log("===" + tasks.Id + "===" + tasks.Name + "===" + tasks.Text);
        Debug.Log("===" + tasks.Scene + "===" + tasks.Passed);
    }
}
