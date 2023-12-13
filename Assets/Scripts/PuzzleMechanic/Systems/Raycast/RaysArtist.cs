using UnityEngine;

namespace PuzzleMechanic.Systems.Raycast
{
    public class RaysArtist : MonoBehaviour
    {
        [SerializeField] private float _rayLength = 0.35f;
        [SerializeField] private float _rayLengthAfter = 0.1f;
        [SerializeField] private int _angleOffset = 40;
        [SerializeField] private Vector3 _direction;
        
        private void Update()
        {
            SetRayLenght();
            CreateRay(_direction, Color.white, _rayLength );
            CreateRay(Quaternion.Euler(0, 0, _angleOffset) * _direction, Color.red, _rayLength);
            CreateRay(Quaternion.Euler(0, 0, -_angleOffset) * _direction, Color.green, _rayLength);
            CreateRay(Quaternion.Euler(_angleOffset, 0, 0) * _direction, Color.blue, _rayLength);
            CreateRay(Quaternion.Euler(-_angleOffset, 0, 0) * _direction, Color.yellow, _rayLength);
            
            CreateRay(Quaternion.Euler(_angleOffset, 0, _angleOffset) * _direction, Color.black, _rayLength);
            CreateRay(Quaternion.Euler(-_angleOffset, 0, -_angleOffset) * _direction, Color.magenta, _rayLength);
            CreateRay(Quaternion.Euler(_angleOffset, 0, -_angleOffset) * _direction, Color.cyan, _rayLength);
            CreateRay(Quaternion.Euler(-_angleOffset, 0, _angleOffset) * _direction, Color.gray, _rayLength);
        }
        void CreateRay(Vector3 direction, Color color, float rayLength)
        {
            Ray ray = new Ray(transform.position, direction);
            Debug.DrawRay(ray.origin, ray.direction * rayLength, color);
        }
        private void SetRayLenght()
        {
            MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();
            _rayLength = mesh.bounds.size.y / 2 + _rayLengthAfter;
        }
    }
}