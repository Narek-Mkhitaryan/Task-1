using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace WorkingChess
{
    public partial class MainWindow : Window
    {
        Random rand = new Random();
        public int[,] board = { { 0, 0, 0, 0, -1000, -33, -30, -50 },
                                { 0, 0, 0, 0, 0,    0, 0, 0 },
                                { 0, 0, 0, 0, 0,    0, 0, 0 },
                                { 0, 0, 0, 0, 0,    0, 0, 0 },
                                { 0, 0, 0, 0, 0,    0, 0, 0 },
                                { 0, 0, 0, 0, 0,    0, 0, 0 },
                                { 0, 0, 0, 0, 10,   0, 0, 0 },
                                { 0, 0, 0, 0, 1000, 0, 0, 0 }  };
        bool rook, horse, bishop, bKing, wPown = true;
        bool WkFigureClicked, WpFigureClicked;
        double DeltaX, DeltaY;
        int checkX, checkY;
        int bBishopX = 5, bBishopY = 0;
        int bHorseX = 6, bHorseY = 0;
        int bRookX = 7, bRookY = 0;
        int bKingX = 4, bKingY = 0;
        int wKingX = 4, wKingY = 7, oldWKingX = 4, oldWKingY = 7;
        int wPownX = 4, wPownY = 6, oldWPownX = 4, oldWPownY = 6;
        public MainWindow()
        {
            InitializeComponent();
            StackPanel.SetZIndex(WinnerBoard, -3);
        }
        public void CleanBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == 1)
                        board[i, j] = 0;
                }
            }
        }
        public void UpdateBoard()
        {
            CleanBoard();
            rook = bishop = horse = bKing = false;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    switch (board[i, j])
                    {
                        case -50:
                            rook = true;
                            break;
                        case -33:
                            bishop = true;
                            break;
                        case -30:
                            horse = true;
                            break;
                        case -1000:
                            bKing = true;
                            break;
                    }
                }
            }
            if (!rook) { StackPanel.SetZIndex(MyBrFigure, -1); }
            if (!bishop) { StackPanel.SetZIndex(MyBbFigure, -1); }
            if (!horse) { StackPanel.SetZIndex(MyBnFigure, -1); }
            if (!bKing)
            {
                StackPanel.SetZIndex(MyBkFigure, -1);
                WinnerBoard.Text = "White Wins";
                StackPanel.SetZIndex(WinnerBoard, 3);
            }
        }
        public void BlackWin()
        {
            WinnerBoard.Text = "Black Wins";
            StackPanel.SetZIndex(WinnerBoard, 3);
        }
        public void BKingWin()
        {
            StackPanel.SetZIndex(MyWkFigure, -1);
            MyBkFigure.Margin = new Thickness(wKingX * 50, wKingY * 50, 0, 0);
            board[bKingY, bKingX] = 0;
            bKingX = wKingX; bKingY = wKingY;
            board[bKingY, bKingX] = -1000;
            BlackWin();
        }
        public void RookEatKing()
        {
            StackPanel.SetZIndex(MyWkFigure, -1);
            MyBrFigure.Margin = new Thickness(wKingX * 50, wKingY * 50, 0, 0);
            board[bRookY, bRookX] = 0;
            bRookX = wKingX; bRookY = wKingY;
            board[bRookY, bRookX] = -50;
            BlackWin();
        }
        public void BishopEatKing()
        {
            StackPanel.SetZIndex(MyWkFigure, -1);
            MyBbFigure.Margin = new Thickness(wKingX * 50, wKingY * 50, 0, 0);
            board[bBishopY, bBishopX] = 0;
            bBishopX = wKingX; bBishopY = wKingY;
            board[bBishopY, bBishopX] = -33;
            BlackWin();
        }
        public void HorseEatKing()
        {
            StackPanel.SetZIndex(MyWkFigure, -1);
            MyBnFigure.Margin = new Thickness(wKingX * 50, wKingY * 50, 0, 0);
            board[bHorseY, bHorseX] = 0;
            bHorseX = wKingX; bHorseY = wKingY;
            board[bHorseY, bHorseX] = -30;
            BlackWin();
        }
        public bool BKingWin(int X, int Y)
        {
            try { if (board[Y + 1, X] == 1000) { return true; } } catch (Exception) { }
            try { if (board[Y + 1, X + 1] == 1000) { return true; } } catch (Exception) { }
            try { if (board[Y, X + 1] == 1000) { return true; } } catch (Exception) { }
            try { if (board[Y - 1, X + 1] == 1000) { return true; } } catch (Exception) { }
            try { if (board[Y - 1, X] == 1000) { return true; } } catch (Exception) { }
            try { if (board[Y - 1, X - 1] == 1000) { return true; } } catch (Exception) { }
            try { if (board[Y, X - 1] == 1000) { return true; } } catch (Exception) { }
            try { if (board[Y + 1, X - 1] == 1000) { return true; } } catch (Exception) { }
            return false;
        }
        public bool RookWin(int X, int Y)
        {
            int tempX = X;
            int tempY = Y;
            while (X > 0)
            {
                X--;
                if (board[tempY, X] <= 10 && board[tempY, X] != 0) { break; }
                else if (board[tempY, X] == 1000) { return true; }
            }
            X = tempX;
            while (X < 7)
            {
                X++;
                if (board[tempY, X] <= 10 && board[tempY, X] != 0) { break; }
                else if (board[tempY, X] == 1000) { return true; }
            }
            while (Y > 0)
            {
                Y--;
                if (board[Y, tempX] <= 10 && board[Y, tempX] != 0) { break; }
                else if (board[Y, tempX] == 1000) { return true; }
            }
            Y = tempY;
            while (Y < 7)
            {
                Y++;
                if (board[Y, tempX] <= 10 && board[Y, tempX] != 0) { break; }
                else if (board[Y, tempX] == 1000) { return true; }
            }
            return false;
        }
        public bool BishopWin(int X, int Y)
        {
            int tempX = X;
            int tempY = Y;
            while (X > 0 && Y > 0)
            {
                X--; Y--;
                if (board[Y, X] <= 10 && board[Y, X] != 0) { break; }
                else if (board[Y, X] == 1000) { return true; }
            }
            X = tempX;
            Y = tempY;
            while (X > 0 && Y < 7)
            {
                X--; Y++;
                if (board[Y, X] <= 10 && board[Y, X] != 0) { break; }
                else if (board[Y, X] == 1000) { return true; }
            }
            X = tempX;
            Y = tempY;
            while (X < 7 && Y > 0)
            {
                X++; Y--;
                if (board[Y, X] <= 10 && board[Y, X] != 0) { break; }
                else if (board[Y, X] == 1000) { return true; }
            }
            X = tempX;
            Y = tempY;
            while (X < 7 && Y < 7)
            {
                X++; Y++;
                if (board[Y, X] <= 10 && board[Y, X] != 0) { break; }
                else if (board[Y, X] == 1000) { return true; }
            }
            return false;
        }
        public bool HorseWin(int X, int Y)
        {
            try { if (board[Y + 1, X + 2] == 1000) { return true; } } catch (Exception) { }
            try { if (board[Y + 1, X - 2] == 1000) { return true; } } catch (Exception) { }
            try { if (board[Y - 1, X + 2] == 1000) { return true; } } catch (Exception) { }
            try { if (board[Y - 1, X - 2] == 1000) { return true; } } catch (Exception) { }
            try { if (board[Y + 2, X + 1] == 1000) { return true; } } catch (Exception) { }
            try { if (board[Y + 2, X - 1] == 1000) { return true; } } catch (Exception) { }
            try { if (board[Y - 2, X + 1] == 1000) { return true; } } catch (Exception) { }
            try { if (board[Y - 2, X - 1] == 1000) { return true; } } catch (Exception) { }
            return false;
        }
        public bool RookCheck(int X, int Y)
        {
            int tempX = X;
            int tempY = Y;
            while (X > 0)
            {
                X--;
                if (board[tempY, X] < 10 && board[tempY, X] != 0) { break; }
                else if (RookWin(X, Y)) { checkX = X; checkY = Y; return true; }
            }
            X = tempX;
            while (X < 7)
            {
                X++;
                if (board[tempY, X] < 10 && board[tempY, X] != 0) { break; }
                else if (RookWin(X, Y)) { checkX = X; checkY = Y; return true; }
            }
            X = tempX;
            while (Y > 0)
            {
                Y--;
                if (board[Y, tempX] < 10 && board[Y, tempX] != 0) { break; }
                else if (RookWin(X, Y)) { checkX = X; checkY = Y; return true; }
            }
            Y = tempY;
            while (Y < 7)
            {
                Y++;
                if (board[Y, tempX] < 10 && board[Y, tempX] != 0) { break; }
                else if (RookWin(X, Y)) { checkX = X; checkY = Y; return true; }
            }
            return false;
        }
        public bool BishopCheck(int X, int Y)
        {
            int tempX = X;
            int tempY = Y;
            while (X > 0 && Y > 0)
            {
                X--; Y--;
                if (board[Y, X] < 10 && board[Y, X] != 0) { break; }
                else if (BishopWin(X, Y)) { checkX = X; checkY = Y; return true; }
            }
            X = tempX;
            Y = tempY;
            while (X > 0 && Y < 7)
            {
                X--; Y++;
                if (board[Y, X] < 10 && board[Y, X] != 0) { break; }
                else if (BishopWin(X, Y)) { checkX = X; checkY = Y; return true; }
            }
            X = tempX;
            Y = tempY;
            while (X < 7 && Y > 0)
            {
                X++; Y--;
                if (board[Y, X] < 10 && board[Y, X] != 0) { break; }
                else if (BishopWin(X, Y)) { checkX = X; checkY = Y; return true; }
            }
            X = tempX;
            Y = tempY;
            while (X < 7 && Y < 7)
            {
                X++; Y++;
                if (board[Y, X] < 10 && board[Y, X] != 0) { break; }
                else if (BishopWin(X, Y)) { checkX = X; checkY = Y; return true; }
            }
            return false;
        }
        public bool HorseCheckCondition(int X, int Y)
        {
            if (!(X >= 0 && X < 8 && Y >= 0 && Y < 8)) { return false; }
            else if (HorseWin(X, Y) && board[Y, X] >= 0) { checkX = X; checkY = Y; return true; }
            return false;
        }
        public bool HorseCheck(int X, int Y)
        {
            if (HorseCheckCondition(X + 2, Y + 1)) { return true; }
            else if (HorseCheckCondition(X - 2, Y + 1)) { return true; }
            else if (HorseCheckCondition(X + 2, Y - 1)) { return true; }
            else if (HorseCheckCondition(X - 2, Y - 1)) { return true; }
            else if (HorseCheckCondition(X + 1, Y + 2)) { return true; }
            else if (HorseCheckCondition(X - 1, Y + 2)) { return true; }
            else if (HorseCheckCondition(X + 1, Y - 2)) { return true; }
            else if (HorseCheckCondition(X - 1, Y - 2)) { return true; }
            return false;
        }
        public bool RandomRookMove(int X, int Y)
        {
            (int X, int Y)[] rookMoves = new (int, int)[14];
            int sqaures = 0;
            int tempX = X;
            int tempY = Y;
            while (X > 0)
            {
                X--;
                if (board[tempY, X] < 10 && board[tempY, X] != 0) { break; }
                else
                {
                    rookMoves[sqaures].X = X;
                    rookMoves[sqaures].Y = Y;
                    sqaures++;
                }
            }
            X = tempX;
            while (X < 7)
            {
                X++;
                if (board[tempY, X] < 10 && board[tempY, X] != 0) { break; }
                else
                {
                    rookMoves[sqaures].X = X;
                    rookMoves[sqaures].Y = Y;
                    sqaures++;
                }
            }
            while (Y > 0)
            {
                Y--;
                if (board[Y, tempX] < 10 && board[Y, tempX] != 0) { break; }
                else
                {
                    rookMoves[sqaures].X = X;
                    rookMoves[sqaures].Y = Y;
                    sqaures++;
                }
            }
            Y = tempY;
            while (Y < 7)
            {
                Y++;
                if (board[Y, tempX] < 10 && board[Y, tempX] != 0) { break; }
                else
                {
                    rookMoves[sqaures].X = X;
                    rookMoves[sqaures].Y = Y;
                    sqaures++;
                }
            }
            if (sqaures == 0) { return false; }
            else
            {
                int index = rand.Next(0, sqaures);
                checkX = rookMoves[index].X;
                checkY = rookMoves[index].Y;
                return true;
            }
        }
        public bool RandomBishopMove(int X, int Y)
        {
            (int X, int Y)[] bishopMoves = new (int, int)[13];
            int sqaures = 0;
            int tempX = X;
            int tempY = Y;
            while (X > 0 && Y > 0)
            {
                X--; Y--;
                if (board[Y, X] < 10 && board[Y, X] != 0) { break; }
                else
                {
                    bishopMoves[sqaures].X = X;
                    bishopMoves[sqaures].Y = Y;
                    sqaures++;
                }
            }
            X = tempX;
            Y = tempY;
            while (X > 0 && Y < 7)
            {
                X--; Y++;
                if (board[Y, X] < 10 && board[Y, X] != 0) { break; }
                else
                {
                    bishopMoves[sqaures].X = X;
                    bishopMoves[sqaures].Y = Y;
                    sqaures++;
                }
            }
            X = tempX;
            Y = tempY;
            while (X < 7 && Y > 0)
            {
                X++; Y--;
                if (board[Y, X] < 10 && board[Y, X] != 0) { break; }
                else
                {
                    bishopMoves[sqaures].X = X;
                    bishopMoves[sqaures].Y = Y;
                    sqaures++;
                }
            }
            X = tempX;
            Y = tempY;
            while (X < 7 && Y < 7)
            {
                X++; Y++;
                if (board[Y, X] < 10 && board[Y, X] != 0) { break; }
                else
                {
                    bishopMoves[sqaures].X = X;
                    bishopMoves[sqaures].Y = Y;
                    sqaures++;
                }
            }
            if (sqaures == 0) { return false; }
            else
            {
                int index = rand.Next(0, sqaures);
                checkX = bishopMoves[index].X;
                checkY = bishopMoves[index].Y;
                return true;
            }
        }
        public bool RandomHorseMove(int X, int Y)
        {
            (int X, int Y)[] horseMoves = new (int, int)[8];
            int sqaures = 0;
            try
            {
                if (board[Y + 1, X + 2] >= 0)
                {
                    horseMoves[sqaures].X = X + 2;
                    horseMoves[sqaures].Y = Y + 1;
                    sqaures++;
                }
            }
            catch (Exception) { }
            try
            {
                if (board[Y + 1, X - 2] >= 0)
                {
                    horseMoves[sqaures].X = X - 2;
                    horseMoves[sqaures].Y = Y + 1;
                    sqaures++;
                }
            }
            catch (Exception) { }
            try
            {
                if (board[Y - 1, X + 2] >= 0)
                {
                    horseMoves[sqaures].X = X + 2;
                    horseMoves[sqaures].Y = Y - 1;
                    sqaures++;
                }
            }
            catch (Exception) { }
            try
            {
                if (board[Y - 1, X - 2] >= 0)
                {
                    horseMoves[sqaures].X = X - 2;
                    horseMoves[sqaures].Y = Y - 1;
                    sqaures++;
                }
            }
            catch (Exception) { }
            try
            {
                if (board[Y + 2, X + 1] >= 0)
                {
                    horseMoves[sqaures].X = X + 1;
                    horseMoves[sqaures].Y = Y + 2;
                    sqaures++;
                }
            }
            catch (Exception) { }
            try
            {
                if (board[Y + 2, X - 1] >= 0)
                {
                    horseMoves[sqaures].X = X - 1;
                    horseMoves[sqaures].Y = Y + 2;
                    sqaures++;
                }
            }
            catch (Exception) { }
            try
            {
                if (board[Y - 2, X + 1] >= 0)
                {
                    horseMoves[sqaures].X = X + 1;
                    horseMoves[sqaures].Y = Y - 2;
                    sqaures++;
                }
            }
            catch (Exception) { }
            try
            {
                if (board[Y - 2, X - 1] >= 0)
                {
                    horseMoves[sqaures].X = X - 1;
                    horseMoves[sqaures].Y = Y - 2;
                    sqaures++;
                }
            }
            catch (Exception) { }
            if (sqaures == 0) { return false; }
            else
            {
                int index = rand.Next(0, sqaures);
                checkX = horseMoves[index].X;
                checkY = horseMoves[index].Y;
                return true;
            }
        }
        public void RandomBlackKingMove(int X, int Y)
        {
            (int X, int Y)[] kingMoves = new (int, int)[8];
            int sqaures = 0;
            try
            {
                if (board[Y + 1, X] >= 0)
                {
                    kingMoves[sqaures].X = X;
                    kingMoves[sqaures].Y = Y + 1;
                    sqaures++;
                }
            }
            catch (Exception) { }
            try
            {
                if (board[Y + 1, X + 1] >= 0)
                {
                    kingMoves[sqaures].X = X + 1;
                    kingMoves[sqaures].Y = Y + 1;
                    sqaures++;
                }
            }
            catch (Exception) { }
            try
            {
                if (board[Y, X + 1] >= 0)
                {
                    kingMoves[sqaures].X = X + 1;
                    kingMoves[sqaures].Y = Y;
                    sqaures++;
                }
            }
            catch (Exception) { }
            try
            {
                if (board[Y - 1, X + 1] >= 0)
                {
                    kingMoves[sqaures].X = X + 1;
                    kingMoves[sqaures].Y = Y - 1;
                    sqaures++;
                }
            }
            catch (Exception) { }
            try
            {
                if (board[Y - 1, X] >= 0)
                {
                    kingMoves[sqaures].X = X;
                    kingMoves[sqaures].Y = Y - 1;
                    sqaures++;
                }
            }
            catch (Exception) { }
            try
            {
                if (board[Y - 1, X - 1] >= 0)
                {
                    kingMoves[sqaures].X = X - 1;
                    kingMoves[sqaures].Y = Y - 1;
                    sqaures++;
                }
            }
            catch (Exception) { }
            try
            {
                if (board[Y, X - 1] >= 0)
                {
                    kingMoves[sqaures].X = X - 1;
                    kingMoves[sqaures].Y = Y;
                    sqaures++;
                }
            }
            catch (Exception) { }
            try
            {
                if (board[Y + 1, X - 1] >= 0)
                {
                    kingMoves[sqaures].X = X - 1;
                    kingMoves[sqaures].Y = Y + 1;
                    sqaures++;
                }
            }
            catch (Exception) { }
            if (sqaures == 0) { return; }
            else
            {
                int index = rand.Next(0, sqaures);
                checkX = kingMoves[index].X;
                checkY = kingMoves[index].Y;
            }
            if (checkX == wPownX && checkY == wPownY) { StackPanel.SetZIndex(MyWpFigure, -1); wPown = false; }
            MyBkFigure.Margin = new Thickness(checkX * 50, checkY * 50, 0, 0);
            board[bKingY, bKingX] = 0;
            bKingX = checkX; bKingY = checkY;
            board[bKingY, bKingX] = -1000;
        }
        public void BlackMove()
        {
            UpdateBoard();
            if (rook && RookWin(bRookX, bRookY)) { RookEatKing(); return; }
            if (bishop && BishopWin(bBishopX, bBishopY)) { BishopEatKing(); return; }
            if (horse && HorseWin(bHorseX, bHorseY)) { HorseEatKing(); return; }
            if (bKing && BKingWin(bKingX, bKingY)) { BKingWin(); return; }
            if (rook && RookCheck(bRookX, bRookY))
            {
                if (checkX == wPownX && checkY == wPownY) { StackPanel.SetZIndex(MyWpFigure, -1); wPown = false; }
                MyBrFigure.Margin = new Thickness(checkX * 50, checkY * 50, 0, 0);
                board[bRookY, bRookX] = 0;
                bRookX = checkX; bRookY = checkY;
                board[bRookY, bRookX] = -50;
                return;
            }
            if (bishop && BishopCheck(bBishopX, bBishopY))
            {
                if (checkX == wPownX && checkY == wPownY) { StackPanel.SetZIndex(MyWpFigure, -1); wPown = false; }
                MyBbFigure.Margin = new Thickness(checkX * 50, checkY * 50, 0, 0);
                board[bBishopY, bBishopX] = 0;
                bBishopX = checkX; bBishopY = checkY;
                board[bBishopY, bBishopX] = -33;
                return;
            }
            if (horse && HorseCheck(bHorseX, bHorseY))
            {
                if (checkX == wPownX && checkY == wPownY) { StackPanel.SetZIndex(MyWpFigure, -1); wPown = false; }
                MyBnFigure.Margin = new Thickness(checkX * 50, checkY * 50, 0, 0);
                board[bHorseY, bHorseX] = 0;
                bHorseX = checkX; bHorseY = checkY;
                board[bHorseY, bHorseX] = -30;
                return;
            }
            if (rook && RandomRookMove(bRookX, bRookY))
            {
                if (checkX == wPownX && checkY == wPownY) { StackPanel.SetZIndex(MyWpFigure, -1); wPown = false; }
                MyBrFigure.Margin = new Thickness(checkX * 50, checkY * 50, 0, 0);
                board[bRookY, bRookX] = 0;
                bRookX = checkX; bRookY = checkY;
                board[bRookY, bRookX] = -50;
                return;
            }
            if (bishop && RandomBishopMove(bBishopX, bBishopY))
            {
                if (checkX == wPownX && checkY == wPownY) { StackPanel.SetZIndex(MyWpFigure, -1); wPown = false; }
                MyBbFigure.Margin = new Thickness(checkX * 50, checkY * 50, 0, 0);
                board[bBishopY, bBishopX] = 0;
                bBishopX = checkX; bBishopY = checkY;
                board[bBishopY, bBishopX] = -33;
                return;
            }
            if (horse && RandomHorseMove(bHorseX, bHorseY))
            {
                if (checkX == wPownX && checkY == wPownY) { StackPanel.SetZIndex(MyWpFigure, -1); wPown = false; }
                MyBnFigure.Margin = new Thickness(checkX * 50, checkY * 50, 0, 0);
                board[bHorseY, bHorseX] = 0;
                bHorseX = checkX; bHorseY = checkY;
                board[bHorseY, bHorseX] = -30;
                return;
            }
            RandomBlackKingMove(bKingX, bKingY);
        }
        public bool ValidKingMoves()
        {
            try
            {
                if (board[wKingY, wKingX] <= 0)
                {
                    return true;
                }
            }
            catch (Exception) { }
            return false;
        }
        public bool ValidPownMoves()
        {
            try
            {
                if (board[wPownY, wPownX] == 0)
                {
                    return true;
                }
            }
            catch (Exception) { }
            return false;
        }
        public bool PownCapturable()
        {
            if (board[wPownY, wPownX] < 0)
            {
                return true;
            }
            return false;
        }
        public void PownMove()
        {
            MyWpFigure.Margin = new Thickness(wPownX * 50, wPownY * 50, 0, 0);
            board[wPownY, wPownX] = 10;
            board[oldWPownY, oldWPownX] = 0;
            oldWPownX = wPownX;
            oldWPownY = wPownY;
            BlackMove();
        }
        void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (WkFigureClicked)
                MyWkFigure.Margin = new Thickness(e.GetPosition(this).X - DeltaX,
                e.GetPosition(this).Y - DeltaY, 0, 0);
            if (WpFigureClicked)
                MyWpFigure.Margin = new Thickness(e.GetPosition(this).X - DeltaX,
                e.GetPosition(this).Y - DeltaY, 0, 0);
        }
        void MyWkFigure_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                StackPanel.SetZIndex(MyWkFigure, 1);
                WkFigureClicked = true;
                DeltaX = e.GetPosition(this).X - MyWkFigure.Margin.Left;
                DeltaY = e.GetPosition(this).Y - MyWkFigure.Margin.Top;
            }
        }
        void MyWkFigure_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StackPanel.SetZIndex(MyWkFigure, 0);
            WkFigureClicked = false;
            wKingX = (int)(MyWkFigure.Margin.Left + 25) / 50;
            wKingY = (int)(MyWkFigure.Margin.Top + 25) / 50;
            CleanBoard();
            if (ValidKingMoves() && (Math.Abs(oldWKingX - wKingX) <= 1 &&
                Math.Abs(oldWKingY - wKingY) <= 1))
            {
                MyWkFigure.Margin = new Thickness(wKingX * 50, wKingY * 50, 0, 0);
                board[wKingY, wKingX] = 1000;
                board[oldWKingY, oldWKingX] = 0;
                oldWKingX = wKingX;
                oldWKingY = wKingY;
                BlackMove();
                return;
            }
            MyWkFigure.Margin = new Thickness(oldWKingX * 50, oldWKingY * 50, 0, 0);
        }
        void MyWpFigure_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton && wPown)
            {
                StackPanel.SetZIndex(MyWpFigure, 1);
                WpFigureClicked = true;
                DeltaX = e.GetPosition(this).X - MyWpFigure.Margin.Left;
                DeltaY = e.GetPosition(this).Y - MyWpFigure.Margin.Top;
            }
        }
        void MyWpFigure_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StackPanel.SetZIndex(MyWpFigure, 1);
            WpFigureClicked = false;
            wPownX = (int)(MyWpFigure.Margin.Left + 25) / 50;
            wPownY = (int)(MyWpFigure.Margin.Top + 25) / 50;
            CleanBoard();
            if (ValidPownMoves() && (oldWPownY - wPownY == 1 ||
                (oldWPownY - wPownY == 2 && oldWPownY == 6)) && oldWPownX == wPownX)
            {
                PownMove();
                return;
            }
            else if (PownCapturable() && oldWPownY - wPownY == 1 && oldWPownX + 1 == wPownX)
            {
                PownMove();
                return;
            }
            else if (PownCapturable() && oldWPownY - wPownY == 1 && oldWPownX - 1 == wPownX)
            {
                PownMove();
                return;
            }
            MyWpFigure.Margin = new Thickness(oldWPownX * 50, oldWPownY * 50, 0, 0);
        }
    }
}
