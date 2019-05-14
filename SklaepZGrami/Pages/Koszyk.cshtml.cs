using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SklaepZGrami.Pages
{
    public class KoszykModel : PageModel
    {
        public String[] Opis, Nazwa, login1, haslo1;
        public float[] cena;
        public int[] obrazy;


        public int prod_i, klienci_i;

        string connectionString = "Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=C:/Users/Gonzowski/source/repos/SklaepZGrami/SklaepZGrami/bin/Debug/netcoreapp2.1/sklep";
        DataSet data;
        public string login;
        public bool loged = false;
        public string[] pic;
        public string koszyk = "";
        public int[] koszykInt;
        public int[] koszykIlosc;
        public int KoszykIle;
        public string per = "";

        public float[] razem;
        public float lacznie;
        public int spers = 0;
        public int spersC = 0;
        public Operacje operacje;

        public void OnGet()
        {
            operacje = new Operacje(Request);

            setUp();
            if (login == "*" || login == "")
                loged = true;
            razem = new float[KoszykIle];
            for (int j = 0; j < KoszykIle; j++)
            {
                razem[j] = cena[koszykInt[j]] * koszykIlosc[j];
                lacznie += razem[j];
            }
            char[] chars = per.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] != '0')
                    spers++;
            }
            spersC = spers * 5;

            lacznie += spersC;
        }
        void setUp()
        {
            operacje = new Operacje(Request);
            Opis = operacje.Opis;
            Nazwa = operacje.Nazwa;
            login1 = operacje.login1;
            haslo1 = operacje.haslo1;

            cena = operacje.cena;
            obrazy = operacje.obrazy;

            prod_i = operacje.prod_i;
            klienci_i = operacje.klienci_i;
            login = operacje.login;
            pic = operacje.pic;
            koszyk = operacje.koszyk;
            per = operacje.per;

            koszykInt = operacje.koszykInt;
            koszykIlosc = operacje.koszykIlosc;
            KoszykIle = operacje.KoszykIle;
        }
      
       
        public IActionResult OnPostAdd(string par, string login, string koszykk, string person)
        {
            setUp();
            return RedirectToPage("Koszyk", operacje.redirect(par, login, koszykk, person));
        }
        public IActionResult OnPostSub(string par, string login, string koszykk, string person)
        {
            char[] c;
            char[] c1;
            char[] p;
            c = koszykk.ToCharArray();
            p = person.ToCharArray();
            c1 = par.ToCharArray();
            bool start = false;
            for (int i = 1; i < c.Length; i++)
            {
                if (c[i - 1] == c1[0])
                    start = true;
                if (true)
                {
                    c[i - 1] = c[i];
                    p[i - 1] = p[i];
                }
            }
            char[] c2 = new char[c.Length - 1];
            char[] p2 = new char[c.Length - 1];
            for (int i = 0; i < c2.Length; i++)
            {
                c2[i] = c[i];
                p2[i] = p[i];
            }
            return RedirectToPage("Koszyk", "l." + login + ",k." + new string(c2) + ",p." + new string(p2) + ",");
        }
        public IActionResult OnPostNext(string login, string koszykk, string person)
        {
            return RedirectToPage("Wysylka", "l." + login + ",k." + koszykk + ",p." + person + ",");
        }
      
        }
}