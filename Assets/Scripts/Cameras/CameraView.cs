using UnityEngine;

namespace Cameras
{
    [RequireComponent(typeof(Camera))]
    public class CameraView : MonoBehaviour, ICameraView
    {
        [SerializeField] private Camera _camera;

        private void Start()
        {
            var gridSize = Contexts.sharedInstance.config.gameSettings.value.GridSize;
            transform.localPosition = new Vector3(gridSize.x * 0.5f, transform.localPosition.y, -gridSize.y * 0.25f);
        }

        public void MovePosition(Vector3 delta)
        {
            transform.localPosition += delta;
        }

        private Plane _plane = new Plane(Vector3.up, Vector3.zero);
        public Vector3 ScreenToWorldPoint(Vector2 screenPosition)
        {
            var ray = _camera.ScreenPointToRay(screenPosition);
            
            return _plane.Raycast(ray, out var enter) ? ray.GetPoint(enter) : Vector3.zero;
        }
    }
}