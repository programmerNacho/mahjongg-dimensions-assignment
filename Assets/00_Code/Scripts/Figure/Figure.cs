// Author: Ignacio María Muñoz Márquez

using System;
using UnityEngine;

namespace MahjonggDimensions
{
    public class Figure : MonoBehaviour
    {
        public enum State { Idle, Hovered, Selected }

        public Guid ID { get; private set; }
        public FigureScriptableObject FigureScriptableObject { get; private set; }
        public State CurrentState { get { return _currentState; } }
        public FigureConnections Connections { get { return _connections; } }

        [SerializeField]
        private MeshRenderer _meshRenderer;

        private State _currentState;
        private FigureConnections _connections;

        public void Initialize(Guid id, FigureScriptableObject figureScriptableObject)
        {
            ID = id;
            FigureScriptableObject = figureScriptableObject;

            if(_meshRenderer == null)
            {
                _meshRenderer = GetComponentInChildren<MeshRenderer>();
            }

            _meshRenderer.material = FigureScriptableObject.LitMaterial;

            _connections = new FigureConnections();
            SetState(State.Idle);
        }

        public void SetState(State state)
        {
            switch (state)
            {
                case State.Idle:
                    SetIdleState();
                    break;
                case State.Hovered:
                    SetHoveredState();
                    break;
                case State.Selected:
                    SetSelectedState();
                    break;
            }
        }

        public bool CanBeSelected()
        {
            return (_connections.NorthFigure == null || _connections.SouthFigure == null) &&
                (_connections.EastFigure == null || _connections.WestFigure == null);
        }

        private void SetIdleState()
        {
            _currentState = State.Idle;

            _meshRenderer.material.SetColor("_Emission_Color", Color.black);
        }

        private void SetHoveredState()
        {
            _currentState = State.Hovered;

            _meshRenderer.material.SetColor("_Emission_Color", Color.white * 0.5f);
        }

        private void SetSelectedState()
        {
            _currentState = State.Selected;

            _meshRenderer.material.SetColor("_Emission_Color", Color.white);
        }
    }
}
