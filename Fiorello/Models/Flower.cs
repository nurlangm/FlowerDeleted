﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Models
{
    public class Flower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string FlowerCode { get; set; }
        public string Weight { get; set; }
        public string Dimension { get; set; }
        public bool InStock { get; set; }

        public int? CampaignId { get; set; }
        public List<FlowerCategory> FlowerCategories { get; set; }
        public Campaign Campaigns { get; set; }

        public List<FlowerImage> FlowerImages { get; set; }
        public List<FlowerTag> FlowerTags { get; set; }
        [NotMapped]
        public List<int> CategoryIds  { get; set; }
        [NotMapped]

        public IFormFile MainImageFile { get; set; }
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; }
        [NotMapped]

        public List<int>  ImageIds { get; set; }


    }
}
