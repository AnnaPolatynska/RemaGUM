using System.IO;

namespace nsDocInDb
{
    /// <summary>
    /// Zapis na napęd i odczyt z napędu pliku zdjęcia - ma zastosowanie do wymiany plików w ramach bazy danych MS ACCESS.
    /// </summary>
    class docInDb
    {
        string _dirNazwa = string.Empty; // Pełna nazwa katalogu tymczasowego, do którego zapisywane są pliki z BD.
        string _zdjecieNazwa = string.Empty; // Nazwa zdjęcia.
        byte[] _zdjecieZawartosc = new byte[0] { }; // Zawartość pliku zdjęcia.

        /// <summary>
        /// Konstruktor klasy docInDb.
        /// </summary>
        public docInDb()
        {
        }// konstruktor docInDb.

        /// <summary>
        /// Pełna nazwa katalogu tymczasowego, do którego zapisywane są pliki.
        /// </summary>
        public string dirNazwa
        {
            get { return _dirNazwa; }
            set { _dirNazwa = value; }
        }

        /// <summary>
        /// Nazwa zdjęcia z rozszerzeniem.
        /// </summary>
        public string zdjecieNazwa
        {
            get { return _zdjecieNazwa; }
            set { _zdjecieNazwa = value; }
        }

        /// <summary>
        /// Zawartość zdjecia.
        /// </summary>
        public byte[] zdjecieZawartosc
        {
            get { return _zdjecieZawartosc; }
            set { _zdjecieZawartosc = value; }
        }

        /// <summary>
        /// Czyta z napędu i umieszcza w tablicy bajtów zawartość zdjęcia.
        /// </summary>
        /// <param name="sciezka">Ścieżka pliku na napędzie.</param>
        /// <returns>eśli udało sie poprawnie odczytać plik zwraca true.</returns>
        public bool czytajZNapedu(string sciezka)
        {
            try
            {
                if (File.Exists(sciezka))
                {
                    FileStream fs = new FileStream(sciezka, FileMode.OpenOrCreate, FileAccess.Read);
                    _zdjecieZawartosc = new byte[fs.Length];
                    fs.Read(_zdjecieZawartosc, 0, System.Convert.ToInt32(fs.Length));
                    fs.Close();
                    return true;
                }
            }
            catch
            {
                _zdjecieZawartosc = new byte[0] { };
            }
            return false;
        }// czytajZNapedu

        /// <summary>
        /// Zapisuje na napęd zawartość pliku zdjęcia (tablica bajtów). Uwaga jeśli istnieje przezd zapisem plik o wskazanej nazwie to jest on usuwany.
        /// </summary>
        /// <param name="zdjecieZawartosc"></param>
        /// <returns></returns>
        public bool zapiszNaNaped(byte[] zdjecieZawartosc)
        {
            try
            {
                string sciezka = _dirNazwa + "\\" + _zdjecieNazwa;
                int dlugosc = zdjecieZawartosc.Length;

                FileInfo fi = new FileInfo(sciezka);
                if (fi.Exists)
                {
                    File.Delete(sciezka);
                }
                FileStream fs = new FileStream(sciezka, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(zdjecieZawartosc, 0, dlugosc);
                fs.Close();

                fi = new FileInfo(sciezka);
                return fi.Exists;
            }
            catch
            { }
            return false;
        }//zapiszNaNaped

    }//class docInDb
}//namespace nsDocInDb