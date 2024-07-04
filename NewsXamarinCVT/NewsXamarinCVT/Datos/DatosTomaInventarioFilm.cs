using NewsXamarinCVT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NewsXamarinCVT.Datos
{
    public class DatosTomaInventarioFilm
    {

        public List<ProductoInventarioFilm> ListProductosInventario(int bobin)
        {
            List<ProductoInventarioFilm> dt = new List<ProductoInventarioFilm>();

            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                var rest = ClientHttp.GetAsync("api/TomaInventarioFilm?bobin=" + bobin).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<List<ProductoInventarioFilm>>(resultadoStr);
            }
            catch
            {

            }

            return dt;

        }
    }
}
