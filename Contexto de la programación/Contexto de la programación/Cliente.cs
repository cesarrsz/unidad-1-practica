using System;
using System.Net.Sockets;
using System.Text;

class Cliente
{
    static void Main()
    {
        try
        {
            // Conectar al servidor
            TcpClient cliente = new TcpClient("127.0.0.1", 8080);
            Console.WriteLine("Conectado al servidor.");

            // Enviar mensaje al servidor
            NetworkStream stream = cliente.GetStream();
            Console.WriteLine("Escribe un mensaje para el servidor:");
            string mensaje = Console.ReadLine();

            byte[] mensajeBytes = Encoding.UTF8.GetBytes(mensaje);
            stream.Write(mensajeBytes, 0, mensajeBytes.Length);

            // Recibir respuesta del servidor
            byte[] buffer = new byte[1024];
            int bytesLeidos = stream.Read(buffer, 0, buffer.Length);
            string respuesta = Encoding.UTF8.GetString(buffer, 0, bytesLeidos);

            Console.WriteLine($"Respuesta del servidor: {respuesta}");

            cliente.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
