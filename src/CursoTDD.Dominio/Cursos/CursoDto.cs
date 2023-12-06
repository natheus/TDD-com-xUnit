namespace CursoTDD.Dominio.Cursos
{
    public class CursoDto
    {
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public double CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public double Valor { get; set; }
    }
}