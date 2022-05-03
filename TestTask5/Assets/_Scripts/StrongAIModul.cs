using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class StrongAIModul : BaseAIModul
    {
        public StrongAIModul(BoardController boardController)
        {
            Init(boardController);
        }
        private bool isFirstStep = true;
        public override void Step()
        {
            if (_boardController.GetBoard.IsPlayerStep) return;

            Board board = new Board(_boardController.GetBoard.GetCopyBoard);
            StepInfo stepPosition;
            if (isFirstStep)
            {
                stepPosition = base.CheckAllSteps(board);
                isFirstStep = false;
            }
            else stepPosition = CheckAllSteps(board);

            if (stepPosition == null) stepPosition = base.CheckAllSteps(board);

            if (stepPosition == null) return;

            _boardController.Step(stepPosition.I, stepPosition.J);
        }
        protected override StepInfo CheckAllSteps(Board board)
        {
            StepInfo stepPosition = new StepInfo();
            int[,] gameBoard = board.GetCopyBoard;
            List<StepInfo> infoAboutAllSteps = new List<StepInfo>();

            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(0); j++)
                {
                    if (gameBoard[i, j] == 0)
                    {
                        if (board.IsPlayerStep)
                        {
                            var newBoard = new Board(board.GetCopyBoard);
                            newBoard.Step(i, j);
                            StepInfo stepInfo = CheckAllSteps(newBoard);

                            stepPosition.I = i;
                            stepPosition.J = j;
                            // Debug.Log(stepInfo.WinInfo);
                            if (stepInfo.WinInfo == -1) return stepPosition;
                            if (stepInfo.WinInfo < 0)
                            {
                                stepPosition.WinInfo = stepInfo.WinInfo;
                                infoAboutAllSteps.Add(stepPosition);
                                continue;
                            }
                            int checkWin = CheckWinStep(board);
                            if (checkWin == 0)
                            {
                                stepPosition.WinInfo = 0;
                                infoAboutAllSteps.Add(stepPosition);
                            }
                            else if (checkWin > 0)
                            {
                                stepPosition.WinInfo = 1;
                                infoAboutAllSteps.Add(stepPosition);
                            }
                            else if (checkWin < 0)
                            {
                                stepPosition.WinInfo = -1;
                                infoAboutAllSteps.Add(stepPosition);
                            }
                        }
                        else
                        {
                            int nextStepWin = board.CheckNextStepWin(i, j);
                            if (nextStepWin != 0 && nextStepWin != _mySymvol)
                            {
                                stepPosition.WinInfo = -1;
                            }
                            else if (nextStepWin == _mySymvol)
                            {
                                stepPosition.WinInfo = -1;
                            }
                            else
                            {
                                stepPosition.I = i;
                                stepPosition.J = j;
                                stepPosition.WinInfo = 0;
                                infoAboutAllSteps.Add(stepPosition);
                                continue;
                            }
                            return stepPosition;
                        }
                    }
                }
            }
            return GetGoodStep(infoAboutAllSteps);
        }
        private StepInfo GetGoodStep(List<StepInfo> infoSteps)
        {
            if (infoSteps.Count == 0 || infoSteps == null) return null;
            StepInfo goodStep = infoSteps[0];
            for (int i = 0; i < infoSteps.Count; i++)
            {
                if (infoSteps[i].WinInfo > goodStep.WinInfo)
                {
                    goodStep = infoSteps[i];
                }
            }
            return goodStep;
        }
    }
}