using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data.OleDb;

namespace nsAccess2DB
{
    class dbConnection
    {
        private OleDbDataAdapter _adapter;
        private OleDbConnection _conn;

        public string _error = string.Empty; //komunikat błedu
        /// <constructor>
        /// Połączenie z bazą.
        /// </constructor>
        public dbConnection(string connString)
        {
            _adapter = new OleDbDataAdapter();
            _conn = new OleDbConnection(connString);
        }//dbConnection
         /// <method>
         /// Otwiera połaczenie jeśli jest zamknięte lub przerwane.
         /// </method>
        private OleDbConnection openConnection()
        {
            if (_conn.State == ConnectionState.Closed || _conn.State ==
                        ConnectionState.Broken)
            {
                _conn.Open();
            }
            return _conn;
        }//SqlConnection

        /// <method>
        /// Polecenie wyboru.
        /// </method>
        public DataTable executeSelectQuery(String query, OleDbParameter[] parameters)
        {
            //SqlCommand comm = new SqlCommand();
            OleDbCommand comm = new OleDbCommand();

            //OleDbCommand
            DataTable dt = new DataTable();
            //dt.CaseSensitive = true;
            DataSet ds = new DataSet();
            //ds.CaseSensitive = true;
            try
            {
                comm.Connection = openConnection();
                comm.CommandText = query;
                comm.Parameters.AddRange(parameters);
                comm.ExecuteNonQuery();
                _adapter.SelectCommand = comm;

                _adapter.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (SqlException e)
            {
                _error = "Error - Connection.executeSelectQuery - Query: " + query + " \nException: " + e.StackTrace.ToString();
            }
            finally
            {
                _conn.Close();
            }
            return dt;
        }//executeSelectQuery

        /// <method>
        /// Polecenie dodania.
        /// </method>
        public bool executeInsertQuery(String query, OleDbParameter[] parameters)
        {
            //SqlCommand comm = new SqlCommand();
            OleDbCommand comm = new OleDbCommand();

            try
            {
                comm.Connection = openConnection();
                comm.CommandText = query;
                comm.Parameters.AddRange(parameters);
                _adapter.InsertCommand = comm;
                comm.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                _error = "Error - Connection.executeInsertQuery - Query: " + query + " \nException: \n" + e.StackTrace.ToString();
                return false;
            }
            finally
            {
                _conn.Close();
            }
            return true;
        }//executeInsertQuery

        /// <method>
        /// Polecenie usunięcia.
        /// </method>
        public bool executeDeleteQuery(String query, OleDbParameter[] parameters)
        {
            //SqlCommand comm = new SqlCommand();
            OleDbCommand comm = new OleDbCommand();
            try
            {
                comm.Connection = openConnection();
                comm.CommandText = query;
                comm.Parameters.AddRange(parameters);
                _adapter.DeleteCommand = comm;
                comm.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                _error = "Error - Connection.executeDeleteQuery - Query: " + query + " \nException: \n" + e.StackTrace.ToString();
                return false;
            }
            finally
            {
                _conn.Close();
            }
            return true;
        }//executeInsertQuery

        /// <method>
        /// Polecenie aktualizacji.
        /// </method>
        public bool executeUpdateQuery(String query, OleDbParameter[] parameters)
        {
            //SqlCommand comm = new SqlCommand();
            OleDbCommand comm = new OleDbCommand();

            try
            {
                comm.Connection = openConnection();
                comm.CommandText = query;
                comm.Parameters.AddRange(parameters);
                _adapter.UpdateCommand = comm;
                comm.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                _error = "Error - Connection.executeUpdateQuery - Query: " + query + " \nException: " + e.StackTrace.ToString();
                return false;
            }
            finally
            {
                _conn.Close();
            }
            return true;
        }//executeUpdateQuery
    }// class dbConnection

    ///////////////////////////////////////////////////////////// klasa wymiany danych z tabelą Maszyny

    //---------------------------------------------------------------> MaszynyVO
    ///<summary>
    ///Klasa wymiany danych z tabelą Maszyny
    ///</summary>
    public class MaszynyVO
    {
        private int _Identyfikator = -1;
        private string _Kategoria = string.Empty; // 100
        private string _Nazwa = string.Empty; // 255
        private string _Typ = string.Empty; // 255
        private string _Nr_inwentarzowy = string.Empty; // 255
        private string _Nr_fabryczny = string.Empty; // 255
        private string _Rok_produkcji = string.Empty; // 50
        private string _Producent = string.Empty; // 255
        private string _Zdjecie = string.Empty;//255
        private byte[] _Zawartosc_pliku = new byte[] { }; // obiekt OLE - zdjęcie
        private string _Rozszerz_zdj = string.Empty; // 255
        private string _Nazwa_os_zarzadzajaca = string.Empty; // 255
        private string _Nr_pom = string.Empty; // 255
        private string _Dzial = string.Empty; // 255
        private string _Nr_prot_BHP = string.Empty; // 255
        private int _Data_ost_przegl = 0; //liczba
        private int _Data_kol_przegl = 0; //liczba
        private string _Uwagi = string.Empty; //255
        private string _Wykorzystanie = string.Empty; // 255
        private string _Stan_techniczny = string.Empty; // 255
        private string _Propozycja = string.Empty; // 255
        private string _Nazwa_op_maszyny = string.Empty; // 255
        private int _Rok_ost_przeg = 0; //liczba
        private int _Mc_ost_przeg = 0; //liczba
        private int _Dz_ost_przeg = 0; //liczba
        private int _Rok_kol_przeg = 0; //liczba
        private int _Mc_kol_przeg = 0; //liczba
        private int _Dz_kol_przeg = 0; //liczba

        ///<summary>
        /// Konstruktor wymiany danych z tabelą Maszyny
        ///</summary>
        //public MaszynyVO (){   }
        public MaszynyVO() { }
        // gettery i settery
        public int Identyfikator
        {
            get { return _Identyfikator; }
            set { _Identyfikator = value; }
        }
        public string Kategoria
        {
            get { return _Kategoria; }
            set { _Kategoria = value; }
        }
        public string Nazwa
        {
            get { return _Nazwa; }
            set { _Nazwa = value; }
        }
        public string Typ
        {
            get { return _Typ; }
            set { _Typ = value; }
        }
        public string Nr_inwentarzowy
        {
            get { return _Nr_inwentarzowy; }
            set { _Nr_inwentarzowy = value; }
        }
        public string Nr_fabryczny
        {
            get { return _Nr_fabryczny; }
            set { _Nr_fabryczny = value; }
        }
        public string Rok_produkcji {
            get { return _Rok_produkcji; }
            set { _Rok_produkcji = value; }
        }
        public string Producent
        {
            get { return _Producent; }
            set { _Producent = value; }
        }
        public string Zdjecie
        {
            get { return _Zdjecie; }
            set { _Zdjecie = value; }
        }
        public byte[] Zawartosc_pliku
        {
            get { return _Zawartosc_pliku; }
            set { _Zawartosc_pliku = value; }
        }
        public string Rozszerz_zdj
        {
            get { return _Rozszerz_zdj; }
            set { _Rozszerz_zdj = value; }
        }       
        public string Nazwa_os_zarzadzajaca
        {
            get { return _Nazwa_os_zarzadzajaca; }
            set { _Nazwa_os_zarzadzajaca = value; }
        }
        public string Nr_pom {
            get { return _Nr_pom; }
            set { _Nr_pom = value; }
        }
        public string Dzial
        {
            get { return _Dzial; }
            set { _Dzial = value; }
        }
        public string Nr_prot_BHP {
            get { return _Nr_prot_BHP; }
            set { _Nr_prot_BHP = value; }
        }
        public int Data_ost_przegl
        {
            get { return _Data_ost_przegl; }
            set { _Data_ost_przegl = value; }
        }
        public int Data_kol_przegl
        {
            get { return _Data_kol_przegl; }
            set { _Data_kol_przegl = value; }
        }
        public string Uwagi {
            get { return _Uwagi; }
            set { _Uwagi = value; }
        }
        public string Wykorzystanie
        {
            get { return _Wykorzystanie; }
            set { _Wykorzystanie = value; }
        }
        public string Stan_techniczny
        {
            get { return _Stan_techniczny; }
            set { _Stan_techniczny = value; }
        }
        public string Propozycja
        {
            get { return _Propozycja; }
            set { _Propozycja = value; }
        }
        public string Nazwa_op_maszyny
        {
            get { return _Nazwa_op_maszyny; }
            set { _Nazwa_op_maszyny = value; }
        }
        public int Rok_ost_przeg
        {
            get { return _Rok_ost_przeg; }
            set { _Rok_ost_przeg = value; }
        }
        public int Mc_ost_przeg
        {
            get { return _Mc_ost_przeg; }
            set { _Mc_ost_przeg = value; }
        }
        public int Dz_ost_przeg
        {
            get { return _Dz_ost_przeg; }
            set { _Dz_ost_przeg = value; }
        }
        public int Rok_kol_przeg
        {
            get { return _Rok_kol_przeg; }
            set { _Rok_kol_przeg = value; }
        }
        public int Mc_kol_przeg
        {
            get { return _Mc_kol_przeg; }
            set { _Mc_kol_przeg = value; }
        }
        public int Dz_kol_przeg
        {
            get { return _Dz_kol_przeg; }
            set { _Dz_kol_przeg = value; }
        }
        
    }// class MaszynyVO

    //klasa dostępu (Data Access Object) do tabeli Maszyny ----------> DAO
    public class MaszynyDAO
    {
        private dbConnection _conn;
        public string _error = string.Empty;

        ///<construktor>
        ///Konstruktor
        ///</construktor>
        public MaszynyDAO(string connString) {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        } //konstruktor MaszynyDAO

