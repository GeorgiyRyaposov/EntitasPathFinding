using UnityEngine;
using Entitas.CodeGeneration.Attributes;

namespace Game
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Data/GameSettings")]
    public class GameSettings : ScriptableObject, IGameSettings
    {
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private float _cameraSens = 2f;
        
        public Vector2Int GridSize => gridSize;
        public float CameraSens => _cameraSens;
    }

    [Config, Unique, ComponentName("GameSettings")]
    public interface IGameSettings
    {
        Vector2Int GridSize { get; }
        float CameraSens { get; }
    }
}