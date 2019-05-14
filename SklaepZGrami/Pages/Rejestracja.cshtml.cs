using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SklaepZGrami.Pages
{
    public class RejestracjaModel : PageModel
    {
        DataSet data;
        int klienci_i;
        String[] login1, haslo1;

        public Operacje operacje;


        public void OnGet()
        {
            operacje = new Operacje(Request);
            login1 = operacje.login1;
            haslo1 = operacje.haslo1;

        }
        public IActionResult OnPostSave(string ap_imie, string ap_nazwisko, string ap_nick, string ap_haslo, string ap_haslo1, string ap_miasto, string ap_ulica, string ap_mieszkanie, bool ap_check)
        {
            if (ap_haslo == ap_haslo1)
            {
                data = new DataSet();
                data.ReadXml("sklep.xml");

                DataRow dr = data.Tables["KLIENCI"].NewRow();
                dr["Imie"] = ap_imie;
                dr["Nazwisko"] = ap_nazwisko;
                dr["Login"] = ap_nick;
                dr["Haslo"] = ap_haslo;
                dr["Miasto"] = ap_miasto;
                dr["Ulica"] = ap_ulica;
                dr["Mieszkanie"] = ap_mieszkanie;
                data.Tables["KLIENCI"].Rows.Add(dr);
                data.WriteXml("sklep.xml");
                return RedirectToPage("Index", ap_nick);
            }
            return RedirectToPage("Rejestracja");
        }
        public IActionResult OnPostLog(string Login, string Password)
        {
            login1 = operacje.login1;
            haslo1 = operacje.haslo1;
            for (int i = 0; i < klienci_i; i++)
            {
                if ((login1[i] == Login) && haslo1[i] == Password)
                {

                }
            }
            return RedirectToPage("Index");
        }
    }
}