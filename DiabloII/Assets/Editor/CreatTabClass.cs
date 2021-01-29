using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreatTabClass : MonoBehaviour {

    [MenuItem("Tools/CreatTableClass")]
    static void Begin()
    {
       CreatTableClass.BeginCreatTableClass();
       CreatTableClass.BeginCreatTableManager();
    }
}
