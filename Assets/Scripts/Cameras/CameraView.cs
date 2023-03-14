using UnityEngine;

namespace Cameras
{
    [RequireComponent(typeof(Camera))]
    public class CameraView : MonoBehaviour, ICameraView
    {
        [SerializeField] private Camera _camera;

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