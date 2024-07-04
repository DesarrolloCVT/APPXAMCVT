using NewsXamarinCVT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NewsXamarinCVT.Datos
{
  public  class DatosProductosCaptura
    {


        public List<ProductosCapturaCod> DatosProductos(string codbarr)
        {
            List<ProductosCapturaCod> ls = new List<ProductosCapturaCod>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/CapturaProductos?codbarr=" + codbarr).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<ProductosCapturaCod>>(resultadoStr);
            }
            catch { }
            return ls;
        }

    }
}
