using UnityEngine;

namespace Systems
{
        public class ObjectBuilder
        {
                private readonly GameObject[] _objectPieces;
                private readonly GameObject _collectedObject;
                private readonly Material _mainMaterial;
        
                public ObjectBuilder(GameObject collectedObject, GameObject[] objectPieces, Material mainMaterial)
                {
                        _collectedObject = collectedObject;
                        _objectPieces = objectPieces;
                        _mainMaterial = mainMaterial;
                }
        
                public void BreakObject(Material newMaterial)
                {
                        SwitchMaterial(newMaterial);
                        SwitchPieces(true);
                }

                public void AssembleObject()
                {
                        SwitchMaterial(_mainMaterial);
                        SwitchPieces(false);
                }

                private void SwitchMaterial(Material newMaterial)
                {
                        _collectedObject.GetComponent<Renderer>().material = newMaterial;
                }
                private void SwitchPieces(bool state)
                {
                        foreach (GameObject i in _objectPieces)
                        {
                                i.SetActive(state);
                        }
                }
        }
}