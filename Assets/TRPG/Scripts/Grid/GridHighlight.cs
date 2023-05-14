using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class GridHighlight : MonoBehaviour
    {
        GridMap grid;
        [SerializeField] GameObject highlightPoint;
        List<GameObject> highlightPointGOs;
        [SerializeField] GameObject container;

        private void Awake()
        {
            grid = GetComponentInParent<GridMap>();
            highlightPointGOs = new List<GameObject>();
        }

        private GameObject CreatePointHighlightObject()
        {
            GameObject go = Instantiate(highlightPoint);
            highlightPointGOs.Add(go);
            go.transform.SetParent(container.transform);
            return go;
        }



        private GameObject GetHighlightPointGO(int i)
        {
            if (highlightPointGOs.Count > i)
            {
                return highlightPointGOs[i];
            }
            GameObject newGO = CreatePointHighlightObject();
            return newGO;
        }

        public void Highlight(int posX, int posY, GameObject highlightObject)
        {
            highlightObject.SetActive(true);
            Vector3 position = grid.GetWorldPosition(posX, posY, true);
            position += 0.2f * Vector3.up;
            highlightObject.transform.position = position;
        }
        public void Highlight(List<Vector2Int> positions)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                Highlight(positions[i].x, positions[i].y, GetHighlightPointGO(i));
            }
        }
        public void Highlight(List<PathNode> positions)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                Highlight(positions[i].pos_x, positions[i].pos_y, GetHighlightPointGO(i));
            }
        }

        public void Hide()
        {
            for (int i = 0; i < highlightPointGOs.Count; i++)
            {
                highlightPointGOs[i].SetActive(false);
            }
        }
    }
}