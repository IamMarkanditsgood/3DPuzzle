using System;
using Enums;
using UnityEngine;

namespace PuzzleMechanic.Systems
{
    [Serializable]
    public class Pieces
    {
        [SerializeField] private Vector3[] _piecesSlotsPosition;
        [SerializeField] private Quaternion[] _piecesSlotsRotation;
        [SerializeField] private bool[] _inRightSlot;
        [SerializeField] private float _allowedError = 0.2f;
        [SerializeField] private float _lowerСubeDistance = 10f;

        private GameObject[] _objectPieces;
        private GameObject[] _basePieces;
        private Spots _spots;

        public event Action OnSpotPutted;
        
        public bool[] InRightSlot => _inRightSlot;
        
        public void InitializePieces(GameObject[] objectPieces, GameObject[] basePieces, Spots spots)
        {
            _basePieces = basePieces;
            _objectPieces = objectPieces;
            _spots = spots;
            int numberOfPieces = _objectPieces.Length;
            _inRightSlot = new bool[numberOfPieces];
            _piecesSlotsPosition = new Vector3[numberOfPieces];
            _piecesSlotsRotation = new Quaternion[numberOfPieces];
        
            for (int i = 0; i < _objectPieces.Length; i++)
            {
                _objectPieces[i].tag = Tags.PiecesOfObject.ToString();
                _piecesSlotsPosition[i] = _objectPieces[i].transform.position;
                _piecesSlotsRotation[i] = _objectPieces[i].transform.rotation;
                _spots.PutSpot(i, _piecesSlotsPosition);
            }
        }
        
        public void ReInitializePieces()
        {
            foreach (var objectPiece in _objectPieces)
            {
                objectPiece.GetComponent<Rigidbody>().isKinematic = false;
                objectPiece.GetComponent<Collider>().enabled = true;
            }
        }

        public void CheckPieceSpotСorrectness(GameObject piece)
        {
            int arrayIndex = GetArrayIndex(piece);
            
            if (IsOnOther(piece) && Vector3.Distance(piece.transform.position, _piecesSlotsPosition[arrayIndex]) <= _allowedError)
            {
                PutPieceOnSpot(piece, arrayIndex);
                OnSpotPutted?.Invoke();
            }
        }
        
        private void PutPieceOnSpot(GameObject piece, int objectIndex)
        {
            piece.GetComponent<Rigidbody>().isKinematic = true;
            piece.GetComponent<Collider>().enabled = false;
            piece.transform.position = _piecesSlotsPosition[objectIndex];
            piece.transform.rotation = _piecesSlotsRotation[objectIndex];
            _inRightSlot[objectIndex] = true;
        }
        
        private int GetArrayIndex(GameObject piece)
        {
            int index = Array.IndexOf(_objectPieces,piece);
        
            return index;
        }
        
        private bool IsOnOther(GameObject currentPieces)
        {
            foreach (var basePiece in _basePieces)
            {
                if (currentPieces == basePiece)
                {
                    return true;
                }
            }
            Vector3 center = currentPieces.transform.position;
            Vector3 rayDirection = -Vector3.up;
            
            if (Physics.Raycast(center, rayDirection, out RaycastHit hit, _lowerСubeDistance))
            {
                if (hit.collider.gameObject.CompareTag(Tags.PiecesOfObject.ToString()))
                {
                    return true;
                }
            }

            return false;
        }
        
    }
}