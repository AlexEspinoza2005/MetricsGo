namespace InocuoGoMetrics.Models
{
    public class DashboardViewModel
    {
        public int TotalConversaciones { get; set; }
        public int UsuariosUnicos { get; set; }
        public Dictionary<string, int> TemasRecurrentes { get; set; } = new Dictionary<string, int>();
    }
}