using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfBoards.Shared
{
    public class SurfBoard
    {

        public Guid Id { get; set; }

        // None of the properties is required, as we only GET information by guid
        //[Required]

        public string BoardName { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Thickness { get; set; }
        public double Volume { get; set; }

        public string Boardtype { get; set; }

        public double Price { get; set; }
        public string Equipment { get; set; }
        public string ImageName { get; set; }

    }
}
