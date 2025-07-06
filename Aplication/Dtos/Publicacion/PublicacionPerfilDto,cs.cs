namespace Aplication.Dtos.Publicacion
{
    public class PublicacionPerfilDto
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<string> RutasArchivos { get; set; } = new List<string>();
        public bool TieneMultiplesArchivos => RutasArchivos.Count > 1;
        public bool TieneVideo => RutasArchivos.Any(r => r.Contains("video") || r.EndsWith(".mp4") || r.EndsWith(".mkv"));
        public string PrimeraImagen => RutasArchivos.FirstOrDefault();
    }
}