        public DataTable selectQuery(string query)
        {
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//selectQuery

        /// <summary>
        /// Zwraca tabelę wszystkich Maszyn.
        /// </summary>
        /// <returns>Tabele.</returns>
        public DataTable select()
        {
            string query = "SELECT * FROM Maszyny ORDER BY Nazwa ASC";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Zwraca tabelę wszystkich Maszyn po ID.
        /// </summary>
        /// <returns>Tabele.</returns>
        public DataTable select(int Identyfikator)
        {
            string query = "SELECT * FROM Maszyny WHERE Identyfikator = " + Identyfikator.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select po ID
        
        
        /// <summary>
        /// Wprowadza nowy rekord.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool insert(nsAccess2DB.MaszynyVO VO)
        {
            string query = "INSERT INTO Maszyny (Kategoria, Nazwa, Typ, Nr_inwentarzowy, Nr_fabryczny, Rok_produkcji, Producent, Zdjecie, Zawartosc_pliku, Rozszerz_zdj, Nazwa_os_zarzadzajaca, Nr_pom, Dzial, Nr_prot_BHP, Data_ost_przegl, Data_kol_przegl, " +
                "Uwagi, Wykorzystanie, Stan_techniczny, Propozycja, Nazwa_op_maszyny, Rok_ost_przeg, Mc_ost_przeg, Dz_ost_przeg, Rok_kol_przeg, Mc_kol_przeg, Dz_kol_przeg)" +
                " VALUES (@Kategoria, @Nazwa, @Typ, @Nr_inwentarzowy, @Nr_fabryczny, @Rok_produkcji, @Producent, @Zdjecie, @Zawartosc_pliku, @Rozszerz_zdj, @Nazwa_os_zarzadzajaca, @Nr_pom, @Dzial, @Nr_prot_BHP, @Data_ost_przegl, @Data_kol_przegl, " +
                "@Uwagi, @Wykorzystanie, @Stan_techniczny, @Propozycja, @Nazwa_op_maszyny, @Rok_ost_przeg, @Mc_ost_przeg, @Dz_ost_przeg, @Rok_kol_przeg, @Mc_kol_przeg, @Dz_kol_przeg);";

            OleDbParameter[] parameters = new OleDbParameter[27];
            parameters[0] = new OleDbParameter("Kategoria", OleDbType.VarChar, 100);
            parameters[0].Value = VO.Kategoria;

            parameters[1] = new OleDbParameter("Nazwa", OleDbType.VarChar, 255);
            parameters[1].Value = VO.Nazwa;

            parameters[2] = new OleDbParameter("Typ", OleDbType.VarChar, 255);
            parameters[2].Value = VO.Typ;

            parameters[3] = new OleDbParameter("Nr_inwentarzowy", OleDbType.VarChar, 255);
            parameters[3].Value = VO.Nr_inwentarzowy;

            parameters[4] = new OleDbParameter("Nr_fabryczny", OleDbType.VarChar, 255);
            parameters[4].Value = VO.Nr_fabryczny;

            parameters[5] = new OleDbParameter("Rok_produkcji", OleDbType.VarChar, 50);
            parameters[5].Value = VO.Rok_produkcji;

            parameters[6] = new OleDbParameter("Producent", OleDbType.VarChar, 255);
            parameters[6].Value = VO.Producent;

            parameters[7] = new OleDbParameter("Zdjecie", OleDbType.VarChar, 255);
            parameters[7].Value = VO.Zdjecie;

            parameters[8] = new OleDbParameter("Zawartosc_pliku", OleDbType.VarBinary, 255);
            parameters[8].Value = VO.Zawartosc_pliku;

            parameters[9] = new OleDbParameter("Rozszerz_zdj", OleDbType.VarChar, 255);
            parameters[9].Value = VO.Rozszerz_zdj;

            parameters[10] = new OleDbParameter("Nazwa_os_zarzadzajaca", OleDbType.VarChar, 255);
            parameters[10].Value = VO.Nazwa_os_zarzadzajaca;

            parameters[11] = new OleDbParameter("Nr_pom", OleDbType.VarChar, 255);
            parameters[11].Value = VO.Nr_pom;
            
            parameters[12] = new OleDbParameter("Dzial", OleDbType.VarChar, 20);
            parameters[12].Value = VO.Dzial;

            parameters[13] = new OleDbParameter("Nr_prot_BHP", OleDbType.VarChar, 20);
            parameters[13].Value = VO.Nr_prot_BHP;

            parameters[14] = new OleDbParameter("Data_ost_przegl", OleDbType.Integer);
            parameters[14].Value = VO.Data_ost_przegl;

            parameters[15] = new OleDbParameter("Data_kol_przegl", OleDbType.Integer);
            parameters[15].Value = VO.Data_kol_przegl;

            parameters[16] = new OleDbParameter("Uwagi", OleDbType.VarChar, 255);
            parameters[16].Value = VO.Uwagi;

            parameters[17] = new OleDbParameter("Wykorzystanie", OleDbType.VarChar, 20);
            parameters[17].Value = VO.Wykorzystanie;

            parameters[18] = new OleDbParameter("Stan_techniczny", OleDbType.VarChar, 20);
            parameters[18].Value = VO.Stan_techniczny;

            parameters[19] = new OleDbParameter("Propozycja", OleDbType.VarChar, 20);
            parameters[19].Value = VO.Propozycja;

            parameters[20] = new OleDbParameter("Nazwa_op_maszyny", OleDbType.VarChar, 100);
            parameters[20].Value = VO.Nazwa_op_maszyny;

            parameters[21] = new OleDbParameter("Rok_ost_przeg", OleDbType.Integer);
            parameters[21].Value = VO.Rok_ost_przeg;

            parameters[22] = new OleDbParameter("Mc_ost_przeg", OleDbType.Integer);
            parameters[22].Value = VO.Mc_ost_przeg;

            parameters[23] = new OleDbParameter("Dz_ost_przeg", OleDbType.Integer);
            parameters[23].Value = VO.Dz_ost_przeg;

            parameters[24] = new OleDbParameter("Rok_kol_przeg", OleDbType.Integer);
            parameters[24].Value = VO.Rok_kol_przeg;

            parameters[25] = new OleDbParameter("Mc_kol_przeg", OleDbType.Integer);
            parameters[25].Value = VO.Mc_kol_przeg;

            parameters[26] = new OleDbParameter("Dz_kol_przeg", OleDbType.Integer);
            parameters[26].Value = VO.Dz_kol_przeg;
            
            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//insert
         /// <summary>
         /// Aktualizuje rekord z wyjątkiem ID.
         /// </summary>
         /// <param name="VO">Obiekt wymiany danych</param>
         /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool update(nsAccess2DB.MaszynyVO VO)
        {
            string query = "UPDATE Maszyny SET Kategoria = @Kategoria, Nazwa = @Nazwa, Typ = @Typ, Nr_inwentarzowy = @Nr_inwentarzowy, Nr_fabryczny = @Nr_fabryczny, Rok_produkcji = @Rok_produkcji, Producent = @Producent, " +
                "Zdjecie = @Zdjecie, Zawartosc_pliku = @Zawartosc_pliku, Rozszerz_zdj = @Rozszerz_zdj, Nazwa_os_zarzadzajaca = @Nazwa_os_zarzadzajaca, Nr_pom = @Nr_pom, Dzial = @Dzial, Nr_prot_BHP = @Nr_prot_BHP, Data_ost_przegl = @Data_ost_przegl, " +
                "Data_kol_przegl = @Data_kol_przegl, Uwagi = @Uwagi, Wykorzystanie = @Wykorzystanie, Stan_techniczny = @Stan_techniczny, Propozycja = @Propozycja, Nazwa_op_maszyny = @Nazwa_op_maszyny, Rok_ost_przeg = @Rok_ost_przeg," +
                " Mc_ost_przeg = @Mc_ost_przeg, Dz_ost_przeg = @Dz_ost_przeg, Rok_kol_przeg = @Rok_kol_przeg, Mc_kol_przeg = @Mc_kol_przeg, Dz_kol_przeg = @Dz_kol_przeg WHERE Identyfikator = " + VO.Identyfikator.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[27];
            parameters[0] = new OleDbParameter("Kategoria", OleDbType.VarChar, 100);
            parameters[0].Value = VO.Kategoria;

            parameters[1] = new OleDbParameter("Nazwa", OleDbType.VarChar, 255);
            parameters[1].Value = VO.Nazwa;

            parameters[2] = new OleDbParameter("Typ", OleDbType.VarChar, 255);
            parameters[2].Value = VO.Typ;

            parameters[3] = new OleDbParameter("Nr_inwentarzowy", OleDbType.VarChar, 255);
            parameters[3].Value = VO.Nr_inwentarzowy;

            parameters[4] = new OleDbParameter("Nr_fabryczny", OleDbType.VarChar, 255);
            parameters[4].Value = VO.Nr_fabryczny;

            parameters[5] = new OleDbParameter("Rok_produkcji", OleDbType.VarChar, 50);
            parameters[5].Value = VO.Rok_produkcji;

            parameters[6] = new OleDbParameter("Producent", OleDbType.VarChar, 255);
            parameters[6].Value = VO.Producent;

            parameters[7] = new OleDbParameter("Zdjecie", OleDbType.VarChar, 255);
            parameters[7].Value = VO.Zdjecie;

            parameters[8] = new OleDbParameter("Zawartosc_pliku", OleDbType.VarBinary, 255);
            parameters[8].Value = VO.Zawartosc_pliku;

            parameters[9] = new OleDbParameter("Rozszerz_zdj", OleDbType.VarChar, 255);
            parameters[9].Value = VO.Rozszerz_zdj;

            parameters[10] = new OleDbParameter("Nazwa_os_zarzadzajaca", OleDbType.VarChar, 255);
            parameters[10].Value = VO.Nazwa_os_zarzadzajaca;

            parameters[11] = new OleDbParameter("Nr_pom", OleDbType.VarChar, 255);
            parameters[11].Value = VO.Nr_pom;

            parameters[12] = new OleDbParameter("Dzial", OleDbType.VarChar, 20);
            parameters[12].Value = VO.Dzial;

            parameters[13] = new OleDbParameter("Nr_prot_BHP", OleDbType.VarChar, 20);
            parameters[13].Value = VO.Nr_prot_BHP;

            parameters[14] = new OleDbParameter("Data_ost_przegl", OleDbType.Integer);
            parameters[14].Value = VO.Data_ost_przegl;

            parameters[15] = new OleDbParameter("Data_kol_przegl", OleDbType.Integer);
            parameters[15].Value = VO.Data_kol_przegl;

            parameters[16] = new OleDbParameter("Uwagi", OleDbType.VarChar, 255);
            parameters[16].Value = VO.Uwagi;

            parameters[17] = new OleDbParameter("Wykorzystanie", OleDbType.VarChar, 20);
            parameters[17].Value = VO.Wykorzystanie;

            parameters[18] = new OleDbParameter("Stan_techniczny", OleDbType.VarChar, 20);
            parameters[18].Value = VO.Stan_techniczny;

            parameters[19] = new OleDbParameter("Propozycja", OleDbType.VarChar, 20);
            parameters[19].Value = VO.Propozycja;

            parameters[20] = new OleDbParameter("Nazwa_op_maszyny", OleDbType.VarChar, 100);
            parameters[20].Value = VO.Nazwa_op_maszyny;

            parameters[21] = new OleDbParameter("Rok_ost_przeg", OleDbType.Integer);
            parameters[21].Value = VO.Rok_ost_przeg;

            parameters[22] = new OleDbParameter("Mc_ost_przeg", OleDbType.Integer);
            parameters[22].Value = VO.Mc_ost_przeg;

            parameters[23] = new OleDbParameter("Dz_ost_przeg", OleDbType.Integer);
            parameters[23].Value = VO.Dz_ost_przeg;

            parameters[24] = new OleDbParameter("Rok_kol_przeg", OleDbType.Integer);
            parameters[24].Value = VO.Rok_kol_przeg;

            parameters[25] = new OleDbParameter("Mc_kol_przeg", OleDbType.Integer);
            parameters[25].Value = VO.Mc_kol_przeg;

            parameters[26] = new OleDbParameter("Dz_kol_przeg", OleDbType.Integer);
            parameters[26].Value = VO.Dz_kol_przeg;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//update

        public bool delete(int Identyfikator)
        {
            string query = "DELETE* FROM Maszyny WHERE Identyfikator = " + Identyfikator.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }// delete

       

    }//class MaszynyDAO

    // -------------------------------------------------------------> BUS - warstawa operacji biznesowych tabeli Maszyny
    public class MaszynyBUS
    {
        MaszynyDAO _DAO;
        private MaszynyVO[] _VOs = new MaszynyVO[0];    //lista danych
        private MaszynyVO _VOi = new MaszynyVO();        //dane na pozycji _idx
        private int _idx = 0;                           //indeks pozycji
        private bool _eof = false;                      //wskaźnik końca pliku
        private int _count = 0;                         //liczba pozycji

        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">ConnectionStrine.</param>
        public MaszynyBUS(string connString)
        {
            _DAO = new MaszynyDAO(connString);
            _error = _DAO._error;
        }//public MaszynyBUS

        /// <summary>
        /// Wprowadza rekord do tabeli.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        private bool insert(nsAccess2DB.MaszynyVO VO)
        {
            bool b = _DAO.insert(VO);
            _error = _DAO._error;
            return b;
        }//insert

        /// <summary>
        /// Aktualizuje rekord z wyjątkiem ID czynności.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        private bool update(nsAccess2DB.MaszynyVO VO)
        {
            bool b = _DAO.update(VO);
            _error = _DAO._error;
            return b;
        }//update
         /// <summary>
         /// Dodaje lub aktualizuje rekord.
         /// </summary>
         /// <param name="VO">Obiekt wymiany danych.</param>
         /// <returns>Wynik powodzenia akcji.</returns>
        public bool write(nsAccess2DB.MaszynyVO VO)
        {
            if (exists(VO.Identyfikator))
            {
                return _DAO.update(VO);
            }
            return _DAO.insert(VO);
        }//write

        /// <summary>
        /// Wypełnia tablice.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            MaszynyVO VOi;
            _VOs = new MaszynyVO[0];

             foreach (DataRow dr in dt.Rows)
             {
                Array.Resize(ref _VOs, _VOs.Length + 1);

                VOi = new MaszynyVO();
               
                VOi.Identyfikator = int.Parse(dr["Identyfikator"].ToString());
                VOi.Kategoria = dr["Kategoria"].ToString();
                VOi.Nazwa = dr["Nazwa"].ToString();
                VOi.Typ = dr["Typ"].ToString();
                VOi.Nr_inwentarzowy = dr["Nr_inwentarzowy"].ToString();
                VOi.Nr_fabryczny = dr["Nr_fabryczny"].ToString();
                VOi.Rok_produkcji = dr["Rok_produkcji"].ToString();
                VOi.Producent = dr["Producent"].ToString();
                VOi.Zdjecie = dr["Zdjecie"].ToString();

                try
                {
                    VOi.Zawartosc_pliku = (byte[])dr["Zawartosc_pliku"];
                }
                catch
                {
                    VOi.Zawartosc_pliku = new byte[] { };
                }

                VOi.Rozszerz_zdj = dr["Rozszerz_zdj"].ToString();
                VOi.Nazwa_os_zarzadzajaca = dr["Nazwa_os_zarzadzajaca"].ToString();
                VOi.Nr_pom = dr["Nr_pom"].ToString();
                VOi.Dzial = dr["Dzial"].ToString();
                VOi.Nr_prot_BHP = dr["Nr_prot_BHP"].ToString();

                try
                {
                    VOi.Data_ost_przegl = int.Parse(dr["Data_ost_przegl"].ToString());
                }
                catch { }

                try
                {
                    VOi.Data_kol_przegl = int.Parse(dr["Data_kol_przegl"].ToString());
                }
                catch { }
                
                VOi.Uwagi = dr["Uwagi"].ToString();
                VOi.Wykorzystanie = dr["Wykorzystanie"].ToString();
                VOi.Stan_techniczny = dr["Stan_techniczny"].ToString();
                VOi.Propozycja = dr["Propozycja"].ToString();
                VOi.Nazwa_op_maszyny = dr["Nazwa_op_maszyny"].ToString();
                VOi.Rok_ost_przeg = int.Parse(dr["Rok_ost_przeg"].ToString());
                VOi.Mc_ost_przeg = int.Parse(dr["Mc_ost_przeg"].ToString());
                VOi.Dz_ost_przeg = int.Parse(dr["Dz_ost_przeg"].ToString());
                VOi.Rok_kol_przeg = int.Parse(dr["Rok_kol_przeg"].ToString());
                VOi.Mc_kol_przeg = int.Parse(dr["Mc_kol_przeg"].ToString());
                VOi.Dz_kol_przeg = int.Parse(dr["Dz_kol_przeg"].ToString());

                _VOs[_VOs.Length - 1] = VOi;
             }
            _eof = _VOs.Length == 0;
            _count = _VOs.Length;
            if (_count > 0)
                _idx = 0;
            else
            {
                _idx = -1;
                _eof = true;
            }
        }//fillTable

        /// <summary>
        /// Wypełnia tablice danych pozycjami.
        /// </summary>
        public void select()
        {
            fillTable(_DAO.select());
        }//select

       
        /// <summary>
        /// Wypełnia tablice danych pozycjami.
        /// </summary>
        /// <param name="Identyfikator">ID maszyny.</param>
        public void select(int Identyfikator)
        {
            fillTable(_DAO.select(Identyfikator));
        }//select

        public bool delete(int Identyfikator)
        {
            return _DAO.delete(Identyfikator);
        }

        /// <summary>
        /// Przemieszcza indeks w tablicy danych o jedną pozycję.
        /// </summary>
        public void skip()
        {
            if (_count > 0)
            {
                _idx++;
                _eof = _idx >= _count;
            }
        }//skip

        /// <summary>
        /// Przemieszcza indeks w tablicy danych na pozycję pierwszą.
        /// </summary>
        public void top()
        {
            if (_count > 0)
            {
                _idx = 0;
                _eof = false;
            }
        }//top

        /// <summary>
        /// Zmienna logiczna osiągnięcia końca pliku.
        /// </summary>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        public MaszynyVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new MaszynyVO();
            }
        }//MaszynyVO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        public int idx
        {
            set
            {
                if (value > -1 & value < _count)
                {
                    _idx = value;
                }
            }
        }//idx

        /// <summary>
        /// Sprawdza istnienie rekordu.
        /// </summary>
        /// <param name="Identyfikator">ID maszyny.</param>
        /// <returns>Wynik logiczny sprawdzenia.</returns>
        private bool exists(int Identyfikator)
        {
            foreach (MaszynyVO VOi in _VOs)
            {
                if (VOi.Identyfikator == Identyfikator) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Identyfikator">ID maszyny.</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora.</returns>
        public int getIdx(int Identyfikator)
        {
            int idx = -1;
            foreach (MaszynyVO VOi in _VOs)
            {
                idx++;
                if (VOi.Identyfikator == Identyfikator) return idx;
            }

            return -1;
        }//getIdx

        /// <summary>
        /// Zwraca maszynę o wskazanym identyfikatorze.
        /// </summary>
        /// <param name="Identyfikator">Identyfikator maszyny.</param>
        /// <returns>Maszyny. Jeśli ID==-1 to maszyny nie znaleziono.</returns>
        public MaszynyVO GetVO(int Identyfikator)
        {
            foreach (nsAccess2DB.MaszynyVO VO in _VOs)
            {
                if (VO.Identyfikator == Identyfikator) return VO;
            }
            return new MaszynyVO();
        }//getVO

        public void selectQuery(string query)
        {
            fillTable(_DAO.selectQuery(query));
        }//selectQuery
        
        /// <summary>
        /// zwraca indeks szukanej nazwy
        /// </summary>
        /// <param name="Nazwa"></param>
        /// <returns></returns>
       

    }//public class MaszynyBUS

    //////////////////////////////////////////////////////////////////////// Wykorzystanie
    /// <summary>
    /// Klasa wymiany danych z tabelą Czestotliwosc.
    /// </summary>
    public class WykorzystanieVO
    {
        private string _Wykorzystanie = string.Empty; //100
            /// <summary>
            /// Konstruktor wymiany danych Wykorzystanie
            /// </summary>
        public WykorzystanieVO() { }

        public string Wykorzystanie
        {
            get { return _Wykorzystanie; }
            set { _Wykorzystanie = value; }
        }
    }//class WykorzystanieVO

    //Klasa dostępu (Data Access Object) do tabeli Wykorzystanie.
    public class WykorzystanieDAO
    {
        private dbConnection _conn;
        public string _error = string.Empty;

        /// <constructor>
        /// Konstruktor.
        /// </constructor>
        public WykorzystanieDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }//WykorzystanieDAO

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        public DataTable select()
        {
            string query = "SELECT * FROM Wykorzystanie;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

    }//class WykorzystanieDAO

    //Warstwa operacji biznesowaych tabeli Wykorzystanie.
    public class WykorzystanieBUS
    {
        WykorzystanieDAO _DAO;

        private WykorzystanieVO[] _VOs = new WykorzystanieVO[0];    //lista danych
        private WykorzystanieVO _VOi = new WykorzystanieVO();       //dane na pozycji _idx
        private int _idx = 0;                       //indeks pozycji
        private bool _eof = false;                  //wskaźnik końca pliku
        private int _count = 0;                     //liczba pozycji

        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">ConnectionStrine.</param>
        public WykorzystanieBUS(string connString)
        {
            _DAO = new WykorzystanieDAO(connString);
            _error = _DAO._error;
        }//WykorzystanieBUS

        /// <summary>
        /// Wypełnia tablice.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            WykorzystanieVO VOi;
            _VOs = new WykorzystanieVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);

                VOi = new WykorzystanieVO();

                VOi.Wykorzystanie = dr["Wykorzystanie"].ToString();

                _VOs[_VOs.Length - 1] = VOi;
            }

            _eof = _VOs.Length == 0;
            _count = _VOs.Length;
            if (_count > 0)
                _idx = 0;
            else
            {
                _idx = -1;
                _eof = true;
            }
        }//fillTable

        /// <summary>
        /// Wypełnia tablice danych pozycjami.
        /// </summary>
        public void select()
        {
            fillTable(_DAO.select());
        }//select

        /// <summary>
        /// Przemieszcza indeks w tablicy danych o jedną pozycję.
        /// </summary>
        public void skip()
        {
            if (_count > 0)
            {
                _idx++;
                _eof = _idx >= _count;
            }
        }//skip

        /// <summary>
        /// Przemieszcza indeks w tablicy danych na pozycję pierwszą.
        /// </summary>
        public void top()
        {
            if (_count > 0)
            {
                _idx = 0;
                _eof = false;
            }
        }//top

        /// <summary>
        /// Zmienna logiczna osiągnięcia końca pliku.
        /// </summary>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        public WykorzystanieVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new WykorzystanieVO();
            }
        }//VO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        public int idx
        {
            set
            {
                if (value > -1 & value < _count)
                {
                    _idx = value;
                }
            }
        }//idx

