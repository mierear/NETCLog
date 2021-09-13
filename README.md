# NETCLog
Pieza de software encargada de guardar archivos de log en caso de fallos de sistema.


#Modo de implementación

1) Agregue a su solución de .net core 2.1 o superior una referencia a la carpeta NETCLog
2) Para generar los archivos de log, genere una nueva Instancia de LogSistema:
   static LogSistema objLog = new LogSistema();
   
   Luego, registrar en el archivo de log:
   
    try
    {
        //Throw Ex
    }
    catch (Exception ex)
    {
        objLog.Log(TipoLog.ERROR, "Error", ex.Message + "; StackTrace: " + ex.StackTrace);               
    }
    
    Primer parámetro: Tipo error puede ser "ERROR", "FATAL", "INFO", "WARNING"
    Segundo parámetro: "Glosa personalizada" Se utiliza para complemetar error en el archivo plano.
    
    [{ERROR o FATAL o INFO o WARNING}: fecha:8/24/2021 12:00:02 AM-> [SEGUNDO PARÁMETRO]]Mensaje:
    
    Ej: [{ERROR}: fecha:8/24/2021 12:00:02 AM-> Error]Mensaje: The                            remote server returned an error: (500) Internal Server Error.;      StackTrace:....................
    
    Tercer Parámetro: Información sobre el error.
    
    
 3) La ruta donde se guardan los archivos en Debug es: RutaCapaOrquestadora/bin/debug/netcoreapp3.1/log
    ej: Software_NETC_API/bin/Debug/netcoreapp3.1/log
 4) Para producción, crear la carpeta "log" en la misma carpeta donde se copiarán las demas piezas DLL de su solución



    
    
    
