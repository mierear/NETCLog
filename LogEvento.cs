using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace NETCLog
{ 
    public class LogEvento
    {
        private string rutaLog_;
        private string clase_;
        private string nombreDll_;

        public LogEvento(string rutaLog, string clase, string nombreDll)
        {
            rutaLog_ = rutaLog;
            clase_ = clase;
            nombreDll_ = nombreDll;
        }

        public void LogSuceso(TipoLog tipoMensaje, string titulo, string mensaje)
        {
            GuardarLog(tipoMensaje, clase_, titulo, mensaje, nombreDll_);
        }

        public void LogSuceso(TipoLog tipoMensaje, string titulo, string mensaje, StackTrace stackTrace)
        {
            //Lanzar Hilo
            Task.Factory.StartNew(() => GuardarLog(tipoMensaje, clase_, titulo, mensaje, nombreDll_, stackTrace));
            GuardarLog(tipoMensaje, clase_, titulo, mensaje, nombreDll_, stackTrace);
        }

        private void GuardarLog(TipoLog tipoMensaje, string clase, string titulo, string mensaje, string nombreDll, StackTrace stackTrace)
        {
            try
            {
                string metodo = stackTrace.GetFrame(5).GetMethod().Name;

                string f = rutaLog_ + "\\" + "log." + nombreDll + "." + clase + "." + metodo + "." + DateTime.Now.ToString("yyMMdd") + ".log";
                if (File.Exists(f))
                {
                    FileInfo fi = new FileInfo(rutaLog_);

                    File.AppendAllText(f, Cabecera(tipoMensaje, titulo) + "Mensaje: " + mensaje + "; StackTrace: " + stackTrace);//, System.Text.Encoding.GetEncoding(1252));
                }
                else
                {
                    System.IO.File.WriteAllText(f, Cabecera(tipoMensaje, titulo) + "Mensaje: " + mensaje + "; StackTrace: " + stackTrace);//, System.Text.Encoding.GetEncoding(1252));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void GuardarLog(TipoLog tipoMensaje, string clase, string titulo, string mensaje, string nombreDll)
        {
            try
            {
                StackTrace stackTrace = new StackTrace();

                // Get calling method name
                string metodo = stackTrace.GetFrame(0).GetMethod().Name;

                Directory.CreateDirectory(rutaLog_ + "//log//");
                string f = rutaLog_ + "//log//" + "log." + nombreDll + "." + clase + "." + metodo + "." + DateTime.Now.ToString("yyMMdd") + ".log";

                if (File.Exists(f))
                {
                    FileInfo fi = new FileInfo(rutaLog_);   

                    File.AppendAllText(f, Cabecera(tipoMensaje, titulo) + "Mensaje: " + mensaje);
                }
                else
                {
                    System.IO.File.WriteAllText(f, Cabecera(tipoMensaje, titulo) + "Mensaje: " + mensaje);//, System.Text.Encoding.GetEncoding(1252));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string Cabecera(TipoLog tipoMensaje, string titulo)
        {
            switch (tipoMensaje)
            {
                case TipoLog.INFO:
                    return "\n[{INFO}: fecha:" + DateTime.Now.ToString() + "-> " + titulo + "]";
                    break;

                case TipoLog.ERROR:
                    return "\n[{ERROR}: fecha:" + DateTime.Now.ToString() + "-> " + titulo + "]";
                    break;

                default:
                    return "\n[{GEN}]: fecha:" + DateTime.Now.ToString() + "-> " + titulo + "]";
            }
        }
    }
}
