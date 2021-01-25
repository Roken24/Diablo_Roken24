using UnityEngine;
using UnityEditor;
using System.IO;

public class TestSaveSprite
{
    [MenuItem("Tools/导出精灵")]
    static void SaveSprite()
    {
        string resourcesPath = "Assets/Game/Resources/";
        string cur_outPath = "Texture/Atlas/";
        bool isSuccessSaved = false;
        foreach (Object obj in Selection.objects)
        {
            string selectionPath = AssetDatabase.GetAssetPath(obj);
            // 必须最上级是"Assets/Game/Resources/Texture/DiabloUITexture"
            if (selectionPath.StartsWith(resourcesPath))
            {
                string selectionExt = System.IO.Path.GetExtension(selectionPath);
                if (selectionExt.Length == 0)
                {
                    continue;
                }

                // 从路径"Assets/Game/Resources/Texture/Atlas/testUI.png"得到路径"Texture/Atlas/testUI"
                string loadPath = selectionPath.Remove(selectionPath.Length - selectionExt.Length);
                loadPath = loadPath.Substring(resourcesPath.Length);
                // 加载此文件下的所有资源
                Sprite[] sprites = Resources.LoadAll<Sprite>(loadPath);
                if (sprites.Length > 0)
                {
                    // 创建导出文件夹
                    string outPath = Application.dataPath + "/Game/Resources/Texture/DiabloUISprite/" + loadPath.Substring(cur_outPath.Length);
                    System.IO.Directory.CreateDirectory(outPath);
                    foreach (Sprite sprite in sprites)
                    {
                        Debug.Log(sprite.name);
                        // 创建单独的纹理
                        Texture2D tex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
                        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                            (int)sprite.textureRect.y, (int)sprite.textureRect.width,
                            (int)sprite.textureRect.height);
                        tex.SetPixels(pixels);
                        tex.Apply();

                        // 写入成PNG文件
                        System.IO.File.WriteAllBytes(outPath + "/" + sprite.name + ".png", tex.EncodeToPNG());
                    }
                    isSuccessSaved = true;
                }
            }
        }
        if (isSuccessSaved)
        {
            Debug.Log("SaveSprite Successed!");
        }
        else
        {
            Debug.LogError("SaveSprite Failed!");
        }
    }
}