using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log
{
    public interface ILog
    {
        /// <summary>
        /// Write a log request to a given output device.
        /// </summary>
        /// <param name="sender">Sender of the log request.</param>
        /// <param name="e">Parameters of the log request.</param>
        void Log(object sender, LogEventArgs e);
    }
}
