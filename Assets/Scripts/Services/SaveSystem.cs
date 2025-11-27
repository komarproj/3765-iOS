using System;
using System.IO;
using UnityEngine;

namespace Game.UserData
{
    public class SaveSystem
    {
        private UserData _userData;
        public UserData Data => _userData;

        private static string FilePath => Path.Combine(Application.persistentDataPath, "userdata.json");

        public SaveSystem()
        {
            Load();
        }

        public void Save()
        {
            if (_userData == null)
            {
                Debug.LogWarning("[SaveSystem] No data to save.");
                return;
            }

            try
            {
                File.WriteAllText(FilePath, JsonUtility.ToJson(_userData, true));
                Debug.Log($"[SaveSystem] Saved to {FilePath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[SaveSystem] Failed to save data: {e}");
            }
        }

        private void Load()
        {
            if (!File.Exists(FilePath))
            {
                Debug.Log("[SaveSystem] Save file not found. Using new UserData.");
                _userData = new UserData();
                return;
            }

            try
            {
                string json = File.ReadAllText(FilePath);
                _userData = JsonUtility.FromJson<UserData>(json) ?? new UserData();
                Debug.Log("[SaveSystem] Load successful.");
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[SaveSystem] Failed to load save file. Using new UserData. Error: {e}");
                _userData = new UserData();
            }
        }

        public void ResetData()
        {
            _userData = new UserData();
            Save();
            Debug.Log("[SaveSystem] Data reset to default.");
        }
    }
}