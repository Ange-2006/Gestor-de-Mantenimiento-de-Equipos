using System;

namespace GestorMantenimiento
{
    class Program
    {
        // Arreglos y listas simples para almacenar equipos y tickets
        static Equipo[] listaEquipos = new Equipo[100];
        static Ticket[] listaTickets = new Ticket[100];
        static int contadorEquipos = 0;
        static int contadorTickets = 0;

        // Login (para sentirme bien fifi)
        static void Login()
        {
            const string usuarioCorrecto = "ianreyes";
            const string contraseñaCorrecta = "12345678";

            while (true)
            {
                Console.Clear();
                Console.Write("Usuario: ");
                string usuario = Console.ReadLine();

                if (usuario == usuarioCorrecto)
                    break;

                Console.WriteLine("Usuario incorrecto");
                Console.ReadKey();
            }

            int intentos = 0;
            while (intentos < 3)
            {
                Console.Clear();
                Console.Write("Contraseña: ");
                string contraseña = Console.ReadLine();

                if (contraseña == contraseñaCorrecta)
                    return;

                intentos++;
                Console.WriteLine("Contraseña incorrecta");
                Console.ReadKey();
            }

            Console.WriteLine("Demasiados intentos");
            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            Login();

            int opcion = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("===== GESTOR DE MANTENIMIENTO =====");
                Console.WriteLine("1. Registrar Equipo");
                Console.WriteLine("2. Listar Equipos");
                Console.WriteLine("3. Buscar Equipo");
                Console.WriteLine("4. Crear Ticket");
                Console.WriteLine("5. Cambiar Estado Ticket");
                Console.WriteLine("6. Listar Tickets");
                Console.WriteLine("7. Salir");
                Console.Write("Seleccione una opcion: ");

                try
                {
                    opcion = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Opcion invalida. Presione una tecla...");
                    Console.ReadKey();
                    Console.Clear();
                    continue; // vuelve al menú limpio
                }
                switch (opcion)
                {
                    case 1: RegistrarEquipo(); break;
                    case 2: ListarEquipos(); break;
                    case 3: BuscarEquipo(); break;
                    case 4: CrearTicket(); break;
                    case 5: CambiarEstado(); break;
                    case 6: ListarTickets(); break;
                }

            } while (opcion != 7);
        }

        static void RegistrarEquipo()
        {
            try
            {
                Console.Write("ID: ");
                int id = int.Parse(Console.ReadLine());

                for (int i = 0; i < contadorEquipos; i++)
                {
                    if (listaEquipos[i].Id == id)
                    {
                        Console.WriteLine("ID duplicado");
                        Console.ReadKey();
                        return;
                    }
                }

                Console.Write("Tipo: ");
                string tipo = Console.ReadLine();

                Console.Write("Marca: ");
                string marca = Console.ReadLine();

                Console.Write("Modelo: ");
                string modelo = Console.ReadLine();

                listaEquipos[contadorEquipos++] = new Equipo(id, tipo, marca, modelo);

                Console.WriteLine("Equipo agregado");
            }
            catch
            {
                Console.WriteLine("Error");
            }
            Console.ReadKey();
        }

        static void ListarEquipos()
        {
            if (contadorEquipos == 0)
            {
                Console.WriteLine("No hay equipos");
            }
            else
            {
                for (int i = 0; i < contadorEquipos; i++)
                {
                    Console.WriteLine($"ID: {listaEquipos[i].Id} - {listaEquipos[i].Marca} {listaEquipos[i].Modelo}");
                }
            }
            Console.ReadKey();
        }

        static void BuscarEquipo()
        {
            try
            {
                Console.Write("ID a buscar: ");
                int id = int.Parse(Console.ReadLine());

                for (int i = 0; i < contadorEquipos; i++)
                {
                    if (listaEquipos[i].Id == id)
                    {
                        Console.WriteLine($"Encontrado: {listaEquipos[i].Marca}");
                        Console.ReadKey();
                        return;
                    }
                }

                Console.WriteLine("No encontrado");
            }
            catch
            {
                Console.WriteLine("Error");
            }
            Console.ReadKey();
        }

        static void CrearTicket()
        {
            try
            {
                Console.Write("ID Ticket: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("ID Equipo: ");
                int idEquipo = int.Parse(Console.ReadLine());

                bool existe = false;
                for (int i = 0; i < contadorEquipos; i++)
                {
                    if (listaEquipos[i].Id == idEquipo)
                        existe = true;
                }

                if (!existe)
                {
                    Console.WriteLine("Equipo no existe");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Descripcion: ");
                string desc = Console.ReadLine();

                listaTickets[contadorTickets++] = new Ticket(id, idEquipo, desc);

                Console.WriteLine("Ticket creado");
            }
            catch
            {
                Console.WriteLine("Error");
            }
            Console.ReadKey();
        }

        static void CambiarEstado()
        {
            try
            {
                Console.Write("ID Ticket: ");
                int id = int.Parse(Console.ReadLine());

                for (int i = 0; i < contadorTickets; i++)
                {
                    if (listaTickets[i].Id == id)
                    {
                        Console.WriteLine("1. Abierto\n2. En Proceso\n3. Resuelto");
                        int op = int.Parse(Console.ReadLine());

                        if (op == 1) listaTickets[i].Estado = "Abierto";
                        if (op == 2) listaTickets[i].Estado = "En Proceso";
                        if (op == 3) listaTickets[i].Estado = "Resuelto";

                        Console.WriteLine("Actualizado");
                        Console.ReadKey();
                        return;
                    }
                }

                Console.WriteLine("No encontrado");
            }
            catch
            {
                Console.WriteLine("Error");
            }
            Console.ReadKey();
        }

        static void ListarTickets()
        {
            if (contadorTickets == 0)
            {
                Console.WriteLine("No hay tickets");
            }
            else
            {
                for (int i = 0; i < contadorTickets; i++)
                {
                    Console.WriteLine($"Ticket {listaTickets[i].Id} - Estado: {listaTickets[i].Estado}");
                }
            }
            Console.ReadKey();
        }
    }
    // lo mismo que en la clase ticket, se usa private para encapsular los datos y se accede a través de propiedades públicas
    class Equipo
    {
        private int id;
        private string tipo;
        private string marca;
        private string modelo;

        public int Id { get => id; set => id = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Marca { get => marca; set => marca = value; }
        public string Modelo { get => modelo; set => modelo = value; }

        public Equipo(int id, string tipo, string marca, string modelo)
        {
            this.id = id;
            this.tipo = tipo;
            this.marca = marca;
            this.modelo = modelo;
        }
    }

    class Ticket
    {
        // Se usa private para encapsular los datos y se accede a través de propiedades públicas
        private int id;
        private int idEquipo;
        private string descripcion;
        private string estado;
        // Propiedades públicas para acceder a los campos privados
        // Los set y get 
        public int Id { get => id; set => id = value; }
        public int IdEquipo { get => idEquipo; set => idEquipo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Estado { get => estado; set => estado = value; }

        public Ticket(int id, int idEquipo, string descripcion)
        {
            // los this es para el atributo interno 
            this.id = id;
            this.idEquipo = idEquipo;
            this.descripcion = descripcion;
            this.estado = "Abierto";
        }
    }
}

