using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDS.TRPG;

namespace GDS.TRPG
{
    [CreateAssetMenu]
    public class SquadData : ScriptableObject
    {
        public List<CharacterData> charactersInTheSquad;
    }
}