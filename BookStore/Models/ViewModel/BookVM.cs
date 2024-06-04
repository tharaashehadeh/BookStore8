﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookStore.Models.ViewModel
{
	public class BookVM
	{
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public DateTime publishDate { get; set; } 
        public string Publisher { get; set; } = null!;
        public string ImageUrl { get; set; }
        public List<string> Categories { get; set; } 

    }
}
