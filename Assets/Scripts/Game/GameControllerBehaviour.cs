using Cameras;
using DG.Tweening;
using Grids;
using UnityEngine;

namespace Game
{
    public class GameControllerBehaviour : MonoBehaviour
    {
        [SerializeField] private CameraView _cameraView;
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private CellView _cellPrefab;
        
        private GameController _gameController;
        
        private void Awake()
        {
            DOTween.SetTweensCapacity(500, 50);
            var args = new GameSceneArguments()
            {
                CameraView = _cameraView,
                GridCellPrefab = _cellPrefab,
            };
            _gameController = new GameController(Contexts.sharedInstance, args, _gameSettings);
        }
        
        private void Start() => _gameController.Initialize();
        private void Update() => _gameController.Execute();
    }
}