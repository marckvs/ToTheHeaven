using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace GM {

    public class GameManager_TogglePause : MonoBehaviour {

        private GameManager_Master gameManagerMaster;
        ChangeToGameManagerScene previousScene;
        private bool isPaused;



        void OnEnable()
        {
            SetInitialReferences();
            gameManagerMaster.MenuToggleEvent += TogglePause;

        }

        void OnDisable()
        {
            gameManagerMaster.MenuToggleEvent -= TogglePause;

        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void TogglePause()
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
        }
    }
}

