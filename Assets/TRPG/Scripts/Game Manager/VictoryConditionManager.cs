using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class VictoryConditionManager : MonoBehaviour
    {
        [SerializeField] ForceContainer enemyForce;
        [SerializeField] GameObject victoryPanel;
        [SerializeField] MouseInput mouseInput;

        public void CheckPlayerVictory()
        {
            if (enemyForce.CheckDefeated() == true)
            {
                Victory();
            }
        }

        private void Victory()
        {
            mouseInput.enabled = false;
            victoryPanel.SetActive(true);
            Debug.Log("Victory");
        }
        public void ReturnToWorldMap()
        {
            SceneManager.LoadScene("WorldMap", LoadSceneMode.Single);
        }
    }
}