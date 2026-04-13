using System;
using System.Collections.Generic;

namespace GestorMantenimiento
{
    class Program
    {
        // Método de login para validar usuario y contraseña
        static void Login()
        {
            // Credenciales fijas
            const string usuarioCorrecto = "ianreyes";
            const string contraseñaCorrecta = "12345678";

            // Validación de usuario (hasta que lo ingrese correctamente)
            while (true)
            {
                Console.Clear();
                Console.Write("Usuario: ");
                string usuario = Console.ReadLine();

                if (usuario == usuarioCorrecto)
                    break; // sale del ciclo si el usuario es correcto

                Console.WriteLine("Usuario incorrecto...");
                Console.ReadKey();
            }

            // Validación de contraseña (máximo 10 intentos)
            int intentos = 0;
            while (intentos < 10)
            {
                Console.Clear();
                Console.Write("Contraseña: ");
                string contraseña = Console.ReadLine();

                if (contraseña == contraseñaCorrecta)
                    return; // acceso concedido

                intentos++;
                Console.WriteLine("Contraseña incorrecta...");
                Console.ReadKey();
            }

            // Si falla muchas veces, se cierra el programa
            Console.WriteLine("Demasiados intentos.");
            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            // Primero se ejecuta el login
            Login();

            // Listas donde se guardan los datos en memoria
            List<Equipo> listaEquipos = new List<Equipo>();
            List<Ticket> listaTickets = new List<Ticket>();

            int opcion = 0;

            // Menú principal del sistema
            do
            {
                Console.Clear();
                Console.WriteLine("===== GESTOR DE MANTENIMIENTO =====");
                Console.WriteLine("1. Registrar Equipo");
                Console.WriteLine("2. Ver Equipos");
                Console.WriteLine("3. Crear Ticket");
                Console.WriteLine("4. Cambiar Estado de Ticket");
                Console.WriteLine("5. Ver Tickets");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opcion: ");

                // Validación de entrada
                try
                {
                    opcion = int.Parse(Console.ReadLine());
                }
                catch
                {
                    opcion = 0; // si falla, manda opción inválida
                }

                // Control de opciones del menú
                switch (opcion)
                {
                    case 1:
                        RegistrarEquipo(listaEquipos);
                        break;

                    case 2:
                        MostrarEquipos(listaEquipos);
                        break;

                    case 3:
                        CrearTicket(listaTickets, listaEquipos);
                        break;

                    case 4:
                        CambiarEstado(listaTickets);
                        break;

                    case 5:
                        MostrarTickets(listaTickets);
                        break;
                }

            } while (opcion != 6);
        }

        // ============================
        // HERENCIA
        // ============================

        // Clase base Equipo (clase padre)
        class Equipo
        {
            public int Id;
            public string Marca;
            public string Modelo;

            public Equipo(int id, string marca, string modelo)
            {
                Id = id;
                Marca = marca;
                Modelo = modelo;
            }

            // Método virtual que luego se sobrescribe
            public virtual string MostrarTipo()
            {
                return "Equipo";
            }
        }

        // Clase PC que hereda de Equipo
        class PC : Equipo
        {
            public PC(int id, string marca, string modelo)
                : base(id, marca, modelo) { }

            // Sobrescribe el método para indicar que es una PC
            public override string MostrarTipo()
            {
                return "PC";
            }
        }

        // Clase Impresora que hereda de Equipo
        class Impresora : Equipo
        {
            public Impresora(int id, string marca, string modelo)
                : base(id, marca, modelo) { }

            // Sobrescribe el método para indicar que es una impresora
            public override string MostrarTipo()
            {
                return "Impresora";
            }
        }

        // ============================
        // FUNCIONES
        // ============================

