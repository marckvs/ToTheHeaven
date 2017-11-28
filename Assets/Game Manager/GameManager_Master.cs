using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{

    public class GameManager_Master : MonoBehaviour
    {

        private static GameManager_Master instance = null;

        public static GameManager_Master GetInstance()
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager_Master>();
            }

            return instance;
        }

        public delegate void GameManagerEventHandler();
        public event GameManagerEventHandler MenuToggleEvent;
        public event GameManagerEventHandler RestartLevelEvent;
        public event GameManagerEventHandler GoToMenuSceneEvent;
        public event GameManagerEventHandler GameOverEvent;

        public bool isGameOver;
        public bool isMenuOn;

        public void CallEventMenuToggle()
        {
            if (MenuToggleEvent != null)
            {
                MenuToggleEvent();
            }
        }

        public void CallEventRestartLevelEvent()
        {
            if (RestartLevelEvent != null)
            {
                RestartLevelEvent();
            }
        }

        public void CallEventGoToMenuSceneEvent()
        {
            if (GoToMenuSceneEvent != null)
            {
                GoToMenuSceneEvent();
            }
        }

        public void CallEventGameOverEvent()
        {
            if (GameOverEvent != null)
            {
                isGameOver = true;
                GameOverEvent();
            }
        }
    }

}
