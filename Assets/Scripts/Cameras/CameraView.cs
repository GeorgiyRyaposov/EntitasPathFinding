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
    }
}