// Author: Ignacio María Muñoz Márquez

using System.Collections.Generic;
using UnityEngine;

namespace MahjonggDimensions
{
    public abstract class FiguresGroupCreator : MonoBehaviour
    {
        protected class FigureScriptableObjectToNumberToCreate
        {
            public FigureScriptableObject FigureScriptableObject;
            public int NumberToCreate;
        }

        [SerializeField]
        protected FiguresListScriptableObject _figuresListScriptableObject;

        public abstract Figure[,,] CreateFiguresGroup(out int pairsNumber);

        protected List<FigureScriptableObject> SelectFiguresToCreate(int figuresNeeded)
        {
            List<FigureScriptableObject> figuresLeftToSelect = new List<FigureScriptableObject>(_figuresListScriptableObject.FiguresList);
            List<FigureScriptableObject> figuresSelected = new List<FigureScriptableObject>();

            do
            {
                int randomIndex = Random.Range(0, figuresLeftToSelect.Count);
                figuresSelected.Add(figuresLeftToSelect[randomIndex]);
                figuresLeftToSelect.RemoveAt(randomIndex);
            } while (figuresSelected.Count < figuresNeeded);

            return figuresSelected;
        }

        protected List<FigureScriptableObjectToNumberToCreate> DetermineNumberOfFiguresToCreatePerType(List<FigureScriptableObject> figuresSelectedToCreate, int totalNumberOfFigures, out int pairsNumber)
        {
            List<FigureScriptableObjectToNumberToCreate> figureScriptableObjectToNumberToCreateList = new List<FigureScriptableObjectToNumberToCreate>();

            int figuresToCreatePerType = totalNumberOfFigures / figuresSelectedToCreate.Count;
            int figuresRemainingWithoutAssigning = totalNumberOfFigures - (figuresToCreatePerType * figuresSelectedToCreate.Count);

            for (int i = 0; i < figuresSelectedToCreate.Count; ++i)
            {
                FigureScriptableObjectToNumberToCreate figureScriptableObjectToNumberToCreate = new FigureScriptableObjectToNumberToCreate();
                figureScriptableObjectToNumberToCreate.FigureScriptableObject = figuresSelectedToCreate[i];
                figureScriptableObjectToNumberToCreate.NumberToCreate = figuresToCreatePerType;

                figureScriptableObjectToNumberToCreateList.Add(figureScriptableObjectToNumberToCreate);
            }

            int figuresPairsRemainingWithoutAssigning = figuresRemainingWithoutAssigning / 2;

            pairsNumber = totalNumberOfFigures / 2;

            do
            {
                int randomIndex = Random.Range(0, figureScriptableObjectToNumberToCreateList.Count);
                FigureScriptableObjectToNumberToCreate figureScriptableObjectToNumberToCreate = figureScriptableObjectToNumberToCreateList[randomIndex];
                figureScriptableObjectToNumberToCreate.NumberToCreate += 2;
            } while (--figuresPairsRemainingWithoutAssigning > 0);

            return figureScriptableObjectToNumberToCreateList;
        }

        protected void SetupFiguresMapConnections(Figure[,,] figuresMap)
        {
            int xLength = figuresMap.GetLength(0);
            int yLength = figuresMap.GetLength(1);
            int zLength = figuresMap.GetLength(2);

            for (int y = 0; y < yLength; ++y)
            {
                for (int z = 0; z < zLength; ++z)
                {
                    for (int x = 0; x < xLength; ++x)
                    {
                        Figure figureAtPosition = figuresMap[x, y, z];

                        if (figureAtPosition == null)
                        {
                            continue;
                        }

                        if (z + 1 < zLength)
                        {
                            figureAtPosition.Connections.NorthFigure = figuresMap[x, y, z + 1];
                        }

                        if (z - 1 >= 0)
                        {
                            figureAtPosition.Connections.SouthFigure = figuresMap[x, y, z - 1];
                        }

                        if (x + 1 < xLength)
                        {
                            figureAtPosition.Connections.EastFigure = figuresMap[x + 1, y, z];
                        }

                        if (x - 1 >= 0)
                        {
                            figureAtPosition.Connections.WestFigure = figuresMap[x - 1, y, z];
                        }
                    }
                }
            }
        }
    }
}
