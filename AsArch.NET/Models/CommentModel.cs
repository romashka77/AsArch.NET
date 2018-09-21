using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//удалить
namespace AsArch.NET.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }
    }
}