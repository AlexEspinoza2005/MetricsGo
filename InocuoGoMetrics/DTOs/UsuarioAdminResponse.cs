namespace InocuoGoMetrics.DTOs
{
    public class UsuarioAdminResponse
    {
        public string idAdm { get; set; }
        public string nombreAdm { get; set; }
        public string correoAdm { get; set; }
        public string passAdm { get; set; }
        public string idOrgAdm { get; set; }
        public DateTime creadoAdm { get; set; }
        public OrganizacionResponse organizacion { get; set; }
    }
}