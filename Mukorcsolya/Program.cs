using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mukorcsolya
{
    class Program
    {
        public struct kori
        {
            public string nev;
            public string oKod;
            public double pontszam;
            public double kPontszam;
            public int hibaPont;
            //utolsó feladathoz
            public double osszPont;
        }
        static List<kori> rovidProgList = new List<kori>();
        static List<kori> dontoList = new List<kori>();
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("rovidprogram.csv", FileMode.Open);//Ez az első fájl a kettőből
            StreamReader sr = new StreamReader(fs);
            string sor = sr.ReadLine();
            while (!sr.EndOfStream)
            {
                sor = sr.ReadLine();
                string[] d = sor.Split(';');
                kori k = new kori();
                k.nev = d[0];
                k.oKod = d[1];
                k.pontszam = Convert.ToDouble(d[2].Replace('.',','));
                k.pontszam = Convert.ToDouble(d[3].Replace('.',','));
                k.hibaPont = Convert.ToInt32(d[4]);
                rovidProgList.Add(k);

            }
            sr.Close();
            fs.Close();

            FileStream fs2 = new FileStream("donto.csv", FileMode.Open);//Ez az első fájl a kettőből
            StreamReader sr2 = new StreamReader(fs2);
            string sor2 = sr2.ReadLine();
            while (!sr2.EndOfStream)
            {
                sor2 = sr2.ReadLine();
                string[] d = sor2.Split(';');
                kori k = new kori();
                k.nev = d[0];
                k.oKod = d[1];
                k.pontszam = Convert.ToDouble(d[2].Replace('.', ','));
                k.pontszam = Convert.ToDouble(d[3].Replace('.', ','));
                k.hibaPont = Convert.ToInt32(d[4]);
                dontoList.Add(k);

            }
            sr.Close();
            fs.Close();

            Console.WriteLine("2. feladat");
            Console.WriteLine("A rövidprogramban indult versenyzők száma: " + rovidProgList.Count);

            Console.WriteLine("3. feladat");

            bool megvan = false;
            int j = 0;
            while (j < dontoList.Count && !megvan)
            {
                if(dontoList[j].oKod == "HUN")
                {
                    megvan = true;
                }
                {
                    j++;
                }
            }
            if (megvan)
            {
                Console.WriteLine("Bejutott magyar a kürbe!");
            }
            else
            {
                Console.WriteLine("Nem jutott be magyar a kürbe!");
            }

            Console.WriteLine("4. Feladat");
            string nev = "Ivett TOTH";
            double osszPontszam =Osszpontszam(nev);
            Console.WriteLine(nev + ": " + osszPontszam);

            Console.WriteLine("5. Feladat:");
            Console.WriteLine("Add meg a versenyző nevét: ");
            string bekertNev = Console.ReadLine();
            bool van_e = false;
            int l = 0;
            while (l < rovidProgList.Count && !van_e)
            {
                if (rovidProgList[l].nev == bekertNev)
                {
                    van_e = true;
                }
                else
                {
                    l++;
                }
            }
            if (van_e == false)
            {
                Console.WriteLine("Ilyen nevű induló nem volt!");

            }

            Console.WriteLine("6. Feladat");
            double bekertOsszpontszam = Osszpontszam(bekertNev);
            Console.WriteLine(bekertNev + ": " + bekertOsszpontszam);
            /*
            Console.WriteLine("5. feladat");
            Console.Write("Kérek egy neve: ");
            string nev = Console.ReadLine();
            e
            Console.WriteLine("6. feladat");
            double osszPont = Osszpontszam(nev);
            if (osszPont == 0)
            {
                Console.WriteLine("Nem volt ilyen versenyző");

            }
            else
            {
                Console.WriteLine("Az összpontszáma: " + osszPont);
            }
            */
            Console.WriteLine("7. Feladat");
            List<string> bejutottOrszagok = new List<string>();
            foreach (kori versenyzo in dontoList)
            {
                if (!bejutottOrszagok.Contains(versenyzo.oKod))
                {
                    bejutottOrszagok.Add(versenyzo.oKod);
                }

            }
            foreach (string orszag in bejutottOrszagok)
            {
                int db = 0;
                foreach (kori versenyzo  in dontoList)
                {
                    if (versenyzo.oKod == orszag)
                    {
                        db++;
                    }
                    

                    
                }
                if (db > 1)
                {
                    Console.WriteLine(orszag + ": " + db);

                }
            }

            Console.WriteLine("8. Feladat");
            FileStream fs3 = new FileStream("vegeredmeny.csv", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs3);
            for (int i = 0; i < dontoList.Count; i++)
            {
                kori newkori = dontoList[i];
                newkori.osszPont = Osszpontszam(dontoList[i].nev);
                dontoList[i] = newkori;
            }
            //rendezzük összpont szerinta listát
            dontoList = dontoList.OrderBy(versenyzo => versenyzo.osszPont).ToList();
            dontoList.Reverse();

            //írjuk ki fájlba
            int helyezes = 1;
            foreach (kori versenyzo in dontoList)
            {
                sw.WriteLine(helyezes + ". " + versenyzo.nev + "; " + versenyzo.oKod
                    + "; " + versenyzo.osszPont);
                helyezes++;
            }
            sw.Close();
            fs3.Close();
              
            Console.ReadLine();

        }


        static double Osszpontszam(string nev)
        {
            double osszPont = 0;
            foreach (kori versenyzo in rovidProgList)
            {
                if (versenyzo.nev == nev)
                {
                    osszPont += versenyzo.pontszam + versenyzo.kPontszam - versenyzo.hibaPont;

                }
            }
            foreach (kori versenyzo in dontoList)
            {
                if (versenyzo.nev == nev)
                {
                    osszPont += versenyzo.pontszam + versenyzo.kPontszam - versenyzo.hibaPont;

                }
            }

            return osszPont;
        }
    }
}
