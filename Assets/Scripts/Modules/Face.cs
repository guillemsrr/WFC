﻿using UnityEngine;
using WFCourse.Utilities;

namespace WFCourse.Modules
{
    public class Face: MonoBehaviour
    {
        [SerializeField] private Direction _direction;
        [SerializeField] private FaceTypeSO _type;
        [SerializeField] private Color _color;

        private readonly Vector3 _faceSize = new Vector3(2, 2, 0.001f);

        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = _color;
            Gizmos.DrawCube(Vector3.zero, _faceSize);
        }
    }
}