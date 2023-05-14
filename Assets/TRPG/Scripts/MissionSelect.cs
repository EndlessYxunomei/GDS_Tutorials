using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class MissionSelect : MonoBehaviour
    {
        public void LoadMission(string missionName)
        {
            SceneManager.LoadScene("CombatEssentions", LoadSceneMode.Single);
            SceneManager.LoadScene(missionName, LoadSceneMode.Additive);
        }
    }
}