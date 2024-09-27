using DDigital.Utilidades;
using Microsoft.IdentityModel.Tokens;
using ProyectoDIGITALPERSONA;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDigital.Interfaz
{
    public class Security
    {
        SymmetricSecurityKey ClaveSecreta;
        QUERIES sql = new QUERIES();
        
       
        public Security() {
            INFO_SISTEMA info = trae_info_sistema();
            string clavestring = info.SEQUENCIAL_SISTEMA+"@SISC@"; //secuencial del sistema + que sistema 
            ClaveSecreta = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(text_to256(clavestring))); }

        public string GenerarToken(string tipo, Token_Payload PAYLOAD)
        {
            var credenciales = new List<Claim>();
            if (tipo == "createRol") //para agregar permisos a la tabla PERMISOS
            {
                credenciales = new List<Claim>
                {
                    new Claim("permisos",PAYLOAD.ListaPermisos),
                    new Claim("rol",PAYLOAD.ROL),
                    new Claim("permisos",PAYLOAD.ListaPermisos),
                };
            }
            if (tipo == "toVerifica")
            {
                credenciales = new List<Claim>
                    {
                    new Claim("EsPermitido", PAYLOAD.EsPermitido),
                    new Claim("usuario",PAYLOAD.NOMBRE_USR),
                    new Claim("rol",PAYLOAD.ROL),
                    };
            }
            if (tipo == "toUsuario")
            {
                credenciales = new List<Claim>
                    {
                    new Claim("EsPermitido", PAYLOAD.EsPermitido),
                    new Claim("usuario",PAYLOAD.NOMBRE_USR),
                    new Claim("rol",PAYLOAD.ROL),

                    };
            }

            //firma
            var firmaCredenciales = new SigningCredentials(ClaveSecreta, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "REDES - DDigital",
                audience: "Registro HID",
                claims: credenciales,
                signingCredentials: firmaCredenciales);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
        public Token_Payload VerificarToken(string tokenString)
        {
            Token_Payload tp = new Token_Payload();
            var validaToken = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = ClaveSecreta
            };

            try
            {
                var handler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;
                ClaimsPrincipal claimsPrincipal = handler.ValidateToken(tokenString, validaToken, out validatedToken); // segn esto validas si es valido o no 

                if (claimsPrincipal != null)
                {
                    //tp.EsPermitido = claimsPrincipal.FindFirst("EsPermitido").ToString();

                    foreach (var item in claimsPrincipal.Claims)
                    {
                        switch (item.Type)
                        {
                            case "EsPermitido":
                                tp.EsPermitido = item.Value; break;
                            case "permisos":
                                tp.ListaPermisos = item.Value; break;
                            case "usuario":
                                tp.NOMBRE_USR = item.Value; break;
                            case "rol":
                                tp.ROL = item.Value; break;
                            default:

                                break;
                        }
                    }

                }
                return tp;

            }
            catch (Exception ex)
            {
                MessageBox.Show("El rol no han sido firmado debidamente para esta instancia", "FALLO VALIDACIÓN");
                //Application.Exit();
                return null;
            }
        }
        public string text_to256(string text)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] HASH = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < HASH.Length; i++)
                {
                    stringBuilder.Append(HASH[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }
        public bool VerificarHash256(string text, string hashedText)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] newHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                string newHashString = BitConverter.ToString(newHash).Replace("-", "").ToLower();

                // Comparar los dos hashes
                return newHashString.Equals(hashedText.ToLower());
            }
        }
        public PERMISOS ObtenerPermisos(string usuario)
        {
            work_flow wf = new work_flow();
            Security secu = new Security();
            DataTable dt_acceso = wf.informacion_acceso(usuario);
            Token_Payload pl = new Token_Payload();
            PERMISOS permissions = new PERMISOS();
            if (dt_acceso.Rows.Count > 0)
            {
                string tken_permisos = dt_acceso.Rows[0][3].ToString();
                string tken_acceso = dt_acceso.Rows[0][4].ToString();
                Token_Payload permisos = secu.VerificarToken(tken_permisos);
                Token_Payload acceso = secu.VerificarToken(tken_acceso);
                permissions.nombre_rol = permisos.ROL;
                permissions.Tiene_acceso = acceso.EsPermitido;
                pl = permisos;
                pl.NOMBRE_USR = acceso.NOMBRE_USR;
                pl.EsPermitido = acceso.EsPermitido;

               
                //Dictionary<string, bool> dic = new Dictionary<string, bool>();
                var partes = pl.ListaPermisos.Split(',');
                foreach (var parte in partes)
                {
                    var index_valor = parte.Split(':');
                    var permiso = int.Parse(index_valor[0]);
                    var valor = bool.Parse(index_valor[1]);
                    switch (permiso)
                    {
                        case 0:
                            permissions.RegistrarHuellas = valor;
                            break;
                        case 1:
                            permissions.VerificarHuellas = valor;
                            break;
                        case 2:
                            permissions.VerificarMancomuna = valor;
                            break;
                        case 3:
                            permissions.SeleccionarMano = valor;
                            break;
                        case 4:
                            permissions.SeleccionarDedos = valor;
                            break;
                        case 5:
                            permissions.VerHuellas = valor;
                            break;
                        case 6:
                            permissions.EliminarHuellas = valor;
                            break;
                        case 7:
                            permissions.AdmonUsuarios = valor;
                            break;
                        case 8:
                            permissions.QuitarAccesoUsr = valor;
                            break;
                        case 9:
                            permissions.AdmonRoles = valor;
                            break;
                        default:
                            break;

                    }

                }

            }
            else
            {
                permissions.Tiene_acceso = "NO";
            }

            return permissions;

        }
        public PERMISOS PERMISOS(string token, out Token_Payload acceso)
        {
            work_flow wf = new work_flow();

            PERMISOS pers = new PERMISOS();
            Token_Payload payload1 = new Token_Payload();
            Token_Payload payload2 = new Token_Payload();
            DataTable td_per = new DataTable();
            ODBC_CONN con1 = new ODBC_CONN();
            payload1 = VerificarToken(token);

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                {"@usuario",  payload1.NOMBRE_USR}
            };
            using (td_per = con1.EjecutarConsultaSelect(sql.JOIN_USR_ROL, parametros))
            {
                payload1.ListaPermisos = td_per.Rows[0][3].ToString();
                payload2 = VerificarToken(payload1.ListaPermisos);
                payload1.ROL = payload2.ROL;
                Dictionary<int, bool> DiccionarioPermisos = convertirCadenaDiccionario(payload2.ListaPermisos);

                foreach (var permiso in DiccionarioPermisos)
                {
                    switch (permiso.Key)
                    {
                        case 0:
                            pers.RegistrarHuellas = permiso.Value; break;
                        case 1:
                            pers.VerificarHuellas = permiso.Value; break;
                        case 2:
                            pers.VerificarMancomuna = permiso.Value; break;
                        case 3:
                            pers.VerHuellas = permiso.Value; break;
                        case 4:
                            pers.SeleccionarMano = permiso.Value; break;
                        case 5:
                            pers.SeleccionarDedos = permiso.Value; break;
                        case 6:
                            pers.EliminarHuellas = permiso.Value; break;
                        case 7:
                            pers.AdmonUsuarios = permiso.Value; break;
                        case 8:

                        case 9:
                        default:
                            break;
                    }
                }
            }
            acceso = payload1;
            return pers;
        }
        public Dictionary<int, bool> convertirCadenaDiccionario(string cadena)
        {
            Dictionary<int, bool> diccionario = new Dictionary<int, bool>();
            string[] parescoma = cadena.Split(',');
            // string[] pares = cadena.Split(':');
            foreach (var item in parescoma)
            {
                string[] valores = item.Split(':');

                if (valores.Length == 2 && int.TryParse(valores[0], out int indice) && bool.TryParse(valores[1], out bool estado))
                {
                    diccionario[indice] = estado;
                }
            }

            return diccionario;
        }

        public INFO_SISTEMA trae_info_sistema()
        {
           // string clientName = Environment.GetEnvironmentVariable("CLIENTNAME"); remoto
            work_flow wf = new work_flow();
            INFO_SISTEMA iNFO = new INFO_SISTEMA();
            string secuencial = wf.trae_secuencial();
            iNFO.SEQUENCIAL_SISTEMA = secuencial;
            iNFO.NOMBRE_MAQUINA = Dns.GetHostName();
            IPAddress[] ipAddresses = Dns.GetHostAddresses(iNFO.NOMBRE_MAQUINA);
            string ipAddress = string.Empty;

            foreach (IPAddress ip in ipAddresses)
            {              
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    iNFO.IP_MAQUINA = ip.ToString();
                    break; 
                }
            }

            iNFO.USUARIO_MAQUINA = Environment.UserName;
            iNFO.MACHINE_NAME = Environment.MachineName;           
            iNFO.DOMINIO_MAQUINA= Environment.UserDomainName;



            string r = "";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
            {
                ManagementObjectCollection information = searcher.Get();
                if (information != null)
                {
                    foreach (ManagementObject obj in information)
                    {
                        iNFO.VERION_OS_MAQUINA  = obj["Caption"].ToString() + " - " + obj["OSArchitecture"].ToString();
                    }
                }
                r = r.Replace("NT 5.1.2600", "XP");
                r = r.Replace("NT 5.2.3790", "Server 2003");
         
            }
            return iNFO;

        }
    }
}
