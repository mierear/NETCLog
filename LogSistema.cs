using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace NETCLog
{
    public class LogSistema
    {
        private string RUTA_LOG = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        LogEvento objLog;

        public LogSistema()
        {
            objLog = new LogEvento(RUTA_LOG, "t1", "dll");
        }

        public void Log(TipoLog tipoLog, string titulo, string mensaje)
        {
            objLog.LogSuceso(tipoLog, titulo, mensaje);
        }

        public void Log(TipoLog tipoLog, string titulo, string mensaje, StackTrace stackTrace)
        {
            objLog.LogSuceso(tipoLog, titulo, mensaje, stackTrace);
        }


    }
}