        /// <summary>
        /// Sprawdza istnienie rekordu.
        /// </summary>
        /// <param name="Id">Nazwa Wykorzystanie.</param>
        /// <returns>Wynik logiczny sprawdzenia.</returns>
        public bool exists(String Wykorzystanie)
        {
            foreach (WykorzystanieVO VOi in _VOs)
            {
                if (VOi.Wykorzystanie == Wykorzystanie) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Wykorzystanie">Identyfikator Wykorzystania maszyny</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora Wykorzystania.</returns>
        public int getIdx(string Wykorzystanie)
        {
            int idx = -1;
            foreach (WykorzystanieVO VOi in _VOs)
            {
                idx++;
                if (VOi.Wykorzystanie == Wykorzystanie) return idx;
            }

            return -1;
        }//getIdx

    }//class WykorzystanieBUS

    /////////////////////////////////////////////////////////////////////////////////// Kategoria
    /// <summary>
    /// Klasa wymiany danych z tabelą Kategoria.
    /// </summary>
    public class KategoriaVO
    {
        private string _Kategoria = string.Empty; //100
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Kategoria
        /// </summary>
        public KategoriaVO() { }

        public string NazwaKategoria
        {
            get { return _Kategoria; }
            set { _Kategoria = value; }
        }
    }//class CzestotliwoscVO

    //Klasa dostępu (Data Access Object) do tabeli Kategoria.
    public class KategoriaDAO
    {
        private dbConnection _conn;
        public string _error = string.Empty;

        /// <constructor>
        /// Konstruktor.
        /// </constructor>
        public KategoriaDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }//KategoriaDAO

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        public DataTable select()
        {
            string query = "SELECT * FROM Kategoria;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

    }//class KategoriaDAO

    //Warstwa operacji biznesowaych tabeli Kategoria.
    public class KategoriaBUS
    {
        KategoriaDAO _DAO;

        private KategoriaVO[] _VOs = new KategoriaVO[0];    //lista danych
        private KategoriaVO _VOi = new KategoriaVO();       //dane na pozycji _idx
        private int _idx = 0;                       //indeks pozycji
        private bool _eof = false;                  //wskaźnik końca pliku
        private int _count = 0;                     //liczba pozycji

        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">ConnectionString.</param>
        public KategoriaBUS(string connString)
        {
            _DAO = new KategoriaDAO(connString);
            _error = _DAO._error;
        }//KategoriaBUS

        /// <summary>
        /// Wypełnia tablice.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            KategoriaVO VOi;
            _VOs = new KategoriaVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);

                VOi = new KategoriaVO();

                VOi.NazwaKategoria = dr["Kategoria"].ToString();

                _VOs[_VOs.Length - 1] = VOi;
            }

            _eof = _VOs.Length == 0;
            _count = _VOs.Length;
            if (_count > 0)
                _idx = 0;
            else
            {
                _idx = -1;
                _eof = true;
            }

        }//fillTable

        /// <summary>
        /// Wypełnia tablice danych pozycjami.
        /// </summary>
        public void select()
        {
            fillTable(_DAO.select());
        }//select

        /// <summary>
        /// Przemieszcza indeks w tablicy danych o jedną pozycję.
        /// </summary>
        public void skip()
        {
            if (_count > 0)
            {
                _idx++;
                _eof = _idx >= _count;
            }
        }//skip

        /// <summary>
        /// Przemieszcza indeks w tablicy danych na pozycję pierwszą.
        /// </summary>
        public void top()
        {
            if (_count > 0)
            {
                _idx = 0;
                _eof = false;
            }
        }//top

