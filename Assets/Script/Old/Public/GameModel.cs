using UnityEngine;

/// <summary>
/// 小道具
/// </summary>
public class ConstString
{
    public static string item_HP = "item_HP";
    public static string item_MP = "item_MP";
    public static string item_Attack = "item_Attack";
    public static string item_Magic = "item_Magic";
    public static string item_ATKDefense = "item_ATKDefense";
    public static string item_MGDefense = "item_MGDefense";
    public static string nickName = "nickName1";
    public static string level = "level";
    public static string coin = "coin";
    public static string task = "task";
    public static string kongfu = "kongfu";
    public static string pos_Born = "pos_Born";
    public static string pos_PK = "pos_PK";

}

/// <summary>
/// 玩家游戏数据模型，存储游戏数据和配置
/// </summary>
public class GameInfo
{
    //购买道具所需金币，10000
    public static int PRICE = 10;

    //设置玩家昵称
    public static void SetPlayerName(string name)
    {
        PlayerPrefs.SetString("nickName", name);
    }

    //读取玩家昵称
    public static string GetPlayerName()
    {
        return PlayerPrefs.GetString("nickName");
    }

    //设置玩家级别
    public static void SetLevel(int level)
    {
        if (level > 4) { level = 4; }
        PlayerPrefs.SetInt("level", level);
    }

    //读取玩家级别
    public static int GetLevel()
    {
        return PlayerPrefs.GetInt("level");
    }

    //设置金币数量
    public static void SetCoin(int coin)
    {
        PlayerPrefs.SetInt("coin", coin);
        Panel_GameMainView.Instance.UpdateCoin();
    }

    //读取金币数量
    public static int GetCoin()
    {
        return PlayerPrefs.GetInt("coin");
    }

    //更新已经完成任务
    public static void SetTask(int task)
    {
        if (task > 3) { task = 3; }
        PlayerPrefs.SetInt("task", task);
    }

    //读取已经完成任务
    public static int GetTask()
    {
        return PlayerPrefs.GetInt("task");
    }

    //更新修炼武功
    public static void SetKongFu(int kongfu)
    {
        PlayerPrefs.SetInt("kongfu", kongfu);
    }

    //是否修炼了武功
    public static bool GetKongFu()
    {
        if (PlayerPrefs.GetInt("kongfu") > 0)
            return true;
        else return false;
    }

    //设置玩家坐标标识
    public static void SetPos(Vector3 pos)
    {
        PlayerPrefs.SetFloat("posX", pos.x);
        PlayerPrefs.SetFloat("posY", pos.y);
        PlayerPrefs.SetFloat("posZ", pos.z);
    }

    //读取玩家坐标标识
    public static Vector3 GetPos()
    {
        var pos=new Vector3();
        pos.x=PlayerPrefs.GetFloat("posX");
        pos.y=PlayerPrefs.GetFloat("posY");
        pos.z=PlayerPrefs.GetFloat("posZ");

        return pos;
    }

    //设置血量药水瓶数量
    public static void Set_Item_HP(int num)
    {
        PlayerPrefs.SetInt("item_HP", num);
    }

    //读取血量药水瓶数量
    public static int Get_Item_HP()
    {
        return PlayerPrefs.GetInt("item_HP");
    }

    //设置体力药水瓶数量
    public static void Set_Item_MP(int num)
    {
        PlayerPrefs.SetInt("item_MP", num);
    }

    //读取体力药水瓶数量
    public static int Get_Item_MP()
    {
        return PlayerPrefs.GetInt("item_MP");
    }

    //设置普通攻击力药水瓶数量
    public static void Set_Item_Attack(int num)
    {
        PlayerPrefs.SetInt("item_Attack", num);
    }

    //读取普通攻击力药水瓶数量
    public static int Get_Item_Attack()
    {
        return PlayerPrefs.GetInt("item_Attack");
    }

    //设置魔法攻击力药水瓶数量
    public static void Set_Item_Magic(int num)
    {
        PlayerPrefs.SetInt("item_Magic", num);
    }

    //读取魔法攻击力药水瓶数量
    public static int Get_Item_Magic()
    {
        return PlayerPrefs.GetInt("item_Magic");
    }

    //设置普通防御力药水瓶数量
    public static void Set_Item_ATKDefense(int num)
    {
        PlayerPrefs.SetInt("item_ATKDefense", num);
    }

    //读取普通防御力药水瓶数量
    public static int Get_Item_ATKDefense()
    {
        return PlayerPrefs.GetInt("item_ATKDefense");
    }

    //设置魔法防御力药水瓶数量
    public static void Set_Item_MGDefense(int num)
    {
        PlayerPrefs.SetInt("item_MGDefense", num);
    }

    //读取魔法防御力药水瓶数量
    public static int Get_Item_MGDefense()
    {
        return PlayerPrefs.GetInt("item_MGDefense");
    }
}
