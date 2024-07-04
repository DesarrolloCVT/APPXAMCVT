﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NewsXamarinCVT.Models
{
    public class ValidadorProductosSMMClass
    {
            public string BcdCode { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public float Price { get; set; }
            public string UM { get; set; }
            public float CantxUM { get; set; }
            public string GrupoArticulo { get; set; }
            public string Estado { get; set; }
            public string DescFlequera { get; set; }      
            public float StockUnidadesSala { get; set; }
    }
}