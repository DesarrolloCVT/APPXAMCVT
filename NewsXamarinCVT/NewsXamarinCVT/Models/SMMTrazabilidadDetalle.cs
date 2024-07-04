using System;
using System.Collections.Generic;
using System.Text;

namespace NewsXamarinCVT.Models
{
    public class SMMTrazabilidadDetalle
    {
           public int Package_Id { get; set; }
            public string NPallet { get; set; }
            public string Entidad { get; set; }
            public string Operacion { get; set; }
            public int IDentidad { get; set; }
            public string Tipo { get; set; }
            public int Ndocumento { get; set; }
            public float CantidadInicial { get; set; }
            public string NombreUsuario { get; set; }
            public DateTime fecha { get; set; }
            public DateTime Package_ProductionDate { get; set; }
            public DateTime Package_ExpiresDate { get; set; }
            public string Reception_Note { get; set; }
            public string Package_Lot { get; set; }
            public string CodProducto { get; set; }
            public string Article_Description { get; set; }
            public string Estado { get; set; }
            public string Bodega { get; set; }       

    }
}
