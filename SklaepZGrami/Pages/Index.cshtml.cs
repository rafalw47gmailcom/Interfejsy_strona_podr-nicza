using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.Odbc;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace SklaepZGrami.Pages
{
    public class IndexModel : PageModel
    {
        public String[] Opis, Nazwa, login1, haslo1, rodzaj;
        public float[] cena;
        public int[] obrazy, iGraczy;

        public int rodzaj1 = 0, iGraczy1 = 0, cena1 = 0;

        public int prod_i, klienci_i;

        public Operacje operacje;

        public string login;
        public bool loged = false;
        public string[] pic;
        public string koszyk = "";
        public string per = "";

        void setUp()
        {
            operacje = new Operacje(Request);
            Opis = operacje.Opis;
            Nazwa = operacje.Nazwa;
            login1 = operacje.login1;
            haslo1 = operacje.haslo1;
            rodzaj = operacje.rodzaj;
            cena = operacje.cena;
            obrazy = operacje.obrazy;
            iGraczy = operacje.iGraczy;
            rodzaj1 = operacje.rodzaj1;
            iGraczy1 = operacje.iGraczy1;
            cena1 = operacje.cena1;
            prod_i = operacje.prod_i;
            klienci_i = operacje.klienci_i;
            login = operacje.login;
            pic = operacje.pic;
            koszyk = operacje.koszyk;
            per = operacje.per;
        }

        public void OnGet()
        {
            setUp();
            if (login == "*" || login == "" || login == null)
                loged = true;
        }
       
        
        
        public IActionResult OnPost(string login, string haslo)
        {
            setUp();
            for (int i = 0; i < klienci_i; i++)
            {
                haslo1[i] = operacje.deleteSpace(haslo1[i]);
                login1[i] = operacje.deleteSpace(login1[i]);
                if ((login1[i] == login) && (haslo1[i] == haslo))
                {
                    return RedirectToPage("Index", "l." + login1[i] + ",k." + koszyk + ",p." + per + ",");
                }
            }
            return RedirectToPage("Index", "InvalidLogin");
            // var cbox = Request.Form["cbox"];
        }
        public IActionResult OnPostSell(string par, string login, string koszykk, string person)
        {
            setUp();
            return RedirectToPage("Index", operacje.redirect(par, login, koszykk, person));
        }
        public IActionResult OnPostFil(string par, string login, string koszykk, string person, string r1, string c1, string i1)
        {
            int j1 = int.Parse(r1), i2 = int.Parse(c1), i3 = int.Parse(i1);

            if (int.Parse(par) == 1)
                j1 = 1;
            if (int.Parse(par) == 2)
                i2 = 1;
            if (int.Parse(par) == 3)
                i2 = 2;
            if (int.Parse(par) == 4)
                i2 = 3;
            if (int.Parse(par) == 5)
                i3 = 1;
            if (int.Parse(par) == 6)
                i3 = 2;
            return RedirectToPage("Index", "l." + login + ",k." + koszykk + ",p." + person + "," + j1 + i2 + i3 + "f");
        }
        public IActionResult OnPostPro(string par, string login, string koszykk, string person)
        {
            return RedirectToPage("Produkt", "l." + login + ",k." + koszykk + ",p." + person + "," + par);
        }
        public IActionResult OnPostKosz(string par, string loginn, string koszykk, string person)
        {
            return RedirectToPage("Koszyk", "l." + loginn + ",k." + koszykk + ",p." + person + ",");
        }
    }
}
