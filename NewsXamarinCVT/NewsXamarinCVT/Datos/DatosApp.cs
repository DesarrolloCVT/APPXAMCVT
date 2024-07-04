using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using NewsXamarinCVT.Models;

namespace NewsXamarinCVT.Datos
{
    public class DatosApp
    {
        public string TraeVersion()
        {
            string Vers ="0.0.0" ;
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");

                var rest2 = ClientHttp.GetAsync("api/DatosAPP?idAPP="+1).Result;

                //if (rest2.IsSuccessStatusCode)
                //{
                    var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                    Vers = JsonConvert.DeserializeObject<string>(resultadoStr);                 
                //}
            }
            catch(Exception ex)
            {
                string mns = ex.Message;

            }

            return Vers;
        
        }

        public List<MenuClass> TraeMenu(int idperfil)
        {
            List<MenuClass> ls = new List<MenuClass>();
            try
            {
                HttpClient ClientHttp = new HttpClient();
                ClientHttp.BaseAddress = new Uri("http://wsintranet.cvt.local/");
                var rest2 = ClientHttp.GetAsync("api/Usuario?idPerfilUsuario=" + idperfil).Result;
                var resultadoStr = rest2.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<MenuClass>>(resultadoStr);
            }
            catch { }
            return ls;
        }
    }
}
