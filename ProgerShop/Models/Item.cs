using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgerShop.Models
{
    internal class Item
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Image { get; set; }

        public string DisplayImage
        {
            get { return "/Images/" + Image; }
        }
        public Item()
        {
        }

        public Item(string title, string description, float price, string image)
        {
            Title = title;
            Description = description;
            Price = price;
            Image = image;
        }
    }
}