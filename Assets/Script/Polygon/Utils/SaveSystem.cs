using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;

namespace PolygonProject
{
    public static class SaveSystem
    {



        #region JSON

        public static void SaveByJson(string saveFileName, object data)
        {
            if(!File.Exists(Application.persistentDataPath+"/usersData"))
            {
                System.IO.Directory.CreateDirectory(Application.persistentDataPath+"/usersData");
            }
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            string jsonData=JsonConvert.SerializeObject(data,settings);

            File.WriteAllText(Application.persistentDataPath+"/usersData/"+saveFileName,jsonData);
        }

        public static T LoadFromJson<T>(string saveFileName)
        {
            var path = Path.Combine(Application.persistentDataPath+"/usersData", saveFileName);
//            Debug.Log(path);
            if(File.Exists(path))
            {
                    var settings = new JsonSerializerSettings();
                    settings.TypeNameHandling = TypeNameHandling.Auto;
                    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    try
                    {
                        string jsonData = File.ReadAllText(path);
                        T usersData=JsonConvert.DeserializeObject<T>(jsonData,settings);
                        return usersData;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"Error loading from JSON: {ex.Message}");
                        throw;
                    }
                
            }
            else
            {
                Debug.Log("读取失败，未找到文件");
                return default;
            }
        }

        #endregion

        #region Deleting

        public static void DeleteSaveFile(string saveFileName)
        {
            var path = Path.Combine(Application.persistentDataPath+"/usersData", saveFileName);

            try
            {
                File.Delete(path);
            }
            catch (System.Exception exception)
            {
                #if UNITY_EDITOR
                Debug.LogError($"Failed to delete {path}. \n{exception}");
                #endif
            }
        }

        #endregion
    }
}