        /// <summary>
        /// Zmienna logiczna osiągnięcia końca pliku.
        /// </summary>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        public KategoriaVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new KategoriaVO();
            }
        }//VO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        public int idx
        {
            set
            {
                if (value > -1 & value < _count)
                {
                    _idx = value;
                }
            }
        }//idx

        /// <summary>
        /// Sprawdza istnienie rekordu.
        /// </summary>
        /// <param name="Id">Nazwa Kategoria.</param>
        /// <returns>Wynik logiczny sprawdzenia.</returns>
        public bool exists(String Nazwa)
        {
            foreach (KategoriaVO VOi in _VOs)
            {
                if (VOi.NazwaKategoria == Nazwa) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Nazwa">Identyfikator Kategoria maszyn</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora Kategori.</returns>
        public int getIdx(string Nazwa)
        {
            int idx = -1;
            foreach (KategoriaVO VOi in _VOs)
            {
                idx++;
                if (VOi.NazwaKategoria == Nazwa) return idx;
            }

            return -1;
        }//getIdx

    }//class KategoriaBUS
     /////////////////////////////////////////////////////////////////////////////////// Propozycja
     /// <summary>
     /// Klasa wymiany danych z tabelą Propozycja.
     /// </summary>
    public class PropozycjaVO
    {
        private string _Propozycja = string.Empty; //100
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Propozycja
        /// </summary>
        public PropozycjaVO() { }

        public string Nazwa
        {
            get { return _Propozycja; }
            set { _Propozycja = value; }
        }
    }//class PropozycjaVO

    //Klasa dostępu (Data Access Object) do tabeli Propozycja.
    public class PropozycjaDAO
    {
        private dbConnection _conn;
        public string _error = string.Empty;

        /// <constructor>
        /// Konstruktor.
        /// </constructor>
        public PropozycjaDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }//PropozycjaDAO

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        public DataTable select()
        {
            string query = "SELECT * FROM Propozycja;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

    }//class PropozycjaDAO

    //Warstwa operacji biznesowaych tabeli Propozycja.
    public class PropozycjaBUS
    {
        PropozycjaDAO _DAO;

        private PropozycjaVO[] _VOs = new PropozycjaVO[0];    //lista danych
        private PropozycjaVO _VOi = new PropozycjaVO();       //dane na pozycji _idx
        private int _idx = 0;                       //indeks pozycji
        private bool _eof = false;                  //wskaźnik końca pliku
        private int _count = 0;                     //liczba pozycji

        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">ConnectionString.</param>
        public PropozycjaBUS(string connString)
        {
            _DAO = new PropozycjaDAO(connString);
            _error = _DAO._error;
        }//PropozycjaBUS

        /// <summary>
        /// Wypełnia tablice.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            PropozycjaVO VOi;
            _VOs = new PropozycjaVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);

                VOi = new PropozycjaVO();

                VOi.Nazwa = dr["Propozycja"].ToString();

                _VOs[_VOs.Length - 1] = VOi;
            }

            _eof = _VOs.Length == 0;
            _count = _VOs.Length;
            if (_count > 0)
                _idx = 0;
            else
            {
                _idx = -1;
                _eof = true;
            }

        }//fillTable

        /// <summary>
        /// Wypełnia tablice danych pozycjami.
        /// </summary>
        public void select()
        {
            fillTable(_DAO.select());
        }//select

        /// <summary>
        /// Przemieszcza indeks w tablicy danych o jedną pozycję.
        /// </summary>
        public void skip()
        {
            if (_count > 0)
            {
                _idx++;
                _eof = _idx >= _count;
            }
        }//skip

        /// <summary>
        /// Przemieszcza indeks w tablicy danych na pozycję pierwszą.
        /// </summary>
        public void top()
        {
            if (_count > 0)
            {
                _idx = 0;
                _eof = false;
            }
        }//top

        /// <summary>
        /// Zmienna logiczna osiągnięcia końca pliku.
        /// </summary>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        public PropozycjaVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new PropozycjaVO();
            }
        }//VO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        public int idx
        {
            set
            {
                if (value > -1 & value < _count)
                {
                    _idx = value;
                }
            }
        }//idx

        /// <summary>
        /// Sprawdza istnienie rekordu.
        /// </summary>
        /// <param name="id">Nazwa Propozycja.</param>
        /// <returns>Wynik logiczny sprawdzenia.</returns>
        public bool exists(String Nazwa)
        {
            foreach (PropozycjaVO VOi in _VOs)
            {
                if (VOi.Nazwa == Nazwa) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Nazwa">Identyfikator Propozycja dot maszyn</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora Propozycja.</returns>
        public int getIdx(string Nazwa)
        {
            int idx = -1;
            foreach (PropozycjaVO VOi in _VOs)
            {
                idx++;
                if (VOi.Nazwa == Nazwa) return idx;
            }

            return -1;
        }//getIdx

    }//class PropozycjaBUS

    /////////////////////////////////////////////////////////////////////////////////// Stan_techniczny
    /// <summary>
    /// Klasa wymiany danych z tabelą Stan_techniczny.
    /// </summary>
    public class Stan_technicznyVO
    {
        private string _Stan_techniczny = string.Empty; //100
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Stan_techniczny
        /// </summary>
        public Stan_technicznyVO() { }

        public string Nazwa
        {
            get { return _Stan_techniczny; }
            set { _Stan_techniczny = value; }
        }
    }//class Stan_technicznyVO

    //Klasa dostępu (Data Access Object) do tabeli Stan_techniczny.
    public class Stan_technicznyDAO
    {
        private dbConnection _conn;
        public string _error = string.Empty;

        /// <constructor>
        /// Konstruktor.
        /// </constructor>
        public Stan_technicznyDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }//Stan_technicznyDAO

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        public DataTable select()
        {
            string query = "SELECT * FROM Stan_techniczny;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

    }//class Stan_technicznyDAO

    //Warstwa operacji biznesowaych tabeli Stan_techniczny.
    public class Stan_technicznyBUS
    {
        Stan_technicznyDAO _DAO;

        private Stan_technicznyVO[] _VOs = new Stan_technicznyVO[0];    //lista danych
        private Stan_technicznyVO _VOi = new Stan_technicznyVO();       //dane na pozycji _idx
        private int _idx = 0;                       //indeks pozycji
        private bool _eof = false;                  //wskaźnik końca pliku
        private int _count = 0;                     //liczba pozycji

        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">ConnectionString.</param>
        public Stan_technicznyBUS(string connString)
        {
            _DAO = new Stan_technicznyDAO(connString);
            _error = _DAO._error;
        }//Stan_technicznyBUS

        /// <summary>
        /// Wypełnia tablice.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            Stan_technicznyVO VOi;
            _VOs = new Stan_technicznyVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);

                VOi = new Stan_technicznyVO();

                VOi.Nazwa = dr["Stan_techniczny"].ToString();

                _VOs[_VOs.Length - 1] = VOi;
            }

            _eof = _VOs.Length == 0;
            _count = _VOs.Length;
            if (_count > 0)
                _idx = 0;
            else
            {
                _idx = -1;
                _eof = true;
            }

        }//fillTable

        /// <summary>
        /// Wypełnia tablice danych pozycjami.
        /// </summary>
        public void select()
        {
            fillTable(_DAO.select());
        }//select

        /// <summary>
        /// Przemieszcza indeks w tablicy danych o jedną pozycję.
        /// </summary>
        public void skip()
        {
            if (_count > 0)
            {
                _idx++;
                _eof = _idx >= _count;
            }
        }//skip

        /// <summary>
        /// Przemieszcza indeks w tablicy danych na pozycję pierwszą.
        /// </summary>
        public void top()
        {
            if (_count > 0)
            {
                _idx = 0;
                _eof = false;
            }
        }//top

        /// <summary>
        /// Zmienna logiczna osiągnięcia końca pliku.
        /// </summary>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        public Stan_technicznyVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new Stan_technicznyVO();
            }
        }//VO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        public int idx
        {
            set
            {
                if (value > -1 & value < _count)
                {
                    _idx = value;
                }
            }
        }//idx

        /// <summary>
        /// Sprawdza istnienie rekordu.
        /// </summary>
        /// <param name="id">Stan_techniczny.</param>
        /// <returns>Wynik logiczny sprawdzenia.</returns>
        public bool exists(String Nazwa)
        {
            foreach (Stan_technicznyVO VOi in _VOs)
            {
                if (VOi.Nazwa == Nazwa) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Nazwa">Identyfikator Stan_techniczny maszyn</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora Propozycja.</returns>
        public int getIdx(string Nazwa)
        {
            int idx = -1;
            foreach (Stan_technicznyVO VOi in _VOs)
            {
                idx++;
                if (VOi.Nazwa == Nazwa) return idx;
            }

            return -1;
        }//getIdx

    }//class Stan_technicznyBUS
     /////////////////////////////////////////////////////////////////////////////////// Dzial
     /// <summary>
     /// Klasa wymiany danych z tabelą Dzial.
     /// </summary>
    public class DzialVO
    {
        private string _Dzial = string.Empty; //100
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Dzial
        /// </summary>
        public DzialVO() { }

        public string Nazwa
        {
            get { return _Dzial; }
            set { _Dzial = value; }
        }
    }//class DzialVO

    //Klasa dostępu (Data Access Object) do tabeli Dzial.
    public class DzialDAO
    {
        private dbConnection _conn;
        public string _error = string.Empty;

        /// <constructor>
        /// Konstruktor.
        /// </constructor>
        public DzialDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }//DzialDAO

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        public DataTable select()
        {
            string query = "SELECT * FROM Dzial;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

    }//class DzialDAO

    //Warstwa operacji biznesowaych tabeli Dzial.
    public class DzialBUS
    {
        DzialDAO _DAO;

        private DzialVO[] _VOs = new DzialVO[0];    //lista danych
        private DzialVO _VOi = new DzialVO();       //dane na pozycji _idx
        private int _idx = 0;                       //indeks pozycji
        private bool _eof = false;                  //wskaźnik końca pliku
        private int _count = 0;                     //liczba pozycji

        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">ConnectionString.</param>
        public DzialBUS(string connString)
        {
            _DAO = new DzialDAO(connString);
            _error = _DAO._error;
        }//DzialBUS

        /// <summary>
        /// Wypełnia tablice.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            DzialVO VOi;
            _VOs = new DzialVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);

                VOi = new DzialVO();

                VOi.Nazwa = dr["Dzial"].ToString();

                _VOs[_VOs.Length - 1] = VOi;
            }

            _eof = _VOs.Length == 0;
            _count = _VOs.Length;
            if (_count > 0)
                _idx = 0;
            else
            {
                _idx = -1;
                _eof = true;
            }

        }//fillTable

        /// <summary>
        /// Wypełnia tablice danych pozycjami.
        /// </summary>
        public void select()
        {
            fillTable(_DAO.select());
        }//select

        /// <summary>
        /// Przemieszcza indeks w tablicy danych o jedną pozycję.
        /// </summary>
        public void skip()
        {
            if (_count > 0)
            {
                _idx++;
                _eof = _idx >= _count;
            }
        }//skip

        /// <summary>
        /// Przemieszcza indeks w tablicy danych na pozycję pierwszą.
        /// </summary>
        public void top()
        {
            if (_count > 0)
            {
                _idx = 0;
                _eof = false;
            }
        }//top

        /// <summary>
        /// Zmienna logiczna osiągnięcia końca pliku.
        /// </summary>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        public DzialVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new DzialVO();
            }
        }//VO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        public int idx
        {
            set
            {
                if (value > -1 & value < _count)
                {
                    _idx = value;
                }
            }
        }//idx

        /// <summary>
        /// Sprawdza istnienie rekordu.
        /// </summary>
        /// <param name="Id">Nazwa Dzial.</param>
        /// <returns>Wynik logiczny sprawdzenia.</returns>
        public bool exists(String Nazwa)
        {
            foreach (DzialVO VOi in _VOs)
            {
                if (VOi.Nazwa == Nazwa) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Nazwa">Identyfikator Dzial do którego należy maszyna</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora Kategori.</returns>
        public int getIdx(string Nazwa)
        {
            int idx = -1;
            foreach (DzialVO VOi in _VOs)
            {
                idx++;
                if (VOi.Nazwa == Nazwa) return idx;
            }

            return -1;
        }//getIdx

    }//class DzialBUS

    /////////////////////////////////////////////////////////////////////////////////// Nazwa_op_maszyny
    /// <summary>
    /// Klasa wymiany danych z tabelą Nazwa_op_maszyny.
    /// </summary>
    public class Operator_maszynyVO
    {
        private int _Identyfikator = 0;
        private string _Nazwa_op_maszyny = string.Empty; //255
        private int _ID_dzial = -1; 
        private string _Nazwa_dzial = string.Empty; //255
        private string _Uprawnienie = string.Empty; //255
        private int _Rok = 0;
        private int _Mc = 0;
        private int _Dzien = 0;
        private string _Data_konca_upr = string.Empty; //255
        private string _Op_imie = string.Empty; //255
        private string _Op_nazwisko = string.Empty; //255
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Nazwa_op_maszyny
        /// </summary>
        public Operator_maszynyVO() { }
        public int Identyfikator
        {
            get { return _Identyfikator; }
            set { _Identyfikator = value; }
        }
        public string Nazwa_op_maszyny
        {
            get { return _Nazwa_op_maszyny; }
            set { _Nazwa_op_maszyny = value; }
        }
        public string Op_imie
        {
            get { return _Op_imie; }
            set { _Op_imie = value; }
        }
        public string Op_nazwisko
        {
            get { return _Op_nazwisko; }
            set { _Op_nazwisko = value; }
        }
        public int ID_dzial
        {
            get { return _ID_dzial; }
            set { _ID_dzial = value; }
        }
        public string Nazwa_dzial {
            get { return _Nazwa_dzial; }
            set { _Nazwa_dzial = value; }
        }
        public string Uprawnienie
        {
            get { return _Uprawnienie; }
            set { _Uprawnienie = value; }
        }
        public int Rok
        {
            get { return _Rok; }
            set { _Rok = value; }
        }
        public int Mc
        {
            get { return _Mc; }
            set { _Mc = value; }
        }
        public int Dzien
        {
            get { return _Dzien; }
            set { _Dzien = value; }
        }
        public string Data_konca_upr
        {
            get { return _Data_konca_upr; }
            set { _Data_konca_upr = value; }
        }
    }//class Operator_maszynyVO

    //Klasa dostępu (Data Access Object) do tabeli Nazwa_op_maszyny.
    public class Operator_maszynyDAO
    {
        private dbConnection _conn;
        public string _error = string.Empty;

        /// <constructor>
        /// Konstruktor.
        /// </constructor>
        public Operator_maszynyDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }//Operator_maszynyDAO

        //                                                 -------------------------------------> dowolne zapytanie z poziomu Form

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable selectQuery(string query)
        {
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//selectQuery


        /// <summary>
        ///  Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <returns>Tabela Nazwa_op_maszyny.</returns>
        public DataTable select()
        {
            string query = "SELECT * FROM Operator_maszyny;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="ID_op_maszyny"></param>
        /// <returns></returns>
        public DataTable select(int Identyfikator)
        {
            string query = "SELECT * FROM Operator_maszyny WHERE ID_op_maszyny = " + Identyfikator.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select po nazwie dzialu
         
        /// <summary>
        /// Wprowadza nowy rekord
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych (insert)</param>
        /// <returns>Wartość logiczna powodzenia operacji</returns>
        public bool insert(nsAccess2DB.Operator_maszynyVO VO)
        {
            string query = "INSERT INTO Operator_maszyny (Nazwa_op_maszyny, ID_dzial, Nazwa_dzial, Uprawnienie, Data_konca_upr, Rok, Mc, Dzien, Op_imie, Op_nazwisko)" +
                "VALUES (@Nazwa_op_maszyny, @ID_dzial, @Nazwa_dzial, @Uprawnienie, @Data_konca_upr, @Rok, @Mc, @Dzien, @Op_imie, @Op_nazwisko)";

            OleDbParameter[] parameters = new OleDbParameter[10];
            parameters[0] = new OleDbParameter("Nazwa_op_maszyny", OleDbType.VarChar, 255);
            parameters[0].Value = VO.Nazwa_op_maszyny;

            parameters[1] = new OleDbParameter("ID_dzial", OleDbType.Integer);
            parameters[1].Value = VO.ID_dzial;

            parameters[2] = new OleDbParameter("Nazwa_dzial", OleDbType.VarChar, 255);
            parameters[2].Value = VO.Nazwa_dzial;

            parameters[3] = new OleDbParameter("Uprawnienie", OleDbType.VarChar, 255);
            parameters[3].Value = VO.Uprawnienie;

            parameters[4] = new OleDbParameter("Data_konca_upr", OleDbType.VarChar, 255);
            parameters[4].Value = VO.Data_konca_upr;

            parameters[5] = new OleDbParameter("Rok", OleDbType.Integer);
            parameters[5].Value = VO.Rok;

            parameters[6] = new OleDbParameter("Mc", OleDbType.Integer);
            parameters[6].Value = VO.Mc;

            parameters[7] = new OleDbParameter("Dzien", OleDbType.Integer);
            parameters[7].Value = VO.Dzien;

            parameters[8] = new OleDbParameter("Op_imie", OleDbType.VarChar, 255);
            parameters[8].Value = VO.Op_imie;

            parameters[9] = new OleDbParameter("Op_nazwisko", OleDbType.VarChar, 255);
            parameters[9].Value = VO.Op_nazwisko;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//insert
        
       
        /// <summary>
        ///  Aktualizuje rekord z wyjątkiem ID.
        /// </summary>
        /// <param name="VO"></param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool update(nsAccess2DB.Operator_maszynyVO VO)
        {
           string query = "UPDATE Operator_maszyny SET Nazwa_op_maszyny = @Nazwa_op_maszyny, ID_dzial = @ID_dzial, Nazwa_dzial = @Nazwa_dzial, Uprawnienie = @Uprawnienie, " +
                "Data_konca_upr = @Data_konca_upr, Rok = @Rok, Mc = @Mc, Dzien = @Dzien " +
                "WHERE Identyfikator = " + VO.Identyfikator.ToString() + ";";
            
            OleDbParameter[] parameters = new OleDbParameter[10];

            parameters[0] = new OleDbParameter("Nazwa_op_maszyny", OleDbType.VarChar, 255);
            parameters[0].Value = VO.Nazwa_op_maszyny;

            parameters[1] = new OleDbParameter("ID_dzial", OleDbType.Integer);
            parameters[1].Value = VO.ID_dzial;

            parameters[2] = new OleDbParameter("Nazwa_dzial", OleDbType.VarChar, 255);
            parameters[2].Value = VO.Nazwa_dzial;

            parameters[3] = new OleDbParameter("Uprawnienie", OleDbType.VarChar, 255);
            parameters[3].Value = VO.Uprawnienie;

            parameters[4] = new OleDbParameter("Data_konca_upr", OleDbType.VarChar, 255);
            parameters[4].Value = VO.Data_konca_upr;

            parameters[5] = new OleDbParameter("Rok", OleDbType.Integer);
            parameters[5].Value = VO.Rok;

            parameters[6] = new OleDbParameter("Mc", OleDbType.Integer);
            parameters[6].Value = VO.Mc;

            parameters[7] = new OleDbParameter("Dzien", OleDbType.Integer);
            parameters[7].Value = VO.Dzien;

            parameters[8] = new OleDbParameter("Op_imie", OleDbType.VarChar, 255);
            parameters[8].Value = VO.Op_imie;

            parameters[9] = new OleDbParameter("Op_nazwisko", OleDbType.VarChar, 255);
            parameters[9].Value = VO.Op_nazwisko;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//update

        /// <summary>
        /// Usuwa rekord.
        /// </summary>
        /// <param name="Identyfikator">Identyfikator.</param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool delete(int Identyfikator)
        {
            string query = "DELETE * FROM Operator_maszyny WHERE Identyfikator = " + Identyfikator.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//delete

    }//class Operator_maszynyDAO
    
    //Warstwa operacji biznesowaych tabeli Nazwa_op_maszyny BUS.
    public class Operator_maszynyBUS
    {
        Operator_maszynyDAO _DAO;

        private Operator_maszynyVO[] _VOs = new Operator_maszynyVO[0];    //lista danych
        private Operator_maszynyVO _VOi = new Operator_maszynyVO();       //dane na pozycji _idx
        private int _idx = 0;                                             //indeks pozycji
        private bool _eof = false;                                        //wskaźnik końca pliku
        private int _count = 0;                                           //liczba pozycji

        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">ConnectionString.</param>
        public Operator_maszynyBUS(string connString)
        {
            _DAO = new Operator_maszynyDAO(connString);
            _error = _DAO._error;
        }//Operator_maszynyBUS

        /// <summary>
        /// Wypełnia tablicę pozycjami danych -------------------------------------> dowolne zapytanie z poziomu Form
        /// </summary>
        /// <param name="query"></param>
        public void selectQuery(string query)
        {
            fillTable(_DAO.selectQuery(query));
        }

        /// <summary>
        /// Wprowadza rekord tabeli.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        private bool insert(nsAccess2DB.Operator_maszynyVO VO)
        {
            bool b = _DAO.insert(VO);
            _error = _DAO._error;
            return b;
        }// insert

        /// <summary>
        /// Aktualizuje rekord z wyjątkiem numeru protokołu.
        /// </summary>
        /// <param name="VO">Obiekt wymiany.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        private bool update(nsAccess2DB.Operator_maszynyVO VO)
        {
            bool b = _DAO.update(VO);
            _error = _DAO._error;
            return b;
        }//update

        /// <summary>
        /// Dodaje lub aktualizuje rekord.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wynik powodzenia akcji.</returns>
       public bool write(nsAccess2DB.Operator_maszynyVO VO)
        {
            if (exists(VO.Identyfikator))
            {
                return _DAO.update(VO);
            }
            return _DAO.insert(VO);
       }//write
        
        /// <summary>
        /// Wypełnia tablice.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            Operator_maszynyVO VOi;
            _VOs = new Operator_maszynyVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                VOi = new Operator_maszynyVO();

                VOi.Identyfikator = int.Parse(dr["Identyfikator"].ToString());
                VOi.Nazwa_op_maszyny = dr["Nazwa_op_maszyny"].ToString();
                VOi.ID_dzial = int.Parse(dr["ID_dzial"].ToString());
                VOi.Nazwa_dzial = dr["Nazwa_dzial"].ToString();
                VOi.Uprawnienie = dr["Uprawnienie"].ToString();
                try
                {
                    VOi.Data_konca_upr = dr["Data_konca_upr"].ToString();
                }
                catch { }
                VOi.Dzien = int.Parse(dr["Dzien"].ToString());
                VOi.Mc = int.Parse(dr["Mc"].ToString());
                VOi.Rok = int.Parse(dr["Rok"].ToString());
                VOi.Op_imie = dr["Op_imie"].ToString();
                VOi.Op_nazwisko = dr["Op_nazwisko"].ToString();

                 Array.Resize(ref _VOs, _VOs.Length + 1);
                _VOs[_VOs.Length - 1] = VOi;
            }

            _eof = _VOs.Length == 0;
            _count = _VOs.Length;
            if (_count > 0)
                _idx = 0;
            else
            {
                _idx = -1;
                _eof = true;
            }
        }//fillTable
        
        /// <summary>
        /// Wypełnia tablice pozycjami danych .
        /// </summary>
        public void select()
        {
            fillTable(_DAO.select());
        }//select

        /// <summary>
        /// Wypełnia tablicę.
        /// </summary>
        /// <param name="Identyfikator"></param>
        public void select(int Identyfikator)
        {
            fillTable(_DAO.select(Identyfikator));
        }//select

        /// <summary>
        /// Usuwa rekord.
        /// </summary>
        /// <param name="ID_op_maszyny"></param>
        public void delete(int ID_op_maszyny)
        {
            _DAO.delete(ID_op_maszyny);
        }
        
        /// <summary>
        /// Przemieszcza indeks w tablicy danych o jedną pozycję.
        /// </summary>
        public void skip()
        {
            if (_count > 0)
            {
                _idx++;
                _eof = _idx >= _count;
            }
        }//skip

        /// <summary>
        /// Przemieszcza indeks w tablicy danych na pozycję pierwszą.
        /// </summary>
        public void top()
        {
            if (_count > 0)
            {
                _idx = 0;
                _eof = false;
            }
        }//top

        /// <summary>
        /// Zmienna logiczna osiągnięcia końca pliku.
        /// </summary>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        public Operator_maszynyVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new Operator_maszynyVO();
            }
        }//Operator_maszynyVO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        public int idx
        {
            set
            {
                if (value > -1 & value < _count)
                {
                    _idx = value;
                }
            }
            get
            {
                return _idx;
            }
        }//idx

        /// <summary>
        /// Sprawdza istnienie rekordu.
        /// </summary>
        /// <param name="Identyfikator">ID operatora maszyny</param>
        /// <returns>Wynik logiczny sprawdzenia.</returns>
        public bool exists(int Identyfikator)
        {
            foreach (Operator_maszynyVO VOi in _VOs)
            {
                if (VOi.Identyfikator == Identyfikator) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Identyfikator"></param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora.</returns>
        public int getIdx(int Identyfikator)
        {
            int idx = -1;
            foreach (Operator_maszynyVO VOi in _VOs)
            {
                idx++;
                if (VOi.Identyfikator == Identyfikator) return idx;
            }
            return -1;
        }//getIdx

        public Operator_maszynyVO GetVO(int Identyfikator)
        {
            foreach (nsAccess2DB.Operator_maszynyVO VO in _VOs)
            {
                if (VO.Identyfikator == Identyfikator) return VO;
            }
            return new Operator_maszynyVO();
        }// getVO

        
    }//class Operator_maszynyBUS

    //////////////////////////////////////////////////////////////////////         Operator_maszyny_Maszyny
    /// <summary>
    /// Klasa wymiany danych z tabelą Operator_maszyny_Maszyny.
    /// </summary>
    public class Operator_maszyny_MaszynyVO
    {
        private int _ID_maszyny = -1;
        private int _ID_op_maszyny = -1;
     
        
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Operator_maszyny_Maszyny
        /// </summary>
        public Operator_maszyny_MaszynyVO() { }

        public int ID_op_maszyny
        {
            get { return _ID_op_maszyny; }
            set { _ID_op_maszyny = value; }
        }
        public int ID_maszyny
        {
            get { return _ID_maszyny; }
            set { _ID_maszyny = value; }
        }
    }//class Operator_maszyny_MaszynyVO

    //Klasa dostępu (Data Access Object) do tabeli Operator_maszyny_Maszyny.
    public class Operator_maszyny_MaszynyDAO
    {
        private dbConnection _conn;
        public string _error = string.Empty;

            /// <constructor>
            /// Konstruktor.
            /// </constructor>
        public Operator_maszyny_MaszynyDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }//Operator_maszyny_MaszynyDAO

        //                                                 -------------------------------------> dowolne zapytanie z poziomu Form

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable selectQuery(string query)
        {
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//selectQuery


        /// <summary>
        ///  Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <returns>Tabela Operator_maszyny_MaszynyDAO.</returns>
        public DataTable select()
        {
            string query = "SELECT Operator_maszyny_Maszyny.ID_maszyny, Operator_maszyny_Maszyny.ID_op_maszyny, Operator_maszyny.Op_nazwisko, Operator_maszyny.Op_imie, Maszyny.Nazwa FROM Operator_maszyny RIGHT JOIN(Maszyny INNER JOIN Operator_maszyny_Maszyny ON Maszyny.[Identyfikator] = Operator_maszyny_Maszyny.[ID_maszyny]) ON Operator_maszyny.[Identyfikator] = Operator_maszyny_Maszyny.[ID_op_maszyny];";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select 

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="ID_op_maszyny"></param>
        /// <returns></returns>
        public DataTable select(int ID_op_maszyny)
        {
            string query = "SELECT Operator_maszyny_Maszyny.ID_maszyny, Operator_maszyny_Maszyny.ID_op_maszyny, Operator_maszyny.Op_nazwisko, Operator_maszyny.Op_imie, Maszyny.Nazwa FROM Operator_maszyny RIGHT JOIN(Maszyny INNER JOIN Operator_maszyny_Maszyny ON Maszyny.[Identyfikator] = Operator_maszyny_Maszyny.[ID_maszyny]) ON Operator_maszyny.[Identyfikator] = Operator_maszyny_Maszyny.[ID_op_maszyny] WHERE ID_op_maszyny = " + ID_op_maszyny.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="ID_maszyny"></param>
        /// <param name="ID_op_maszyny"></param>
        /// <returns></returns>
        public DataTable select(int ID_maszyny, int ID_op_maszyny)
        {
            string query = "SELECT Operator_maszyny_Maszyny.ID_maszyny, Operator_maszyny_Maszyny.ID_op_maszyny, Operator_maszyny.Op_nazwisko, Operator_maszyny.Op_imie, Maszyny.Nazwa FROM Operator_maszyny RIGHT JOIN(Maszyny INNER JOIN Operator_maszyny_Maszyny ON Maszyny.[Identyfikator] = Operator_maszyny_Maszyny.[ID_maszyny]) ON Operator_maszyny.[Identyfikator] = Operator_maszyny_Maszyny.[ID_op_maszyny] WHERE ID_maszyny = " + ID_maszyny.ToString() + " AND ID_op_maszyny = " + ID_op_maszyny.ToString() + "; ";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

       
        /// <summary>
        /// Wprowadza nowy rekord
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych (insert)</param>
        /// <returns>Wartość logiczna powodzenia operacji</returns>
        public bool insert(nsAccess2DB.Operator_maszyny_MaszynyVO VO)
        {
            string query = "INSERT INTO Operator_maszyny_Maszyny (ID_op_maszyny, ID_maszyny)" +
                    "VALUES (@ID_op_maszyny, @ID_maszyny)";
            OleDbParameter[] parameters = new OleDbParameter[2];
            parameters[0] = new OleDbParameter("ID_op_maszyny", OleDbType.Integer);
            parameters[0].Value = VO.ID_op_maszyny;

            parameters[1] = new OleDbParameter("ID_maszyny", OleDbType.Integer);
            parameters[1].Value = VO.ID_maszyny;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//insert

            /// <summary>
            ///  Aktualizuje rekord z wyjątkiem ID.
            /// </summary>
            /// <param name="VO"></param>
            /// <returns>Wartość logiczna powodzenia operacji.</returns>
         public bool update(nsAccess2DB.Operator_maszyny_MaszynyVO VO)
         {
            string query = "UPDATE Operator_maszyny_Maszyny SET ID_op_maszyny = @ID_op_maszyny, ID_maszyny = @ID_maszyny WHERE ID_op_maszyny = " + VO.ID_op_maszyny.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[2];
            parameters[0] = new OleDbParameter("ID_op_maszyny", OleDbType.Integer);
            parameters[0].Value = VO.ID_op_maszyny;

            parameters[1] = new OleDbParameter("ID_maszyny", OleDbType.Integer);
            parameters[1].Value = VO.ID_maszyny;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
         }//update

        public bool delete(int ID_maszyny)
        {
            string query = "DELETE * FROM Operator_maszyny_Maszyny WHERE ID_maszyny = " + ID_maszyny.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//delete

        /// <summary>
        /// usuwa rekord
        /// </summary>
        /// <param name="ID_op_maszyny"></param>
        /// <param name="ID_maszyny"></param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool delete(int ID_op_maszyny, int ID_maszyny)
         {
            string query = "DELETE * FROM Operator_maszyny_Maszyny WHERE ID_op_maszyny = " + ID_op_maszyny.ToString() + "AND ID_maszyny = " + ID_maszyny.ToString() +";";

            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//delete
      
    }//class Operator_maszyny_MaszynyDAO

        //Warstwa operacji biznesowaych tabeli Operator_maszyny_Maszyny BUS.
    public class Operator_maszyny_MaszynyBUS
    {
        Operator_maszyny_MaszynyDAO _DAO;

        private Operator_maszyny_MaszynyVO[] _VOs = new Operator_maszyny_MaszynyVO[0];    //lista danych
        private Operator_maszyny_MaszynyVO _VOi = new Operator_maszyny_MaszynyVO();       //dane na pozycji _idx
        private int _idx = 0;                                             //indeks pozycji
        private bool _eof = false;                                        //wskaźnik końca pliku
        private int _count = 0;                                           //liczba pozycji

        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString"></param>
        public Operator_maszyny_MaszynyBUS(string connString)
        {
            _DAO = new Operator_maszyny_MaszynyDAO(connString);
            _error = _DAO._error;
        }//Operator_maszyny_MaszynyBUS

        /// <summary>
        /// Wypełnia tablicę.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            Operator_maszyny_MaszynyVO VOi;
            _VOs = new Operator_maszyny_MaszynyVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);

                VOi = new Operator_maszyny_MaszynyVO();
                VOi.ID_op_maszyny = int.Parse(dr["ID_op_maszyny"].ToString());
                VOi.ID_maszyny = int.Parse(dr["ID_maszyny"].ToString());
                
                _VOs[_VOs.Length - 1] = VOi;
            }

            _eof = _VOs.Length == 0;
            _count = _VOs.Length;
            if (_count > 0)
                _idx = 0;
            else
            {
                _idx = -1;
                _eof = true;
            }
        }//fillTable

        /// <summary>
        /// Wypełnia tablice pozycjami danych.
        /// </summary>
        public void select()
        {
            fillTable(_DAO.select());
        }//select

        /// <summary>
        /// Wypełnia tablice pozycjami danych.
        /// </summary>
        /// <param name="ID_op_maszyny"></param>
        public void select(int ID_op_maszyny)
        {
            fillTable(_DAO.select(ID_op_maszyny));
        }//select

        /// <summary>
        /// Wypełnia tablice pozycjami danych.
        /// </summary>
        /// <param name="ID_maszyny"></param>
        /// <param name="ID_op_maszyny"></param>
        public void select(int ID_maszyny, int ID_op_maszyny)
        {
            fillTable(_DAO.select(ID_maszyny, ID_op_maszyny));
        }//select





        /// <summary>
        /// Wypełnia tablicę pozycjami danych -------------------------------------> dowolne zapytanie z poziomu Form
        /// </summary>
        /// <param name="query"></param>
        public void selectQuery(string query)
        {
            fillTable(_DAO.selectQuery(query));
        }

        /// <summary>
        /// Wprowadza rekord do tabeli.
        /// </summary>
        /// <param name="ID_maszyny"></param>
        /// <param name="ID_op_maszyny"></param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool insert(int ID_maszyny, int ID_op_maszyny)
        {
            Operator_maszyny_MaszynyVO VO = new Operator_maszyny_MaszynyVO();
            VO.ID_maszyny = ID_maszyny;
            VO.ID_op_maszyny = ID_op_maszyny;
            
            add(VO, ref _VOs);

            return _DAO.insert(VO);
        }//insert

        /// <summary>
        /// Aktualizuje rekord z wyjątkiem numeru protokołu.
        /// </summary>
        /// <param name="VO"></param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        private bool update(nsAccess2DB.Operator_maszyny_MaszynyVO VO)
        {
            bool b = _DAO.update(VO);
            _error = _DAO._error;
            return b;
        }//update

        /// <summary>
        /// Dodaje pozycję do tabeli.
        /// </summary>
        /// <param name="VO"></param>
        /// <param name="VOs"></param>
        private void add(Operator_maszyny_MaszynyVO VO, ref Operator_maszyny_MaszynyVO[] VOs)
        {
            Array.Resize(ref VOs, VOs.Length + 1);
            VOs[_VOs.Length - 1] = VO;
        }//add

        /// <summary>
        /// Usuwa z tabeli pozycję o wskazanych parametrach.
        /// </summary>
        /// <param name="ID_maszyny"></param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        public bool delete(int ID_maszyny)
        {
            return _DAO.delete(ID_maszyny);
        }// delete

        /// <summary>
        /// Usuwa z tabeli pozycję o wskazanych parametrach.
        /// </summary>
        /// <param name="ID_maszyny"></param>
        /// <param name="ID_op_maszyny"></param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        public bool delete(int ID_maszyny, int ID_op_maszyny)
        {
            return _DAO.delete(ID_maszyny, ID_op_maszyny);
        }// delete

        // <summary>
        /// Przemieszcza indeks w tablicy danych o jedną pozycję.
        /// </summary>
        public void skip()
        {
            if (_count > 0)
            {
                _idx++;
                _eof = _idx >= _count;
            }
        }//skip

        /// <summary>
        /// Przemieszcza indeks w tablicy danych na pozycję pierwszą.
        /// </summary>
        public void top()
        {
            if (_count > 0)
            {
                _idx = 0;
                _eof = false;
            }
        }//top

        /// <summary>
        /// Zmienna logiczna osiągnięcia końca pliku.
        /// </summary>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        public int count
        {
            get { return _count; }
        }
        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        public Operator_maszyny_MaszynyVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new Operator_maszyny_MaszynyVO();
            }
        }//VO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        public int idx
        {
            set
            {
                if (value > -1 & value < _count)
                {
                    _idx = value;
                }
            }
        }//idx

        /// <summary>
        /// Sprawdza istnienie pozycji w tabeli uzyskanej po ostanim poleceniu select.
        /// </summary>
        /// <param name="ID_maszyny"></param>
        /// <param name="ID_op_maszyny"></param>
        /// <returns>Wartość logiczna istnienia pozycji.</returns>
        public bool exists(int ID_maszyny, int ID_op_maszyny)
        {
            foreach (Operator_maszyny_MaszynyVO VOi in _VOs)
            {
                if (VOi.ID_maszyny == ID_maszyny & VOi.ID_op_maszyny == ID_op_maszyny) return true;
            }
            return false;
        }//exists

    }//class Operator_maszyny_MaszynyBUS
    


        /////////////////////////////////////////////////////////////////////////////////// Osoba_zarzadzajaca 
        /// <summary>
        /// Klasa wymiany danych z tabelą Osoba_zarzadzajaca.
        /// </summary>
        public class Osoba_zarzadzajacaVO
        {
            private int _Identyfikator = 0;
            private string _Nazwa_os_zarzadzajaca = string.Empty; //255
            private int _ID_dzial = 0;
            private string _Nazwa_dzial = string.Empty; //255
            private string _Os_zarz_nazwisko = string.Empty; //255
            private string _Os_zarz_imie = string.Empty; //255

            /// <summary>
            /// Konstruktor wymiany danych z tabelą Osoba_zarzadzajaca
            /// </summary>
            public Osoba_zarzadzajacaVO() { }

            public int Identyfikator
            {
                get { return _Identyfikator; }
                set { _Identyfikator = value; }
            }
            public string Nazwa_os_zarzadzajaca
        {
                get { return _Nazwa_os_zarzadzajaca; }
                set { _Nazwa_os_zarzadzajaca = value; }
            }
            public int ID_dzial
            {
                get { return _ID_dzial; }
                set { _ID_dzial = value; }
            }
            public string Nazwa_dzial
            {
                get { return _Nazwa_dzial; }
                set { _Nazwa_dzial = value; }
            }
            public string Os_zarz_nazwisko
            {
                get { return _Os_zarz_nazwisko; }
                set { _Os_zarz_nazwisko = value; }
            }
            public string Os_zarz_imie
            {
                get { return _Os_zarz_imie; }
                set { _Os_zarz_imie = value; }
            }
        }//class Osoba_zarzadzajacaVO

        //Klasa dostępu (Data Access Object) do tabeli Osoba_zarzadzajaca.
        public class Osoba_zarzadzajacaDAO
        {
            private dbConnection _conn;
            public string _error = string.Empty;

            /// <constructor>
            /// Konstruktor.
            /// </constructor>
            public Osoba_zarzadzajacaDAO(string connString)
            {
                _conn = new dbConnection(connString);
                _error = _conn._error;
            }//Osoba_zarzadzajacaDAO


        //                                                 -------------------------------------> dowolne zapytanie z poziomu Form

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable selectQuery(string query)
        {
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//selectQuery


         /// <summary>
         /// Zwraca tabelę spełniającą wartości parametrów.
         /// </summary>
        public DataTable select()
            {
                string query = "SELECT * FROM Osoba_zarzadzajaca;";

                OleDbParameter[] parameters = new OleDbParameter[0];
                DataTable dt = _conn.executeSelectQuery(query, parameters);
                _error = _conn._error;
                return dt;
            }//select
           
            /// <summary>
            /// Wprowadza nowy rekord do tabeli osoba zatrządzająca.
            /// </summary>
            /// <param name="VO">Obiekt wymiany danych.</param>
            /// <returns>Wartość logiczna powodzenia operacji.</returns>
            public bool insert(nsAccess2DB.Osoba_zarzadzajacaVO VO)
            {
                string query = "INSERT INTO Osoba_zarzadzajaca (Identyfikator, Nazwa_os_zarzadzajaca, ID_dzial, Nazwa_dzial, Os_zarz_nazwisko, Os_zarz_imie)" +
                    " VALUES (@Identyfikator, Nazwa_os_zarzadzajaca, @ID_dzial, @Nazwa_dzial, @Os_zarz_nazwisko, @Os_zarz_imie)";

                OleDbParameter[] parameters = new OleDbParameter[6];

                parameters[0] = new OleDbParameter("Identyfikator", OleDbType.Integer);
                parameters[0].Value = VO.Identyfikator;

                parameters[1] = new OleDbParameter("Nazwa_os_zarzadzajaca", OleDbType.VarChar, 255);
                parameters[1].Value = VO.Nazwa_os_zarzadzajaca;

                parameters[2] = new OleDbParameter("ID_dzial", OleDbType.Integer);
                parameters[2].Value = VO.ID_dzial;

                parameters[3] = new OleDbParameter("Nazwa_dzial", OleDbType.VarChar, 255);
                parameters[3].Value = VO.Nazwa_dzial;

                parameters[4] = new OleDbParameter("Os_zarz_nazwisko", OleDbType.VarChar, 255);
                parameters[4].Value = VO.Os_zarz_nazwisko;

                parameters[5] = new OleDbParameter("Os_zarz_imie", OleDbType.Integer);
                parameters[5].Value = VO.Os_zarz_imie;

                bool b = _conn.executeInsertQuery(query, parameters);
                _error = _conn._error;
                return b;
            }//insert

        }//class Osoba_zarzadzajacaDAO

        //Warstwa operacji biznesowaych tabeli Osoba_zarzadzajaca     ->    BUS.
        public class Osoba_zarzadzajacaBUS
        {
            Osoba_zarzadzajacaDAO _DAO;

            private Osoba_zarzadzajacaVO[] _VOs = new Osoba_zarzadzajacaVO[0];    //lista danych
            private Osoba_zarzadzajacaVO _VOi = new Osoba_zarzadzajacaVO();       //dane na pozycji _idx
            private int _idx = 0;                       //indeks pozycji
            private bool _eof = false;                  //wskaźnik końca pliku
            private int _count = 0;                     //liczba pozycji

            public string _error = string.Empty;

            /// <summary>
            /// Konstruktor.
            /// </summary>
            /// <param name="connString">ConnectionString.</param>
            public Osoba_zarzadzajacaBUS(string connString)
            {
                _DAO = new Osoba_zarzadzajacaDAO(connString);
                _error = _DAO._error;
            }//Osoba_zarzadzajacaBUS

            /// <summary>
            /// Wypełnia tablice.
            /// </summary>
            /// <param name="dt">Tabela danych.</param>
            private void fillTable(DataTable dt)
            {
                Osoba_zarzadzajacaVO VOi;
                _VOs = new Osoba_zarzadzajacaVO[0];

                foreach (DataRow dr in dt.Rows)
                {
                    Array.Resize(ref _VOs, _VOs.Length + 1);

                    VOi = new Osoba_zarzadzajacaVO();

                    VOi.Nazwa_os_zarzadzajaca = dr["Nazwa_os_zarzadzajaca"].ToString();

                    _VOs[_VOs.Length - 1] = VOi;
                }

                _eof = _VOs.Length == 0;
                _count = _VOs.Length;
                if (_count > 0)
                    _idx = 0;
                else
                {
                    _idx = -1;
                    _eof = true;
                }

            }//fillTable

            /// <summary>
            /// Wypełnia tablice danych pozycjami.
            /// </summary>
            public void select()
            {
                fillTable(_DAO.select());
            }//select

            /// <summary>
            /// Przemieszcza indeks w tablicy danych o jedną pozycję.
            /// </summary>
            public void skip()
            {
                if (_count > 0)
                {
                    _idx++;
                    _eof = _idx >= _count;
                }
            }//skip

            /// <summary>
            /// Przemieszcza indeks w tablicy danych na pozycję pierwszą.
            /// </summary>
            public void top()
            {
                if (_count > 0)
                {
                    _idx = 0;
                    _eof = false;
                }
            }//top

            /// <summary>
            /// Zmienna logiczna osiągnięcia końca pliku.
            /// </summary>
            public bool eof
            {
                get { return _eof; }
            }

            /// <summary>
            /// Zwraca liczbę pozycji tablicy.
            /// </summary>
            public int count
            {
                get { return _count; }
            }

            /// <summary>
            /// Zwraca daną okrśloną wskaźnikiem pozycji.
            /// </summary>
            public Osoba_zarzadzajacaVO VO
            {
                get
                {
                    if (_idx > -1 & _idx < _count)
                    {
                        return _VOi = _VOs[_idx];
                    }
                    return new Osoba_zarzadzajacaVO();
                }
            }//VO

            /// <summary>
            /// Ustawia wskaźnik pozycji.
            /// </summary>
            public int idx
            {
                set
                {
                    if (value > -1 & value < _count)
                    {
                        _idx = value;
                    }
                }
            }//idx

            /// <summary>
            /// Sprawdza istnienie rekordu.
            /// </summary>
            /// <param name="Id">Osoba_zarzadzajaca.</param>
            /// <returns>Wynik logiczny sprawdzenia.</returns>
            public bool exists(String Nazwa_os_zarzadzajaca)
            {
                foreach (Osoba_zarzadzajacaVO VOi in _VOs)
                {
                    if (VOi.Nazwa_os_zarzadzajaca == Nazwa_os_zarzadzajaca) return true;
                }
                return false;
            }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Nazwa_os_zarzadzajaca">Identyfikator Osoba_zarzadzajaca maszyną</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora Osoba_zarzadzajaca.</returns>
        public int getIdx(string Nazwa_os_zarzadzajaca)
            {
                int idx = -1;
                foreach (Osoba_zarzadzajacaVO VOi in _VOs)
                {
                    idx++;
                    if (VOi.Nazwa_os_zarzadzajaca == Nazwa_os_zarzadzajaca) return idx;
                }

                return -1;
            }//getIdx

        }//class Osoba_zarzadzajacaBUS

    ///////////////////////////////////////////////////////////////// klasa wymiany danych z tabelą Materialy

    // ---------------------------------------------------------- MateriałyVO
    /// <summary>
    /// Klasa wymiany danych z tabelą Materialy
    /// </summary>
    public class MaterialyVO
    {
        private int _Identyfikator = -1; 
        private string _Nazwa_mat = string.Empty; //255
        private string _Typ_mat = string.Empty;
        private string _Rodzaj_mat = string.Empty;
        private string _Jednostka_miar_mat = string.Empty;
        private string _Dostawca_mat = string.Empty;
        private int _Stan_mat = 0; // liczba
        private int _Zuzycie_mat = 0; // liczba
        private int _Odpad_mat = 0; // liczba
        private int _Stan_min_mat = 0;// liczba
        private int _Zapotrzebowanie_mat = 0;// liczba
        private int _Stan_mag_po_mat = 0;// liczba
        
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Materialy
        /// </summary>
        public MaterialyVO() { }

        // gettery i settery
        public int Identyfikator
        {
            get { return _Identyfikator; }
            set { _Identyfikator = value; }
        }
        public string Nazwa_mat
        {
            get { return _Nazwa_mat; }
            set { _Nazwa_mat = value; }
        }
        public string Typ_mat
        {
            get { return _Typ_mat; }
            set { _Typ_mat = value; }
        }
        public string Rodzaj_mat
        {
            get { return _Rodzaj_mat; }
            set { _Rodzaj_mat = value; }
        }
        public string Jednostka_miar_mat
        {
            get { return _Jednostka_miar_mat; }
            set { _Jednostka_miar_mat = value; }
        }
        public string Dostawca_mat
        {
            get { return _Dostawca_mat; }
            set { _Dostawca_mat = value; }
        }
        public int Stan_mat
        {
            get { return _Stan_mat; }
            set { _Stan_mat = value; }
        }
        public int Zuzycie_mat
        {
            get { return _Zuzycie_mat; }
            set { _Zuzycie_mat = value; }
        }
        public int Odpad_mat
        {
            get { return _Odpad_mat; }
            set { _Odpad_mat = value; }
        }
        public int Stan_min_mat
        {
            get { return _Stan_min_mat; }
            set { _Stan_min_mat = value; }
        }
        public int Zapotrzebowanie_mat
        {
            get { return _Zapotrzebowanie_mat; }
            set { _Zapotrzebowanie_mat = value; }
        }
        public int Stan_mag_po_mat
        {
            get { return _Stan_mag_po_mat; }
            set { _Stan_mag_po_mat = value; }
        }
    } //class MaterialyVO

    // --------------------------------------------klasa dostępu (Data Access Object) do tabeli Materiały DAO
    public class MaterialyDAO
    {
        private dbConnection _conn;
        public string _error = string.Empty;

        ///<construktor>
        ///Konstruktor
        ///</construktor>
        public MaterialyDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        } //konstruktor MaterialyDAO

        public DataTable selectQuery(string query)
        {
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//selectQuery

        /// <summary>
        /// Zwraca tabelę wszystkich Materiałów.
        /// </summary>
        /// <returns>Tabela Materiałów.</returns>
        public DataTable select()
        {
            string query = "SELECT * FROM Materialy ORDER BY Nazwa_mat ASC";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Zwraca tabelę wszystkich Materiałów po ID.
        /// </summary>
        /// <returns>Tabela Materiałów.</returns>
        public DataTable select(int Identyfikator)
        {
            string query = "SELECT * FROM Materialy WHERE Identyfikator = " + Identyfikator.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select po ID Materialów

        /// <summary>
        /// Wprowadza nowy rekord.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool insert(nsAccess2DB.MaterialyVO VO)
        {
            string query = "INSERT INTO Materialy(Nazwa_mat, Typ_mat, Rodzaj_mat, Jednostka_miar_mat, Dostawca_mat, Stan_mat, Zuzycie_mat, Odpad_mat, Stan_min_mat, Zapotrzebowanie_mat, Stan_mag_po_mat) VALUES (@Nazwa_mat, @Typ_mat, @Rodzaj_mat, @Jednostka_miar_mat, @Dostawca_mat, @Stan_mat, @Zuzycie_mat, @Odpad_mat, @Stan_min_mat, @Zapotrzebowanie_mat, @Stan_mag_po_mat);";

            OleDbParameter[] parameters = new OleDbParameter[11];
            parameters[0] = new OleDbParameter("Nazwa_mat", OleDbType.VarChar, 255);
            parameters[0].Value = VO.Nazwa_mat;

            parameters[1] = new OleDbParameter("Typ_mat", OleDbType.VarChar, 255);
            parameters[1].Value = VO.Typ_mat;

            parameters[2] = new OleDbParameter("Rodzaj_mat", OleDbType.VarChar, 255);
            parameters[2].Value = VO.Rodzaj_mat;

            parameters[3] = new OleDbParameter("Jednostka_miar_mat", OleDbType.VarChar, 100);
            parameters[3].Value = VO.Jednostka_miar_mat;

            parameters[4] = new OleDbParameter("Dostawca_mat", OleDbType.VarChar, 255);
            parameters[4].Value = VO.Dostawca_mat;

            parameters[5] = new OleDbParameter("Stan_mat", OleDbType.VarChar, 255);
            parameters[5].Value = VO.Stan_mat;

            parameters[6] = new OleDbParameter("Zuzycie_mat", OleDbType.VarChar, 255);
            parameters[6].Value = VO.Zuzycie_mat;

            parameters[7] = new OleDbParameter("Odpad_mat", OleDbType.VarChar, 255);
            parameters[7].Value = VO.Odpad_mat;

            parameters[8] = new OleDbParameter("Stan_min_mat", OleDbType.Integer);
            parameters[8].Value = VO.Stan_min_mat;

            parameters[9] = new OleDbParameter("Zapotrzebowanie_mat", OleDbType.VarChar, 255);
            parameters[9].Value = VO.Zapotrzebowanie_mat;

            parameters[10] = new OleDbParameter("Stan_mag_po_mat", OleDbType.VarChar, 255);
            parameters[10].Value = VO.Stan_mag_po_mat;
            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//insert

        /// <summary>
        /// Aktualizuje rekord z wyjątkiem Identyfikatora
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool update(nsAccess2DB.MaterialyVO VO)
        {
            string query = "UPDATE Materialy SET Nazwa_mat = @Nazwa_mat, Typ_mat = @Typ_mat, Rodzaj_mat = @Rodzaj_mat, Jednostka_miar_mat = @Jednostka_miar_mat, Dostawca_mat = @Dostawca_mat, Stan_mat = @Stan_mat, Zuzycie_mat = @Zuzycie_mat, Odpad_mat = @Odpad_mat, Stan_min_mat = @Stan_min_mat, Zapotrzebowanie_mat = @Zapotrzebowanie_mat, Stan_mag_po_mat = @Stan_mag_po_mat WHERE Identyfikator = " + VO.Identyfikator.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[11];
            parameters[0] = new OleDbParameter("Nazwa_mat", OleDbType.VarChar, 255);
            parameters[0].Value = VO.Nazwa_mat;

            parameters[1] = new OleDbParameter("Typ_mat", OleDbType.VarChar, 255);
            parameters[1].Value = VO.Typ_mat;

            parameters[2] = new OleDbParameter("Rodzaj_mat", OleDbType.VarChar, 255);
            parameters[2].Value = VO.Rodzaj_mat;

            parameters[3] = new OleDbParameter("Jednostka_miar_mat", OleDbType.VarChar, 100);
            parameters[3].Value = VO.Jednostka_miar_mat;

            parameters[4] = new OleDbParameter("Dostawca_mat", OleDbType.VarChar, 255);
            parameters[4].Value = VO.Dostawca_mat;

            parameters[5] = new OleDbParameter("Stan_mat", OleDbType.VarChar, 255);
            parameters[5].Value = VO.Stan_mat;

            parameters[6] = new OleDbParameter("Zuzycie_mat", OleDbType.VarChar, 255);
            parameters[6].Value = VO.Zuzycie_mat;

            parameters[7] = new OleDbParameter("Odpad_mat", OleDbType.VarChar, 255);
            parameters[7].Value = VO.Odpad_mat;

            parameters[8] = new OleDbParameter("Stan_min_mat", OleDbType.Integer);
            parameters[8].Value = VO.Stan_min_mat;

            parameters[9] = new OleDbParameter("Zapotrzebowanie_mat", OleDbType.VarChar, 255);
            parameters[9].Value = VO.Zapotrzebowanie_mat;

            parameters[10] = new OleDbParameter("Stan_mag_po_mat", OleDbType.VarChar, 255);
            parameters[10].Value = VO.Stan_mag_po_mat;
            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//update

        public bool delete(int Identyfikator)
        {
            string query = "DELETE * FROM Materialy WHERE Identyfikator = " + Identyfikator.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//delete
    }//clasa MaterialyDAO

    // ---------------------------------------------------------> BUS - warstawa operacji biznesowych tabeli Materialy
    public class MaterialyBUS
    {
        MaterialyDAO _DAO;
        private MaterialyVO[] _VOs = new MaterialyVO[0];//lista danych
        private MaterialyVO _VOi = new MaterialyVO();        //dane na pozycji _idx
        private int _idx = 0;                           //indeks pozycji
        private bool _eof = false;                      //wskaźnik końca pliku
        private int _count = 0;                         //liczba pozycji
        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString"></param>
        public MaterialyBUS(string connString)
        {
        _DAO = new MaterialyDAO(connString);
        _error = _DAO._error;
        }// konstruktor MaterailyBUS

        /// <summary>
        /// Wprowadza rekord do tabeli.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        private bool insert(nsAccess2DB.MaterialyVO VO)
        {
            bool b = _DAO.insert(VO);
            _error = _DAO._error;
            return b;
        }//insert

        /// <summary>
        /// Aktualizuje rekord z wyjątkiem ID czynności.
        /// </summary>
        /// <param name="VO"></param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        private bool update(nsAccess2DB.MaterialyVO VO)
        {
            bool b = _DAO.update(VO);
            _error = _DAO._error;
            return b;
        }// update

        /// <summary>
        /// Dodaje lub aktualizuje rekord.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wynik powodzenia akcji.</returns>
        public bool write(nsAccess2DB.MaterialyVO VO)
        {
            if (exists(VO.Identyfikator))
            {
                return _DAO.update(VO);
            }
            return _DAO.insert(VO);
        }//write

        /// <summary>
        /// Wypełnia tablice.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            MaterialyVO VOi;
            _VOs = new MaterialyVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);

                VOi = new MaterialyVO();

                VOi.Nazwa_mat = dr["Nazwa_mat"].ToString();
                VOi.Typ_mat = dr["Typ_mat"].ToString();
                VOi.Rodzaj_mat = dr["Rodzaj_mat"].ToString();
                VOi.Jednostka_miar_mat = dr["Jednostka_miar_mat"].ToString();
                VOi.Dostawca_mat = dr["Dostawca_mat"].ToString();
                VOi.Stan_mat = int.Parse (dr["Stan_mat"].ToString());
                VOi.Zuzycie_mat = int.Parse(dr["Zuzycie_mat"].ToString());
                VOi.Odpad_mat = int.Parse(dr["Odpad_mat"].ToString());
                VOi.Stan_min_mat = int.Parse(dr["Stan_min_mat"].ToString());
                VOi.Zapotrzebowanie_mat = int.Parse(dr["Zapotrzebowanie_mat "].ToString());
                VOi.Stan_mag_po_mat = int.Parse(dr["Stan_mag_po_mat"].ToString());

                _VOs[_VOs.Length - 1] = VOi;
            }
            _eof = _VOs.Length == 0;
            _count = _VOs.Length;
            if (_count > 0)
                _idx = 0;
            else
            {
                _idx = -1;
                _eof = true;
            }
        }//fillTable

        /// <summary>
        /// Wypełnia tablice danych pozycjami.
        /// </summary>
        public void select()
        {
            fillTable(_DAO.select());
        }//select


        /// <summary>
        /// Wypełnia tablice danych pozycjami.
        /// </summary>
        /// <param name="Identyfikator">ID maszyny.</param>
        public void select(int Identyfikator)
        {
            fillTable(_DAO.select(Identyfikator));
        }//select

        public bool delete(int Identyfikator)
        {
            return _DAO.delete(Identyfikator);
        }

        /// <summary>
        /// Przemieszcza indeks w tablicy danych o jedną pozycję.
        /// </summary>
        public void skip()
        {
            if (_count > 0)
            {
                _idx++;
                _eof = _idx >= _count;
            }
        }//skip

        /// <summary>
        /// Przemieszcza indeks w tablicy danych na pozycję pierwszą.
        /// </summary>
        public void top()
        {
            if (_count > 0)
            {
                _idx = 0;
                _eof = false;
            }
        }//top

        /// <summary>
        /// Zmienna logiczna osiągnięcia końca pliku.
        /// </summary>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        public MaterialyVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new MaterialyVO();
            }
        }//MaszynyVO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        public int idx
        {
            set
            {
                if (value > -1 & value < _count)
                {
                    _idx = value;
                }
            }
        }//idx

        /// <summary>
        /// Sprawdza istnienie rekordu.
        /// </summary>
        /// <param name="Identyfikator">Identyfikator Materiału</param>
        /// <returns>Wynik logiczny sprawdzenia.</returns>
        private bool exists(int Identyfikator)
        {
            foreach (MaterialyVO VOi in _VOs)
            {
                if (VOi.Identyfikator == Identyfikator) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Identyfikator">Identyfikator Materiału.</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora.</returns>
        public int getIdx(int Identyfikator)
        {
            int idx = -1;
            foreach (MaterialyVO VOi in _VOs)
            {
                idx++;
                if (VOi.Identyfikator == Identyfikator) return idx;
            }

            return -1;
        }//getIdx

        /// <summary>
        /// Zwraca material o wskazanym identyfikatorze.
        /// </summary>
        /// <param name="Identyfikator">Identyfikator Materiału.</param>
        /// <returns>Materialy. Jeśli ID == -1 to materialu nie znaleziono.</returns>
        public MaterialyVO GetVO(int Identyfikator)
        {
            foreach (nsAccess2DB.MaterialyVO VO in _VOs)
            {
                if (VO.Identyfikator == Identyfikator) return VO;
            }
            return new MaterialyVO();
        }//getVO

        public void selectQuery(string query)
        {
            fillTable(_DAO.selectQuery(query));
        }//selectQuery

      
    }// clasa MaterialyBUS

}//namespace nsAccess2DB
