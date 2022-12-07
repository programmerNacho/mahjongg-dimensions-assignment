// Author: Ignacio María Muñoz Márquez

using UnityEngine;

namespace MahjonggDimensions
{
    public class FigureSelector : MonoBehaviour
    {
        public delegate void FigureSelectorEvent();

        public event FigureSelectorEvent OnFigurePairCompleted;

        public Figure FigureSelected { get { return _figureSelected; } }
        public Figure FigureHovered { get { return _figureHovered; } }

        [SerializeField]
        private GameplayInput _gameplayInput;
        [SerializeField]
        private float _selectRayLength;
        [SerializeField]
        private LayerMask _selectRayLayerMask;
        [SerializeField]
        private ParticleSystem _figureDisappearParticleSystem;

        private Camera _mainCamera;
        private Figure _figureHovered;
        private Figure _figureSelected;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            _gameplayInput.OnShootInput += CheckFigureSelected;
        }

        private void OnDisable()
        {
            _gameplayInput.OnShootInput -= CheckFigureSelected;
        }

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        private void FixedUpdate()
        {
            CheckFigureHoveredWithCameraRay();
        }
#endif

        private void CheckFigureHoveredWithCameraRay()
        {
            if (_mainCamera == null)
            {
                return;
            }

            Ray cameraMouseRay = _mainCamera.ScreenPointToRay(_gameplayInput.PointerScreenPosition);

            if (Physics.Raycast(cameraMouseRay, out RaycastHit hitInfo, _selectRayLength, _selectRayLayerMask))
            {
                Figure figureRayHit = hitInfo.transform.GetComponent<Figure>();

                if (figureRayHit != null)
                {
                    if (_figureHovered != null && _figureHovered != figureRayHit && !IsFigureSelected(_figureHovered))
                    {
                        _figureHovered.SetState(Figure.State.Idle);
                    }

                    _figureHovered = figureRayHit;

                    if (!IsFigureSelected(_figureHovered))
                    {
                        _figureHovered.SetState(Figure.State.Hovered);
                    }
                }
            }
            else
            {
                if (_figureHovered != null && !IsFigureSelected(_figureHovered))
                {
                    _figureHovered.SetState(Figure.State.Idle);
                }

                _figureHovered = null;
            }
        }

        private void CheckFigureSelected()
        {
#if UNITY_ANDROID || UNITY_IOS
            CheckFigureHoveredWithCameraRay();
#endif
            if (_figureHovered != null && !IsFigureSelected(_figureHovered))
            {
                if (_figureHovered.CanBeSelected())
                {
                    if (_figureSelected == null)
                    {
                        _figureSelected = _figureHovered;
                        _figureSelected.SetState(Figure.State.Selected);

                        SoundManager.Instance.PlayFX(SoundManager.Instance.GameAudioClips.FigureSelected);
                    }
                    else
                    {
                        _figureHovered.SetState(Figure.State.Selected);

                        if (_figureSelected.ID == _figureHovered.ID)
                        {
                            CreateFiguresParticles();

                            Destroy(_figureHovered.gameObject);
                            Destroy(_figureSelected.gameObject);

                            _figureHovered = null;
                            _figureSelected = null;

                            SoundManager.Instance.PlayFX(SoundManager.Instance.GameAudioClips.FigurePairCompleted);

                            OnFigurePairCompleted?.Invoke();
                        }
                        else
                        {
                            _figureSelected.SetState(Figure.State.Idle);
                            _figureSelected = _figureHovered;

                            SoundManager.Instance.PlayFX(SoundManager.Instance.GameAudioClips.FigureSelected);
                        }
                    }
                }
                else
                {
                    SoundManager.Instance.PlayFX(SoundManager.Instance.GameAudioClips.FigureCannotSelect);
                }
            }
        }

        private bool IsFigureSelected(Figure figure)
        {
            return figure.CurrentState == Figure.State.Selected && figure == _figureSelected;
        }

        private void CreateFiguresParticles()
        {
            ParticleSystem figureHoveredParticles = Instantiate(_figureDisappearParticleSystem, _figureHovered.transform.position, _figureHovered.transform.rotation);
            figureHoveredParticles.GetComponent<ParticleSystemRenderer>().material = _figureHovered.FigureScriptableObject.LitMaterial;
            ParticleSystem figureSelectedParticles = Instantiate(_figureDisappearParticleSystem, _figureSelected.transform.position, _figureSelected.transform.rotation);
            figureSelectedParticles.GetComponent<ParticleSystemRenderer>().material = _figureSelected.FigureScriptableObject.LitMaterial;
        }
    }
}
