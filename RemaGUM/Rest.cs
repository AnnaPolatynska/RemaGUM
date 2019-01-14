namespace RemaGUM
{
    /// <summary>
    /// Class Rest.
    /// </summary>
    class Rest
    {
        /// <summary>
        /// Połączenie z bazą danych.
        /// </summary>
        /// <param name="connString">The connection string.</param>
        /// <returns>System.String.</returns>
        public string dbConnection(string connString)
        {
            return connString + "; Jet OLEDB:Database Password=zlom";
        }
    }// class Rest
}//namespace nsRest
