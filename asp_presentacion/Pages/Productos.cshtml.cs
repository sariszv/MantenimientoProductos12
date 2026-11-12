using lib_dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace asp_presentacion.Pages
{
    public class ProductosModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlServicio = "http://localhost:5000/Productos/";

        public ProductosModel()
        {
            _httpClient = new HttpClient();
        }

        [BindProperty]
        public Productos Actual { get; set; } = new Productos();

        public List<Productos> ListaProductos { get; set; } = new List<Productos>();
        public List<Categorias> ListaCategorias { get; set; } = new List<Categorias>();
        public List<Proveedores> ListaProveedores { get; set; } = new List<Proveedores>();
        public string? MensajeError { get; set; }

        public async Task OnGetAsync()
        {
            await CargarDatosUnicoServicio();
        }

        private async Task CargarDatosUnicoServicio()
        {
            try
            {
                var content = new StringContent("{}", Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_urlServicio + "CargarTodo", content);
                var json = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (root.TryGetProperty("Respuesta", out var resp) && resp.GetString() == "OK")
                {
                    ListaProductos = JsonSerializer.Deserialize<List<Productos>>(
                        root.GetProperty("Productos").GetRawText()) ?? new();

                    ListaCategorias = JsonSerializer.Deserialize<List<Categorias>>(
                        root.GetProperty("Categorias").GetRawText()) ?? new();

                    ListaProveedores = JsonSerializer.Deserialize<List<Proveedores>>(
                        root.GetProperty("Proveedores").GetRawText()) ?? new();
                }
                else if (root.TryGetProperty("Error", out var err))
                {
                    MensajeError = err.GetString();
                }
            }
            catch (Exception ex)
            {
                MensajeError = "Error conectando con el servicio: " + ex.Message;
            }
        }

        public async Task<IActionResult> OnPostGuardarAsync()
        {
            var json = JsonSerializer.Serialize(Actual);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(_urlServicio + "Guardar", content);

            Actual = new Productos();
            ModelState.Clear();
            await CargarDatosUnicoServicio();
            return Page();
        }

        public async Task<IActionResult> OnPostEditarAsync(int id)
        {
            await CargarDatosUnicoServicio();
            var encontrado = ListaProductos.FirstOrDefault(x => x.Id == id);
            if (encontrado != null)
            {
                Actual = encontrado;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            var json = JsonSerializer.Serialize(new Productos { Id = id });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(_urlServicio + "Borrar", content);

            await CargarDatosUnicoServicio();
            return Page();
        }

        public async Task<IActionResult> OnPostCancelarAsync()
        {
            Actual = new Productos();
            ModelState.Clear();
            await CargarDatosUnicoServicio();
            return Page();
        }
    }
}
