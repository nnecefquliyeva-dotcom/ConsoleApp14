
using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Metn faylinin yolunu daxil edin: ");
            string yol = Console.ReadLine();

            if (!File.Exists(yol))
            {
                Console.WriteLine("Xeta: Fayl movcud deyil!");
                return;
            }

            string metn = File.ReadAllText(yol);

            string[] setirler = File.ReadAllLines(yol);
            int setirSayi = setirler.Length;

            string[] sozler = metn.Split(
                new char[] { ' ', '\n', '\r', '\t', '.', ',', '!', '?', ';', ':' },
                StringSplitOptions.RemoveEmptyEntries
            );

            int sozSayi = sozler.Length;

            int simvolSayi = metn.Length;

            string enUzunSoz = "";

            Dictionary<string, int> sozlerL = new Dictionary<string, int>();

            foreach (string soz in sozler)
            {
                string temizSoz = soz.ToLower();

                if (temizSoz.Length > enUzunSoz.Length)
                {
                    enUzunSoz = temizSoz;
                }

                if (sozlerL.ContainsKey(temizSoz))
                {
                    sozlerL[temizSoz]++;
                }
                else
                {
                    sozlerL.Add(temizSoz, 1);
                }
            }

            var siralanmis = new List<KeyValuePair<string, int>>(sozlerL);

            siralanmis.Sort((a, b) => b.Value.CompareTo(a.Value));

            using (StreamWriter sw = new StreamWriter("hesabat.txt"))
            {
                sw.WriteLine("--- METN ANALIZI ---");

                sw.WriteLine("Setir sayi: " + setirSayi);
                sw.WriteLine("Soz sayi: " + sozSayi);
                sw.WriteLine("Simvol sayi: " + simvolSayi);
                sw.WriteLine("En uzun soz: " + enUzunSoz);

                sw.WriteLine("\nEn cox tekrarlanan 3 soz:");

                for (int i = 0; i < 3 && i < siralanmis.Count; i++)
                {
                    sw.WriteLine(
                        siralanmis[i].Key + " - " +
                        siralanmis[i].Value + " defe"
                    );
                }
            }

            Console.WriteLine("Analiz tamamlandi!");
            Console.WriteLine("Neticeler hesabat.txt faylina yazildi.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Xeta bas verdi: " + ex.Message);
        }
    }
}
