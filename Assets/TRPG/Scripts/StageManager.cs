using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class StageManager : MonoBehaviour
    {
        public GridMap stageGrid;
        public Pathfinding pathfinding;

        //возможно протом вернуть обратно в сцену оверлей
        public GridHighlight moveHighlight;
        public GridHighlight attackHighlight;

        //границы сцены
        public Transform ldCorner;
        public Transform ruCorner;
    }
}
