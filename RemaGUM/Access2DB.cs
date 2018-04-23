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
        private string _Kategoria = string.Empty; // 100
        private string _Nazwa = string.Empty; // 255
        private string _Typ = string.Empty; // 255
        private string _Nr_inwentarzowy = string.Empty; // 255
        private string _Nr_fabryczny = string.Empty; // 255
        private string _Rok_produkcji = string.Empty; // 50
        private string _Producent = string.Empty; // 255
        private string _Zdjecie1 = string.Empty; // obiekt OLE ????????????????????????????????? zdjęcie
        private string _Rozszerz_zdj1 = string.Empty; // 255
        private string _Osoba_zarzadzajaca = string.Empty; // 255
        private string _Nr_pom = string.Empty; // 255
        private string _Dzial = string.Empty; // 255
        private string _Nr_prot_BHP = string.Empty; // 255
        private string _Data_ost_przegl = string.Empty; //Data
        private string _Data_kol_przegl = string.Empty; //Data
        private string _Uwagi = string.Empty; //255
        private string _Wykorzystanie = string.Empty; // 255
        private string _Stan_techniczny = string.Empty; // 255
        private string _Propozycja = string.Empty; // 255
        private string _Operator_maszyny = string.Empty; // 255


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
        public string Osoba_zarzadzajaca
        {
            get { return _Osoba_zarzadzajaca; }
            set { _Osoba_zarzadzajaca = value; }
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
        public string Propozycja
        {
            get { return _Propozycja; }
            set { _Propozycja = value; }
        }
        public string Operator_maszyny
        {
            get { return _Operator_maszyny; }
            set { _Operator_maszyny = value; }
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
            string query = "SELECT * FROM Maszyny WHERE ID_maszyny = " + ID_maszyny.ToString() + ";";

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
            string query = "INSERT INTO Maszyny (Kategoria, Nazwa, Typ, Nr_inwentarzowy, Nr_fabryczny, Rok_produkcji, Producent, Zdjecie1, Rozszerz_zdj1, Osoba_zarzadzajaca, Nr_pom, Dzial, Nr_prot_BHP, Data_ost_przegl, Data_kol_przegl, Uwagi, Wykorzystanie, Stan_techniczny, Propozycja, Operator_maszyny)" +
                " VALUES (@Kategoria, @Nazwa, @Typ, @Nr_inwentarzowy, @Nr_fabryczny, @Rok_produkcji, @Producent, @Zdjecie1, @Rozszerz_zdj1, @Osoba_zarzadzajaca, @Nr_pom, @Dzial, @Nr_prot_BHP, @Data_ost_przegl, @Data_kol_przegl, @Uwagi, @Wykorzystanie, @Stan_techniczny, @Propozycja, @Operator_maszyny);";

            OleDbParameter[] parameters = new OleDbParameter[20];
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

            parameters[7] = new OleDbParameter("Zdjecie1", OleDbType.VarChar, 255);
            parameters[7].Value = VO.Zdjecie1;

            parameters[8] = new OleDbParameter("Rozszerz_zdj1", OleDbType.VarChar, 255);
            parameters[8].Value = VO.Rozszerz_zdj1;

            parameters[9] = new OleDbParameter("Osoba_zaradzajaca", OleDbType.VarChar, 255);
            parameters[9].Value = VO.Osoba_zarzadzajaca;

            parameters[10] = new OleDbParameter("Nr_pom", OleDbType.VarChar, 255);
            parameters[10].Value = VO.Nr_pom;
            
            parameters[11] = new OleDbParameter("Dzial", OleDbType.VarChar, 20);
            parameters[11].Value = VO.Dzial;

            parameters[12] = new OleDbParameter("Nr_prot_BHP", OleDbType.VarChar, 20);
            parameters[12].Value = VO.Nr_prot_BHP;

            parameters[13] = new OleDbParameter("Data_ost_przegl", OleDbType.DBDate);
            parameters[13].Value = VO.Data_ost_przegl;

            parameters[14] = new OleDbParameter("Data_kol_przegl", OleDbType.DBDate);
            parameters[14].Value = VO.Data_kol_przegl;

            parameters[15] = new OleDbParameter("Uwagi", OleDbType.VarChar, 255);
            parameters[15].Value = VO.Uwagi;

            parameters[16] = new OleDbParameter("Wykorzystanie", OleDbType.VarChar, 20);
            parameters[16].Value = VO.Wykorzystanie;

            parameters[17] = new OleDbParameter("Stan_techniczny", OleDbType.VarChar, 20);
            parameters[17].Value = VO.Stan_techniczny;

            parameters[18] = new OleDbParameter("Propozycja", OleDbType.VarChar, 20);
            parameters[18].Value = VO.Propozycja;

            parameters[19] = new OleDbParameter("Operator_maszyny", OleDbType.VarChar, 100);
            parameters[19].Value = VO.Operator_maszyny;
            
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
            string query = "UPDATE Maszyny SET Kategoria = @Kategoria, Nazwa = @Nazwa, Typ = @Typ, Nr_inwentarzowy = @Nr_inwentarzowy, Nr_fabryczny = @Nr_fabryczny, Rok_produkcji = @Rok_produkcji, Producent = @Producent, Zdjecie1 = @Zdjecie1, Rozszerz_zdj1 = @Rozszerz_zdj1, Osoba_zarzadzajaca = @Osoba_zarzadzajaca, Nr_pom = @Nr_pom, Dzial = @Dzial, Nr_prot_BHP = @Nr_prot_BHP, Data_ost_przegl = @Data_ost_przegl, Data_kol_przegl=@Data_kol_przegl, Uwagi = @Uwagi, Wykorzystanie = @Wykorzystanie, Stan_techniczny = @Stan_techniczny, Propozycja = @Propozycja, Operator_maszyny = @Operator_maszyny WHERE ID_maszyny = " + VO.ID_maszyny.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[20];
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

            parameters[7] = new OleDbParameter("Zdjecie1", OleDbType.VarChar, 255);
            parameters[7].Value = VO.Zdjecie1;

            parameters[8] = new OleDbParameter("Rozszerz_zdj1", OleDbType.VarChar, 255);
            parameters[8].Value = VO.Rozszerz_zdj1;

            parameters[9] = new OleDbParameter("Osoba_zaradzajaca", OleDbType.VarChar, 255);
            parameters[9].Value = VO.Osoba_zarzadzajaca;

            parameters[10] = new OleDbParameter("Nr_pom", OleDbType.VarChar, 255);
            parameters[10].Value = VO.Nr_pom;

            parameters[11] = new OleDbParameter("Dzial", OleDbType.VarChar, 20);
            parameters[11].Value = VO.Dzial;

            parameters[12] = new OleDbParameter("Nr_prot_BHP", OleDbType.VarChar, 20);
            parameters[12].Value = VO.Nr_prot_BHP;

            parameters[13] = new OleDbParameter("Data_ost_przegl", OleDbType.DBDate);
            parameters[13].Value = VO.Data_ost_przegl;

            parameters[14] = new OleDbParameter("Data_kol_przegl", OleDbType.DBDate);
            parameters[14].Value = VO.Data_kol_przegl;

            parameters[15] = new OleDbParameter("Uwagi", OleDbType.VarChar, 255);
            parameters[15].Value = VO.Uwagi;

            parameters[16] = new OleDbParameter("Wykorzystanie", OleDbType.VarChar, 20);
            parameters[16].Value = VO.Wykorzystanie;

            parameters[17] = new OleDbParameter("Stan_techniczny", OleDbType.VarChar, 20);
            parameters[17].Value = VO.Stan_techniczny;

            parameters[18] = new OleDbParameter("Propozycja", OleDbType.VarChar, 20);
            parameters[18].Value = VO.Propozycja;

            parameters[19] = new OleDbParameter("Operator_maszyny", OleDbType.VarChar, 100);
            parameters[19].Value = VO.Operator_maszyny;

            bool b = _conn.executeUpdateQuery(query, parameters);
            _error = _conn._error;
            return b;
        }//update
        public bool delete(int ID_maszyny)
        {
            string query = "DELETE* FROM Maszyny WHERE ID_Maszyny = " + ID_maszyny.ToString() + ";";

            OleDbParameter[] parameters = new OleDbParameter[0];
            bool b = _conn.executeDeleteQuery(query, parameters);
            _error = _conn._error;
            return b;
        }// delete
        public DataTable selectQuery(string query)
        {
            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//selectQuery

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
                VOi.Osoba_zarzadzajaca = dr["Osoba_zarzadzajaca"].ToString();
                VOi.Nr_pom = dr["Nr_pom"].ToString();
                VOi.Dzial = dr["Dzial"].ToString();
                VOi.Nr_prot_BHP = dr["Nr_prot_BHP"].ToString();
                VOi.Data_ost_przegl = dr["Data_ost_przegl"].ToString();
                VOi.Data_kol_przegl = dr["Data_kol_przegl"].ToString();
                VOi.Uwagi = dr["Uwagi"].ToString();
                VOi.Wykorzystanie = dr["Wykorzystanie"].ToString();
                VOi.Stan_techniczny = dr["Stan_techniczny"].ToString();
                VOi.Propozycja = dr["Propozycja"].ToString();
                VOi.Operator_maszyny = dr["Operator_maszyny"].ToString();

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
        public void selectID_maszyny(int ID_maszyny)
        {
            fillTable(_DAO.select(ID_maszyny));
        }//select

        public bool delete(int ID_maszyny) {
            return _DAO.delete(ID_maszyny);
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

        public void selectQuery(string query)
        {
            fillTable(_DAO.selectQuery(query));
        }//selectQuery
        
        /// <summary>
        /// zwraca indeks szukanej nazwy
        /// </summary>
        /// <param name="Nazwa"></param>
        /// <returns></returns>
        public int nazwaIdx(string Nazwa)
        {
            top();
            int idx = -1;
            while (!eof)
            {
                idx++;
                if (_VOi.Nazwa.Contains(Nazwa)) return idx;                  
            }
            return idx;
        }//nazwaIdx

        /// <summary>
        /// zwraca nowy indeks nazwy
        /// </summary>
        /// <param name="Nazwa"></param>
        /// <param name="startIdx"></param>
        /// <returns></returns>
        public int nazwaIdx(string Nazwa, int startIdx)
        {
            _idx = startIdx;
            int idx = startIdx;
            while (!eof)
            {
                
                if (_VOi.Nazwa.Contains(Nazwa)) return idx;
                idx++;
            }
            return -1;
        }//nazwaIdx

    }//public class MaszynyBUS

    //////////////////////////////////////////////////////////////////////// Częstotliwość
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
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora częstotliwości.</returns>
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

        public string Nazwa
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

                VOi.Nazwa = dr["Kategoria"].ToString();

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
                if (VOi.Nazwa == Nazwa) return true;
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
                if (VOi.Nazwa == Nazwa) return idx;
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

    /////////////////////////////////////////////////////////////////////////////////// Operator_maszyny
    /// <summary>
    /// Klasa wymiany danych z tabelą Operator_maszyny.
    /// </summary>
    public class Operator_maszynyVO
    {
        private string _Operator_maszyny = string.Empty; //255
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Operator_maszyny
        /// </summary>
        public Operator_maszynyVO() { }

        public string Nazwa
        {
            get { return _Operator_maszyny; }
            set { _Operator_maszyny = value; }
        }
    }//class Operator_maszynyVO

    //Klasa dostępu (Data Access Object) do tabeli Operator_maszyny.
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

        /// <summary>
        /// Zwraca tabelę spełniającą wartości parametrów.
        /// </summary>
        public DataTable select()
        {
            string query = "SELECT * FROM Operator_maszyny;";

            OleDbParameter[] parameters = new OleDbParameter[0];
            DataTable dt = _conn.executeSelectQuery(query, parameters);
            _error = _conn._error;
            return dt;
        }//select

    }//class Operator_maszynyDAO

    //Warstwa operacji biznesowaych tabeli Operator_maszyny BUS.
    public class Operator_maszynyBUS
    {
        Operator_maszynyDAO _DAO;

        private Operator_maszynyVO[] _VOs = new Operator_maszynyVO[0];    //lista danych
        private Operator_maszynyVO _VOi = new Operator_maszynyVO();       //dane na pozycji _idx
        private int _idx = 0;                       //indeks pozycji
        private bool _eof = false;                  //wskaźnik końca pliku
        private int _count = 0;                     //liczba pozycji

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
        /// Wypełnia tablice.
        /// </summary>
        /// <param name="dt">Tabela danych.</param>
        private void fillTable(DataTable dt)
        {
            Operator_maszynyVO VOi;
            _VOs = new Operator_maszynyVO[0];

            foreach (DataRow dr in dt.Rows)
            {
                Array.Resize(ref _VOs, _VOs.Length + 1);

                VOi = new Operator_maszynyVO();

                VOi.Nazwa = dr["Operator_maszyny"].ToString();

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
        /// <param name="Id">Operator_maszyny.</param>
        /// <returns>Wynik logiczny sprawdzenia.</returns>
        public bool exists(String Nazwa)
        {
            foreach (Operator_maszynyVO VOi in _VOs)
            {
                if (VOi.Nazwa == Nazwa) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Nazwa">Identyfikator Operator_maszyny - osoba posiadająca uprawnienia do pracy na danej maszynie</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora operator_maszyny.</returns>
        public int getIdx(string Nazwa)
        {
            int idx = -1;
            foreach (Operator_maszynyVO VOi in _VOs)
            {
                idx++;
                if (VOi.Nazwa == Nazwa) return idx;
            }

            return -1;
        }//getIdx

    }//class Operator_maszynyBUS

    
    /////////////////////////////////////////////////////////////////////////////////// Osoba_zarzadzajaca maszyną
    /// <summary>
    /// Klasa wymiany danych z tabelą Osoba_zarzadzajaca.
    /// </summary>
    public class Osoba_zarzadzajacaVO
    {
        private string _Osoba_zarzadzajaca = string.Empty; //255
        /// <summary>
        /// Konstruktor wymiany danych z tabelą Osoba_zarzadzajaca
        /// </summary>
        public Osoba_zarzadzajacaVO() { }

        public string Nazwa
        {
            get { return _Osoba_zarzadzajaca; }
            set { _Osoba_zarzadzajaca = value; }
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

    }//class Osoba_zarzadzajacaDAO

    //Warstwa operacji biznesowaych tabeli Osoba_zarzadzajaca BUS.
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

                VOi.Nazwa = dr["Osoba_zarzadzajaca"].ToString();

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
        public bool exists(String Nazwa)
        {
            foreach (Osoba_zarzadzajacaVO VOi in _VOs)
            {
                if (VOi.Nazwa == Nazwa) return true;
            }
            return false;
        }//exists

        /// <summary>
        /// Zwraca indeks pozycji.
        /// </summary>
        /// <param name="Nazwa">Identyfikator Osoba_zarzadzajaca maszyną</param>
        /// <returns>Indeks pozycji. -1 oznacza brak identyfikatora Osoba_zarzadzajaca.</returns>
        public int getIdx(string Nazwa)
        {
            int idx = -1;
            foreach (Osoba_zarzadzajacaVO VOi in _VOs)
            {
                idx++;
                if (VOi.Nazwa == Nazwa) return idx;
            }

            return -1;
        }//getIdx

    }//class Osoba_zarzadzajacaBUS

}//namespace nsAccess2DB
