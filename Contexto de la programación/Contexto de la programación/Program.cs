using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Servidor
{
    static void Main()
    {
        try
        {
            TcpListener servidor = new TcpListener(IPAddress.Any, 8080);
            servidor.Start();
            Console.WriteLine("Servidor iniciado en el puerto 8080...");

            while (true)
            {
                // Aceptar conexiones de clientes
                TcpClient cliente = servidor.AcceptTcpClient();
                Console.WriteLine("Cliente conectado.");

                // Manejo del cliente
                NetworkStream stream = cliente.GetStream();
                byte[] buffer = new byte[1024];
                int bytesLeidos = stream.Read(buffer, 0, buffer.Length);

                string mensajeCliente = Encoding.UTF8.GetString(buffer, 0, bytesLeidos);
                Console.WriteLine($"Mensaje recibido: {mensajeCliente}");

                // Procesar la solicitud del cliente
                string respuesta = ProcesarSolicitud(mensajeCliente);

                // Enviar respuesta al cliente
                byte[] respuestaBytes = Encoding.UTF8.GetBytes(respuesta);
                stream.Write(respuestaBytes, 0, respuestaBytes.Length);

                Console.WriteLine("Respuesta enviada al cliente.");
                cliente.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static string ProcesarSolicitud(string mensaje)
    {
        // Simular lógica de procesamiento de negocio
        if (mensaje.ToLower() == "hola servidor")
        {
            return "Hola cliente, bienvenido al servidor.";
        }
        else if (mensaje.ToLower() == "¿qué servicios ofreces?")
        {
            return "Servicios disponibles: 1. Almacenamiento 2. Procesamiento de datos 3. Comunicación.";
        }
        else
        {
            return "Comando no reconocido.";
        }
    }
}
