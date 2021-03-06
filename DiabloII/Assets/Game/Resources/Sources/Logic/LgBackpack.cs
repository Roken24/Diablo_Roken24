using UnityEngine;
using System.Collections;


public class LgBackpack : MonoBehaviour
{
    private GameObject selectedTile;


    void Start()
    {
        InitUIComponent(Global.LocalHero.charactor, transform);
    }

    void OnClose()
    {
        Game.ChangeScene("Main");
    }

    public static void InitUIComponent(RemoteChar charactor, Transform transform)
    {
        Transform root = transform.Find("Camera/Anchor/Panel/Backpack");

        if (Config.DataLoader.equipmentMaps.ContainsKey(charactor.armor))
        {
            UISprite sp = root.Find("Armor").GetComponent<UISprite>();
            sp.spriteName = Config.DataLoader.equipmentMaps[charactor.armor].icon;

            UISpriteData data = sp.atlas.GetSprite(sp.spriteName);
            sp.width = data.width;
            sp.height = data.height;

            UserData ud = sp.gameObject.AddComponent<UserData>();
            ud.data = charactor.armor.ToString();
        }

        if (Config.DataLoader.equipmentMaps.ContainsKey(charactor.headgear))
        {
            UISprite sp = root.Find("Headgear").GetComponent<UISprite>();
            sp.spriteName = Config.DataLoader.equipmentMaps[charactor.headgear].icon;

            //UISpriteData data = sp.atlas.GetSprite(sp.spriteName);
            //sp.width = data.width;
            //sp.height = data.height;

            UserData ud = sp.gameObject.AddComponent<UserData>();
            ud.data = charactor.headgear.ToString();
        }

        if (Config.DataLoader.equipmentMaps.ContainsKey(charactor.weaponL))
        {
            UISprite sp = root.Find("WeaponL").GetComponent<UISprite>();
            sp.spriteName = Config.DataLoader.equipmentMaps[charactor.weaponL].icon;

            UISpriteData data = sp.atlas.GetSprite(sp.spriteName);
            sp.width = data.width;
            sp.height = data.height;

            UserData ud = sp.gameObject.AddComponent<UserData>();
            ud.data = charactor.weaponL.ToString();
        }

        if (Config.DataLoader.equipmentMaps.ContainsKey(charactor.weaponR))
        {
            UISprite sp = root.Find("WeaponR").GetComponent<UISprite>();
            sp.spriteName = Config.DataLoader.equipmentMaps[charactor.weaponR].icon;

            UISpriteData data = sp.atlas.GetSprite(sp.spriteName);
            sp.width = data.width;
            sp.height = data.height;

            UserData ud = sp.gameObject.AddComponent<UserData>();
            ud.data = charactor.weaponR.ToString();
        }

        if (Config.DataLoader.equipmentMaps.ContainsKey(charactor.gloves))
        {
            UISprite sp = root.Find("Gloves").GetComponent<UISprite>();
            sp.spriteName = Config.DataLoader.equipmentMaps[charactor.gloves].icon;

            UserData ud = sp.gameObject.AddComponent<UserData>();
            ud.data = charactor.gloves.ToString();
        }

        if (Config.DataLoader.equipmentMaps.ContainsKey(charactor.boots))
        {
            UISprite sp = root.Find("Boots").GetComponent<UISprite>();
            sp.spriteName = Config.DataLoader.equipmentMaps[charactor.boots].icon;

            //UISpriteData data = sp.atlas.GetSprite(sp.spriteName);
            //sp.width = data.width;
            //sp.height = data.height;

            UserData ud = sp.gameObject.AddComponent<UserData>();
            ud.data = charactor.boots.ToString();
        }

        if (Config.DataLoader.equipmentMaps.ContainsKey(charactor.amulets))
        {
            UISprite sp = root.Find("Amulets").GetComponent<UISprite>();
            sp.spriteName = Config.DataLoader.equipmentMaps[charactor.amulets].icon;
            sp.gameObject.SetActive(true);

            UserData ud = sp.gameObject.AddComponent<UserData>();
            ud.data = charactor.amulets.ToString();
        }

        if (Config.DataLoader.equipmentMaps.ContainsKey(charactor.belts))
        {
            UISprite sp = root.Find("Belts").GetComponent<UISprite>();
            sp.spriteName = Config.DataLoader.equipmentMaps[charactor.belts].icon;

            UserData ud = sp.gameObject.AddComponent<UserData>();
            ud.data = charactor.belts.ToString();
        }

        if (Config.DataLoader.equipmentMaps.ContainsKey(charactor.ring))
        {
            UISprite sp = root.Find("Ring").GetComponent<UISprite>();
            sp.spriteName = Config.DataLoader.equipmentMaps[charactor.ring].icon;
            sp.gameObject.SetActive(true);

            UserData ud = sp.gameObject.AddComponent<UserData>();
            ud.data = charactor.ring.ToString();
        }

        UILabel nameLab = root.Find("NameLab").GetComponent<UILabel>();
        nameLab.text = charactor.name;

        UILabel profLab = root.Find("ProfTxt").GetComponent<UILabel>();
        profLab.text = Global.Profession(charactor.profession);

        UILabel lvlLab = root.Find("LvlTxt").GetComponent<UILabel>();
        lvlLab.text = charactor.level.ToString();

        UILabel hpLab = root.Find("HpTxt").GetComponent<UILabel>();
        hpLab.text = Config.CharAttribute.HP(charactor).ToString();

        UILabel rankLab = root.Find("RankTxt").GetComponent<UILabel>();
        rankLab.text = charactor.rank.ToString();

        UILabel phyxLab = root.Find("PhyxTxt").GetComponent<UILabel>();
        phyxLab.text = Config.CharAttribute.DamagePhyx(charactor).ToString();

        UILabel defLab = root.Find("DefenceTxt").GetComponent<UILabel>();
        defLab.text = Config.CharAttribute.Defence(charactor).ToString();
    }

    void OnClickTip(GameObject obj)
    {
        UserData ud = obj.GetComponent<UserData>();
        if (ud == null)
            return;

        ushort eid = ushort.Parse(ud.data);
        if (!Config.DataLoader.equipmentMaps.ContainsKey(eid))
            return;

        if (selectedTile != obj)
        {
            UITooltip.ShowText(Global.CombEquipmentTips(Global.LocalHero.charactor, eid, true, false));

            Transform root = transform.Find("Camera/Anchor/Panel/Tooltip_UI");
            UISprite bg = root.GetComponent<UISprite>();

            UILabel lab = root.Find("Label").GetComponent<UILabel>();
            lab.transform.localPosition = new Vector2(0, -6);
            bg.width = Mathf.Max(bg.width + 8, 100);
            bg.height += 16;

            float dw = root.localPosition.x + bg.width - Screen.width / 2;
            float dh = Mathf.Abs(root.localPosition.y - bg.height) - Screen.height / 2;
            if (dh > 0)
                dh += root.localPosition.y;
            else
                dh = root.localPosition.y;

            if (dw > 0)
                dw = root.localPosition.x + bg.width / 2 - dw;
            else
                dw = root.localPosition.x + bg.width / 2;

            root.localPosition = new Vector3(dw, dh);

            selectedTile = obj;
        }
    }

    void OnCancelTip()
    {
        UITooltip.ShowText(null);
        selectedTile = null;
    }
}