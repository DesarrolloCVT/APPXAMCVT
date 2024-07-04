using Newtonsoft.Json;
using SMMWMS.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SMMWMS.Datos
{
    public class DatosSMM_TomaInventario
    {
        public List<TomaInventarioClass> TraeFoliosInventarios()
        {
            List<TomaInventarioClass> ls = new List<TomaInventarioClass>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM").Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<TomaInventarioClass>>(resultadoStr);
            }
            catch { }
            return ls;
        }


        public int ValidaBodegaSMM(string CodBodega)
        {
            int ret = 0;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM?CodBodega=" + CodBodega).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public string ValidaCodProducto(string codProd)
        {
            string ret = "";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM?CodBarraProd=" + codProd).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<string>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public string TraeCodProducti(string codBar)
        {
            string ret = "";
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM?CodBarr=" + codBar).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<string>(resultadoStr);
            }
            catch { }
            return ret;
        }


        public bool insertaInventario(int Inventario_Id, string Dun14, string CodProducto, int Cantidad, int SiteID, int IdUsuario, string UbiPasillo)
        {
            bool ret = false;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/TomaInventarioSMM?vInventario_Id=" + Inventario_Id + "&vDun14=" + Dun14 + "&vCodProducto=" + CodProducto + "&vCantidad=" + Cantidad + "&vSiteID=" + SiteID + "&vIdUsuario=" + IdUsuario + "&vUbiPasillo=" + UbiPasillo).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<bool>(resultadoStr);
            }
            catch { }
            return ret;
        }
    }
}
