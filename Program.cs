using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConwaysGameOfLife
{
    class Program
    {        
        static void Main(string[] args)
        {
            try
            {
                string file = @"file.txt";
                int turn = 1;
                Console.WriteLine(" **// MENU \\** Entrez le nombre de tours que vous souhaitez simuler");
                int turnNumber = int.Parse(Console.ReadLine());
                int nearbyCells = 0;
                bool stop = false;
                while (stop != true)
                {
                    int nombre;
                    Console.WriteLine(" //** MENU **\\ Choisissez votre constructeur : (1) pour taille et pourcentage, (2) pour lecture de fichier");
                    nombre = int.Parse(Console.ReadLine());
                    switch (nombre)
                    {
                        case 1:
                            Console.WriteLine("Choisissez la taille de la grille");
                            int taille = int.Parse(Console.ReadLine());
                            Console.WriteLine("Choisissez le pourcentage de cellules vivantes");
                            int pourcentage = int.Parse(Console.ReadLine());
                            Grille grille1 = new Grille(taille, pourcentage);
                            Console.WriteLine(" //** MENU **\\ Choisissez le niveau (1 ou 2)");
                            int reponse1 = int.Parse(Console.ReadLine());
                            Console.Clear();
                            if (reponse1 == 2)
                            {
                                grille1.Affiche_Grille("Tour " + turn);
                                Grille grilleClone = new Grille(grille1.Taille); // stockage de la grille à afficher dans une grille Clone sur laquelle on opère
                                Console.ReadLine();
                                while (turn != turnNumber)
                                {
                                    for (int i = 0; i < grille1.Board.GetLength(0); i++)
                                    {
                                        for (int j = 0; j < grille1.Board.GetLength(1); j++)
                                        {
                                            grilleClone.Board[i, j] = grille1.JeuNiveau2(grille1.Board[i, j], nearbyCells); // application du jeu sur la grille clone
                                        }
                                    }
                                    turn++;
                                    Console.Clear();
                                    grille1.Clone(grilleClone); // clonage de la nouvelle grille sur laquelle on a appliqué le jeu dans la grille à afficher
                                    grille1.Affiche_Grille("Tour" + turn);
                                    Console.ReadLine();
                                }
                                stop = true;
                            }
                            else
                            {
                                grille1.Affiche_Grille("Tour " + turn);
                                Grille grilleClone = new Grille(grille1.Taille);
                                Console.ReadLine();
                                while (turn != turnNumber)
                                {
                                    for (int i = 0; i < grille1.Board.GetLength(0); i++)
                                    {
                                        for (int j = 0; j < grille1.Board.GetLength(1); j++)
                                        {
                                            grilleClone.Board[i, j] = grille1.JeuNiveau1(grille1.Board[i, j], nearbyCells);
                                        }
                                    }
                                    turn++;
                                    Console.Clear();
                                    grille1.Clone(grilleClone);
                                    grille1.Affiche_Grille("Tour" + turn);
                                    Console.ReadLine();
                                }
                                stop = true;
                            }

                            break;
                        case 2:

                            Grille grille2 = new Grille(file);
                            Console.WriteLine(" //** MENU **\\ Choisissez le niveau (1 ou 2)");
                            int reponse2 = int.Parse(Console.ReadLine());
                            Console.Clear();
                            if (reponse2 == 2)
                            {
                                grille2.Affiche_Grille("Tour " + turn);
                                Grille grilleClone = new Grille(grille2.Taille);
                                Console.ReadLine();
                                while (turn != turnNumber)
                                {
                                    for (int i = 0; i < grille2.Board.GetLength(0); i++)
                                    {
                                        for (int j = 0; j < grille2.Board.GetLength(1); j++)
                                        {
                                            grilleClone.Board[i, j] = grille2.JeuNiveau2(grille2.Board[i, j], nearbyCells);
                                        }
                                    }
                                    turn++;
                                    Console.Clear();
                                    grille2.Clone(grilleClone);
                                    grille2.Affiche_Grille("Tour" + turn);
                                    Console.ReadLine();
                                }
                                stop = true;
                            }
                            else
                            {
                                grille2.Affiche_Grille("Tour " + turn);
                                Grille grilleClone = new Grille(grille2.Taille);
                                Console.ReadLine();
                                while (turn != turnNumber)
                                {
                                    for (int i = 0; i < grille2.Board.GetLength(0); i++)
                                    {
                                        for (int j = 0; j < grille2.Board.GetLength(1); j++)
                                        {
                                            grilleClone.Board[i, j] = grille2.JeuNiveau1(grille2.Board[i, j], nearbyCells);
                                        }
                                    }
                                    turn++;
                                    Console.Clear();
                                    grille2.Clone(grilleClone);
                                    grille2.Affiche_Grille("Tour" + turn);
                                    Console.ReadLine();
                                }
                                stop = true;
                            }
                            break;

                        default:
                            stop = true;
                            break;
                    }


                }

            }
            catch (FormatException) // catch au cas-où l'utilisateur tenterait planter le programme intentionnellement en entrant autre chose qu'un int dans un int.Parse(Console.Readline());
            {
                Console.WriteLine("An error has occured, solution is closing now");
                Console.ReadLine();
            }
            
        }
    }
}
