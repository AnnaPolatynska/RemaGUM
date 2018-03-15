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
        private int _ID_maszyny = -1;
        private string _Kategoria = string.Empty; // 50
        private string _Nazwa = string.Empty; //100
        private string _Typ = string.Empty; //50
        private string _Nr_inwentarzowy = string.Empty; //50
        private string _Nr_fabryczny = string.Empty; //50
        private string _Rok_produkcji = string.Empty; //50
        private string _Producent = string.Empty; //50
        private string _Zdjecie1 = string.Empty; // ????????????????????????????????? zdjęcie
        private string _Rozszerz_zdj1 = string.Empty; //10
        private string _Zdjecie2 = string.Empty; // ????????????????????????????????? zdjęcie
        private string _Rozszerz_zdj2 = string.Empty; //10
        private string _Nr_GUM = string.Empty; //50
        private string _Nr_pom = string.Empty; //50
        private string _Dzial = string.Empty; //50
        private string _Nr_prot_BHP = string.Empty; //10
        private string _Data_ost_przegl = string.Empty; //50
        private string _Data_kol_przegl = string.Empty; //50
        private string _Uwagi = string.Empty; //255
        private string _Wykorzystanie = string.Empty; //50
        private string _Stan_techniczny = string.Empty; //50
        private string _Priorytet = string.Empty; //50
        private string _Propozycja = string.Empty; //50
        private int _Punktacja = -1;

        ///<summary>
        /// Konstruktor wymiany danych z tabelą Maszyny
        ///</summary>
        //public MaszynyVO (){   }
        public MaszynyVO() { }
        // gettery i settery
        public int ID_maszyny
        {
            get { return _ID_maszyny; }
            set { _ID_maszyny = value; }
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
        public string Zdjecie1
        {
            get { return _Zdjecie1; }
            set { _Zdjecie1 = value; }
        }

        public string Rozszerz_zdj1
        {
            get { return _Rozszerz_zdj1; }
            set { _Rozszerz_zdj1 = value; }
        }

        public string Zdjecie2
        {
            get { return _Zdjecie2; }
            set { _Zdjecie2 = value; }
        }
        public string Rozszerz_zdj2
        {
            get { return _Rozszerz_zdj2; }
            set { _Rozszerz_zdj2 = value; }
        }

        public string Nr_GUM {
            get { return _Nr_GUM; }
            set { _Nr_GUM = value; }
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
        public string Data_ost_przegl
        {
            get { return _Data_ost_przegl; }
            set { _Data_ost_przegl = value; }
        }
        public string Data_kol_przegl
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
        public string Priorytet {
            get { return _Priorytet; }
            set { _Priorytet = value; }
        }
        public string Propozycja
        {
            get { return _Propozycja; }
            set { _Propozycja = value; }
        }
        public int Punktacja
        {
            get { return _Punktacja; }
            set { _Punktacja = value; }
        }

    }// class MaszynyVO

    //klasa dostępu (Data Access Object) do tabeli Maszyny ----------> DAO
    public class MaszynyDAO {
        private dbConnection _conn;
        public string _error = string.Empty;

        ///<construktor>
        ///Konstruktor
        ///</construktor>
        public MaszynyDAO(string connString) {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        } //konstruktor MaszynyDAO

        /// <summary>
        /// Zwraca tabelę wszystkich Maszyn.
        /// </summary>
        /// <returns>Tabele.</returns>
        public DataTable select()
        {
            string query = "SELECT * FROM Maszyny;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

        /// <summary>
        /// Zwraca tabelę wszystkich Maszyn po ID.
        /// </summary>
        /// <returns>Tabele.</returns>
        public DataTable select(int ID_maszyny)
        {
            string query = "SELECT * FROM Maszyny WHERE ID_maszyny ='" + ID_maszyny.ToString() + "';";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select po ID

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="Nazwa">zwraca maszynę po nazwie</param>
        /// <returns>Tabela.</returns>
        public DataTable selectNazwa(string Nazwa)
        {
            string query = "SELECT * FROM Maszyny WHERE Nazwa = '" + Nazwa + "'ORDER BY Nazwa ASC;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//selectNazwa

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="Typ">zwraca maszynę po typie</param>
        /// <returns>Tabela.</returns>
        public DataTable selectTyp(string Typ)
        {
            string query = "SELECT * FROM Maszyny WHERE Typ = " + Typ + "order by Typ ASC;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//selectTyp

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="Nr_inwentarzowy">zwraca maszynę po Nr inwentarzowym</param>
        /// <returns>Tabela.</returns>
        public DataTable selectNr_inwentarzowy(string Nr_inwentarzowy)
        {
            string query = "SELECT * FROM Maszyny WHERE Nr_inwentarzowy = " + Nr_inwentarzowy + "order by Typ ASC;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//selectNr_inwentarzowy

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="Nr_fabryczny">zwraca maszynę po Nr fabrycznym</param>
        /// <returns>Tabela.</returns>
        public DataTable selectNr_fabryczny(string Nr_fabryczny)
        {
            string query = "SELECT * FROM Maszyny WHERE Nr_fabryczny = " + Nr_fabryczny + "order by Typ ASC;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//selectNr_fabryczny

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        /// <param name="Nr_pom">zwraca maszynę po Nr pomieszczenia</param>
        /// <returns>Tabela.</returns>
        public DataTable selectNr_pom(string Nr_pom)
        {
            string query = "SELECT * FROM Maszyny WHERE Nr_pom = " + Nr_pom + "order by Typ ASC;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//selectNr_pom


        /// <summary>
        /// Wprowadza nowy rekord.
        /// </summary>
        /// <param name="VO">Obiekt wymiany danych.</param>
        /// <returns>Wartość logiczna powodzenia operacji.</returns>
        public bool insert(nsAccess2DB.MaszynyVO VO)
        {
            string query = "INSERT INTO Maszyny (Kategoria, Nazwa, Typ, Nr_inwentarzowy, Nr_fabryczny, Rok_produkcji, Producent, Zdjecie1, Rozszerz_zdj1, Zdjecie2, Rozszerz_zdj2, Nr_GUM, Nr_pom, Dzial, Nr_prot_BHP, Data_ost_przegl, Data_kol_przegl, Uwagi, Wykorzystanie, Stan_techniczny, Priorytet, Propozycja, Punktacja)" +
                " VALUES (@Kategoria, @Nazwa, @Typ, @Nr_inwentarzowy, @Nr_fabryczny, @Rok_produkcji, @Producent, @Zdjecie1, @Rozszerz_zdj1, @Zdjecie2, @Rozszerz_zdj2, @Nr_GUM, @Nr_pom, @Dzial, @Nr_prot_BHP, @Data_ost_przegl, @Data_kol_przegl, @Uwagi, @Wykorzystanie, @Stan_techniczny, @Priorytet, @Propozycja, @Punktacja)";

            OleDbParameter[] parameters = new OleDbParameter[23];
            parameters[0] = new OleDbParameter("Kategoria", OleDbType.VarChar, 100);
            parameters[0].Value = VO.Kategoria;

            parameters[1] = new OleDbParameter("Nazwa", OleDbType.VarChar, 100);
            parameters[1].Value = VO.Nazwa;

            parameters[2] = new OleDbParameter("Typ", OleDbType.VarChar, 100);
            parameters[2].Value = VO.Typ;

            parameters[3] = new OleDbParameter("Nr_inwentarzowy", OleDbType.VarChar, 100);
            parameters[3].Value = VO.Nr_inwentarzowy;

            parameters[4] = new OleDbParameter("Nr_fabryczny", OleDbType.VarChar, 100);
            parameters[4].Value = VO.Nr_fabryczny;

            parameters[5] = new OleDbParameter("Rok_produkcji", OleDbType.VarChar, 20);
            parameters[5].Value = VO.Rok_produkcji;

            parameters[6] = new OleDbParameter("Producent", OleDbType.VarChar, 100);
            parameters[6].Value = VO.Producent;

            parameters[7] = new OleDbParameter("Zdjecie1", OleDbType.VarChar, 255);
            parameters[7].Value = VO.Zdjecie1;

            parameters[8] = new OleDbParameter("Rozszerz_zdj1", OleDbType.VarChar, 6);
            parameters[8].Value = VO.Rozszerz_zdj1;

            parameters[9] = new OleDbParameter("Zdjecie2", OleDbType.VarChar, 255);
            parameters[9].Value = VO.Zdjecie2;

            parameters[10] = new OleDbParameter("Rozszerz_zdj2", OleDbType.VarChar, 6);
            parameters[10].Value = VO.Rozszerz_zdj2;

            parameters[11] = new OleDbParameter("Nr_GUM", OleDbType.VarChar, 100);
            parameters[11].Value = VO.Nr_GUM;

            parameters[12] = new OleDbParameter("Nr_GUM", OleDbType.VarChar, 100);
            parameters[12].Value = VO.Nr_GUM;

            parameters[13] = new OleDbParameter("Dzial", OleDbType.VarChar, 20);
            parameters[13].Value = VO.Dzial;

            parameters[14] = new OleDbParameter("Nr_prot_BHP", OleDbType.VarChar, 20);
            parameters[14].Value = VO.Nr_prot_BHP;

            parameters[15] = new OleDbParameter("Data_ost_przegl", OleDbType.DBDate);
            parameters[15].Value = VO.Data_ost_przegl;

            parameters[16] = new OleDbParameter("Data_kol_przegl", OleDbType.DBDate);
            parameters[16].Value = VO.Data_kol_przegl;

            parameters[17] = new OleDbParameter("Uwagi", OleDbType.VarChar, 255);
            parameters[17].Value = VO.Uwagi;

            parameters[18] = new OleDbParameter("Wykorzystanie", OleDbType.VarChar, 20);
            parameters[18].Value = VO.Wykorzystanie;

            parameters[19] = new OleDbParameter("Stan_techniczny", OleDbType.VarChar, 20);
            parameters[19].Value = VO.Stan_techniczny;

            parameters[20] = new OleDbParameter("Priorytet", OleDbType.VarChar, 20);
            parameters[20].Value = VO.Priorytet;

            parameters[21] = new OleDbParameter("Propozycja", OleDbType.VarChar, 20);
            parameters[21].Value = VO.Propozycja;

            parameters[22] = new OleDbParameter("Punktacja", OleDbType.Integer);
            parameters[22].Value = VO.Punktacja;

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
            string query = "UPDATE Maszyny SET Kategoria = @Kategoria, Nazwa =  @Nazwa, Typ = @Typ, Nr_inwentarzowy = @Nr_inwentarzowy, Nr_fabryczny=  @Nr_fabryczny," +
                " Rok_produkcji = @Rok_produkcji, Producent = @Producent, Zdjecie1 = @Zdjecie1 , Rozszerz_zdj1 = @Rozszerz_zdj1 , Zdjecie2 = @Zdjecie2, Rozszerz_zdj2 = @Rozszerz_zdj2" +
                "Nr_GUM = @Nr_GUM , Nr_pom = @Nr_pom, Dzial = @Dzial, Nr_prot_BHP = @Nr_prot_BHP, Data_ost_przegl = @Data_ost_przegl, Data_kol_przegl=@Data_kol_przegl," +
                " Uwagi = @Uwagi, Wykorzystanie = @Wykorzystanie , Stan_techniczny = @Stan_techniczny, Priorytet=@Priorytet, Propozycja=@Propozycja, Punktacja=@Punktacja) WHERE _ID_maszyny = " + VO.ID_maszyny.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[23];
            parameters[0] = new OleDbParameter("Kategoria", OleDbType.VarChar, 100);
            parameters[0].Value = VO.Kategoria;

            parameters[1] = new OleDbParameter("Nazwa", OleDbType.VarChar, 100);
            parameters[1].Value = VO.Nazwa;

            parameters[2] = new OleDbParameter("Typ", OleDbType.VarChar, 100);
            parameters[2].Value = VO.Typ;

            parameters[3] = new OleDbParameter("Nr_inwentarzowy", OleDbType.VarChar, 100);
            parameters[3].Value = VO.Nr_inwentarzowy;

            parameters[4] = new OleDbParameter("Nr_fabryczny", OleDbType.VarChar, 100);
            parameters[4].Value = VO.Nr_fabryczny;

            parameters[5] = new OleDbParameter("Rok_produkcji", OleDbType.VarChar, 20);
            parameters[5].Value = VO.Rok_produkcji;

            parameters[6] = new OleDbParameter("Producent", OleDbType.VarChar, 100);
            parameters[6].Value = VO.Producent;

            parameters[7] = new OleDbParameter("Zdjecie1", OleDbType.VarChar, 255);
            parameters[7].Value = VO.Zdjecie1;

            parameters[8] = new OleDbParameter("Rozszerz_zdj1", OleDbType.VarChar, 6);
            parameters[8].Value = VO.Rozszerz_zdj1;

            parameters[9] = new OleDbParameter("Zdjecie2", OleDbType.VarChar, 255);
            parameters[9].Value = VO.Zdjecie2;

            parameters[10] = new OleDbParameter("Rozszerz_zdj2", OleDbType.VarChar, 6);
            parameters[10].Value = VO.Rozszerz_zdj2;

            parameters[11] = new OleDbParameter("Nr_GUM", OleDbType.VarChar, 100);
            parameters[11].Value = VO.Nr_GUM;

            parameters[12] = new OleDbParameter("Nr_GUM", OleDbType.VarChar, 100);
            parameters[12].Value = VO.Nr_GUM;

            parameters[13] = new OleDbParameter("Dzial", OleDbType.VarChar, 20);
            parameters[13].Value = VO.Dzial;

            parameters[14] = new OleDbParameter("Nr_prot_BHP", OleDbType.VarChar, 20);
            parameters[14].Value = VO.Nr_prot_BHP;

            parameters[15] = new OleDbParameter("Data_ost_przegl", OleDbType.DBDate);
            parameters[15].Value = VO.Data_ost_przegl;

            parameters[16] = new OleDbParameter("Data_kol_przegl", OleDbType.DBDate);
            parameters[16].Value = VO.Data_kol_przegl;

            parameters[17] = new OleDbParameter("Uwagi", OleDbType.VarChar, 255);
            parameters[17].Value = VO.Uwagi;

            parameters[18] = new OleDbParameter("Wykorzystanie", OleDbType.VarChar, 20);
            parameters[18].Value = VO.Wykorzystanie;

            parameters[19] = new OleDbParameter("Stan_techniczny", OleDbType.VarChar, 20);
            parameters[19].Value = VO.Stan_techniczny;

            parameters[20] = new OleDbParameter("Priorytet", OleDbType.VarChar, 20);
            parameters[20].Value = VO.Priorytet;

            parameters[21] = new OleDbParameter("Propozycja", OleDbType.VarChar, 20);
            parameters[21].Value = VO.Propozycja;

            parameters[22] = new OleDbParameter("Punktacja", OleDbType.Integer);
            parameters[22].Value = VO.Punktacja;

            bool b = _conn.executeUpdateQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//update

    }//class MaszynyDAO

    // -------------------------------> BUS - warstawa operacji biznesowych tabeli Maszyny
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
            if (exists(VO.ID_maszyny))
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
                VOi.ID_maszyny = int.Parse(dr["ID_maszyny"].ToString());
                VOi.Kategoria = dr["Kategoria"].ToString();
                VOi.Nazwa = dr["Nazwa"].ToString();
                VOi.Typ = dr["Typ"].ToString();
                VOi.Nr_inwentarzowy = dr["Nr_inwentarzowy"].ToString();
                VOi.Nr_fabryczny = dr["Nr_fabryczny"].ToString();
                VOi.Rok_produkcji = dr["Rok_produkcji"].ToString();
                VOi.Producent = dr["Producent"].ToString();
                VOi.Zdjecie1 = dr["Zdjecie1"].ToString();
                VOi.Rozszerz_zdj1 = dr["Rozszerz_zdj1"].ToString();
                VOi.Zdjecie2 = dr["Zdjecie2"].ToString();
                VOi.Rozszerz_zdj2 = dr["Rozszerz_zdj2"].ToString();
                VOi.Nr_GUM = dr["Nr_GUM"].ToString();
                VOi.Nr_pom = dr["Nr_pom"].ToString();
                VOi.Dzial = dr["Dzial"].ToString();
                VOi.Nr_prot_BHP = dr["Nr_prot_BHP"].ToString();
                VOi.Data_ost_przegl = dr["Data_ost_przegl"].ToString();
                VOi.Data_kol_przegl = dr["Data_kol_przegl"].ToString();
                VOi.Uwagi = dr["Uwagi"].ToString();
                VOi.Wykorzystanie = dr["Wykorzystanie"].ToString();
                VOi.Stan_techniczny = dr["Stan_techniczny"].ToString();
                VOi.Priorytet = dr["Priorytet"].ToString();
                VOi.Propozycja = dr["Propozycja"].ToString();

                try
                {
                    VOi.Punktacja = int.Parse(dr["Punktacja"].ToString());
                }
                catch { VOi.Punktacja = 0; }


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
        /// <param name="ID_maszyny">ID maszyny.</param>
        public void select(int ID_maszyny)
        {
            fillTable(_DAO.select(ID_maszyny));
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
        /// Zwraca maszynę o wskazanym identyfikatorze.
        /// </summary>
        /// <param name="id">Identyfikator maszyny.</param>
        /// <returns>Maszyny. Jeśli ID==-1 to maszyny nie znaleziono.</returns>
        public MaszynyVO GetVO(int id)
        {
            foreach (nsAccess2DB.MaszynyVO VO in _VOs)
            {
                if (VO.ID_maszyny == id) return VO;
            }
            return new MaszynyVO();
        }//getVO
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

        /// <returns>Wartość logiczną sprawdzenia.</returns>
        private bool exists(int ID_maszyny)
        {
            foreach (MaszynyVO VOi in _VOs)
            {
                if (VOi.ID_maszyny == ID_maszyny) return true;
            }
            return false;
        }//exists

    }//public class MaszynyBUS

    ///////////////////////////////////// Częstotliwość
    /// <summary>
    /// Klasa wymiany danych z tabelą Czestotliwosc.
    /// </summary>
    public class CzestotliwoscVO
    {
        private string _Czestotliwosc = string.Empty; //100
            /// <summary>
            /// Konstruktor wymiany danych Czestotliwosc
            /// </summary>
        public CzestotliwoscVO() { }

        public string Nazwa
        {
            get { return _Czestotliwosc; }
            set { _Czestotliwosc = value; }
        }

    }//class CzestotliwoscVO

    //Klasa dostępu (Data Access Object) do tabeli Czestotliwosc.
    public class CzestotliwoscDAO
    {
        private dbConnection _conn;
        public string _error = string.Empty;

        /// <constructor>
        /// Konstruktor.
        /// </constructor>
        public CzestotliwoscDAO(string connString)
        {
            _conn = new dbConnection(connString);
            _error = _conn._error;
        }//CzestotliwoscDAO

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        public DataTable select()
        {
            string query = "SELECT * FROM Czestotliwosc;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

    }//class CzestotliwoscDAO

    //Warstwa operacji biznesowaych tabeli Czestotliwosc.
    public class CzestotliwoscBUS
    {
        CzestotliwoscDAO _DAO;

        private CzestotliwoscVO[] _VOs = new CzestotliwoscVO[0];    //lista danych
        private CzestotliwoscVO _VOi = new CzestotliwoscVO();       //dane na pozycji _idx
        private int _idx = 0;                       //indeks pozycji
        private bool _eof = false;                  //wskaźnik końca pliku
        private int _count = 0;                     //liczba pozycji

        public string _error = string.Empty;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="connString">ConnectionStrine.</param>
        public CzestotliwoscBUS(string connString)
        {
            _DAO = new CzestotliwoscDAO(connString);
            _error = _DAO._error;
        }//CzestotliwoscBUS

        /// <summary>
        /// Wypełnia tablice.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            CzestotliwoscVO VOi;
            _VOs = new CzestotliwoscVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);

                VOi = new CzestotliwoscVO();

                VOi.Nazwa = dr["Czestotliwosc"].ToString();

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
        public CzestotliwoscVO VO
        {
            get
            {
                if (_idx > -1 & _idx < _count)
                {
                    return _VOi = _VOs[_idx];
                }
                return new CzestotliwoscVO();
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
        /// <param name="Id">Nazwa Czestotliwosc.</param>
        /// <returns>Wynik logiczny sprawdzenia.</returns>
        public bool exists(String Nazwa)
        {
            foreach (CzestotliwoscVO VOi in _VOs)
            {
                if (VOi.Nazwa == Nazwa) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Nazwa">Identyfikator maszyny</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora działu.</returns>
        public int getIdx(string Nazwa)
        {
            int idx = -1;
            foreach (CzestotliwoscVO VOi in _VOs)
            {
                idx++;
                if (VOi.Nazwa == Nazwa) return idx;
            }

            return -1;
        }//getIdx

    }//class CzestotliwoscBUS





}//namespace nsAccess2DB
