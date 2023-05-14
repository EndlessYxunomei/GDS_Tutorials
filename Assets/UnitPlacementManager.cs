using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    public class UnitPlacementManager : MonoBehaviour
    {
        [SerializeField] GameObject characterPrefab;
        [SerializeField] GameObject characterModel;
        [SerializeField] SquadData squadData;
        MouseInput mouseInput;
        List<PlaceableUnitNode> nodes;
        List<GameObject> characterObjects = new List<GameObject>();

        private void Awake()
        {
            mouseInput = GetComponent<MouseInput>();
        }
        private void Update()
        {
            ProcessInput();
        }
        private void Start()
        {
            Init();
        }
        private void Init()
        {
            characterObjects = new List<GameObject>();
            for (int i = 0; i < squadData.charactersInTheSquad.Count; i++)
            {
                InitCharacter(squadData.charactersInTheSquad[i]);
            }
        }
        private void InitCharacter(CharacterData characterData)
        {
            GameObject newCharacterGameObject = Instantiate(characterPrefab);
            GameObject newCharacterModel = Instantiate(characterModel);
            newCharacterModel.transform.parent = newCharacterGameObject.transform;
            newCharacterGameObject.GetComponent<Character>().SetCharacterData(characterData);
            characterObjects.Add(newCharacterGameObject);
        }
        private void ProcessInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceableUnitNode placeNode = nodes.Find(x => x.gridObject.positionOnGrid == mouseInput.positionOnGrid);
                if (placeNode != null)
                {
                    if (placeNode.character == null)
                    {
                        PlaceCharacter(placeNode, characterObjects[0]);
                    }
                }
            }
        }

        private void PlaceCharacter(PlaceableUnitNode placeNode, GameObject characterObject)
        {
            characterObject.transform.position = placeNode.transform.position;
            placeNode.character = characterObject.GetComponent<Character>();
            characterObjects.Remove(characterObject);
        }

        public void AddMe(PlaceableUnitNode placeableUnitNode)
        {
            if (nodes == null) { nodes = new List<PlaceableUnitNode>(); }
            nodes.Add(placeableUnitNode);
        }
    }
}