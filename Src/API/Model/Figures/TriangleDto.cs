namespace API.Model.Figures
{
    public sealed class TriangleDto : IFigureDto
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
    }
}