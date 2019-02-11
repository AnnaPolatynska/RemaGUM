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
    /// <summary>
    /// Class dbConnection.
    /// </summary>
    class dbConnection
    {
        /// <exclude />
        private OleDbDataAdapter _adapter;
        /// <exclude />
        private OleDbConnection _conn;
        /// <exclude />
        public string _error = string.Empty; //komunikat błedu
        /// <summary>
        /// Initializes a new instance of the <see cref="dbConnection"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        /// <constructor>
        /// Połączenie z bazą.
        /// </constructor>
        public dbConnection(string connString)
        {
            _adapter = new OleDbDataAdapter();
            _conn = new OleDbConnection(connString);
        }//dbConnection
         /// <summary>
         /// Opens the connection.
         /// </summary>
         /// <returns>OleDbConnection.</returns>
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

        /// <summary>
        /// Executes the select query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>DataTable.</returns>
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

        /// <summary>
        /// Executes the insert query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Executes the delete query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Executes the update query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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
    /// <summary>
    /// Klasa wymiany danych z tabelą Maszyny
    /// </summary>
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
        private byte[] _Zawartosc_pliku = new byte[]{}; // obiekt OLE - zdjęcie
        private string _Rozszerz_zdj = string.Empty; // 255
        private string _Nazwa_dysponent = string.Empty; // 255
        private string _Nr_pom = string.Empty; // 255
        private string _Dzial = string.Empty; // 255
        private string _Nr_prot_BHP = string.Empty; // 255
        private int _Data_ost_przegl = 0; //liczba
        private int _Data_kol_przegl = 0; //liczba
        private string _Uwagi = string.Empty; //255
        private string _Wykorzystanie = string.Empty; // 255
        private string _Stan_techniczny = string.Empty; // 255
        private string _Propozycja = string.Empty; // 255
        private int _Rok_ost_przeg = 0; //liczba
        private int _Mc_ost_przeg = 0; //liczba
        private int _Dz_ost_przeg = 0; //liczba
        private int _Rok_kol_przeg = 0; //liczba
        private int _Mc_kol_przeg = 0; //liczba
        private int _Dz_kol_przeg = 0; //liczba

        /// <summary>
        /// Konstruktor wymiany danych z tabelą Maszyny
        /// </summary>
        //public MaszynyVO (){   }
        public MaszynyVO() { }
        // gettery i settery
        /// <summary>
        /// Gets or sets the identyfikator.
        /// </summary>
        /// <value>The identyfikator.</value>
        public int Identyfikator
        {
            get { return _Identyfikator; }
            set { _Identyfikator = value; }
        }
        /// <summary>
        /// Gets or sets the kategoria.
        /// </summary>
        /// <value>The kategoria.</value>
        public string Kategoria
        {
            get { return _Kategoria; }
            set { _Kategoria = value; }
        }
        /// <summary>
        /// Gets or sets the nazwa.
        /// </summary>
        /// <value>The nazwa.</value>
        public string Nazwa
        {
            get { return _Nazwa; }
            set { _Nazwa = value; }
        }
        /// <summary>
        /// Gets or sets the typ.
        /// </summary>
        /// <value>The typ.</value>
        public string Typ
        {
            get { return _Typ; }
            set { _Typ = value; }
        }
        /// <summary>
        /// Gets or sets the nr inwentarzowy.
        /// </summary>
        /// <value>The nr inwentarzowy.</value>
        public string Nr_inwentarzowy
        {
            get { return _Nr_inwentarzowy; }
            set { _Nr_inwentarzowy = value; }
        }
        /// <summary>
        /// Gets or sets the nr fabryczny.
        /// </summary>
        /// <value>The nr fabryczny.</value>
        public string Nr_fabryczny
        {
            get { return _Nr_fabryczny; }
            set { _Nr_fabryczny = value; }
        }
        /// <summary>
        /// Gets or sets the rok produkcji.
        /// </summary>
        /// <value>The rok produkcji.</value>
        public string Rok_produkcji {
            get { return _Rok_produkcji; }
            set { _Rok_produkcji = value; }
        }
        /// <summary>
        /// Gets or sets the producent.
        /// </summary>
        /// <value>The producent.</value>
        public string Producent
        {
            get { return _Producent; }
            set { _Producent = value; }
        }
        /// <summary>
        /// Gets or sets the zdjecie.
        /// </summary>
        /// <value>The zdjecie.</value>
        public string Zdjecie
        {
            get { return _Zdjecie; }
            set { _Zdjecie = value; }
        }
        /// <summary>
        /// Gets or sets the zawartosc pliku.
        /// </summary>
        /// <value>The zawartosc pliku.</value>
        public byte[] Zawartosc_pliku
        {
            get { return _Zawartosc_pliku; }
            set { _Zawartosc_pliku = value; }
        }
        /// <summary>
        /// Gets or sets the rozszerz ZDJ.
        /// </summary>
        /// <value>The rozszerz ZDJ.</value>
        public string Rozszerz_zdj
        {
            get { return _Rozszerz_zdj; }
            set { _Rozszerz_zdj = value; }
        }
        /// <summary>
        /// Gets or sets the nazwa dysponent.
        /// </summary>
        /// <value>The nazwa dysponent.</value>
        public string Nazwa_dysponent
        {
            get { return _Nazwa_dysponent; }
            set { _Nazwa_dysponent = value; }
        }
        /// <summary>
        /// Gets or sets the nr pom.
        /// </summary>
        /// <value>The nr pom.</value>
        public string Nr_pom {
            get { return _Nr_pom; }
            set { _Nr_pom = value; }
        }
        /// <summary>
        /// Gets or sets the dzial.
        /// </summary>
        /// <value>The dzial.</value>
        public string Dzial
        {
            get { return _Dzial; }
            set { _Dzial = value; }
        }
        /// <summary>
        /// Gets or sets the nr prot BHP.
        /// </summary>
        /// <value>The nr prot BHP.</value>
        public string Nr_prot_BHP {
            get { return _Nr_prot_BHP; }
            set { _Nr_prot_BHP = value; }
        }
        /// <summary>
        /// Gets or sets the data ost przegl.
        /// </summary>
        /// <value>The data ost przegl.</value>
        public int Data_ost_przegl
        {
            get { return _Data_ost_przegl; }
            set { _Data_ost_przegl = value; }
        }
        /// <summary>
        /// Gets or sets the data kol przegl.
        /// </summary>
        /// <value>The data kol przegl.</value>
        public int Data_kol_przegl
        {
            get { return _Data_kol_przegl; }
            set { _Data_kol_przegl = value; }
        }
        /// <summary>
        /// Gets or sets the uwagi.
        /// </summary>
        /// <value>The uwagi.</value>
        public string Uwagi {
            get { return _Uwagi; }
            set { _Uwagi = value; }
        }
        /// <summary>
        /// Gets or sets the wykorzystanie.
        /// </summary>
        /// <value>The wykorzystanie.</value>
        public string Wykorzystanie
        {
            get { return _Wykorzystanie; }
            set { _Wykorzystanie = value; }
        }
        /// <summary>
        /// Gets or sets the stan techniczny.
        /// </summary>
        /// <value>The stan techniczny.</value>
        public string Stan_techniczny
        {
            get { return _Stan_techniczny; }
            set { _Stan_techniczny = value; }
        }
        /// <summary>
        /// Gets or sets the propozycja.
        /// </summary>
        /// <value>The propozycja.</value>
        public string Propozycja
        {
            get { return _Propozycja; }
            set { _Propozycja = value; }
        }
        /// <summary>
        /// Gets or sets the rok ost przeg.
        /// </summary>
        /// <value>The rok ost przeg.</value>
        public int Rok_ost_przeg
        {
            get { return _Rok_ost_przeg; }
            set { _Rok_ost_przeg = value; }
        }
        /// <summary>
        /// Gets or sets the mc ost przeg.
        /// </summary>
        /// <value>The mc ost przeg.</value>
        public int Mc_ost_przeg
        {
            get { return _Mc_ost_przeg; }
            set { _Mc_ost_przeg = value; }
        }
        /// <summary>
        /// Gets or sets the dz ost przeg.
        /// </summary>
        /// <value>The dz ost przeg.</value>
        public int Dz_ost_przeg
        {
            get { return _Dz_ost_przeg; }
            set { _Dz_ost_przeg = value; }
        }
        /// <summary>
        /// Gets or sets the rok kol przeg.
        /// </summary>
        /// <value>The rok kol przeg.</value>
        public int Rok_kol_przeg
        {
            get { return _Rok_kol_przeg; }
            set { _Rok_kol_przeg = value; }
        }
        /// <summary>
        /// Gets or sets the mc kol przeg.
        /// </summary>
        /// <value>The mc kol przeg.</value>
        public int Mc_kol_przeg
        {
            get { return _Mc_kol_przeg; }
            set { _Mc_kol_przeg = value; }
        }
        /// <summary>
        /// Gets or sets the dz kol przeg.
        /// </summary>
        /// <value>The dz kol przeg.</value>
        public int Dz_kol_przeg
        {
            get { return _Dz_kol_przeg; }
            set { _Dz_kol_przeg = value; }
        }

    }// class MaszynyVO

    //klasa dostępu (Data Access Object) do tabeli Maszyny ----------> DAO
    /// <summary>
    /// Class MaszynyDAO.
    /// </summary>
    public class MaszynyDAO
    {
        /// <summary>
        /// The connection
        /// </summary>
        private dbConnection _conn;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaszynyDAO"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        /// <construktor>
        /// Konstruktor
        /// </construktor>
        public MaszynyDAO(string connString) {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        } //konstruktor MaszynyDAO

        /// <summary>
        /// Selects the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>DataTable.</returns>
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
            string query = "SELECT * FROM Maszyny";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Zwraca tabelę wszystkich Maszyn po ID.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
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
        /// Selects the nazwa.
        /// </summary>
        /// <param name="Nazwa">The nazwa.</param>
        /// <returns>DataTable.</returns>
        public DataTable selectNazwa(string Nazwa)
        {
            string query = "SELECT * FROM Maszyny WHERE Nazwa = '" + Nazwa.ToString() + "';";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        } //selectNazwa

        /// <summary>
        /// Zwraca z tabeli Maszyny Zdjęcie wybranej maszyny.
        /// </summary>
        /// <param name="Zdjecie">The zdjecie.</param>
        /// <returns>DataTable.</returns>
        public DataTable selectZdjecie(string Zdjecie)
        {
            string query = "SELECT * FROM Maszyny WHERE Zdjecie = '" + Zdjecie.ToString() + "';";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        } //selectZdjecie

        /// <summary>
        /// Wprowadza nowy rekord.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool insert(nsAccess2DB.MaszynyVO VO)
        {
            string query = "INSERT INTO Maszyny (Kategoria, Nazwa, Typ, Nr_inwentarzowy, Nr_fabryczny, Rok_produkcji, Producent, Zdjecie, Zawartosc_pliku, Rozszerz_zdj, Nazwa_dysponent, Nr_pom, Dzial, Nr_prot_BHP, Data_ost_przegl, Data_kol_przegl, " +
                "Uwagi, Wykorzystanie, Stan_techniczny, Propozycja, Rok_ost_przeg, Mc_ost_przeg, Dz_ost_przeg, Rok_kol_przeg, Mc_kol_przeg, Dz_kol_przeg)" +
                " VALUES (@Kategoria, @Nazwa, @Typ, @Nr_inwentarzowy, @Nr_fabryczny, @Rok_produkcji, @Producent, @Zdjecie, @Zawartosc_pliku, @Rozszerz_zdj, @Nazwa_dysponent, @Nr_pom, @Dzial, @Nr_prot_BHP, @Data_ost_przegl, @Data_kol_przegl, " +
                "@Uwagi, @Wykorzystanie, @Stan_techniczny, @Propozycja, @Rok_ost_przeg, @Mc_ost_przeg, @Dz_ost_przeg, @Rok_kol_przeg, @Mc_kol_przeg, @Dz_kol_przeg);";

            OleDbParameter[] parameters = new OleDbParameter[26];
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

            parameters[8] = new OleDbParameter("Zawartosc_pliku", OleDbType.VarBinary);
            parameters[8].Value = VO.Zawartosc_pliku;

            parameters[9] = new OleDbParameter("Rozszerz_zdj", OleDbType.VarChar, 255);
            parameters[9].Value = VO.Rozszerz_zdj;

            parameters[10] = new OleDbParameter("Nazwa_dysponent", OleDbType.VarChar, 255);
            parameters[10].Value = VO.Nazwa_dysponent;

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

            parameters[20] = new OleDbParameter("Rok_ost_przeg", OleDbType.Integer);
            parameters[20].Value = VO.Rok_ost_przeg;

            parameters[21] = new OleDbParameter("Mc_ost_przeg", OleDbType.Integer);
            parameters[21].Value = VO.Mc_ost_przeg;

            parameters[22] = new OleDbParameter("Dz_ost_przeg", OleDbType.Integer);
            parameters[22].Value = VO.Dz_ost_przeg;

            parameters[23] = new OleDbParameter("Rok_kol_przeg", OleDbType.Integer);
            parameters[23].Value = VO.Rok_kol_przeg;

            parameters[24] = new OleDbParameter("Mc_kol_przeg", OleDbType.Integer);
            parameters[24].Value = VO.Mc_kol_przeg;

            parameters[25] = new OleDbParameter("Dz_kol_przeg", OleDbType.Integer);
            parameters[25].Value = VO.Dz_kol_przeg;

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
            string query = "UPDATE Maszyny SET Kategoria = @Kategoria, Nazwa = @Nazwa, Typ = @Typ, Nr_inwentarzowy = @Nr_inwentarzowy, Nr_fabryczny = @Nr_fabryczny, Rok_produkcji = @Rok_produkcji, Producent = @Producent, Zdjecie = @Zdjecie, Zawartosc_pliku = @Zawartosc_pliku, Rozszerz_zdj = @Rozszerz_zdj, Nazwa_dysponent = @Nazwa_dysponent, Nr_pom = @Nr_pom, Dzial = @Dzial, Nr_prot_BHP = @Nr_prot_BHP, Data_ost_przegl = @Data_ost_przegl, " +
                "Data_kol_przegl = @Data_kol_przegl, Uwagi = @Uwagi, Wykorzystanie = @Wykorzystanie, Stan_techniczny = @Stan_techniczny, Propozycja = @Propozycja, Rok_ost_przeg = @Rok_ost_przeg," +
                " Mc_ost_przeg = @Mc_ost_przeg, Dz_ost_przeg = @Dz_ost_przeg, Rok_kol_przeg = @Rok_kol_przeg, Mc_kol_przeg = @Mc_kol_przeg, Dz_kol_przeg = @Dz_kol_przeg WHERE Identyfikator = " + VO.Identyfikator.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[26];
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

            parameters[8] = new OleDbParameter("Zawartosc_pliku", OleDbType.VarBinary);
            parameters[8].Value = VO.Zawartosc_pliku;

            parameters[9] = new OleDbParameter("Rozszerz_zdj", OleDbType.VarChar, 255);
            parameters[9].Value = VO.Rozszerz_zdj;

            parameters[10] = new OleDbParameter("Nazwa_dysponent", OleDbType.VarChar, 255);
            parameters[10].Value = VO.Nazwa_dysponent;

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

            parameters[20] = new OleDbParameter("Rok_ost_przeg", OleDbType.Integer);
            parameters[20].Value = VO.Rok_ost_przeg;

            parameters[21] = new OleDbParameter("Mc_ost_przeg", OleDbType.Integer);
            parameters[21].Value = VO.Mc_ost_przeg;

            parameters[22] = new OleDbParameter("Dz_ost_przeg", OleDbType.Integer);
            parameters[22].Value = VO.Dz_ost_przeg;

            parameters[23] = new OleDbParameter("Rok_kol_przeg", OleDbType.Integer);
            parameters[23].Value = VO.Rok_kol_przeg;

            parameters[24] = new OleDbParameter("Mc_kol_przeg", OleDbType.Integer);
            parameters[24].Value = VO.Mc_kol_przeg;

            parameters[25] = new OleDbParameter("Dz_kol_przeg", OleDbType.Integer);
            parameters[25].Value = VO.Dz_kol_przeg;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//update

        /// <summary>
        /// Deletes the specified identyfikator.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool delete(int Identyfikator)
        {
            string query = "DELETE * FROM Maszyny WHERE Identyfikator = " + Identyfikator.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }// delete

    }//class MaszynyDAO

    // warstawa operacji biznesowych tabeli Maszyny ---> BUS 
    /// <summary>
    /// Class MaszynyBUS.
    /// </summary>
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
        /// <param name="connString">ConnectionString.</param>
        public MaszynyBUS(string connString)
        {
            _DAO = new MaszynyDAO(connString);
            _error = _DAO._error;
        }//konstruktor MaszynyBUS

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

        /// <summary>
        /// Wypełnia tablice danych pozycjami.
        /// </summary>
        /// <param name="Nazwa">The nazwa.</param>
        public void selectNazwa(string Nazwa)
        {
            fillTable(_DAO.selectNazwa(Nazwa));
        } //selectNazwa

        /// <summary>
        /// Wypełnia tablice danych pozycjami.
        /// </summary>
        /// <param name="Zdjecie">The zdjecie.</param>
        public void selectZdjecie(string Zdjecie)
        {
            fillTable(_DAO.selectZdjecie(Zdjecie));
        }// selectZdjecie

        /// <summary>
        /// Dowolne zapytanie z formularza.
        /// </summary>
        /// <param name="query">The query.</param>
        public void selectQuery(string query)
        {
            fillTable(_DAO.selectQuery(query));
        }//selectQuery

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
        /// Aktualizuje rekord z wyjątkiem Identyfikatora Maszyny.
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
        /// Usuwa rekord po identyfikatorze.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool delete(int Identyfikator)
        {
            return _DAO.delete(Identyfikator);
        }// delete

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
                    VOi.Zawartosc_pliku = new byte[]{};
                }

                VOi.Rozszerz_zdj = dr["Rozszerz_zdj"].ToString();
                VOi.Nazwa_dysponent = dr["Nazwa_dysponent"].ToString();
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
        }//count

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        public MaszynyVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    _VOi = _VOs[_idx];
                    return _VOi;
                }
                return new MaszynyVO();
            }
        }//MaszynyVO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        /// <value>The index.</value>
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

    }//public class MaszynyBUS

    //////////////////////////////////////////////////////////////////////// Wykorzystanie - dane słownikowe
    /// <summary>
    /// Klasa wymiany danych z tabelą Czestotliwosc.
    /// </summary>
    public class WykorzystanieVO
    {
        /// <summary>
        /// The wykorzystanie
        /// </summary>
        private string _Wykorzystanie = string.Empty; //100

        /// <summary>
        /// Konstruktor wymiany danych Wykorzystanie
        /// </summary>
        public WykorzystanieVO() { }

        /// <summary>
        /// Gets or sets the wykorzystanie.
        /// </summary>
        /// <value>The wykorzystanie.</value>
        public string Wykorzystanie
        {
            get { return _Wykorzystanie; }
            set { _Wykorzystanie = value; }
        }
    }//class WykorzystanieVO

    //Klasa dostępu (Data Access Object) do tabeli Wykorzystanie.
    /// <summary>
    /// Class WykorzystanieDAO.
    /// </summary>
    public class WykorzystanieDAO
    {
        /// <summary>
        /// The connection
        /// </summary>
        private dbConnection _conn;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="WykorzystanieDAO"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
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
        /// <returns>DataTable.</returns>
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
    /// <summary>
    /// Class WykorzystanieBUS.
    /// </summary>
    public class WykorzystanieBUS
    {
        /// <summary>
        /// The DAO
        /// </summary>
        WykorzystanieDAO _DAO;

        /// <summary>
        /// The v os
        /// </summary>
        private WykorzystanieVO[] _VOs = new WykorzystanieVO[0];    //lista danych
        /// <summary>
        /// The v oi
        /// </summary>
        private WykorzystanieVO _VOi = new WykorzystanieVO();       //dane na pozycji _idx
        /// <summary>
        /// The index
        /// </summary>
        private int _idx = 0;                       //indeks pozycji
        /// <summary>
        /// The EOF
        /// </summary>
        private bool _eof = false;                  //wskaźnik końca pliku
        /// <summary>
        /// The count
        /// </summary>
        private int _count = 0;                     //liczba pozycji

        /// <summary>
        /// The error
        /// </summary>
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
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        /// <value>The count.</value>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        /// <value>The vo.</value>
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
        /// <value>The index.</value>
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
        /// <param name="Wykorzystanie">The wykorzystanie.</param>
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

    /////////////////////////////////////////////////////////////////////////////////// Kategoria - dane słownikowe
    /// <summary>
    /// Klasa wymiany danych z tabelą Kategoria.
    /// </summary>
    public class KategoriaVO
    {
        /// <summary>
        /// The kategoria
        /// </summary>
        private string _Kategoria = string.Empty; //100
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Kategoria
        /// </summary>
        public KategoriaVO() { }

        /// <summary>
        /// Gets or sets the nazwa kategoria.
        /// </summary>
        /// <value>The nazwa kategoria.</value>
        public string NazwaKategoria
        {
            get { return _Kategoria; }
            set { _Kategoria = value; }
        }
    }//class CzestotliwoscVO

    //Klasa dostępu (Data Access Object) do tabeli Kategoria.
    /// <summary>
    /// Class KategoriaDAO.
    /// </summary>
    public class KategoriaDAO
    {
        /// <summary>
        /// The connection
        /// </summary>
        private dbConnection _conn;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="KategoriaDAO"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
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
        /// <returns>DataTable.</returns>
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
    /// <summary>
    /// Class KategoriaBUS.
    /// </summary>
    public class KategoriaBUS
    {
        /// <summary>
        /// The DAO
        /// </summary>
        KategoriaDAO _DAO;

        /// <summary>
        /// The v os
        /// </summary>
        private KategoriaVO[] _VOs = new KategoriaVO[0];    //lista danych
        /// <summary>
        /// The v oi
        /// </summary>
        private KategoriaVO _VOi = new KategoriaVO();       //dane na pozycji _idx
        /// <summary>
        /// The index
        /// </summary>
        private int _idx = 0;                       //indeks pozycji
        /// <summary>
        /// The EOF
        /// </summary>
        private bool _eof = false;                  //wskaźnik końca pliku
        /// <summary>
        /// The count
        /// </summary>
        private int _count = 0;                     //liczba pozycji

        /// <summary>
        /// The error
        /// </summary>
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
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        /// <value>The count.</value>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        /// <value>The vo.</value>
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
        /// <value>The index.</value>
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
        /// <param name="Nazwa">The nazwa.</param>
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
     /////////////////////////////////////////////////////////////////////////////////// Propozycja - dane słownikowe
     /// <summary>
     /// Klasa wymiany danych z tabelą Propozycja.
     /// </summary>
    public class PropozycjaVO
    {
        /// <summary>
        /// The propozycja
        /// </summary>
        private string _Propozycja = string.Empty; //100
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Propozycja
        /// </summary>
        public PropozycjaVO() { }

        /// <summary>
        /// Gets or sets the nazwa.
        /// </summary>
        /// <value>The nazwa.</value>
        public string Nazwa
        {
            get { return _Propozycja; }
            set { _Propozycja = value; }
        }
    }//class PropozycjaVO

    //Klasa dostępu (Data Access Object) do tabeli Propozycja.
    /// <summary>
    /// Class PropozycjaDAO.
    /// </summary>
    public class PropozycjaDAO
    {
        /// <summary>
        /// The connection
        /// </summary>
        private dbConnection _conn;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropozycjaDAO"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
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
        /// <returns>DataTable.</returns>
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
    /// <summary>
    /// Class PropozycjaBUS.
    /// </summary>
    public class PropozycjaBUS
    {
        /// <summary>
        /// The DAO
        /// </summary>
        PropozycjaDAO _DAO;

        /// <summary>
        /// The v os
        /// </summary>
        private PropozycjaVO[] _VOs = new PropozycjaVO[0];    //lista danych
        /// <summary>
        /// The v oi
        /// </summary>
        private PropozycjaVO _VOi = new PropozycjaVO();       //dane na pozycji _idx
        /// <summary>
        /// The index
        /// </summary>
        private int _idx = 0;                       //indeks pozycji
        /// <summary>
        /// The EOF
        /// </summary>
        private bool _eof = false;                  //wskaźnik końca pliku
        /// <summary>
        /// The count
        /// </summary>
        private int _count = 0;                     //liczba pozycji

        /// <summary>
        /// The error
        /// </summary>
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
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        /// <value>The count.</value>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        /// <value>The vo.</value>
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
        /// <value>The index.</value>
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
        /// <param name="Nazwa">The nazwa.</param>
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

    /////////////////////////////////////////////////////////////////////////////////// Stan_techniczny - dane słownikowe.
    /// <summary>
    /// Klasa wymiany danych z tabelą Stan_techniczny.
    /// </summary>
    public class Stan_technicznyVO
    {
        /// <summary>
        /// The stan techniczny
        /// </summary>
        private string _Stan_techniczny = string.Empty; //100
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Stan_techniczny
        /// </summary>
        public Stan_technicznyVO() { }

        /// <summary>
        /// Gets or sets the nazwa.
        /// </summary>
        /// <value>The nazwa.</value>
        public string Nazwa
        {
            get { return _Stan_techniczny; }
            set { _Stan_techniczny = value; }
        }
    }//class Stan_technicznyVO

    //Klasa dostępu (Data Access Object) do tabeli Stan_techniczny.
    /// <summary>
    /// Class Stan_technicznyDAO.
    /// </summary>
    public class Stan_technicznyDAO
    {
        /// <summary>
        /// The connection
        /// </summary>
        private dbConnection _conn;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Stan_technicznyDAO"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
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
        /// <returns>DataTable.</returns>
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
    /// <summary>
    /// Class Stan_technicznyBUS.
    /// </summary>
    public class Stan_technicznyBUS
    {
        /// <summary>
        /// The DAO
        /// </summary>
        Stan_technicznyDAO _DAO;

        /// <summary>
        /// The v os
        /// </summary>
        private Stan_technicznyVO[] _VOs = new Stan_technicznyVO[0];    //lista danych
        /// <summary>
        /// The v oi
        /// </summary>
        private Stan_technicznyVO _VOi = new Stan_technicznyVO();       //dane na pozycji _idx
        /// <summary>
        /// The index
        /// </summary>
        private int _idx = 0;                       //indeks pozycji
        /// <summary>
        /// The EOF
        /// </summary>
        private bool _eof = false;                  //wskaźnik końca pliku
        /// <summary>
        /// The count
        /// </summary>
        private int _count = 0;                     //liczba pozycji

        /// <summary>
        /// The error
        /// </summary>
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
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        /// <value>The count.</value>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        /// <value>The vo.</value>
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
        /// <value>The index.</value>
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
        /// <param name="Nazwa">The nazwa.</param>
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
     /////////////////////////////////////////////////////////////////////////////////// Dzial - dane słownikowe.
     /// <summary>
     /// Klasa wymiany danych z tabelą Dzial.
     /// </summary>
    public class DzialVO
    {
        /// <summary>
        /// The dzial
        /// </summary>
        private string _Dzial = string.Empty; //100
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Dzial
        /// </summary>
        public DzialVO() { }

        /// <summary>
        /// Gets or sets the nazwa.
        /// </summary>
        /// <value>The nazwa.</value>
        public string Nazwa
        {
            get { return _Dzial; }
            set { _Dzial = value; }
        }
    }//class DzialVO

    //Klasa dostępu (Data Access Object) do tabeli Dzial.
    /// <summary>
    /// Class DzialDAO.
    /// </summary>
    public class DzialDAO
    {
        /// <summary>
        /// The connection
        /// </summary>
        private dbConnection _conn;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="DzialDAO"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
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
        /// <returns>DataTable.</returns>
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
    /// <summary>
    /// Class DzialBUS.
    /// </summary>
    public class DzialBUS
    {
        /// <summary>
        /// The DAO
        /// </summary>
        DzialDAO _DAO;

        /// <summary>
        /// The v os
        /// </summary>
        private DzialVO[] _VOs = new DzialVO[0];    //lista danych
        /// <summary>
        /// The v oi
        /// </summary>
        private DzialVO _VOi = new DzialVO();       //dane na pozycji _idx
        /// <summary>
        /// The index
        /// </summary>
        private int _idx = 0;                       //indeks pozycji
        /// <summary>
        /// The EOF
        /// </summary>
        private bool _eof = false;                  //wskaźnik końca pliku
        /// <summary>
        /// The count
        /// </summary>
        private int _count = 0;                     //liczba pozycji

        /// <summary>
        /// The error
        /// </summary>
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
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        /// <value>The count.</value>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        /// <value>The vo.</value>
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
        /// <value>The index.</value>
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
        /// <param name="Nazwa">The nazwa.</param>
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

    /////////////////////////////////////////////////////////////////////////////////// Operator_maszyny
    /// <summary>
    /// Klasa wymiany danych z tabelą Operator_maszyny.
    /// </summary>
    public class OperatorVO
    {
        /// <summary>
        /// The identyfikator
        /// </summary>
        private int _Identyfikator = 0;
        /// <summary>
        /// The dzial
        /// </summary>
        private string _Dzial = string.Empty; //255
        /// <summary>
        /// The uprawnienie
        /// </summary>
        private string _Uprawnienie = string.Empty; //255
        /// <summary>
        /// The rok
        /// </summary>
        private int _Rok = 0;
        /// <summary>
        /// The mc
        /// </summary>
        private int _Mc = 0;
        /// <summary>
        /// The dzien
        /// </summary>
        private int _Dzien = 0;
        /// <summary>
        /// The data konca upr
        /// </summary>
        private DateTime _Data_konca_upr = DateTime.Now;
        /// <summary>
        /// The op imie
        /// </summary>
        private string _Op_imie = string.Empty; //255
        /// <summary>
        /// The op nazwisko
        /// </summary>
        private string _Op_nazwisko = string.Empty; //255
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Operator_maszyny
        /// </summary>
        public OperatorVO() { }
        /// <summary>
        /// Gets or sets the identyfikator.
        /// </summary>
        /// <value>The identyfikator.</value>
        public int Identyfikator
        {
            get { return _Identyfikator; }
            set { _Identyfikator = value; }
        }
        /// <summary>
        /// Gets or sets the op imie.
        /// </summary>
        /// <value>The op imie.</value>
        public string Op_imie
        {
            get { return _Op_imie; }
            set { _Op_imie = value; }
        }
        /// <summary>
        /// Gets or sets the op nazwisko.
        /// </summary>
        /// <value>The op nazwisko.</value>
        public string Op_nazwisko
        {
            get { return _Op_nazwisko; }
            set { _Op_nazwisko = value; }
        }
        /// <summary>
        /// Gets or sets the dzial.
        /// </summary>
        /// <value>The dzial.</value>
        public string Dzial
        {
            get { return _Dzial; }
            set { _Dzial = value; }
        }
        /// <summary>
        /// Gets or sets the uprawnienie.
        /// </summary>
        /// <value>The uprawnienie.</value>
        public string Uprawnienie
        {
            get { return _Uprawnienie; }
            set { _Uprawnienie = value; }
        }
        /// <summary>
        /// Gets or sets the rok.
        /// </summary>
        /// <value>The rok.</value>
        public int Rok
        {
            get { return _Rok; }
            set { _Rok = value; }
        }
        /// <summary>
        /// Gets or sets the mc.
        /// </summary>
        /// <value>The mc.</value>
        public int Mc
        {
            get { return _Mc; }
            set { _Mc = value; }
        }
        /// <summary>
        /// Gets or sets the dzien.
        /// </summary>
        /// <value>The dzien.</value>
        public int Dzien
        {
            get { return _Dzien; }
            set { _Dzien = value; }
        }
        /// <summary>
        /// Gets or sets the data konca upr.
        /// </summary>
        /// <value>The data konca upr.</value>
        public DateTime Data_konca_upr
        {
            get { return _Data_konca_upr; }
            set { _Data_konca_upr = value; }
        }
    }//class OperatorVO

    //Klasa dostępu (Data Access Object) do tabeli Nazwa_operatora.
    /// <summary>
    /// Class OperatorDAO.
    /// </summary>
    public class OperatorDAO
    {
        /// <summary>
        /// The connection
        /// </summary>
        private dbConnection _conn;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorDAO"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        /// <constructor>
        /// Konstruktor.
        /// </constructor>
        public OperatorDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }//OperatorDAO

        //---> dowolne zapytanie z poziomu Form
        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>DataTable.</returns>
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
        /// <returns>Tabela Nazwa_operatora.</returns>
        public DataTable select()
        {
            string query = "SELECT * FROM Operator;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
        /// <returns>DataTable.</returns>
        public DataTable select(int Identyfikator)
        {
            string query = "SELECT * FROM Operator WHERE Identyfikator = " + Identyfikator.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select po nazwie dzialu

        /// <summary>
        /// Wprowadza nowy rekord do tabeli Operator_maszyny.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych (insert)</param>
        /// <returns>Wartość logiczna powodzenia operacji</returns>
        public bool insert(nsAccess2DB.OperatorVO VO)
        {
            string query = "INSERT INTO Operator (Dzial, Uprawnienie, Data_konca_upr, Rok, Mc, Dzien, Op_imie, Op_nazwisko)" +
                " VALUES (@Dzial, @Uprawnienie, @Data_konca_upr, @Rok, @Mc, @Dzien, @Op_imie, @Op_nazwisko)";

            OleDbParameter[] parameters = new OleDbParameter[8];

            parameters[0] = new OleDbParameter("Dzial", OleDbType.VarChar, 255);
            parameters[0].Value = VO.Dzial;

            parameters[1] = new OleDbParameter("Uprawnienie", OleDbType.VarChar, 255);
            parameters[1].Value = VO.Uprawnienie;

            parameters[2] = new OleDbParameter("Data_konca_upr", OleDbType.Date);
            parameters[2].Value = VO.Data_konca_upr;

            parameters[3] = new OleDbParameter("Rok", OleDbType.Integer);
            parameters[3].Value = VO.Rok;

            parameters[4] = new OleDbParameter("Mc", OleDbType.Integer);
            parameters[4].Value = VO.Mc;

            parameters[5] = new OleDbParameter("Dzien", OleDbType.Integer);
            parameters[5].Value = VO.Dzien;

            parameters[6] = new OleDbParameter("Op_imie", OleDbType.VarChar, 255);
            parameters[6].Value = VO.Op_imie;

            parameters[7] = new OleDbParameter("Op_nazwisko", OleDbType.VarChar, 255);
            parameters[7].Value = VO.Op_nazwisko;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//insert

        /// <summary>
        /// Aktualizuje rekord z wyjątkiem Identyfikatora Operatora_maszyny.
        /// </summary>
        /// <param name="VO">The vo.</param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool update(nsAccess2DB.OperatorVO VO)
        {
            string query = "UPDATE Operator SET Dzial = @Dzial, Uprawnienie = @Uprawnienie, Data_konca_upr = @Data_konca_upr, Rok = @Rok, Mc = @Mc, Dzien = @Dzien, Op_imie = @Op_imie, Op_nazwisko = @Op_nazwisko WHERE Identyfikator = " + VO.Identyfikator.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[8];

            parameters[0] = new OleDbParameter("Dzial", OleDbType.VarChar, 255);
            parameters[0].Value = VO.Dzial;

            parameters[1] = new OleDbParameter("Uprawnienie", OleDbType.VarChar, 255);
            parameters[1].Value = VO.Uprawnienie;

            parameters[2] = new OleDbParameter("Data_konca_upr", OleDbType.Date);
            parameters[2].Value = VO.Data_konca_upr;

            parameters[3] = new OleDbParameter("Rok", OleDbType.Integer);
            parameters[3].Value = VO.Rok;

            parameters[4] = new OleDbParameter("Mc", OleDbType.Integer);
            parameters[4].Value = VO.Mc;

            parameters[5] = new OleDbParameter("Dzien", OleDbType.Integer);
            parameters[5].Value = VO.Dzien;

            parameters[6] = new OleDbParameter("Op_imie", OleDbType.VarChar, 255);
            parameters[6].Value = VO.Op_imie;

            parameters[7] = new OleDbParameter("Op_nazwisko", OleDbType.VarChar, 255);
            parameters[7].Value = VO.Op_nazwisko;

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
            string query = "DELETE * FROM Operator WHERE Identyfikator = " + Identyfikator.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//delete

    }//class OperatorDAO

    //Warstwa operacji biznesowaych tabeli Nazwa_operatora BUS.
    /// <summary>
    /// Class OperatorBUS.
    /// </summary>
    public class OperatorBUS
    {
        /// <summary>
        /// The DAO
        /// </summary>
        OperatorDAO _DAO;

        /// <summary>
        /// The v os
        /// </summary>
        private OperatorVO[] _VOs = new OperatorVO[0];    //lista danych
        /// <summary>
        /// The v oi
        /// </summary>
        private OperatorVO _VOi = new OperatorVO();       //dane na pozycji _idx
        /// <summary>
        /// The index
        /// </summary>
        private int _idx = 0;                                             //indeks pozycji
        /// <summary>
        /// The EOF
        /// </summary>
        private bool _eof = false;                                        //wskaźnik końca pliku
        /// <summary>
        /// The count
        /// </summary>
        private int _count = 0;                                           //liczba pozycji

        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">ConnectionString.</param>
        public OperatorBUS(string connString)
        {
            _DAO = new OperatorDAO(connString);
            _error = _DAO._error;
        }//OperatorBUS

        /// <summary>
        /// Wypełnia tablicę pozycjami danych -------------------------------------&gt; dowolne zapytanie z poziomu Form
        /// </summary>
        /// <param name="query">The query.</param>
        public void selectQuery(string query)
        {
            fillTable(_DAO.selectQuery(query));
        }

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
        /// <param name="Identyfikator">The identyfikator.</param>
        public void select(int Identyfikator)
        {
            fillTable(_DAO.select(Identyfikator));
        }//select

        /// <summary>
        /// Wprowadza rekord tabeli.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        private bool insert(nsAccess2DB.OperatorVO VO)
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
        private bool update(nsAccess2DB.OperatorVO VO)
        {
            bool b = _DAO.update(VO);
            _error = _DAO._error;
            return b;
        }//update

        /// <summary>
        /// Usuwa rekord.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
        public void delete(int Identyfikator)
        {
            _DAO.delete(Identyfikator);
        }// delete

        /// <summary>
        /// Wypełnia tablice.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            OperatorVO VOi;
            _VOs = new OperatorVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                VOi = new OperatorVO();

                VOi.Identyfikator = int.Parse(dr["Identyfikator"].ToString());
                VOi.Dzial = dr["Dzial"].ToString();
                VOi.Uprawnienie = dr["Uprawnienie"].ToString();
                try
                {
                    //VOi.Data_konca_upr = int.Parse(dr["Data_konca_upr"].ToString());
                    VOi.Data_konca_upr = DateTime.Parse(dr["Data_konca_upr"].ToString());
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
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        /// <value>The count.</value>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        /// <value>The vo.</value>
        public OperatorVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new OperatorVO();
            }
        }//OperatorVO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        /// <value>The index.</value>
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
            foreach (OperatorVO VOi in _VOs)
            {
                if (VOi.Identyfikator == Identyfikator) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora.</returns>
        public int getIdx(int Identyfikator)
        {
            int idx = -1;
            foreach (OperatorVO VOi in _VOs)
            {
                idx++;
                if (VOi.Identyfikator == Identyfikator) return idx;
            }
            return -1;
        }//getIdx
        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Identyfikator">Identyfikator Operator maszyny</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora dla Operatora maszyny.</returns>
        public OperatorVO GetVO(int Identyfikator)
        {
            foreach (nsAccess2DB.OperatorVO VO in _VOs)
            {
                if (VO.Identyfikator == Identyfikator) return VO;
            }
            return new OperatorVO();
        }// getVO
         /// <summary>
         /// Dodaje lub aktualizuje rekord.
         /// </summary>
         /// <param name="VO">Obiekt wymiany danych.</param>
         /// <returns>Wynik powodzenia akcji.</returns>
        public bool write(nsAccess2DB.OperatorVO VO)
        {
            if (exists(VO.Identyfikator))
            {
                return _DAO.update(VO);
            }
            return _DAO.insert(VO);
        }//write


    }//class OperatorBUS

    //////////////////////////////////////////////////////////////////////         Operator_maszyny_Maszyny
    /// <summary>
    /// Klasa wymiany danych z tabelą Operator_maszyny_Maszyny.
    /// </summary>
    public class Maszyny_OperatorVO
    {
        /// <summary>
        /// The identyfikator
        /// </summary>
        private int _Identyfikator = 0;
        /// <summary>
        /// The identifier maszyny
        /// </summary>
        private int _ID_maszyny = -1;
        /// <summary>
        /// The identifier op maszyny
        /// </summary>
        private int _ID_op_maszyny = -1;
        /// <summary>
        /// The maszyny nazwa
        /// </summary>
        private string _Maszyny_nazwa = string.Empty;


        /// <summary>
        /// Konstruktor wymiany danych z tabelą Operator_maszyny_Maszyny
        /// </summary>
        public Maszyny_OperatorVO() { }
        /// <summary>
        /// Gets or sets the identyfikator.
        /// </summary>
        /// <value>The identyfikator.</value>
        public int Identyfikator
        {
            get { return _Identyfikator; }
            set { _Identyfikator = value; }
        }
        /// <summary>
        /// Gets or sets the identifier op maszyny.
        /// </summary>
        /// <value>The identifier op maszyny.</value>
        public int ID_op_maszyny
        {
            get { return _ID_op_maszyny; }
            set { _ID_op_maszyny = value; }
        }
        /// <summary>
        /// Gets or sets the identifier maszyny.
        /// </summary>
        /// <value>The identifier maszyny.</value>
        public int ID_maszyny
        {
            get { return _ID_maszyny; }
            set { _ID_maszyny = value; }
        }
        /// <summary>
        /// Gets or sets the maszyny nazwa.
        /// </summary>
        /// <value>The maszyny nazwa.</value>
        public string Maszyny_nazwa
        {
            get { return _Maszyny_nazwa; }
            set { _Maszyny_nazwa = value; }
        }
    }//class Maszyny_OperatorVO

    //Klasa dostępu (Data Access Object) do tabeli Operator_maszyny_Maszyny.
    /// <summary>
    /// Class Maszyny_OperatorDAO.
    /// </summary>
    public class Maszyny_OperatorDAO
    {
        /// <summary>
        /// The connection
        /// </summary>
        private dbConnection _conn;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Maszyny_OperatorDAO"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        /// <constructor>
        /// Konstruktor.
        /// </constructor>
        public Maszyny_OperatorDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }//Maszyny_OperatorDAO

        //-----> dowolne zapytanie z poziomu Form

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>DataTable.</returns>
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
        /// <returns>Tabela Maszyny_OperatorDAO.</returns>
        public DataTable select()
        {
            string query = "SELECT * FROM Maszyny_Operator;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select 

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <returns>DataTable.</returns>
        public DataTable select(int ID_maszyny)
        {
            string query = "SELECT * FROM Maszyny_Operator WHERE ID_maszyny = " + ID_maszyny.ToString() + ";"; 

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <param name="ID_op_maszyny">The identifier op maszyny.</param>
        /// <returns>DataTable.</returns>
        public DataTable select(int ID_maszyny, int ID_op_maszyny)
        {
            string query = "SELECT * FROM Maszyny_Operator WHERE ID_maszyny = " + ID_maszyny.ToString() + " AND ID_op_maszyny = " + ID_op_maszyny.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Selects the operator.
        /// </summary>
        /// <returns>DataTable.</returns>
        public DataTable selectOperator()
        {
            string query = "SELECT * FROM Maszyny_Operator;";
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        } // selectOperator

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="ID_op_maszyny">The identifier op maszyny.</param>
        /// <returns>DataTable.</returns>
        public DataTable selectOperator(int ID_op_maszyny)
        {
            string query = "SELECT * FROM Maszyny_Operator WHERE ID_op_maszyny = " + ID_op_maszyny.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        } // selectOperator


        /// <summary>
        /// Wprowadza nowy rekord
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych (insert)</param>
        /// <returns>Wartość logiczna powodzenia operacji</returns>
        public bool insert(nsAccess2DB.Maszyny_OperatorVO VO)
        {
            string query = "INSERT INTO Maszyny_Operator (ID_op_maszyny, ID_maszyny, Maszyny_nazwa)" +
                    "VALUES (@ID_op_maszyny, @ID_maszyny, @Maszyny_nazwa)";
            OleDbParameter[] parameters = new OleDbParameter[3];
            parameters[0] = new OleDbParameter("ID_op_maszyny", OleDbType.Integer);
            parameters[0].Value = VO.ID_op_maszyny;

            parameters[1] = new OleDbParameter("ID_maszyny", OleDbType.Integer);
            parameters[1].Value = VO.ID_maszyny;

            parameters[2] = new OleDbParameter("Maszyny_nazwa", OleDbType.VarChar, 255);
            parameters[2].Value = VO.Maszyny_nazwa;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//insert

        /// <summary>
        /// Aktualizuje rekord z wyjątkiem ID.
        /// </summary>
        /// <param name="VO">The vo.</param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool update(nsAccess2DB.Maszyny_OperatorVO VO)
        {
            string query = "UPDATE Maszyny_Operator SET ID_op_maszyny = @ID_op_maszyny, Maszyny_nazwa = @Maszyny_nazwa, ID_maszyny = @ID_maszyny WHERE ID_op_maszyny = " + VO.ID_op_maszyny.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[3];
            parameters[0] = new OleDbParameter("ID_op_maszyny", OleDbType.Integer);
            parameters[0].Value = VO.ID_op_maszyny;

            parameters[1] = new OleDbParameter("ID_maszyny", OleDbType.Integer);
            parameters[1].Value = VO.ID_maszyny;

            parameters[2] = new OleDbParameter("Maszyny_nazwa", OleDbType.VarChar, 255);
            parameters[2].Value = VO.Maszyny_nazwa;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//update

        /// <summary>
        /// Kasuje rekord po podanym Identyfikatorze.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool delete(int ID_maszyny)
        {
            string query = "DELETE * FROM Maszyny_Operator WHERE ID_maszyny = " + ID_maszyny.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//delete

        /// <summary>
        /// Kasuje rekord po podanym Identyfikatorze.
        /// </summary>
        /// <param name="ID_op_maszyny">The identifier op maszyny.</param>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool delete(int ID_op_maszyny, int ID_maszyny)
        {
            string query = "DELETE * FROM Maszyny_Operator WHERE ID_op_maszyny = " + ID_op_maszyny.ToString() + "AND ID_maszyny = " + ID_maszyny.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//delete

    }//class Maszyny_OperatorDAO

    //Warstwa operacji biznesowaych tabeli Maszyny_Operator ---> BUS.
    /// <summary>
    /// Class Maszyny_OperatorBUS.
    /// </summary>
    public class Maszyny_OperatorBUS
    {
        /// <summary>
        /// The DAO
        /// </summary>
        Maszyny_OperatorDAO _DAO;

        /// <summary>
        /// The v os
        /// </summary>
        private Maszyny_OperatorVO[] _VOs = new Maszyny_OperatorVO[0];    //lista danych
        /// <summary>
        /// The v oi
        /// </summary>
        private Maszyny_OperatorVO _VOi = new Maszyny_OperatorVO();       //dane na pozycji _idx
        /// <summary>
        /// The index
        /// </summary>
        private int _idx = 0;                                             //indeks pozycji
        /// <summary>
        /// The EOF
        /// </summary>
        private bool _eof = false;                                        //wskaźnik końca pliku
        /// <summary>
        /// The count
        /// </summary>
        private int _count = 0;                                           //liczba pozycji

        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        public Maszyny_OperatorBUS(string connString)
        {
            _DAO = new Maszyny_OperatorDAO(connString);
            _error = _DAO._error;
        }//Maszyny_OperatorBUS

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
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        public void select(int ID_maszyny)
        {
            fillTable(_DAO.select(ID_maszyny));
        }//select

        /// <summary>
        /// Wypełnia tablicę pozycjami danych.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <param name="ID_op_maszyny">The identifier op maszyny.</param>
        public void select(int ID_maszyny, int ID_op_maszyny)
        {
            fillTable(_DAO.select(ID_maszyny, ID_op_maszyny));
        }//select

        /// <summary>
        /// Wypełnia tablicę pozycjami danych.
        /// </summary>
        internal void selectOperator()
        {
            fillTable(_DAO.selectOperator());
        }
        /// <summary>
        /// Wypełnia tablicę pozycjami danych.
        /// </summary>
        /// <param name="ID_operator">The identifier operator.</param>
        public void selectOperator(int ID_operator)
        {
            fillTable(_DAO.selectOperator(ID_operator));
        }//selectOperator

        /// <summary>
        /// Wypełnia tablicę pozycjami danych -------------------------------------&gt; dowolne zapytanie z poziomu Form
        /// </summary>
        /// <param name="query">The query.</param>
        public void selectQuery(string query)
        {
            fillTable(_DAO.selectQuery(query));
        }// selectQuery


        /// <summary>
        /// Wprowadza rekord do tabeli.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <param name="ID_op_maszyny">The identifier op maszyny.</param>
        /// <param name="Maszyny_nazwa">The maszyny nazwa.</param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool insert(int ID_maszyny, int ID_op_maszyny, string Maszyny_nazwa)
        {
            Maszyny_OperatorVO VO = new Maszyny_OperatorVO();
            VO.ID_maszyny = ID_maszyny;
            VO.ID_op_maszyny = ID_op_maszyny;
            VO.Maszyny_nazwa = Maszyny_nazwa;

            add(VO, ref _VOs);
            return _DAO.insert(VO);

        }//insert

        /// <summary>
        /// Aktualizuje rekord z wyjątkiem numeru protokołu.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <param name="ID_op_maszyny">The identifier op maszyny.</param>
        /// <param name="Maszyny_nazwa">The maszyny nazwa.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        private bool update(int ID_maszyny, int ID_op_maszyny, string Maszyny_nazwa)
        {
            bool b = _DAO.update(VO);
            _error = _DAO._error;
            return b;
        }//update


        /// <summary>
        /// Usuwa z tabeli pozycję o wskazanych parametrach.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        public bool delete(int ID_maszyny)
        {
           return _DAO.delete(ID_maszyny);
           
        }// delete

        /// <summary>
        /// Usuwa z tabeli pozycję o wskazanych parametrach.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <param name="ID_op_maszyny">The identifier op maszyny.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        public bool delete(int ID_maszyny, int ID_op_maszyny)
        {
            return _DAO.delete(ID_maszyny, ID_op_maszyny);

        }// delete

        /// <summary>
        /// Wypełnia tablicę.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            Maszyny_OperatorVO VOi;
            _VOs = new Maszyny_OperatorVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);

                VOi = new Maszyny_OperatorVO();
                VOi.Identyfikator = int.Parse(dr["Identyfikator"].ToString());
                VOi.ID_op_maszyny = int.Parse(dr["ID_op_maszyny"].ToString());
                VOi.ID_maszyny = int.Parse(dr["ID_maszyny"].ToString());
                VOi.Maszyny_nazwa = dr["Maszyny_nazwa"].ToString();
                
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


        // <summary>
        /// <summary>
        /// Skips this instance.
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
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        public bool eof
        {
            get { return _eof; }
        }// eof

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        /// <value>The count.</value>
        public int count
        {
            get { return _count; }
        }//count

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        /// <value>The vo.</value>
        public Maszyny_OperatorVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new Maszyny_OperatorVO();
            }
        }//VO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        /// <value>The index.</value>
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
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <param name="ID_op_maszyny">The identifier op maszyny.</param>
        /// <returns>Wartość logiczna istnienia pozycji.</returns>
        public bool exists(int ID_maszyny, int ID_op_maszyny)
        {
            foreach (Maszyny_OperatorVO VOi in _VOs)
            {
                if (VOi.ID_maszyny == ID_maszyny & VOi.ID_op_maszyny == ID_op_maszyny) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="ID_maszyny">ID_maszyny z taleli Maszyny_Operator.</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora.</returns>
        public int getIdx(int ID_maszyny)
        {
            int idx = -1;
            foreach (Maszyny_OperatorVO VOi in _VOs)
            {
                idx++;
                if (VOi.ID_maszyny == ID_maszyny) return idx;
            }
            return -1;
        }// getIdx

        /// <summary>
        /// Zwraca maszynę o podanym indeksie z tabeli Maszyny_Operator.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <returns>Maszyny_OperatorVO.</returns>
        public Maszyny_OperatorVO GetVO(int ID_maszyny)
        {
            if (VO.ID_maszyny == ID_maszyny)
            {
                return VO;
            }
            return new Maszyny_OperatorVO();
        }// GetVO

        /// <summary>
        /// Writes the specified vo.
        /// </summary>
        /// <param name="VO">The vo.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool write(nsAccess2DB.Maszyny_OperatorVO VO)
        {
            if (exists(VO.ID_maszyny, VO.ID_op_maszyny))
            {
                return _DAO.update(VO);
            }
            return _DAO.insert(VO);
        }// write

        /// <summary>
        /// Dodaje pozycję do tabeli.
        /// </summary>
        /// <param name="VO">The vo.</param>
        /// <param name="VOs">The v os.</param>
        private void add(Maszyny_OperatorVO VO, ref Maszyny_OperatorVO[] VOs)
        {
            Array.Resize(ref VOs, VOs.Length + 1);
            VOs[_VOs.Length - 1] = VO;
        }//add

       
    }//class Maszyny_OperatorBUS



    /////////////////////////////////////////////////////////////////////////////////// Dysponent 
    /// <summary>
    /// Klasa wymiany danych z tabelą Dysponent .
    /// </summary>
    public class DysponentVO
    {
        /// <summary>
        /// The identyfikator
        /// </summary>
        private int _Identyfikator = 0;
        /// <summary>
        /// The dysp dane
        /// </summary>
        private string _Dysp_dane = string.Empty; //255
        /// <summary>
        /// The dzial
        /// </summary>
        private string _Dzial = string.Empty; //255
        /// <summary>
        /// The dysp nazwisko
        /// </summary>
        private string _Dysp_nazwisko = string.Empty; //255
        /// <summary>
        /// The dysp imie
        /// </summary>
        private string _Dysp_imie = string.Empty; //255
        /// <summary>
        /// The dysp nazwa
        /// </summary>
        private string _Dysp_nazwa = string.Empty;// 255
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Dysponent.
        /// </summary>
        public DysponentVO() { }

        /// <summary>
        /// Gets or sets the identyfikator.
        /// </summary>
        /// <value>The identyfikator.</value>
        public int Identyfikator
        {
            get { return _Identyfikator; }
            set { _Identyfikator = value; }
        }
        /// <summary>
        /// Gets or sets the dysp dane.
        /// </summary>
        /// <value>The dysp dane.</value>
        public string Dysp_dane
        {
            get { return _Dysp_dane; }
            set { _Dysp_dane = value; }
        }
        /// <summary>
        /// Gets or sets the dzial.
        /// </summary>
        /// <value>The dzial.</value>
        public string Dzial
        {
            get { return _Dzial; }
            set { _Dzial = value; }
        }
        /// <summary>
        /// Gets or sets the dysp nazwisko.
        /// </summary>
        /// <value>The dysp nazwisko.</value>
        public string Dysp_nazwisko
        {
            get { return _Dysp_nazwisko; }
            set { _Dysp_nazwisko = value; }
        }
        /// <summary>
        /// Gets or sets the dysp imie.
        /// </summary>
        /// <value>The dysp imie.</value>
        public string Dysp_imie
        {
            get { return _Dysp_imie; }
            set { _Dysp_imie = value; }
        }
        /// <summary>
        /// Gets or sets the dysp nazwa.
        /// </summary>
        /// <value>The dysp nazwa.</value>
        public string Dysp_nazwa
        {
            get { return _Dysp_nazwa; }
            set { _Dysp_nazwa = value; }
        }
    }//class DysponentVO

    //Klasa dostępu (Data Access Object) do tabeli Dysponent . ----------> DAO
    /// <summary>
    /// Class DysponentDAO.
    /// </summary>
    public class DysponentDAO
    {
        /// <summary>
        /// The connection
        /// </summary>
        private dbConnection _conn;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="DysponentDAO"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        /// <constructor>
        /// Konstruktor.
        /// </constructor>
        public DysponentDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }//DysponentDAO

        // ---> dowolne zapytanie z poziomu Form

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>DataTable.</returns>
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
        /// <returns>DataTable.</returns>
        public DataTable select()
        {
            string query = "SELECT * FROM Dysponent;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
        /// <returns>DataTable.</returns>
        public DataTable select(int Identyfikator)
        {
            string query = "SELECT * FROM Dysponent WHERE Identyfikator = " + Identyfikator.ToString() + ";";

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
        public bool insert(nsAccess2DB.DysponentVO VO)
        {
            string query = "INSERT INTO Dysponent (Dysp_dane, Dzial, Dysp_nazwisko, Dysp_imie, Dysp_nazwa)" +
                " VALUES (@Dysp_dane, @Dzial, @Dysp_nazwisko, @Dysp_imie, @Dysp_nazwa)";

            OleDbParameter[] parameters = new OleDbParameter[5];

            parameters[0] = new OleDbParameter("Dysp_dane", OleDbType.VarChar, 255);
            parameters[0].Value = VO.Dysp_dane;

            parameters[1] = new OleDbParameter("Dzial", OleDbType.VarChar, 255);
            parameters[1].Value = VO.Dzial;

            parameters[2] = new OleDbParameter("Dysp_nazwisko", OleDbType.VarChar, 255);
            parameters[2].Value = VO.Dysp_nazwisko;

            parameters[3] = new OleDbParameter("Dysp_imie", OleDbType.VarChar, 255);
            parameters[3].Value = VO.Dysp_imie;

            parameters[4] = new OleDbParameter("Dysp_nazwa", OleDbType.VarChar, 255);
            parameters[4].Value = VO.Dysp_nazwa;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//insert

        /// <summary>
        /// Aktualizuje rekord z wyjątkiem Identyfikatora.
        /// </summary>
        /// <param name="VO">The vo.</param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool update(nsAccess2DB.DysponentVO VO)
        {
            string query = "UPDATE Dysponent SET Dysp_dane = @Dysp_dane, Dzial = @Dzial, Dysp_nazwisko = @Dysp_nazwisko, Dysp_imie = @Dysp_imie, Dysp_nazwa = @Dysp_nazwa WHERE Identyfikator = " + VO.Identyfikator.ToString() + ";";
         
            OleDbParameter[] parameters = new OleDbParameter[5];

            parameters[0] = new OleDbParameter("Dysp_dane", OleDbType.VarChar, 255);
            parameters[0].Value = VO.Dysp_dane;

            parameters[1] = new OleDbParameter("Dzial", OleDbType.VarChar, 255);
            parameters[1].Value = VO.Dzial;

            parameters[2] = new OleDbParameter("Dysp_nazwisko", OleDbType.VarChar, 255);
            parameters[2].Value = VO.Dysp_nazwisko;

            parameters[3] = new OleDbParameter("Dysp_imie", OleDbType.VarChar, 255);
            parameters[3].Value = VO.Dysp_imie;

            parameters[4] = new OleDbParameter("Dysp_nazwa", OleDbType.VarChar, 255);
            parameters[4].Value = VO.Dysp_nazwa;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        } // update

        /// <summary>
        /// Kasuje rekord po podanym Identyfikatorze.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool delete(int Identyfikator)
        {
            string query = "DELETE * FROM Dysponent WHERE Identyfikator = " + Identyfikator.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }// delete

    }//class DysponentDAO

    // Warstwa operacji biznesowaych tabeli Dysponent  --->    BUS.
    /// <summary>
    /// Class DysponentBUS.
    /// </summary>
    public class DysponentBUS
    {
        /// <summary>
        /// The DAO
        /// </summary>
        DysponentDAO _DAO;

        /// <summary>
        /// The v os
        /// </summary>
        private DysponentVO[] _VOs = new DysponentVO[0];    //lista danych
        /// <summary>
        /// The v oi
        /// </summary>
        private DysponentVO _VOi = new DysponentVO();       //dane na pozycji _idx
        /// <summary>
        /// The index
        /// </summary>
        private int _idx = 0;                       //indeks pozycji
        /// <summary>
        /// The EOF
        /// </summary>
        private bool _eof = false;                  //wskaźnik końca pliku
        /// <summary>
        /// The count
        /// </summary>
        private int _count = 0;                     //liczba pozycji

        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">ConnectionString.</param>
        public DysponentBUS(string connString)
        {
            _DAO = new DysponentDAO(connString);
            _error = _DAO._error;
        }//DysponentBUS

        /// <summary>
        /// Wypełnia tablice danych pozycjami.
        /// </summary>
        public void select()
        {
            fillTable(_DAO.select());
        }//select

        /// <summary>
        /// Wypełnia tablicę danych pozycjami.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
        public void select(int Identyfikator)
        {
            fillTable(_DAO.select(Identyfikator));
        }//select

        /// <summary>
        /// Dowolne zapytanie z formularza.
        /// </summary>
        /// <param name="query">The query.</param>
        public void selectQuery(string query)
        {
            fillTable(_DAO.selectQuery(query));
        }//selectQuery

        /// <summary>
        /// Wprowadza rekord do tabeli Osoba zarządzająca.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        private bool insert(nsAccess2DB.DysponentVO VO)
        {
            bool b = _DAO.insert(VO);
            _error = _DAO._error;
            return b;
        }//insert

        /// <summary>
        /// Aktualizuje rekord z wyjątkiem ID osoby zarządzającej.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        private bool update(nsAccess2DB.DysponentVO VO)
        {
            bool b = _DAO.update(VO);
            _error = _DAO._error;
            return b;
        }//update

        /// <summary>
        /// Usuwa rekord po Identyfikatorze.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool delete(int Identyfikator)
        {
            return _DAO.delete(Identyfikator);
        }//delete

        /// <summary>
        /// Wypełnia tablice.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            DysponentVO VOi;
            _VOs = new DysponentVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);
         
                VOi = new DysponentVO();

                VOi.Identyfikator = int.Parse(dr["Identyfikator"].ToString());
                VOi.Dysp_dane = dr["Dysp_dane"].ToString();
                VOi.Dzial = dr["Dzial"].ToString();
                VOi.Dysp_nazwisko = dr["Dysp_nazwisko"].ToString();
                VOi.Dysp_imie = dr["Dysp_imie"].ToString();
                VOi.Dysp_nazwa = dr["Dysp_nazwa"].ToString();
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
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        public bool eof
        {
            get { return _eof; }
        }// eof

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        /// <value>The count.</value>
        public int count
        {
            get { return _count; }
        }// count

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        /// <value>The vo.</value>
        public DysponentVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new DysponentVO();
            }
        }//VO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        /// <value>The index.</value>
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
        /// <param name="Identyfikator">Identyfikator Dysponenta.</param>
        /// <returns>Wynik logiczny sprawdzenia.</returns>
        public bool exists(int Identyfikator)
        {
            foreach (DysponentVO VOi in _VOs)
            {
                if (VOi.Identyfikator == Identyfikator) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Identyfikator">Identyfikator Dysponenta.</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora Dysponenta.</returns>
        public int getIdx(int Identyfikator)
        {
            int idx = -1;
            foreach (DysponentVO VOi in _VOs)
            {
                idx++;
                if (VOi.Identyfikator == Identyfikator) return idx;
            }
            return -1;
        }//getIdx

        /// <summary>
        /// Zwraca Osobę zarządzającą o podanym Identyfikatorze.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
        /// <returns>Jeżeli Identyfikator == -1 oznacza że Osoby Zarządzającej nie znaleziono.</returns>
        public DysponentVO GetVO(int Identyfikator)
        {
            foreach (nsAccess2DB.DysponentVO VO in _VOs)
            {
                if (VO.Identyfikator == Identyfikator) return VO;
            }
            return new DysponentVO();
        }//getVO

        /// <summary>
        /// Dodaje lub aktualizuje rekord.
        /// </summary>
        /// <param name="VO">The vo.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool write(nsAccess2DB.DysponentVO VO)
        {
            if (exists(VO.Identyfikator))
            {
                return _DAO.update(VO);
            }
            return _DAO.insert(VO);
        }//write

    }//class DysponentBUS



    //////////////////////////////////////////////////////////////////////        Maszyny_Dysponent
    /// <summary>
    /// Klasa wymiany danych z tabelą Maszyny_Dysponent.
    /// </summary>
    public class Maszyny_DysponentVO
    {
        /// <summary>
        /// The identifier maszyny
        /// </summary>
        private int _ID_maszyny = -1;
        /// <summary>
        /// The identifier dysponent
        /// </summary>
        private int _ID_dysponent = -1;
        /// <summary>
        /// The maszyny nazwa d
        /// </summary>
        private string _Maszyny_nazwa_D = string.Empty;


        /// <summary>
        /// Konstruktor wymiany danych z tabelą Maszyny_Dysponent.
        /// </summary>
        public Maszyny_DysponentVO() { }

        /// <summary>
        /// Gets or sets the identifier dysponent.
        /// </summary>
        /// <value>The identifier dysponent.</value>
        public int ID_dysponent
        {
            get { return _ID_dysponent; }
            set { _ID_dysponent = value; }
        }
        /// <summary>
        /// Gets or sets the identifier maszyny.
        /// </summary>
        /// <value>The identifier maszyny.</value>
        public int ID_maszyny
        {
            get { return _ID_maszyny; }
            set { _ID_maszyny = value; }
        }
        /// <summary>
        /// Gets or sets the maszyny nazwa d.
        /// </summary>
        /// <value>The maszyny nazwa d.</value>
        public string Maszyny_nazwa_D
        {
            get { return _Maszyny_nazwa_D; }
            set { _Maszyny_nazwa_D = value; }
        }
    }//class Maszyny_DysponentVO

    //Klasa dostępu (Data Access Object) do tabeli Maszyny_Dysponent.
    /// <summary>
    /// Class Maszyny_DysponentDAO.
    /// </summary>
    public class Maszyny_DysponentDAO
    {
        /// <summary>
        /// The connection
        /// </summary>
        private dbConnection _conn;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Maszyny_DysponentDAO"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        /// <constructor>
        /// Konstruktor.
        /// </constructor>
        public Maszyny_DysponentDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }//Maszyny_DysponentDAO

        //-----> dowolne zapytanie z poziomu Form

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>DataTable.</returns>
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
        /// <returns>Tabela Maszyny_DysponentDAO.</returns>
        public DataTable select()
        {
            string query = "SELECT * FROM Maszyny_Dysponent;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select 

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <returns>DataTable.</returns>
        public DataTable select(int ID_maszyny)
        {
            string query = "SELECT * FROM Maszyny_Dysponent WHERE ID_maszyny = " + ID_maszyny.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <param name="ID_dysponent">The identifier dysponent.</param>
        /// <returns>DataTable.</returns>
        public DataTable select(int ID_maszyny, int ID_dysponent)
        {
            string query = "SELECT * FROM Maszyny_Dysponent WHERE ID_maszyny = " + ID_maszyny.ToString() + " AND ID_dysponent = " + ID_dysponent.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Selects the dysponent.
        /// </summary>
        /// <returns>DataTable.</returns>
        public DataTable selectDysponent()
        {
            string query = "SELECT * FROM Maszyny_Dysponent;";
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        } // selectDyspoenent

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="ID_dysponent">The identifier dysponent.</param>
        /// <returns>DataTable.</returns>
        public DataTable selectDysponent(int ID_dysponent)
        {
            string query = "SELECT * FROM Maszyny_Dysponent WHERE ID_dysponent = " + ID_dysponent.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        } // selectDysponent


        /// <summary>
        /// Wprowadza nowy rekord.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych (insert)</param>
        /// <returns>Wartość logiczna powodzenia operacji</returns>
        public bool insert(nsAccess2DB.Maszyny_DysponentVO VO)
        {
            string query = "INSERT INTO Maszyny_Dysponent (ID_dysponent, ID_maszyny, Maszyny_nazwa_D)" +
                    "VALUES (@ID_dysponent, @ID_maszyny, @Maszyny_nazwa_D)";
            OleDbParameter[] parameters = new OleDbParameter[3];
            parameters[0] = new OleDbParameter("ID_dysponent", OleDbType.Integer);
            parameters[0].Value = VO.ID_dysponent;

            parameters[1] = new OleDbParameter("ID_maszyny", OleDbType.Integer);
            parameters[1].Value = VO.ID_maszyny;

            parameters[2] = new OleDbParameter("Maszyny_nazwa_D", OleDbType.VarChar, 255);
            parameters[2].Value = VO.Maszyny_nazwa_D;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//insert

        /// <summary>
        /// Aktualizuje rekord z wyjątkiem ID.
        /// </summary>
        /// <param name="VO">The vo.</param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool update(nsAccess2DB.Maszyny_DysponentVO VO)
        {
            string query = "UPDATE Maszyny_Operator SET ID_dysponent = @ID_dysponent, Maszyny_nazwa_D = @Maszyny_nazwa_D, ID_maszyny = @ID_maszyny WHERE ID_dysponent = " + VO.ID_dysponent.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[3];
            parameters[0] = new OleDbParameter("ID_dysponent", OleDbType.Integer);
            parameters[0].Value = VO.ID_dysponent;

            parameters[1] = new OleDbParameter("ID_maszyny", OleDbType.Integer);
            parameters[1].Value = VO.ID_maszyny;

            parameters[2] = new OleDbParameter("Maszyny_nazwa_D", OleDbType.VarChar, 255);
            parameters[2].Value = VO.Maszyny_nazwa_D;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//update

        /// <summary>
        /// Kasuje rekord po podanym Identyfikatorze.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool delete(int ID_maszyny)
        {
            string query = "DELETE * FROM Maszyny_Dysponent WHERE ID_maszyny = " + ID_maszyny.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//delete

        /// <summary>
        /// Kasuje rekord po podanym Identyfikatorze.
        /// </summary>
        /// <param name="ID_dysponent">The identifier dysponent.</param>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool delete(int ID_dysponent, int ID_maszyny)
        {
            string query = "DELETE * FROM Maszyny_Dysponent WHERE ID_dysponent = " + ID_dysponent.ToString() + "AND ID_maszyny = " + ID_maszyny.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//delete

    }//class Maszyny_DysponentDAO

    //Warstwa operacji biznesowaych tabeli Maszyny_Dysponent ---> BUS.
    /// <summary>
    /// Class Maszyny_DysponentBUS.
    /// </summary>
    public class Maszyny_DysponentBUS
    {
        /// <summary>
        /// The DAO
        /// </summary>
        Maszyny_DysponentDAO _DAO;

        /// <summary>
        /// The v os
        /// </summary>
        private Maszyny_DysponentVO[] _VOs = new Maszyny_DysponentVO[0];    //lista danych
        /// <summary>
        /// The v oi
        /// </summary>
        private Maszyny_DysponentVO _VOi = new Maszyny_DysponentVO();       //dane na pozycji _idx
        /// <summary>
        /// The index
        /// </summary>
        private int _idx = 0;                                             //indeks pozycji
        /// <summary>
        /// The EOF
        /// </summary>
        private bool _eof = false;                                        //wskaźnik końca pliku
        /// <summary>
        /// The count
        /// </summary>
        private int _count = 0;                                           //liczba pozycji

        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        public Maszyny_DysponentBUS(string connString)
        {
            _DAO = new Maszyny_DysponentDAO(connString);
            _error = _DAO._error;
        }//Maszyny_DysponentBUS

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
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        public void select(int ID_maszyny)
        {
            fillTable(_DAO.select(ID_maszyny));
        }//select

        /// <summary>
        /// Wypełnia tablicę pozycjami danych.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <param name="ID_dysponent">The identifier dysponent.</param>
        public void select(int ID_maszyny, int ID_dysponent)
        {
            fillTable(_DAO.select(ID_maszyny, ID_dysponent));
        }//select

        /// <summary>
        /// Wypełnia tablicę pozycjami danych.
        /// </summary>
        /// <param name="ID_dysponent">The identifier dysponent.</param>
        public void selectDysponent(int ID_dysponent)
        {
            fillTable(_DAO.selectDysponent(ID_dysponent));
        }//selectDysponent

        /// <summary>
        /// Wypełnia tablicę pozycjami danych -------------------------------------&gt; dowolne zapytanie z poziomu Form
        /// </summary>
        /// <param name="query">The query.</param>
        public void selectQuery(string query)
        {
            fillTable(_DAO.selectQuery(query));
        }// selectQuery


        /// <summary>
        /// Wprowadza rekord do tabeli.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <param name="ID_dysponent">The identifier dysponent.</param>
        /// <param name="Maszyny_nazwa_D">The maszyny nazwa d.</param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool insert(int ID_maszyny, int ID_dysponent, string Maszyny_nazwa_D)
        {
            Maszyny_DysponentVO VO = new Maszyny_DysponentVO();
            VO.ID_maszyny = ID_maszyny;
            VO.ID_dysponent = ID_dysponent;
            VO.Maszyny_nazwa_D = Maszyny_nazwa_D;

            add(VO, ref _VOs);
            return _DAO.insert(VO);

        }//insert

        /// <summary>
        /// Aktualizuje rekord z wyjątkiem numeru protokołu.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <param name="ID_dysponent">The identifier dysponent.</param>
        /// <param name="Maszyny_nazwa_D">The maszyny nazwa d.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        private bool update(int ID_maszyny, int ID_dysponent, string Maszyny_nazwa_D)
        {
            bool b = _DAO.update(VO);
            _error = _DAO._error;
            return b;
        }//update


        /// <summary>
        /// Usuwa z tabeli pozycję o wskazanych parametrach.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        public bool delete(int ID_maszyny)
        {
            return _DAO.delete(ID_maszyny);

        }// delete

        /// <summary>
        /// Usuwa z tabeli pozycję o wskazanych parametrach.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <param name="ID_dysponent">The identifier dysponent.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        public bool delete(int ID_maszyny, int ID_dysponent)
        {
            return _DAO.delete(ID_maszyny, ID_dysponent);

        }// delete

        /// <summary>
        /// Wypełnia tablicę.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            Maszyny_DysponentVO VOi;
            _VOs = new Maszyny_DysponentVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);

                VOi = new Maszyny_DysponentVO();
                VOi.ID_dysponent = int.Parse(dr["ID_dysponent"].ToString());
                VOi.ID_maszyny = int.Parse(dr["ID_maszyny"].ToString());
                VOi.Maszyny_nazwa_D = dr["Maszyny_nazwa_D"].ToString();

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


        // <summary>
        /// <summary>
        /// Skips this instance.
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
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        public bool eof
        {
            get { return _eof; }
        }// eof

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        /// <value>The count.</value>
        public int count
        {
            get { return _count; }
        }//count

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        /// <value>The vo.</value>
        public Maszyny_DysponentVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new Maszyny_DysponentVO();
            }
        }//VO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        /// <value>The index.</value>
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
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <param name="ID_dysponent">The identifier dysponent.</param>
        /// <returns>Wartość logiczna istnienia pozycji.</returns>
        public bool exists(int ID_maszyny, int ID_dysponent)
        {
            foreach (Maszyny_DysponentVO VOi in _VOs)
            {
                if (VOi.ID_maszyny == ID_maszyny & VOi.ID_dysponent == ID_dysponent) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="ID_maszyny">ID_maszyny z taleli Maszyny_Dysponent.</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora.</returns>
        public int getIdx(int ID_maszyny)
        {
            int idx = -1;
            foreach (Maszyny_DysponentVO VOi in _VOs)
            {
                idx++;
                if (VOi.ID_maszyny == ID_maszyny) return idx;
            }
            return -1;
        }// getIdx

        /// <summary>
        /// Zwraca maszynę o podanym indeksie z tabeli Maszyny_Dysponent.
        /// </summary>
        /// <param name="ID_maszyny">The identifier maszyny.</param>
        /// <returns>Maszyny_DysponentVO.</returns>
        public Maszyny_DysponentVO GetVO(int ID_maszyny)
        {
            if (VO.ID_maszyny == ID_maszyny)
            {
                return VO;
            }
            return new Maszyny_DysponentVO();
        }// GetVO

        /// <summary>
        /// Writes the specified vo.
        /// </summary>
        /// <param name="VO">The vo.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool write(nsAccess2DB.Maszyny_DysponentVO VO)
        {
            if (exists(VO.ID_maszyny, VO.ID_dysponent))
            {
                return _DAO.update(VO);
            }
            return _DAO.insert(VO);
        }// write

        /// <summary>
        /// Dodaje pozycję do tabeli.
        /// </summary>
        /// <param name="VO">The vo.</param>
        /// <param name="VOs">The v os.</param>
        private void add(Maszyny_DysponentVO VO, ref Maszyny_DysponentVO[] VOs)
        {
            Array.Resize(ref VOs, VOs.Length + 1);
            VOs[_VOs.Length - 1] = VO;
        }//add


    }//class Maszyny_DysponentBUS


    ///////////////////////////////////////////////////////////////// klasa wymiany danych z tabelą Materialy

    // ---------------------------------------------------------- MateriałyVO
    /// <summary>
    /// Klasa wymiany danych z tabelą Materialy
    /// </summary>
    public class MaterialyVO
    {
        /// <summary>
        /// The identyfikator
        /// </summary>
        private int _Identyfikator = -1;
        /// <summary>
        /// The nazwa mat
        /// </summary>
        private string _Nazwa_mat = string.Empty; //255
        /// <summary>
        /// The typ mat
        /// </summary>
        private string _Typ_mat = string.Empty;
        /// <summary>
        /// The rodzaj mat
        /// </summary>
        private string _Rodzaj_mat = string.Empty;
        /// <summary>
        /// The jednostka miar mat
        /// </summary>
        private string _Jednostka_miar_mat = string.Empty;
        /// <summary>
        /// The dostawca mat
        /// </summary>
        private string _Dostawca_mat = string.Empty;
        /// <summary>
        /// The stan mat
        /// </summary>
        private int _Stan_mat = 0; // liczba
        /// <summary>
        /// The zuzycie mat
        /// </summary>
        private int _Zuzycie_mat = 0; // liczba
        /// <summary>
        /// The odpad mat
        /// </summary>
        private int _Odpad_mat = 0; // liczba
        /// <summary>
        /// The stan minimum mat
        /// </summary>
        private int _Stan_min_mat = 0;// liczba
        /// <summary>
        /// The zapotrzebowanie mat
        /// </summary>
        private int _Zapotrzebowanie_mat = 0;// liczba
        /// <summary>
        /// The stan mag po mat
        /// </summary>
        private int _Stan_mag_po_mat = 0;// liczba
        /// <summary>
        /// The magazyn
        /// </summary>
        private string _Magazyn = string.Empty; //255

        /// <summary>
        /// Konstruktor wymiany danych z tabelą Materialy
        /// </summary>
        public MaterialyVO() { }

        // gettery i settery
        /// <summary>
        /// Gets or sets the identyfikator.
        /// </summary>
        /// <value>The identyfikator.</value>
        public int Identyfikator
        {
            get { return _Identyfikator; }
            set { _Identyfikator = value; }
        }
        /// <summary>
        /// Gets or sets the nazwa mat.
        /// </summary>
        /// <value>The nazwa mat.</value>
        public string Nazwa_mat
        {
            get { return _Nazwa_mat; }
            set { _Nazwa_mat = value; }
        }
        /// <summary>
        /// Gets or sets the typ mat.
        /// </summary>
        /// <value>The typ mat.</value>
        public string Typ_mat
        {
            get { return _Typ_mat; }
            set { _Typ_mat = value; }
        }
        /// <summary>
        /// Gets or sets the rodzaj mat.
        /// </summary>
        /// <value>The rodzaj mat.</value>
        public string Rodzaj_mat
        {
            get { return _Rodzaj_mat; }
            set { _Rodzaj_mat = value; }
        }
        /// <summary>
        /// Gets or sets the jednostka miar mat.
        /// </summary>
        /// <value>The jednostka miar mat.</value>
        public string Jednostka_miar_mat
        {
            get { return _Jednostka_miar_mat; }
            set { _Jednostka_miar_mat = value; }
        }
        /// <summary>
        /// Gets or sets the dostawca mat.
        /// </summary>
        /// <value>The dostawca mat.</value>
        public string Dostawca_mat
        {
            get { return _Dostawca_mat; }
            set { _Dostawca_mat = value; }
        }
        /// <summary>
        /// Gets or sets the stan mat.
        /// </summary>
        /// <value>The stan mat.</value>
        public int Stan_mat
        {
            get { return _Stan_mat; }
            set { _Stan_mat = value; }
        }
        /// <summary>
        /// Gets or sets the zuzycie mat.
        /// </summary>
        /// <value>The zuzycie mat.</value>
        public int Zuzycie_mat
        {
            get { return _Zuzycie_mat; }
            set { _Zuzycie_mat = value; }
        }
        /// <summary>
        /// Gets or sets the odpad mat.
        /// </summary>
        /// <value>The odpad mat.</value>
        public int Odpad_mat
        {
            get { return _Odpad_mat; }
            set { _Odpad_mat = value; }
        }
        /// <summary>
        /// Gets or sets the stan minimum mat.
        /// </summary>
        /// <value>The stan minimum mat.</value>
        public int Stan_min_mat
        {
            get { return _Stan_min_mat; }
            set { _Stan_min_mat = value; }
        }
        /// <summary>
        /// Gets or sets the zapotrzebowanie mat.
        /// </summary>
        /// <value>The zapotrzebowanie mat.</value>
        public int Zapotrzebowanie_mat
        {
            get { return _Zapotrzebowanie_mat; }
            set { _Zapotrzebowanie_mat = value; }
        }
        /// <summary>
        /// Gets or sets the stan mag po mat.
        /// </summary>
        /// <value>The stan mag po mat.</value>
        public int Stan_mag_po_mat
        {
            get { return _Stan_mag_po_mat; }
            set { _Stan_mag_po_mat = value; }
        }
        /// <summary>
        /// Gets or sets the magazyn.
        /// </summary>
        /// <value>The magazyn.</value>
        public string Magazyn
        {
            get { return _Magazyn; }
            set { _Magazyn = value; }
        }

    } //class MaterialyVO

    // --------------------------------------------klasa dostępu (Data Access Object) do tabeli Materiały DAO
    /// <summary>
    /// Class MaterialyDAO.
    /// </summary>
    public class MaterialyDAO
    {
        /// <summary>
        /// The connection
        /// </summary>
        private dbConnection _conn;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialyDAO"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        /// <construktor>
        /// Konstruktor
        /// </construktor>
        public MaterialyDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        } //konstruktor MaterialyDAO

        /// <summary>
        /// Selects the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>DataTable.</returns>
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
            string query = "SELECT * FROM Materialy;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Zwraca tabelę wszystkich Materiałów po ID.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
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
            string query = "INSERT INTO Materialy (Nazwa_mat, Typ_mat, Rodzaj_mat, Jednostka_miar_mat, Dostawca_mat, Stan_mat, Zuzycie_mat, Odpad_mat, Stan_min_mat, Zapotrzebowanie_mat, Stan_mag_po_mat, Magazyn) VALUES (@Nazwa_mat, @Typ_mat, @Rodzaj_mat, @Jednostka_miar_mat, @Dostawca_mat, @Stan_mat, @Zuzycie_mat, @Odpad_mat, @Stan_min_mat, @Zapotrzebowanie_mat, @Stan_mag_po_mat, @Magazyn);";

            OleDbParameter[] parameters = new OleDbParameter[12];
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

            parameters[5] = new OleDbParameter("Stan_mat", OleDbType.Integer);
            parameters[5].Value = VO.Stan_mat;

            parameters[6] = new OleDbParameter("Zuzycie_mat", OleDbType.Integer);
            parameters[6].Value = VO.Zuzycie_mat;

            parameters[7] = new OleDbParameter("Odpad_mat", OleDbType.Integer);
            parameters[7].Value = VO.Odpad_mat;

            parameters[8] = new OleDbParameter("Stan_min_mat", OleDbType.Integer);
            parameters[8].Value = VO.Stan_min_mat;

            parameters[9] = new OleDbParameter("Zapotrzebowanie_mat", OleDbType.Integer);
            parameters[9].Value = VO.Zapotrzebowanie_mat;

            parameters[10] = new OleDbParameter("Stan_mag_po_mat", OleDbType.Integer);
            parameters[10].Value = VO.Stan_mag_po_mat;

            parameters[11] = new OleDbParameter("Magazyn", OleDbType.VarChar, 100);
            parameters[11].Value = VO.Magazyn;

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
            string query = "UPDATE Materialy SET Nazwa_mat = @Nazwa_mat, Typ_mat = @Typ_mat, Rodzaj_mat = @Rodzaj_mat, Jednostka_miar_mat = @Jednostka_miar_mat, Dostawca_mat = @Dostawca_mat, Stan_mat = @Stan_mat, Zuzycie_mat = @Zuzycie_mat, Odpad_mat = @Odpad_mat, Stan_min_mat = @Stan_min_mat, Zapotrzebowanie_mat = @Zapotrzebowanie_mat, Stan_mag_po_mat = @Stan_mag_po_mat, Magazyn = @Magazyn WHERE Identyfikator = " + VO.Identyfikator.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[12];
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

            parameters[5] = new OleDbParameter("Stan_mat", OleDbType.Integer);
            parameters[5].Value = VO.Stan_mat;

            parameters[6] = new OleDbParameter("Zuzycie_mat", OleDbType.Integer);
            parameters[6].Value = VO.Zuzycie_mat;

            parameters[7] = new OleDbParameter("Odpad_mat", OleDbType.Integer);
            parameters[7].Value = VO.Odpad_mat;

            parameters[8] = new OleDbParameter("Stan_min_mat", OleDbType.Integer);
            parameters[8].Value = VO.Stan_min_mat;

            parameters[9] = new OleDbParameter("Zapotrzebowanie_mat", OleDbType.Integer);
            parameters[9].Value = VO.Zapotrzebowanie_mat;

            parameters[10] = new OleDbParameter("Stan_mag_po_mat", OleDbType.Integer);
            parameters[10].Value = VO.Stan_mag_po_mat;

            parameters[11] = new OleDbParameter("Magazyn", OleDbType.VarChar, 100);
            parameters[11].Value = VO.Magazyn;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//update

        /// <summary>
        /// Deletes the specified identyfikator.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool delete(int Identyfikator)
        {
            string query = "DELETE * FROM Materialy WHERE Identyfikator = " + Identyfikator.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//delete
    }//clasa MaterialyDAO

    // warstawa operacji biznesowych tabeli Materialy ---> BUS 
    /// <summary>
    /// Class MaterialyBUS.
    /// </summary>
    public class MaterialyBUS
    {
        /// <summary>
        /// The DAO
        /// </summary>
        MaterialyDAO _DAO;
        /// <summary>
        /// The v os
        /// </summary>
        private MaterialyVO[] _VOs = new MaterialyVO[0];//lista danych
        /// <summary>
        /// The v oi
        /// </summary>
        private MaterialyVO _VOi = new MaterialyVO();        //dane na pozycji _idx
        /// <summary>
        /// The index
        /// </summary>
        private int _idx = 0;                           //indeks pozycji
        /// <summary>
        /// The EOF
        /// </summary>
        private bool _eof = false;                      //wskaźnik końca pliku
        /// <summary>
        /// The count
        /// </summary>
        private int _count = 0;                         //liczba pozycji
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        public MaterialyBUS(string connString)
        {
            _DAO = new MaterialyDAO(connString);
            _error = _DAO._error;
        }// konstruktor MateralyBUS

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
        /// <param name="Identyfikator">Identyfikator Materiału.</param>
        public void select(int Identyfikator)
        {
            fillTable(_DAO.select(Identyfikator));
        }//select

        /// <summary>
        /// Dowolne zapytanie z formularza.
        /// </summary>
        /// <param name="query">The query.</param>
        public void selectQuery(string query)
        {
            fillTable(_DAO.selectQuery(query));
        }//selectQuery

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
        /// Aktualizuje rekord z wyjątkiem Identyfikatora czynności.
        /// </summary>
        /// <param name="VO">The vo.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        private bool update(nsAccess2DB.MaterialyVO VO)
        {
            bool b = _DAO.update(VO);
            _error = _DAO._error;
            return b;
        }// update

        /// <summary>
        /// Usuwa rekord po identyfikatorze.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool delete(int Identyfikator)
        {
            return _DAO.delete(Identyfikator);
        }// delete

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

                VOi.Identyfikator = int.Parse(dr["Identyfikator"].ToString());
                VOi.Nazwa_mat = dr["Nazwa_mat"].ToString();
                VOi.Typ_mat = dr["Typ_mat"].ToString();
                VOi.Rodzaj_mat = dr["Rodzaj_mat"].ToString();
                VOi.Jednostka_miar_mat = dr["Jednostka_miar_mat"].ToString();
                VOi.Dostawca_mat = dr["Dostawca_mat"].ToString();
                VOi.Stan_mat = int.Parse(dr["Stan_mat"].ToString());
                VOi.Zuzycie_mat = int.Parse(dr["Zuzycie_mat"].ToString());
                VOi.Odpad_mat = int.Parse(dr["Odpad_mat"].ToString());
                VOi.Stan_min_mat = int.Parse(dr["Stan_min_mat"].ToString());
                VOi.Zapotrzebowanie_mat = int.Parse(dr["Zapotrzebowanie_mat"].ToString());
                VOi.Stan_mag_po_mat = int.Parse(dr["Stan_mag_po_mat"].ToString());
                VOi.Magazyn = dr["Magazyn"].ToString();

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
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        /// <value>The count.</value>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        /// <value>The vo.</value>
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
        /// <value>The index.</value>
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

    }// clasa MaterialyBUS

    ////////////////////////////////////////////////////////////////////////////////// Magazyn - dane słownikowe.
    /// <summary>
    /// Klasa wymiany danych z Wybor_magazynu.
    /// </summary>
    public class Wybor_magazynuVO
    {
        /// <summary>
        /// The magazyn
        /// </summary>
        private string _Magazyn = string.Empty;
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Wybor_magazynu.
        /// </summary>
        public Wybor_magazynuVO() { }
        /// <summary>
        /// Gets or sets the magazyn.
        /// </summary>
        /// <value>The magazyn.</value>
        public string Magazyn
        {
            get { return _Magazyn; }
            set { _Magazyn = value; }
        }
    }//class  Wybor_magazynuVO

    /// <summary>
    /// Class Wybor_magazynuDAO.
    /// </summary>
    public class Wybor_magazynuDAO
    {
        /// <summary>
        /// The connection
        /// </summary>
        private dbConnection _conn;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Wybor_magazynuDAO"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        /// <constructor>
        /// Konstruktor.
        /// </constructor>
        public Wybor_magazynuDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }// Wybor_magazynuDAO
        /// <summary>
        /// Selects this instance.
        /// </summary>
        /// <returns>DataTable.</returns>
        public DataTable select()
        {
            string query = "SELECT * FROM Wybor_magazynu;";
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select
    }//class Wybor_magazynuDAO

    /// <summary>
    /// Class Wybor_magazynuBUS.
    /// </summary>
    public class Wybor_magazynuBUS
    {
        /// <summary>
        /// The DAO
        /// </summary>
        Wybor_magazynuDAO _DAO;

        /// <summary>
        /// The v os
        /// </summary>
        private Wybor_magazynuVO[] _VOs = new Wybor_magazynuVO[0];//lista danych
        /// <summary>
        /// The v oi
        /// </summary>
        private Wybor_magazynuVO _VOi = new Wybor_magazynuVO();       //dane na pozycji _idx
        /// <summary>
        /// The index
        /// </summary>
        private int _idx = 0;                       //indeks pozycji
        /// <summary>
        /// The EOF
        /// </summary>
        private bool _eof = false;                  //wskaźnik końca pliku
        /// <summary>
        /// The count
        /// </summary>
        private int _count = 0;                     //liczba pozycji

        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">ConnectionString.</param>
        public Wybor_magazynuBUS(string connString)
        {
            _DAO = new Wybor_magazynuDAO(connString);
            _error = _DAO._error;
        }//Wybor_magazynuBUS

        /// <summary>
        /// Wypełnia tablice.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            Wybor_magazynuVO VOi;
            _VOs = new Wybor_magazynuVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);

                VOi = new Wybor_magazynuVO();

                VOi.Magazyn = dr["Magazyn"].ToString();

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
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        /// <value>The count.</value>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        /// <value>The vo.</value>
        public Wybor_magazynuVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new Wybor_magazynuVO();
            }
        }//Wybor_magazynuVO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        /// <value>The index.</value>
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
        /// <param name="Magazyn">The magazyn.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool exists(String Magazyn)
        {
            foreach (Wybor_magazynuVO VOi in _VOs)
            {
                if (VOi.Magazyn == Magazyn) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Magazyn">The magazyn.</param>
        /// <returns>System.Int32.</returns>
        public int getIdx(string Magazyn)
        {
            int idx = -1;
            foreach (Wybor_magazynuVO VOi in _VOs)
            {
                idx++;
                if (VOi.Magazyn == Magazyn) return idx;
            }
            return -1;
        }//getIdx
    }//Wybor_magazynuBUS


    ////////////////////////////////////////////////////////////////////////////////// Jednostka_miar - dane słownikowe.
    /// <summary>
    /// Klasa wymiany danych z tabelą Jednostka_miar
    /// </summary>
    public class Jednostka_miarVO
    {
        /// <summary>
        /// The nazwa jednostka miar
        /// </summary>
        private string _Nazwa_jednostka_miar = string.Empty;
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Jednostka_miar
        /// </summary>
        public Jednostka_miarVO() { }
        /// <summary>
        /// Gets or sets the nazwa jednostka miar.
        /// </summary>
        /// <value>The nazwa jednostka miar.</value>
        public string Nazwa_jednostka_miar
        {
            get { return _Nazwa_jednostka_miar; }
            set { _Nazwa_jednostka_miar = value; }
        }
    }//class Jednostak_miarVO

    /// <summary>
    /// Class Jednostka_miarDAO.
    /// </summary>
    public class Jednostka_miarDAO
    {
        /// <summary>
        /// The connection
        /// </summary>
        private dbConnection _conn;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Jednostka_miarDAO"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        /// <constructor>
        /// Konstruktor.
        /// </constructor>
        public Jednostka_miarDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }// Jednostka_miarDAO
        /// <summary>
        /// Selects this instance.
        /// </summary>
        /// <returns>DataTable.</returns>
        public DataTable select()
        {
            string query = "SELECT * FROM Jednostka_miar;";
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

    }//class Jednostka_miarDAO

    /// <summary>
    /// Class Jednostka_miarBUS.
    /// </summary>
    public class Jednostka_miarBUS
    {
        /// <summary>
        /// The DAO
        /// </summary>
        Jednostka_miarDAO _DAO;

        /// <summary>
        /// The v os
        /// </summary>
        private Jednostka_miarVO[] _VOs = new Jednostka_miarVO[0];//lista danych
        /// <summary>
        /// The v oi
        /// </summary>
        private Jednostka_miarVO _VOi = new Jednostka_miarVO();       //dane na pozycji _idx
        /// <summary>
        /// The index
        /// </summary>
        private int _idx = 0;                       //indeks pozycji
        /// <summary>
        /// The EOF
        /// </summary>
        private bool _eof = false;                  //wskaźnik końca pliku
        /// <summary>
        /// The count
        /// </summary>
        private int _count = 0;                     //liczba pozycji

        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">ConnectionString.</param>
        public Jednostka_miarBUS(string connString)
        {
            _DAO = new Jednostka_miarDAO(connString);
            _error = _DAO._error;
        }//KategoriaBUS

        /// <summary>
        /// Wypełnia tablice.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            Jednostka_miarVO VOi;
            _VOs = new Jednostka_miarVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);

                VOi = new Jednostka_miarVO();

                VOi.Nazwa_jednostka_miar = dr["Nazwa_jednostka_miar"].ToString();

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
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        /// <value>The count.</value>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        /// <value>The vo.</value>
        public Jednostka_miarVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new Jednostka_miarVO();
            }
        }//VO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        /// <value>The index.</value>
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
        /// <param name="Nazwa_jednostka_miar">The nazwa jednostka miar.</param>
        /// <returns>Wynik logiczny sprawdzenia.</returns>
        public bool exists(String Nazwa_jednostka_miar)
        {
            foreach (Jednostka_miarVO VOi in _VOs)
            {
                if (VOi.Nazwa_jednostka_miar == Nazwa_jednostka_miar) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Nazwa_jednostka_miar">Identyfikator Jednostka_miar</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora jednostki miar.</returns>
        public int getIdx(string Nazwa_jednostka_miar)
        {
            int idx = -1;
            foreach (Jednostka_miarVO VOi in _VOs)
            {
                idx++;
                if (VOi.Nazwa_jednostka_miar == Nazwa_jednostka_miar) return idx;
            }

            return -1;
        }//getIdx
    }//class Jednostka_miarBUS

    /////////////////////////////////////////////////////////////////////////// Rodzaj mat - dane słownikowe.
    /// <summary>
    /// Klasa wymiany danych z tabelą Rodzaj materiału.
    /// </summary>
    public class Rodzaj_matVO
    {
        /// <summary>
        /// The nazwa rodzaj mat
        /// </summary>
        private string _Nazwa_rodzaj_mat; // 255

        /// <summary>
        /// Konstruktor wymiany danych z tabelą Rodzaj_mat
        /// </summary>
        public Rodzaj_matVO() { }

        /// <summary>
        /// Gets or sets the nazwa rodzaj mat.
        /// </summary>
        /// <value>The nazwa rodzaj mat.</value>
        public string Nazwa_rodzaj_mat
        {
            get { return _Nazwa_rodzaj_mat; }
            set { _Nazwa_rodzaj_mat = value; }
        }
    }//class Rodzaj_matVO

    //Klasa dostępu (Data Access Object) do tabeli Rodzaj_mat.
    /// <summary>
    /// Class Rodzaj_matDAO.
    /// </summary>
    public class Rodzaj_matDAO
    {
        /// <summary>
        /// The connection
        /// </summary>
        private dbConnection _conn;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        public Rodzaj_matDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }// Rodzaj_matDAO

        /// <summary>
        /// Selects this instance.
        /// </summary>
        /// <returns>DataTable.</returns>
        public DataTable select()
        {
            string query = "SELECT * from Rodzaj_mat;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }// select

    }// class Rodzaj_matDAO
     // Warstwa operacji biznesowaych tabeli Rodzaj_mat --> BUS.
     /// <summary>
     /// Class Rodzaj_matBUS.
     /// </summary>
    public class Rodzaj_matBUS
    {
        /// <summary>
        /// The DAO
        /// </summary>
        Rodzaj_matDAO _DAO;

        /// <summary>
        /// The v os
        /// </summary>
        private Rodzaj_matVO[] _VOs = new Rodzaj_matVO[0];    //lista danych
        /// <summary>
        /// The v oi
        /// </summary>
        private Rodzaj_matVO _VOi = new Rodzaj_matVO();       //dane na pozycji _idx
        /// <summary>
        /// The index
        /// </summary>
        private int _idx = 0;                       //indeks pozycji
        /// <summary>
        /// The EOF
        /// </summary>
        private bool _eof = false;                  //wskaźnik końca pliku
        /// <summary>
        /// The count
        /// </summary>
        private int _count = 0;                     //liczba pozycji

        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">ConnectionString.</param>
        public Rodzaj_matBUS(string connString)
        {
            _DAO = new Rodzaj_matDAO(connString);
            _error = _DAO._error;
        }//Rodzaj_matBUS

        /// <summary>
        /// Wypełnia tablice.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            Rodzaj_matVO VOi;
            _VOs = new Rodzaj_matVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);

                VOi = new Rodzaj_matVO();

                VOi.Nazwa_rodzaj_mat = dr["Nazwa_rodzaj_mat"].ToString();

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
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        /// <value>The count.</value>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        /// <value>The vo.</value>
        public Rodzaj_matVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new Rodzaj_matVO();
            }
        }//VO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        /// <value>The index.</value>
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
        /// <param name="Nazwa_rodzaj_mat">The nazwa rodzaj mat.</param>
        /// <returns>Wynik logiczny sprawdzenia.</returns>
        public bool exists(String Nazwa_rodzaj_mat)
        {
            foreach (Rodzaj_matVO VOi in _VOs)
            {
                if (VOi.Nazwa_rodzaj_mat == Nazwa_rodzaj_mat) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Nazwa_rodzaj_mat">Identyfikator Nazwa_rodzaj_mat</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora Nazwa_rodzaj_mat.</returns>
        public int getIdx(string Nazwa_rodzaj_mat)
        {
            int idx = -1;
            foreach (Rodzaj_matVO VOi in _VOs)
            {
                idx++;
                if (VOi.Nazwa_rodzaj_mat == Nazwa_rodzaj_mat) return idx;
            }

            return -1;
        }//getIdx
    }//class Rodzaj_matBUS
    /// <summary>
    /// </summary>
    public class Dostawca_MaterialVO
    {
        /// <summary>
        /// The identyfikator
        /// </summary>
        private int _Identyfikator = -1;
        /// <summary>
        /// The identifier material
        /// </summary>
        private int _ID_material = 0;
        /// <summary>
        /// The identifier dostawca mat
        /// </summary>
        private int _ID_dostawca_mat = 0;
        /// <summary>
        /// The material nazwa
        /// </summary>
        private string _Material_nazwa = string.Empty;
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Dostawca_MaterialVO
        /// </summary>
        public Dostawca_MaterialVO() { }

        /// <summary>
        /// Gets or sets the identyfikator.
        /// </summary>
        /// <value>The identyfikator.</value>
        public int Identyfikator
        {
            get { return _Identyfikator; }
            set { _Identyfikator = value; }
        }
        /// <summary>
        /// Gets or sets the identifier material.
        /// </summary>
        /// <value>The identifier material.</value>
        public int ID_material
        {
            get { return _ID_material; }
            set { _ID_material = value; }
        }
        /// <summary>
        /// Gets or sets the identifier dostawca mat.
        /// </summary>
        /// <value>The identifier dostawca mat.</value>
        public int ID_dostawca_mat
        {
            get { return _ID_dostawca_mat; }
            set { _ID_dostawca_mat = value; }
        }
        /// <summary>
        /// Gets or sets the material nazwa.
        /// </summary>
        /// <value>The material nazwa.</value>
        public string Material_nazwa
        {
            get { return _Material_nazwa; }
            set { _Material_nazwa = value; }
        }
    }//class Dostawca_MaterialVO
     // // // // // // // // // // // // klasa dostępu (Data Access Object) do tabeli Dostawca_Material.
     /// <summary>
     /// Class Dostawca_MaterialDAO.
     /// </summary>
    public class Dostawca_MaterialDAO
    {
        /// <summary>
        /// The connection
        /// </summary>
        private dbConnection _conn;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dostawca_MaterialDAO"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        /// <constructor>
        /// Konstruktor.
        /// </constructor>
        public Dostawca_MaterialDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }//Dostawca_MaterialDAO

        // -------------------------------------> dowolne zapytanie z poziomu Form
        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>DataTable.</returns>
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
         /// <returns>Tabela Dostawca_Material.</returns>
        public DataTable select()
        {
            string query = "SELECT * FROM Dostawca_Material;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="ID_material">The identifier material.</param>
        /// <returns>Tabela Dostawca_Material</returns>
        public DataTable select(int ID_material)
        {
            string query = "SELECT * FROM Dostawca_Material WHERE ID_material = " + ID_material.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Selects the specified identifier material.
        /// </summary>
        /// <param name="ID_material">The identifier material.</param>
        /// <param name="ID_dostawca_mat">The identifier dostawca mat.</param>
        /// <returns>DataTable.</returns>
        public DataTable select(int ID_material, int ID_dostawca_mat)
        {
            string query = "SELECT * FROM Dostawca_Material WHERE ID_material = " + ID_material.ToString() + " AND ID_dostawca_mat = " + ID_dostawca_mat.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Selects the dostawca.
        /// </summary>
        /// <param name="ID_dostawca">The identifier dostawca.</param>
        /// <returns>DataTable.</returns>
        public DataTable selectDostawca(int ID_dostawca)
        {
            string query = "SELECT * FROM Dostawca_Material WHERE ID_dostawca = " + ID_dostawca.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }// selectDostawca

        /// <summary>
        /// Wprowadza nowy rekord
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych (insert)</param>
        /// <returns>Wartość logiczna powodzenia operacji</returns>
        public bool insert(nsAccess2DB.Dostawca_MaterialVO VO)
        {
            string query = "INSERT INTO Dostawca_Material (ID_material, ID_dostawca_mat, Material_nazwa) " +
                "VALUES (@ID_material, @ID_dostawca_mat, @Material_nazwa)";

            OleDbParameter[] parameters = new OleDbParameter[3];
            parameters[0] = new OleDbParameter("ID_material", OleDbType.Integer);
            parameters[0].Value = VO.ID_material;

            parameters[1] = new OleDbParameter("ID_dostawca_mat", OleDbType.Integer);
            parameters[1].Value = VO.ID_dostawca_mat;

            parameters[2] = new OleDbParameter("Material_nazwa", OleDbType.VarChar, 255);
            parameters[2].Value = VO.Material_nazwa;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }// insert

        /// <summary>
        /// Aktualizuje rekord z wyjątkiem ID.
        /// </summary>
        /// <param name="VO">obiekt wymiany danych</param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool update(nsAccess2DB.Dostawca_MaterialVO VO)
        {
            string query = "UPDATE Dostawca_Material SET ID_material = @ID_material, ID_dostawca_mat = @ID_dostawca_mat, Material_nazwa = @Material_nazwa WHERE ID_material = " + VO.ID_material.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[3];
            parameters[0] = new OleDbParameter("ID_material", OleDbType.Integer);
            parameters[0].Value = VO.ID_material;

            parameters[1] = new OleDbParameter("ID_dostawca_mat", OleDbType.Integer);
            parameters[1].Value = VO.ID_dostawca_mat;

            parameters[2] = new OleDbParameter("Material_nazwa", OleDbType.VarChar, 255);
            parameters[2].Value = VO.Material_nazwa;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }// update

        /// <summary>
        /// usuwa rekord
        /// </summary>
        /// <param name="ID_material">The identifier material.</param>
        /// <returns>Wartośc logiczna powodzenia operacji.</returns>
        public bool delete(int ID_material)
        {
            string query = "DELETE * FROM Dostawca_Material WHERE ID_material = " + ID_material.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }// delete

        /// <summary>
        /// usuwa rekord
        /// </summary>
        /// <param name="ID_material">The identifier material.</param>
        /// <param name="ID_dostawca_mat">The identifier dostawca mat.</param>
        /// <returns>Wartośc logiczna powodzenia operacji.</returns>
        public bool delete(int ID_material, int ID_dostawca_mat)
        {
            string query = "DELETE * FROM Dostawca_Material WHERE ID_material = " + ID_material.ToString() +
                " AND ID_dostawca_mat = " + ID_dostawca_mat.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }// delete
    }//class Dostawca_MaterialDAO

    // // // // // // // // Warstwa operacji biznesowych tabeli Dostawca_Material BUS.
    /// <summary>
    /// Class Dostawca_MaterialBUS.
    /// </summary>
    public class Dostawca_MaterialBUS
    {
        /// <summary>
        /// The DAO
        /// </summary>
        Dostawca_MaterialDAO _DAO;

        /// <summary>
        /// The v os
        /// </summary>
        private Dostawca_MaterialVO[] _VOs = new Dostawca_MaterialVO[0]; // lista danych
        /// <summary>
        /// The v oi
        /// </summary>
        private Dostawca_MaterialVO _VOi = new Dostawca_MaterialVO(); // dane na pozycji _idx
        /// <summary>
        /// The index
        /// </summary>
        private int _idx = 0;
        /// <summary>
        /// The EOF
        /// </summary>
        private bool _eof = false;
        /// <summary>
        /// The count
        /// </summary>
        private int _count = 0;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        public Dostawca_MaterialBUS(string connString)
        {
            _DAO = new Dostawca_MaterialDAO(connString);
            _error = _DAO._error;
        } //operator Dostawca_MaterialBUS

        /// <summary>
        /// Wypełnia tablicę danych pozycjami.
        /// </summary>
        public void select()
        {
            fillTable(_DAO.select());
        }// select

        /// <summary>
        /// Wypełnia tablicę danych pozycjami.
        /// </summary>
        /// <param name="ID_material">The identifier material.</param>
        public void select(int ID_material)
        {
            fillTable(_DAO.select(ID_material));
        }// select

        /// <summary>
        /// Wypełnia tablicę danych pozycjami.
        /// </summary>
        /// <param name="ID_material">The identifier material.</param>
        /// <param name="ID_dostawca_mat">The identifier dostawca mat.</param>
        public void select(int ID_material, int ID_dostawca_mat)
        {
            fillTable(_DAO.select(ID_material, ID_dostawca_mat));
        }// select

        /// <summary>
        /// Selects the dostawca.
        /// </summary>
        /// <param name="ID_dostawca">The identifier dostawca.</param>
        public void selectDostawca(int ID_dostawca)
        {
            fillTable(_DAO.selectDostawca(ID_dostawca));
        }// selectDostawca

        /// <summary>
        /// Wypełnia tablicę pozycjami danych ---&gt; dowolne zapytanie z poziomu Form
        /// </summary>
        /// <param name="query">The query.</param>
        public void selectQuery(string query)
        {
            fillTable(_DAO.selectQuery(query));
        }// selectQuery

        /// <summary>
        /// Wprowadza rekord do tabeli.
        /// </summary>
        /// <param name="ID_material">The identifier material.</param>
        /// <param name="ID_dostawca_mat">The identifier dostawca mat.</param>
        /// <returns>Wartośc logiczna powodzenia operacji.</returns>
        public bool insert(int ID_material, int ID_dostawca_mat)
        {
            Dostawca_MaterialVO VO = new Dostawca_MaterialVO();
            VO.ID_dostawca_mat = ID_dostawca_mat;
            VO.ID_material = ID_material;

            add(VO, ref _VOs);
            return _DAO.insert(VO);
        }// insert

        /// <summary>
        /// Inserts the specified identifier material.
        /// </summary>
        /// <param name="ID_material">The identifier material.</param>
        /// <param name="ID_dostawca_mat">The identifier dostawca mat.</param>
        /// <param name="Material_nazwa">The material nazwa.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool insert(int ID_material, int ID_dostawca_mat, string Material_nazwa)
        {
            Dostawca_MaterialVO VO = new Dostawca_MaterialVO();
            VO.ID_dostawca_mat = ID_dostawca_mat;
            VO.ID_material = ID_material;
            VO.Material_nazwa = Material_nazwa;

            add(VO, ref _VOs);
            return _DAO.insert(VO);
        }
        /// <summary>
        /// Aktualizuje rekord z wyjatkiem numeru protokołu.
        /// </summary>
        /// <param name="VO">The vo.</param>
        /// <returns>Wartośc logiczna powodzenia akcji.</returns>
        private bool update(nsAccess2DB.Dostawca_MaterialVO VO)
        {
            bool b = _DAO.update(VO);
            _error = _DAO._error;
            return b;
        }// update

        /// <summary>
        /// Usuwa z tabeli pozycję o wskazanych parametrach.
        /// </summary>
        /// <param name="ID_material">The identifier material.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        public bool delete(int ID_material)
        {
            return _DAO.delete(ID_material);
        }// delete

        /// <summary>
        /// Wypełnia tablicę
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            Dostawca_MaterialVO VOi;
            _VOs = new Dostawca_MaterialVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);

                VOi = new Dostawca_MaterialVO();

                VOi.Identyfikator = int.Parse(dr["Identyfikator"].ToString());
                VOi.ID_dostawca_mat = int.Parse(dr["ID_dostawca_mat"].ToString());
                VOi.ID_material = int.Parse(dr["ID_material"].ToString());
                VOi.Material_nazwa = dr["Material_nazwa"].ToString();
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
        }// fillTable


        // <summary>
        /// <summary>
        /// Skips this instance.
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
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        /// <value>The count.</value>
        public int count
        {
            get { return _count; }
        }

        /// <summary>
        /// Zwraca dane okreslone wskaźnikiem pozycji.
        /// </summary>
        /// <value>The vo.</value>
        public Dostawca_MaterialVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new Dostawca_MaterialVO();
            }
        }// VO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        /// <value>The index.</value>
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
        /// Exists the specified identifier material.
        /// </summary>
        /// <param name="ID_material">The identifier material.</param>
        /// <param name="ID_dostawca_mat">The identifier dostawca mat.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool exist(int ID_material, int ID_dostawca_mat)
        {
            foreach (Dostawca_MaterialVO VOi in _VOs)
            {
                if (VOi.ID_material == ID_material & VOi.ID_dostawca_mat == VOi.ID_dostawca_mat)
                    return true;
            }
            return false;
        }// exist

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="ID_material">ID_materialu z tabeli Dostawca_Material.</param>
        /// <returns>System.Int32.</returns>
        public int getIdx(int ID_material)
        {
            int idx = -1;
            foreach (Dostawca_MaterialVO VOi in _VOs)
            {
                idx++;
                if (VOi.ID_material == ID_material)
                {
                    return idx;
                }
            }
            return -1;
        }// getIdx

        /// <summary>
        /// Gets the vo.
        /// </summary>
        /// <param name="ID_material">The identifier material.</param>
        /// <returns>Dostawca_MaterialVO.</returns>
        public Dostawca_MaterialVO GetVO(int ID_material)
        {
            foreach (nsAccess2DB.Dostawca_MaterialVO VO in _VOs)
            {
                if (VO.ID_material == ID_material)
                {
                    return VO;
                }
            }
            return new Dostawca_MaterialVO();
        }// GetVO


        /*
        public bool write(nsAccess2DB.Dostawca_MaterialVO VO)
        {
            if (exist(VO.ID_material))
            {
                return _DAO.update(VO);
            }
            return _DAO.insert(VO);
        }// write
        */

        /// <summary>
        /// Dodaje pozycje tabeli Dostawca_Material.
        /// </summary>
        /// <param name="VO">The vo.</param>
        /// <param name="VOs">The v os.</param>
        private void add(Dostawca_MaterialVO VO, ref Dostawca_MaterialVO[] VOs)
        {
            Array.Resize(ref _VOs, _VOs.Length + 1);
            _VOs[_VOs.Length - 1] = VO;
        }// add

    }// class Dostawca_MaterialBUS

    // // // // // // // // // // // // tabela Dostawca_mat
    /// <summary>
    /// Class Dostawca_matVO.
    /// </summary>
    public class Dostawca_matVO
    {
        /// <summary>
        /// The identyfikator
        /// </summary>
        private int _Identyfikator = -1;
        /// <summary>
        /// The nazwa dostawca mat
        /// </summary>
        private string _Nazwa_dostawca_mat = string.Empty; //255
        /// <summary>
        /// The link dostawca mat
        /// </summary>
        private string _Link_dostawca_mat = string.Empty; //255
        /// <summary>
        /// The dod information dostawca mat
        /// </summary>
        private string _Dod_info_dostawca_mat = string.Empty; //255 

        /// <summary>
        /// Konstruktor wymiany danych z tabelą Dostawca_mat
        /// </summary>
        public Dostawca_matVO() { }

        /// <summary>
        /// Gets or sets the identyfikator.
        /// </summary>
        /// <value>The identyfikator.</value>
        public int Identyfikator
        {
            get { return _Identyfikator; }
            set { _Identyfikator = value; }
        }
        /// <summary>
        /// Gets or sets the nazwa dostawca mat.
        /// </summary>
        /// <value>The nazwa dostawca mat.</value>
        public string Nazwa_dostawca_mat
        {
            get {return _Nazwa_dostawca_mat;}
            set {_Nazwa_dostawca_mat = value;}
        }
        /// <summary>
        /// Gets or sets the link dostawca mat.
        /// </summary>
        /// <value>The link dostawca mat.</value>
        public string Link_dostawca_mat
        {
            get { return _Link_dostawca_mat; }
            set { _Link_dostawca_mat = value; }
        }
        /// <summary>
        /// Gets or sets the dod information dostawca mat.
        /// </summary>
        /// <value>The dod information dostawca mat.</value>
        public string Dod_info_dostawca_mat
        {
            get { return _Dod_info_dostawca_mat; }
            set { _Dod_info_dostawca_mat = value; }
        }
    }// class Dostawca_matVO

    /// <summary>
    /// Klasa dostępu (Data Access Object) do tabeli Dostawca_mat
    /// </summary>
    public class Dostawca_matDAO
    {
        /// <summary>
        /// The connection
        /// </summary>
        private dbConnection _conn;
        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dostawca_matDAO"/> class.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        public Dostawca_matDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }// Dostawca_matDAO

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>DataTable.</returns>
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
        /// <returns>DataTable.</returns>
        public DataTable select()
        {
            string query = "SELECT * FROM Dostawca_mat;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Selects the specified identyfikator.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
        /// <returns>DataTable.</returns>
        public DataTable select(int Identyfikator)
        {
            string query = "SELECT * FROM Dostawca_mat WHERE Identyfikator = " + Identyfikator.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Inserts the specified vo.
        /// </summary>
        /// <param name="VO">The vo.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool insert(nsAccess2DB.Dostawca_matVO VO)
        {
            string query = "INSERT INTO Dostawca_mat (Nazwa_dostawca_mat, Link_dostawca_mat, Dod_info_dostawca_mat) VALUES (@Nazwa_dostawca_mat, @Link_dostawca_mat, @Dod_info_dostawca_mat);";

            OleDbParameter[] parameters = new OleDbParameter[3];

            parameters[0] = new OleDbParameter("Nazwa_dostawca_mat", OleDbType.VarChar, 255);
            parameters[0].Value = VO.Nazwa_dostawca_mat;

            parameters[1] = new OleDbParameter("Link_dostawca_mat", OleDbType.VarChar, 255);
            parameters[1].Value = VO.Link_dostawca_mat;

            parameters[2] = new OleDbParameter("Dod_info_dostawca_mat", OleDbType.VarChar, 255);
            parameters[2].Value = VO.Dod_info_dostawca_mat;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//insert

        /// <summary>
        /// Aktualizuje rekord z wyjątkiem Identyfikatora.
        /// </summary>
        /// <param name="VO">The vo.</param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool update(nsAccess2DB.Dostawca_matVO VO)
        {
            string query = "UPDATE Dostawca_mat SET Nazwa_dostawca_mat = @Nazwa_dostawca_mat, Link_dostawca_mat = @Link_dostawca_mat, Dod_info_dostawca_mat = @Dod_info_dostawca_mat WHERE Identyfikator = " + VO.Identyfikator.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[3];

            parameters[0] = new OleDbParameter("Nazwa_dostawca_mat", OleDbType.VarChar, 255);
            parameters[0].Value = VO.Nazwa_dostawca_mat;

            parameters[1] = new OleDbParameter("Link_dostawca_mat", OleDbType.VarChar, 255);
            parameters[1].Value = VO.Link_dostawca_mat;

            parameters[2] = new OleDbParameter("Dod_info_dostawca_mat", OleDbType.VarChar, 255);
            parameters[2].Value = VO.Dod_info_dostawca_mat;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//update


        /// <summary>
        /// Deletes the specified identyfikator.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool delete(int Identyfikator)
        {
            string query = "DELETE * FROM Dostawca_mat WHERE Identyfikator = " + Identyfikator.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }// delete

    }// class Dostawca_matDAO

    /// <summary>
    /// Class Dostawca_matBUS.
    /// </summary>
    public class Dostawca_matBUS
    {
        /// <summary>
        /// The DAO
        /// </summary>
        Dostawca_matDAO _DAO;
        /// <summary>
        /// The v os
        /// </summary>
        private Dostawca_matVO[] _VOs = new Dostawca_matVO[0]; //lista danych
        /// <summary>
        /// The v oi
        /// </summary>
        private Dostawca_matVO _VOi = new Dostawca_matVO();        //dane na pozycji _idx
        /// <summary>
        /// The index
        /// </summary>
        private int _idx = 0;                           //indeks pozycji
        /// <summary>
        /// The EOF
        /// </summary>
        private bool _eof = false;                      //wskaźnik końca pliku
        /// <summary>
        /// The count
        /// </summary>
        private int _count = 0;                         //liczba pozycji

        /// <summary>
        /// The error
        /// </summary>
        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">ConnectionString.</param>
        public Dostawca_matBUS(string connString)
        {
            _DAO = new Dostawca_matDAO(connString);
            _error = _DAO._error;
        }// konstruktor Dostawca_matBUS

        /// <summary>
        /// Wypełnia tablice danych pozycjami.
        /// </summary>
        public void select()
        {
            fillTable(_DAO.select());
        }// select

        /// <summary>
        /// Wypełnia tablice danych pozycjami.
        /// </summary>
        /// <param name="Identyfikator">Identyfikator Dostawcy materiału.</param>
        public void select(int Identyfikator)
        {
            fillTable(_DAO.select(Identyfikator));
        }// select

        /// <summary>
        /// Dowolne zapytanie z formularza.
        /// </summary>
        /// <param name="query">The query.</param>
        public void selectQuery(string query)
        {
            fillTable(_DAO.selectQuery(query));
        }// selectQuery

        /// <summary>
        /// Wprowadza rekord do tabeli.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        public bool insert(nsAccess2DB.Dostawca_matVO VO)
        {
            bool b = _DAO.insert(VO);
            _error = _DAO._error;
            return b;
        }// insert

        /// <summary>
        /// Aktualizuje rekord z wyjątkiem Identyfikatora Dostawcy materiału.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        public bool update(nsAccess2DB.Dostawca_matVO VO)
        {
            bool b = _DAO.insert(VO);
            _error = _DAO._error;
            return b;
        }// update

        /// <summary>
        /// Usuwa rekord po identyfikatorze.
        /// </summary>
        /// <param name="Identyfikator">The identyfikator.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool delete(int Identyfikator)
        {
            return _DAO.delete(Identyfikator);
        }//delete

        /// <summary>
        /// Wypełnia tablice Dostawca Materiału.
        /// </summary>
        /// <param name="dt">The dt.</param>
        private void fillTable(DataTable dt)
        {
            Dostawca_matVO VOi;
            _VOs = new Dostawca_matVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);
                VOi = new Dostawca_matVO();

                VOi.Identyfikator = int.Parse(dr["Identyfikator"].ToString());
                VOi.Nazwa_dostawca_mat = dr["Nazwa_dostawca_mat"].ToString();
                VOi.Link_dostawca_mat = dr["Link_dostawca_mat"].ToString();
                VOi.Dod_info_dostawca_mat = dr["Dod_info_dostawca_mat"].ToString();

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
        /// <value><c>true</c> if EOF; otherwise, <c>false</c>.</value>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        /// Zwraca liczbę pozycji tablicy.
        /// </summary>
        /// <value>The count.</value>
        public int count
        {
            get { return _count; }
        }//count

        /// <summary>
        /// Gets the vo.
        /// </summary>
        /// <value>The vo.</value>
        public Dostawca_matVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new Dostawca_matVO();
            }
        }// Dostawca_matVO

        /// <summary>
        /// Ustawia wskaźnik pozycji.
        /// </summary>
        /// <value>The index.</value>
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
        /// <param name="Identyfikator">ID dostawcy materiału.</param>
        /// <returns>Wynik logiczny sprawdzenia.</returns>
        private bool exists(int Identyfikator)
        {
            foreach (Dostawca_matVO VOi in _VOs)
            {
                if (VOi.Identyfikator == Identyfikator) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Identyfikator">ID dostawcy materiału.</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora.</returns>
        public int getIdx(int Identyfikator)
        {
            int idx = -1;
            foreach (Dostawca_matVO VOi in _VOs)
            {
                idx++;
                if (VOi.Identyfikator == Identyfikator) return idx;
            }

            return -1;
        }//getIdx

        /// <summary>
        /// Zwraca maszynę o wskazanym identyfikatorze.
        /// </summary>
        /// <param name="Identyfikator">Identyfikator dostawcy materiału.</param>
        public Dostawca_matVO GetVO(int Identyfikator)
        {
            foreach (nsAccess2DB.Dostawca_matVO VO in _VOs)
            {
                if (VO.Identyfikator == Identyfikator) return VO;
            }
            return new Dostawca_matVO();
        }//getVO

        /// <summary>
        /// Dodaje lub aktualizuje rekord.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wynik powodzenia akcji.</returns>
        public bool write(nsAccess2DB.Dostawca_matVO VO)
        {
            if (exists(VO.Identyfikator))
            {
                return _DAO.update(VO);
            }
            return _DAO.insert(VO);
        }//write

    }// Dostawca_matBUS

    ////////////////////////////////////////////////////////////////  Klasa wymiany danych z tabelą Przyrząd
    // -----------------------------------------------------------------> PrzyrzadVO
    /// <summary>
    /// Klasa wymiany danych z tabelą Przyrząd
    /// </summary>
    public class PrzyrzadVO
    {
        private int _Identyfikator = -1;
        private string _Nazwa_przyrzadu = string.Empty;//100
        private string _Typ_przyrzadu = string.Empty; //100
        private string _Rodzaj_przyrzadu = string.Empty;//100
        private string _Nr_fabryczny_przyrzadu = string.Empty;//100
        private string _Nr_systemowy_przyrzadu = string.Empty;//100
        private string _Dane_producenta_przyrzadu = string.Empty;//255
        private string _Nazwa_stanowiska = string.Empty;//100
        private int _Data_ost_przeg_przyrzadu = 0;
        private int _Rok_ost_przeg_przyrzadu = 0;
        private int _Mc_ost_przeg_przyrzadu = 0;
        private int _Dz_ost_przeg_przyrzadu = 0;
        private string _Opiekun_przyrzadu = string.Empty;
        private string _Zdjecie_przyrzadu = string.Empty;//255
        private byte[] _Zawartosc_pliku_zdj_przyrzadu = new byte[] { }; // obiekt OLE - zdjęcie
        private string _Rozszerz_zdj_przyrzadu = string.Empty; // 255
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Przyrząd
        /// </summary>
        public PrzyrzadVO() { }
        public int Identyfikator
        {
            get { return _Identyfikator; }
            set { _Identyfikator = value; }
        }
        public string Nazwa_przyrzadu
        {
            get { return _Nazwa_przyrzadu; }
            set { _Nazwa_przyrzadu = value; }
        }
        public string Typ_przyrzadu
        {
            get { return _Typ_przyrzadu; }
            set { _Typ_przyrzadu = value; }
        }
        public string Rodzaj_przyrzadu
        {
            get { return _Rodzaj_przyrzadu; }
            set { _Rodzaj_przyrzadu = value; }
        }
        public string Nr_fabryczny_przyrzadu
        {
            get { return _Nr_fabryczny_przyrzadu; }
            set { _Nr_fabryczny_przyrzadu = value; }
        }
        public string Nr_systemowy_przyrzadu
        {
            get { return _Nr_systemowy_przyrzadu; }
            set { _Nr_systemowy_przyrzadu = value; }
        }
        public string Dane_producenta_przyrzadu
        {
            get { return _Dane_producenta_przyrzadu; }
            set { _Dane_producenta_przyrzadu = value; }
        }
        public string Nazwa_stanowiska
        {
            get { return _Nazwa_stanowiska; }
            set { _Nazwa_stanowiska = value; }
        }
        public int Data_ost_przeg_przyrzadu
        {
            get { return _Data_ost_przeg_przyrzadu; }
            set { _Data_ost_przeg_przyrzadu = value; }
        }
        public int Rok_ost_przeg_przyrzadu
        {
            get { return _Rok_ost_przeg_przyrzadu; }
            set { _Rok_ost_przeg_przyrzadu = value; }
        }
        public int Mc_ost_przeg_przyrzadu
        {
            get { return _Mc_ost_przeg_przyrzadu; }
            set { _Mc_ost_przeg_przyrzadu = value; }
        }
        public int Dz_ost_przeg_przyrzadu
        {
            get { return _Dz_ost_przeg_przyrzadu; }
            set { _Dz_ost_przeg_przyrzadu = value; }
        }
        public string Opiekun_przyrzadu
        {
            get { return _Opiekun_przyrzadu; }
            set { _Opiekun_przyrzadu = value; }
        }
        public string Zdjecie_przyrzadu
        {
            get { return _Zdjecie_przyrzadu; }
            set { _Zdjecie_przyrzadu = value; }
        }
        public byte[] Zawartosc_pliku_zdj_przyrzadu
        {
            get { return _Zawartosc_pliku_zdj_przyrzadu; }
            set { _Zawartosc_pliku_zdj_przyrzadu = value; }
        }
         public string Rozszerz_zdj_przyrzadu
        {
            get { return _Rozszerz_zdj_przyrzadu; }
            set { _Rozszerz_zdj_przyrzadu = value; }
        }
    }// PrzyrzadVO
    
    // Klasa dostępu do tabeli Przyrzad (Data Access Object) ---------------------------->DAO
    /// <summary>
    /// Klasa PrzyrządDAO
    /// </summary>
    public class PrzyrzadDAO
    {
        private dbConnection _conn;
        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString"></param>
        public PrzyrzadDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        } //konstruktor PrzyrzadDAO

        /// <summary>
        /// Selects the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>DataTable.</returns>
        public DataTable SelectQuery(string query)
        {
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//selectQuery
         
        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <returns>DataTable.</returns>
        public DataTable Select()
        {
            string query = "SELECT * FROM Przyrzad;";
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select
         /// <summary>
         /// Selects the specified identyfikator.
         /// </summary>
         /// <param name="Identyfikator">The identyfikator.</param>
         /// <returns>DataTable.</returns>
        public DataTable Select(int Identyfikator)
        {
            string query = "SELECT * FROM Przyrzad WHERE Identyfikator = " + Identyfikator.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Wprowadza nowy rekord do tabeli Przyrzad.
        /// </summary>
        /// <param name="VO"></param>
        /// <returns></returns>
        public bool Insert(nsAccess2DB.PrzyrzadVO VO)
        {
            string query = "INSERT INTO Przyrzad (Nazwa_przyrzadu, Typ_przyrzadu, Rodzaj_przyrzadu, Nr_fabryczny_przyrzadu, Nr_systemowy_przyrzadu, Dane_producenta_przyrzadu, Nazwa_stanowiska, Data_ost_przeg_przyrzadu, Rok_ost_przeg_przyrzadu," +
                " Mc_ost_przeg_przyrzadu, Dz_ost_przeg_przyrzadu, Opiekun_przyrzadu, Zdjecie_przyrzadu, Zawartosc_pliku_zdj_przyrzadu, Rozszerz_zdj_przyrzadu);";

            OleDbParameter[] parameters = new OleDbParameter[15];
            parameters[0] = new OleDbParameter("Nazwa_przyrzadu", OleDbType.VarChar, 100);
            parameters[0].Value = VO.Nazwa_przyrzadu;

            parameters[1] = new OleDbParameter("Typ_przyrzadu", OleDbType.VarChar, 100);
            parameters[1].Value = VO.Typ_przyrzadu;

            parameters[2] = new OleDbParameter("Rodzaj_przyrzadu", OleDbType.VarChar, 100);
            parameters[2].Value = VO.Rodzaj_przyrzadu;

            parameters[3] = new OleDbParameter("Nr_fabryczny_przyrzadu", OleDbType.VarChar, 100);
            parameters[3].Value = VO.Nr_fabryczny_przyrzadu;

            parameters[4] = new OleDbParameter("Nr_systemowy_przyrzadu", OleDbType.VarChar, 100);
            parameters[4].Value = VO.Nr_systemowy_przyrzadu;

            parameters[5] = new OleDbParameter("Dane_producenta_przyrzadu", OleDbType.VarChar, 255);
            parameters[5].Value = VO.Dane_producenta_przyrzadu;

            parameters[6] = new OleDbParameter("Nazwa_stanowiska", OleDbType.VarChar, 100);
            parameters[6].Value = VO.Nazwa_stanowiska;

            parameters[7] = new OleDbParameter("Data_ost_przeg_przyrzadu", OleDbType.Integer);
            parameters[7].Value = VO.Data_ost_przeg_przyrzadu;

            parameters[8] = new OleDbParameter("Rok_ost_przeg_przyrzadu", OleDbType.Integer);
            parameters[8].Value = VO.Rok_ost_przeg_przyrzadu;

            parameters[9] = new OleDbParameter("Mc_ost_przeg_przyrzadu", OleDbType.Integer);
            parameters[9].Value = VO.Mc_ost_przeg_przyrzadu;

            parameters[10] = new OleDbParameter("Dz_ost_przeg_przyrzadu", OleDbType.Integer);
            parameters[10].Value = VO.Nazwa_przyrzadu;

            parameters[11] = new OleDbParameter("Opiekun_przyrzadu", OleDbType.VarChar, 100);
            parameters[11].Value = VO.Opiekun_przyrzadu;

            parameters[12] = new OleDbParameter("Zdjecie_przyrzadu", OleDbType.VarChar, 100);
            parameters[12].Value = VO.Zdjecie_przyrzadu;

            parameters[13] = new OleDbParameter("Zawartosc_pliku_zdj_przyrzadu", OleDbType.VarBinary);
            parameters[13].Value = VO.Zawartosc_pliku_zdj_przyrzadu;

            parameters[14] = new OleDbParameter("Rozszerz_zdj_przyrzadu", OleDbType.VarChar, 255);
            parameters[14].Value = VO.Rozszerz_zdj_przyrzadu;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//insert

        /// <summary>
        /// Aktualizauje rekord tabeli Przyrzad.
        /// </summary>
        /// <param name="VO"></param>
        /// <returns></returns>
        public bool Update(nsAccess2DB.PrzyrzadVO VO)
        {
            string query = "UPDATE Przyrzad SET " +
                "Nazwa_przyrzadu = @Nazwa_przyrzadu, " +
                "Typ_przyrzadu = @Typ_przyrzadu, " +
                "Rodzaj_przyrzadu = @Rodzaj_przyrzadu, " +
                "Nr_fabryczny_przyrzadu = @Nr_fabryczny_przyrzadu, " +
                "Nr_systemowy_przyrzadu = @Nr_systemowy_przyrzadu, " +
                "Dane_producenta_przyrzadu = @Dane_producenta_przyrzadu, " +
                "Nazwa_stanowiska = @Nazwa_stanowiska, " +
                "Data_ost_przeg_przyrzadu = @Data_ost_przeg_przyrzadu, " +
                "Rok_ost_przeg_przyrzadu = @Rok_ost_przeg_przyrzadu, " +
                "Mc_ost_przeg_przyrzadu = @Mc_ost_przeg_przyrzadu, " +
                "Dz_ost_przeg_przyrzadu = @Dz_ost_przeg_przyrzadu, " +
                "Opiekun_przyrzadu = @Opiekun_przyrzadu, " +
                "Zdjecie_przyrzadu = @Zdjecie_przyrzadu, " +
                "Zawartosc_pliku_zdj_przyrzadu = @Zawartosc_pliku_zdj_przyrzadu, " +
                "Rozszerz_zdj_przyrzadu = @Rozszerz_zdj_przyrzadu);";

            OleDbParameter[] parameters = new OleDbParameter[15];
            parameters[0] = new OleDbParameter("Nazwa_przyrzadu", OleDbType.VarChar, 100);
            parameters[0].Value = VO.Nazwa_przyrzadu;

            parameters[1] = new OleDbParameter("Typ_przyrzadu", OleDbType.VarChar, 100);
            parameters[1].Value = VO.Typ_przyrzadu;

            parameters[2] = new OleDbParameter("Rodzaj_przyrzadu", OleDbType.VarChar, 100);
            parameters[2].Value = VO.Rodzaj_przyrzadu;

            parameters[3] = new OleDbParameter("Nr_fabryczny_przyrzadu", OleDbType.VarChar, 100);
            parameters[3].Value = VO.Nr_fabryczny_przyrzadu;

            parameters[4] = new OleDbParameter("Nr_systemowy_przyrzadu", OleDbType.VarChar, 100);
            parameters[4].Value = VO.Nr_systemowy_przyrzadu;

            parameters[5] = new OleDbParameter("Dane_producenta_przyrzadu", OleDbType.VarChar, 255);
            parameters[5].Value = VO.Dane_producenta_przyrzadu;

            parameters[6] = new OleDbParameter("Nazwa_stanowiska", OleDbType.VarChar, 100);
            parameters[6].Value = VO.Nazwa_stanowiska;

            parameters[7] = new OleDbParameter("Data_ost_przeg_przyrzadu", OleDbType.Integer);
            parameters[7].Value = VO.Data_ost_przeg_przyrzadu;

            parameters[8] = new OleDbParameter("Rok_ost_przeg_przyrzadu", OleDbType.Integer);
            parameters[8].Value = VO.Rok_ost_przeg_przyrzadu;

            parameters[9] = new OleDbParameter("Mc_ost_przeg_przyrzadu", OleDbType.Integer);
            parameters[9].Value = VO.Mc_ost_przeg_przyrzadu;

            parameters[10] = new OleDbParameter("Dz_ost_przeg_przyrzadu", OleDbType.Integer);
            parameters[10].Value = VO.Nazwa_przyrzadu;

            parameters[11] = new OleDbParameter("Opiekun_przyrzadu", OleDbType.VarChar, 100);
            parameters[11].Value = VO.Opiekun_przyrzadu;

            parameters[12] = new OleDbParameter("Zdjecie_przyrzadu", OleDbType.VarChar, 100);
            parameters[12].Value = VO.Zdjecie_przyrzadu;

            parameters[13] = new OleDbParameter("Zawartosc_pliku_zdj_przyrzadu", OleDbType.VarBinary);
            parameters[13].Value = VO.Zawartosc_pliku_zdj_przyrzadu;

            parameters[14] = new OleDbParameter("Rozszerz_zdj_przyrzadu", OleDbType.VarChar, 255);
            parameters[14].Value = VO.Rozszerz_zdj_przyrzadu;

            bool b = _conn.executeInsertQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//update

        public bool Delete(int Identyfikator)
        {
            string query = "DELETE * FROM Przyrzad WHERE Identyfikator = " + Identyfikator.ToString() + ";";
            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//Delete

    }//PrzyrzadDAO

    // Warstwa operacji biznesowych tabeli Przyrzad -----> BUS
    /// <summary>
    /// Klasa PrzyrzadBUS
    /// </summary>
    public class PrzyrzadBUS
    {
        PrzyrzadDAO _DAO;
        private PrzyrzadVO[] _VOs = new PrzyrzadVO[0];  //lista danych
        private PrzyrzadVO _VOi = new PrzyrzadVO();       //dane na pozycji _idx
        private int _idx = 0;                           //indeks pozycji
        private bool _eof = false;                      //wskaźnik końca pliku
        private int _count = 0;                         //liczba pozycji
        public string _error = string.Empty;

        /// <summary>
        /// Kkonstruktor PrzyrzadBUS.
        /// </summary>
        /// <param name="connString"></param>
        public PrzyrzadBUS(string connString)
        {
            _DAO = new PrzyrzadDAO(connString);
            _error = _DAO._error;
        }// konstruktor PrzyrzadBUS

        /// <summary>
        /// Wypełnia tablice danych pozycjami.
        /// </summary>
        public void Select()
        {
            FillTable(_DAO.Select());
        }//select

        /// <summary>
        /// Wypełnia tablicę pozycjami.
        /// </summary>
        /// <param name="Identyfikator"></param>
        public void Select(int Identyfikator)
        {
            FillTable(_DAO.Select(Identyfikator));
        }//select
      
        /// <summary>
        /// Dowolne zapytanie z formularza.
        /// </summary>
        /// <param name="query">The query.</param>
        public void SelectQuery(string query)
        {
            FillTable(_DAO.SelectQuery(query));
        }//selectQuery

        /// <summary>
        /// Wprowadza rekord do tabeli.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        private bool Insert(nsAccess2DB.PrzyrzadVO VO)
        {
            bool b = _DAO.Insert(VO);
            _error = _DAO._error;
            return b;
        }//insert

        /// <summary>
        /// Aktualizuje rekord z wyjątkiem Identyfikatora Przyrzadu.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wartość logiczna powodzenia akcji.</returns>
        private bool Update(nsAccess2DB.PrzyrzadVO VO)
        {
            bool b = _DAO.Update(VO);
            _error = _DAO._error;
            return b;
        }//update

        /// <summary>
        /// Kasuje rekord wybrany po Identyfikatorze.
        /// </summary>
        /// <param name="Identyfikator"></param>
        /// <returns></returns>
        public bool Delete(int Identyfikator)
        {
            return _DAO.Delete(Identyfikator);
        }// delete

        /// <summary>
        /// Wypełnia tablice.
        /// </summary>
        /// <param name="dt"></param>
        private void FillTable(DataTable dt)
        {
            PrzyrzadVO VOi;
            _VOs = new PrzyrzadVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);
                VOi = new PrzyrzadVO();

                VOi.Identyfikator = int.Parse(dr["Identyfikator"].ToString());
                VOi.Nazwa_przyrzadu = dr["Nazwa_przyrzadu"].ToString();
                VOi.Typ_przyrzadu = dr["Typ_przyrzadu"].ToString();
                VOi.Rodzaj_przyrzadu = dr["Rodzaj_przyrzadu"].ToString();
                VOi.Nr_fabryczny_przyrzadu = dr["Nr_fabryczny_przyrzadu"].ToString();
                VOi.Nr_systemowy_przyrzadu = dr["Nr_systemowy_przyrzadu"].ToString();
                VOi.Dane_producenta_przyrzadu = dr["Dane_producenta_przyrzadu"].ToString();
                VOi.Nazwa_stanowiska = dr["Nazwa_stanowiska"].ToString();
                try
                {
                    VOi.Data_ost_przeg_przyrzadu = int.Parse(dr["Data_ost_przeg_przyrzadu"].ToString());
                }
                catch { }
                VOi.Rok_ost_przeg_przyrzadu = int.Parse(dr["Rok_ost_przeg_przyrzadu"].ToString());
                VOi.Mc_ost_przeg_przyrzadu = int.Parse(dr["Mc_ost_przeg_przyrzadu"].ToString());
                VOi.Dz_ost_przeg_przyrzadu = int.Parse(dr["Dz_ost_przeg_przyrzadu"].ToString());
                VOi.Opiekun_przyrzadu = dr["Opiekun_przyrzadu"].ToString();
                VOi.Zdjecie_przyrzadu = dr["Zdjecie_przyrzadu"].ToString();
                try
                {
                    VOi.Zawartosc_pliku_zdj_przyrzadu = (byte[])dr["Zawartosc_pliku"];
                }
                catch
                {
                    VOi.Zawartosc_pliku_zdj_przyrzadu = new byte[] { };
                }
                VOi.Rozszerz_zdj_przyrzadu = dr["Rozszerz_zdj_przyrzadu"].ToString();

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
        ///  Zmienna logiczna osiągnięcia końca pliku.
        /// </summary>
        public bool eof
        {
            get { return _eof; }
        }

        /// <summary>
        ///  Zwraca liczbę pozycji tablicy.
        /// </summary>
        public int count
        {
            get { return _count; }
        }//count

        /// <summary>
        /// Zwraca daną okrśloną wskaźnikiem pozycji.
        /// </summary>
        public PrzyrzadVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    _VOi = _VOs[_idx];
                    return _VOi;
                }
                return new PrzyrzadVO();
            }
        }//PrzyrzadVO

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
        /// <param name="Identyfikator"></param>
        /// <returns></returns>
        private bool Exists(int Identyfikator)
        {
            foreach (PrzyrzadVO VOi in _VOs)
            {
                if (VOi.Identyfikator == Identyfikator) return true;
            }
            return false;
        }//exist

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Identyfikator"></param>
        /// <returns></returns>
        public int getIdx(int Identyfikator)
        {
            int idx = -1;
            foreach (PrzyrzadVO VOi in _VOs)
            {
                idx++;
                if (VOi.Identyfikator == Identyfikator) return idx;
            }
            return -1;
        }//getIdx

        /// <summary>
        /// Zwraca maszynę o wskazanym identyfikatorze.
        /// </summary>
        /// <param name="Identyfikator"></param>
        /// <returns></returns>
        public PrzyrzadVO GetVO(int Identyfikator)
        {
            foreach (nsAccess2DB.PrzyrzadVO VO in _VOs)
            {
                if (VO.Identyfikator == Identyfikator) return VO;
            }
            return new PrzyrzadVO();
        }//getVO

        public bool write(nsAccess2DB.PrzyrzadVO VO)
        {
            if (Exists(VO.Identyfikator))
            {
                return _DAO.Update(VO);
            }
            return _DAO.Insert(VO);
        }//write

    }//PrzyrzadBUS


}//namespace nsAccess2DB
