using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class StepInfo
    {
        public StepInfo(int i = 0, int j = 0, int winInfo = 0, int countSteps = 0)
        {
            I = i;
            J = j;
            WinInfo = winInfo;
            CountSteps = countSteps;
        }
        public int I = 0, J = 0;
        public int WinInfo = 0;
        public int CountSteps = 0;

    }
    public class BaseAIModul
    {
        protected BoardController _boardController;

        protected int _mySymvol;
        public virtual void Step()
        {
            if (_boardController.GetBoard.IsPlayerStep) return;

            Board board = new Board(_boardController.GetBoard.GetCopyBoard);
            StepInfo stepPosition = CheckAllSteps(board);

            if (stepPosition == null) return;

            _boardController.Step(stepPosition.I, stepPosition.J);

            if (_boardController.GetBoard.CheckWin() != 0) Debug.Log("WIN!");
        }
        protected virtual void Init(BoardController boardController)
        {
            if (boardController.GetBoard.IsPlayerStep == boardController.GetBoard.IsPair) _mySymvol = 2;
            else _mySymvol = 1;
            _boardController = boardController;
            boardController.StepEvents.AddEvent(Step);
        }
        protected int CheckWinStep(Board board)
        {
            return CheckWinStep(board, _mySymvol);
        }
        protected int CheckWinStep(Board board, int MySymvol)
        {
            int checkWin = board.CheckWin();
            if (checkWin == MySymvol) return 1;
            else if (checkWin == 0) return 0;
            else return -1;
        }
        protected virtual StepInfo CheckAllSteps(Board board)
        {
            int[,] gameBoard = board.GetCopyBoard;
            List<StepInfo> infoAboutAllWinSteps = new List<StepInfo>();
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(0); j++)
                {
                    if (gameBoard[i, j] == 0)
                    {
                        infoAboutAllWinSteps.Add(new StepInfo(i, j));
                    }
                }
            }
            if (infoAboutAllWinSteps.Count == 0) return null;
            return infoAboutAllWinSteps[Random.Range(0, infoAboutAllWinSteps.Count)];
        }
    }
}