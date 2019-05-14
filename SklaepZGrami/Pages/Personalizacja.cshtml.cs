using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SklaepZGrami.Pages
{
    public class PersonalizacjaModel : PageModel
    {
        public String[] Opis, Nazwa, login1, haslo1;
        public float[] cena;
        public int[] obrazy;


        public int prod_i, klienci_i;

        DataSet data;
        public string login;
        public bool loged = false;
        public string[] pic;
        public string[] pic1;
        public int[] iGraczy;
        public string[] iG4;
        public string[] iG2;
        public string koszyk = "";
        public string per = "";

        public int produkt = 0;

        public Operacje operacje;
        public void OnGet()
        {
            setUp();
            if (login == "*" || login == "")
                loged = true;


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
            iGraczy = operacje.iGraczy;
            prod_i = operacje.prod_i;
            klienci_i = operacje.klienci_i;
            login = operacje.login;
            pic = operacje.pic;
            koszyk = operacje.koszyk;
            per = operacje.per;
            iG2 = operacje.iG2;
            iG4 = operacje.iG4;
            produkt = operacje.produkt;
        }
        public IActionResult OnPostCol(string kol, string par, string login, string koszykk, string person)
        {
            setUp();
            return RedirectToPage("Index", operacje.redirect(par, login, koszykk, person));
        }
    }
}