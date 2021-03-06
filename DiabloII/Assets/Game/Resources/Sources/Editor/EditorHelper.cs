using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;


public class EditorHelper : MonoBehaviour 
{
    [MenuItem("Tools/Build Game")]
    public static void BuildGame()
    {
        string verFile = Application.dataPath + "/Sources/Logic/LgAbout.cs";

        StreamReader sr = File.OpenText(verFile);
        string txt = sr.ReadToEnd();
        sr.Close();

        string vKey = "version = \"版本：";
        int begin = txt.IndexOf(vKey);
        int end = txt.IndexOf("\";",begin+vKey.Length);
        string sub = txt.Substring(begin + vKey.Length, end - begin - vKey.Length);

        if (sub != PlayerSettings.bundleVersion)
        {
            txt = txt.Replace(sub, PlayerSettings.bundleVersion);

            StreamWriter sw = new StreamWriter(verFile, false, System.Text.Encoding.UTF8);
            sw.Write(txt);
            sw.Close();

            AssetDatabase.Refresh();
        }

        // Get filename.
        string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");

        path += "/DiabloII_v" + PlayerSettings.bundleVersion + ".apk";

        string[] levels = { "Assets/start.unity" };

        // Build player.
        BuildPipeline.BuildPlayer(levels, path, BuildTarget.Android, BuildOptions.None);
    }
}