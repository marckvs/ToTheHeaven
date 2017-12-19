using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GM
{
    public class GameManager_ToggleMenu: MonoBehaviour
    {

        private GameManager_Master gameManagerMaster;
        public GameObject menu;
        // Use this for initialization
        void Start()
        {
            //ToggleMenu();
        }

        // Update is called once per frame
        void Update()
        {
            CheckForMenuToggleRequest();
        }

        void OnEnable()
        {
            SetInitialReferences();
            Debug.Log("toggleMenu");
            gameManagerMaster.GameOverEvent += ToggleMenu;

        }

        void OnDisable()
        {
            gameManagerMaster.GameOverEvent -= ToggleMenu;

        }

        void SetInitialReferences()
        {
            gameManagerMaster = GameManager_Master.GetInstance();
        }

        void CheckForMenuToggleRequest()
        {
			if(Input.GetKeyUp(KeyCode.Escape) && !gameManagerMaster.isGameOver && SceneManager.GetActiveScene().buildIndex !=1)
            {
                ToggleMenu();
            }
        }

        void ToggleMenu()
        {
            if (menu != null)
            {
                menu.SetActive(!menu.activeSelf);
                gameManagerMaster.isMenuOn = !gameManagerMaster.isMenuOn;
                gameManagerMaster.CallEventMenuToggle();
            }
           
        }


    }
}
