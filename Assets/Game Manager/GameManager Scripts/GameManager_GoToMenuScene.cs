using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace GM
{

    public class GameManager_GoToMenuScene : MonoBehaviour
    {

        private GameManager_Master gameManagerMaster;
        void OnEnable()
        {
            Debug.Log("Menu");
            SetInitialReferences();
            gameManagerMaster.GoToMenuSceneEvent += GoToMenuScene;
        }

        void OnDisable()
        {
            gameManagerMaster.GoToMenuSceneEvent -= GoToMenuScene;

        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void GoToMenuScene()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }

    }
}
