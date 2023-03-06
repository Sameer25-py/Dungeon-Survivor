using System.Collections;
using System.Collections.Generic;
using DungeonSurvivor.Core.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonSurvivor
{
    public class SceneController : MonoBehaviour
    {
        public void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadLobby()
        {
            GameManager.Instance.LoadScene("Lobby");
        }
    }
}
