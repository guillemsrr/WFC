using Cinemachine;
using UnityEngine;

namespace WFC.UI
{
    public class CameraController : MonoBehaviour
    {
        void Start()
        {
            CinemachineCore.GetInputAxis += GetDraggedAxis;
        }

        private float GetDraggedAxis(string axis)
        {
            if (!Input.GetMouseButton(0))
            {
                return 0;
            }

            return Input.GetAxis(axis);
        }
    }
}