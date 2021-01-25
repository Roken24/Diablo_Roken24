using UnityEngine;
using System.Collections;


public class LgRank : MonoBehaviour
{

    void Start()
    {

        for (int i = 0; i < Global.RankHeros.Count; i++)
        {
            string root = "Camera/Anchor/Panel/Player" + (i + 1) + "/Root";

            Transform ts = transform.Find(root);
            ts.gameObject.SetActive(true);

            ts = transform.Find(root + "/Level");
            UILabel lab = ts.GetComponent<UILabel>();
            lab.text = "等级:" + Global.RankHeros[i].charactor.level;

            ts = transform.Find(root + "/Name");
            lab = ts.GetComponent<UILabel>();
            lab.text = Global.RankHeros[i].charactor.name;

            ts = transform.Find(root + "/Rank");
            lab = ts.GetComponent<UILabel>();
            lab.text = "排名:[EAEE00]" + Global.RankHeros[i].charactor.rank + "[-]";

            ts = transform.Find(root + "/Player");
            UISprite spr = ts.GetComponent<UISprite>();
            spr.spriteName = Global.CharIcon[(int)Global.RankHeros[i].charactor.profession];
        }

        UILabel curRank = transform.Find("Camera/Anchor/Panel/CurrRank").GetComponent<UILabel>();
        curRank.text = "当前排名：" + Global.LocalHero.charactor.rank;
    }

    void OnClose()
    {
        Game.ChangeScene("Main");
    }

    void OnViewing(GameObject arg)
    {
        string str = arg.transform.parent.parent.name;
        int index = int.Parse(str.Replace("Player", ""));

        Global.OtherHero = Global.RankHeros[index - 1];

        Game.ChangeScene("OtherProp");
    }

    void OnFighting(GameObject arg)
    {
        string str = arg.transform.parent.parent.name;
        int index = int.Parse(str.Replace("Player", ""));

        Global.OtherHero = Global.RankHeros[index - 1];

        Game.ChangeScene("DarePvP", 0, false);
    }
}