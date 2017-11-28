using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace GM
{

    public class GameManager_GoToMenuScene : MonoBehaviour
    {

        private GameManager_Master gameManagerMaster;
        [SerializeField]
        private GameObject menuPausa;
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
            gameManagerMaster = GameManager_Master.GetInstance();
        }

        void GoToMenuScene()
        {
            SceneManager.LoadScene(0);
            menuPausa.SetActive(false);
            Time.timeScale = 1;
        }

    }
}
