﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NewsXamarinCVT.Models
{
    public class SMMProductosOrdenDeVenta
    {
            public string Codproducto { get; set; }
            public string Producto { get; set; }
            public int Familia { get; set; }
            public int UnxCaja { get; set; }
            public int CostoPromedio { get; set; }
            public int UltimoPrecioCompra { get; set; }
            public int Precio { get; set; }
            public float Margen { get; set; }
            public int Contribucion { get; set; }
            public float Stock { get; set; }      

    }
}
