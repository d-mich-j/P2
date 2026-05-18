using SistemaRecetas.Gestores;
using SistemaRecetas.Modelos;
using SistemaRecetas.Servicios;

var gestor = new GestorRecetas();
var exportador = new ExportadorTxt();
var servicio = new ServicioRecetas(gestor, exportador);

gestor.AgregarReceta(new Receta("Paella Valenciana", "Chef Ramírez", 45));
gestor.AgregarReceta(new Receta("Tacos al Pastor", "Chef González", 30));
gestor.AgregarReceta(new Receta("Risotto de Hongos", "Chef Martínez", 50));
gestor.AgregarReceta(new Receta("Ceviche Clásico", "Chef Ríos", 20));
gestor.AgregarReceta(new Receta("Ramen Tradicional", "Chef Tanaka", 90));
gestor.AgregarReceta(new Receta("Guacamole", "Chef López", 10));
gestor.AgregarReceta(new Receta("Croissant de Mantequilla", "Chef Dubois", 120));
gestor.AgregarReceta(new Receta("Tiramisú", "Chef Rossi", 40));

Console.WriteLine("=== SISTEMA DE GESTIÓN DE RECETAS ===\n");

Console.Write("Ingresa tu nombre de usuario: ");
string nombreUsuario = Console.ReadLine() ?? "Usuario";
var usuario = servicio.RegistrarUsuario(nombreUsuario);

Console.Write("Ingresa el nombre de tu primer libro de recetas: ");
string nombreLibro = Console.ReadLine() ?? "Mi Libro";
usuario.CrearLibroRecetas(nombreLibro);
string libroActual = nombreLibro;

Console.WriteLine($"\nBienvenido, {usuario.Nombre}! Libro actual: '{libroActual}'\n");

bool continuar = true;
while (continuar)
{
    Console.WriteLine("\n=== MENÚ PRINCIPAL ===");
    Console.WriteLine($"Libro actual: '{libroActual}'");
    Console.WriteLine("1. Mostrar recetas del catálogo");
    Console.WriteLine("2. Ordenar libro");
    Console.WriteLine("3. Búsqueda binaria");
    Console.WriteLine("4. Crear nuevo libro");
    Console.WriteLine("5. Cambiar de libro");
    Console.WriteLine("6. Ver mis libros");
    Console.WriteLine("7. Exportar a .txt");
    Console.WriteLine("8. Salir");
    Console.Write("\nElige una opción: ");

    string? entrada = Console.ReadLine();

    if (!int.TryParse(entrada, out int opcion))
    {
        Console.WriteLine("Opción inválida. Por favor ingresa un número.");
        continue;
    }

    switch (opcion)
    {
        case 1:
            Console.WriteLine("\n--- Recetas disponibles en el catálogo ---");
            if (gestor.RecetasDisponibles.Count == 0)
            {
                Console.WriteLine("El catálogo está vacío.");
            }
            else
            {
                for (int i = 0; i < gestor.RecetasDisponibles.Count; i++)
                    Console.WriteLine($"  [{i}] {gestor.RecetasDisponibles[i]}");
            }
            break;

        case 2:
            Console.WriteLine("\nElige el algoritmo de ordenamiento:");
            Console.WriteLine("  1. QuickSort");
            Console.WriteLine("  2. MergeSort");
            Console.Write("Opción: ");
            string? opAlgoritmo = Console.ReadLine();

            string algoritmo = opAlgoritmo == "1" ? "quick" : opAlgoritmo == "2" ? "merge" : "";

            if (string.IsNullOrEmpty(algoritmo))
            {
                Console.WriteLine("Opción inválida.");
                break;
            }

            int tiempoTotal = servicio.OrdenarLibroYCalcularTiempo(usuario, libroActual);
            servicio.OrdenarCatalogo(algoritmo);

            var libroOrdenado = usuario.ObtenerLibro(libroActual);
            Console.WriteLine($"\nLibro '{libroActual}' ordenado:");
            foreach (var r in libroOrdenado)
                Console.WriteLine($"  - {r}");
            Console.WriteLine($"\nTiempo total de preparación: {tiempoTotal} minutos.");
            break;

        case 3:
            Console.Write("\nIngresa el nombre exacto de la receta a buscar: ");
            string? nombreBuscar = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombreBuscar))
            {
                Console.WriteLine("Nombre inválido.");
                break;
            }

            int indice = gestor.BusquedaBinaria(nombreBuscar);
            if (indice == -1)
            {
                Console.WriteLine($"No se encontró ninguna receta con el nombre '{nombreBuscar}'.");
            }
            else
            {
                var copiaOrdenada = gestor.RecetasDisponibles
                    .OrderBy(r => r.Nombre, StringComparer.OrdinalIgnoreCase)
                    .ToList();

                Console.WriteLine($"\nReceta encontrada en índice [{indice}]: {copiaOrdenada[indice]}");
                Console.Write($"¿Deseas agregarla al libro '{libroActual}'? (Escribe el índice para agregar o 10 para cancelar): ");

                string? respuesta = Console.ReadLine();
                if (!int.TryParse(respuesta, out int seleccion) || seleccion == 10)
                {
                    Console.WriteLine("Operación cancelada.");
                }
                else if (seleccion == indice)
                {
                    try
                    {
                        usuario.AgregarRecetaALibro(libroActual, copiaOrdenada[indice]);
                        Console.WriteLine($"Receta '{copiaOrdenada[indice].Nombre}' agregada al libro '{libroActual}'.");
                    }
                    catch (KeyNotFoundException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Índice no coincide. Operación cancelada.");
                }
            }
            break;

        case 4:
            Console.Write("\nIngresa el nombre del nuevo libro: ");
            string? nuevoLibro = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nuevoLibro))
            {
                Console.WriteLine("Nombre inválido.");
                break;
            }
            try
            {
                usuario.CrearLibroRecetas(nuevoLibro);
                Console.WriteLine($"Libro '{nuevoLibro}' creado exitosamente.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            break;

        case 5:
            Console.WriteLine("\n--- Tus libros disponibles ---");
            foreach (var libro in usuario.LibrosRecetas.Keys)
                Console.WriteLine($"  - {libro}");

            Console.Write("Ingresa el nombre del libro al que deseas cambiar: ");
            string? cambio = Console.ReadLine();
            if (usuario.LibrosRecetas.ContainsKey(cambio ?? ""))
            {
                libroActual = cambio!;
                Console.WriteLine($"Libro actual cambiado a '{libroActual}'.");
            }
            else
            {
                Console.WriteLine("El libro especificado no existe.");
            }
            break;

        case 6:
            Console.WriteLine("\n--- Tus libros de recetas ---");
            usuario.MostrarLibros();
            break;

        case 7:
            string ruta = $"{usuario.Nombre}_recetas_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            servicio.Exportador.ExportarATxt(usuario, ruta);
            Console.WriteLine($"\nLibros exportados exitosamente a: {ruta}");
            break;

        case 8:
            Console.WriteLine("\nHasta luego. ¡Buen provecho!");
            continuar = false;
            break;

        default:
            Console.WriteLine("Opción no válida. Elige entre 1 y 8.");
            break;
    }
}
