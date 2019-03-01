using System;

namespace Life_sim
{
  class Program
  {
    static char[,] gameboard = new char[18, 34];
    static char[,] tempgameboard = new char[18, 34];
    static char[,] q = new char[3, 3];
    static char[,] w = new char[3, 3];
    static char[,] e = new char[3, 3];
    static char[,] r = new char[3, 3];
    static int ckix = 16;
    static int ckiy = 8;
    static ConsoleKeyInfo cki;
    static int step = 1;
    static int lives = 0;
    static int birthcounter = 0;
    static int survivalcounter = 0;
    static int crowdcounter = 0;
    static int lonelinesscounter = 0;
    static void menu()
    {
      
      Console.Clear();
      Console.SetCursorPosition(5, 5);
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine("W E L C O M E   T O   O U R   S I M U L A T I O N");
      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.DarkCyan;
      Console.Write("1-->");
      Console.ForegroundColor = ConsoleColor.Magenta;
      Console.WriteLine("If you want to play the game please enter n");
      Console.WriteLine();
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.DarkCyan;
      Console.Write("2-->");
      Console.ForegroundColor = ConsoleColor.Magenta;
      Console.WriteLine("If you want to inform about game rules please enter m");
      Console.WriteLine();
      Console.WriteLine();
      Console.ForegroundColor = ConsoleColor.DarkCyan;
      Console.Write("3-->");
      Console.ForegroundColor = ConsoleColor.Magenta;
      Console.WriteLine("If you want escape please enter k");

      string choice = Convert.ToString(Console.ReadLine());
      if (choice == "m") gamerules();
      if (choice == "k") Environment.Exit(0);
      else
        refresh();
    }
    static void generatematrices()
    {
      //generating gameboard
      for (int i = 0; i < gameboard.GetLength(0); i++)
      {
        for (int j = 0; j < gameboard.GetLength(1); j++)
        {
          gameboard[i, j] = '.';
        }
      }
      //generating q matrix//
      for (int i = 0; i < q.GetLength(0); i++)
      {
        for (int j = 0; j < q.GetLength(1); j++)
        {
          if (j == 1)
          {
            q[i, j] = 'o';
          }
          else
            q[i, j] = '.';
        }
      }
      //generating w matrix//
      for (int i = 0; i < w.GetLength(0); i++)
      {
        for (int j = 0; j < w.GetLength(1); j++)
        {
          if ((j == 2) || (j == 1 && i == 2) || (j == 0 && i == 1))
            w[i, j] = 'o';
          else
            w[i, j] = '.';
        }
      }
      //generating r matrix//
      Regeneratematrixr();
      //generating e matrix//
      for (int i = 0; i < e.GetLength(0); i++)
      {
        for (int j = 0; j < e.GetLength(1); j++)
        {
          if (j == 1 && i == 1)
          {
            e[i, j] = 'o';
          }
          else
            e[i, j] = '.';
        }
      }
    }
    static void gamerules()
    {
      Console.Clear();
      Console.ResetColor();
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("-                 GAME RULES -");
      Console.WriteLine(" 1-- > LifeSim is a simple life simulation program.Each square cell can be alive('o') or dead('.').");
      Console.WriteLine(" 2-->Every cell has eight neighbours.Cells' life is depend on their neighbours.");
      Console.WriteLine();
      Console.WriteLine("*****IF YOU WANT TO STOP THE GAME MUSIC PRESS 'P'! *****");
      Console.WriteLine(" The Meanings of Keys---> ");
      Console.WriteLine("0:clear the screen");
      Console.WriteLine("1:rotate the particle-Q");
      Console.WriteLine("2:rotate the particle-W");
      Console.WriteLine("3:delete the cell");
      Console.WriteLine("4:rotate the particle-R");
      Console.WriteLine("Y:generate new particle-R and place it randomly");
      Console.WriteLine("T:generate new particle-R");
      Console.WriteLine("E or e:placement the particle-E");
      Console.WriteLine("Q or q:placement the particle-Q");
      Console.WriteLine("W or w:placement the particle-W");
      Console.WriteLine("R or r:placement the particle-R");
      Console.WriteLine("Space:generate the next step");
      Console.WriteLine();
      Console.WriteLine("The Evolution Rules--->");
      Console.WriteLine("a)Birth: Any dead cell with exactly three live neighbours becomes a live cell.");
      Console.WriteLine("b)Survival:Any live cell with two or three live neighbours lives in the next generation.");
      Console.WriteLine("c)Death by Loneliness:Any live cell with fewer than two live neighbours dies,as in under-population.");
      Console.WriteLine("d)Death by Overcrowding:Any live cell with  more than three live neighbours dies,as in over-population.");
      menu();

    }
    static void space()//When user presses space key, the next iteration of the simulation is calculated and printed.
    {
      if (cki.Key == ConsoleKey.Spacebar)
      {
        for (int i = 0; i < gameboard.GetLength(0); i++)
        {
          for (int j = 0; j < gameboard.GetLength(1); j++)
          {
            tempgameboard[i, j] = gameboard[i, j];
          }
        }
        for (int i = 1; i < gameboard.GetLength(0) - 1; i++)
        {
          for (int j = 1; j < gameboard.GetLength(1) - 1; j++)
          {
            for (int k = -1; k < 2; k++)
            {
              for (int l = -1; l < 2; l++)
              {
                if (tempgameboard[i, j] == '.' && tempgameboard[i + k, j + l] == 'o') birthcounter++;
                if (tempgameboard[i, j] == 'o' && tempgameboard[i + k, j + l] == 'o') survivalcounter++;
                if (tempgameboard[i, j] == 'o' && tempgameboard[i + k, j + l] == 'o') crowdcounter++;
                if (tempgameboard[i, j] == 'o' && tempgameboard[i + k, j + l] == '.') lonelinesscounter++;
              }
            }
            if (birthcounter == 3) gameboard[i, j] = 'o';
            if (survivalcounter == 2 || survivalcounter == 3) gameboard[i, j] = 'o';
            if (crowdcounter > 4) gameboard[i, j] = '.';
            if (lonelinesscounter > 6) gameboard[i, j] = '.';
            birthcounter = 0;
            survivalcounter = 0;
            crowdcounter = 0;
            lonelinesscounter = 0;
          }
        }
        lives = 0;
        step++;
        refresh();
        Console.Beep(700, 90);
      }
    }
    static void refresh()//This code is used to refresh the screen in every move
    {

      for (int i = 0; i < gameboard.GetLength(0); i++)
      {
        for (int j = 0; j < gameboard.GetLength(1); j++)
        {
          gameboard[i, 0] = '.';  ///these codes for determine the boundaries
          gameboard[0, j] = '.';
          gameboard[i, 33] = '.';
          gameboard[17, j] = '.';
          if (gameboard[i, j] == 'o') lives++;  // for counting 'o''s on the gameboard
        }
      }
      Console.Clear();
      Console.ResetColor();
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine("+-----------------------------------------------------------------+     Step:" + step);
      Console.WriteLine("| " + gameboard[1, 1] + " " + gameboard[1, 2] + " " + gameboard[1, 3] + " " + gameboard[1, 4] + " " + gameboard[1, 5] + " " + gameboard[1, 6] + " " + gameboard[1, 7] + " " + gameboard[1, 8] + " " + gameboard[1, 9] + " " + gameboard[1, 10] + " " + gameboard[1, 11] + " " + gameboard[1, 12] + " " + gameboard[1, 13] + " " + gameboard[1, 14] + " " + gameboard[1, 15] + " " + gameboard[1, 16] + " " + gameboard[1, 17] + " " + gameboard[1, 18] + " " + gameboard[1, 19] + " " + gameboard[1, 20] + " " + gameboard[1, 21] + " " + gameboard[1, 22] + " " + gameboard[1, 23] + " " + gameboard[1, 24] + " " + gameboard[1, 25] + " " + gameboard[1, 26] + " " + gameboard[1, 27] + " " + gameboard[1, 28] + " " + gameboard[1, 29] + " " + gameboard[1, 30] + " " + gameboard[1, 31] + " " + gameboard[1, 32] + " |     Lives: " + lives);
      Console.WriteLine("| " + gameboard[2, 1] + " " + gameboard[2, 2] + " " + gameboard[2, 3] + " " + gameboard[2, 4] + " " + gameboard[2, 5] + " " + gameboard[2, 6] + " " + gameboard[2, 7] + " " + gameboard[2, 8] + " " + gameboard[2, 9] + " " + gameboard[2, 10] + " " + gameboard[2, 11] + " " + gameboard[2, 12] + " " + gameboard[2, 13] + " " + gameboard[2, 14] + " " + gameboard[2, 15] + " " + gameboard[2, 16] + " " + gameboard[2, 17] + " " + gameboard[2, 18] + " " + gameboard[2, 19] + " " + gameboard[2, 20] + " " + gameboard[2, 21] + " " + gameboard[2, 22] + " " + gameboard[2, 23] + " " + gameboard[2, 24] + " " + gameboard[2, 25] + " " + gameboard[2, 26] + " " + gameboard[2, 27] + " " + gameboard[2, 28] + " " + gameboard[2, 29] + " " + gameboard[2, 30] + " " + gameboard[2, 31] + " " + gameboard[2, 32] + " |       " + q[0, 0] + " " + q[0, 1] + " " + q[0, 2]);
      Console.WriteLine("| " + gameboard[3, 1] + " " + gameboard[3, 2] + " " + gameboard[3, 3] + " " + gameboard[3, 4] + " " + gameboard[3, 5] + " " + gameboard[3, 6] + " " + gameboard[3, 7] + " " + gameboard[3, 8] + " " + gameboard[3, 9] + " " + gameboard[3, 10] + " " + gameboard[3, 11] + " " + gameboard[3, 12] + " " + gameboard[3, 13] + " " + gameboard[3, 14] + " " + gameboard[3, 15] + " " + gameboard[3, 16] + " " + gameboard[3, 17] + " " + gameboard[3, 18] + " " + gameboard[3, 19] + " " + gameboard[3, 20] + " " + gameboard[3, 21] + " " + gameboard[3, 22] + " " + gameboard[3, 23] + " " + gameboard[3, 24] + " " + gameboard[3, 25] + " " + gameboard[3, 26] + " " + gameboard[3, 27] + " " + gameboard[3, 28] + " " + gameboard[3, 29] + " " + gameboard[3, 30] + " " + gameboard[3, 31] + " " + gameboard[3, 32] + " |     Q " + q[1, 0] + " " + q[1, 1] + " " + q[1, 2]);
      Console.WriteLine("| " + gameboard[4, 1] + " " + gameboard[4, 2] + " " + gameboard[4, 3] + " " + gameboard[4, 4] + " " + gameboard[4, 5] + " " + gameboard[4, 6] + " " + gameboard[4, 7] + " " + gameboard[4, 8] + " " + gameboard[4, 9] + " " + gameboard[4, 10] + " " + gameboard[4, 11] + " " + gameboard[4, 12] + " " + gameboard[4, 13] + " " + gameboard[4, 14] + " " + gameboard[4, 15] + " " + gameboard[4, 16] + " " + gameboard[4, 17] + " " + gameboard[4, 18] + " " + gameboard[4, 19] + " " + gameboard[4, 20] + " " + gameboard[4, 21] + " " + gameboard[4, 22] + " " + gameboard[4, 23] + " " + gameboard[4, 24] + " " + gameboard[4, 25] + " " + gameboard[4, 26] + " " + gameboard[4, 27] + " " + gameboard[4, 28] + " " + gameboard[4, 29] + " " + gameboard[4, 30] + " " + gameboard[4, 31] + " " + gameboard[4, 32] + " |       " + q[2, 0] + " " + q[2, 1] + " " + q[2, 2]);
      Console.WriteLine("| " + gameboard[5, 1] + " " + gameboard[5, 2] + " " + gameboard[5, 3] + " " + gameboard[5, 4] + " " + gameboard[5, 5] + " " + gameboard[5, 6] + " " + gameboard[5, 7] + " " + gameboard[5, 8] + " " + gameboard[5, 9] + " " + gameboard[5, 10] + " " + gameboard[5, 11] + " " + gameboard[5, 12] + " " + gameboard[5, 13] + " " + gameboard[5, 14] + " " + gameboard[5, 15] + " " + gameboard[5, 16] + " " + gameboard[5, 17] + " " + gameboard[5, 18] + " " + gameboard[5, 19] + " " + gameboard[5, 20] + " " + gameboard[5, 21] + " " + gameboard[5, 22] + " " + gameboard[5, 23] + " " + gameboard[5, 24] + " " + gameboard[5, 25] + " " + gameboard[5, 26] + " " + gameboard[5, 27] + " " + gameboard[5, 28] + " " + gameboard[5, 29] + " " + gameboard[5, 30] + " " + gameboard[5, 31] + " " + gameboard[5, 32] + " |       ");
      Console.WriteLine("| " + gameboard[6, 1] + " " + gameboard[6, 2] + " " + gameboard[6, 3] + " " + gameboard[6, 4] + " " + gameboard[6, 5] + " " + gameboard[6, 6] + " " + gameboard[6, 7] + " " + gameboard[6, 8] + " " + gameboard[6, 9] + " " + gameboard[6, 10] + " " + gameboard[6, 11] + " " + gameboard[6, 12] + " " + gameboard[6, 13] + " " + gameboard[6, 14] + " " + gameboard[6, 15] + " " + gameboard[6, 16] + " " + gameboard[6, 17] + " " + gameboard[6, 18] + " " + gameboard[6, 19] + " " + gameboard[6, 20] + " " + gameboard[6, 21] + " " + gameboard[6, 22] + " " + gameboard[6, 23] + " " + gameboard[6, 24] + " " + gameboard[6, 25] + " " + gameboard[6, 26] + " " + gameboard[6, 27] + " " + gameboard[6, 28] + " " + gameboard[6, 29] + " " + gameboard[6, 30] + " " + gameboard[6, 31] + " " + gameboard[6, 32] + " |       " + w[0, 0] + " " + w[0, 1] + " " + w[0, 2]);
      Console.WriteLine("| " + gameboard[7, 1] + " " + gameboard[7, 2] + " " + gameboard[7, 3] + " " + gameboard[7, 4] + " " + gameboard[7, 5] + " " + gameboard[7, 6] + " " + gameboard[7, 7] + " " + gameboard[7, 8] + " " + gameboard[7, 9] + " " + gameboard[7, 10] + " " + gameboard[7, 11] + " " + gameboard[7, 12] + " " + gameboard[7, 13] + " " + gameboard[7, 14] + " " + gameboard[7, 15] + " " + gameboard[7, 16] + " " + gameboard[7, 17] + " " + gameboard[7, 18] + " " + gameboard[7, 19] + " " + gameboard[7, 20] + " " + gameboard[7, 21] + " " + gameboard[7, 22] + " " + gameboard[7, 23] + " " + gameboard[7, 24] + " " + gameboard[7, 25] + " " + gameboard[7, 26] + " " + gameboard[7, 27] + " " + gameboard[7, 28] + " " + gameboard[7, 29] + " " + gameboard[7, 30] + " " + gameboard[7, 31] + " " + gameboard[7, 32] + " |     W " + w[1, 0] + " " + w[1, 1] + " " + w[1, 2]);
      Console.WriteLine("| " + gameboard[8, 1] + " " + gameboard[8, 2] + " " + gameboard[8, 3] + " " + gameboard[8, 4] + " " + gameboard[8, 5] + " " + gameboard[8, 6] + " " + gameboard[8, 7] + " " + gameboard[8, 8] + " " + gameboard[8, 9] + " " + gameboard[8, 10] + " " + gameboard[8, 11] + " " + gameboard[8, 12] + " " + gameboard[8, 13] + " " + gameboard[8, 14] + " " + gameboard[8, 15] + " " + gameboard[8, 16] + " " + gameboard[8, 17] + " " + gameboard[8, 18] + " " + gameboard[8, 19] + " " + gameboard[8, 20] + " " + gameboard[8, 21] + " " + gameboard[8, 22] + " " + gameboard[8, 23] + " " + gameboard[8, 24] + " " + gameboard[8, 25] + " " + gameboard[8, 26] + " " + gameboard[8, 27] + " " + gameboard[8, 28] + " " + gameboard[8, 29] + " " + gameboard[8, 30] + " " + gameboard[8, 31] + " " + gameboard[8, 32] + " |       " + w[2, 0] + " " + w[2, 1] + " " + w[2, 2]);
      Console.WriteLine("| " + gameboard[9, 1] + " " + gameboard[9, 2] + " " + gameboard[9, 3] + " " + gameboard[9, 4] + " " + gameboard[9, 5] + " " + gameboard[9, 6] + " " + gameboard[9, 7] + " " + gameboard[9, 8] + " " + gameboard[9, 9] + " " + gameboard[9, 10] + " " + gameboard[9, 11] + " " + gameboard[9, 12] + " " + gameboard[9, 13] + " " + gameboard[9, 14] + " " + gameboard[9, 15] + " " + gameboard[9, 16] + " " + gameboard[9, 17] + " " + gameboard[9, 18] + " " + gameboard[9, 19] + " " + gameboard[9, 20] + " " + gameboard[9, 21] + " " + gameboard[9, 22] + " " + gameboard[9, 23] + " " + gameboard[9, 24] + " " + gameboard[9, 25] + " " + gameboard[9, 26] + " " + gameboard[9, 27] + " " + gameboard[9, 28] + " " + gameboard[9, 29] + " " + gameboard[9, 30] + " " + gameboard[9, 31] + " " + gameboard[9, 32] + " |     ");
      Console.WriteLine("| " + gameboard[10, 1] + " " + gameboard[10, 2] + " " + gameboard[10, 3] + " " + gameboard[10, 4] + " " + gameboard[10, 5] + " " + gameboard[10, 6] + " " + gameboard[10, 7] + " " + gameboard[10, 8] + " " + gameboard[10, 9] + " " + gameboard[10, 10] + " " + gameboard[10, 11] + " " + gameboard[10, 12] + " " + gameboard[10, 13] + " " + gameboard[10, 14] + " " + gameboard[10, 15] + " " + gameboard[10, 16] + " " + gameboard[10, 17] + " " + gameboard[10, 18] + " " + gameboard[10, 19] + " " + gameboard[10, 20] + " " + gameboard[10, 21] + " " + gameboard[10, 22] + " " + gameboard[10, 23] + " " + gameboard[10, 24] + " " + gameboard[10, 25] + " " + gameboard[10, 26] + " " + gameboard[10, 27] + " " + gameboard[10, 28] + " " + gameboard[10, 29] + " " + gameboard[10, 30] + " " + gameboard[10, 31] + " " + gameboard[10, 32] + " |       " + r[0, 0] + " " + r[0, 1] + " " + r[0, 2]);
      Console.WriteLine("| " + gameboard[11, 1] + " " + gameboard[11, 2] + " " + gameboard[11, 3] + " " + gameboard[11, 4] + " " + gameboard[11, 5] + " " + gameboard[11, 6] + " " + gameboard[11, 7] + " " + gameboard[11, 8] + " " + gameboard[11, 9] + " " + gameboard[11, 10] + " " + gameboard[11, 11] + " " + gameboard[11, 12] + " " + gameboard[11, 13] + " " + gameboard[11, 14] + " " + gameboard[11, 15] + " " + gameboard[11, 16] + " " + gameboard[11, 17] + " " + gameboard[11, 18] + " " + gameboard[11, 19] + " " + gameboard[11, 20] + " " + gameboard[11, 21] + " " + gameboard[11, 22] + " " + gameboard[11, 23] + " " + gameboard[11, 24] + " " + gameboard[11, 25] + " " + gameboard[11, 26] + " " + gameboard[11, 27] + " " + gameboard[11, 28] + " " + gameboard[11, 29] + " " + gameboard[11, 30] + " " + gameboard[11, 31] + " " + gameboard[11, 32] + " |     R " + r[1, 0] + " " + r[1, 1] + " " + r[1, 2]);
      Console.WriteLine("| " + gameboard[12, 1] + " " + gameboard[12, 2] + " " + gameboard[12, 3] + " " + gameboard[12, 4] + " " + gameboard[12, 5] + " " + gameboard[12, 6] + " " + gameboard[12, 7] + " " + gameboard[12, 8] + " " + gameboard[12, 9] + " " + gameboard[12, 10] + " " + gameboard[12, 11] + " " + gameboard[12, 12] + " " + gameboard[12, 13] + " " + gameboard[12, 14] + " " + gameboard[12, 15] + " " + gameboard[12, 16] + " " + gameboard[12, 17] + " " + gameboard[12, 18] + " " + gameboard[12, 19] + " " + gameboard[12, 20] + " " + gameboard[12, 21] + " " + gameboard[12, 22] + " " + gameboard[12, 23] + " " + gameboard[12, 24] + " " + gameboard[12, 25] + " " + gameboard[12, 26] + " " + gameboard[12, 27] + " " + gameboard[12, 28] + " " + gameboard[12, 29] + " " + gameboard[12, 30] + " " + gameboard[12, 31] + " " + gameboard[12, 32] + " |       " + r[2, 0] + " " + r[2, 1] + " " + r[2, 2]);
      Console.WriteLine("| " + gameboard[13, 1] + " " + gameboard[13, 2] + " " + gameboard[13, 3] + " " + gameboard[13, 4] + " " + gameboard[13, 5] + " " + gameboard[13, 6] + " " + gameboard[13, 7] + " " + gameboard[13, 8] + " " + gameboard[13, 9] + " " + gameboard[13, 10] + " " + gameboard[13, 11] + " " + gameboard[13, 12] + " " + gameboard[13, 13] + " " + gameboard[13, 14] + " " + gameboard[13, 15] + " " + gameboard[13, 16] + " " + gameboard[13, 17] + " " + gameboard[13, 18] + " " + gameboard[13, 19] + " " + gameboard[13, 20] + " " + gameboard[13, 21] + " " + gameboard[13, 22] + " " + gameboard[13, 23] + " " + gameboard[13, 24] + " " + gameboard[13, 25] + " " + gameboard[13, 26] + " " + gameboard[13, 27] + " " + gameboard[13, 28] + " " + gameboard[13, 29] + " " + gameboard[13, 30] + " " + gameboard[13, 31] + " " + gameboard[13, 32] + " |");
      Console.WriteLine("| " + gameboard[14, 1] + " " + gameboard[14, 2] + " " + gameboard[14, 3] + " " + gameboard[14, 4] + " " + gameboard[14, 5] + " " + gameboard[14, 6] + " " + gameboard[14, 7] + " " + gameboard[14, 8] + " " + gameboard[14, 9] + " " + gameboard[14, 10] + " " + gameboard[14, 11] + " " + gameboard[14, 12] + " " + gameboard[14, 13] + " " + gameboard[14, 14] + " " + gameboard[14, 15] + " " + gameboard[14, 16] + " " + gameboard[14, 17] + " " + gameboard[14, 18] + " " + gameboard[14, 19] + " " + gameboard[14, 20] + " " + gameboard[14, 21] + " " + gameboard[14, 22] + " " + gameboard[14, 23] + " " + gameboard[14, 24] + " " + gameboard[14, 25] + " " + gameboard[14, 26] + " " + gameboard[14, 27] + " " + gameboard[14, 28] + " " + gameboard[14, 29] + " " + gameboard[14, 30] + " " + gameboard[14, 31] + " " + gameboard[14, 32] + " |");
      Console.WriteLine("| " + gameboard[15, 1] + " " + gameboard[15, 2] + " " + gameboard[15, 3] + " " + gameboard[15, 4] + " " + gameboard[15, 5] + " " + gameboard[15, 6] + " " + gameboard[15, 7] + " " + gameboard[15, 8] + " " + gameboard[15, 9] + " " + gameboard[15, 10] + " " + gameboard[15, 11] + " " + gameboard[15, 12] + " " + gameboard[15, 13] + " " + gameboard[15, 14] + " " + gameboard[15, 15] + " " + gameboard[15, 16] + " " + gameboard[15, 17] + " " + gameboard[15, 18] + " " + gameboard[15, 19] + " " + gameboard[15, 20] + " " + gameboard[15, 21] + " " + gameboard[15, 22] + " " + gameboard[15, 23] + " " + gameboard[15, 24] + " " + gameboard[15, 25] + " " + gameboard[15, 26] + " " + gameboard[15, 27] + " " + gameboard[15, 28] + " " + gameboard[15, 29] + " " + gameboard[15, 30] + " " + gameboard[15, 31] + " " + gameboard[15, 32] + " |");
      Console.WriteLine("| " + gameboard[16, 1] + " " + gameboard[16, 2] + " " + gameboard[16, 3] + " " + gameboard[16, 4] + " " + gameboard[16, 5] + " " + gameboard[16, 6] + " " + gameboard[16, 7] + " " + gameboard[16, 8] + " " + gameboard[16, 9] + " " + gameboard[16, 10] + " " + gameboard[16, 11] + " " + gameboard[16, 12] + " " + gameboard[16, 13] + " " + gameboard[15, 14] + " " + gameboard[16, 15] + " " + gameboard[16, 16] + " " + gameboard[16, 17] + " " + gameboard[16, 18] + " " + gameboard[16, 19] + " " + gameboard[16, 20] + " " + gameboard[16, 21] + " " + gameboard[16, 22] + " " + gameboard[16, 23] + " " + gameboard[16, 24] + " " + gameboard[16, 25] + " " + gameboard[16, 26] + " " + gameboard[16, 27] + " " + gameboard[16, 28] + " " + gameboard[16, 29] + " " + gameboard[16, 30] + " " + gameboard[16, 31] + " " + gameboard[16, 32] + " |");
      Console.WriteLine("+-----------------------------------------------------------------+");
      Console.SetCursorPosition(ckix, ckiy);
      lives = 0;
    }
    static void placement(char[,] x)///placement procedure for every matrices
    {
      for (int i = 0; i < x.GetLength(0); i++)
      {
        for (int j = 0; j < x.GetLength(1); j++)
        {
          if (gameboard[(ckiy) - 1 + i, (ckix / 2) - 1 + j] != 'o')
            gameboard[(ckiy) - 1 + i, (ckix / 2) - 1 + j] = x[i, j];
        }
      }
      refresh();
    }
    static void Regeneratematrixr() //This code to reproduce the matrix r randomly
    {
      Random rnd = new Random();
      int a = rnd.Next(4, 7);
      for (int i = 0; i < r.GetLength(0); i++)
      {
        for (int j = 0; j < r.GetLength(1); j++)
        {
          r[i, j] = '.';
        }
      }
      while (a > 0)
      {
        int rx = rnd.Next(0, 3);
        int ry = rnd.Next(0, 3);
        if (r[rx, ry] == '.')
        {
          r[rx, ry] = 'o';
          a--;
        }
        else
          continue;
      }
      refresh();
    }
    static char[,] RotateMatrix(char[,] oldMatrix)//this function is used to rotate a specific particle (particle Q, W, R) 90 degrees in clockwise direction.
    {
      char[,] newMatrix = new char[oldMatrix.GetLength(1), oldMatrix.GetLength(0)];
      int newColumn = 0;
      int newRow = 0;
      for (int oldColumn = oldMatrix.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
      {
        newColumn = 0;
        for (int oldRow = 0; oldRow < oldMatrix.GetLength(0); oldRow++)
        {
          newMatrix[oldRow, oldColumn] = oldMatrix[newRow, newColumn];
          newColumn++;
        }
        newRow++;
      }
      return newMatrix;
    }
    static void Main(string[] args)
    {
      generatematrices();

      menu();
      //main.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "main.wav";
      //main.PlayLooping();            
      refresh();
      for (; ; )//a main loop for taking user commands and movements
      {
        if (Console.KeyAvailable)
        {       // true: there is a key in keyboard buffer
          cki = Console.ReadKey(true);       // true: do not write character 
                                             //if (cki.Key == ConsoleKey.P) main.Stop();// this command for stopping the music
                                             //if (cki.Key == ConsoleKey.O) main.PlayLooping();// this command for restarting the music
          if (cki.Key == ConsoleKey.RightArrow && ckix < 64)//cursor right
          {
            Console.SetCursorPosition(ckix, ckiy);
            ckix += 2;
            Console.SetCursorPosition(ckix, ckiy);
          }
          if (cki.Key == ConsoleKey.LeftArrow && ckix > 2)//cursor left
          {
            Console.SetCursorPosition(ckix, ckiy);
            ckix -= 2;
            Console.SetCursorPosition(ckix, ckiy);
          }
          if (cki.Key == ConsoleKey.UpArrow && ckiy > 1)//cursor up
          {
            Console.SetCursorPosition(ckix, ckiy);
            ckiy--;
            Console.SetCursorPosition(ckix, ckiy);
          }
          if (cki.Key == ConsoleKey.DownArrow && ckiy < 16)//cursor down
          {
            Console.SetCursorPosition(ckix, ckiy);
            ckiy++;
            Console.SetCursorPosition(ckix, ckiy);
          }
          /////rotating q matris with key 1
          if (cki.Key == ConsoleKey.NumPad1 || cki.Key == ConsoleKey.D1) q = RotateMatrix(q);
          refresh();
          /////rotating w matris with key 2
          if (cki.Key == ConsoleKey.NumPad2 || cki.Key == ConsoleKey.D2) w = RotateMatrix(w);
          refresh();
          /////rotating r matris with key 4
          if (cki.Key == ConsoleKey.NumPad4 || cki.Key == ConsoleKey.D4) r = RotateMatrix(r);
          refresh();
          /////Regenerate maxtix r with key t
          if (cki.Key == ConsoleKey.T) Regeneratematrixr();
          ///placement q matrix                    
          //placement q matrix to middle cells
          if (cki.Key == ConsoleKey.Q) placement(q);
          //placement w matrix to  cells
          if (cki.Key == ConsoleKey.W) placement(w);
          //placement r matrix to cells
          if (cki.Key == ConsoleKey.R) placement(r);
          ///placement e matrix
          if (cki.Key == ConsoleKey.E)
          {
            gameboard[(ckiy), (ckix / 2)] = e[1, 1];
            refresh();
          }
          ////button y
          if (cki.Key == ConsoleKey.Y)
          {
            Random rnd = new Random();
            int xrnd = rnd.Next(1, 31);
            int yrnd = rnd.Next(1, 15);
            Regeneratematrixr();
            for (int i = 0; i < r.GetLength(1); i++)
            {
              for (int j = 0; j < r.GetLength(1); j++)
              {
                if (gameboard[yrnd - 1 + i, xrnd - 1 + j] != 'o')
                  gameboard[yrnd - 1 + i, xrnd - 1 + j] = r[i, j];
              }
            }
            refresh();
          }
          ///clear game board
          if (cki.Key == ConsoleKey.NumPad0 || cki.Key == ConsoleKey.D0)
          {
            for (int i = 0; i < gameboard.GetLength(0); i++)
            {
              for (int j = 0; j < gameboard.GetLength(1); j++)
              {
                gameboard[i, j] = '.';
              }
            }
            refresh();
          }
          ///button 3 that works deleting one cell at the cursor position
          if (cki.Key == ConsoleKey.NumPad3 || cki.Key == ConsoleKey.D3)
          {
            gameboard[(ckiy), (ckix / 2)] = '.';
            refresh();
          }
          ///space button
          space();
        }
      }
    }
  }
}
