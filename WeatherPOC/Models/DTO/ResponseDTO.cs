﻿using System;

namespace WeatherPOC.Models.DTO
{
    public class ResponseDTO
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Msg { get; set; } = string.Empty;
    }
}

