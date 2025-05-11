using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find24Avalonia.Models
{
    public static class Helper
    {
        public static string? GetStinger(string str)
        {
            return str switch
            {
                "0 7 0 9" => "XiangGu is the MOST HANDSOME!!!",
                "0 6 0 8" => "Hello Mr. Coffee :)",
                "0 9 1 3" => "Cold girl?",
                _ => null,
            };
        }
    }
}
