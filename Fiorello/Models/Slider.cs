using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SliderImage { get; set; }
        public string Subtitle { get; set; }
        public string SignatureImage { get; set; }
        [NotMapped]
        public IFormFile  ImageFile { get; set; }
        [NotMapped]
        public IFormFile  SignatureFile { get; set; }
    }
}
