namespace InocuoGoMetrics.API.DTOs
{
    public class DashboardResponse
    {
        public int TotalConversaciones { get; set; }
        public int UsuariosUnicos { get; set; }
        public Dictionary<string, int> TemasRecurrentes { get; set; } = new();
    }

    public class ConversacionResumenDto
    {
        public Guid IdCon { get; set; }  // ✅ Guid
        public string Usuario { get; set; } = string.Empty;
        public string Chatbot { get; set; } = string.Empty;
        public string? EstadoCon { get; set; }
        public DateTime? Iniciocon { get; set; }
        public DateTime? ActualizaCon { get; set; }
    }

    public class MensajeResumenDto
    {
        public long IdMen { get; set; }  // ✅ long
        public string TipoMen { get; set; } = string.Empty;
        public string CuerpoMen { get; set; } = string.Empty;
        public DateTime CreadoMen { get; set; }
    }

}