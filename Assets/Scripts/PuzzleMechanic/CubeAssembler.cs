using System.Linq;
using Interfaces;
using PuzzleMechanic.Interfaces;
using PuzzleMechanic.Systems;
using PuzzleMechanic.Systems.Pieces;
using PuzzleMechanic.Systems.PuzzleBuilder;
using UnityEngine;

namespace PuzzleMechanic
{
    public class CubeAssembler : MonoBehaviour, IBreakable, IAssemble
    {
        [Header("Puzzle")]
        [SerializeField] private GameObject[] _currentObject;
        [SerializeField] private GameObject[] _objectPieces;
        [SerializeField] private GameObject[] _basePieces;
        [SerializeField] private GameObject[] _baseHologramPieces;
        [SerializeField] private Material _newMaterial;
        [SerializeField] private Material _newBaseMaterial;
        [SerializeField] private Material _mainMaterial;
        [Header("Systems")]
        [SerializeField] private Pieces _pieces = new();
        [SerializeField] private Spots _spots = new();
        [SerializeField] private Exploder _exploder = new();
        [Header("Permits")]
        [SerializeField] private bool _isExploded = true;

        private ObjectBuilder _objectBuilder;
        private bool _isBroken;

        private void Awake()
        {
            Subscribe();
            Initialize();
        }
        private void OnDestroy()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            _pieces.OnSpotPutted += CheckWinScore;
        }

        private void UnSubscribe()
        {
            _pieces.OnSpotPutted -= CheckWinScore;
        }
        
        public void BreakObject()
        {
            if (!_isBroken)
            {
                _isBroken = true;
                _pieces.InitializePieces(_objectPieces, _basePieces, _spots, _newBaseMaterial);
                _objectBuilder.BreakObject(_newMaterial,_newBaseMaterial);
                if (_isExploded)
                {
                    _exploder.Explode(_objectPieces);
                }
            }
        }
        public void AssembleObject()
        {
            _objectBuilder.AssembleObject();
            _spots.OffAllSpots();
            ReInitializeObject();
        }

        public void CheckPieceSpotСorrectness(GameObject piece)
        {
            _pieces.CheckPieceSpotСorrectness(piece);
        }

        private void Initialize()
        {
            gameObject.tag = Constants.DestructibleObject;
            _objectBuilder = new ObjectBuilder(_baseHologramPieces,_currentObject,_objectPieces, _mainMaterial);
        }
        
        private void ReInitializeObject()
        {
            _isBroken = false;
            _pieces.ReInitializePieces();
            Initialize();
        }
        
        private void CheckWinScore()
        {
            int score = _pieces.InRightSlot.Count(slot => slot);

            if (score == _pieces.InRightSlot.Length)
            {
                AssembleObject();
            }
        }
    }
}