using System;
using System.Threading;

namespace Geometry
{
    class Program
    {
        static void Main(string[] args)
        {
            StartGame();
        }
        static void StartGame()
        {
            Console.WriteLine("Перед вами игра Geometry, где каждый из участников заполняет поле фигурами, по все клетки поля не будут заполнены");
            Console.WriteLine("Побеждает тот игрок, кто заполнит большее количество площади поля");
            Console.WriteLine("Для начала игры вам необходимо ввести размеры поля - его ширину и длину");
            string[,] playingField = FillField();
            Console.WriteLine("Так же пользователь задаёт количество ходов");
            int numberMoves = GetNumberMoves();
            ShowField(playingField);
            string[,] finalField = Game(numberMoves, playingField);
            EndGame(finalField);
        }

        static string[,] Game(int numberMoves, string[,] array)
        {
            int firstPlayerMoves = 4; //numberMoves;
            int secondPlayerMoves = 4; // numberMoves;
            for (int allMoves = numberMoves; allMoves > 0; allMoves--)
            {
                CheckFullField(array);

                if (firstPlayerMoves > 0)
                {
                    int player = 1;
                    Console.WriteLine("Первый игрок делает ход");
                    MakeMove(array, player);
                    firstPlayerMoves--;
                }

                CheckFullField(array);

                if (secondPlayerMoves > 0)
                {

                    int player = 2;
                    Console.WriteLine("Второй игрок делает ход");
                    MakeMove(array, player);
                    secondPlayerMoves--;
                }
            }
            return array;
        }

