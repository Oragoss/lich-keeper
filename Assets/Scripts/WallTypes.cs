using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "WallTypes", menuName = "DungeonObjects/Wall Type")]
    public class WallTypes : ScriptableObject
    {
        [SerializeField] Sprite wall;

        public Sprite Sprite
        {
            get
            {
                return wall;
            }
        }
    }
}