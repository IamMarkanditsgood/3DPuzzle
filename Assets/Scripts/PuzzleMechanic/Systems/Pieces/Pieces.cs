using System;
using PuzzleMechanic.Systems.Raycast;
using UnityEngine;

namespace PuzzleMechanic.Systems.Pieces
{
    [Serializable]
    public class Pieces
    {
        [SerializeField] private Vector3[] _piecesSlotsPosition;
        [SerializeField] private Quaternion[] _piecesSlotsRotation;
        [SerializeField] private bool[] _inRightSlot;
        [SerializeField] private float _allowedError = 0.2f;
        [SerializeField] private PiecesRays _piecesRays = new();
        [SerializeField] private Vector3 _directionOfConnection;
        [SerializeField] private Vector3 _directionOfPainting;
        
        private Material _nextPieceMaterial;
        private GameObject[] _objectPieces;
        private GameObject[] _basePieces;
        private Spots _spots;

        public event Action OnSpotPutted;
        
        public bool[] InRightSlot => _inRightSlot;
        
        public void InitializePieces(GameObject[] objectPieces, GameObject[] basePieces, Spots spots, Material nextPieceMaterial)
        {
            _nextPieceMaterial = nextPieceMaterial;
            _basePieces = basePieces;
            _objectPieces = objectPieces;
            _spots = spots;
            int numberOfPieces = _objectPieces.Length;
            _inRightSlot = new bool[numberOfPieces];
            _piecesSlotsPosition = new Vector3[numberOfPieces];
            _piecesSlotsRotation = new Quaternion[numberOfPieces];
        
            for (int i = 0; i < _objectPieces.Length; i++)
            {
                _objectPieces[i].tag = Constants.PiecesOfObject;
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
            int arrayIndex = PiecesArray.GetArrayIndex(piece, _objectPieces);
            
            if (IsOnOther(piece) && Vector3.Distance(piece.transform.position, _piecesSlotsPosition[arrayIndex]) <= _allowedError)
            {
                
                PutPieceOnSpot(piece, arrayIndex);
                OnSpotPutted?.Invoke();
            }
        }
        
        private void PutPieceOnSpot(GameObject piece, int objectIndex)
        {
            piece.GetComponent<Rigidbody>().isKinematic = true;
            piece.tag = Constants.UntouchedPiece;
            piece.transform.position = _piecesSlotsPosition[objectIndex];
            piece.transform.rotation = _piecesSlotsRotation[objectIndex];
            _inRightSlot[objectIndex] = true;
            _piecesRays.PaintNextStage(piece, _directionOfPainting, Constants.HologramOfObject,_nextPieceMaterial);
            
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

            return _piecesRays.IsTouch(currentPieces, _directionOfConnection, Constants.UntouchedPiece);
        }
        
    }
}