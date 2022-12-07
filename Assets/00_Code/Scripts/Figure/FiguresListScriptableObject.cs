// Author: Ignacio María Muñoz Márquez

using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace MahjonggDimensions
{
    [CreateAssetMenu(fileName = "FiguresListScriptableObject", menuName = "MahjonggDimensions/Figure/FiguresListScriptableObject", order = 1)]
    public class FiguresListScriptableObject : ScriptableObject
    {
        public List<FigureScriptableObject> FiguresList { get { return _figuresList.Distinct().ToList(); } }

        [SerializeField]
        private List<FigureScriptableObject> _figuresList;
    }
}
