using FullCatalog.Business;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Text.RegularExpressions;

namespace FullCatalog.App.Extensions
{
    public static class RazorExtensions
    {
        public static string FormatDocumentNumber(this RazorPage page, int type, string document)
        {
            if(!Regex.IsMatch(document, @"^\d+$")) return document;

            return type == 1 ?  Convert.ToUInt64(document).ToString(@"000\.000\.000\-00") 
                                                      :  Convert.ToUInt64(document).ToString(@"00\.000\.000\/0000\-00");
        }

    }
}
