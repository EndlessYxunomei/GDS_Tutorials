using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class GameMenu : MonoBehaviour
    {
        public GameObject panel;

        SelectCharacter selectCharacter;
        private void Awake()
        {
            selectCharacter = GetComponent<SelectCharacter>();
        }

        void Update()
        {
            if (selectCharacter.enabled == false) { return; }
            if (Input.GetMouseButtonDown(1))
            {
                panel.SetActive(!panel.activeInHierarchy);
            }
        }
    }
}