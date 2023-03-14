using UnityEngine;
using Entitas.CodeGeneration.Attributes;

namespace Game
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Data/GameSettings")]
    public class GameSettings : ScriptableObject, IGameSettings
    {
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private float _cameraSens = 2f;
        [SerializeField] private float _pathStepDelay = 0.5f;
        
        public Vector2Int GridSize => gridSize;
        public float CameraSens => _cameraSens;
        public float PathStepDelay => _pathStepDelay;
    }

    [Config, Unique, ComponentName("GameSettings")]
    public interface IGameSettings
    {
        Vector2Int GridSize { get; }
        float CameraSens { get; }
        float PathStepDelay { get; }
    }
}