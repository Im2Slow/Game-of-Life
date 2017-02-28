using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConwaysGameOfLife
{
    class Grille
    {
        //déclaration des champs

        Cellule[,] board;
        int taille;
        int pourcentage;
        string libelle;
        const int AGE_MORT = 5;
        const int ENERGIE = 4;
        const int ENERGIE_REPRODUCTION = 10;
        const int ENERGIE_INITIALE = 1;

        //déclaration des constructeurs

        public Grille (int taille)
        {
            this.taille = taille;
            board = new Cellule[taille, taille];
            for (int i = 0; i < taille; i++)
            {   
                for (int j = 0; j < taille; j++)
                {
                    board[i, j] = new Cellule(false, i, j); // Initialisation d'une grille vide, on n'utilisera pas ce constructeur sauf pour instancier la matrice Clone sur laquelle on effectuera les opérations

                }
            }
        }
        public Grille (int taille, int pourcentage)
        {
            this.taille = taille;
            this.pourcentage = pourcentage;
            Random aleatoire = new Random();
            board = new Cellule[taille, taille];
            for(int i=0; i < board.GetLength(0); i++)
            {
                for(int j=0; j < board.GetLength(1); j++)
                {
                    int nombreAleatoire = aleatoire.Next(100);
                    if (nombreAleatoire<pourcentage)
                    {
                        board[i, j] = new Cellule(true, i, j);
                    }
                    else
                    {
                        board[i,j] = new Cellule(false,i, j);
                    }
                }
            }
        }
        public Grille (string file) 
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines(file); // lecture du fichier et stockage dans un tableau de string
                int lineNumber = 0;
                int charNumber = 0;     
                foreach (string line in lines)
                {
                    lineNumber++;
                    charNumber = line.Length; // calcul et stockage du nombre de caractères par ligne
                }
                if (lineNumber == charNumber) this.taille = lineNumber;
                else Console.WriteLine("la matrice contenue dans file.txt n'est pas carrée");
                board = new Cellule[taille, taille]; // initialisation d'une matrice de cellules adaptée aux dimensions de la matrice contenue dans le fichier
                for (int i = 0; i < taille; i++)
                {
                    for (int j = 0; j < taille; j++)
                    {
                        if (lines[i][j] == '0') board[i, j] = new Cellule(false, i, j);     //traduction de la matrice de caractères du fichier en matrice de cellules.
                        if (lines[i][j] == '1') board[i, j] = new Cellule(true, i, j);
                    }
                }
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("An error has occured : file not readable"); // au cas où pour prevenir d'un potentiel crash
                Console.ReadLine();
            }

         }                                                            

        public Grille (int taille, int pourcentage, string libelle, int age, int energie) // même chose que le 2eme constructeur mais avec des paramètres supplémentaires
        {
            this.taille = taille;
            this.pourcentage = pourcentage;
            this.libelle = libelle;
            Random aleatoire = new Random();
            board = new Cellule[taille, taille];
            for (int i=0; i < board.GetLength(0); i++)
            {
                for(int j=0; j < board.GetLength(1); j++)
                {
                    this.board[i,j].Age = age;
                    this.board[i,j].Energie = energie;

                    int nombreAleatoire = aleatoire.Next(100);                      

                    if (nombreAleatoire < pourcentage)
                    {
                        board[i,j]= new Cellule(true, i, j);
                    }
                    else
                    {
                        board[i,j]= new Cellule(false, i, j);
                    }
                }
            }
        }

        // déclaration des propriétés

        public Cellule[,] Board
        {
            get { return board; }
        }
        public int Taille
        {
            get { return taille; }
        }
        public int Pourcentage
        {
            get { return pourcentage; }
        }

        //déclaration des méthodes

        public void Affiche_Grille(string message)
        {
            Console.WriteLine(message);
            for(int i=0; i < board.GetLength(0); i++)
            {                
                for(int j=0; j < board.GetLength(1); j++)
                {
                    if (board[i, j].Alive == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    board[i, j].Affiche(); // on réutilise la méthode affiche de la classe cellule 
                }
                Console.Write("\n");
            }
        }
        public void Ecriture_Grille(string fichier)
        {
            string[] text = new string[taille];
            for(int i=0; i < board.GetLength(0); i++)
            {
                for(int j=0; j < board.GetLength(1); j++)
                {
                    if (board[i, j].Alive == true) text[i] += "1";
                    else text[i] += "0";
                }
                text[i] += "\n";
            }
            File.WriteAllLines(fichier, text);
        }
        public Cellule VoisinNord (Cellule maCase)
        {
            if (maCase.LinePosition - 1 < 0) return board[maCase.LinePosition - 1 + board.GetLength(0), maCase.ColumnPosition]; // test si le voisin nord est hors de la matrice, et le place de manière a obtenir une grille torique
            else
            {
                return board[maCase.LinePosition - 1, maCase.ColumnPosition]; //sinon retourne la case nord
            }
        }
        public Cellule VoisinNordEst (Cellule maCase)
        {
            if (maCase.LinePosition - 1 < 0 && maCase.ColumnPosition + 1 > board.GetLength(1) - 1) return board[maCase.LinePosition - 1 + board.GetLength(0), maCase.ColumnPosition + 1 - board.GetLength(1)]; // test toricité deux directions à la fois : Nord et Est
            if (maCase.LinePosition - 1 < 0) return board[maCase.LinePosition - 1 + board.GetLength(0), maCase.ColumnPosition]; // test toricité Nord
            if (maCase.ColumnPosition + 1 > board.GetLength(1) - 1) return board[maCase.LinePosition, maCase.ColumnPosition + 1 - board.GetLength(1)]; // test toricité Est
            else
            {
                return board[maCase.LinePosition - 1, maCase.ColumnPosition + 1];
            }
        }
        public Cellule VoisinNordOuest(Cellule maCase)
        {
            if (maCase.LinePosition - 1 < 0 && maCase.ColumnPosition - 1 < 0) return board[maCase.LinePosition - 1 + board.GetLength(0), maCase.ColumnPosition - 1 + board.GetLength(1)];
            if (maCase.LinePosition - 1 < 0) return board[maCase.LinePosition - 1 + board.GetLength(0), maCase.ColumnPosition];
            if (maCase.ColumnPosition - 1 < 0) return board[maCase.LinePosition, maCase.ColumnPosition - 1 + board.GetLength(1)];
            else
            {
                return board[maCase.LinePosition - 1, maCase.ColumnPosition - 1];
            }
        }
        public Cellule VoisinSud(Cellule maCase)
        {
            if (maCase.LinePosition + 1 > board.GetLength(0) - 1) return board[maCase.LinePosition + 1 - board.GetLength(0), maCase.ColumnPosition];

            else
            {
                return board[maCase.LinePosition + 1, maCase.ColumnPosition];
            }
        }
        public Cellule VoisinSudEst(Cellule maCase)
        {
            if (maCase.LinePosition + 1 > board.GetLength(0) - 1 && maCase.ColumnPosition + 1 > board.GetLength(1) - 1) return board[maCase.LinePosition + 1 - board.GetLength(0), maCase.ColumnPosition + 1 - board.GetLength(1)];
            if (maCase.LinePosition + 1 > board.GetLength(0) - 1) return board[maCase.LinePosition + 1 - board.GetLength(0), maCase.ColumnPosition];
            if (maCase.ColumnPosition + 1 > board.GetLength(1) - 1) return board[maCase.LinePosition, maCase.ColumnPosition + 1 - board.GetLength(1)];
            else
            {
                return board[maCase.LinePosition + 1, maCase.ColumnPosition + 1];
            }
        }
        public Cellule VoisinSudOuest(Cellule maCase)
        {
            if (maCase.LinePosition + 1 > board.GetLength(0) - 1 && maCase.ColumnPosition - 1 < 0) return board[maCase.LinePosition + 1 - board.GetLength(0), maCase.ColumnPosition - 1 + board.GetLength(1)];
            if (maCase.ColumnPosition - 1 < 0) return board[maCase.LinePosition, maCase.ColumnPosition - 1 + board.GetLength(1)];
            if (maCase.LinePosition + 1 > board.GetLength(0) - 1) return board[maCase.LinePosition + 1 - board.GetLength(0), maCase.ColumnPosition];
            else
            {
                return board[maCase.LinePosition + 1, maCase.ColumnPosition - 1];
            }
        }
        public Cellule VoisinOuest(Cellule maCase)
        {
            if (maCase.ColumnPosition - 1 < 0) return board[maCase.LinePosition, maCase.ColumnPosition - 1 + board.GetLength(1)];
            else
            {
                return board[maCase.LinePosition, maCase.ColumnPosition - 1];
            }
        }
        public Cellule VoisinEst(Cellule maCase)
        {
            if (maCase.ColumnPosition + 1 > board.GetLength(1) - 1) return board[maCase.LinePosition, maCase.ColumnPosition + 1 - board.GetLength(1)];
            else
            {
                return board[maCase.LinePosition, maCase.ColumnPosition + 1];
            }
        }
        public Cellule JeuNiveau1(Cellule maCase, int cellulesVoisines)
        {
            Cellule newCell = new Cellule(true, 0, 0); // cellule définie par défaut sur laquelle on va effectuer les opérations.
            newCell.Clone(maCase);
            if (VoisinNord(maCase).Alive == true) cellulesVoisines += 1;
            if (VoisinNordEst(maCase).Alive == true) cellulesVoisines += 1;
            if (VoisinNordOuest(maCase).Alive == true) cellulesVoisines += 1;
            if (VoisinSud(maCase).Alive == true) cellulesVoisines += 1;
            if (VoisinSudEst(maCase).Alive == true) cellulesVoisines += 1;      // test de tous les voisins et incrémentation du compteur pour chaque voisin vivant
            if (VoisinSudOuest(maCase).Alive == true) cellulesVoisines += 1;
            if (VoisinEst(maCase).Alive == true) cellulesVoisines += 1;
            if (VoisinOuest(maCase).Alive == true) cellulesVoisines += 1;

            if (maCase.Alive == true &&(cellulesVoisines == 2 | cellulesVoisines == 3)) newCell.Alive = true; //survie
            if (cellulesVoisines > 3 | cellulesVoisines < 2) newCell.Alive = false; // Mort par surpopulation ou solitude
            if (maCase.Alive == false && cellulesVoisines == 3) newCell.Alive = true;//naissance
            return newCell;
        }
        public Cellule JeuNiveau2(Cellule maCase, int cellulesVoisines)
        {
            Cellule newCell = new Cellule(true, 0,0,0,0);
            newCell.Clone(maCase);
            if (VoisinNord(maCase).Alive == true) cellulesVoisines += 1;
            if (VoisinNordEst(maCase).Alive == true) cellulesVoisines += 1;
            if (VoisinNordOuest(maCase).Alive == true) cellulesVoisines += 1;
            if (VoisinSud(maCase).Alive == true) cellulesVoisines += 1;
            if (VoisinSudEst(maCase).Alive == true) cellulesVoisines += 1;
            if (VoisinSudOuest(maCase).Alive == true) cellulesVoisines += 1;
            if (VoisinEst(maCase).Alive == true) cellulesVoisines += 1;
            if (VoisinOuest(maCase).Alive == true) cellulesVoisines += 1;
            
            if (maCase.Alive == true && (cellulesVoisines == 2 | cellulesVoisines == 3)) newCell.Alive = true;
            if (cellulesVoisines > 3 | cellulesVoisines < 2) newCell.Alive = false;
            if (maCase.Alive == false && cellulesVoisines == 3)
            {
                newCell.Alive = true;
                newCell.Age = 0;
                newCell.Energie = ENERGIE_INITIALE;
            }
            if (newCell.Alive == true) // vieillissement et accumulation d'énergie des cellules vivantes
            {
                newCell.Age++;
                newCell.Energie += ENERGIE;

            }
            else // valeurs par défaut des cellules mortes
            {
                newCell.Age = 0;
                newCell.Energie = 0;

            }
            if (maCase.Age == AGE_MORT) newCell.Alive = false; // mort de vieillesse
            if (maCase.Age < AGE_MORT && maCase.Energie >= ENERGIE_REPRODUCTION) // conditions de reproduction
            {
                if(VoisinEst(maCase).Alive == false || VoisinNord(maCase).Alive == false || VoisinNordEst(maCase).Alive == false || VoisinNordOuest(maCase).Alive == false || VoisinOuest(maCase).Alive == false || VoisinSud(maCase).Alive == false || VoisinSudEst(maCase).Alive == false || VoisinSudOuest(maCase).Alive == false) // test si il y a au moins une cellule voisine qui n'est pas vivante pour que la reproduction puisse avoir lieu
                {
                    if( VoisinSudOuest(maCase).Alive == false) // Test une par une si les cellules voisines sont mortes pour que la reproduction puisse avoir lieu
                    {
                        VoisinSudOuest(newCell).Alive = true; // naissance d'une cellule voisine
                        VoisinSudOuest(newCell).Age = 0;
                        VoisinSudOuest(newCell).Energie = ENERGIE_INITIALE;
                    }
                    if (VoisinOuest(maCase).Alive == false)
                    {
                        VoisinOuest(newCell).Alive = true;
                        VoisinOuest(newCell).Age = 0;
                        VoisinOuest(newCell).Energie = ENERGIE_INITIALE;
                    }
                    if (VoisinSud(maCase).Alive == false)
                    {
                        VoisinSud(newCell).Alive = true;
                        VoisinSud(newCell).Age = 0;
                        VoisinSud(newCell).Energie = ENERGIE_INITIALE;
                    }
                    if (VoisinNordEst(maCase).Alive == false)
                    {
                        VoisinNordEst(newCell).Alive = true;
                        VoisinNordEst(newCell).Age = 0;
                        VoisinNordEst(newCell).Energie = ENERGIE_INITIALE;
                    }
                    if (VoisinNord(maCase).Alive == false)
                    {
                        VoisinNord(newCell).Alive = true;
                        VoisinNord(newCell).Age = 0;
                        VoisinNord(newCell).Energie = ENERGIE_INITIALE;
                    }
                    if (VoisinEst(maCase).Alive == false)
                    {
                        VoisinEst(newCell).Alive = true;
                        VoisinEst(newCell).Age = 0;
                        VoisinEst(newCell).Energie = ENERGIE_INITIALE;
                    }
                    if (VoisinNordOuest(maCase).Alive == false)
                    {
                        VoisinNordOuest(newCell).Alive = true;
                        VoisinNordOuest(newCell).Age = 0;
                        VoisinNordOuest(newCell).Energie = ENERGIE_INITIALE;
                    }
                    if (VoisinSudEst(maCase).Alive == false)
                    {
                        VoisinSudEst(newCell).Alive = true;
                        VoisinSudEst(newCell).Age = 0;
                        VoisinSudEst(newCell).Energie = ENERGIE_INITIALE;
                    }
                    newCell.Energie -= ENERGIE_REPRODUCTION; // perte d'énergie après reproduction
                }
            }
            return newCell;
        }
        public void Clone(Grille grillec) // copie de la taille et la matrice de la grille entrée en paramètre dans la grille avec laquelle la fonction est appelée (exemple : grilleTest.Clone(grillec))
        {
            this.taille = grillec.Taille;
            this.board = grillec.Board;


        }
    }
}
