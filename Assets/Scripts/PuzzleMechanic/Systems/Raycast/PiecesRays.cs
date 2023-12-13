using System;
using System.Linq;
using UnityEngine;

namespace PuzzleMechanic.Systems.Raycast
{
    [Serializable]
    public class PiecesRays
    {
        [SerializeField] private float _rayLenghtAfter = 0.1f;
        [SerializeField] private int _angleOffset = 30;
        [SerializeField] private float _rayLength;

        private string _searchedTags;
    
        private const int SizeOfArray = 9;

        public bool IsHexagonTouch(GameObject currentPieces, Vector3 direction, string searchedTags)
        {
            _searchedTags = searchedTags;
            SetRayLenght(currentPieces);
            bool[] rays = new bool[SizeOfArray];
            rays[0] = CreateRay(direction, currentPieces, _rayLength );
            rays[1] =CreateRay(Quaternion.Euler(0, 0, _angleOffset) * direction, currentPieces, _rayLength);
            rays[2] =CreateRay(Quaternion.Euler(0, 0, -_angleOffset) * direction, currentPieces, _rayLength);
            rays[3] =CreateRay(Quaternion.Euler(_angleOffset, 0, 0) * direction, currentPieces, _rayLength);
            rays[4] =CreateRay(Quaternion.Euler(-_angleOffset, 0, 0) * direction, currentPieces, _rayLength);
       
            rays[5] =CreateRay(Quaternion.Euler(_angleOffset, 0, _angleOffset) * direction, currentPieces, _rayLength);
            rays[6] =CreateRay(Quaternion.Euler(-_angleOffset, 0, -_angleOffset) * direction, currentPieces, _rayLength);
            rays[7] =CreateRay(Quaternion.Euler(_angleOffset, 0, -_angleOffset) * direction, currentPieces, _rayLength);
            rays[8] =CreateRay(Quaternion.Euler(-_angleOffset, 0, _angleOffset) * direction, currentPieces, _rayLength);
            if (rays.Any(t => t == true))
            {
                return true;
            }

            return false;
        }

        public void PaintUpperPieces(GameObject currentPieces, Vector3 direction, string searchedTags, Material newMaterial)
        {
       
            _searchedTags = searchedTags;
            SetRayLenght(currentPieces);
            ShootFormPainer(direction, currentPieces, _rayLength,newMaterial );
            ShootFormPainer(Quaternion.Euler(0, 0, _angleOffset) * direction, currentPieces, _rayLength,newMaterial);
            ShootFormPainer(Quaternion.Euler(0, 0, -_angleOffset) * direction, currentPieces, _rayLength,newMaterial);
            ShootFormPainer(Quaternion.Euler(_angleOffset, 0, 0) * direction, currentPieces, _rayLength,newMaterial);
            ShootFormPainer(Quaternion.Euler(-_angleOffset, 0, 0) * direction, currentPieces, _rayLength,newMaterial);
       
            ShootFormPainer(Quaternion.Euler(_angleOffset, 0, _angleOffset) * direction, currentPieces, _rayLength,newMaterial);
            ShootFormPainer(Quaternion.Euler(-_angleOffset, 0, -_angleOffset) * direction, currentPieces, _rayLength,newMaterial);
            ShootFormPainer(Quaternion.Euler(_angleOffset, 0, -_angleOffset) * direction, currentPieces, _rayLength,newMaterial);
            ShootFormPainer(Quaternion.Euler(-_angleOffset, 0, _angleOffset) * direction, currentPieces, _rayLength,newMaterial);
        }
    
        private void ShootFormPainer(Vector3 direction, GameObject piece, float rayLength, Material newMaterial)
        {
            Collider pieceCollider = piece.GetComponent<Collider>();
            Ray ray = new Ray(piece.transform.position, direction);
            pieceCollider.enabled = false;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayLength))
            {
            
                if (hit.collider.gameObject.CompareTag(_searchedTags.ToString()))
                {
                    pieceCollider.enabled = true;
                    hit.collider.gameObject.GetComponent<Renderer>().material = newMaterial;
                    hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                }
            }
            pieceCollider.enabled = true;
        }
    
        private bool CreateRay(Vector3 direction, GameObject piece, float rayLength)
        {
            Collider pieceCollider = piece.GetComponent<Collider>();
            Ray ray = new Ray(piece.transform.position, direction);
            pieceCollider.enabled = false;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayLength))
            {
                if (hit.collider.gameObject.CompareTag(_searchedTags))
                {
                    pieceCollider.enabled = true;
                    return true;
                }
            }
            pieceCollider.enabled = true;
            return false;
        }

        private void SetRayLenght(GameObject piece)
        {
            MeshRenderer mesh = piece.GetComponent<MeshRenderer>();
            _rayLength = mesh.bounds.size.y / 2 + _rayLenghtAfter;
        }
    }
}