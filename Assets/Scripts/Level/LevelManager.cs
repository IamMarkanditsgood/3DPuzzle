using Enums;
using InputSystem;
using Interfaces;
using PuzzleMechanic.Interfaces;
using Systems;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float _rayDistance = 10f;

    private InputManager _inputManager = new();
    private ObjectManipulator _objectManipulator = new();
    private GameObject _choosedPiece;
    private GameObject _choosedPazzle;
    private bool _isDragging;

    private void Awake()
    {
        Subscribe();
    }

    private void Update()
    {
        _inputManager.CheckPressedKey();
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }

    private void Subscribe()
    {
        _inputManager.OnButtonPressed += MouseButtonPressed;
        _inputManager.OnButtonUp += MouseButtonUp;

    }

    private void UnSubscribe()
    {
        _inputManager.OnButtonPressed -= MouseButtonPressed;
        _inputManager.OnButtonUp -= MouseButtonUp;
    }

    private void MouseButtonPressed()
    {
        InteractWithWorld();
    }
    
    private void MouseButtonUp()
    {
        CheckPieceSpot();
    }

    private void InteractWithWorld()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, _rayDistance))
        {
            GameObject hitObject = hitInfo.collider.gameObject;
            if (hitObject.CompareTag(Tags.PiecesOfObject.ToString()))
            {
                _isDragging = true;
                _choosedPiece = hitObject;
            }
            else if (hitObject.CompareTag(Tags.DestructibleObject.ToString()))
            {
                _choosedPazzle = hitObject;
                IBreakable destructibleObject = hitObject.GetComponent<IBreakable>();
                destructibleObject.BreakObject();
            }
        }
        
        if (_isDragging)
        {
            _objectManipulator.MoveObject(_choosedPiece, ray, _rayDistance);
        }
    }
    
    private void CheckPieceSpot()
    {
        if (_choosedPiece != null)
        {
            _choosedPazzle.GetComponent<IAssemble>().CheckPieceSpotСorrectness(_choosedPiece);
        }
        _choosedPiece = null;
        _isDragging = false;
    }
}
