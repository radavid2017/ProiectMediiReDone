using GameLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Proiect_MediiDeProgramare
{
    class Program
    {
        private static List<Monster> CitireMonstrii()
        {
            string[] lines = null;
            while (lines == null)
            {
                try
                {
                    lines = File.ReadAllLines("C:/Users/ACASA/Desktop/ProiectMediiReDone/monstrii.txt");
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine("Fisierul nu exista\n");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.GetType().Name);
                    Console.WriteLine("Press any key to try again");
                    Console.ReadKey();
                }
            }

            return lines
                .ToList()
                .Select(x => ParseMonster(x))
                .Where(x => x != null)
                .ToList();
        }

        private static Monster ParseMonster(string line)
        {
            Monster monster = null;

            try
            {
                var values = line.Split(',');
                var monsterName = values[0];
                var monsterDmg = int.Parse(values[1]);
                var monsterDeff = int.Parse(values[2]);

                monster = new Monster(monsterName, monsterDmg, monsterDeff);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Could not parse monsterDmg and/or monsterDeff\n");
            }
            return monster;
        }

        private static List<Player> CitireJucatori()
        {
            string[] lines = null;
            while (lines == null)
            {
                try
                {
                    lines = File.ReadAllLines("C:/Users/ACASA/Desktop/ProiectMediiReDone/jucatori.txt");
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine("Fisierul nu exista 'jucatori.txt'\n");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.GetType().Name);
                    Console.WriteLine("Press any key to try again");
                    Console.ReadKey();
                }
            }

            return lines
                .ToList()
                .Select(x => ParsePlayer(x))
                .Where(x => x != null)
                .ToList();
        }

        private static Player ParsePlayer(string line)
        {
            Player player = null;

            try
            {
                var values = line.Split(',');
                var playerName = values[0];
                var weaponName = values[1];
                var weaponDmg = int.Parse(values[2]);
                var shieldName = values[3];
                var shieldDeff = int.Parse(values[4]);

                player = new Player(playerName, new Weapon(weaponName, weaponDmg), new Shield(shieldName, shieldDeff));
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Could not parse weaponDmg and/or shieldDeff\n");
            }
            return player;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var jucatori = CitireJucatori();
            var monstrii = CitireMonstrii();

            Console.WriteLine("\nLista jucatori:\n");
            jucatori.ForEach(x => Console.WriteLine(x));
            Console.WriteLine("\nLista monstrii:\n");
            monstrii.ForEach(x => Console.WriteLine(x));

            Console.Write("Arata jucatorii care il pot invinge pe: ");
            string numeMonstru = Console.ReadLine();
            Monster strongMonster = null;
            foreach (Monster monster in monstrii)
            {
                if (monster.GetName() == numeMonstru)
                {
                    strongMonster = monster;
                    break;
                }
            }
            Console.WriteLine("\nMonstrul -> " + strongMonster + "\nJucatorii de tip Last-Man-Standing in lupta vs " + strongMonster.GetName() + ":");
            jucatori
                .Where(x => x.GetDefenseValue() / strongMonster.getAttack() > strongMonster.getDefense() / x.GetAttackValue())
                .ToList()
                .ForEach(x => Console.WriteLine(x));

            Console.WriteLine("Monstrii dupa rezistenta:\n");
            Console.Write("Afiseaza (apasati orice tasta)");
            Console.ReadKey();
            monstrii
                .OrderByDescending(x => x.getDefense())
                .ToList()
                .ForEach(x => Console.WriteLine(x));
        }
    }
}
