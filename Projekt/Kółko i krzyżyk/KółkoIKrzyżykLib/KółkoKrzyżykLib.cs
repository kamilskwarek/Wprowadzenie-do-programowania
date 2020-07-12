using System;

namespace KółkoKrzyżykLib
{
    //typ wyliczeniowy znaków kółka i krzyżka, ich indeksy posłużą do sprwadzenia czy ktoś wygrał i jeśli tak to kto
    public enum Sign { cross = 1, circle = 5,}
    public struct Player
    {
        public string PlayerName; // nazwa gracza
        public int NumberOfWinnings; // ilość zwycięstw
        public Sign SignType; // Znak gracza
    }
    public class KolkoKrzyzyk
    {
        // tablica z współrzędnymi planszy
        public string[,] Position = new string[3, 3] { { "1 1", "1 2", "1 3" }, { "2 1", "2 2", "2 3" }, { "3 1", "3 2", "3 3" } };
        // tablica 3x3 - plansza gry
        private Sign[,] GameBoard = new Sign[3, 3];
        //zwycięstwo któregoś gracza
        private bool PlayerWinner;
        // aktywny gracz
        private int ActivePlayer;
        // tablica graczy
        private Player[] GamePlayer = new Player[2];

        private string GetPlayer1()
        // przypisanie nazwy gracza1
        {
            return GamePlayer[0].PlayerName;
        }
        private void SetPlayer1(string name)
        {
            GamePlayer[0].PlayerName = name;
        }
        //przypisanie nazwy gracza2
        private string GetPlayer2()
        {
            return GamePlayer[1].PlayerName;
        }
        private void SetPlayer2(string name)
        {
            GamePlayer[1].PlayerName = name;
        }
        //aktywny gracz
        private Player GetActive()
        {
            return GamePlayer[this.ActivePlayer];
        }
        public bool Winner
        {
            get
            {
                return PlayerWinner;
            }

        }
        // akrtywny gracz
        public Player Active
        {
            get
            {
                return GetActive();
            }
        }
        // inforamcje o graczu1
        public string Player1
        {
            get
            {
                return GetPlayer1();
            }
            set
            {
                SetPlayer1(value);
            }
        }
        // inforamcje o graczu2
        public string Player2
        {
            get
            {
                return GetPlayer2();
            }
            set
            {
                SetPlayer2(value);
            }
        }
        //inforamcje o stanie planszy
        public Sign[,] Board
        {
            get
            {
                return GameBoard;
            }
        }
        // sprawdzenie czy ktoś wygrał
        private void SUM(int Value)
        {
            if (Value == 3 || Value == 15)
            {
                GamePlayer[ActivePlayer].NumberOfWinnings++;
                PlayerWinner = true;
            }
        }
        //alogrytm sprwadzający czy ktoś wygrał
        private void CheckWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                SUM((int)GameBoard[i, 0] + (int)GameBoard[i, 1] + (int)GameBoard[i, 2]);
                SUM((int)GameBoard[0, i] + (int)GameBoard[1, i] + (int)GameBoard[2, i]);
            }

            SUM((int)GameBoard[0, 0] + (int)GameBoard[1, 1] + (int)GameBoard[2, 2]);
            SUM((int)GameBoard[0, 2] + (int)GameBoard[1, 1] + (int)GameBoard[2, 0]);
        }
        //rozpoczęcie gry
        public void Start()
        {
            // przypisanie znaku
            GamePlayer[0].SignType = Sign.cross;
            GamePlayer[1].SignType = Sign.circle;

            PlayerWinner = false;

            //wyczyszczenie planszy
            System.Array.Clear(GameBoard, 0, GameBoard.Length);
        }

        //nowa gra - zresetowanie liczby zwyciestw 
        public void NewGame()
        {
            GamePlayer[0].NumberOfWinnings = 0;
            GamePlayer[1].NumberOfWinnings = 0;
        }

        //wprowadzanie współrzędnych 
        public bool set(int X, int Y)
        {
            //zmniejszenie wartości podanych przez uzytkownika o 1 żeby odnosiły się do odpowiedniego miejsca w tablicy
            --X;
            --Y;
            // sprwadzenie czy współżędne są prawidłowe
            if (((X > 2) || (Y > 2)) || ((X <= -1) || (Y <= -1)))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Warotość X lub Y jest nie poprawna");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            // sprawdzenie czy pole jest zajęte
            else if (GameBoard[X, Y] > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pole jest już zajęte");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }

            //postawienie znaku
            GameBoard[X, Y] = GetActive().SignType;

            //ktoś wygrał?
            CheckWinner();
            if (!Winner)
            {
                ActivePlayer = (ActivePlayer == 0 ? 1 : 0);
            }
            return true;


        }
    }
}