using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using NewsXamarinCVT.Models;
using System.Data;

namespace NewsXamarinCVT.Datos
{
    public class DatosTrazabilidadPallet
    {
        public DatosTrazabilidadPallet(){}

        public List<TrazabilidadPaletClass> BuscaTraabilidadPallet(int npallet)
        {
            List<TrazabilidadPaletClass> dt = new List<TrazabilidadPaletClass>();

            try {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                var rest = ClientHttp.GetAsync("api/TrazabilidadPallet?NumPallet=" + npallet).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<List<TrazabilidadPaletClass>>(resultadoStr);


            }
            catch {

            }

            return dt;       


        }

        public DataTable DetalleTrazabilidadPallet(int npallet)
        {
            DataTable dt = new DataTable();

            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                var rest = ClientHttp.GetAsync("api/TrazabilidadPallet?NPallet=" + npallet).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<DataTable>(resultadoStr);


            }
            catch
            {

            }

            return dt;


        }

    }
}
