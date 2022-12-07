// Author: Ignacio María Muñoz Márquez

using System.Collections.Generic;
using UnityEngine;

namespace MahjonggDimensions
{
    public class FiguresGroup4x4x4Creator : FiguresGroupCreator
    {
        private const int _dimensionSize = 4;
        private const float _displacementHorizontal = 1.5f;
        private const int _figuresNeeded = 6;

        public override Figure[,,] CreateFiguresGroup(out int pairsNumber)
        {
            Figure[,,] figuresMap = null;
            pairsNumber = 0;

            if (_figuresListScriptableObject == null || _figuresListScriptableObject.FiguresList.Count < _figuresNeeded)
            {
                return figuresMap;
            }

            List<FigureScriptableObject> figuresSelectedToCreate = SelectFiguresToCreate(_figuresNeeded);
            int totalNumberOfFigures = _dimensionSize * _dimensionSize * _dimensionSize;
            List<FigureScriptableObjectToNumberToCreate> figureScriptableObjectToNumberToCreateList = DetermineNumberOfFiguresToCreatePerType(figuresSelectedToCreate, totalNumberOfFigures, out pairsNumber);
            figuresMap = CreateAndPosition4x4x4FiguresMap(figureScriptableObjectToNumberToCreateList);
            SetupFiguresMapConnections(figuresMap);

            return figuresMap;
        }

        private Figure[,,] CreateAndPosition4x4x4FiguresMap(List<FigureScriptableObjectToNumberToCreate> figureScriptableObjectToNumberToCreateList)
        {
            Figure[,,] figuresMap = new Figure[_dimensionSize, _dimensionSize, _dimensionSize];

            int xLength = figuresMap.GetLength(0);
            int yLength = figuresMap.GetLength(1);
            int zLength = figuresMap.GetLength(2);

            for (int y = 0; y < yLength; ++y)
            {
                for (int z = 0; z < zLength; ++z)
                {
                    for (int x = 0; x < xLength; ++x)
                    {
                        int randomIndex = Random.Range(0, figureScriptableObjectToNumberToCreateList.Count);

                        FigureScriptableObjectToNumberToCreate figureScriptableObjectToNumberToCreate = figureScriptableObjectToNumberToCreateList[randomIndex];

                        if (--figureScriptableObjectToNumberToCreate.NumberToCreate <= 0)
                        {
                            figureScriptableObjectToNumberToCreateList.RemoveAt(randomIndex);
                        }

                        Figure figureCreated = figureScriptableObjectToNumberToCreate.FigureScriptableObject.CreateFigure();
                        figuresMap[x, y, z] = figureCreated;
                        figureCreated.transform.position = new Vector3(x - _displacementHorizontal, y, z - _displacementHorizontal);
                        figureCreated.transform.SetParent(transform, true);
                    }
                }
            }

            return figuresMap;
        }
    }
}
