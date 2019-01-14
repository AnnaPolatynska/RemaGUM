// ***********************************************************************
// Assembly         : RemaGUM
// Author           : anna.polatynska
// Created          : 05-15-2018
//
// Last Modified By : anna.polatynska
// Last Modified On : 05-10-2018
// ***********************************************************************
// <copyright file="Program.cs" company="Hewlett-Packard Company">
//     Copyright © Hewlett-Packard Company 2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemaGUM
{
    /// <summary>
    /// Class Program.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SpisForm());
        }
    }
}
