// Author: Ignacio María Muñoz Márquez

using System;
using UnityEngine;

namespace MahjonggDimensions
{
    [CreateAssetMenu(fileName = "FigureScriptableObject", menuName = "MahjonggDimensions/Figure/FigureScriptableObject", order = 1)]
    public class FigureScriptableObject : ScriptableObject
    {
        public Guid ID { get; private set; }
        public Material LitMaterial { get { return _litMaterial; } }
        public Material UnlitMaterial { get { return _unlitMaterial; } }

        [SerializeField]
        private Figure _figurePrefab;
        [SerializeField]
        private Material _litMaterial;
        [SerializeField]
        private Material _unlitMaterial;

        public Figure CreateFigure()
        {
            if (ID == Guid.Empty)
            {
                AssignNewID();
            }

            Figure figure = Instantiate(_figurePrefab);
            figure.Initialize(ID, this);
            return figure;
        }

        private void OnValidate()
        {
            if(ID == Guid.Empty)
            {
                AssignNewID();
            }
        }

        private void Reset()
        {
            AssignNewID();
        }

        private void AssignNewID()
        {
            ID = Guid.NewGuid();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}
