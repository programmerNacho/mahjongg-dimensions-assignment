// Author: Ignacio María Muñoz Márquez

using UnityEngine;

namespace MahjonggDimensions
{
    public class FigureSelectorUI : MonoBehaviour
    {
        [SerializeField]
        private FigureSelector _figureSelector;
        [SerializeField]
        private GameObject _figureA;
        [SerializeField]
        private GameObject _figureB;
        [SerializeField]
        private float _rotationSpeed;

        private MeshRenderer _figureAMeshRenderer;
        private MeshRenderer _figureBMeshRenderer;

        private void Awake()
        {
            _figureAMeshRenderer = _figureA.GetComponentInChildren<MeshRenderer>();
            _figureBMeshRenderer = _figureB.GetComponentInChildren<MeshRenderer>();

            _figureAMeshRenderer.enabled = false;
            _figureBMeshRenderer.enabled = false;
        }

        private void Update()
        {
            RotateFigures();
            CheckFigureVisibilityAndMaterial();
        }

        private void RotateFigures()
        {
            _figureA.transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime, Space.Self);
            _figureB.transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime, Space.Self);
        }

        private void CheckFigureVisibilityAndMaterial()
        {
            if (_figureSelector.FigureSelected != null)
            {
                _figureAMeshRenderer.enabled = true;
                _figureAMeshRenderer.sharedMaterial = _figureSelector.FigureSelected.FigureScriptableObject.UnlitMaterial;

                if (_figureSelector.FigureHovered != null && _figureSelector.FigureHovered != _figureSelector.FigureSelected)
                {
                    _figureBMeshRenderer.enabled = true;
                    _figureBMeshRenderer.sharedMaterial = _figureSelector.FigureHovered.FigureScriptableObject.UnlitMaterial;
                }
                else
                {
                    _figureBMeshRenderer.enabled = false;
                }
            }
            else
            {
                if (_figureSelector.FigureHovered != null)
                {
                    _figureAMeshRenderer.enabled = true;
                    _figureAMeshRenderer.sharedMaterial = _figureSelector.FigureHovered.FigureScriptableObject.UnlitMaterial;
                }
                else
                {
                    _figureAMeshRenderer.enabled = false;
                }

                _figureBMeshRenderer.enabled = false;
            }
        }
    }
}
