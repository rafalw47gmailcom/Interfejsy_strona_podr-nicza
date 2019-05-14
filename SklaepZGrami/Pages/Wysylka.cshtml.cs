using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SklaepZGrami.Pages
{
    public class WysylkaModel : PageModel
    {
        public String[] Opis, Nazwa, login1, haslo1, imie,nazwisko,miasto,ulica,nr;
        public float[] cena;
        public int[] obrazy;


        public int prod_i, klienci_i;

        DataSet data;
        public string login;
        public bool loged = false;
        public string[] pic;
        public string koszyk = "";
        public string per = "";
        public int[] koszykInt;
        public int[] koszykIlosc;
        public int KoszykIle;

        public int wysylka = 0;

        public Operacje operacje;

        public void OnGet()
        {
            operacje = new Operacje(Request);
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
            prod_i = operacje.prod_i;
            klienci_i = operacje.klienci_i;
            login = operacje.login;
            pic = operacje.pic;
            koszyk = operacje.koszyk;
            per = operacje.per;
            wysylka = operacje.wysylka;
            imie = operacje.imie;
            nazwisko = operacje.nazwisko;
            miasto = operacje.miasto;
            ulica = operacje.ulica;
            nr = operacje.nr;

            koszykInt = operacje.koszykInt;
            koszykIlosc = operacje.koszykIlosc;
            KoszykIle = operacje.KoszykIle;
        }
      
       
      
        public IActionResult OnPostLog(string login,string haslo, string koszykk, string person, string wys)
        {
            setUp();
            for (int i = 0; i < klienci_i; i++)
            {
                if (login1[i] == login && haslo1[i] == haslo)
                {
                    data = new DataSet();
                    data.ReadXml("sklep.xml");
                    DataRow dr = data.Tables["ZAMOWIENIA"].NewRow();
                    dr["Imie"] = imie[i];
                    dr["Nazwisko"] = nazwisko[i];
                    dr["Adres"] = miasto[i] + " " + ulica[i] + " " + nr[i];
                    dr["Produkty"] = koszykk;
                    dr["Personalizacja"] = person;
                    if(wys == "1")
                        dr["Wysylka"] = "kurierska";
                    if (wys == "2")
                        dr["Wysylka"] = "zaPobraniem";
                    data.Tables["ZAMOWIENIA"].Rows.Add(dr);
                    data.WriteXml("sklep.xml");
                }
            }
            return RedirectToPage("Index");
        }
        public IActionResult OnPostSave(string ap_imie, string ap_nazwisko, string ap_miasto, string ap_ulica, string ap_mieszkanie, string koszykk, string person, string wys)
        {

            data = new DataSet();
            data.ReadXml("sklep.xml");
            DataRow dr = data.Tables["ZAMOWIENIA"].NewRow();
                    dr["Imie"] = ap_imie;
                    dr["Nazwisko"] = ap_nazwisko;
                    dr["Adres"] = ap_miasto + " " + ap_ulica + " " + ap_mieszkanie;
                    dr["Produkty"] = koszykk;
                    dr["Personalizacja"] = person;
                    if (wys == "1")
                        dr["Wysylka"] = "kurierska";
                    if (wys == "2")
                        dr["Wysylka"] = "zaPobraniem";
                    data.Tables["ZAMOWIENIA"].Rows.Add(dr);
                    data.WriteXml("sklep.xml");
            return RedirectToPage("Index");
        }
        public IActionResult OnPostNpob(string loginn, string koszykk, string person)
        {
            return RedirectToPage("Wysylka", "l." + loginn + ",k." + koszykk + ",p." + person + ",1");
        }
        public IActionResult OnPostPob(string loginn, string koszykk, string person)
        {
            return RedirectToPage("Wysylka", "l." + loginn + ",k." + koszykk + ",p." + person + ",2");
        }
        public IActionResult OnPostLogIn(string loginn, string koszykk, string person, string wys)
        {
            setUp();
            for (int i = 0; i < klienci_i; i++)
            {
                if (login1[i] == loginn)
                {
                    data = new DataSet();
                    data.ReadXml("sklep.xml");
                    DataRow dr = data.Tables["ZAMOWIENIA"].NewRow();
                    dr["Imie"] = imie[i];
                    dr["Nazwisko"] = nazwisko[i];
                    dr["Adres"] = miasto[i] + " " + ulica[i] + " " + nr[i];
                    dr["Produkty"] = koszykk;
                    dr["Personalizacja"] = person;
                    if (wys == "1")
                        dr["Wysylka"] = "kurierska";
                    if (wys == "2")
                        dr["Wysylka"] = "zaPobraniem";
                    data.Tables["ZAMOWIENIA"].Rows.Add(dr);
                    data.WriteXml("sklep.xml");
                }
            }
            return RedirectToPage("Index");
        }
    }
}