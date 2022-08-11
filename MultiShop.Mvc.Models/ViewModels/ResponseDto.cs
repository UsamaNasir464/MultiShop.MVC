using System;
using System.Collections.Generic;

namespace MultiShop.Mvc.Models.ViewModels
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public Object Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public List<string>ErrorMessage  { get; set; }
    }
}
