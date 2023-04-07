using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilitaires
{
    public static class SubstringExtensions
    {
        /// <summary>
        /// Get string value between [first] a and [last] b.
        /// </summary>
        public static string Between(this string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }

        /// <summary>
        /// Get string value after [first] a.
        /// </summary>
        public static string Before(this string value, string a)
        {
            int posA = value.IndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            return value.Substring(0, posA);
        }

        /// <summary>
        /// Get string value after [last] a.
        /// </summary>
        public static string After(this string value, string a)
        {
            int posA = value.LastIndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= value.Length)
            {
                return "";
            }
            return value.Substring(adjustedPosA);
        }

        /// <summary>
        /// Renvoi la succession des chiffres 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Numerique(this string s, int k)
        {
            string Result = String.Empty;
            string w = String.Empty;
            string Numbers = "0123456789";

            for (int i = 0; i < s.Length; i++)
            {
                if (Numbers.Contains(s.ElementAt(i)))
                    w = w + s[i];
                else
                {
                    if (w.Length > k)
                        break;
                    else
                    {
                        w = String.Empty;
                    }

                }
            }
            return w;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="L1"></param>
        /// <param name="L2"></param>
        /// <returns></returns>
        public static List<string> Merges(this string str1, string str2)
        {
            var str11 = str1.Split(' ');
            var str22 = str2.Split(' ');

            List<string> L1 = new List<string>();
            List<string> L2 = new List<string>();
            List<string> Result = new List<string>();

            foreach(var el in str11)
            {
                L1.Add(el);
            }

            foreach (var el in str22)
            {
                L2.Add(el);
            }

            foreach (var s2 in L2)
            {
                var s = str1 + " " + s2;
                Result.Add(s);
            }


            foreach (var s1 in L1)
            {
                foreach(var s2 in L2)
                {
                    var s = s1 + " " + s2;
                    Result.Add(s);
                }
            }



            return Result;
        }




        /// <summary>
        /// Permet de decouper un champ en n mots
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> Decoupage(this string str)
        {
            List<string> searchTerm = new List<string>();
            var lst = str.Split(' ');
            foreach(var l in lst)
            {
                searchTerm.Add(l);
            }
            return searchTerm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        public static bool ContainsAny(this string str, IEnumerable<string> searchTerms)
        {
            return searchTerms.Any(searchTerm => str.ToLower().Contains(searchTerm.ToLower()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        public static bool ContainsAll(this string str, IEnumerable<string> searchTerms)
        {
            return searchTerms.All(searchTerm => str.ToLower().Contains(searchTerm.ToLower()));
        }
    }
}
