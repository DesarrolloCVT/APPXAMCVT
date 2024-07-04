using NewsXamarinCVT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;

namespace NewsXamarinCVT.Datos
{
    public class DatosTrazabilidadSMM
    {
        //public List<SMMTrazabilidadBusqueda> BuscaTrazaSMM(string NumPallet)
        //{
        //    List<SMMTrazabilidadBusqueda> ls = new List<SMMTrazabilidadBusqueda>();

        //    try
        //    {

        //        HttpClient ClientHttp = new HttpClient();
        //        ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

        //        var rest = ClientHttp.GetAsync("api/TrazabilidadSMM?NumPallet="+NumPallet).Result;
        //        var resultadoStr = rest.Content.ReadAsStringAsync().Result;
        //        ls = JsonConvert.DeserializeObject<List<SMMTrazabilidadBusqueda>>(resultadoStr);

        //    }
        //    catch
        //    {

        //    }

        //    return ls;
        //}

        
        public DataTable DetalleTrazaSMM(string PalletTraza)
        {
            DataTable dt = new DataTable();

            try
            {

                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                var rest = ClientHttp.GetAsync("api/TrazabilidadSMM?PalletTraza=" + PalletTraza).Result;
                var resultadoStr = rest.Content.ReadAsStringAsync().Result;
                dt = JsonConvert.DeserializeObject<DataTable>(resultadoStr);


            }
            catch
            {

            }

            return dt;

        }

        public List<SMMTrazabilidadBusqueda> ObtienedatosTraza(string SSCC)
        {
            List<SMMTrazabilidadBusqueda> ls = new List<SMMTrazabilidadBusqueda>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TrazabilidadSMM?NumPallet=" + SSCC).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<SMMTrazabilidadBusqueda>>(resultadoStr);
            }
            catch { }
            return ls;
        }

    }
}
