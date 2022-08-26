namespace Surf_Boards.Models
{
    public class SurfBoard
    {
        public Guid Id { get; set; }
        public string BoardName { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Thickness { get; set; }
        public double Volume { get; set; }
        public string Boardtype { get; set; }
        public double Price { get; set; }
        public string Equipment { get; set; }
        public SurfBoard()
        {

        }
    }
}
