using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class PlaceableUnitNode : MonoBehaviour
    {
        public Character character;
        UnitPlacementManager manager;
        public GridObject gridObject;

        private void Awake()
        {
            manager = FindObjectOfType<UnitPlacementManager>();
            gridObject = GetComponent<GridObject>();
        }
        void Start()
        {
            manager.AddMe(this);
        }
    }
}
