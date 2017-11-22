using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GM
{ 

    public class GameManager_TogglePlayer : MonoBehaviour
    {

        public GameObject playerController;
        private GameManager_Master gameManagerMaster;

        void OnEnable()
        {
            SetInitialReferences();
            gameManagerMaster.RestartLevelEvent += TogglePlayerController;
        }

        void OnDisable()
        {
            gameManagerMaster.RestartLevelEvent -= TogglePlayerController;
        }

        void SetInitialReferences()
        {
            gameManagerMaster = GetComponent<GameManager_Master>();
        }

        void TogglePlayerController()
        {
            if (playerController != null)
            {
                playerController.SetActive(!playerController.activeInHierarchy);
            }
        }
    }
}
