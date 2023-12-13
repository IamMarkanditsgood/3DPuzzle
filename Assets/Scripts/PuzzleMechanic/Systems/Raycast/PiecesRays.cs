using System;
using UnityEngine;

namespace PuzzleMechanic.Systems.Raycast
{
    [Serializable]
    public class PiecesRays
    {
        [SerializeField] private float _rayLenghtAfter = 0.1f;
        [SerializeField] private int _angleOffset = 30;
        [SerializeField] private float _rayLength;

        private Vector3 _directionOfHexagon;
        private GameObject _currentPieces;
        private string _searchedTags;
        private RaycastHit _currentHit;

        public bool IsTouch(GameObject currentPieces, Vector3 direction, string searchedTags)
        {
            Initialize(currentPieces, direction, searchedTags);
            return CheckHits();
        }
        
        public void PaintNextStage(GameObject currentPieces, Vector3 direction, string searchedTags, Material newMaterial)
        {
            Initialize(currentPieces, direction, searchedTags);
            if (CheckHits())
            {
                PaintPiece(_currentHit.collider.gameObject, newMaterial);
            }
        }

        private void Initialize(GameObject currentPieces, Vector3 direction, string searchedTags)
        {
            _directionOfHexagon = direction;
            _currentPieces = currentPieces;
            _searchedTags = searchedTags;
        }
        
        private bool CheckHits()
        {
            RaycastHit[] raycastHits = CreateHexagonRays();
            foreach (var hit in raycastHits)
            {
                if (hit.collider != null && hit.collider.gameObject.CompareTag(_searchedTags))
                {
                    _currentHit = hit;
                    return true;
                }
            }

            return false;
        }
        
        private void PaintPiece(GameObject piece, Material newMaterial)
        {
            piece.GetComponent<Renderer>().material = newMaterial;
            piece.GetComponent<Collider>().enabled = false;
        }
        
        private RaycastHit[] CreateHexagonRays()
        {
            SetRayLenght();
            
            RaycastHit[] raycastHits = new RaycastHit[Constants.SizeOfArray];

            raycastHits[0] = CreateRay(_directionOfHexagon, _currentPieces, _rayLength );
            
            raycastHits[1] = CreateRay(Quaternion.Euler(0, 0, _angleOffset) * _directionOfHexagon, _currentPieces, _rayLength);
            raycastHits[2] = CreateRay(Quaternion.Euler(0, 0, -_angleOffset) * _directionOfHexagon, _currentPieces, _rayLength);
            raycastHits[3] = CreateRay(Quaternion.Euler(_angleOffset, 0, 0) * _directionOfHexagon, _currentPieces, _rayLength);
            raycastHits[4] = CreateRay(Quaternion.Euler(-_angleOffset, 0, 0) * _directionOfHexagon, _currentPieces, _rayLength);
       
            raycastHits[5] = CreateRay(Quaternion.Euler(_angleOffset, 0, _angleOffset) * _directionOfHexagon, _currentPieces, _rayLength);
            raycastHits[6] = CreateRay(Quaternion.Euler(-_angleOffset, 0, -_angleOffset) * _directionOfHexagon, _currentPieces, _rayLength);
            raycastHits[7] = CreateRay(Quaternion.Euler(_angleOffset, 0, -_angleOffset) * _directionOfHexagon, _currentPieces, _rayLength);
            raycastHits[8] = CreateRay(Quaternion.Euler(-_angleOffset, 0, _angleOffset) * _directionOfHexagon, _currentPieces, _rayLength);
            return raycastHits;
        }
        
        private RaycastHit CreateRay(Vector3 direction, GameObject piece, float rayLength)
        {
            Collider pieceCollider = piece.GetComponent<Collider>();
            Ray ray = new Ray(piece.transform.position, direction);
            pieceCollider.enabled = false;
            Physics.Raycast(ray, out RaycastHit hit, rayLength);
            pieceCollider.enabled = true;
            return hit;
        }
        
        private void SetRayLenght()
        {
            MeshRenderer mesh = _currentPieces.GetComponent<MeshRenderer>();
            _rayLength = mesh.bounds.size.y / 2 + _rayLenghtAfter;
        }
    }
}