using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class PlayerData
    {
        #region Fields
        //总等级
        int level = 0;
        //金币数量
        int coin = 0;
        //生命等级
        int healthLevel=0;
        //魔法等级
        int magicLevel=0;
        //体力等级
        int staminaLevel=0;
        class SaveData
        {
            public int playerLevel;
            public int playerCoin;
            public int playerHealthLevel;
            public int playerMagicLevel;
            public int playerStaminaLevel;
        }

        const string PLAYER_DATA_KEY = "PlayerData";
        const string PLAYER_DATA_FILE_NAME = "PlayerData.sav";
        #endregion



        #region Save and Load

        public void Save()
        {
            SaveByJson();
        }

        public void Load()
        {
            LoadFromJson();
        }

        #endregion

        #region PlayerPrefs

        void SaveByPlayerPrefs()
        {
            SaveSystem.SaveByPlayerPrefs(PLAYER_DATA_KEY, SavingData());
        }

        void LoadFromPlayerPrefs()
        {
            var json = SaveSystem.LoadFromPlayerPrefs(PLAYER_DATA_KEY);
            var saveData = JsonUtility.FromJson<SaveData>(json);
            LoadData(saveData);
        }

        #endregion

        #region JSON

        void SaveByJson()
        {
            SaveSystem.SaveByJson(PLAYER_DATA_FILE_NAME, SavingData());
            //SaveSystem.SaveByJson($"{System.DateTime.Now:yyyy.dd.M HH-mm-ss}.sav", SavingData());
        }

        void LoadFromJson()
        {
            var saveData = SaveSystem.LoadFromJson<SaveData>(PLAYER_DATA_FILE_NAME);

            LoadData(saveData);
        }

        #endregion

        #region Help Functions

        SaveData SavingData()
        {
            var saveData = new SaveData();

            saveData.playerLevel = level;
            saveData.playerCoin = coin;
            saveData.playerHealthLevel=healthLevel;
            saveData.playerMagicLevel=magicLevel;
            saveData.playerStaminaLevel=staminaLevel;
            return saveData;
        }

        void LoadData(SaveData saveData)
        {
            level = saveData.playerLevel;
            coin = saveData.playerCoin;
            healthLevel=saveData.playerHealthLevel;
            magicLevel=saveData.playerMagicLevel;
            staminaLevel=saveData.playerStaminaLevel;
        }

        #if UNITY_EDITOR
        [UnityEditor.MenuItem("Developer/Delete Player Data Prefs")]
        public static void DeletePlayerDataPrefs()
        {
            PlayerPrefs.DeleteKey(PLAYER_DATA_KEY);
        }

        [UnityEditor.MenuItem("Developer/Delete Player Data Save File")]
        public static void DeletePlayerDataSaveFile()
        {
            SaveSystem.DeleteSaveFile(PLAYER_DATA_FILE_NAME);
        }
        #endif

        #endregion
    }
}
