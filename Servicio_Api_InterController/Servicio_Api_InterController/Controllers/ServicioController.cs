using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Servicio_Api_InterController_Peter.Models;
using System.Diagnostics;

namespace Servicio_Api_InterController.Controllers
{

    [ApiController]
    [Route("mando")]
    public class ServicioController : ControllerBase
    {
        private string fileListApps = "listado_apps.json";

        [HttpGet]
        [Route("conexion")]
        public dynamic conexion()
        {
            return this.StatusCode(200);
        }

        [HttpGet]
        [Route("apagar")]
        public dynamic apagarOrdenador()
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.StartInfo.Arguments = "/c shutdown /s /t 60";
            process.Start();
            return this.StatusCode(200);
        }

        [HttpGet]
        [Route("volumen")]
        public dynamic subirVolumen(int Vol)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            String texto = "/c setvol " + Vol;
            process.StartInfo.Arguments = texto;
            process.Start();
            return this.StatusCode(200);
        }


        [HttpGet]
        [Route("reiniciar")]
        public dynamic reiniciarOrdenador()
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.StartInfo.Arguments = "/c shutdown /r /t 60";
            process.Start();
            return this.StatusCode(200);
        }

        [HttpGet]
        [Route("sesion")]
        public dynamic cerrarSesionOrdenador()
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.StartInfo.Arguments = "/c shutdown /h";
            process.Start();
            return this.StatusCode(200);
        }

        [HttpGet]
        [Route("aplicaciones")]
        public dynamic getAplicaciones()
        {
            var list_aplications = new List<Aplicacion>();
            try
            {
                foreach (string line in System.IO.File.ReadLines(fileListApps))
                {
                    Aplicacion aplications = JsonConvert.DeserializeObject<Aplicacion>(line);
                    list_aplications.Add(aplications);
                }
            }
            catch (FileNotFoundException ignored)
            {
            }
            return list_aplications;
        }


        [HttpGet]
        [Route("ejecutarAplicacion")]
        public dynamic getAplicacionesId(int id)
        {
            Aplicacion aplicacion = null;
            foreach (string line in System.IO.File.ReadLines(fileListApps))
            {
                aplicacion = JsonConvert.DeserializeObject<Aplicacion>(line);
                if (aplicacion.Id == id)
                {
                    break;
                }
            }
            if (aplicacion != null)
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = aplicacion.Ruta;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    process.Start();
                    return this.StatusCode(200);
                }
                catch (Exception)
                {
                    return this.StatusCode(404);
                }
            }
            else
            {
                return this.StatusCode(599);
            }

        }



        [HttpGet]
        [Route("pausar")]
        public dynamic cerrarSesionPausar()
        {
            Process process = new Process();
            process.StartInfo.FileName = "powershell.exe";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.StartInfo.Arguments = ".\\enter.ps1";
            process.Start();
            return this.StatusCode(200);

        }


        [HttpGet]
        [Route("pasar")]
        public dynamic cerrarSesionpasar(int flecha)
        {
            if (flecha == 1) {
                Process process = new Process();
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                process.StartInfo.Arguments = ".\\adelantar.ps1";
                process.Start();
                return this.StatusCode(200);
            }else if (flecha == 2)
            {
                Process process = new Process();
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                process.StartInfo.Arguments = ".\\atrasar.ps1";
                process.Start();
                return this.StatusCode(200);
            }
            return this.StatusCode(401);


        }

        [HttpGet]
        [Route("tab")]
        public dynamic cerrarSesionTab()
        {
            Process process = new Process();
            process.StartInfo.FileName = "powershell.exe";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.StartInfo.Arguments = ".\\tab.ps1";
            process.Start();
            return this.StatusCode(200);

        }

    }
}