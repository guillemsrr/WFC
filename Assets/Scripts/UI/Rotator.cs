using UnityEngine;

namespace WFC.UI
{
    public class Rotator: MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _speed = 1f;

        private void Update()
        {
            _transform.Rotate(Vector3.up, _speed*Time.deltaTime);
        }
    }
}