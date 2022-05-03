using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class Board
    {
        public Board(int size)
        {
            _gameBoard = new int[size, size];
        }
        public Board(int[,] boardData)
        {
            _gameBoard = new int[boardData.GetLength(0), boardData.GetLength(0)];
            _gameBoard = boardData;
        }
        private int[,] _gameBoard;
        public int[,] GetCopyBoard => CopyBoard(_gameBoard);
        private bool _isPlayerStep = true;
        private bool _isPair = false;
        private int[,] CopyBoard(int[,] board)
        {
            int[,] newBoard = new int[board.GetLength(0), board.GetLength(0)];
            for (int i = 0; i < newBoard.GetLength(0); i++)
            {
                for (int j = 0; j < newBoard.GetLength(0); j++)
                {
                    newBoard[i, j] = board[i, j];
                }
            }
            return newBoard;
        }
        public int CheckWin()
        {
            for (int i = 0; i < _gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < _gameBoard.GetLength(0); j++)
                {
                    int winCount = 0;
                    for (int k = 0; k < _gameBoard.GetLength(0) - i; k++)
                        if (CheckOverlap(i, j, i + k, j)) winCount++;
                    if (winCount >= 3) return _gameBoard[i, j];
                    winCount = 0;

                    for (int k = 0; k < _gameBoard.GetLength(0) - j; k++)
                        if (CheckOverlap(i, j, i, j + k)) winCount++;
                    if (winCount >= 3) return _gameBoard[i, j];
                    winCount = 0;

                    for (int k = 0; k < _gameBoard.GetLength(0) - j - i; k++)
                        if (CheckOverlap(i, j, i + k, j + k)) winCount++;
                    if (winCount >= 3) return _gameBoard[i, j];
                    winCount = 0;

                    for (int k = 0; k < _gameBoard.GetLength(0) - j; k++)
                    {
                        if (i - k < 0) continue;
                        if (CheckOverlap(i, j, i - k, j + k)) winCount++;
                    }
                    if (winCount >= 3) return _gameBoard[i, j];
                }
            }


            return 0;
        }
        private bool CheckOverlap(int i1, int j1, int i2, int j2)
        {
            if (_gameBoard[i1, j1] == _gameBoard[i2, j2]) return _gameBoard[i1, j1] != 0;
            return false;
        }
        private bool BaseStep(int i, int j)
        {
            if (i >= _gameBoard.GetLength(0) || j >= _gameBoard.GetLength(0)
                        || i < 0 || j < 0 || _gameBoard[i, j] != 0) return false;
            if (!_isPair) _gameBoard[i, j] = 1;
            else _gameBoard[i, j] = 2;
            IsGameEnd = CheckGameEnd();
            return true;
        }
        private bool CheckGameEnd()
        {
            for (int i = 0; i < _gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < _gameBoard.GetLength(0); j++)
                {
                    if (_gameBoard[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool Step(int i, int j)
        {
            if (!BaseStep(i, j)) return false;
            _isPair = !_isPair;
            _isPlayerStep = !_isPlayerStep;
            return true;
        }
        public int CheckNextStepWin(int i, int j)
        {
            int result = 0;
            BaseStep(i, j);
            result = CheckWin();
            _gameBoard[i, j] = 0;
            return result;
        }
        public bool IsPlayerStep => _isPlayerStep;
        public bool IsPair => _isPair;
        public bool IsGameEnd = false;
    }
    public class BoardController : MonoBehaviour
    {
        private EventController _stepEvents = new EventController();
        private EventController _winEvents = new EventController();
        private EventController _deffEvents = new EventController();
        private EventController _endGameEvents = new EventController();
        [SerializeField] private List<Block> blocks;
        private Block[,] _blocks;
        private Board _board = new Board(3);
        private void Start()
        {
            int size = (int)Mathf.Sqrt(blocks.Count);
            _blocks = new Block[size, size];
            int blockId = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    _blocks[i, j] = blocks[blockId];
                    blockId++;
                    int I = i;
                    int J = j;
                    _blocks[i, j].AddEvent(() =>
                    {
                        if (_board.IsPlayerStep) Step(I, J);
                        int checkWin = _board.CheckWin();
                        if (checkWin != 0 && !_board.IsPlayerStep) _winEvents.EventsInvoke();
                        else if (checkWin != 0) _deffEvents.EventsInvoke();
                        else if(_board.IsGameEnd) _endGameEvents.EventsInvoke();
                    });
                    if (_blocks[i, j] == null)
                    {
                        Debug.LogError("Null block");
                        return;
                    }
                }
            }
        }
        public void Step(int i, int j)
        {
            if (_board.Step(i, j))
            {
                _blocks[i, j].SetBlock(_board.GetCopyBoard[i, j] - 1);
                _stepEvents.EventsInvoke();
            }
        }
        public EventController StepEvents => _stepEvents;
        public EventController WinEvents => _winEvents;
        public EventController DeffEvents => _deffEvents;
        public EventController EndGameEvents => _endGameEvents;
        public Board GetBoard => _board;
    }
}