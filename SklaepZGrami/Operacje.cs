using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SklaepZGrami
{

    public class Operacje
    {


        public String[] Opis, Nazwa, login1, haslo1, imie, nazwisko, miasto, ulica, nr, rodzaj;
        public float[] cena;
        public int[] obrazy, iGraczy;
        public string[] pic, pic1;

        public int rodzaj1 = 0, iGraczy1 = 0, cena1 = 0;

        DataSet data;

        public int prod_i, klienci_i;

        public string koszyk = "";
        public string per = "";

        public string login;

        public string[] iG4;
        public string[] iG2;

        public int produkt;

        public int wysylka;



        public int[] koszykInt;
        public int[] koszykIlosc;
        public int KoszykIle;

        void getKoszyk()
        {
            char[] koszykchar = koszyk.ToCharArray();
            int[] koszykChar = new int[koszykchar.Length];
            for (int i = 1; i < koszykChar.Length; i++)
            {
                char[] letter = new char[1];
                letter[0] = koszykchar[i];
                string lettar = new string(letter);
                koszykChar[i] = int.Parse(lettar);
            }
            int repos;
            bool repat = true;
            while (repat)
            {
                repat = false;
                for (int i = 1; i < koszykChar.Length; i++)
                {
                    if (koszykChar[i - 1] > koszykChar[i])
                    {
                        repat = true;
                        repos = koszykChar[i];
                        koszykChar[i] = koszykChar[i - 1];
                        koszykChar[i - 1] = repos;
                    }
                }
            }
            for (int i = 1; i < koszykChar.Length; i++)
            {
                string str = koszykChar[i].ToString();
                char[] cr = str.ToCharArray();
                koszykchar[i] = cr[0];

            }
            koszyk = new string(koszykchar);
            countKoszyk();
        }
        public string redirect(string par, string login, string koszykk, string person)
        {
            char[] c;
            char[] c1;
            char[] p;
            char[] p1;
            if (koszykk != null)
            {
                c = koszykk.ToCharArray();
                c1 = new char[koszykk.Length + 1];
                p = person.ToCharArray();
                p1 = new char[person.Length + 1];
            }
            else
            {
                c = new char[1];
                c1 = new char[1];
                p = new char[1];
                p1 = new char[1];
            }
            for (int i = 0; i < c1.Length; i++)
            {
                if (i < c1.Length - 1)
                {
                    c1[i] = c[i];
                    p1[i] = p[i];
                }
                else
                {
                    string str = par, str1 = "0";
                    char[] c2 = str.ToCharArray();
                    char[] p2 = str1.ToCharArray();
                    c1[i] = c2[0];
                    p1[i] = p2[0];
                }
            }
            return  "l." + login + ",k." + new string(c1) + ",p." + new string(p1) + ",";
        }
        void countKoszyk()
        {
            char[] charArray = koszyk.ToCharArray();
            int[] koszykChar = new int[charArray.Length];
            for (int i = 1; i < koszykChar.Length; i++)
            {
                char[] letter = new char[1];
                letter[0] = charArray[i];
                string lettar = new string(letter);
                koszykChar[i] = int.Parse(lettar);
            }


            bool first = true;
            KoszykIle = 1;
            for (int i = 1; i < koszykChar.Length; i++)
            {
                if (koszykChar[i] != koszykChar[i - 1])
                    KoszykIle++;
            }
            koszykInt = new int[KoszykIle];
            koszykIlosc = new int[KoszykIle];
            KoszykIle = 1;
            for (int i = 0; i < koszykIlosc.Length; i++)
            {
                koszykIlosc[i] = 1;
            }
            for (int i = 1; i < koszykChar.Length; i++)
            {
                if (koszykChar[i] != koszykChar[i - 1])
                {
                    koszykInt[KoszykIle] = koszykChar[i];
                    KoszykIle++;
                }
                else
                {
                    koszykIlosc[KoszykIle - 1]++;
                }
            }
        }
        public Operacje(HttpRequest request)
        {
            get_offer();
            getLogin(string.Concat(
                        request.Scheme,
                        "://",
                        request.Host.ToUriComponent(),
                        request.PathBase.ToUriComponent(),
                        request.Path.ToUriComponent(),
                        request.QueryString.ToUriComponent()));
            getKoszyk();
        }
        protected void get_offer()
        {
            data = new DataSet();

            data.ReadXml("sklep.xml");

            prod_i = data.Tables["PRODUKTY"].Rows.Count;
            klienci_i = data.Tables["KLIENCI"].Rows.Count;

            Opis = new string[prod_i];
            Nazwa = new string[prod_i];
            cena = new float[prod_i];
            obrazy = new int[prod_i];
            pic = new string[prod_i];
            pic1 = new string[prod_i];
            iGraczy = new int[prod_i];
            rodzaj = new string[prod_i];
            iG4 = new string[5];
            iG2 = new string[5];

            login1 = new string[klienci_i];
            haslo1 = new string[klienci_i];
            imie = new string[klienci_i];
            nazwisko = new string[klienci_i];
            miasto = new string[klienci_i];
            ulica = new string[klienci_i];
            nr = new string[klienci_i];

            for (int i = 0; i < prod_i; i++)
            {
                Opis[i] = data.Tables["PRODUKTY"].Rows[i]["Opis"].ToString();
                Nazwa[i] = data.Tables["PRODUKTY"].Rows[i]["Nazwa"].ToString();
                rodzaj[i] = data.Tables["PRODUKTY"].Rows[i]["Rodzaj"].ToString();

                Nazwa[i] = deleteSpace(Nazwa[i]);

                cena[i] = float.Parse(data.Tables["PRODUKTY"].Rows[i]["Cena"].ToString()) / 100;
                obrazy[i] = int.Parse(data.Tables["PRODUKTY"].Rows[i]["Obrazy"].ToString());
                iGraczy[i] = int.Parse(data.Tables["PRODUKTY"].Rows[i]["IGraczy"].ToString());
                pic[i] = "img/" + obrazy[i].ToString() + ".png";
                pic1[i] = "img/" + obrazy[i].ToString() + "_1.png";
            }

            for (int i = 0; i < klienci_i; i++)
            {
                login1[i] = data.Tables["KLIENCI"].Rows[i]["Login"].ToString();
                haslo1[i] = data.Tables["KLIENCI"].Rows[i]["Haslo"].ToString();
                imie[i] = data.Tables["KLIENCI"].Rows[i]["Imie"].ToString();
                nazwisko[i] = data.Tables["KLIENCI"].Rows[i]["Nazwisko"].ToString();
                miasto[i] = data.Tables["KLIENCI"].Rows[i]["Miasto"].ToString();
                ulica[i] = data.Tables["KLIENCI"].Rows[i]["Ulica"].ToString();
                nr[i] = data.Tables["KLIENCI"].Rows[i]["Mieszkanie"].ToString();
            }
            for (int i = 0; i < 5; i++)
            {
                iG4[i] = "img/4w" + i + ".png";
                iG2[i] = "img/2w" + i + ".png";
            }
        }
        public string deleteSpace(string str)
        {
            string str1 = str;
            char empty = ' ';
            str1 = str1.Replace(empty, '*');
            char[] c = str1.ToCharArray();
            char[] c1;
            int j = 0;
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] != '*')
                {
                    j++;
                }
            }
            c1 = new char[j];
            for (int i = 0; i < c1.Length; i++)
            {
                c1[i] = c[i];
            }
            return new string(c1);
        }
        public void getLogin(String url)
        {
            bool bol = false;
            String absoluteUri = url;
            char[] chars = absoluteUri.ToCharArray();
            char[] c = new char[] { '*' };
            int count = 0;

            if (chars[chars.Length - 1] == 'f')
            {
                char[] cr = new char[1];
                cr[0] = chars[chars.Length - 2];
                iGraczy1 = int.Parse(new string(cr));
                cr[0] = chars[chars.Length - 3];
                cena1 = int.Parse(new string(cr));
                cr[0] = chars[chars.Length - 4];
                rodzaj1 = int.Parse(new string(cr));

            }

            for (int i = 0; i < chars.Length - 8; i++)
            {
                if ((chars[i] == 'h') && (chars[i + 1] == 'a') && (chars[i + 2] == 'n') && (chars[i + 3] == 'd') && (chars[i + 4] == 'l') && (chars[i + 5] == 'e') && (chars[i + 6] == 'r') && (chars[i + 7] == '='))
                {
                    count = chars.Length - (i + 8);
                    c = new char[count];
                    bol = true;
                    for (int j = 0; j < count; j++)
                    {
                        c[j] = chars[j + i + 8];
                    }
                }
            }
            bool loginStart = false, koszykStart = false, perStart = false;
            int loginCount = 0, koszykCount = 0, perCount = 0;
            int logStart = 0, koszStart = 0, pStart = 0;
            bool koszyyk = false, person = false;

            char[] cahr = new char[1];
            cahr[0] = chars[chars.Length - 1];
            string srt = new string(cahr);
            try
            {
                produkt = int.Parse(srt);
            }
            catch
            {

            }

            for (int i = 2; i < c.Length; i++)
            {
                if (loginStart)
                {
                    loginCount++;
                }
                if ((c[i - 2] == 'l') && (c[i - 1] == '.'))
                {
                    loginStart = true;
                    logStart = i;
                }
                if (c[i] == ',')
                {
                    loginStart = false;
                    koszyyk = true;
                }

            }
            for (int i = 2; i < c.Length; i++)
            {
                if (koszykStart)
                {
                    koszykCount++;
                }
                if ((c[i - 2] == 'k') && (c[i - 1] == '.'))
                {
                    koszykStart = true;
                    koszStart = i;
                }
                if (c[i] == ',' && koszyyk)
                {
                    koszykStart = false;
                    person = true;
                }
            }
            for (int i = 2; i < c.Length; i++)
            {
                if (perStart)
                {
                    perCount++;
                }
                if ((c[i - 2] == 'p') && (c[i - 1] == '.'))
                {
                    perStart = true;
                    pStart = i;
                }
                if (c[i] == ',' && person)
                {
                    perStart = false;
                }
            }
            char[] newLogin = new char[] { '*' };
            char[] newKoszyk = new char[] { '*' };
            char[] newPerson = new char[] { '*' };
            if (bol)
            {
                newLogin = new char[loginCount];
                newKoszyk = new char[koszykCount];
                newPerson = new char[koszykCount];

                for (int i = 0; i < newLogin.Length; i++)
                {
                    newLogin[i] = c[i + logStart];
                    //    if (i == newLogin.Length - 2)
                    //        newLogin[i + 1] = '*';
                }
                for (int i = 0; i < newKoszyk.Length; i++)
                {
                    newKoszyk[i] = c[i + koszStart];
                    newPerson[i] = c[i + pStart];
                }
                //  newLogin = newLogin.Except(new char[] { '*' }).ToArray();
                //  newKoszyk = newKoszyk.Except(new char[] { '*' }).ToArray();
                koszyk = new string(newKoszyk);
                per = new string(newPerson);
                login = new string(newLogin);
                if (chars[chars.Length - 1] == '1')
                    wysylka = 1;
                if (chars[chars.Length - 1] == '2')
                    wysylka = 2;
                if (login == "")
                    login = "*";
            }

          // login = new string(c);
        }
    }
}
