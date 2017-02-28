using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConwaysGameOfLife
{
    class Cellule
    {
        //déclaration des champs 

        bool alive;
        int linePosition;
        int columnPosition;
        int age;
        int energie;

        //déclaration des constructeur

        public Cellule(bool alive, int linePosition, int columnPosition) // constructeur pour le niveau 1
        {
            this.alive = alive;
            this.linePosition = linePosition;
            this.columnPosition = columnPosition;
        }
        public Cellule(bool alive, int linePosition, int columnPosition, int age, int energie) // constructeur pour le niveau 2
        {
            this.alive = alive;
            this.linePosition = linePosition;
            this.columnPosition = columnPosition;
            this.age = age;
            this.energie = energie;
        }

        //déclaration des propriétés

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }
        public int LinePosition
        {
            get { return linePosition; }
            set { linePosition = value; }
        }
        public int ColumnPosition
        {
            get { return columnPosition; }
            set { columnPosition = value; }
        }
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public int Energie
        {
            get { return energie; }
            set { energie = value; }
        }

        //déclaraton des méthodes

        public void Clone(Cellule mycell) // copie les attributs de la cellule entrée en paramètre dans la cellule sur laquelle on applique la fonction. exemple : cell.Clone(mycell)
        {
            this.alive = mycell.Alive;
            this.linePosition = mycell.LinePosition;
            this.columnPosition = mycell.ColumnPosition;
            this.age = mycell.Age;
            this.energie = mycell.Energie;
        }
        
        public override string ToString()
        {
            string chain;
            chain = "La cellule est à la ligne " + linePosition + " et à la colonne " + columnPosition;
            if (alive == false) chain += " .La cellule est morte";
            if (alive == true) chain += " .La cellule est vivante";
            return chain;          
        }
        public void Affiche()
        {
            if (alive == true) Console.Write("V ");
            else
            {
                Console.Write("  ");
            }
        }
    }
}
