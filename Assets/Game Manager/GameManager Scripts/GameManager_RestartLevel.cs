using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace GM
{
    public class GameManager_RestartLevel : MonoBehaviour
    {
        private GameManager_Master gameManagerMaster;

        void OnEnable()
        {
            SetInitialReferences();
            Debug.Log("restartLevel");
            gameManagerMaster.RestartLevelEvent += RestartLevel;
        }

        void OnDisable()
        {
            gameManagerMaster.RestartLevelEvent -= RestartLevel;
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
    }
}
