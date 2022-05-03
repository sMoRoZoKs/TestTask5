using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class LowAIModul : BaseAIModul
    {
        public LowAIModul(BoardController boardController)
        {
            Init(boardController);
        }
        // public override void Step()
        // {
        //     if (_boardController.GetBoard.IsPlayerStep) return;

        //     Board board = new Board(_boardController.GetBoard.GetCopyBoard);
        //     StepInfo stepPosition = CheckAllSteps(board);
        //     _boardController.Step(stepPosition.I, stepPosition.J);

        //     if (_boardController.GetBoard.CheckWin() != 0) Debug.Log("WIN!");
        // }
        // private StepInfo CheckAllSteps(Board board)
        // {
        //     StepInfo stepPosition = new StepInfo();
        //     int[,] gameBoard = board.GetCopyBoard;
        //     List<StepInfo> infoAboutAllWinSteps = new List<StepInfo>();
        //     for (int i = 0; i < gameBoard.GetLength(0); i++)
        //     {
        //         for (int j = 0; j < gameBoard.GetLength(0); j++)
        //         {
        //             if (gameBoard[i, j] == 0)
        //             {
        //                 int checkWin = CheckWinStep(board);
        //                 if (checkWin == 0)
        //                 {
        //                     var newBoard = new Board(board.GetCopyBoard);
        //                     newBoard.Step(i, j);
        //                     StepInfo stepInfo = CheckAllSteps(newBoard);
        //                     stepPosition.WinInfo += stepInfo.WinInfo;
        //                     stepPosition.CountSteps += stepInfo.CountSteps + 1;
        //                     infoAboutAllWinSteps.Add(stepPosition);
        //                     stepPosition.I = i;
        //                     stepPosition.J = j;
        //                 }
        //                 else if (checkWin >= 0)
        //                 {
        //                     stepPosition.WinInfo++;
        //                     stepPosition.I = i;
        //                     stepPosition.J = j;
        //                     Debug.Log(stepPosition.I + " " + stepPosition.J + "count steps: " + stepPosition.CountSteps);
        //                 }
        //                 if (stepPosition.WinInfo > 0)
        //                 {
        //                     // return stepPosition;
        //                 }
        //                 if (checkWin < 0)
        //                 {
        //                     return stepPosition;
        //                 }
        //                 stepPosition.CountSteps = RemoveMaxCountSteps(infoAboutAllWinSteps).CountSteps;
        //                 infoAboutAllWinSteps.Clear();
        //             }
        //         }
        //     }
        //     return stepPosition;
        // }
        // private StepInfo RemoveMaxCountSteps(List<StepInfo> infoSteps)
        // {
        //     StepInfo minSteps = infoSteps[0];
        //     for (int i = 0; i < infoSteps.Count; i++)
        //     {
        //         if (infoSteps[i].CountSteps < minSteps.CountSteps)
        //         {
        //             minSteps = infoSteps[i];
        //         }
        //     }
        //     return minSteps;
        // }
        // private int CheckWinStep(Board board)
        // {
        //     int checkWin = board.CheckWin();
        //     if (checkWin == _mySymvol) return 1;
        //     else if (checkWin == 0) return 0;
        //     else return -1;
        // }
    }
}