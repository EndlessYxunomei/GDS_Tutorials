using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class ClearUtility : MonoBehaviour
    {
        Pathfinding targetPF;
        GridHighlight attackHighlight;
        GridHighlight moveHighlight;

        private void Start()
        {
            StageManager stageManager = FindObjectOfType<StageManager>();
            attackHighlight = stageManager.attackHighlight;
            moveHighlight = stageManager.moveHighlight;
            targetPF = stageManager.pathfinding;
        }
        public void ClearPathfinding()
        {
            targetPF.Clear();
        }
        public void ClearGridHighlightAttack()
        {
            attackHighlight.Hide();
        }
        public void ClearGridHighlightMove()
        {
            moveHighlight.Hide();
        }
    }
}