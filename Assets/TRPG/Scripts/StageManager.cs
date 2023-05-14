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

        //�������� ������ ������� ������� � ����� �������
        public GridHighlight moveHighlight;
        public GridHighlight attackHighlight;

        //������� �����
        public Transform ldCorner;
        public Transform ruCorner;
    }
}