        // Método para registrar equipos
        static void RegistrarEquipo(List<Equipo> lista)
        {
            int tipo;
            int id;

            // Se pide el tipo de equipo
            try
            {
                Console.WriteLine("Tipo de equipo (1-PC / 2-Impresora): ");
                tipo = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Dato invalido");
                Console.ReadKey();
                return;
            }

            // Se pide el ID
            try
            {
                Console.Write("Ingrese ID: ");
                id = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Dato invalido");
                Console.ReadKey();
                return;
            }

            // Validación para que no se repita el ID
            foreach (var eq in lista)
            {
                if (eq.Id == id)
                {
                    Console.WriteLine("ID ya existe");
                    Console.ReadKey();
                    return;
                }
            }

            // Datos del equipo
            Console.Write("Marca: ");
            string marca = Console.ReadLine();

            Console.Write("Modelo: ");
            string modelo = Console.ReadLine();

            Equipo nuevo;

            // Aquí se aplica la herencia
            if (tipo == 1)
                nuevo = new PC(id, marca, modelo);
            else if (tipo == 2)
                nuevo = new Impresora(id, marca, modelo);
            else
            {
                Console.WriteLine("Tipo invalido");
                Console.ReadKey();
                return;
            }

            // Se agrega a la lista
            lista.Add(nuevo);
            Console.WriteLine("Equipo registrado!");
            Console.ReadKey();
        }

        // Método para mostrar equipos
        static void MostrarEquipos(List<Equipo> lista)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay equipos");
                Console.ReadKey();
                return;
            }

            // Se recorre la lista y se muestran los datos
            foreach (var eq in lista)
            {
                Console.WriteLine("------");
                Console.WriteLine($"ID: {eq.Id}");
                Console.WriteLine($"Tipo: {eq.MostrarTipo()}"); // polimorfismo
                Console.WriteLine($"Marca: {eq.Marca}");
                Console.WriteLine($"Modelo: {eq.Modelo}");
            }

            Console.ReadKey();
        }

        // Método para crear tickets
        static void CrearTicket(List<Ticket> listaTickets, List<Equipo> listaEquipos)
        {
            int idTicket;
            int idEquipo;

            // Se piden los datos
            try
            {
                Console.Write("ID Ticket: ");
                idTicket = int.Parse(Console.ReadLine());

                Console.Write("ID Equipo: ");
                idEquipo = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Dato invalido");
                Console.ReadKey();
                return;
            }

            // Validar que el equipo exista
            bool existe = false;

            foreach (var eq in listaEquipos)
            {
                if (eq.Id == idEquipo)
                    existe = true;
            }

            if (!existe)
            {
                Console.WriteLine("Equipo no existe");
                Console.ReadKey();
                return;
            }

            // Descripción del problema
            Console.Write("Descripcion: ");
            string desc = Console.ReadLine();

            // Se crea el ticket
            Ticket t = new Ticket(idTicket, idEquipo, desc);
            listaTickets.Add(t);

            Console.WriteLine("Ticket creado!");
            Console.ReadKey();
        }

        // Método para cambiar el estado de un ticket
        static void CambiarEstado(List<Ticket> lista)
        {
            int id;

            try
            {
                Console.Write("ID Ticket: ");
                id = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Dato invalido");
                Console.ReadKey();
                return;
            }

            foreach (var t in lista)
            {
                if (t.Id == id)
                {
                    Console.WriteLine("1. Abierto");
                    Console.WriteLine("2. En Proceso");
                    Console.WriteLine("3. Resuelto");

                    int op = int.Parse(Console.ReadLine());

                    // Cambio de estado
                    if (op == 1) t.Estado = "Abierto";
                    else if (op == 2) t.Estado = "En Proceso";
                    else if (op == 3) t.Estado = "Resuelto";

                    Console.WriteLine("Actualizado!");
                }
            }

            Console.ReadKey();
        }

        // Método para mostrar tickets
        static void MostrarTickets(List<Ticket> lista)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay tickets");
                Console.ReadKey();
                return;
            }

            foreach (var t in lista)
            {
                Console.WriteLine("------");
                Console.WriteLine($"ID: {t.Id}");
                Console.WriteLine($"Equipo: {t.IdEquipo}");
                Console.WriteLine($"Estado: {t.Estado}");
                Console.WriteLine($"Desc: {t.Descripcion}");
            }

            Console.ReadKey();
        }

        // Clase Ticket
        class Ticket
        {
            public int Id;
            public int IdEquipo;
            public string Descripcion;
            public string Estado;

            // Constructor
            public Ticket(int id, int idEquipo, string descripcion)
            {
                Id = id;
                IdEquipo = idEquipo;
                Descripcion = descripcion;
                Estado = "Abierto"; // estado inicial
            }
        }
    }
}
