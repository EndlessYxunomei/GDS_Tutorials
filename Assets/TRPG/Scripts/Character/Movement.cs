using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class Movement : MonoBehaviour
    {
        GridObject gridObject;
        CharacterAnimator characterAnimator;
        List<Vector3> pathWorldPositions;

        [SerializeField] float moveSpeed = 1f;

        public bool isMoving
        {
            get
            {
                if (pathWorldPositions == null) { return false; }
                return pathWorldPositions.Count > 0;
            }
        }

        private void Awake()
        {
            gridObject = GetComponent<GridObject>();
            characterAnimator = GetComponentInChildren<CharacterAnimator>();
        }

        private void Update()
        {
            if (pathWorldPositions == null || pathWorldPositions.Count == 0) { return; }
            transform.position = Vector3.MoveTowards(transform.position, pathWorldPositions[0], moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, pathWorldPositions[0]) < 0.05f)
            {
                pathWorldPositions.RemoveAt(0);
                if (pathWorldPositions.Count == 0)
                {
                    characterAnimator.StopMoving();
                }
                else
                {
                    RotateCharacter(transform.position, pathWorldPositions[0]);
                }
            }
        }

        public void Move(List<PathNode> path)
        {
            if (isMoving)
            {
                ScipAnimation();
            }

            pathWorldPositions = gridObject.targetGrid.ConvertPathNodesToWorldPositions(path);

            //ищем косяк вроде нашли
            //if (pathWorldPositions == null || pathWorldPositions.Count == 0) { return ; }

            gridObject.targetGrid.RemoveObject(gridObject.positionOnGrid, gridObject);

            gridObject.positionOnGrid.x = path[path.Count - 1].pos_x;
            gridObject.positionOnGrid.y = path[path.Count - 1].pos_y;

            gridObject.targetGrid.PlaceObject(gridObject.positionOnGrid, gridObject);

            RotateCharacter(transform.position, pathWorldPositions[0]);

            characterAnimator.StartMoving();
        }

        public void ScipAnimation()
        {
            //есть глюк. если попытаться отменить анимацию (путем задания другой команды) движения на соседнюю клетку, персонаж зависнет в движении между клетками
            //вроде вылечили в command input
            if (pathWorldPositions == null || pathWorldPositions.Count < 2) { return; }
            transform.position = pathWorldPositions[pathWorldPositions.Count - 1];
            Vector3 originPosition = pathWorldPositions[pathWorldPositions.Count - 2];
            Vector3 destanationPosition = pathWorldPositions[pathWorldPositions.Count - 1];
            RotateCharacter(originPosition, destanationPosition);
            pathWorldPositions.Clear();
            characterAnimator.StopMoving();
        }

        private void RotateCharacter(Vector3 originPosition, Vector3 destinationPosition)
        {
            Vector3 direction = (destinationPosition - originPosition).normalized;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}