using UnityEngine;

namespace Cameras
{
    public interface ICameraView
    {
        void MovePosition(Vector3 delta);
    }
}