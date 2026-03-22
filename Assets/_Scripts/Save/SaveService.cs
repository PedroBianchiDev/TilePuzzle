using TilePuzzle.Core;
using UnityEngine;

namespace TilePuzzle.Save
{
    public class SaveService : MonoBehaviourService
    {
        [SerializeField]
        private PlayerData playerData;

        [SerializeField]
        private ApplicationData applicationData;

        public override void Initialize()
        {
            LoadApplicationData();
            //PlayerPrefs.DeleteAll();
        }

        public PlayerData GetPlayerData() => playerData;

        public ApplicationData GetApplicationData() => applicationData;

        public void ModifyGold(int value)
        {
            playerData.gold += value;
        }

        public void AddLevel(string id)
        {
            playerData.boughtLevels.Add(id);
        }

        public void SaveApplicationData()
        {
            string json = JsonUtility.ToJson(applicationData);
            PlayerPrefs.SetString("ApplicationData", json);
            PlayerPrefs.Save();
        }

        public void LoadApplicationData()
        {
            if (!PlayerPrefs.HasKey("ApplicationData"))
                return;

            string json = PlayerPrefs.GetString("ApplicationData");
            applicationData = JsonUtility.FromJson<ApplicationData>(json);
        }

        public void SavePlayerData()
        {
            string json = JsonUtility.ToJson(playerData);
            PlayerPrefs.SetString("PlayerData", json);
            PlayerPrefs.Save();
        }

        public void LoadPlayerData()
        {
            if (!PlayerPrefs.HasKey("PlayerData"))
                return;

            string json = PlayerPrefs.GetString("PlayerData");
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
    }
}