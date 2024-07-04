using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NewsXamarinCVT.Datos
{
    public class DatosExtintores
    {
        public DatosExtintores() { }

        public int insertaNuevoCheck(int Usuario, int NumExtintor, string Vigencia, string UbicaExtintor, int PesoExtintor, string TipoAgente)
        {
            int ret = 0;
            try
            {
                //string Fvenc = Fvencimiento + " " + "00:00:00.000";
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/Extintores?Usuario=" + Usuario + "&NumExtintor=" + NumExtintor + "&Vigencia=" + Vigencia + "&UbicaExtintor=" + UbicaExtintor + "&PesoExtintor=" + PesoExtintor + "&TipoAgente=" + TipoAgente).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public int insertaNuevoDetalle(int idExtintor, string resp,string observacion,string pregun)
        {
            int ret = 0;
            try
            {
                //string Fvenc = Fvencimiento + " " + "00:00:00.000";
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/Extintores?idExtintor=" + idExtintor + "&resp=" + resp + "&obs=" + observacion + "&preg=" + pregun).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public int insertaNuevoEntorno(string pregunta,string respuesta,string observacion,int idRevisionEx)
        {
            int ret = 0;
            try
            {
                //string Fvenc = Fvencimiento + " " + "00:00:00.000";
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/Extintores?pregunta=" + pregunta + "&respuesta=" + respuesta + "&observacion=" + observacion + "&idRevisionEx=" + idRevisionEx).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }

        public int insertaRegistroFosfina(string hora, string FechaFumi, string Bodega, string MayorPP, string A1, string Distancia)
        {
            int ret = 0;
            try
            {
                //string Fvenc = Fvencimiento + " " + "00:00:00.000";
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/Extintores?hora=" + hora + "&FechaFumi=" + FechaFumi + "&Bodega=" + Bodega + "&MayorPP=" + MayorPP + "&A1=" + A1 + "&Distancia=" + Distancia).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ret = JsonConvert.DeserializeObject<int>(resultadoStr);
            }
            catch { }
            return ret;
        }
    }
}