        static void CheckFullField(string[,] array)
        {
            int counter = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] != ".")
                    {
                        counter++;
                    }
                }
            }
            if (counter == array.Length)
            {
                EndGame(array);
            }
        }

        static bool CheckingInsertion(string[,] array, string[,] figure)
        {
            int counter = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == ".")
                    {
                        if(!(i + figure.GetLength(0) > array.GetLength(0) || j + figure.GetLength(1) > array.GetLength(1)))
                        {
                            for (int k = i; k < i + figure.GetLength(0); k++)
                            {
                                for (int l = j; l < j + figure.GetLength(1); l++)
                                {
                                    if (array[k, l] == ".")
                                    {
                                        counter++;
                                    }
                                }
                            }
                        }
                    }

                    if (counter == figure.GetLength(0)*figure.GetLength(1))
                    {
                        return true;
                    }

                }

            }
            return false;
            //if (check == false)
            //{
            //    count++;
            //    CheckField(check);
            //}

            //if (count == 2)
            //{
            //    Console.WriteLine("Переход хода");
            //}
        }


        static string[,] MakeMove(string[,] array, int player)
        {

            string[,] figure = RollDice();
            ShowField(figure);
            if (CheckingInsertion(array, figure))
            {
                ChangingField(array, figure, player);
                Console.Clear();
                ShowField(array);
                return array;
            }
            else
            {
                Console.WriteLine("Переброс");
                figure = RollDice();
                ShowField(figure);
                if (CheckingInsertion(array, figure))
                {
                    ChangingField(array, figure, player);
                    Console.Clear();
                    ShowField(array);
                    return array;
                }
                else Console.WriteLine("Неповезло, переход хода");
            }
            return array;
        }

        static string[,] RollDice()
        {
            Random rnd = new Random();
            int firstCube = rnd.Next(1, 6);
            int secondCube = rnd.Next(1, 6);
            string[,] figure = new string[firstCube, secondCube];

            Console.WriteLine("Полученная фигура, которую необходимо вставить в массив");

            for (int i = 0; i < figure.GetLength(0); i++)
            {
                for (int j = 0; j < figure.GetLength(1); j++)
                {
                    figure[i, j] = "*";
                }
            }

            return figure;
        }

        static string[,] ChangingField(string[,] array, string[,] figure, int player)
        {
        check:
            Console.WriteLine("Введите точки, с которых хотите отрисовать фигуру: ");

            int CoordinateX = GetCoordinateX(array);
            int CoordinateY = GetCoordinateY(array);
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if ((i >= CoordinateX - 1 && i < CoordinateX - 1 + figure.GetLength(0)) && (j >= CoordinateY - 1 && j < CoordinateY - 1 + figure.GetLength(1)))
                    {
                        if (array[i, j] != "." || (CoordinateX - 1 + figure.GetLength(0) > array.GetLength(0)) || (CoordinateY - 1 + figure.GetLength(1) > array.GetLength(1)))
                        {
                            Console.WriteLine("Неккорректная постановка фигуры");
                            goto check;
                        }
                        switch (player)
                        {
                            case 1:
                                array[i, j] = "1";
                                break;
                            case 2:
                                array[i, j] = "2";
                                break;
                        }
                    }
                }
            }

            return array;
        }

        static int GetNumberMoves()
        {
            Console.WriteLine("Введите количество ходов, не меньше 20");
            int number = 0;

            while (!int.TryParse(Console.ReadLine(), out number) || number <= 19)
            {
                Console.WriteLine("Неккоретное значение для количества ходов, повторите ввод:");
            }

            return number;
        }

        static int GetCoordinateX(string[,] array)
        {
            int number = 0;

            while (!int.TryParse(Console.ReadLine(), out number) || number > array.GetLength(0) || number < 0)
            {
                Console.WriteLine("Некорректное значение координаты X, повторите ввод:");
            }

            return number;
        }

        static int GetCoordinateY(string[,] array)
        {
            int number = 0;

            while (!int.TryParse(Console.ReadLine(), out number) || number > array.GetLength(1) || number < 0)
            {
                Console.WriteLine("Некорректное значение координаты Y, повторите ввод:");
            }

            return number;
        }

        static int GetX()
        {
            Console.WriteLine("Введите длину(X) поля, не меньше 20: ");
            int number = 0;

            while (!int.TryParse(Console.ReadLine(), out number) || number <= 19)
            {
                Console.WriteLine("Некорректное значение X, повторите ввод:");
            }

            return number;
        }
        static int GetY()
        {
            Console.WriteLine("Введите ширину(Y) поля, не меньше 30: ");
            int number = 0;

            while (!int.TryParse(Console.ReadLine(), out number) || number <= 29)
            {
                Console.WriteLine("Некорректное значение Y, повторите ввод:");
            }

            return number;
        }

        static string[,] FillField()
        {
            int X = 5; //GetX();
            int Y = 5; //GetY();
            string[,] array = new string[X, Y];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = ".";
                }
            }

            return array;
        }

        static void ShowField(string[,] array)
        {
            Console.Write("   ");
            for (int i = 0; i < array.GetLength(1); i++)
            {
                Console.Write("{0,-3}", i + 1);
            }

            Console.WriteLine();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        if (i < 9 && j == 0)
                        {
                            Console.Write($"{i + 1}  ");
                        }
                        else Console.Write($"{i + 1} ");
                    }

                    Console.Write("{0,-3}", array[i, j]);
                }
                Console.WriteLine("");
            }
        }

        static void EndGame(string[,] array)
        {
            int counterFirstPlayer = 0;
            int counterSecondPlayer = 0;
            Console.WriteLine("У обоих игроков закончилось количество ходов.");
            Console.WriteLine("Подстчёт очков");
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(0); j++)
                {

                    if (array[i, j] == "1")
                    {
                        counterFirstPlayer++;
                    }

                    if (array[i, j] == "2")
                    {
                        counterSecondPlayer++;
                    }
                }
            }

            if (counterFirstPlayer > counterSecondPlayer)
            {
                Console.WriteLine($"Победил игрок номер 1! Первый игрок набрал {counterFirstPlayer} очков, Второй игрок набрал {counterSecondPlayer} очков");
            }
            else if (counterSecondPlayer == counterFirstPlayer)
            {
                Console.WriteLine($"Ничья! Счёт: {counterFirstPlayer} :{counterSecondPlayer}");
            }
            else Console.WriteLine($"Победил игрок номер 2! Второй игрок набрал {counterSecondPlayer} очков, Первый игрок набрал {counterFirstPlayer} очков");
        }
    }
}
