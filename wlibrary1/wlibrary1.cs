using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;
using intra_library;
using Oracle.ManagedDataAccess.Client;

namespace wlibrary1
{
    public class wfunciones1
    {
        public DataTable dt, dt2, aux;
        public String error, server, min, elodbc, cns, cadena, lanissan;
        OdbcConnection conn;
        public bool sistema_global, sindato;
        private OracleConnection cnx;
        string segmento="11";


        public bool show_select(String columnas, Boolean distinto, String tabla, String where, String group, String order)
        {
            funciones obj = new funciones();
            try
            {
                String sql = "select ";

                if (distinto) sql += "distinct " + columnas + " from " + tabla;
                else sql += columnas + " from " + tabla;

                if (where != "") sql += " where " + where;
                if (group != "") sql += " group by " + group;
                if (order != "") sql += " order by " + order;


                if (obj.conexion("cons", sql))
                {
                    if (obj.dt.Rows.Count < 1)
                    {
                        error = "Sin Datos";
                        obj.dt.Dispose();
                        obj.dt = null;
                        obj = null;
                        return false;
                    }
                    else
                    {
                        dt = obj.dt.Copy();
                        obj.dt.Dispose();
                        obj.dt = null;
                        obj = null;
                        return true;
                    }
                }
                else
                {
                    error = obj.error;
                    obj.dt.Dispose();
                    obj.error = null;
                    obj = null;
                    return false;
                }
            }
            catch (Exception er)
            {
                error = er.ToString();
                obj = null;
                return false;
            }
        }


        public bool show_join(String columnas, String tabla, String join, String on, String where, String group, String order)
        {
            funciones obj = new funciones();
            try
            {
                String sql = "select " + columnas + " from " + tabla + " inner join " + join + " on " + on;

                if (where != "") sql += " where " + where;
                if (group != "") sql += " group by " + group;
                if (order != "") sql += " order by " + order;


                if (obj.conexion("cons", sql))
                {
                    if (obj.dt.Rows.Count < 1)
                    {
                        error = "Sin Datos";
                        obj.dt.Dispose();
                        obj.dt = null;
                        obj = null;
                        return false;
                    }
                    else
                    {
                        dt = obj.dt.Copy();
                        obj.dt.Dispose();
                        obj.dt = null;
                        obj = null;
                        return true;
                    }
                }
                else
                {
                    error = obj.error;
                    obj.dt.Dispose();
                    obj.error = null;
                    obj = null;
                    return false;
                }
            }
            catch (Exception er)
            {
                error = er.ToString();
                obj = null;
                return false;
            }
        }

        public bool show_sql(String sql)
        {
            funciones obj = new funciones();
            try
            {
                if (obj.conexion("cons", sql))
                {
                    if (obj.dt.Rows.Count < 1)
                    {
                        error = "Sin Datos";
                        obj.dt.Dispose();
                        obj.dt = null;
                        obj = null;
                        return false;
                    }
                    else
                    {
                        dt = obj.dt.Copy();
                        obj.dt.Dispose();
                        obj.dt = null;
                        obj = null;
                        return true;
                    }
                }
                else
                {
                    error = obj.error;
                    obj.error = null;
                    obj = null;
                    return false;
                }
            }
            catch (Exception er)
            {
                error = er.ToString();
                obj = null;
                return false;
            }
        }

        public bool data(String cmd, String columnas, String values, String tabla, String where)
        {
            funciones obj = new funciones();
            String sql = "";

            if (cmd == "insert") sql = "insert into " + tabla + " values(" + values + ")";
            if (cmd == "update") sql = "update " + tabla + " set " + values;
            if (cmd == "delete") sql = "delete from " + tabla;
            if (where != "") sql += " where " + where;

            if (obj.conexion("mod", sql)) return true;
            else
            {
                error = obj.error;
                obj.error = null;
                obj = null;
                return false;
            }
        }

        public bool mod_sql(String sql)
        {
            funciones obj = new funciones();

            if (obj.conexion("mod", sql)) return true;
            else
            {
                error = obj.error;
                obj.error = null;
                obj = null;
                return false;
            }
        }

        private Boolean get_dato(String cad)
        {
            try
            {
                sindato = false;
                if (show_sql(cad))
                {
                    aux = null;
                    aux = dt;
                    dt.Dispose();
                    return true;
                }
                else
                {
                    if (error == "Sin Datos") sindato = true;
                    return false;
                }
            }
            catch (Exception er)
            {
                error = er.ToString();
                return false;
            }
        }

        public bool conexion2(String quien, String marca, String lugaragen, String lalista, Boolean ventas_global)
        {
            try
            {
                int ip = 0;
                string filtro = "";

                if (marca == "NISSAN")
                {
                    segmento = "10";

                    if (lugaragen == "SAN JERONIMO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "SAN JERONIMO LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "LAS AGUILAS")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='3'";
                    }
                    else if (lugaragen == "TLALPAN")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='4'";
                    }
                    else if (lugaragen == "TLALPAN LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='5'";
                    }
                    else if (lugaragen == "LA JOYA")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='6'";
                    }
                    else if (lugaragen == "CROSSING")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='7'";
                    }
                    else if (lugaragen == "LA JOYA LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='8'";
                    }
                    else
                    {
                        return true;
                        /***  no tengo webservice para los datos del cliente en forma de lista solo de uno en uno
                       String cambio2;

                        if ((lugaragen == "MANTE") || (lugaragen == "VALLES"))
                        {   
                            golfoservicio2.ServicioSiaClient cliente2 = new golfoservicio2.ServicioSiaClient();
                            lanissan = cliente2.enc2(quien, marca, lugaragen, data);
                            return true;
                        }
                        else if ((lugaragen == "TUXPAN") || (lugaragen == "POZA RICA"))
                        {
                            if (lugaragen == "POZA RICA") cambio2 = "POZA";
                            else cambio2 = lugaragen;

                            golfoservicio4.ServicioSiaClient cliente4 = new golfoservicio4.ServicioSiaClient();
                            lanissan = cliente4.enc2(quien, marca, cambio2, data);
                            return true;
                        }
                       **********/
                    }
                }
                else if (marca == "JETOUR")
                {
                    segmento = "10";

                    if (lugaragen == "SALTILLO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='20'";
                    }
                    else if (lugaragen == "LAS TORRES")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='21'";
                    }
                    else if (lugaragen == "LAS TORRES LAREDO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='28'";
                    }
                }
                else if (marca == "GEELY")
                {
                    segmento = "10";

                    if (lugaragen == "SENDERO")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "SENDERO FRONTERA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='3'";
                    }
                    else if (lugaragen == "LINDA VISTA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "LINDA VISTA FRONTERA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='4'";
                    }
                    else if (lugaragen == "CONTRY")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='5'";
                    }
                    else if (lugaragen == "CONCORDIA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='7'";
                    }
                }
                else if (marca == "GREAT WALL")
                {
                    segmento = "10";

                    if (lugaragen == "SENDERO")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "SENDERO FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='4'";
                    }
                    else if (lugaragen == "GONZALITOS")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "GONZALITOS FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "OMODA")
                {
                    segmento = "10";

                    if (lugaragen == "LINDA VISTA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "GONZALITOS")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "GONZALITOS LAREDO")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "FORD")
                {
                    if (lugaragen == "VALLE")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "VALLE LAREDO")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (lugaragen == "VASCONCELOS")
                    {
                        ip = 164;
                    }
                }
                else if (marca == "MERCEDES BENZ") return false;
                else if (marca == "MG")
                {
                    if (lugaragen == "LINDA VISTA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='10'";
                    }
                    else if (lugaragen == "LINDA VISTA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='29'";
                    }
                    else if (lugaragen == "CONSTITUYENTES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='31'";
                    }
                    else if (lugaragen == "CONSTITUYENTES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='40'";
                    }
                    else if (lugaragen == "LAS TORRES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='9'";
                    }
                    else if (lugaragen == "LAS TORRES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='33'";
                    }
                    else if (lugaragen == "TEC")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='37'";
                    }                    
                    else if (lugaragen == "VALLE")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='11'";
                    }
                    else if (lugaragen == "VALLE LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='38'";
                    }
                    else if (lugaragen == "JURIQUILLA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='23'";
                    }
                    else if (lugaragen == "JURIQUILLA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='39'";
                    }
                    else if (lugaragen == "LOMAS VERDES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='14'";
                    }
                    else if (lugaragen == "LOMAS VERDES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='34'";
                    }
                    else if (lugaragen == "TIJUANA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='19'";
                    }
                    else if (lugaragen == "TIJUANA SUCURSAL")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='17'";
                    }
                    else if (lugaragen == "CORREGIDORA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='12'";
                    }
                    else if (lugaragen == "CORREGIDORA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='30'";
                    }
                    else if (lugaragen == "QUERETARO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='13'";
                    }
                    else if (lugaragen == "LOPEZ MATEOS")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='16'";
                    }
                    else if (lugaragen == "LOPEZ MATEOS LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='35'";
                    }
                    else if (lugaragen == "LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='9' and AGEN_IDAGENCIA='15'";
                    }
                    else if (lugaragen == "MEXICALI")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='12' and AGEN_IDAGENCIA='18'";
                    }
                    else if (lugaragen == "LINARES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='26'";
                    }
                    else if (lugaragen == "TOREO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='41'";
                    }
                }
                else if (marca == "CHIREY")
                {
                    if (lugaragen == "LINDA VISTA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "LINDA VISTA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='7'";
                    }
                    else if (lugaragen == "FLETEROS")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "FLETEROS LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='5'";
                    }
                    else if (lugaragen == "CORREGIDORA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='3'";
                    }
                    else if (lugaragen == "CORREGIDORA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='6'";
                    }
                    else if (lugaragen == "JURIQUILLA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='4'";
                    }
                    else if (lugaragen == "JURIQUILLA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='8'";
                    }
                }
                else if (marca == "CHANGAN")
                {
                    segmento = "10";

                    if (lugaragen == "CORREGIDORA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='5'";
                    }
                }
                else if (marca == "KIA")
                {
                    if (lugaragen == "GONZALITOS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "GONZALITOS FRONTERA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='7'";
                    }
                    else if (lugaragen == "FLETEROS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='9'";
                    }
                    else if (lugaragen == "LINDA VISTA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "LINDA VISTA FRONTERA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (lugaragen == "REYNOSA") ip = 173;
                    else if (lugaragen == "LAREDO") ip = 78;
                    else if (lugaragen == "SANTA CATARINA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='5'";
                    }
                    else if (lugaragen == "LA FE")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='11'";
                    }
                }
                else if (marca == "CHRYSLER")
                {
                    if (lugaragen == "CUMBRES")
                    {
                        ip = 75;
                    }
                    else if (lugaragen == "SANTA LUCIA")
                    {
                        segmento = "11";
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (lugaragen == "SANTA LUCIA LAREDO")
                    {
                        segmento = "10";
                        ip = 27;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='32'";
                    }
                    else if (lugaragen == "P.ABASTOS")
                    {
                        ip = 79;
                    }
                    else if (lugaragen == "LINARES")
                    {
                        ip = 79;
                    }
                    else if (lugaragen == "CONTRY")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (lugaragen == "CONTRY LAREDO")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='36'";
                    }
                }
                else if ((marca == "FIAT") && (lugaragen == "CUMBRES")) ip = 75;
                else if (marca == "MITSUBISHI")
                {
                    if (lugaragen == "SANTA LUCIA")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '14' and AGEN_IDAGENCIA = '4' and AGEN_NOMAGENCIA = 'CARONEMOTORS'";
                    }
                    else if (lugaragen == "SALTILLO")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5'";
                        //filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5' and AGEN_NOMAGENCIA = ''";
                    }
                }
                else if ((marca == "ALFA ROMEO") && (lugaragen == "CONTRY")) ip = 98;
                else if ((marca == "CHEVROLET") && (lugaragen == "TLALPAN")) ip = 38;
                else if ((marca == "CHEVROLET") && (lugaragen == "LAS BOMBAS")) ip = 82;
                else if ((marca == "CHEVROLET") && (lugaragen == "RUIZ CORTINES")) { segmento = "10"; ip = 33; }
                else if ((marca == "CHEVROLET") && (lugaragen == "NOGALAR")) ip = 160;
                else if ((marca == "CHEVROLET") && (lugaragen == "CONCORDIA")) ip = 46;
                else if ((marca == "CHEVROLET") && (lugaragen == "LAS TORRES"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '3'";
                }
                else if ((marca == "CHEVROLET") && (lugaragen == "LAS TORRES LAREDO"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '8'";
                }
                else if ((marca == "CHEVROLET") && (lugaragen == "ALLENDE")) ip = 84;
                else if ((marca == "CHEVROLET") && (lugaragen == "UNIVERSIDAD")) ip = 46;
                else
                {
                    if (show_minempresas(marca, lugaragen))
                    {
                        min = dt2.Rows[0][0].ToString();
                        elodbc = "dsn=" + dt2.Rows[0][1].ToString() + ";";
                        dt2.Dispose();
                        dt2 = null;
                    }
                    else
                    {
                        error = "Error: no conecto minempresas";
                        dt2.Dispose();
                        dt2 = null;
                        return false;
                    }
                    if (!conectar()) return false;
                    try
                    {
                        OdbcCommand sqlCmd = new OdbcCommand(envolvente2(quien, lalista, marca, lugaragen), conn);
                        OdbcDataAdapter sqlDa = new OdbcDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        sqlCmd.Dispose();
                        sqlCmd = null;
                        sqlDa.Dispose();
                        sqlDa = null;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        return false;
                    }
                }

                if (ip > 0)
                {
                    if (conectar2(ip))
                    {
                        try
                        {
                            String sql1 = "", invent = "";

                            if (ventas_global)
                            {
                                invent = lalista;
                            }
                            else invent = lalista.Replace(",", "','").Replace(" ", "  ");

                            if ((marca == "CHEVROLET") && (lugaragen == "LAS TORRES"))
                            {
                                if (quien == "venta") sql1 = "select INVENTARIO,VEHI_SERIE,FAAU_NOFACTURA,FAAU_FECHA,CTAS_DESCRIPTABLERO,DESCMODELO,VEHI_ANIO_MODELO,COLOR,VEHI_MOTOR,CLIE_CLAVE,(CLIE_NOMBRE|| ' '||CLIE_APELLIDOPATERNO|| ' '||CLIE_APELLIDOMATERNO) as CLIENTE,FAAU_RAZONFACTURA,CLIE_EMAIL,CLIE_TELEFONO1,NOMVENDEDOR,VEND_EMAIL from AUTOS.VT_VLIBRODEVENTAS where " + lalista + " and AGEN_NOMAGENCIA = 'LAS TORRES'";
                                ///else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and CANALVENTA='PUBLICAS' and ORSE_PREFIJO='O' and FASE_ORSE_FOLIO in ('" + invent + "') and AGEN_NOMAGENCIA = 'LAS TORRES'";
                                else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and FASE_ORSE_FOLIO in ('" + invent + "') and AGEN_NOMAGENCIA = 'LAS TORRES'";
                            }
                            else if ((marca == "CHEVROLET") && (lugaragen == "ALLENDE"))
                            {
                                if (quien == "venta") sql1 = "select INVENTARIO,VEHI_SERIE,FAAU_NOFACTURA,FAAU_FECHA,CTAS_DESCRIPTABLERO,DESCMODELO,VEHI_ANIO_MODELO,COLOR,VEHI_MOTOR,CLIE_CLAVE,(CLIE_NOMBRE|| ' '||CLIE_APELLIDOPATERNO|| ' '||CLIE_APELLIDOMATERNO) as CLIENTE,FAAU_RAZONFACTURA,CLIE_EMAIL,CLIE_TELEFONO1,NOMVENDEDOR,VEND_EMAIL from AUTOS.VT_VLIBRODEVENTAS where " + lalista + " and AGEN_NOMAGENCIA = 'ALLENDE'";
                                /////else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and CANALVENTA='PUBLICAS' and ORSE_PREFIJO='O' and FASE_ORSE_FOLIO in ('" + invent + "') and AGEN_NOMAGENCIA = 'ALLENDE'";
                                else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and FASE_ORSE_FOLIO in ('" + invent + "') and AGEN_NOMAGENCIA = 'ALLENDE'";
                            }
                            else if ((marca == "FORD") && (lugaragen == "VASCONCELOS"))
                            {
                                if (quien == "venta") sql1 = "select INVENTARIO,VEHI_SERIE,FAAU_NOFACTURA,FAAU_FECHA,CTAS_DESCRIPTABLERO,DESCMODELO,VEHI_ANIO_MODELO,COLOR,VEHI_MOTOR,CLIE_CLAVE,(CLIE_NOMBRE|| ' '||CLIE_APELLIDOPATERNO|| ' '||CLIE_APELLIDOMATERNO) as CLIENTE,FAAU_RAZONFACTURA,CLIE_EMAIL,CLIE_TELEFONO1,NOMVENDEDOR,VEND_EMAIL from AUTOS.VT_VLIBRODEVENTAS where " + invent + " and AGEN_NOMAGENCIA='VASCONCELOS'";
                                ///else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and CANALVENTA='PUBLICAS' and ORSE_PREFIJO='ASA' and AGEN_NOMAGENCIA='VASCONCELOS' and FASE_ORSE_FOLIO in ('" + invent + "')";
                                else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and AGEN_NOMAGENCIA='VASCONCELOS' and FASE_ORSE_FOLIO in ('" + invent + "')";
                            }
                            else if ((marca == "FORD") && (lugaragen == "VALLE"))
                            {
                                if (quien == "venta") sql1 = "select INVENTARIO,VEHI_SERIE,FAAU_NOFACTURA,FAAU_FECHA,CTAS_DESCRIPTABLERO,DESCMODELO,VEHI_ANIO_MODELO,COLOR,VEHI_MOTOR,CLIE_CLAVE,(CLIE_NOMBRE|| ' '||CLIE_APELLIDOPATERNO|| ' '||CLIE_APELLIDOMATERNO) as CLIENTE,FAAU_RAZONFACTURA,CLIE_EMAIL,CLIE_TELEFONO1,NOMVENDEDOR,VEND_EMAIL from AUTOS.VT_VLIBRODEVENTAS where " + invent + " and AGEN_NOMAGENCIA='VALLE'";
                                ///else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and CANALVENTA='PUBLICAS' and ORSE_PREFIJO='ASA' and AGEN_NOMAGENCIA='VALLE' and FASE_ORSE_FOLIO in ('" + invent + "')";
                                else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and AGEN_NOMAGENCIA='VALLE' and FASE_ORSE_FOLIO in ('" + invent + "')";
                            }
                            /*else if ((marca == "MITSUBISHI") && (lugaragen == "SANTA LUCIA"))
                            {
                                if (quien == "venta") sql1 = "select INVENTARIO,VEHI_SERIE,FAAU_NOFACTURA,FAAU_FECHA,CTAS_DESCRIPTABLERO,DESCMODELO,VEHI_ANIO_MODELO,COLOR,VEHI_MOTOR,CLIE_CLAVE,(CLIE_NOMBRE|| ' '||CLIE_APELLIDOPATERNO|| ' '||CLIE_APELLIDOMATERNO) as CLIENTE,FAAU_RAZONFACTURA,CLIE_EMAIL,CLIE_TELEFONO1,NOMVENDEDOR,VEND_EMAIL from AUTOS.VT_VLIBRODEVENTAS where " + invent + " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4' and AGEN_NOMAGENCIA='CARONEMOTORS'";
                                ////else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and CANALVENTA='PUBLICAS' and ORSE_PREFIJO='O' and FASE_ORSE_FOLIO in ('" + invent + "') and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4'";
                                else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and FASE_ORSE_FOLIO in ('" + invent + "') and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4'";
                            }
                            else if ((marca == "NISSAN") && (lugaragen == "SAN JERONIMO"))
                            {
                                
                            }*/
                            else
                            {
                                if (quien == "venta") sql1 = "select INVENTARIO,VEHI_SERIE,FAAU_NOFACTURA,FAAU_FECHA,CTAS_DESCRIPTABLERO,DESCMODELO,VEHI_ANIO_MODELO,COLOR,VEHI_MOTOR,CLIE_CLAVE,(CLIE_NOMBRE|| ' '||CLIE_APELLIDOPATERNO|| ' '||CLIE_APELLIDOMATERNO) as CLIENTE,FAAU_RAZONFACTURA,CLIE_EMAIL,CLIE_TELEFONO1,NOMVENDEDOR,VEND_EMAIL from AUTOS.VT_VLIBRODEVENTAS where " + lalista + filtro;
                                ///else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and CANALVENTA='PUBLICAS' and ORSE_PREFIJO='O' and FASE_ORSE_FOLIO in ('" + invent + "')";
                                else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and FASE_ORSE_FOLIO in ('" + invent + "')" + filtro;
                            }

                            OracleCommand cmd = new OracleCommand(sql1, cnx);
                            OracleDataAdapter Da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            Da.Fill(dt);
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            cmd.Dispose();
                            cmd = null;
                            Da.Dispose();
                            Da = null;
                            return true;
                        }
                        catch (Exception ex)
                        {
                            error = "Error: Excepción1 " + ex.ToString();
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            return false;
                        }
                    }
                    else
                    {
                        error = "Error: No Abrió";
                        return false;
                    }
                }

                return false;

            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }


        public bool conexion(String quien, String marca, String lugaragen, String data)
        {
            int ip = 0;
            String cambio, filtro = "";

            try
            {
                if (marca == "NISSAN")
                {
                    segmento = "10";

                    if (lugaragen == "SAN JERONIMO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "SAN JERONIMO LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "LAS AGUILAS")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='3'";
                    }
                    else if (lugaragen == "TLALPAN")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='4'";
                    }
                    else if (lugaragen == "TLALPAN LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='5'";
                    }
                    else if (lugaragen == "LA JOYA")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='6'";
                    }
                    else if (lugaragen == "CROSSING")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='7'";
                    }
                    else if (lugaragen == "LA JOYA LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='8'";
                    }
                    else
                    {
                        return true;
                        /***  no tengo webservice para los datos del cliente en forma de lista solo de uno en uno
                       String cambio2;

                        if ((lugaragen == "MANTE") || (lugaragen == "VALLES"))
                        {   
                            golfoservicio2.ServicioSiaClient cliente2 = new golfoservicio2.ServicioSiaClient();
                            lanissan = cliente2.enc2(quien, marca, lugaragen, data);
                            return true;
                        }
                        else if ((lugaragen == "TUXPAN") || (lugaragen == "POZA RICA"))
                        {
                            if (lugaragen == "POZA RICA") cambio2 = "POZA";
                            else cambio2 = lugaragen;

                            golfoservicio4.ServicioSiaClient cliente4 = new golfoservicio4.ServicioSiaClient();
                            lanissan = cliente4.enc2(quien, marca, cambio2, data);
                            return true;
                        }
                       **********/
                    }
                }
                else if (marca == "JETOUR")
                {
                    segmento = "10";

                    if (lugaragen == "SALTILLO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='20'";
                    }
                    else if (lugaragen == "LAS TORRES")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='21'";
                    }
                    else if (lugaragen == "LAS TORRES LAREDO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='28'";
                    }
                }
                else if (marca == "GEELY")
                {
                    segmento = "10";

                    if (lugaragen == "SENDERO")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "LINDA VISTA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "CONTRY")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='5'";
                    }
                    else if (lugaragen == "CONCORDIA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='7'";
                    }
                }
                else if (marca == "GREAT WALL")
                {
                    segmento = "10";

                    if (lugaragen == "SENDERO")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "SENDERO FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='4'";
                    }
                    else if (lugaragen == "GONZALITOS")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "GONZALITOS FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "OMODA")
                {
                    segmento = "10";

                    if (lugaragen == "LINDA VISTA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "GONZALITOS")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "GONZALITOS LAREDO")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "FORD")
                {
                    if (lugaragen == "VALLE")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "VALLE LAREDO")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (lugaragen == "VASCONCELOS")
                    {
                        ip = 164;
                    }
                }
                else if (marca == "MERCEDES BENZ") return false;
                else if (marca == "MG")
                {
                    if (lugaragen == "LINDA VISTA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='10'";
                    }
                    else if (lugaragen == "LINDA VISTA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='29'";
                    }
                    else if (lugaragen == "CONSTITUYENTES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='31'";
                    }
                    else if (lugaragen == "CONSTITUYENTES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='40'";
                    }
                    else if (lugaragen == "LAS TORRES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='9'";
                    }
                    else if (lugaragen == "LAS TORRES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='33'";
                    }
                    else if (lugaragen == "TEC")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='37'";
                    }
                    else if (lugaragen == "VALLE")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='11'";
                    }
                    else if (lugaragen == "VALLE LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='38'";
                    }
                    else if (lugaragen == "JURIQUILLA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='23'";
                    }
                    else if (lugaragen == "JURIQUILLA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='39'";
                    }
                    else if (lugaragen == "LOMAS VERDES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='14'";
                    }
                    else if (lugaragen == "LOMAS VERDES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='34'";
                    }
                    else if (lugaragen == "TIJUANA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='19'";
                    }
                    else if (lugaragen == "TIJUANA SUCURSAL")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='17'";
                    }
                    else if (lugaragen == "CORREGIDORA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='12'";
                    }
                    else if (lugaragen == "CORREGIDORA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='30'";
                    }
                    else if (lugaragen == "QUERETARO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='13'";
                    }
                    else if (lugaragen == "LOPEZ MATEOS")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='16'";
                    }
                    else if (lugaragen == "LOPEZ MATEOS LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='35'";
                    }
                    else if (lugaragen == "LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='9' and AGEN_IDAGENCIA='15'";
                    }
                    else if (lugaragen == "MEXICALI")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='12' and AGEN_IDAGENCIA='18'";
                    }
                    else if (lugaragen == "LINARES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='26'";
                    }
                    else if (lugaragen == "TOREO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='41'";
                    }
                }
                else if (marca == "CHIREY")
                {
                    if (lugaragen == "LINDA VISTA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "LINDA VISTA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='7'";
                    }
                    else if (lugaragen == "FLETEROS")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "FLETEROS LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='5'";
                    }
                    else if (lugaragen == "CORREGIDORA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='3'";
                    }
                    else if (lugaragen == "CORREGIDORA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='6'";
                    }
                    else if (lugaragen == "JURIQUILLA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='4'";
                    }
                    else if (lugaragen == "JURIQUILLA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='8'";
                    }
                }
                else if (marca == "CHANGAN")
                {
                    segmento = "10";

                    if (lugaragen == "CORREGIDORA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='5'";
                    }
                }
                else if (marca == "KIA")
                {
                    if (lugaragen == "GONZALITOS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "GONZALITOS FRONTERA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='7'";
                    }
                    else if (lugaragen == "FLETEROS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='9'";
                    }
                    else if (lugaragen == "LINDA VISTA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "LINDA VISTA FRONTERA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (lugaragen == "REYNOSA") ip = 173;
                    else if (lugaragen == "LAREDO") ip = 78;
                    else if (lugaragen == "SANTA CATARINA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='5'";
                    }
                    else if (lugaragen == "LA FE")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='11'";
                    }
                }
                else if (marca == "CHRYSLER")
                {
                    if (lugaragen == "CUMBRES")
                    {
                        ip = 75;
                    }
                    else if (lugaragen == "SANTA LUCIA")
                    {
                        segmento = "11";
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (lugaragen == "SANTA LUCIA LAREDO")
                    {
                        segmento = "10";
                        ip = 27;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='32'";
                    }
                    else if (lugaragen == "P.ABASTOS")
                    {
                        ip = 79;
                    }
                    else if (lugaragen == "LINARES")
                    {
                        ip = 79;
                    }
                    else if (lugaragen == "CONTRY")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (lugaragen == "CONTRY LAREDO")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='36'";
                    }
                }
                else if ((marca == "FIAT") && (lugaragen == "CUMBRES")) ip = 75;
                else if (marca == "MITSUBISHI")
                {
                    if (lugaragen == "SANTA LUCIA")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '14' and AGEN_IDAGENCIA = '4' and AGEN_NOMAGENCIA = 'CARONEMOTORS'";
                    }
                    else if (lugaragen == "SALTILLO")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5'";
                        //filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5' and AGEN_NOMAGENCIA = ''";
                    }
                }
                else if ((marca == "ALFA ROMEO") && (lugaragen == "CONTRY")) ip = 98;
                else if ((marca == "CHEVROLET") && (lugaragen == "TLALPAN")) ip = 38;
                else if ((marca == "CHEVROLET") && (lugaragen == "LAS BOMBAS")) ip = 82;
                else if ((marca == "CHEVROLET") && (lugaragen == "RUIZ CORTINES")) { segmento = "10"; ip = 33; }
                else if ((marca == "CHEVROLET") && (lugaragen == "FIDEL VELAZQUEZ")) ip = 159;
                else if ((marca == "CHEVROLET") && (lugaragen == "NOGALAR")) ip = 160;
                else if ((marca == "CHEVROLET") && (lugaragen == "CONCORDIA")) ip = 46;
                else if ((marca == "CHEVROLET") && (lugaragen == "LAS TORRES"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '3'";
                }
                else if ((marca == "CHEVROLET") && (lugaragen == "LAS TORRES LAREDO"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '8'";
                }
                else if ((marca == "CHEVROLET") && (lugaragen == "ALLENDE")) ip = 84;
                else if ((marca == "CHEVROLET") && (lugaragen == "UNIVERSIDAD")) ip = 46;
                else
                {
                    if (show_minempresas(marca, lugaragen))
                    {
                        min = dt2.Rows[0][0].ToString();
                        elodbc = "dsn=" + dt2.Rows[0][1].ToString() + ";";
                        dt2.Dispose();
                        dt2 = null;
                    }
                    else
                    {
                        error = "Error: no conecto minempresas";
                        dt2.Dispose();
                        dt2 = null;
                        return false;
                    }
                    if (!conectar()) return false;
                    try
                    {
                        OdbcCommand sqlCmd = new OdbcCommand(envolvente(quien, data, marca, lugaragen), conn);
                        OdbcDataAdapter sqlDa = new OdbcDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        sqlCmd.Dispose();
                        sqlCmd = null;
                        sqlDa.Dispose();
                        sqlDa = null;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        return false;
                    }
                }

                if (ip > 0)
                {
                    if (conectar2(ip))
                    {
                        try
                        {
                            String sql1 = "";
                            String invent = data.Replace(" ", "% %");

                            if ((marca == "CHEVROLET") && (lugaragen == "LAS TORRES"))
                            {
                                if (quien == "venta") sql1 = "select INVENTARIO,VEHI_SERIE,FAAU_NOFACTURA,FAAU_FECHA,CTAS_DESCRIPTABLERO,DESCMODELO,VEHI_ANIO_MODELO,COLOR,VEHI_MOTOR,CLIE_CLAVE,(CLIE_NOMBRE|| ' '||CLIE_APELLIDOPATERNO|| ' '||CLIE_APELLIDOMATERNO) as CLIENTE,FAAU_RAZONFACTURA,CLIE_EMAIL,CLIE_TELEFONO1,NOMVENDEDOR,VEND_EMAIL from AUTOS.VT_VLIBRODEVENTAS where FAAU_STATUS='AC' and INVENTARIO like '" + invent + "' and AGEN_NOMAGENCIA = 'LAS TORRES'";
                                ////else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,TIOR_DESCRIPCION,ORSE_CONTACTOCLIENTE,FASE_RFC,ORSE_TELEFONO1CLIENTE,ORSE_CONT_EMAIL,ORSE_DESCRIPCIONMODELO,ORSE_ANIOMODELO,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_KILOMETRAJE,NOMBRE,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,FASE_FECHA,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where FASE_ORSE_FOLIO = " + data + " and AGEN_NOMAGENCIA = 'LAS TORRES'";
                                ////else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and CANALVENTA='PUBLICAS' and ORSE_PREFIJO='O' and FASE_ORSE_FOLIO = " + data + " and AGEN_NOMAGENCIA = 'LAS TORRES'";
                                else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and FASE_ORSE_FOLIO = " + data + " and AGEN_NOMAGENCIA = 'LAS TORRES'";
                            }
                            if ((marca == "CHEVROLET") && (lugaragen == "FIDEL VELAZQUEZ"))
                            {
                                if (quien == "venta") sql1 = "select INVENTARIO,VEHI_SERIE,FAAU_NOFACTURA,FAAU_FECHA,CTAS_DESCRIPTABLERO,DESCMODELO,VEHI_ANIO_MODELO,COLOR,VEHI_MOTOR,CLIE_CLAVE,(CLIE_NOMBRE|| ' '||CLIE_APELLIDOPATERNO|| ' '||CLIE_APELLIDOMATERNO) as CLIENTE,FAAU_RAZONFACTURA,CLIE_EMAIL,CLIE_TELEFONO1,NOMVENDEDOR,VEND_EMAIL from AUTOS.VT_VLIBRODEVENTAS where FAAU_STATUS='AC' and EMPR_EMPRESAID=3 and AGEN_IDAGENCIA=6 and INVENTARIO like '" + invent + "'";
                                else sql1 = "";
                            }
                            else if ((marca == "CHEVROLET") && (lugaragen == "ALLENDE"))
                            {
                                if (quien == "venta") sql1 = "select INVENTARIO,VEHI_SERIE,FAAU_NOFACTURA,FAAU_FECHA,CTAS_DESCRIPTABLERO,DESCMODELO,VEHI_ANIO_MODELO,COLOR,VEHI_MOTOR,CLIE_CLAVE,(CLIE_NOMBRE|| ' '||CLIE_APELLIDOPATERNO|| ' '||CLIE_APELLIDOMATERNO) as CLIENTE,FAAU_RAZONFACTURA,CLIE_EMAIL,CLIE_TELEFONO1,NOMVENDEDOR,VEND_EMAIL from AUTOS.VT_VLIBRODEVENTAS where FAAU_STATUS='AC' and INVENTARIO like '" + invent + "' and AGEN_NOMAGENCIA = 'ALLENDE'";
                                /////else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,TIOR_DESCRIPCION,ORSE_CONTACTOCLIENTE,FASE_RFC,ORSE_TELEFONO1CLIENTE,ORSE_CONT_EMAIL,ORSE_DESCRIPCIONMODELO,ORSE_ANIOMODELO,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_KILOMETRAJE,NOMBRE,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,FASE_FECHA,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where FASE_ORSE_FOLIO = " + data + " and AGEN_NOMAGENCIA = 'ALLENDE'";
                                ///else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and CANALVENTA='PUBLICAS' and ORSE_PREFIJO='O' and FASE_ORSE_FOLIO = " + data + " and AGEN_NOMAGENCIA = 'ALLENDE'";
                                else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and FASE_ORSE_FOLIO = " + data + " and AGEN_NOMAGENCIA = 'ALLENDE'";
                            }
                            else if ((marca == "FORD") && (lugaragen == "VASCONCELOS"))
                            {
                                if (quien == "venta") sql1 = "select INVENTARIO,VEHI_SERIE,FAAU_NOFACTURA,FAAU_FECHA,CTAS_DESCRIPTABLERO,DESCMODELO,VEHI_ANIO_MODELO,COLOR,VEHI_MOTOR,CLIE_CLAVE,(CLIE_NOMBRE|| ' '||CLIE_APELLIDOPATERNO|| ' '||CLIE_APELLIDOMATERNO) as CLIENTE,FAAU_RAZONFACTURA,CLIE_EMAIL,CLIE_TELEFONO1,NOMVENDEDOR,VEND_EMAIL from AUTOS.VT_VLIBRODEVENTAS where FAAU_STATUS='AC' and INVENTARIO like '" + invent + "' and AGEN_NOMAGENCIA='VASCONCELOS'";
                                ///else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and CANALVENTA='PUBLICAS' and ORSE_PREFIJO='VAS' and AGEN_NOMAGENCIA='VASCONCELOS' and FASE_ORSE_FOLIO = " + data;
                                else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and AGEN_NOMAGENCIA='VASCONCELOS' and FASE_ORSE_FOLIO = " + data;
                            }
                            else if ((marca == "FORD") && (lugaragen == "VALLE"))
                            {
                                if (quien == "venta") sql1 = "select INVENTARIO,VEHI_SERIE,FAAU_NOFACTURA,FAAU_FECHA,CTAS_DESCRIPTABLERO,DESCMODELO,VEHI_ANIO_MODELO,COLOR,VEHI_MOTOR,CLIE_CLAVE,(CLIE_NOMBRE|| ' '||CLIE_APELLIDOPATERNO|| ' '||CLIE_APELLIDOMATERNO) as CLIENTE,FAAU_RAZONFACTURA,CLIE_EMAIL,CLIE_TELEFONO1,NOMVENDEDOR,VEND_EMAIL from AUTOS.VT_VLIBRODEVENTAS where FAAU_STATUS='AC' and INVENTARIO like '" + invent + "' and AGEN_NOMAGENCIA='VALLE'";
                                //else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and CANALVENTA='PUBLICAS' and ORSE_PREFIJO='ASA' and AGEN_NOMAGENCIA='VALLE' and FASE_ORSE_FOLIO = " + data;
                                else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and AGEN_NOMAGENCIA='VALLE' and FASE_ORSE_FOLIO = " + data;
                            }
                            else if (marca == "CHRYSLER")
                            {
                                if (quien == "venta") sql1 = "select INVENTARIO,VEHI_SERIE,FAAU_NOFACTURA,FAAU_FECHA,CTAS_DESCRIPTABLERO,DESCMODELO,VEHI_ANIO_MODELO,COLOR,VEHI_MOTOR,CLIE_CLAVE,(CLIE_NOMBRE|| ' '||CLIE_APELLIDOPATERNO|| ' '||CLIE_APELLIDOMATERNO) as CLIENTE,FAAU_RAZONFACTURA,CLIE_EMAIL,CLIE_TELEFONO1,NOMVENDEDOR,VEND_EMAIL from AUTOS.VT_VLIBRODEVENTAS where INVENTARIO like '" + invent + "'" + filtro;
                                else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and FASE_ORSE_FOLIO = " + data + filtro;
                            }
                            /*else if ((marca == "MITSUBISHI") && (lugaragen == "SANTA LUCIA"))
                            {
                                if (quien == "venta") sql1 = "select INVENTARIO,VEHI_SERIE,FAAU_NOFACTURA,FAAU_FECHA,CTAS_DESCRIPTABLERO,DESCMODELO,VEHI_ANIO_MODELO,COLOR,VEHI_MOTOR,CLIE_CLAVE,(CLIE_NOMBRE|| ' '||CLIE_APELLIDOPATERNO|| ' '||CLIE_APELLIDOMATERNO) as CLIENTE,FAAU_RAZONFACTURA,CLIE_EMAIL,CLIE_TELEFONO1,NOMVENDEDOR,VEND_EMAIL from AUTOS.VT_VLIBRODEVENTAS where FAAU_STATUS='AC' and INVENTARIO like '" + invent + "' and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4'";
                                ///else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and CANALVENTA='PUBLICAS' and ORSE_PREFIJO='O' and FASE_ORSE_FOLIO = " + data + " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4'";
                                else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and FASE_ORSE_FOLIO = " + data + " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4'";
                            }*/
                            else
                            {
                                if (quien == "venta") sql1 = "select INVENTARIO,VEHI_SERIE,FAAU_NOFACTURA,FAAU_FECHA,CTAS_DESCRIPTABLERO,DESCMODELO,VEHI_ANIO_MODELO,COLOR,VEHI_MOTOR,CLIE_CLAVE,(CLIE_NOMBRE|| ' '||CLIE_APELLIDOPATERNO|| ' '||CLIE_APELLIDOMATERNO) as CLIENTE,FAAU_RAZONFACTURA,CLIE_EMAIL,CLIE_TELEFONO1,NOMVENDEDOR,VEND_EMAIL from AUTOS.VT_VLIBRODEVENTAS where FAAU_STATUS='AC' and INVENTARIO like '" + invent + "'" + filtro;
                                /////else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,TIOR_DESCRIPCION,ORSE_CONTACTOCLIENTE,FASE_RFC,ORSE_TELEFONO1CLIENTE,ORSE_CONT_EMAIL,ORSE_DESCRIPCIONMODELO,ORSE_ANIOMODELO,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_KILOMETRAJE,NOMBRE,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,FASE_FECHA,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where FASE_ORSE_FOLIO = " + data;
                                ////else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and CANALVENTA='PUBLICAS' and ORSE_PREFIJO='O' and FASE_ORSE_FOLIO = " + data;
                                else if (quien == "hyp") sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='HOJALATERIA Y PINTURA' and FASE_ORSE_FOLIO = " + data + filtro;
                                else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and FASE_ORSE_FOLIO = " + data + filtro;
                            }

                            OracleCommand cmd = new OracleCommand(sql1, cnx);
                            OracleDataAdapter Da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            Da.Fill(dt);
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            cmd.Dispose();
                            cmd = null;
                            Da.Dispose();
                            Da = null;
                            return true;
                        }
                        catch (Exception ex)
                        {
                            error = "Error: Excepción1 " + ex.ToString();
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            return false;
                        }
                    }
                    else
                    {
                        error = "Error: No Abrió";
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        public bool conexion3(String quien, String marca, String agencia, String fec_ini, String fec_fin)
        {
            int ip = 0;
            string filtro = "";
            try
            {
                if (marca == "MERCEDES BENZ") return false;

                if (marca == "NISSAN")
                {
                    segmento = "10";

                    if (agencia == "SAN JERONIMO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "SAN JERONIMO LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "LAS AGUILAS")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "TLALPAN")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "TLALPAN LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "LA JOYA")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "CROSSING")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "LA JOYA LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='8'";
                    }
                    else
                    {
                        return true;
                        /***  no tengo webservice para los datos del cliente en forma de lista solo de uno en uno
                       String cambio2;

                        if ((agencia == "MANTE") || (agencia == "VALLES"))
                        {   
                            golfoservicio2.ServicioSiaClient cliente2 = new golfoservicio2.ServicioSiaClient();
                            lanissan = cliente2.enc2(quien, marca, agencia, data);
                            return true;
                        }
                        else if ((agencia == "TUXPAN") || (agencia == "POZA RICA"))
                        {
                            if (agencia == "POZA RICA") cambio2 = "POZA";
                            else cambio2 = agencia;

                            golfoservicio4.ServicioSiaClient cliente4 = new golfoservicio4.ServicioSiaClient();
                            lanissan = cliente4.enc2(quien, marca, cambio2, data);
                            return true;
                        }
                       **********/
                    }
                }
                else if (marca == "JETOUR")
                {
                    segmento = "10";

                    if (agencia == "SALTILLO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='20'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='21'";
                    }
                    else if (agencia == "LAS TORRES LAREDO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='28'";
                    }
                }
                else if (marca == "GEELY")
                {
                    segmento = "10";

                    if (agencia == "SENDERO")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "CONTRY")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "CONCORDIA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='7'";
                    }
                }
                else if (marca == "GREAT WALL")
                {
                    segmento = "10";

                    if (agencia == "SENDERO")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "SENDERO FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "GONZALITOS")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "GONZALITOS FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "OMODA")
                {
                    segmento = "10";

                    if (agencia == "LINDA VISTA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "GONZALITOS")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "GONZALITOS LAREDO")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "MG")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='10'";
                    }
                    else if (agencia == "LINDA VISTA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='29'";
                    }
                    else if (agencia == "CONSTITUYENTES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='31'";
                    }
                    else if (agencia == "CONSTITUYENTES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='40'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "LAS TORRES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='33'";
                    }
                    else if (agencia == "TEC")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='37'";
                    }
                    else if (agencia == "VALLE")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='11'";
                    }
                    else if (agencia == "VALLE LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='38'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='23'";
                    }
                    else if (agencia == "JURIQUILLA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='39'";
                    }
                    else if (agencia == "LOMAS VERDES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='14'";
                    }
                    else if (agencia == "LOMAS VERDES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='34'";
                    }
                    else if (agencia == "TIJUANA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='19'";
                    }
                    else if (agencia == "TIJUANA SUCURSAL")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='17'";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='12'";
                    }
                    else if (agencia == "CORREGIDORA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='30'";
                    }
                    else if (agencia == "QUERETARO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='13'";
                    }
                    else if (agencia == "LOPEZ MATEOS")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='16'";
                    }
                    else if (agencia == "LOPEZ MATEOS LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='35'";
                    }
                    else if (agencia == "LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='9' and AGEN_IDAGENCIA='15'";
                    }
                    else if (agencia == "MEXICALI")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='12' and AGEN_IDAGENCIA='18'";
                    }
                    else if (agencia == "LINARES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='26'";
                    }
                    else if (agencia == "TOREO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='41'";
                    }
                }
                else if (marca == "CHIREY")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "FLETEROS")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "FLETEROS LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "CORREGIDORA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "JURIQUILLA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='8'";
                    }
                }
                else if (marca == "CHANGAN")
                {
                    segmento = "10";

                    if (agencia == "CORREGIDORA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='5'";
                    }
                }
                else if (marca == "KIA")
                {
                    if (agencia == "GONZALITOS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "GONZALITOS FRONTERA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "FLETEROS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA FRONTERA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "REYNOSA") ip = 173;
                    else if (agencia == "LAREDO") ip = 78;
                    else if (agencia == "SANTA CATARINA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "LA FE")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='11'";
                    }
                }
                else if (marca == "CHRYSLER")
                {
                    if (agencia == "CUMBRES")
                    {
                        ip = 75;
                    }
                    else if (agencia == "SANTA LUCIA")
                    {
                        segmento = "11";
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "SANTA LUCIA LAREDO")
                    {
                        segmento = "10";
                        ip = 27;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='32'";
                    }
                    else if (agencia == "P.ABASTOS")
                    {
                        ip = 79;
                    }
                    else if (agencia == "LINARES")
                    {
                        ip = 79;
                    }
                    else if (agencia == "CONTRY")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "CONTRY LAREDO")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='36'";
                    }
                }
                else if (marca == "FORD")
                {
                    if (agencia == "VALLE")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "VALLE LAREDO")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "VASCONCELOS")
                    {
                        ip = 164;
                    }
                }
                else if ((marca == "FIAT") && (agencia == "CUMBRES")) ip = 75;
                else if (marca == "MITSUBISHI")
                {
                    if (agencia == "SANTA LUCIA")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '14' and AGEN_IDAGENCIA = '4' and AGEN_NOMAGENCIA = 'CARONEMOTORS'";
                    }
                    else if (agencia == "SALTILLO")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5'";
                        //filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5' and AGEN_NOMAGENCIA = ''";
                    }
                }
                else if ((marca == "ALFA ROMEO") && (agencia == "CONTRY")) ip = 98;
                else if ((marca == "CHEVROLET") && (agencia == "TLALPAN")) ip = 38;
                else if ((marca == "CHEVROLET") && (agencia == "LAS BOMBAS")) ip = 82;
                else if ((marca == "CHEVROLET") && (agencia == "RUIZ CORTINES")) ip = 159;
                else if ((marca == "CHEVROLET") && (agencia == "NOGALAR")) ip = 160;
                else if ((marca == "CHEVROLET") && (agencia == "CONCORDIA")) ip = 46;
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '3'";
                }
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES LAREDO"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '8'";
                }
                else if ((marca == "CHEVROLET") && (agencia == "ALLENDE")) ip = 84;
                else if ((marca == "CHEVROLET") && (agencia == "UNIVERSIDAD")) ip = 46;
                else
                {
                    ///// esto es por que no es global el sistema y no tengo el correo del vendedor deje en comentario el código
                    error = "no es sistema global";
                    return false;
                    ///////////////////////////////////////////

                    /*if (show_minempresas(marca, agencia))
                    {
                        min = dt2.Rows[0][0].ToString();
                        elodbc = "dsn=" + dt2.Rows[0][1].ToString() + ";";
                        dt2.Dispose();
                        dt2 = null;
                    }
                    else
                    {
                        error = "Error: no conecto minempresas";
                        dt2.Dispose();
                        dt2 = null;
                        return false;
                    }
                    if (!conectar()) return false;
                    try
                    {
                        OdbcCommand sqlCmd = new OdbcCommand(envolvente3(quien, Email_vendedor, marca, agencia), conn);
                        OdbcDataAdapter sqlDa = new OdbcDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        sqlCmd.Dispose();
                        sqlCmd = null;
                        sqlDa.Dispose();
                        sqlDa = null;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        return false;
                    }*/
                }

                if (ip > 0)
                {
                    if (conectar2(ip))
                    {
                        try
                        {
                            String sql1 = "";
                            ///String invent = data.Replace(" ", "% %");

                            if (marca == "KIA")
                            {
                                if (agencia == "GONZALITOS")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "LINDA VISTA")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "REYNOSA")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "SANTA CATARINA")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                            }
                            if (marca == "MG")
                            {
                                if (agencia == "LINDA VISTA")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "LAS TORRES")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "VALLE")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                            }
                            else if (marca == "CHRYSLER")
                            {
                                if (agencia == "CUMBRES")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR,VEND_EMAIL,sum(ventas) ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR,sum(ventas) ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR,VEND_EMAIL,sum(ventas) ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES')AND MARC_DESCRIP <>'FIAT'and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES')AND MARC_DESCRIP <>'FIAT'and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR,sum(ventas) ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES')AND MARC_DESCRIP <>'FIAT'and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES')AND MARC_DESCRIP <>'FIAT'and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "CONTRY")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR,VEND_EMAIL,sum(ventas) ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES')AND MARC_DESCRIP <>'FIAT'and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES')AND MARC_DESCRIP <>'FIAT'and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR,sum(ventas) ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND MARC_DESCRIP <> 'FIAT'and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND MARC_DESCRIP <> 'FIAT'and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "P.ABASTOS")
                                {
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR,sum(ventas) ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES')AND MARC_DESCRIP <>'FIAT'and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES')AND MARC_DESCRIP <>'FIAT'and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "LINARES")
                                {
                                    if (quien == "venta") sql1 = "";
                                    //else sql1 = "";
                                }
                            }
                            else if (marca == "FORD")
                            {
                                if (agencia == "VALLE")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VALLE' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VALLE' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VALLE' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VALLE' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "VASCONCELOS")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VASCONCELOS' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VASCONCELOS' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VASCONCELOS' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VASCONCELOS' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                            }
                            else if (marca == "FIAT")
                            {
                                if (agencia == "CUMBRES")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR,VEND_EMAIL,sum(ventas) ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='FNU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='FNU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR,sum(ventas) ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='FNU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='FNU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR,VEND_EMAIL,sum(ventas) ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA')AND MARC_DESCRIP ='FIAT'and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA')AND MARC_DESCRIP ='FIAT'and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR,sum(ventas) ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA')AND MARC_DESCRIP ='FIAT'and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA')AND MARC_DESCRIP ='FIAT'and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                            }
                            else if ((marca == "MITSUBISHI") && (agencia == "SANTA LUCIA"))
                            {
                                //if (quien == "venta") sql1 = "select NOMVENDEDOR,VEND_EMAIL,sum(ventas) ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('MITSU')and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('MITSU') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                if (quien == "venta") sql1 = "select NOMVENDEDOR,sum(ventas) ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4' and AGEN_NOMAGENCIA='CARONEMOTORS' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4' and AGEN_NOMAGENCIA='CARONEMOTORS' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                //else sql1 = "";
                            }
                            else if ((marca == "MITSUBISHI") && (agencia == "SALTILLO"))
                            {
                                //if (quien == "venta") sql1 = "select NOMVENDEDOR,VEND_EMAIL,sum(ventas) ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('MITSU')and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('MITSU') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                if (quien == "venta") sql1 = "select NOMVENDEDOR,sum(ventas) ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                //else sql1 = "";
                            }
                            else if (marca == "CHEVROLET")
                            {
                                if (agencia == "TLALPAN")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "LAS BOMBAS")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "RUIZ CORTINES")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "NOGALAR")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='NOGALAR' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='NOGALAR' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='NOGALAR' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='NOGALAR' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "CONCORDIA")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='CONCORDIA' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='CONCORDIA' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='CONCORDIA' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='CONCORDIA' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "LAS TORRES")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='LAS TORRES' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='LAS TORRES' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='LAS TORRES' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='LAS TORRES' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "ALLENDE")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='ALLENDE' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='ALLENDE' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='ALLENDE' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='ALLENDE' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                                else if (agencia == "UNIVERSIDAD")
                                {
                                    //if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,VEND_EMAIL Email,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,VEND_EMAIL,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,VEND_EMAIL,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by VEND_EMAIL,NOMVENDEDOR";
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                    //else sql1 = "";
                                }
                            }
                            else if (marca == "NISSAN")
                            {
                                if (agencia == "SAN JERONIMO")
                                {
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                }
                                if (agencia == "LAS AGUILAS")
                                {
                                    if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                }
                            }
                            else
                            {
                                if (quien == "venta") sql1 = "select NOMVENDEDOR Vendedor,sum(ventas) Ventas,sum(ventas*(AUTO_VENTA-AUTO_COSTO)) UB from (select NOMVENDEDOR,(1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select NOMVENDEDOR,(-1) ventas,(COSTOOPERACION + FAAU_MONTOCORTESIAQUEST + FAAU_OTROSCOSTOS + PEAU_PARTICIPACION - BONIFICACION ) AS AUTO_cOSTO,(COALESCE(FAAU_INCENTIVODEALER,0)+ COALESCE(FAAU_INCETIVOPENETRA,0)+ COALESCE(FAAU_NOTACARGOADIC,0)+ FAAU_TOTAL+FAU_MONTONOTACARGO - FAAU_ISAN- FAAU_IVA- FAU_MONTONOTACREDITO+ VEHI_OTROSCARCONT- VEHI_OTROSABOCONT) AS AUTO_VENTA from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by NOMVENDEDOR";
                                //else if (quien == "hyp") sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='HOJALATERIA Y PINTURA' and FASE_ORSE_FOLIO = " + data + filtro;
                                //else sql1 = "select FASE_ORSE_FOLIO,TIPO_SERVICIO,ORSE_CONTACTOCLIENTE,FASE_RAZONSOCIAL,ORSE_CONT_EMAIL,ORSE_TELEFONO1CLIENTE,ORSE_TELEFONO2CLIENTE,ORSE_NUMEROSERIE,ORSE_MOTOR,ORSE_PLACAS,ORSE_COLOR1,ORSE_COLOR2,ORSE_MARCA,ORSE_CLAVEMODELO,ORSE_ANIOMODELO,ORSE_KILOMETRAJE,NOMBRE,FASE_FECHA,ORSE_FECHAENTRADA,ORSE_FECHACIERRE,ORSE_COMENTARIOS from AUTOS.CONE_VVTAS_SERVICIO where TIOR_SUBDEPTO='SERVICIO' and FASE_ORSE_FOLIO = " + data + filtro;
                            }

                            OracleCommand cmd = new OracleCommand(sql1, cnx);
                            OracleDataAdapter Da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            Da.Fill(dt);
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            cmd.Dispose();
                            cmd = null;
                            Da.Dispose();
                            Da = null;
                            return true;
                        }
                        catch (Exception ex)
                        {
                            error = "Error: Excepción1 " + ex.ToString();
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            return false;
                        }
                    }
                    else
                    {
                        error = "Error: No Abrió";
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        public bool conexion4(String quien, String marca, String agencia, String fec_ini, String fec_fin)
        {
            int ip = 0;
            string filtro = "";
            try
            {
                if (marca == "MERCEDES BENZ") return false;

                if (marca == "NISSAN")
                {
                    segmento = "10";

                    if (agencia == "SAN JERONIMO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "SAN JERONIMO LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "LAS AGUILAS")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "TLALPAN")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "TLALPAN LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "LA JOYA")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "CROSSING")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "LA JOYA LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='8'";
                    }
                    else
                    {
                        return true;
                        /***  no tengo webservice para los datos del cliente en forma de lista solo de uno en uno
                       String cambio2;

                        if ((agencia == "MANTE") || (agencia == "VALLES"))
                        {   
                            golfoservicio2.ServicioSiaClient cliente2 = new golfoservicio2.ServicioSiaClient();
                            lanissan = cliente2.enc2(quien, marca, agencia, data);
                            return true;
                        }
                        else if ((agencia == "TUXPAN") || (agencia == "POZA RICA"))
                        {
                            if (agencia == "POZA RICA") cambio2 = "POZA";
                            else cambio2 = agencia;

                            golfoservicio4.ServicioSiaClient cliente4 = new golfoservicio4.ServicioSiaClient();
                            lanissan = cliente4.enc2(quien, marca, cambio2, data);
                            return true;
                        }
                       **********/
                    }
                }
                else if (marca == "JETOUR")
                {
                    segmento = "10";

                    if (agencia == "SALTILLO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='20'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='21'";
                    }
                    else if (agencia == "LAS TORRES LAREDO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='28'";
                    }
                }
                else if (marca == "GEELY")
                {
                    segmento = "10";

                    if (agencia == "SENDERO")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "CONTRY")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "CONCORDIA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='7'";
                    }
                }
                else if (marca == "GREAT WALL")
                {
                    segmento = "10";

                    if (agencia == "SENDERO")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "SENDERO FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "GONZALITOS")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "GONZALITOS FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "OMODA")
                {
                    segmento = "10";

                    if (agencia == "LINDA VISTA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "GONZALITOS")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "GONZALITOS LAREDO")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "KIA")
                {
                    if (agencia == "GONZALITOS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "GONZALITOS FRONTERA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "FLETEROS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA FRONTERA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "REYNOSA") ip = 173;
                    else if (agencia == "LAREDO") ip = 78;
                    else if (agencia == "SANTA CATARINA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "LA FE")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='11'";
                    }
                }
                else if (marca == "MG")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='10'";
                    }
                    else if (agencia == "LINDA VISTA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='29'";
                    }
                    else if (agencia == "CONSTITUYENTES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='31'";
                    }
                    else if (agencia == "CONSTITUYENTES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='40'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "LAS TORRES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='33'";
                    }
                    else if (agencia == "TEC")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='37'";
                    }
                    else if (agencia == "VALLE")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='11'";
                    }
                    else if (agencia == "VALLE LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='38'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='23'";
                    }
                    else if (agencia == "JURIQUILLA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='39'";
                    }
                    else if (agencia == "LOMAS VERDES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='14'";
                    }
                    else if (agencia == "LOMAS VERDES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='34'";
                    }
                    else if (agencia == "TIJUANA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='19'";
                    }
                    else if (agencia == "TIJUANA SUCURSAL")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='17'";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='12'";
                    }
                    else if (agencia == "CORREGIDORA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='30'";
                    }
                    else if (agencia == "QUERETARO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='13'";
                    }
                    else if (agencia == "LOPEZ MATEOS")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='16'";
                    }
                    else if (agencia == "LOPEZ MATEOS LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='35'";
                    }
                    else if (agencia == "LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='9' and AGEN_IDAGENCIA='15'";
                    }
                    else if (agencia == "MEXICALI")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='12' and AGEN_IDAGENCIA='18'";
                    }
                    else if (agencia == "LINARES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='26'";
                    }
                    else if (agencia == "TOREO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='41'";
                    }
                }
                else if (marca == "CHIREY")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "FLETEROS")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "FLETEROS LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "CORREGIDORA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "JURIQUILLA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='8'";
                    }
                }
                else if (marca == "CHANGAN")
                {
                    segmento = "10";

                    if (agencia == "CORREGIDORA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='5'";
                    }
                }
                else if (marca == "CHRYSLER")
                {
                    if (agencia == "CUMBRES")
                    {
                        ip = 75;
                    }
                    else if (agencia == "SANTA LUCIA")
                    {
                        segmento = "11";
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "SANTA LUCIA LAREDO")
                    {
                        segmento = "10";
                        ip = 27;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='32'";
                    }
                    else if (agencia == "P.ABASTOS")
                    {
                        ip = 79;
                    }
                    else if (agencia == "LINARES")
                    {
                        ip = 79;
                    }
                    else if (agencia == "CONTRY")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "CONTRY LAREDO")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='36'";
                    }
                }
                else if (marca == "FORD")
                {
                    if (agencia == "VALLE")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "VALLE LAREDO")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "VASCONCELOS")
                    {
                        ip = 164;
                    }
                }
                else if ((marca == "FIAT") && (agencia == "CUMBRES")) ip = 75;
                else if (marca == "MITSUBISHI")
                {
                    if (agencia == "SANTA LUCIA")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '14' and AGEN_IDAGENCIA = '4' and AGEN_NOMAGENCIA = 'CARONEMOTORS'";
                    }
                    else if (agencia == "SALTILLO")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5'";
                        //filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5' and AGEN_NOMAGENCIA = ''";
                    }
                }
                else if ((marca == "ALFA ROMEO") && (agencia == "CONTRY")) ip = 98;
                else if ((marca == "CHEVROLET") && (agencia == "TLALPAN")) ip = 38;
                else if ((marca == "CHEVROLET") && (agencia == "LAS BOMBAS")) ip = 82;
                else if ((marca == "CHEVROLET") && (agencia == "RUIZ CORTINES")) ip = 159;
                else if ((marca == "CHEVROLET") && (agencia == "NOGALAR")) ip = 160;
                else if ((marca == "CHEVROLET") && (agencia == "CONCORDIA")) ip = 46;
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '3'";
                }
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES LAREDO"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '8'";
                }
                else if ((marca == "CHEVROLET") && (agencia == "ALLENDE")) ip = 84;
                else if ((marca == "CHEVROLET") && (agencia == "UNIVERSIDAD")) ip = 46;
                else
                {
                    ///// esto es por que no es global el sistema y no tengo el correo del vendedor deje en comentario el código
                    error = "no es sistema global";
                    return false;
                    ///////////////////////////////////////////

                    /*if (show_minempresas(marca, agencia))
                    {
                        min = dt2.Rows[0][0].ToString();
                        elodbc = "dsn=" + dt2.Rows[0][1].ToString() + ";";
                        dt2.Dispose();
                        dt2 = null;
                    }
                    else
                    {
                        error = "Error: no conecto minempresas";
                        dt2.Dispose();
                        dt2 = null;
                        return false;
                    }
                    if (!conectar()) return false;
                    try
                    {
                        OdbcCommand sqlCmd = new OdbcCommand(envolvente3(quien, Email_vendedor, marca, agencia), conn);
                        OdbcDataAdapter sqlDa = new OdbcDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        sqlCmd.Dispose();
                        sqlCmd = null;
                        sqlDa.Dispose();
                        sqlDa = null;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        return false;
                    }*/
                }

                if (ip > 0)
                {
                    if (conectar2(ip))
                    {
                        try
                        {
                            String sql1 = "";
                            ////String invent = data.Replace(" ", "% %");

                            if (marca == "KIA")
                            {
                                if (agencia == "GONZALITOS" || agencia == "SANTA CATARINA")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIOR_ARTURS_TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIOR_ARTURS_TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";

                                }
                                else if (agencia == "LINDA VISTA")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                                else if (agencia == "REYNOSA")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                            }
                            else if (marca == "MG")
                            {
                                if (agencia == "LINDA VISTA")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                                else if (agencia == "LAS TORRES")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                            }
                            else if (marca == "CHRYSLER")
                            {
                                if (agencia == "CUMBRES")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR = 'SEMINUEVOS' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR ='SEMINUEVOS' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES')AND MARC_DESCRIP <>'FIAT'and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES')AND MARC_DESCRIP <>'FIAT'and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR = 'SEMINUEVOS' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR ='SEMINUEVOS' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                }
                                else if (agencia == "CONTRY")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND MARC_DESCRIP <> 'FIAT'and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND MARC_DESCRIP <> 'FIAT'and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR = 'SEMINUEVOS' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR ='SEMINUEVOS' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                }
                                else if (agencia == "P.ABASTOS")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES')AND MARC_DESCRIP <>'FIAT'and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES')AND MARC_DESCRIP <>'FIAT'and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR = 'SEMINUEVOS' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR ='SEMINUEVOS' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                }
                                else if (agencia == "LINARES")
                                {
                                    if (quien == "venta") sql1 = "";
                                    else if (quien == "seminuevos") sql1 = "";
                                }
                            }
                            else if (marca == "FORD")
                            {
                                if (agencia == "VALLE")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VALLE' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VALLE' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='VALLE' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='VALLE' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=1 and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=1 and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=1 and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=1 and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=1 and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=1 and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                                else if (agencia == "VASCONCELOS")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VASCONCELOS' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VASCONCELOS' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='VASCONCELOS' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='VASCONCELOS' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=2 and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=2 and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=2 and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=2 and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=2 and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=2 and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                            }
                            else if (marca == "FIAT")
                            {
                                if (agencia == "CUMBRES")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='FNU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='FNU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "";
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA')AND MARC_DESCRIP ='FIAT'and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA')AND MARC_DESCRIP ='FIAT'and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "";
                                }
                            }
                            else if ((marca == "MITSUBISHI") && (agencia == "SANTA LUCIA"))
                            {
                                if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4' and AGEN_NOMAGENCIA='CARONEMOTORS' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4' and AGEN_NOMAGENCIA='CARONEMOTORS' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                else if (quien == "seminuevos") sql1 = "";
                            }
                            else if ((marca == "MITSUBISHI") && (agencia == "SALTILLO"))
                            {
                                if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                else if (quien == "seminuevos") sql1 = "";
                            }
                            else if (marca == "CHEVROLET")
                            {
                                if (agencia == "TLALPAN")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                    ////ordenes en proceso select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between 20170401 and 20170431) group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between 20170401 and 20170431) group by agrupar,fecha) group by fecha order by TO_DATE(fecha)
                                }
                                else if (agencia == "LAS BOMBAS")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "";
                                }
                                else if (agencia == "RUIZ CORTINES")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                                else if (agencia == "NOGALAR")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='NOGALAR' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='NOGALAR' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='NOGALAR' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='NOGALAR' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                                else if (agencia == "CONCORDIA")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='CONCORDIA' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='CONCORDIA' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "";
                                }
                                else if (agencia == "LAS TORRES")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='LAS TORRES' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='LAS TORRES' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='LAS TORRES' and FORM_GRUPODEVENTADIR = 'SEMINUEVOS' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='LAS TORRES' and FORM_GRUPODEVENTADIR = 'SEMINUEVOS' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=3 and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=3 and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=3 and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=3 and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=3 and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=3 and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                                else if (agencia == "ALLENDE")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='ALLENDE' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='ALLENDE' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='ALLENDE' and FORM_GRUPODEVENTADIR = 'SEMINUEVOS' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='ALLENDE' and FORM_GRUPODEVENTADIR = 'SEMINUEVOS' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=4 and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=4 and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=4 and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=4 and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=4 and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=4 and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                                else if (agencia == "UNIVERSIDAD")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                            }
                            else if (marca == "NISSAN")
                            {
                                if (agencia == "SAN JERONIMO")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                }
                                if (agencia == "LAS AGUILAS")
                                {
                                    if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                }
                            }
                            else
                            {
                                if (quien == "venta") sql1 = "select fecha,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by fecha";
                                else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                            }

                            OracleCommand cmd = new OracleCommand(sql1, cnx);
                            OracleDataAdapter Da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            Da.Fill(dt);
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            cmd.Dispose();
                            cmd = null;
                            Da.Dispose();
                            Da = null;
                            return true;
                        }
                        catch (Exception ex)
                        {
                            error = "Error: Excepción1 " + ex.ToString();
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            return false;
                        }
                    }
                    else
                    {
                        error = "Error: No Abrió";
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        public bool conexion6(String quien, String marca, String agencia, String fec_ini, String fec_fin)
        {
            int ip = 0;
            string filtro = "";
            try
            {
                if (marca == "MERCEDES BENZ") return false;

                if (marca == "NISSAN")
                {
                    segmento = "10";

                    if (agencia == "SAN JERONIMO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "SAN JERONIMO LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "LAS AGUILAS")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "TLALPAN")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "TLALPAN LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "LA JOYA")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "CROSSING")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "LA JOYA LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='8'";
                    }
                    else
                    {
                        return true;
                        /***  no tengo webservice para los datos del cliente en forma de lista solo de uno en uno
                       String cambio2;

                        if ((agencia == "MANTE") || (agencia == "VALLES"))
                        {   
                            golfoservicio2.ServicioSiaClient cliente2 = new golfoservicio2.ServicioSiaClient();
                            lanissan = cliente2.enc2(quien, marca, agencia, data);
                            return true;
                        }
                        else if ((agencia == "TUXPAN") || (agencia == "POZA RICA"))
                        {
                            if (agencia == "POZA RICA") cambio2 = "POZA";
                            else cambio2 = agencia;

                            golfoservicio4.ServicioSiaClient cliente4 = new golfoservicio4.ServicioSiaClient();
                            lanissan = cliente4.enc2(quien, marca, cambio2, data);
                            return true;
                        }
                       **********/
                    }
                }
                else if (marca == "JETOUR")
                {
                    segmento = "10";

                    if (agencia == "SALTILLO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='20'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='21'";
                    }
                    else if (agencia == "LAS TORRES LAREDO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='28'";
                    }
                }
                else if (marca == "GEELY")
                {
                    segmento = "10";

                    if (agencia == "SENDERO")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "CONTRY")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "CONCORDIA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='7'";
                    }
                }
                else if (marca == "GREAT WALL")
                {
                    segmento = "10";

                    if (agencia == "SENDERO")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "SENDERO FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "GONZALITOS")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "GONZALITOS FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "OMODA")
                {
                    segmento = "10";

                    if (agencia == "LINDA VISTA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "GONZALITOS")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "GONZALITOS LAREDO")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "KIA")
                {
                    if (agencia == "GONZALITOS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "GONZALITOS FRONTERA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "FLETEROS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA FRONTERA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "REYNOSA") ip = 173;
                    else if (agencia == "LAREDO") ip = 78;
                    else if (agencia == "SANTA CATARINA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "LA FE")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='11'";
                    }
                }
                else if (marca == "MG")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='10'";
                    }
                    else if (agencia == "LINDA VISTA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='29'";
                    }
                    else if (agencia == "CONSTITUYENTES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='31'";
                    }
                    else if (agencia == "CONSTITUYENTES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='40'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "LAS TORRES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='33'";
                    }
                    else if (agencia == "TEC")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='37'";
                    }
                    else if (agencia == "VALLE")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='11'";
                    }
                    else if (agencia == "VALLE LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='38'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='23'";
                    }
                    else if (agencia == "JURIQUILLA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='39'";
                    }
                    else if (agencia == "LOMAS VERDES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='14'";
                    }
                    else if (agencia == "LOMAS VERDES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='34'";
                    }
                    else if (agencia == "TIJUANA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='19'";
                    }
                    else if (agencia == "TIJUANA SUCURSAL")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='17'";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='12'";
                    }
                    else if (agencia == "CORREGIDORA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='30'";
                    }
                    else if (agencia == "QUERETARO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='13'";
                    }
                    else if (agencia == "LOPEZ MATEOS")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='16'";
                    }
                    else if (agencia == "LOPEZ MATEOS LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='35'";
                    }
                    else if (agencia == "LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='9' and AGEN_IDAGENCIA='15'";
                    }
                    else if (agencia == "MEXICALI")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='12' and AGEN_IDAGENCIA='18'";
                    }
                    else if (agencia == "LINARES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='26'";
                    }
                    else if (agencia == "TOREO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='41'";
                    }
                }
                else if (marca == "CHIREY")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "FLETEROS")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "FLETEROS LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "CORREGIDORA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "JURIQUILLA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='8'";
                    }
                }
                else if (marca == "CHANGAN")
                {
                    segmento = "10";

                    if (agencia == "CORREGIDORA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='5'";
                    }
                }
                else if (marca == "CHRYSLER")
                {
                    if (agencia == "CUMBRES")
                    {
                        ip = 75;
                    }
                    else if (agencia == "SANTA LUCIA")
                    {
                        segmento = "11";
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "SANTA LUCIA LAREDO")
                    {
                        segmento = "10";
                        ip = 27;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='32'";
                    }
                    else if (agencia == "P.ABASTOS")
                    {
                        ip = 79;
                    }
                    else if (agencia == "LINARES")
                    {
                        ip = 79;
                    }
                    else if (agencia == "CONTRY")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "CONTRY LAREDO")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='36'";
                    }
                }
                else if (marca == "FORD")
                {
                    if (agencia == "VALLE")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "VALLE LAREDO")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "VASCONCELOS")
                    {
                        ip = 164;
                    }
                }
                else if ((marca == "FIAT") && (agencia == "CUMBRES")) ip = 75;
                else if (marca == "MITSUBISHI")
                {
                    if (agencia == "SANTA LUCIA")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '14' and AGEN_IDAGENCIA = '4' and AGEN_NOMAGENCIA = 'CARONEMOTORS'";
                    }
                    else if (agencia == "SALTILLO")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5'";
                        //filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5' and AGEN_NOMAGENCIA = ''";
                    }
                }
                else if ((marca == "ALFA ROMEO") && (agencia == "CONTRY")) ip = 98;
                else if ((marca == "CHEVROLET") && (agencia == "TLALPAN")) ip = 38;
                else if ((marca == "CHEVROLET") && (agencia == "LAS BOMBAS")) ip = 82;
                else if ((marca == "CHEVROLET") && (agencia == "RUIZ CORTINES")) ip = 159;
                else if ((marca == "CHEVROLET") && (agencia == "NOGALAR")) ip = 160;
                else if ((marca == "CHEVROLET") && (agencia == "CONCORDIA")) ip = 46;
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '3'";
                }
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES LAREDO"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '8'";
                }
                else if ((marca == "CHEVROLET") && (agencia == "ALLENDE")) ip = 84;
                else if ((marca == "CHEVROLET") && (agencia == "UNIVERSIDAD")) ip = 46;
                else
                {
                    ///// esto es por que no es global el sistema y no tengo el correo del vendedor deje en comentario el código
                    error = "no es sistema global";
                    return false;
                    ///////////////////////////////////////////

                    /*if (show_minempresas(marca, agencia))
                    {
                        min = dt2.Rows[0][0].ToString();
                        elodbc = "dsn=" + dt2.Rows[0][1].ToString() + ";";
                        dt2.Dispose();
                        dt2 = null;
                    }
                    else
                    {
                        error = "Error: no conecto minempresas";
                        dt2.Dispose();
                        dt2 = null;
                        return false;
                    }
                    if (!conectar()) return false;
                    try
                    {
                        OdbcCommand sqlCmd = new OdbcCommand(envolvente3(quien, Email_vendedor, marca, agencia), conn);
                        OdbcDataAdapter sqlDa = new OdbcDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        sqlCmd.Dispose();
                        sqlCmd = null;
                        sqlDa.Dispose();
                        sqlDa = null;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        return false;
                    }*/
                }

                if (ip > 0)
                {
                    if (conectar2(ip))
                    {
                        try
                        {
                            String sql1 = "";
                            ////String invent = data.Replace(" ", "% %");

                            if (marca == "KIA")
                            {
                                if (agencia == "GONZALITOS" || agencia == "SANTA CATARINA")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIOR_ARTURS_TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIOR_ARTURS_TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";

                                }
                                else if (agencia == "LINDA VISTA")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                                else if (agencia == "REYNOSA")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                            }
                            else if (marca == "MG")
                            {
                                if (agencia == "LINDA VISTA")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                                else if (agencia == "LAS TORRES")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                            }
                            else if (marca == "CHRYSLER")
                            {
                                if (agencia == "CUMBRES")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR = 'SEMINUEVOS' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR ='SEMINUEVOS' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES') AND MARC_DESCRIP <>'FIAT'and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES')AND MARC_DESCRIP <>'FIAT'and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR = 'SEMINUEVOS' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR ='SEMINUEVOS' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                }
                                else if (agencia == "CONTRY")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND MARC_DESCRIP <> 'FIAT'and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND MARC_DESCRIP <> 'FIAT'and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR = 'SEMINUEVOS' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR ='SEMINUEVOS' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                }
                                else if (agencia == "P.ABASTOS")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES') AND MARC_DESCRIP <>'FIAT'and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES')AND MARC_DESCRIP <>'FIAT'and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select fecha,sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR = 'SEMINUEVOS' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR ='SEMINUEVOS' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha";
                                }
                                else if (agencia == "LINARES")
                                {
                                    if (quien == "venta") sql1 = "";
                                    else if (quien == "seminuevos") sql1 = "";
                                }
                            }
                            else if (marca == "FORD")
                            {
                                if (agencia == "VALLE")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VALLE' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VALLE' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='VALLE' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='VALLE' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=1 and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=1 and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=1 and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=1 and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=1 and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=1 and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                                else if (agencia == "VASCONCELOS")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VASCONCELOS' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VASCONCELOS' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='VASCONCELOS' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='VASCONCELOS' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=2 and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=2 and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=2 and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=2 and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=2 and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=2 and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                            }
                            else if (marca == "FIAT")
                            {
                                if (agencia == "CUMBRES")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='FNU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='FNU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "";
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA')AND MARC_DESCRIP ='FIAT'and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND AGEN_NOMAGENCIA IN ('STALUCIA') AND MARC_DESCRIP ='FIAT' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "";
                                }
                            }
                            else if ((marca == "MITSUBISHI") && (agencia == "SANTA LUCIA"))
                            {
                                if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4' and AGEN_NOMAGENCIA='CARONEMOTORS' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4' and AGEN_NOMAGENCIA='CARONEMOTORS' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                else if (quien == "seminuevos") sql1 = "";
                            }
                            else if ((marca == "MITSUBISHI") && (agencia == "SALTILLO"))
                            {
                                if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') AND EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                else if (quien == "seminuevos") sql1 = "";
                            }
                            else if (marca == "CHEVROLET")
                            {
                                if (agencia == "TLALPAN")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    ////ordenes en proceso select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between 20170401 and 20170431) group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between 20170401 and 20170431) group by agrupar,fecha) group by fecha order by TO_DATE(fecha)
                                }
                                else if (agencia == "LAS BOMBAS")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "";
                                }
                                else if (agencia == "RUIZ CORTINES")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                                else if (agencia == "NOGALAR")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='NOGALAR' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='NOGALAR' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='NOGALAR' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='NOGALAR' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                                else if (agencia == "CONCORDIA")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='CONCORDIA' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='CONCORDIA' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "";
                                }
                                else if (agencia == "LAS TORRES")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='LAS TORRES' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='LAS TORRES' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='LAS TORRES' and FORM_GRUPODEVENTADIR = 'SEMINUEVOS' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='LAS TORRES' and FORM_GRUPODEVENTADIR = 'SEMINUEVOS' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=3 and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=3 and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=3 and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=3 and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=3 and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=3 and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                                else if (agencia == "ALLENDE")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='ALLENDE' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='ALLENDE' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='ALLENDE' and FORM_GRUPODEVENTADIR = 'SEMINUEVOS' and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and AGEN_NOMAGENCIA='ALLENDE' and FORM_GRUPODEVENTADIR = 'SEMINUEVOS' and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=4 and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=4 and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=4 and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=4 and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=4 and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and AGEN_IDAGENCIA=4 and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                                else if (agencia == "UNIVERSIDAD")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                    else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                    else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP' and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                }
                            }
                            else if (marca == "NISSAN")
                            {
                                if (agencia == "SAN JERONIMO")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                }
                                if (agencia == "LAS AGUILAS")
                                {
                                    if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                    else if (quien == "seminuevos") sql1 = "select sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US' and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                }
                            }
                            else
                            {
                                if (quien == "venta") sql1 = "select sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                else if (quien == "seminuevos") sql1 = "select sum(ventas) venta from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='US'" + filtro + " and FORM_GRUPODEVENTADIR in ('SEMINUEVOS','SEMINUEVOS INTERCAMBIOS') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")";
                                else if (quien == "ofacturadas") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                else if (quien == "ofachyp") sql1 = "select fecha,sum(ventas) ventas from (select count(agrupar) suma,agrupar,fecha,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA fecha from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha union all select count(agrupar) suma,agrupar,fecha,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,FASE_FECHA1 fecha from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,fecha) group by fecha order by TO_DATE(fecha)";
                                else if (quien == "oproceso") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                                else if (quien == "oprocesohyp") sql1 = "select fecha,sum(ordenes) ordenes from (select count(*),ORSE_FOLIO,ORSE_FECHAENTRADA fecha,(1) ordenes from AUTOS.SE_VORDENSERVICIO_ARTURS where TIPOVTA='HP'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO'  and to_number(to_char(TO_DATE(ORSE_FECHAENTRADA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + " group by ORSE_FOLIO,ORSE_FECHAENTRADA) group by fecha order by fecha";
                            }

                            OracleCommand cmd = new OracleCommand(sql1, cnx);
                            OracleDataAdapter Da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            Da.Fill(dt);
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            cmd.Dispose();
                            cmd = null;
                            Da.Dispose();
                            Da = null;
                            return true;
                        }
                        catch (Exception ex)
                        {
                            error = "Error: Excepción1 " + ex.ToString();
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            return false;
                        }
                    }
                    else
                    {
                        error = "Error: No Abrió";
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        public bool conexion5(String marca, String lugaragen)
        {
            String filtro = "";
            int ip = 0;

            sistema_global = true;

            try
            {
                if (marca == "NISSAN")
                {
                    segmento = "10";

                    if (lugaragen == "SAN JERONIMO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "SAN JERONIMO LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "LAS AGUILAS")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='3'";
                    }
                    else if (lugaragen == "TLALPAN")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='4'";
                    }
                    else if (lugaragen == "TLALPAN LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='5'";
                    }
                    else if (lugaragen == "LA JOYA")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='6'";
                    }
                    else if (lugaragen == "CROSSING")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='7'";
                    }
                    else if (lugaragen == "LA JOYA LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='8'";
                    }
                    else
                    {
                        return true;
                        /***  no tengo webservice para los datos del cliente en forma de lista solo de uno en uno
                       String cambio2;

                        if ((lugaragen == "MANTE") || (lugaragen == "VALLES"))
                        {   
                            golfoservicio2.ServicioSiaClient cliente2 = new golfoservicio2.ServicioSiaClient();
                            lanissan = cliente2.enc2(quien, marca, lugaragen, data);
                            return true;
                        }
                        else if ((lugaragen == "TUXPAN") || (lugaragen == "POZA RICA"))
                        {
                            if (lugaragen == "POZA RICA") cambio2 = "POZA";
                            else cambio2 = lugaragen;

                            golfoservicio4.ServicioSiaClient cliente4 = new golfoservicio4.ServicioSiaClient();
                            lanissan = cliente4.enc2(quien, marca, cambio2, data);
                            return true;
                        }
                       **********/
                    }
                }
                else if (marca == "JETOUR")
                {
                    segmento = "10";

                    if (lugaragen == "SALTILLO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='20'";
                    }
                    else if (lugaragen == "LAS TORRES")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='21'";
                    }
                    else if (lugaragen == "LAS TORRES LAREDO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='28'";
                    }
                }
                else if (marca == "GEELY")
                {
                    segmento = "10";

                    if (lugaragen == "SENDERO")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "LINDA VISTA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "CONTRY")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='5'";
                    }
                    else if (lugaragen == "CONCORDIA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='7'";
                    }
                }
                else if (marca == "GREAT WALL")
                {
                    segmento = "10";

                    if (lugaragen == "SENDERO")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "SENDERO FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='4'";
                    }
                    else if (lugaragen == "GONZALITOS")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "GONZALITOS FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "OMODA")
                {
                    segmento = "10";

                    if (lugaragen == "LINDA VISTA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "GONZALITOS")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "GONZALITOS LAREDO")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "FORD")
                {
                    if (lugaragen == "VALLE")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "VALLE LAREDO")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (lugaragen == "VASCONCELOS")
                    {
                        ip = 164;
                    }
                }
                else if (marca == "MERCEDES BENZ") return false;
                else if (marca == "MG")
                {
                    if (lugaragen == "LINDA VISTA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='10'";
                    }
                    else if (lugaragen == "LINDA VISTA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='29'";
                    }
                    else if (lugaragen == "CONSTITUYENTES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='31'";
                    }
                    else if (lugaragen == "CONSTITUYENTES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='40'";
                    }
                    else if (lugaragen == "LAS TORRES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='9'";
                    }
                    else if (lugaragen == "LAS TORRES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='33'";
                    }
                    else if (lugaragen == "TEC")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='37'";
                    }
                    else if (lugaragen == "VALLE")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='11'";
                    }
                    else if (lugaragen == "VALLE LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='38'";
                    }
                    else if (lugaragen == "JURIQUILLA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='23'";
                    }
                    else if (lugaragen == "JURIQUILLA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='39'";
                    }
                    else if (lugaragen == "LOMAS VERDES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='14'";
                    }
                    else if (lugaragen == "LOMAS VERDES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='34'";
                    }
                    else if (lugaragen == "TIJUANA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='19'";
                    }
                    else if (lugaragen == "TIJUANA SUCURSAL")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='17'";
                    }
                    else if (lugaragen == "CORREGIDORA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='12'";
                    }
                    else if (lugaragen == "CORREGIDORA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='30'";
                    }
                    else if (lugaragen == "QUERETARO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='13'";
                    }
                    else if (lugaragen == "LOPEZ MATEOS")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='16'";
                    }
                    else if (lugaragen == "LOPEZ MATEOS LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='35'";
                    }
                    else if (lugaragen == "LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='9' and AGEN_IDAGENCIA='15'";
                    }
                    else if (lugaragen == "MEXICALI")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='12' and AGEN_IDAGENCIA='18'";
                    }
                    else if (lugaragen == "LINARES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='26'";
                    }
                    else if (lugaragen == "TOREO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='41'";
                    }
                }
                else if (marca == "CHIREY")
                {
                    if (lugaragen == "LINDA VISTA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "LINDA VISTA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='7'";
                    }
                    else if (lugaragen == "FLETEROS")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "FLETEROS LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='5'";
                    }
                    else if (lugaragen == "CORREGIDORA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='3'";
                    }
                    else if (lugaragen == "CORREGIDORA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='6'";
                    }
                    else if (lugaragen == "JURIQUILLA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='4'";
                    }
                    else if (lugaragen == "JURIQUILLA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='8'";
                    }
                }
                else if (marca == "CHANGAN")
                {
                    segmento = "10";

                    if (lugaragen == "CORREGIDORA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='5'";
                    }
                }
                else if (marca == "KIA")
                {
                    if (lugaragen == "GONZALITOS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='2'";
                    }
                    else if (lugaragen == "GONZALITOS FRONTERA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='7'";
                    }
                    else if (lugaragen == "FLETEROS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='9'";
                    }
                    else if (lugaragen == "LINDA VISTA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (lugaragen == "LINDA VISTA FRONTERA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (lugaragen == "REYNOSA") ip = 173;
                    else if (lugaragen == "LAREDO") ip = 78;
                    else if (lugaragen == "SANTA CATARINA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='5'";
                    }
                    else if (lugaragen == "LA FE")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='11'";
                    }
                }
                else if (marca == "CHRYSLER")
                {
                    if (lugaragen == "CUMBRES")
                    {
                        ip = 75;
                    }
                    else if (lugaragen == "SANTA LUCIA")
                    {
                        segmento = "11";
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (lugaragen == "SANTA LUCIA LAREDO")
                    {
                        segmento = "10";
                        ip = 27;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='32'";
                    }
                    else if (lugaragen == "P.ABASTOS")
                    {
                        ip = 79;
                    }
                    else if (lugaragen == "LINARES")
                    {
                        ip = 79;
                    }
                    else if (lugaragen == "CONTRY")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (lugaragen == "CONTRY LAREDO")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='36'";
                    }
                }
                else if ((marca == "FIAT") && (lugaragen == "CUMBRES")) ip = 75;
                else if (marca == "MITSUBISHI")
                {
                    if (lugaragen == "SANTA LUCIA")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '14' and AGEN_IDAGENCIA = '4' and AGEN_NOMAGENCIA = 'CARONEMOTORS'";
                    }
                    else if (lugaragen == "SALTILLO")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5'";
                        //filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5' and AGEN_NOMAGENCIA = ''";
                    }
                }
                else if ((marca == "ALFA ROMEO") && (lugaragen == "CONTRY")) ip = 98;
                else if ((marca == "CHEVROLET") && (lugaragen == "TLALPAN")) ip = 38;
                else if ((marca == "CHEVROLET") && (lugaragen == "LAS BOMBAS")) ip = 82;
                else if ((marca == "CHEVROLET") && (lugaragen == "RUIZ CORTINES")) { segmento = "10"; ip = 33; }
                else if ((marca == "CHEVROLET") && (lugaragen == "FIDEL VELAZQUEZ")) ip = 159;
                else if ((marca == "CHEVROLET") && (lugaragen == "NOGALAR")) ip = 160;
                else if ((marca == "CHEVROLET") && (lugaragen == "CONCORDIA")) ip = 46;
                else if ((marca == "CHEVROLET") && (lugaragen == "LAS TORRES"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '3'";
                }
                else if ((marca == "CHEVROLET") && (lugaragen == "LAS TORRES LAREDO"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '8'";
                }
                else if ((marca == "CHEVROLET") && (lugaragen == "CADEREYTA")) ip = 84;
                else if ((marca == "CHEVROLET") && (lugaragen == "ALLENDE")) ip = 84;
                else if ((marca == "CHEVROLET") && (lugaragen == "UNIVERSIDAD")) ip = 46;
                else
                {
                    //para multimarca
                    sistema_global = false;
                    String sql1 = "";
                    String dato_odbc = "";
                    try
                    {
                        if ((marca == "VOLKSWAGEN") && (lugaragen == "GONZALITOS")) dato_odbc = "dsn=c1vwgonzalitos;";
                        else if ((marca == "VOLKSWAGEN") && (lugaragen == "CONTRY")) dato_odbc = "dsn=c1vwcontry;";
                        else if ((marca == "VOLKSWAGEN") && (lugaragen == "LINDA VISTA")) dato_odbc = "dsn=c1vwlindavista;";
                        else if ((marca == "VOLKSWAGEN") && (lugaragen == "REYNOSA")) dato_odbc = "dsn=c1vwreynosa;";
                        else if ((marca == "VOLKSWAGEN") && (lugaragen == "RIO BRAVO")) dato_odbc = "dsn=c1vwreynosa;";
                        else if ((marca == "SEAT") && (lugaragen == "CONTRY")) dato_odbc = "dsn=c1vwcontry;";
                        else if ((marca == "SEAT") && (lugaragen == "LINDA VISTA")) dato_odbc = "dsn=c1vwlindavista;";
                        else if ((marca == "SEAT") && (lugaragen == "REYNOSA")) dato_odbc = "dsn=c1vwreynosa;";
                        else if ((marca == "SEAT") && (lugaragen == "CONCORDIA")) dato_odbc = "dsn=c1vwlindavista;";
                        //else if ((marca == "CHEVROLET") && (lugaragen == "ALLENDE")) dato_odbc = "dsn=c1chlastorres;";
                        //else if ((marca == "CHEVROLET") && (lugaragen == "LAS TORRES")) dato_odbc = "dsn=c1chlastorres;";
                        //else if ((marca == "CHEVROLET") && (lugaragen == "NOGALAR")) dato_odbc = "dsn=c1chruizcortines;";
                        //else if ((marca == "CHEVROLET") && (lugaragen == "RUIZ CORTINEZ")) dato_odbc = "dsn=c1chruizcortines;";
                        else if ((marca == "CHEVROLET") && (lugaragen == "UNIVERSIDAD")) dato_odbc = "dsn=c1chuniversidad;";
                        conn = new OdbcConnection();
                        conn.ConnectionString = dato_odbc;
                        conn.Open();
                        if (conn.State == ConnectionState.Open)
                        {
                            /*sql1 = "SELECT C15.ID,C15.NOMBRE_VISIBLE,C15.FECHA_ALTA,C15.APELLIDO_PRIMERO,C15.APELLIDO_SEGUNDO,C15.NOMBRES,C15.CODIGO_FISCAL,";
                            sql1 += "C15.RAZON_SOCIAL,C15.PERSONA_FISICA,C15.COMPONER_NOMBRE_VISIBLE,C15.NO_ENCUESTAR,C15.NO_CORREO_MASIVO,C15.NO_SMS_MASIVO,";
                            sql1 += "extract (year from c15.FECHA_ALTA)||'-'||RIGHT('00'||extract (month from c15.FECHA_ALTA),2)||'-'||RIGHT('00'||extract (Day from c15.FECHA_ALTA),2) as FechaAlta,";
                            sql1 += "' 'AS FechaBaja from comercial C14 LEFT JOIN ente C15 ON C15.ID = C14.id_ente";*/

                            sql1 = "SELECT C15.ID,C15.NOMBRE_VISIBLE,C15.FECHA_ALTA,C15.APELLIDO_PRIMERO,C15.APELLIDO_SEGUNDO,C15.NOMBRES,";
                            sql1 += "C15.CODIGO_FISCAL,C15.RAZON_SOCIAL,C15.PERSONA_FISICA,C15.COMPONER_NOMBRE_VISIBLE,C15.NO_ENCUESTAR,C15.NO_CORREO_MASIVO,C15.NO_SMS_MASIVO";
                            sql1 += ",extract (year from c15.FECHA_ALTA)||'-'||RIGHT('00'||extract (month from c15.FECHA_ALTA),2)||'-'||RIGHT('00'||extract (Day from c15.FECHA_ALTA),2) as FechaAlta,";
                            sql1 += "baja_fh AS FechaBaja,C14.tipovend,C14.codigo,C14.vigente from comercial C14 LEFT JOIN ente C15 ON C15.ID = C14.id_ente where C14.vigente='T'";

                            // ARREGLO QUE NO JALO VUELVO AL ANTERIOR
                            //sql1 = "select C14.codigo,C15.NOMBRE_VISIBLE,C15.NO_CORREO_MASIVO from comercial C14 LEFT JOIN ente C15 ON C15.ID = C14.id_ente where C14.vigente='T'";

                            OdbcCommand sqlCmd = new OdbcCommand(sql1, conn);
                            OdbcDataAdapter sqlDa = new OdbcDataAdapter(sqlCmd);
                            dt = new DataTable();
                            sqlDa.Fill(dt);
                            conn.Close();
                            conn.Dispose();
                            conn = null;
                            sqlCmd.Dispose();
                            sqlCmd = null;
                            sqlDa.Dispose();
                            sqlDa = null;
                            return true;
                        }
                        else return false;
                    }
                    catch (Exception ex)
                    {
                        error = "Error: " + ex.ToString();
                        if (conn.State == ConnectionState.Open) conn.Close();
                        conn.Dispose();
                        conn = null;
                        return false;
                    }
                }

                if (ip > 0)
                {
                    if (conectar2(ip))
                    {
                        try
                        {
                            // and AGEN_NOMAGENCIA='CUMBRES'
                            String sql1 = "";

                            if (marca == "KIA")
                            {
                                sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC'" + filtro + " order by nombre";
                            }
                            else if (marca == "MG")
                            {
                                sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC'" + filtro + " order by nombre";
                            }
                            else if (marca == "CHIREY")
                            {
                                sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC'" + filtro + " order by nombre";
                            }
                            else if (marca == "CHANGAN")
                            {
                                sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC'" + filtro + " order by nombre";
                            }
                            else if (marca == "JETOUR")
                            {
                                sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC'" + filtro + " order by nombre";
                            }
                            else if (marca == "GEELY")
                            {
                                sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC'" + filtro + " order by nombre";
                            }
                            else if (marca == "GREAT WALL")
                            {
                                sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC'" + filtro + " order by nombre";
                            }
                            else if (marca == "OMODA")
                            {
                                sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC'" + filtro + " order by nombre";
                            }
                            else if (marca == "CHRYSLER")
                            {
                                if (lugaragen == "CUMBRES") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' and EMPR_EMPRESAID='2' and AGEN_NOMAGENCIA='CUMBRES' order by nombre";
                                //////else if (lugaragen == "SANTA LUCIA") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' and EMPR_EMPRESAID='3' and AGEN_NOMAGENCIA='STALUCIA' order by nombre";
                                else if (lugaragen == "SANTA LUCIA") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC'" + filtro + " and AGEN_NOMAGENCIA='STALUCIA' order by nombre";
                                else if (lugaragen == "P.ABASTOS") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' and EMPR_EMPRESAID='3' and AGEN_NOMAGENCIA='STALUCIA' order by nombre";
                                else if (lugaragen == "LINARES") sql1 = "";
                                else if (lugaragen == "CONTRY") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' order by nombre";
                            }
                            else if (marca == "FORD")
                            {
                                if (lugaragen == "VALLE") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' and AGEN_NOMAGENCIA='VALLE' order by nombre";
                                else if (lugaragen == "VASCONCELOS") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' order by nombre";
                            }
                            else if (marca == "FIAT")
                            {
                                if (lugaragen == "CUMBRES") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' and EMPR_EMPRESAID='2' and AGEN_NOMAGENCIA='FIAT' order by nombre";
                                else if (lugaragen == "SANTA LUCIA") sql1 = "";
                            }
                            else if ((marca == "MITSUBISHI") && (lugaragen == "SANTA LUCIA")) sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4' order by nombre";
                            else if ((marca == "MITSUBISHI") && (lugaragen == "SALTILLO")) sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' order by nombre";
                            else if ((marca == "ALFA ROMEO") && (lugaragen == "CONTRY")) sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' order by nombre";
                            else if (marca == "CHEVROLET")
                            {
                                if (lugaragen == "TLALPAN") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' order by nombre";
                                else if (lugaragen == "LAS BOMBAS") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' order by nombre";
                                else if (lugaragen == "RUIZ CORTINES") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' order by nombre";
                                else if (lugaragen == "NOGALAR") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' order by nombre";
                                else if (lugaragen == "CONCORDIA") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' order by nombre";
                                else if (lugaragen == "LAS TORRES") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' and AGEN_NOMAGENCIA='LAS TORRES' order by nombre";
                                else if (lugaragen == "CADEREYTA") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMCORTO)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' order by nombre";
                                //else if (lugaragen == "ALLENDE") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' order by nombre";
                                else if (lugaragen == "ALLENDE") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMCORTO)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' order by nombre";
                                else if (lugaragen == "UNIVERSIDAD") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' order by nombre";
                                //else if (lugaragen == "FIDEL VELAZQUEZ") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' and EMPR_EMPRESAID=3 and AGEN_IDAGENCIA=6 order by nombre";
                                else if (lugaragen == "FIDEL VELAZQUEZ") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' order by nombre";
                            }
                            else if (marca == "NISSAN")
                            {
                                if (lugaragen == "SAN JERONIMO") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' and AGEN_NOMAGENCIA='CARONE SANJE' and EMPR_EMPRESAID=4 and AGEN_IDAGENCIA=1 order by nombre";
                                else if (lugaragen == "LAS AGUILAS") sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' and EMPR_EMPRESAID=4 and AGEN_IDAGENCIA=3 order by nombre";
                                else sql1 = "SELECT VEND_CLAVE,(trim(VEND_APELLIDOPATERNO) || ' ' || trim(VEND_APELLIDOMATERNO) || ' ' || trim(VEND_NOMBRE)) nombre,VEND_EMAIL Email FROM AUTOS.vt_vvendedores where VEND_STATUS='AC' order by nombre";
                            }
                            

                            OracleCommand cmd = new OracleCommand(sql1, cnx);
                            OracleDataAdapter Da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            Da.Fill(dt);
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            cmd.Dispose();
                            cmd = null;
                            Da.Dispose();
                            Da = null;
                            return true;
                        }
                        catch (Exception ex)
                        {
                            error = "Error: Excepción1 " + ex.ToString();
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            return false;
                        }
                    }
                    else
                    {
                        error = "Error: No Abrió";
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        public bool conexion7(String quien, String marca, String agencia, String fec_ini, String fec_fin, String vendedor_asesor)
        {
            /*if (vendedor_asesor != "")
            {
                vendedor_asesor = vendedor_asesor;
            }*/

            int ip = 0;
            string filtro = "";
            try
            {
                if (marca == "MERCEDES BENZ") return false;

                if (marca == "NISSAN")
                {
                    segmento = "10";

                    if (agencia == "SAN JERONIMO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "SAN JERONIMO LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "LAS AGUILAS")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "TLALPAN")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "TLALPAN LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "LA JOYA")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "CROSSING")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "LA JOYA LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='8'";
                    }
                    else
                    {
                        return true;
                        /***  no tengo webservice para los datos del cliente en forma de lista solo de uno en uno
                       String cambio2;

                        if ((agencia == "MANTE") || (agencia == "VALLES"))
                        {   
                            golfoservicio2.ServicioSiaClient cliente2 = new golfoservicio2.ServicioSiaClient();
                            lanissan = cliente2.enc2(quien, marca, agencia, data);
                            return true;
                        }
                        else if ((agencia == "TUXPAN") || (agencia == "POZA RICA"))
                        {
                            if (agencia == "POZA RICA") cambio2 = "POZA";
                            else cambio2 = agencia;

                            golfoservicio4.ServicioSiaClient cliente4 = new golfoservicio4.ServicioSiaClient();
                            lanissan = cliente4.enc2(quien, marca, cambio2, data);
                            return true;
                        }
                       **********/
                    }
                }
                else if (marca == "JETOUR")
                {
                    segmento = "10";

                    if (agencia == "SALTILLO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='20'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='21'";
                    }
                    else if (agencia == "LAS TORRES LAREDO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='28'";
                    }
                }
                else if (marca == "GEELY")
                {
                    segmento = "10";

                    if (agencia == "SENDERO")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "CONTRY")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "CONCORDIA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='7'";
                    }
                }
                else if (marca == "GREAT WALL")
                {
                    segmento = "10";

                    if (agencia == "SENDERO")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "SENDERO FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "GONZALITOS")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "GONZALITOS FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "OMODA")
                {
                    segmento = "10";

                    if (agencia == "LINDA VISTA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "GONZALITOS")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "GONZALITOS LAREDO")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "KIA")
                {
                    if (agencia == "GONZALITOS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "GONZALITOS FRONTERA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "FLETEROS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA FRONTERA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "REYNOSA") ip = 173;
                    else if (agencia == "LAREDO") ip = 78;
                    else if (agencia == "SANTA CATARINA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "LA FE")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='11'";
                    }
                }
                else if (marca == "MG")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='10'";
                    }
                    else if (agencia == "LINDA VISTA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='29'";
                    }
                    else if (agencia == "CONSTITUYENTES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='31'";
                    }
                    else if (agencia == "CONSTITUYENTES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='40'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "LAS TORRES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='33'";
                    }
                    else if (agencia == "TEC")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='37'";
                    }
                    else if (agencia == "VALLE")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='11'";
                    }
                    else if (agencia == "VALLE LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='38'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='23'";
                    }
                    else if (agencia == "JURIQUILLA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='39'";
                    }
                    else if (agencia == "LOMAS VERDES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='14'";
                    }
                    else if (agencia == "LOMAS VERDES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='34'";
                    }
                    else if (agencia == "TIJUANA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='19'";
                    }
                    else if (agencia == "TIJUANA SUCURSAL")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='17'";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='12'";
                    }
                    else if (agencia == "CORREGIDORA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='30'";
                    }
                    else if (agencia == "QUERETARO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='13'";
                    }
                    else if (agencia == "LOPEZ MATEOS")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='16'";
                    }
                    else if (agencia == "LOPEZ MATEOS LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='35'";
                    }
                    else if (agencia == "LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='9' and AGEN_IDAGENCIA='15'";
                    }
                    else if (agencia == "MEXICALI")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='12' and AGEN_IDAGENCIA='18'";
                    }
                    else if (agencia == "LINARES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='26'";
                    }
                    else if (agencia == "TOREO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='41'";
                    }
                }
                else if (marca == "CHIREY")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "FLETEROS")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "FLETEROS LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "CORREGIDORA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "JURIQUILLA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='8'";
                    }
                }
                else if (marca == "CHANGAN")
                {
                    segmento = "10";

                    if (agencia == "CORREGIDORA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='5'";
                    }
                }
                else if (marca == "CHRYSLER")
                {
                    if (agencia == "CUMBRES")
                    {
                        ip = 75;
                    }
                    else if (agencia == "SANTA LUCIA")
                    {
                        segmento = "11";
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "SANTA LUCIA LAREDO")
                    {
                        segmento = "10";
                        ip = 27;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='32'";
                    }
                    else if (agencia == "P.ABASTOS")
                    {
                        ip = 79;
                    }
                    else if (agencia == "LINARES")
                    {
                        ip = 79;
                    }
                    else if (agencia == "CONTRY")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "CONTRY LAREDO")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='36'";
                    }
                }
                else if (marca == "FORD")
                {
                    if (agencia == "VALLE")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "VALLE LAREDO")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "VASCONCELOS")
                    {
                        ip = 164;
                    }
                }
                else if ((marca == "FIAT") && (agencia == "CUMBRES")) ip = 75;
                else if (marca == "MITSUBISHI")
                {
                    if (agencia == "SANTA LUCIA")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '14' and AGEN_IDAGENCIA = '4' and AGEN_NOMAGENCIA = 'CARONEMOTORS'";
                    }
                    else if (agencia == "SALTILLO")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5'";
                        //filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5' and AGEN_NOMAGENCIA = ''";
                    }
                }
                else if ((marca == "ALFA ROMEO") && (agencia == "CONTRY")) ip = 98;
                else if ((marca == "CHEVROLET") && (agencia == "TLALPAN")) ip = 38;
                else if ((marca == "CHEVROLET") && (agencia == "LAS BOMBAS")) ip = 82;
                else if ((marca == "CHEVROLET") && (agencia == "RUIZ CORTINES")) ip = 159;
                else if ((marca == "CHEVROLET") && (agencia == "NOGALAR")) ip = 160;
                else if ((marca == "CHEVROLET") && (agencia == "CONCORDIA")) ip = 46;
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '3'";
                }
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES LAREDO"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '8'";
                }
                else if ((marca == "CHEVROLET") && (agencia == "ALLENDE")) ip = 84;
                else if ((marca == "CHEVROLET") && (agencia == "UNIVERSIDAD")) ip = 46;
                else
                {
                    ///// esto es por que no es global el sistema y no tengo el correo del vendedor deje en comentario el código
                    error = "no es sistema global";
                    return false;
                    ///////////////////////////////////////////

                    /*if (show_minempresas(marca, agencia))
                    {
                        min = dt2.Rows[0][0].ToString();
                        elodbc = "dsn=" + dt2.Rows[0][1].ToString() + ";";
                        dt2.Dispose();
                        dt2 = null;
                    }
                    else
                    {
                        error = "Error: no conecto minempresas";
                        dt2.Dispose();
                        dt2 = null;
                        return false;
                    }
                    if (!conectar()) return false;
                    try
                    {
                        OdbcCommand sqlCmd = new OdbcCommand(envolvente3(quien, Email_vendedor, marca, agencia), conn);
                        OdbcDataAdapter sqlDa = new OdbcDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        sqlCmd.Dispose();
                        sqlCmd = null;
                        sqlDa.Dispose();
                        sqlDa = null;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        return false;
                    }*/
                }

                if (ip > 0)
                {
                    if (conectar2(ip))
                    {
                        try
                        {
                            String sql1 = "", vendedor_asesor_var;
                            ////String invent = data.Replace(" ", "% %");

                            if (vendedor_asesor == "") vendedor_asesor_var = "";
                            else
                            {
                                if (quien == "venta") vendedor_asesor_var = "and NOMVENDEDOR like '%" + vendedor_asesor + "%' ";
                                else vendedor_asesor_var = "and NOMBRE like '%" + vendedor_asesor + "%' ";
                            }

                            if (marca == "KIA")
                            {
                                if (agencia == "GONZALITOS" || agencia == "SANTA CATARINA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null " + vendedor_asesor_var + "and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "" + filtro + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "" + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "LINDA VISTA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null " + vendedor_asesor_var + "and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "" + filtro + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "" + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "REYNOSA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null " + vendedor_asesor_var + "and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "LAREDO")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null " + vendedor_asesor_var + "and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }
                            else if (marca == "MG")
                            {
                                if (agencia == "LINDA VISTA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null " + vendedor_asesor_var + "and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "" + filtro + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "" + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "LAS TORRES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null " + vendedor_asesor_var + "and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "" + filtro + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "" + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }
                            else if (marca == "CHRYSLER")
                            {
                                if (agencia == "CUMBRES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and MARC_DESCRIP!='FIAT' and FAAU_FECHAENTREGA is not null " + vendedor_asesor_var + "and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='2' and AGEN_IDAGENCIA = '1' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') " + vendedor_asesor_var + "AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES') and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') " + vendedor_asesor_var + "AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES')AND MARC_DESCRIP <>'FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA != '4' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "CONTRY")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') " + vendedor_asesor_var + "AND FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') " + vendedor_asesor_var + "AND MARC_DESCRIP <> 'FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA != '4' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "P.ABASTOS")
                                {
                                    if (quien == "venta") return false;
                                    else return false;
                                }
                                else if (agencia == "LINARES")
                                {
                                    if (quien == "venta") sql1 = "";
                                    else sql1 = "";
                                }
                            }
                            else if (marca == "FORD")
                            {
                                if (agencia == "VALLE")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='VALLE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='VALLE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '1' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "VASCONCELOS")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='VASCONCELOS' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='VASCONCELOS' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '2' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }
                            else if (marca == "FIAT")
                            {
                                if (agencia == "CUMBRES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and MARC_DESCRIP='FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='2' and AGEN_IDAGENCIA = '2' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    if (quien == "venta") return false;
                                    else return false;
                                }

                                /*if (agencia == "CUMBRES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='FNU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') " + vendedor_asesor_var + "AND AGEN_NOMAGENCIA IN ('STALUCIA') AND MARC_DESCRIP ='FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }*/
                            }
                            else if ((marca == "MITSUBISHI") && (agencia == "SANTA LUCIA"))
                            {
                                if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='MNU' and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4' and AGEN_NOMAGENCIA='CARONEMOTORS' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') " + vendedor_asesor_var + "AND EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4' and AGEN_NOMAGENCIA='CARONEMOTORS' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                            }
                            else if ((marca == "MITSUBISHI") && (agencia == "SALTILLO"))
                            {
                                if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='MNU' and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') " + vendedor_asesor_var + "AND EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                            }
                            else if (marca == "CHEVROLET")
                            {
                                if (agencia == "TLALPAN")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "LAS BOMBAS")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else return false;
                                }
                                else if (agencia == "RUIZ CORTINES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '5' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "NOGALAR")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='NOGALAR' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='NOGALAR' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '7' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "CONCORDIA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='CONCORDIA' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='CONCORDIA' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else return false;
                                }
                                else if (agencia == "LAS TORRES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='LAS TORRES' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='LAS TORRES' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '3' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "ALLENDE")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='ALLENDE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='ALLENDE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '4' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "UNIVERSIDAD")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }
                            else if (marca == "NISSAN")
                            {
                                if (agencia == "SAN JERONIMO")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                if (agencia == "LAS AGUILAS")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }
                            else
                            {
                                if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null " + vendedor_asesor_var + "and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "" + filtro + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "" + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                            }

                            OracleCommand cmd = new OracleCommand(sql1, cnx);
                            OracleDataAdapter Da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            Da.Fill(dt);
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            cmd.Dispose();
                            cmd = null;
                            Da.Dispose();
                            Da = null;
                            return true;
                        }
                        catch (Exception ex)
                        {
                            error = "Error: Excepción1 " + ex.ToString();
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            return false;
                        }
                    }
                    else
                    {
                        error = "Error: No Abrió";
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        public bool conexion8(String quien, String marca, String agencia, String fec_ini, String fec_fin)
        {
            int ip = 0;
            string filtro = "";
            try
            {
                if (marca == "MERCEDES BENZ") return false;

                if (marca == "NISSAN")
                {
                    segmento = "10";

                    if (agencia == "SAN JERONIMO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "SAN JERONIMO LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "LAS AGUILAS")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "TLALPAN")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "TLALPAN LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "LA JOYA")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "CROSSING")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "LA JOYA LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='8'";
                    }
                    else
                    {
                        return true;
                        /***  no tengo webservice para los datos del cliente en forma de lista solo de uno en uno
                       String cambio2;

                        if ((agencia == "MANTE") || (agencia == "VALLES"))
                        {   
                            golfoservicio2.ServicioSiaClient cliente2 = new golfoservicio2.ServicioSiaClient();
                            lanissan = cliente2.enc2(quien, marca, agencia, data);
                            return true;
                        }
                        else if ((agencia == "TUXPAN") || (agencia == "POZA RICA"))
                        {
                            if (agencia == "POZA RICA") cambio2 = "POZA";
                            else cambio2 = agencia;

                            golfoservicio4.ServicioSiaClient cliente4 = new golfoservicio4.ServicioSiaClient();
                            lanissan = cliente4.enc2(quien, marca, cambio2, data);
                            return true;
                        }
                       **********/
                    }                    
                }
                else if (marca == "JETOUR")
                {
                    segmento = "10";

                    if (agencia == "SALTILLO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='20'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='21'";
                    }
                    else if (agencia == "LAS TORRES LAREDO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='28'";
                    }
                }
                else if (marca == "GEELY")
                {
                    segmento = "10";

                    if (agencia == "SENDERO")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "CONTRY")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "CONCORDIA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='7'";
                    }
                }
                else if (marca == "GREAT WALL")
                {
                    segmento = "10";

                    if (agencia == "SENDERO")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "SENDERO FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "GONZALITOS")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "GONZALITOS FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "OMODA")
                {
                    segmento = "10";

                    if (agencia == "LINDA VISTA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "GONZALITOS")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "GONZALITOS LAREDO")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "KIA")
                {
                    if (agencia == "GONZALITOS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "GONZALITOS FRONTERA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "FLETEROS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA FRONTERA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "REYNOSA") ip = 173;
                    else if (agencia == "LAREDO") ip = 78;
                    else if (agencia == "SANTA CATARINA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "LA FE")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='11'";
                    }
                }
                else if (marca == "MG")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='10'";
                    }
                    else if (agencia == "LINDA VISTA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='29'";
                    }
                    else if (agencia == "CONSTITUYENTES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='31'";
                    }
                    else if (agencia == "CONSTITUYENTES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='40'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "LAS TORRES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='33'";
                    }
                    else if (agencia == "TEC")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='37'";
                    }
                    else if (agencia == "VALLE")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='11'";
                    }
                    else if (agencia == "VALLE LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='38'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='23'";
                    }
                    else if (agencia == "JURIQUILLA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='39'";
                    }
                    else if (agencia == "LOMAS VERDES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='14'";
                    }
                    else if (agencia == "LOMAS VERDES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='34'";
                    }
                    else if (agencia == "TIJUANA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='19'";
                    }
                    else if (agencia == "TIJUANA SUCURSAL")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='17'";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='12'";
                    }
                    else if (agencia == "CORREGIDORA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='30'";
                    }
                    else if (agencia == "QUERETARO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='13'";
                    }
                    else if (agencia == "LOPEZ MATEOS")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='16'";
                    }
                    else if (agencia == "LOPEZ MATEOS LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='35'";
                    }
                    else if (agencia == "LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='9' and AGEN_IDAGENCIA='15'";
                    }
                    else if (agencia == "MEXICALI")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='12' and AGEN_IDAGENCIA='18'";
                    }
                    else if (agencia == "LINARES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='26'";
                    }
                    else if (agencia == "TOREO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='41'";
                    }
                }
                else if (marca == "CHIREY")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "FLETEROS")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "FLETEROS LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "CORREGIDORA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "JURIQUILLA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='8'";
                    }
                }
                else if (marca == "CHANGAN")
                {
                    segmento = "10";

                    if (agencia == "CORREGIDORA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='5'";
                    }
                }
                else if (marca == "CHRYSLER")
                {
                    if (agencia == "CUMBRES")
                    {
                        ip = 75;
                    }
                    else if (agencia == "SANTA LUCIA")
                    {
                        segmento = "11";
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "SANTA LUCIA LAREDO")
                    {
                        segmento = "10";
                        ip = 27;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='32'";
                    }
                    else if (agencia == "P.ABASTOS")
                    {
                        ip = 79;
                    }
                    else if (agencia == "LINARES")
                    {
                        ip = 79;
                    }
                    else if (agencia == "CONTRY")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "CONTRY LAREDO")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='36'";
                    }
                }
                else if (marca == "FORD")
                {
                    if (agencia == "VALLE")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "VALLE LAREDO")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "VASCONCELOS")
                    {
                        ip = 164;
                    }
                }
                else if ((marca == "FIAT") && (agencia == "CUMBRES")) ip = 75;
                else if (marca == "MITSUBISHI")
                {
                    if (agencia == "SANTA LUCIA")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '14' and AGEN_IDAGENCIA = '4' and AGEN_NOMAGENCIA = 'CARONEMOTORS'";
                    }
                    else if (agencia == "SALTILLO")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5'";
                        //filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5' and AGEN_NOMAGENCIA = ''";
                    }
                }
                else if ((marca == "ALFA ROMEO") && (agencia == "CONTRY")) ip = 98;
                else if ((marca == "CHEVROLET") && (agencia == "TLALPAN")) ip = 38;
                else if ((marca == "CHEVROLET") && (agencia == "LAS BOMBAS")) ip = 82;
                else if ((marca == "CHEVROLET") && (agencia == "RUIZ CORTINES")) ip = 159;
                else if ((marca == "CHEVROLET") && (agencia == "NOGALAR")) ip = 160;
                else if ((marca == "CHEVROLET") && (agencia == "CONCORDIA")) ip = 46;
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '3'";
                }
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES LAREDO"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '8'";
                }
                else if ((marca == "CHEVROLET") && (agencia == "ALLENDE")) ip = 84;
                else if ((marca == "CHEVROLET") && (agencia == "UNIVERSIDAD")) ip = 46;
                else
                {
                    ///// esto es por que no es global el sistema y no tengo el correo del vendedor deje en comentario el código
                    error = "no es sistema global";
                    return false;
                    ///////////////////////////////////////////

                    /*if (show_minempresas(marca, agencia))
                    {
                        min = dt2.Rows[0][0].ToString();
                        elodbc = "dsn=" + dt2.Rows[0][1].ToString() + ";";
                        dt2.Dispose();
                        dt2 = null;
                    }
                    else
                    {
                        error = "Error: no conecto minempresas";
                        dt2.Dispose();
                        dt2 = null;
                        return false;
                    }
                    if (!conectar()) return false;
                    try
                    {
                        OdbcCommand sqlCmd = new OdbcCommand(envolvente3(quien, Email_vendedor, marca, agencia), conn);
                        OdbcDataAdapter sqlDa = new OdbcDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        sqlCmd.Dispose();
                        sqlCmd = null;
                        sqlDa.Dispose();
                        sqlDa = null;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        return false;
                    }*/
                }

                if (ip > 0)
                {
                    if (conectar2(ip))
                    {
                        try
                        {
                            String sql1 = "";
                            String[] peda1 = fec_ini.Split('/');
                            String[] peda2 = fec_fin.Split('/');
                            fec_ini = peda1[2] + peda1[1] + peda1[0];
                            fec_fin = peda2[2] + peda2[1] + peda2[0];

                            if (marca == "KIA")
                            {
                                if (agencia == "GONZALITOS" || agencia == "SANTA CATARINA")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                else if (agencia == "LINDA VISTA")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                else if (agencia == "REYNOSA")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                else if (agencia == "LAREDO")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                            }
                            if (marca == "MG")
                            {
                                if (agencia == "LINDA VISTA")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                else if (agencia == "LAS TORRES")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                            }
                            else if (marca == "CHRYSLER")
                            {
                                if (agencia == "CUMBRES")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and MARC_DESCRIP!='FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='2' and AGEN_IDAGENCIA = '1' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES') and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA != '4' and  TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                else if (agencia == "CONTRY")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') AND FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA != '4' and  TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                else if (agencia == "P.ABASTOS")
                                {
                                    if (quien == "venta") return false;
                                    else return false;
                                }
                                else if (agencia == "LINARES")
                                {
                                    if (quien == "venta") return false;
                                    else return false;
                                }
                            }
                            else if (marca == "FORD")
                            {
                                if (agencia == "VALLE")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VALLE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '1' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                else if (agencia == "VASCONCELOS")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VASCONCELOS' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '2' and  TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                            }
                            else if (marca == "FIAT")
                            {
                                if (agencia == "CUMBRES")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and MARC_DESCRIP='FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='2' and AGEN_IDAGENCIA = '2' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    if (quien == "venta") return false;
                                    else return false;
                                }
                            }
                            else if ((marca == "MITSUBISHI") && (agencia == "SANTA LUCIA"))
                            {
                                if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='MNU' and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4' and AGEN_NOMAGENCIA='CARONEMOTORS' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4' and  TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;

                            }
                            else if ((marca == "MITSUBISHI") && (agencia == "SALTILLO"))
                            {
                                if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='MNU' and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and  TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;

                            }
                            else if (marca == "CHEVROLET")
                            {
                                if (agencia == "TLALPAN")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                else if (agencia == "LAS BOMBAS")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else return false;
                                }
                                else if (agencia == "RUIZ CORTINES")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '5' and  TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                else if (agencia == "NOGALAR")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='NOGALAR' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='2' and AGEN_IDAGENCIA = '7' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                else if (agencia == "CONCORDIA")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='CONCORDIA' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else return false;
                                }
                                else if (agencia == "LAS TORRES")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='LAS TORRES' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '3' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                else if (agencia == "ALLENDE")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='ALLENDE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '4' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                else if (agencia == "UNIVERSIDAD")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                            }
                            else if (marca == "NISSAN")
                            {
                                if (agencia == "SAN JERONIMO")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                if (agencia == "SAN JERONIMO")
                                {
                                    if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                            }
                            else
                            {
                                if (quien == "venta") sql1 = "select distinct NOMVENDEDOR as NOMBRE from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                else sql1 = "select distinct NOMBRE from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                            }

                            OracleCommand cmd = new OracleCommand(sql1, cnx);
                            OracleDataAdapter Da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            Da.Fill(dt);
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            cmd.Dispose();
                            cmd = null;
                            Da.Dispose();
                            Da = null;
                            return true;
                        }
                        catch (Exception ex)
                        {
                            error = "Error: Excepción1 " + ex.ToString();
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            return false;
                        }
                    }
                    else
                    {
                        error = "Error: No Abrió";
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }


        public bool conexion9(String quien, String marca, String agencia, String invords, String asesor)
        {
            if (invords == "") return false;

            int ip = 0;
            string filtro = "";
            try
            {
                if (marca == "MERCEDES BENZ") return false;

                if (marca == "NISSAN")
                {
                    segmento = "10";

                    if (agencia == "SAN JERONIMO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "SAN JERONIMO LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "LAS AGUILAS")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "TLALPAN")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "TLALPAN LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "LA JOYA")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "CROSSING")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "LA JOYA LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='8'";
                    }
                    else
                    {
                        return true;
                        /***  no tengo webservice para los datos del cliente en forma de lista solo de uno en uno
                       String cambio2;

                        if ((agencia == "MANTE") || (agencia == "VALLES"))
                        {   
                            golfoservicio2.ServicioSiaClient cliente2 = new golfoservicio2.ServicioSiaClient();
                            lanissan = cliente2.enc2(quien, marca, agencia, data);
                            return true;
                        }
                        else if ((agencia == "TUXPAN") || (agencia == "POZA RICA"))
                        {
                            if (agencia == "POZA RICA") cambio2 = "POZA";
                            else cambio2 = agencia;

                            golfoservicio4.ServicioSiaClient cliente4 = new golfoservicio4.ServicioSiaClient();
                            lanissan = cliente4.enc2(quien, marca, cambio2, data);
                            return true;
                        }
                       **********/
                    }
                }
                else if (marca == "JETOUR")
                {
                    segmento = "10";

                    if (agencia == "SALTILLO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='20'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='21'";
                    }
                    else if (agencia == "LAS TORRES LAREDO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='28'";
                    }
                }
                else if (marca == "GEELY")
                {
                    segmento = "10";

                    if (agencia == "SENDERO")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "CONTRY")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "CONCORDIA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='7'";
                    }
                }
                else if (marca == "GREAT WALL")
                {
                    segmento = "10";

                    if (agencia == "SENDERO")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "SENDERO FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "GONZALITOS")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "GONZALITOS FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "OMODA")
                {
                    segmento = "10";

                    if (agencia == "LINDA VISTA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "GONZALITOS")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "GONZALITOS LAREDO")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "KIA")
                {
                    if (agencia == "GONZALITOS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "GONZALITOS FRONTERA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "FLETEROS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA FRONTERA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "REYNOSA") ip = 173;
                    else if (agencia == "LAREDO") ip = 78;
                    else if (agencia == "SANTA CATARINA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "LA FE")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='11'";
                    }
                }
                else if (marca == "MG")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='10'";
                    }
                    else if (agencia == "LINDA VISTA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='29'";
                    }
                    else if (agencia == "CONSTITUYENTES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='31'";
                    }
                    else if (agencia == "CONSTITUYENTES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='40'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "LAS TORRES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='33'";
                    }
                    else if (agencia == "TEC")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='37'";
                    }
                    else if (agencia == "VALLE")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='11'";
                    }
                    else if (agencia == "VALLE LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='38'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='23'";
                    }
                    else if (agencia == "JURIQUILLA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='39'";
                    }
                    else if (agencia == "LOMAS VERDES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='14'";
                    }
                    else if (agencia == "LOMAS VERDES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='34'";
                    }
                    else if (agencia == "TIJUANA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='19'";
                    }
                    else if (agencia == "TIJUANA SUCURSAL")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='17'";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='12'";
                    }
                    else if (agencia == "CORREGIDORA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='30'";
                    }
                    else if (agencia == "QUERETARO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='13'";
                    }
                    else if (agencia == "LOPEZ MATEOS")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='16'";
                    }
                    else if (agencia == "LOPEZ MATEOS LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='35'";
                    }
                    else if (agencia == "LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='9' and AGEN_IDAGENCIA='15'";
                    }
                    else if (agencia == "MEXICALI")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='12' and AGEN_IDAGENCIA='18'";
                    }
                    else if (agencia == "LINARES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='26'";
                    }
                    else if (agencia == "TOREO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='41'";
                    }
                }
                else if (marca == "CHIREY")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "FLETEROS")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "FLETEROS LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "CORREGIDORA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "JURIQUILLA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='8'";
                    }
                }
                else if (marca == "CHANGAN")
                {
                    segmento = "10";

                    if (agencia == "CORREGIDORA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='5'";
                    }
                }
                else if (marca == "CHRYSLER")
                {
                    if (agencia == "CUMBRES")
                    {
                        ip = 75;
                    }
                    else if (agencia == "SANTA LUCIA")
                    {
                        segmento = "11";
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "SANTA LUCIA LAREDO")
                    {
                        segmento = "10";
                        ip = 27;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='32'";
                    }
                    else if (agencia == "P.ABASTOS")
                    {
                        ip = 79;
                    }
                    else if (agencia == "LINARES")
                    {
                        ip = 79;
                    }
                    else if (agencia == "CONTRY")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "CONTRY LAREDO")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='36'";
                    }
                }
                else if (marca == "FORD")
                {
                    if (agencia == "VALLE")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "VALLE LAREDO")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "VASCONCELOS")
                    {
                        ip = 164;
                    }
                }
                else if ((marca == "FIAT") && (agencia == "CUMBRES")) ip = 75;
                else if (marca == "MITSUBISHI")
                {
                    if (agencia == "SANTA LUCIA")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '14' and AGEN_IDAGENCIA = '4' and AGEN_NOMAGENCIA = 'CARONEMOTORS'";
                    }
                    else if (agencia == "SALTILLO")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5'";
                        //filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5' and AGEN_NOMAGENCIA = ''";
                    }
                }
                else if ((marca == "ALFA ROMEO") && (agencia == "CONTRY")) ip = 98;
                else if ((marca == "CHEVROLET") && (agencia == "TLALPAN")) ip = 38;
                else if ((marca == "CHEVROLET") && (agencia == "LAS BOMBAS")) ip = 82;
                else if ((marca == "CHEVROLET") && (agencia == "RUIZ CORTINES")) ip = 159;
                else if ((marca == "CHEVROLET") && (agencia == "NOGALAR")) ip = 160;
                else if ((marca == "CHEVROLET") && (agencia == "CONCORDIA")) ip = 46;
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '3'";
                }
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES LAREDO"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '8'";
                }
                else if ((marca == "CHEVROLET") && (agencia == "ALLENDE")) ip = 84;
                else if ((marca == "CHEVROLET") && (agencia == "UNIVERSIDAD")) ip = 46;
                else
                {
                    ///// esto es por que no es global el sistema y no tengo el correo del vendedor deje en comentario el código
                    error = "no es sistema global";
                    return false;
                    ///////////////////////////////////////////

                    /*if (show_minempresas(marca, agencia))
                    {
                        min = dt2.Rows[0][0].ToString();
                        elodbc = "dsn=" + dt2.Rows[0][1].ToString() + ";";
                        dt2.Dispose();
                        dt2 = null;
                    }
                    else
                    {
                        error = "Error: no conecto minempresas";
                        dt2.Dispose();
                        dt2 = null;
                        return false;
                    }
                    if (!conectar()) return false;
                    try
                    {
                        OdbcCommand sqlCmd = new OdbcCommand(envolvente3(quien, Email_vendedor, marca, agencia), conn);
                        OdbcDataAdapter sqlDa = new OdbcDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        sqlCmd.Dispose();
                        sqlCmd = null;
                        sqlDa.Dispose();
                        sqlDa = null;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        return false;
                    }*/
                }

                if (ip > 0)
                {
                    if (conectar2(ip))
                    {
                        try
                        {
                            String sql1 = "", invents = "";
                            string[] invents3 = new string[10];
                            int yo, indice = 0;

                            string[] peda = invords.Split(',');
                            invents3[0] = "";
                            invents3[1] = "";
                            invents3[2] = "";
                            invents3[3] = "";
                            invents3[4] = "";
                            invents3[5] = "";
                            invents3[6] = "";
                            invents3[7] = "";
                            invents3[8] = "";
                            invents3[9] = "";

                            if (quien == "venta")
                            {
                                for (yo = 0; yo < peda.Length; yo++) invents += "INVENTARIO like '" + peda[yo].Replace(" ", "% %") + "' OR ";

                                if (invents != "") invents = invents.Substring(0, (invents.Length - 3));
                                sql1 = "select INVENTARIO from AUTOS.VT_VLIBRODEVENTAS where NOMVENDEDOR='" + asesor + "' AND (" + invents + ")";
                            }
                            else
                            {
                                for (yo = 0; yo < peda.Length; yo++)
                                {
                                    invents3[indice] += "'" + peda[yo].ToString() + "',";
                                    if ((yo == 900) || (yo == 1800) || (yo == 2700) || (yo == 3600) || (yo == 4500) || (yo == 5400) || (yo == 6300) || (yo == 7200) || (yo == 8100))
                                    {
                                        if (invents3[indice] != "") invents3[indice] = invents3[indice].Substring(0, (invents3[indice].Length - 1));
                                        sql1 += "select FASE_ORSE_FOLIO from AUTOS.SE_VVENTASSERVICIOS_ARTURS where NOMBRE='" + asesor + "'" + filtro + " AND FASE_ORSE_FOLIO IN (" + invents3[indice] + ") UNION ALL ";
                                        indice++;
                                    }
                                }

                                if (invents3[indice] != "") invents3[indice] = invents3[indice].Substring(0, (invents3[indice].Length - 1));
                                sql1 += "select FASE_ORSE_FOLIO from AUTOS.SE_VVENTASSERVICIOS_ARTURS where NOMBRE='" + asesor + "'" + filtro + " AND FASE_ORSE_FOLIO IN (" + invents3[indice] + ")";

                            }

                            OracleCommand cmd = new OracleCommand(sql1, cnx);
                            OracleDataAdapter Da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            Da.Fill(dt);
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            cmd.Dispose();
                            cmd = null;
                            Da.Dispose();
                            Da = null;
                            return true;
                        }
                        catch (Exception ex)
                        {
                            error = "Error: Excepción1 " + ex.ToString();
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            return false;
                        }
                    }
                    else
                    {
                        error = "Error: No Abrió";
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        public bool conexion10(String quien, String marca, String agencia, String fec_ini, String fec_fin, String vendedor_asesor, String anio)
        {
            /*if (vendedor_asesor != "")
            {
                vendedor_asesor = vendedor_asesor;
            }*/

            int ip = 0;
            string filtro = "";
            try
            {
                if (marca == "MERCEDES BENZ") return false;

                if (marca == "NISSAN")
                {
                    segmento = "10";

                    if (agencia == "SAN JERONIMO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "SAN JERONIMO LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "LAS AGUILAS")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "TLALPAN")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "TLALPAN LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "LA JOYA")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "CROSSING")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "LA JOYA LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='8'";
                    }
                    else
                    {
                        return true;
                        /***  no tengo webservice para los datos del cliente en forma de lista solo de uno en uno
                       String cambio2;

                        if ((agencia == "MANTE") || (agencia == "VALLES"))
                        {   
                            golfoservicio2.ServicioSiaClient cliente2 = new golfoservicio2.ServicioSiaClient();
                            lanissan = cliente2.enc2(quien, marca, agencia, data);
                            return true;
                        }
                        else if ((agencia == "TUXPAN") || (agencia == "POZA RICA"))
                        {
                            if (agencia == "POZA RICA") cambio2 = "POZA";
                            else cambio2 = agencia;

                            golfoservicio4.ServicioSiaClient cliente4 = new golfoservicio4.ServicioSiaClient();
                            lanissan = cliente4.enc2(quien, marca, cambio2, data);
                            return true;
                        }
                       **********/
                    }
                }
                else if (marca == "JETOUR")
                {
                    segmento = "10";

                    if (agencia == "SALTILLO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='20'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='21'";
                    }
                    else if (agencia == "LAS TORRES LAREDO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='28'";
                    }
                }
                else if (marca == "GEELY")
                {
                    segmento = "10";

                    if (agencia == "SENDERO")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "CONTRY")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "CONCORDIA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='7'";
                    }
                }
                else if (marca == "GREAT WALL")
                {
                    segmento = "10";

                    if (agencia == "SENDERO")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "SENDERO FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "GONZALITOS")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "GONZALITOS FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "OMODA")
                {
                    segmento = "10";

                    if (agencia == "LINDA VISTA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "GONZALITOS")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "GONZALITOS LAREDO")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "KIA")
                {
                    if (agencia == "GONZALITOS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "GONZALITOS FRONTERA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "FLETEROS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA FRONTERA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "REYNOSA") ip = 173;
                    else if (agencia == "LAREDO") ip = 78;
                    else if (agencia == "SANTA CATARINA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "LA FE")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='11'";
                    }
                }
                else if (marca == "MG")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='10'";
                    }
                    else if (agencia == "LINDA VISTA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='29'";
                    }
                    else if (agencia == "CONSTITUYENTES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='31'";
                    }
                    else if (agencia == "CONSTITUYENTES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='40'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "LAS TORRES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='33'";
                    }
                    else if (agencia == "TEC")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='37'";
                    }
                    else if (agencia == "VALLE")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='11'";
                    }
                    else if (agencia == "VALLE LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='38'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='23'";
                    }
                    else if (agencia == "JURIQUILLA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='39'";
                    }
                    else if (agencia == "LOMAS VERDES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='14'";
                    }
                    else if (agencia == "LOMAS VERDES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='34'";
                    }
                    else if (agencia == "TIJUANA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='19'";
                    }
                    else if (agencia == "TIJUANA SUCURSAL")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='17'";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='12'";
                    }
                    else if (agencia == "CORREGIDORA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='30'";
                    }
                    else if (agencia == "QUERETARO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='13'";
                    }
                    else if (agencia == "LOPEZ MATEOS")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='16'";
                    }
                    else if (agencia == "LOPEZ MATEOS LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='35'";
                    }
                    else if (agencia == "LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='9' and AGEN_IDAGENCIA='15'";
                    }
                    else if (agencia == "MEXICALI")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='12' and AGEN_IDAGENCIA='18'";
                    }
                    else if (agencia == "LINARES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='26'";
                    }
                    else if (agencia == "TOREO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='41'";
                    }
                }
                else if (marca == "CHIREY")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "FLETEROS")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "FLETEROS LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "CORREGIDORA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "JURIQUILLA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='8'";
                    }
                }
                else if (marca == "CHANGAN")
                {
                    segmento = "10";

                    if (agencia == "CORREGIDORA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='5'";
                    }
                }
                else if (marca == "CHRYSLER")
                {
                    if (agencia == "CUMBRES")
                    {
                        ip = 75;
                    }
                    else if (agencia == "SANTA LUCIA")
                    {
                        segmento = "11";
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "SANTA LUCIA LAREDO")
                    {
                        segmento = "10";
                        ip = 27;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='32'";
                    }
                    else if (agencia == "P.ABASTOS")
                    {
                        ip = 79;
                    }
                    else if (agencia == "LINARES")
                    {
                        ip = 79;
                    }
                    else if (agencia == "CONTRY")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "CONTRY LAREDO")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='36'";
                    }
                }
                else if (marca == "FORD")
                {
                    if (agencia == "VALLE")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "VALLE LAREDO")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "VASCONCELOS")
                    {
                        ip = 164;
                    }
                }
                else if ((marca == "FIAT") && (agencia == "CUMBRES")) ip = 75;
                else if (marca == "MITSUBISHI")
                {
                    if (agencia == "SANTA LUCIA")
                    {
                        ip = 79;
                        //filtro = " and EMPR_EMPRESAID = '14' and AGEN_IDAGENCIA = '4' and AGEN_NOMAGENCIA = 'CARONEMOTORS'";
                        filtro = " and EMPR_EMPRESAID = '14' and AGEN_IDAGENCIA = '4'";
                    }
                    else if (agencia == "SALTILLO")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5'";
                        //filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5' and AGEN_NOMAGENCIA = ''";
                    }
                }
                else if ((marca == "ALFA ROMEO") && (agencia == "CONTRY")) ip = 98;
                else if ((marca == "CHEVROLET") && (agencia == "TLALPAN")) ip = 38;
                else if ((marca == "CHEVROLET") && (agencia == "LAS BOMBAS")) ip = 82;
                else if ((marca == "CHEVROLET") && (agencia == "RUIZ CORTINES")) ip = 159;
                else if ((marca == "CHEVROLET") && (agencia == "NOGALAR")) ip = 160;
                else if ((marca == "CHEVROLET") && (agencia == "CONCORDIA")) ip = 46;
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '3'";
                }
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES LAREDO"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '8'";
                }
                else if ((marca == "CHEVROLET") && (agencia == "ALLENDE")) ip = 84;
                else if ((marca == "CHEVROLET") && (agencia == "UNIVERSIDAD")) ip = 46;
                else
                {
                    if (show_minempresas(marca, agencia))
                    {
                        min = dt2.Rows[0][0].ToString();
                        elodbc = "dsn=" + dt2.Rows[0][1].ToString() + ";";
                        dt2.Dispose();
                        dt2 = null;
                    }
                    else
                    {
                        error = "Error: no conecto minempresas";
                        dt2.Dispose();
                        dt2 = null;
                        return false;
                    }
                    if (!conectar()) return false;
                    try
                    {
                        string sql1 = "";

                        if (quien == "venta") sql1 = "SELECT PERIODO,EJERCICIO,sum(DEBITOS) DEBITOS  FROM QUEST_ESTADISTICOS WHERE ORIGEN='VENTAS AUTOS NUEVOS' AND CLASE='MENUDEO' AND EJERCICIO=" + anio + " group by PERIODO,EJERCICIO";
                        else if (quien == "postventa") sql1 = "SELECT PERIODO,EJERCICIO,sum(DEBITOS) DEBITOS FROM QUEST_ESTADISTICOS WHERE ORIGEN='VENTAS TALLER' AND CLASE IN ('PUBLICO','GARANTIA') AND EJERCICIO=" + anio + " group by PERIODO,EJERCICIO";

                        OdbcCommand sqlCmd = new OdbcCommand(sql1, conn);
                        OdbcDataAdapter sqlDa = new OdbcDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        sqlCmd.Dispose();
                        sqlCmd = null;
                        sqlDa.Dispose();
                        sqlDa = null;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        return false;
                    }
                }

                if (ip > 0)
                {
                    if (conectar2(ip))
                    {
                        try
                        {
                            String sql1 = "", vendedor_asesor_var;
                            ////String invent = data.Replace(" ", "% %");

                            if (vendedor_asesor == "") vendedor_asesor_var = "";
                            else
                            {
                                if (quien == "venta") vendedor_asesor_var = "and NOMVENDEDOR like '%" + vendedor_asesor + "%' ";
                                else vendedor_asesor_var = "and NOMBRE like '%" + vendedor_asesor + "%' ";
                            }

                            if (marca == "KIA")
                            {
                                if (agencia == "GONZALITOS" || agencia == "SANTA CATARINA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null " + vendedor_asesor_var + "and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "" + filtro + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "" + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "" + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "LINDA VISTA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null " + vendedor_asesor_var + "and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "" + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "REYNOSA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null " + vendedor_asesor_var + "and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "LAREDO")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null " + vendedor_asesor_var + "and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }
                            if (marca == "MG")
                            {
                                if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null " + vendedor_asesor_var + "and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "" + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";

                                /*if (agencia == "LINDA VISTA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null " + vendedor_asesor_var + "and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "" + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "LINDA VISTA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null " + vendedor_asesor_var + "and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "" + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }*/
                            }
                            else if (marca == "CHRYSLER")
                            {
                                if (agencia == "CUMBRES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and MARC_DESCRIP!='FIAT' and FAAU_FECHAENTREGA is not null " + vendedor_asesor_var + "and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='2' and AGEN_IDAGENCIA = '1' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') " + vendedor_asesor_var + "AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES') and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') " + vendedor_asesor_var + "AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES')AND MARC_DESCRIP <>'FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA != '4' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "CONTRY")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') " + vendedor_asesor_var + "AND FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') " + vendedor_asesor_var + "AND MARC_DESCRIP <> 'FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA != '4' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "P.ABASTOS")
                                {
                                    if (quien == "venta") return false;
                                    else return false;
                                }
                                else if (agencia == "LINARES")
                                {
                                    if (quien == "venta") sql1 = "";
                                    else sql1 = "";
                                }
                            }
                            else if (marca == "FORD")
                            {
                                if (agencia == "VALLE")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='VALLE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='VALLE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '1' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "VASCONCELOS")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='VASCONCELOS' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='VASCONCELOS' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '2' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }
                            else if (marca == "FIAT")
                            {
                                if (agencia == "CUMBRES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and MARC_DESCRIP='FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin;
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='2' and AGEN_IDAGENCIA = '2' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin;
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    if (quien == "venta") return false;
                                    else return false;
                                }

                                /*if (agencia == "CUMBRES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='FNU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') " + vendedor_asesor_var + "AND AGEN_NOMAGENCIA IN ('STALUCIA') AND MARC_DESCRIP ='FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }*/
                            }
                            else if ((marca == "MITSUBISHI") && (agencia == "SANTA LUCIA"))
                            {
                                if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='MNU' and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4' and AGEN_NOMAGENCIA='CARONEMOTORS' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') " + vendedor_asesor_var + "AND EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4' and AGEN_NOMAGENCIA='CARONEMOTORS' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='4' and AGEN_NOMAGENCIA='CARONEMOTORS' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                            }
                            else if ((marca == "MITSUBISHI") && (agencia == "SALTILLO"))
                            {
                                if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='MNU' and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') " + vendedor_asesor_var + "AND EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                            }
                            else if (marca == "CHEVROLET")
                            {
                                if (agencia == "TLALPAN")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "LAS BOMBAS")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else return false;
                                }
                                else if (agencia == "RUIZ CORTINES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '5' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "NOGALAR")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='NOGALAR' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='NOGALAR' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '7' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "CONCORDIA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='CONCORDIA' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='CONCORDIA' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else return false;
                                }
                                else if (agencia == "LAS TORRES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='LAS TORRES' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='LAS TORRES' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '3' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "ALLENDE")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='ALLENDE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and AGEN_NOMAGENCIA='ALLENDE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '4' and TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "UNIVERSIDAD")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }
                            else if (marca == "NISSAN")
                            {
                                if (agencia == "SAN JERONIMO")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                if (agencia == "LAS AGUILAS")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) between " + fec_ini + " and " + fec_fin + " union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' " + vendedor_asesor_var + "and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ")  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' " + vendedor_asesor_var + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) between " + fec_ini + " and " + fec_fin + ") group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }

                            OracleCommand cmd = new OracleCommand(sql1, cnx);
                            OracleDataAdapter Da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            Da.Fill(dt);
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            cmd.Dispose();
                            cmd = null;
                            Da.Dispose();
                            Da = null;
                            return true;
                        }
                        catch (Exception ex)
                        {
                            error = "Error: Excepción1 " + ex.ToString();
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            return false;
                        }
                    }
                    else
                    {
                        error = "Error: No Abrió";
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

            public bool conexion11(String quien, String marca, String agencia)
        {
            int ip = 0;
            string filtro = "";
            try
            {
                if (marca == "MERCEDES BENZ") return false;

                if (marca == "NISSAN")
                {
                    segmento = "10";

                    if (agencia == "SAN JERONIMO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "SAN JERONIMO LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "LAS AGUILAS")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "TLALPAN")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "TLALPAN LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "LA JOYA")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "CROSSING")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "LA JOYA LAREDO")
                    {
                        ip = 85;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='8'";
                    }
                    else
                    {
                        return true;
                        /***  no tengo webservice para los datos del cliente en forma de lista solo de uno en uno
                       String cambio2;

                        if ((agencia == "MANTE") || (agencia == "VALLES"))
                        {   
                            golfoservicio2.ServicioSiaClient cliente2 = new golfoservicio2.ServicioSiaClient();
                            lanissan = cliente2.enc2(quien, marca, agencia, data);
                            return true;
                        }
                        else if ((agencia == "TUXPAN") || (agencia == "POZA RICA"))
                        {
                            if (agencia == "POZA RICA") cambio2 = "POZA";
                            else cambio2 = agencia;

                            golfoservicio4.ServicioSiaClient cliente4 = new golfoservicio4.ServicioSiaClient();
                            lanissan = cliente4.enc2(quien, marca, cambio2, data);
                            return true;
                        }
                       **********/
                    }                    
                }
                else if (marca == "JETOUR")
                {
                    segmento = "10";

                    if (agencia == "SALTILLO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='20'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='21'";
                    }
                    else if (agencia == "LAS TORRES LAREDO")
                    {
                        ip = 3;
                        filtro = " and EMPR_EMPRESAID='21' and AGEN_IDAGENCIA='28'";
                    }
                }
                else if (marca == "GEELY")
                {
                    segmento = "10";

                    if (agencia == "SENDERO")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "CONTRY")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='20' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "CONCORDIA")
                    {
                        ip = 13;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='7'";
                    }
                }
                else if (marca == "GREAT WALL")
                {
                    segmento = "10";

                    if (agencia == "SENDERO")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "SENDERO FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "GONZALITOS")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "GONZALITOS FRONTERA")
                    {
                        ip = 16;
                        filtro = " and EMPR_EMPRESAID='18' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "OMODA")
                {
                    segmento = "10";

                    if (agencia == "LINDA VISTA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "GONZALITOS")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "GONZALITOS LAREDO")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='17' and AGEN_IDAGENCIA='3'";
                    }
                }
                else if (marca == "KIA")
                {
                    if (agencia == "GONZALITOS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "GONZALITOS FRONTERA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "FLETEROS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA FRONTERA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "REYNOSA") ip = 173;
                    else if (agencia == "LAREDO") ip = 78;
                    else if (agencia == "SANTA CATARINA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "LA FE")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='11'";
                    }
                }
                else if (marca == "MG")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='10'";
                    }
                    else if (agencia == "LINDA VISTA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='29'";
                    }
                    else if (agencia == "CONSTITUYENTES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='31'";
                    }
                    else if (agencia == "CONSTITUYENTES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='40'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "LAS TORRES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='33'";
                    }
                    else if (agencia == "TEC")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='37'";
                    }
                    else if (agencia == "VALLE")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='11'";
                    }
                    else if (agencia == "VALLE LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='38'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='23'";
                    }
                    else if (agencia == "JURIQUILLA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='39'";
                    }
                    else if (agencia == "LOMAS VERDES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='14'";
                    }
                    else if (agencia == "LOMAS VERDES LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='34'";
                    }
                    else if (agencia == "TIJUANA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='19'";
                    }
                    else if (agencia == "TIJUANA SUCURSAL")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='17'";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='12'";
                    }
                    else if (agencia == "CORREGIDORA LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='30'";
                    }
                    else if (agencia == "QUERETARO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='13'";
                    }
                    else if (agencia == "LOPEZ MATEOS")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='16'";
                    }
                    else if (agencia == "LOPEZ MATEOS LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='35'";
                    }
                    else if (agencia == "LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='9' and AGEN_IDAGENCIA='15'";
                    }
                    else if (agencia == "MEXICALI")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='12' and AGEN_IDAGENCIA='18'";
                    }
                    else if (agencia == "LINARES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='26'";
                    }
                    else if (agencia == "TOREO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='41'";
                    }
                }
                else if (marca == "CHIREY")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "LINDA VISTA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='7'";
                    }
                    else if (agencia == "FLETEROS")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "FLETEROS LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='14' and AGEN_IDAGENCIA='5'";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "CORREGIDORA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='15' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='4'";
                    }
                    else if (agencia == "JURIQUILLA LAREDO")
                    {
                        ip = 253;
                        filtro = " and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='8'";
                    }
                }
                else if (marca == "CHANGAN")
                {
                    segmento = "10";

                    if (agencia == "CORREGIDORA")
                    {
                        ip = 5;
                        filtro = " and EMPR_EMPRESAID='19' and AGEN_IDAGENCIA='5'";
                    }
                }
                else if (marca == "CHRYSLER")
                {
                    if (agencia == "CUMBRES")
                    {
                        ip = 75;
                    }
                    else if (agencia == "SANTA LUCIA")
                    {
                        segmento = "11";
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "SANTA LUCIA LAREDO")
                    {
                        segmento = "10";
                        ip = 27;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='32'";
                    }
                    else if (agencia == "P.ABASTOS")
                    {
                        ip = 79;
                    }
                    else if (agencia == "LINARES")
                    {
                        ip = 79;
                    }
                    else if (agencia == "CONTRY")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='6'";
                    }
                    else if (agencia == "CONTRY LAREDO")
                    {
                        ip = 98;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='36'";
                    }
                }
                else if (marca == "FORD")
                {
                    if (agencia == "VALLE")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "VALLE LAREDO")
                    {
                        ip = 164;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='3'";
                    }
                    else if (agencia == "VASCONCELOS")
                    {
                        ip = 164;
                    }
                }
                else if ((marca == "FIAT") && (agencia == "CUMBRES")) ip = 75;
                else if (marca == "MITSUBISHI")
                {
                    if (agencia == "SANTA LUCIA")
                    {
                        ip = 79;
                        //filtro = " and EMPR_EMPRESAID = '14' and AGEN_IDAGENCIA = '4' and AGEN_NOMAGENCIA = 'CARONEMOTORS'";
                        filtro = " and EMPR_EMPRESAID = '14' and AGEN_IDAGENCIA = '4'";
                    }
                    else if (agencia == "SALTILLO")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5'";
                        //filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5' and AGEN_NOMAGENCIA = ''";
                    }
                }
                else if ((marca == "ALFA ROMEO") && (agencia == "CONTRY")) ip = 98;
                else if ((marca == "CHEVROLET") && (agencia == "TLALPAN")) ip = 38;
                else if ((marca == "CHEVROLET") && (agencia == "LAS BOMBAS")) ip = 82;
                else if ((marca == "CHEVROLET") && (agencia == "RUIZ CORTINES")) ip = 159;
                else if ((marca == "CHEVROLET") && (agencia == "NOGALAR")) ip = 160;
                else if ((marca == "CHEVROLET") && (agencia == "CONCORDIA")) ip = 46;
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '3'";
                }
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES LAREDO"))
                {
                    ip = 84;
                    filtro = " and EMPR_EMPRESAID = '3' and AGEN_IDAGENCIA = '8'";
                }
                else if ((marca == "CHEVROLET") && (agencia == "ALLENDE")) ip = 84;
                else if ((marca == "CHEVROLET") && (agencia == "UNIVERSIDAD")) ip = 46;
                else
                {
                    if (show_minempresas(marca, agencia))
                    {
                        min = dt2.Rows[0][0].ToString();
                        elodbc = "dsn=" + dt2.Rows[0][1].ToString() + ";";
                        dt2.Dispose();
                        dt2 = null;
                    }
                    else
                    {
                        error = "Error: no conecto minempresas";
                        dt2.Dispose();
                        dt2 = null;
                        return false;
                    }
                    if (!conectar()) return false;
                    try
                    {
                        string sql1 = "";

                        //if (quien == "venta") sql1 = "SELECT PERIODO,EJERCICIO,DEBITOS FROM QUEST_ESTADISTICOS WHERE ORIGEN='VENTAS AUTOS NUEVOS' AND CLASE='MENUDEO' AND EJERCICIO=" + DateTime.Now.ToString("yyyy").ToString();
                        //else if (quien == "postventa") sql1 = "SELECT PERIODO,EJERCICIO,DEBITOS FROM QUEST_ESTADISTICOS WHERE ORIGEN='VENTAS TALLER' AND CLASE IN ('PUBLICO','GARANTIA') AND EJERCICIO=" + DateTime.Now.ToString("yyyy").ToString();

                        if (quien == "venta") sql1 = "SELECT PERIODO,EJERCICIO,sum(DEBITOS) DEBITOS  FROM QUEST_ESTADISTICOS WHERE ORIGEN='VENTAS AUTOS NUEVOS' AND CLASE='MENUDEO' AND EJERCICIO=" + DateTime.Now.ToString("yyyy").ToString() + " and PERIODO=" + int.Parse(DateTime.Now.ToString("MM").ToString()) + " group by PERIODO,EJERCICIO";
                        else if (quien == "postventa") sql1 = "SELECT PERIODO,EJERCICIO,sum(DEBITOS) DEBITOS FROM QUEST_ESTADISTICOS WHERE ORIGEN='VENTAS TALLER' AND CLASE IN ('PUBLICO','GARANTIA') AND EJERCICIO=" + DateTime.Now.ToString("yyyy").ToString() + " AND PERIODO=" + int.Parse(DateTime.Now.ToString("MM").ToString()) + " group by PERIODO,EJERCICIO";

                        OdbcCommand sqlCmd = new OdbcCommand(sql1, conn);
                        OdbcDataAdapter sqlDa = new OdbcDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        sqlCmd.Dispose();
                        sqlCmd = null;
                        sqlDa.Dispose();
                        sqlDa = null;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                        conn.Close();
                        conn.Dispose();
                        conn = null;                        
                        return false;
                    }
                }

                if (ip > 0)
                {
                    if (conectar2(ip))
                    {
                        try
                        {
                            String sql1 = "";
                            ////String invent = data.Replace(" ", "% %");

                            if (marca == "KIA")
                            {
                                if (agencia == "GONZALITOS" || agencia == "SANTA CATARINA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD'))  like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD'))  like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD'))  like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "LINDA VISTA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD'))  like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD'))  like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "REYNOSA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "LAREDO")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }
                            if (marca == "MG")
                            {
                                /*if (agencia == "LINDA VISTA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "LINDA VISTA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }*/

                                if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                            }
                            else if (marca == "CHRYSLER")
                            {
                                if (agencia == "CUMBRES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and MARC_DESCRIP!='FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='2' and AGEN_IDAGENCIA = '1' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES') and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES') AND MARC_DESCRIP <>'FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA != '4' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "CONTRY")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') AND FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') AND MARC_DESCRIP <> 'FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA != '4' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "P.ABASTOS")
                                {
                                    if (quien == "venta") return false;
                                    else return false;
                                }
                                else if (agencia == "LINARES")
                                {
                                    if (quien == "venta") sql1 = "";
                                    else sql1 = "";
                                }
                            }
                            else if (marca == "FORD")
                            {
                                if (agencia == "VALLE")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VALLE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VALLE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '1' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "VASCONCELOS")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VASCONCELOS' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VASCONCELOS' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '2' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }
                            else if (marca == "FIAT")
                            {
                                if (agencia == "CUMBRES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and MARC_DESCRIP='FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%'";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='2' and AGEN_IDAGENCIA = '2' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%'";
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    if (quien == "venta") return false;
                                    else return false;
                                }
                            }
                            else if ((marca == "MITSUBISHI") && (agencia == "SANTA LUCIA"))
                            {
                                if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='MNU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU')" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                            }
                            else if ((marca == "MITSUBISHI") && (agencia == "SALTILLO"))
                            {
                                if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='MNU' and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') AND EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                            }
                            else if (marca == "CHEVROLET")
                            {
                                if (agencia == "TLALPAN")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "LAS BOMBAS")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else return false;
                                }
                                else if (agencia == "RUIZ CORTINES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '5' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "NOGALAR")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='NOGALAR' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='NOGALAR' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '7' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "CONCORDIA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='CONCORDIA' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='CONCORDIA' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else return false;
                                }
                                else if (agencia == "LAS TORRES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='LAS TORRES' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='LAS TORRES' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '3' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "ALLENDE")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='ALLENDE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='ALLENDE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '4' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "UNIVERSIDAD")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }
                            else if (marca == "NISSAN")
                            {
                                if (agencia == "SAN JERONIMO")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                if (agencia == "LAS AGUILAS")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }

                            OracleCommand cmd = new OracleCommand(sql1, cnx);
                            OracleDataAdapter Da = new OracleDataAdapter(cmd);
                            dt = null;
                            dt = new DataTable();
                            Da.Fill(dt);
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            cmd.Dispose();
                            cmd = null;
                            Da.Dispose();
                            Da = null;
                            return true;
                        }
                        catch (Exception ex)
                        {
                            error = "Error: Excepción1 " + ex.ToString();
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;                            
                            return false;
                        }
                    }
                    else
                    {
                        error = "Error: No Abrió";                        
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                error = ex.ToString();                
                return false;
            }
        }

        //RESERVA
        /*public bool conexion11(String quien, String marca, String agencia)
        {
            int ip = 0;
            string filtro = "";
            try
            {
                if (marca == "MERCEDES BENZ") return false;

                if (marca == "NISSAN")
                {
                    if (agencia == "SAN JERONIMO")
                    {
                        ip = 133;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (marca == "KIA")
                {
                    if (agencia == "GONZALITOS")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='2'";
                    }
                    else if (agencia == "LINDA VISTA")
                    {
                        ip = 41;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='1'";
                    }
                    else if (agencia == "REYNOSA") ip = 173;
                    else if (agencia == "LAREDO") ip = 78;
                    else if (agencia == "SANTA CATARINA")
                    {
                        ip = 88;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='5'";
                    }
                }
                else if (marca == "MG")
                {
                    if (agencia == "LINDA VISTA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='4' and AGEN_IDAGENCIA='10'";
                    }
                    else if (agencia == "LAS TORRES")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='3' and AGEN_IDAGENCIA='9'";
                    }
                    else if (agencia == "VALLE")
                    {
                        ip = 251;
                        //filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='11' and AGEN_NOMAGENCIA='MG VALLE'";
                        filtro = " and EMPR_EMPRESAID='5' and AGEN_IDAGENCIA='11'";
                    }
                    else if (agencia == "JURIQUILLA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='13' and AGEN_IDAGENCIA='23'";
                    }
                    else if (agencia == "LOMAS VERDES")
                    {
                        ip = 251;
                        //filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='14' and AGEN_NOMAGENCIA='MG LOMAS VER'";
                        filtro = " and EMPR_EMPRESAID='8' and AGEN_IDAGENCIA='14'";
                    }
                    else if (agencia == "TIJUANA")
                    {
                        ip = 251;
                        //filtro = " and EMPR_EMPRESAID='11' and AGEN_IDAGENCIA='17'";
        filtro = " and EMPR_EMPRESAID='11' and (AGEN_IDAGENCIA='17' or AGEN_IDAGENCIA='19')";
                    }
                    else if (agencia == "CORREGIDORA")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='6' and AGEN_IDAGENCIA='12'";
                    }
                    else if (agencia == "LOPEZ MATEOS")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='10' and AGEN_IDAGENCIA='16'";
                    }
                    else if (agencia == "LAREDO")
                    {
                        ip = 251;
                        filtro = " and EMPR_EMPRESAID='9' and AGEN_IDAGENCIA='15'";
                    }
                }
                else if (marca == "CHRYSLER")
                {
                    if (agencia == "CUMBRES") ip = 75;
                    else if (agencia == "SANTA LUCIA") ip = 79;
                    else if (agencia == "P.ABASTOS") ip = 79;
                    else if (agencia == "LINARES") ip = 79;
                    else if (agencia == "CONTRY") ip = 98;
                }
                else if (marca == "FORD")
                {
                    if (agencia == "VALLE") ip = 164;
                    else if (agencia == "VASCONCELOS") ip = 164;
                }
                else if ((marca == "FIAT") && (agencia == "CUMBRES")) ip = 75;
                else if (marca == "MITSUBISHI")
                {
                    if (agencia == "SANTA LUCIA")
                    {
                        ip = 79;
                        //filtro = " and EMPR_EMPRESAID = '14' and AGEN_IDAGENCIA = '4' and AGEN_NOMAGENCIA = 'CARONEMOTORS'";
                        filtro = " and EMPR_EMPRESAID = '14' and AGEN_IDAGENCIA = '4'";
                    }
                    else if (agencia == "SALTILLO")
                    {
                        ip = 79;
                        filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5'";
                        //filtro = " and EMPR_EMPRESAID = '16' and AGEN_IDAGENCIA = '5' and AGEN_NOMAGENCIA = ''";
                    }
                }
                else if ((marca == "ALFA ROMEO") && (agencia == "CONTRY")) ip = 98;
                else if ((marca == "CHEVROLET") && (agencia == "TLALPAN")) ip = 38;
                else if ((marca == "CHEVROLET") && (agencia == "LAS BOMBAS")) ip = 82;
                else if ((marca == "CHEVROLET") && (agencia == "RUIZ CORTINES")) ip = 159;
                else if ((marca == "CHEVROLET") && (agencia == "NOGALAR")) ip = 160;
                else if ((marca == "CHEVROLET") && (agencia == "CONCORDIA")) ip = 160;
                else if ((marca == "CHEVROLET") && (agencia == "LAS TORRES")) ip = 84;
                else if ((marca == "CHEVROLET") && (agencia == "ALLENDE")) ip = 84;
                else if ((marca == "CHEVROLET") && (agencia == "UNIVERSIDAD")) ip = 46;
                else
                {
                    if (show_minempresas(marca, agencia))
                    {
                        min = dt2.Rows[0][0].ToString();
                        elodbc = "dsn=" + dt2.Rows[0][1].ToString() + ";";
                        dt2.Dispose();
                        dt2 = null;
                    }
                    else
                    {
                        error = "Error: no conecto minempresas";
                        dt2.Dispose();
                        dt2 = null;
                        return false;
                    }
                    if (!conectar()) return false;
                    try
                    {
                        string sql1 = "";

                        //if (quien == "venta") sql1 = "SELECT PERIODO,EJERCICIO,DEBITOS FROM QUEST_ESTADISTICOS WHERE ORIGEN='VENTAS AUTOS NUEVOS' AND CLASE='MENUDEO' AND EJERCICIO=" + DateTime.Now.ToString("yyyy").ToString();
                        //else if (quien == "postventa") sql1 = "SELECT PERIODO,EJERCICIO,DEBITOS FROM QUEST_ESTADISTICOS WHERE ORIGEN='VENTAS TALLER' AND CLASE IN ('PUBLICO','GARANTIA') AND EJERCICIO=" + DateTime.Now.ToString("yyyy").ToString();
                        
                        if (quien == "venta") sql1 = "SELECT PERIODO,EJERCICIO,sum(DEBITOS) DEBITOS  FROM QUEST_ESTADISTICOS WHERE ORIGEN='VENTAS AUTOS NUEVOS' AND CLASE='MENUDEO' AND EJERCICIO=" + DateTime.Now.ToString("yyyy").ToString() + " and PERIODO=" + int.Parse(DateTime.Now.ToString("MM").ToString()) + " group by PERIODO,EJERCICIO";
                        else if (quien == "postventa") sql1 = "SELECT PERIODO,EJERCICIO,sum(DEBITOS) DEBITOS FROM QUEST_ESTADISTICOS WHERE ORIGEN='VENTAS TALLER' AND CLASE IN ('PUBLICO','GARANTIA') AND EJERCICIO=" + DateTime.Now.ToString("yyyy").ToString() + " AND PERIODO=" + int.Parse(DateTime.Now.ToString("MM").ToString()) + " group by PERIODO,EJERCICIO";

                        OdbcCommand sqlCmd = new OdbcCommand(sql1, conn);
                        OdbcDataAdapter sqlDa = new OdbcDataAdapter(sqlCmd);
                        dt = new DataTable();
                        sqlDa.Fill(dt);
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        sqlCmd.Dispose();
                        sqlCmd = null;
                        sqlDa.Dispose();
                        sqlDa = null;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        error = ex.ToString();
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        return false;
                    }
                }

                if (ip > 0)
                {
                    if (conectar2(ip))
                    {
                        try
                        {
                            String sql1 = "";
                            ////String invent = data.Replace(" ", "% %");

                            if (marca == "KIA")
                            {
                                if (agencia == "GONZALITOS" || agencia == "SANTA CATARINA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD'))  like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD'))  like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD'))  like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "LINDA VISTA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD'))  like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD'))  like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "REYNOSA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "LAREDO")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }
                            if (marca == "MG")
                            {
                                //if (agencia == "LINDA VISTA")
                                //{
                                    //if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    //else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                //}
                                //else if (agencia == "LINDA VISTA")
                                //{
                                    //if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    //else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + "and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                //}

                                if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                            }
                            else if (marca == "CHRYSLER")
                            {
                                if (agencia == "CUMBRES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and MARC_DESCRIP!='FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='2' and AGEN_IDAGENCIA = '1' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES') and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') AND AGEN_NOMAGENCIA IN ('STALUCIA','LINARES') AND MARC_DESCRIP <>'FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA != '4' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "CONTRY")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') AND FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('CNU','CNU.') AND MARC_DESCRIP <> 'FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA != '4' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "P.ABASTOS")
                                {
                                    if (quien == "venta") return false;
                                    else return false;
                                }
                                else if (agencia == "LINARES")
                                {
                                    if (quien == "venta") sql1 = "";
                                    else sql1 = "";
                                }
                            }
                            else if (marca == "FORD")
                            {
                                if (agencia == "VALLE")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VALLE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VALLE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '1' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "VASCONCELOS")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VASCONCELOS' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='VASCONCELOS' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '2' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }
                            else if (marca == "FIAT")
                            {
                                if (agencia == "CUMBRES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='CNU' and MARC_DESCRIP='FIAT' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%'";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='2' and AGEN_IDAGENCIA = '2' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%'";
                                }
                                else if (agencia == "SANTA LUCIA")
                                {
                                    if (quien == "venta") return false;
                                    else return false;
                                }
                            }
                            else if ((marca == "MITSUBISHI") && (agencia == "SANTA LUCIA"))
                            {
                                if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='MNU'" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU')" + filtro + " and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV'" + filtro + " and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                            }
                            else if ((marca == "MITSUBISHI") && (agencia == "SALTILLO"))
                            {
                                if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='MNU' and EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE IN ('MNU') AND EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='16' and AGEN_IDAGENCIA='5' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                            }
                            else if (marca == "CHEVROLET")
                            {
                                if (agencia == "TLALPAN")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "LAS BOMBAS")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else return false;
                                }
                                else if (agencia == "RUIZ CORTINES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '5' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "NOGALAR")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='NOGALAR' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='NOGALAR' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '7' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "CONCORDIA")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='CONCORDIA' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='CONCORDIA' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else return false;
                                }
                                else if (agencia == "LAS TORRES")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='LAS TORRES' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='LAS TORRES' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '3' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "ALLENDE")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='ALLENDE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and AGEN_NOMAGENCIA='ALLENDE' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where EMPR_EMPRESAID='3' and AGEN_IDAGENCIA = '4' and TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                                else if (agencia == "UNIVERSIDAD")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }
                            else if (marca == "NISSAN")
                            {
                                if (agencia == "SAN JERONIMO")
                                {
                                    if (quien == "venta") sql1 = "select mes,anio,sum(ventas) ventas from (select to_number(SUBSTR(fecha,4,2)) as mes,to_number(SUBSTR(fecha,7,4)) as anio,sum(ventas) ventas from (select to_char(FAAU_FECHA, 'DD/MM/YYYY') fecha,(1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and to_number( to_char( FAAU_FECHA ,'YYYYMMDD' )) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%' union all select to_char(FAAU_FECHACANCELACION, 'DD/MM/YYYY') fecha,(-1) ventas from AUTOS.VT_VLIBRODEVENTAS where VEHI_CLAS_CLAVE='NU' and FAAU_FECHAENTREGA is not null and FORM_GRUPODEVENTADIR IN ('TRADICIONAL','FINANCIERA','FINANCIERA PLANTA','PLAN ONE') and To_number( to_char(FAAU_FECHACANCELACION ,'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by fecha order by anio,mes) group by anio,mes order by anio,mes";
                                    else sql1 = "select mes,anio,sum(ventas) ventas from (select count(agrupar) suma,agrupar,mes,anio,(1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA,4,2)) as mes,to_number(SUBSTR(FASE_FECHA,7,4)) as anio from AUTOS.SE_VVENTASSERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%')  group by agrupar,anio,mes union all select count(agrupar) suma,agrupar,mes,anio,(-1) ventas from (select (trim(FASE_FOLIO) || '-' || trim(FASE_ORSE_FOLIO)) agrupar,to_number(SUBSTR(FASE_FECHA1,4,2)) as mes,to_number(SUBSTR(FASE_FECHA1,7,4)) as anio from AUTOS.SE_VCANCELASERVICIOS_ARTURS where TIPOVTA='SV' and TIOR_DESCRIPCION != 'INTERNO' and to_number(to_char(TO_DATE(FASE_FECHA1),'YYYYMMDD')) like '" + DateTime.Now.ToString("yyyy").ToString() + DateTime.Now.ToString("MM").ToString() + "%') group by agrupar,anio,mes) group by anio,mes order by anio,mes";
                                }
                            }

                            OracleCommand cmd = new OracleCommand(sql1, cnx);
                            OracleDataAdapter Da = new OracleDataAdapter(cmd);
                            dt = new DataTable();
                            Da.Fill(dt);
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            cmd.Dispose();
                            cmd = null;
                            Da.Dispose();
                            Da = null;
                            return true;
                        }
                        catch (Exception ex)
                        {
                            error = "Error: Excepción1 " + ex.ToString();
                            cnx.Close();
                            cnx.Dispose();
                            cnx = null;
                            return false;
                        }
                    }
                    else
                    {
                        error = "Error: No Abrió";
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }*/
















        private bool show_minempresas(String marca, String lugar)
        {
            funciones obj = new funciones();
            try
            {
                if (obj.conexion("cons", "select minepresas,odbc from tagenandroid where marca='" + marca + "' and lugaragen='" + lugar + "'"))
                {
                    if (obj.dt.Rows.Count < 1)
                    {
                        error = "Sin Datos";
                        obj.dt.Dispose();
                        obj.dt = null;
                        obj = null;
                        return false;
                    }
                    else
                    {
                        dt2 = obj.dt.Copy();
                        obj.dt.Dispose();
                        obj.dt = null;
                        obj = null;
                        return true;
                    }
                }
                else
                {
                    error = obj.error;
                    obj.dt.Dispose();
                    obj.error = null;
                    obj = null;
                    return false;
                }
            }
            catch (Exception er)
            {
                error = er.ToString();
                obj = null;
                return false;
            }
        }

        private bool conectar()
        {
            try
            {
                conn = new OdbcConnection();
                conn.ConnectionString = elodbc;
                conn.Open();
                return (conn.State == ConnectionState.Open);
            }
            catch (Exception ex)
            {
                error = "Error: " + ex.ToString();
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                conn = null;
                return false;
            }
        }

        private String envolvente(String quien, String data, String marca, String lugar)
        {
            String[] pedazos = new String[12];
            String[] junks = new String[7];

            if ((marca == "VOLKSWAGEN") || (marca == "SEAT"))
            {
                pedazos[2] = "cl.mail_2,";
                pedazos[4] = "cl.telcontacto,";
                pedazos[8] = "cl.mail_2,";
                pedazos[10] = "cl.telcontacto,";
                junks[2] = "cte.mail_2,";
                junks[4] = "cte.telcontacto,";
            }
            else if (marca == "CHEVROLET")
            {
                pedazos[2] = "' ' AS mail_2,";
                pedazos[4] = "' ' AS telcontacto,";
                pedazos[8] = "";
                pedazos[10] = "";
                junks[2] = "' ' AS mail_2,";
                junks[4] = "' ' AS telcontacto,";
            }


            if (quien == "venta")
            {
                pedazos[1] = "select " +
                "vn.num_exp as Inventario," +
                "c.tomado as Seccion, " +
                "c.numero," +
                "C.SERIE," +
                "C.FECHA AS FechaFactura," +

                "vnmod.nombre as Modelo," +
                "vnver.deno_comer as DenominacionComercial," +
                "vn.ano_modelo as Ano," +
                "vncol.color as Color, " +
                "vn.bastidor as Chasis," +

                "cl.codigo as CODIGO," +
                "(cl.cf1 || ' '||cl.apellido1|| ' '||cl.apellido2)  as cliente," +
                "c.nomfis as Facturado_a," +
                "cl.mail,";

                pedazos[3] = "cl.telefono," +
                "cl.telefonooficina,";

                pedazos[5] = "cl.movil,";

                if ((marca == "CHEVROLET") && (lugar == "UNIVERSIDAD"))
                {
                    pedazos[5] += "' ' AS nextel,";
                }
                else
                {
                    pedazos[5] += "cl.nextel,";
                }

                pedazos[5] +=
                    "c.vendedor as NombreAgente" +
                    " from cabfactu c" +
                    " inner join mapdocs m on c.id=m.mmid" +
                    " inner join ope_vn_fc cf on m.fwid_1=cf.id_operacion" +
                    " inner join vh_vn vn on m.id_vh=vn.id" +
                    " inner join vh_color vncol on vn.id_color=vncol.id" +
                    " inner join vh_vn_compra vnc on vn.id=vnc.id_vn" +
                    " inner join vh_version vnver on vn.id_version=vnver.id" +
                    " inner join vh_modelo vnmod on vnver.id_modelo=vnmod.id" +
                    " inner join pedido_vn_de pde on c.jefeventas=pde.id" +
                    " inner join pedido_vn_de_fin pdef on pde.id=pdef.id_pedido_vn_de" +
                    " inner JOIN pedido_vn_cli pcli on pde.id_pedido_vn = pcli.id_pedido_vn" +
                    " inner  join clientes cl on pcli.cli_id=cl.id" +

                    " where (m.fwcod_3=511 or m.fwcod_3=512)" +
                    " AND C.minempresas = ";

                pedazos[6] = " AND M.ultimo ='T'" +
                    " AND m.fwcod_3 = 511 " +
                    " and vn.num_exp = ";

                pedazos[7] = " GROUP BY" +
                    " C.FECHA, c.numero, C.SERIE, c.tomado,pdef.financiera, vnmod.nombre, cl.codigo, vnver.deno_comer,vn.cvevehi, vn.num_exp, vn.bastidor, vn.ano_modelo,vncol.color,vn.nro_motor, vn.carroceria, cl.telefono, cl.mail, cl.cif, cf.fh_carta_factura, c.nomfis, c.vendedor, vnc.precio_ff, vnc.hbck_precio," +
                    "VNC.hbck_iva_imp, VNC.iva_imp,PDE.precio_neto,PDE.iva_imp,PDE.matri_imp,PDE.total_carretera," +
                    "M.FWCOD_3 ," +
                    "cl.codigo," +
                    "(cl.cf1 || ' '||cl.apellido1|| ' '||cl.apellido2)," +
                    "c.nomfis ," +
                    "cl.mail,";

                pedazos[9] = "cl.telefono," +
                    "cl.telefonooficina,";

                if ((marca == "CHEVROLET") && (lugar == "UNIVERSIDAD"))
                {
                    pedazos[11] = "cl.movil";
                    pedazos[11] += "";
                }
                else
                {
                    pedazos[11] = "cl.movil,";
                    pedazos[11] += "cl.nextel";
                }

                pedazos[11] += " ORDER BY C.FECHA, C.NUMERO";

                String cad = pedazos[0] + pedazos[1] + pedazos[2] + pedazos[3] + pedazos[4] + pedazos[5] + min + pedazos[6] + data + pedazos[7] + pedazos[8] + pedazos[9] + pedazos[10] + pedazos[11];
                return cad;
            }
            else if (quien == "postventa")
            {
                junks[1] = "select " +
                "ht.codigoht," +
                "ht.tag as tipo_orden," +
                "(cte.cf1 || ' '||cte.apellido1|| ' '||cte.apellido2)  as cliente," +
                "cte.mail,";

                junks[3] = "cte.telefono," +
                    "cte.telefonooficina,";


                junks[5] = "cte.movil,";

                if ((marca == "CHEVROLET") && (lugar == "UNIVERSIDAD"))
                {
                    junks[5] += "' ' AS nextel,";
                }
                else
                {
                    junks[5] += "cte.nextel,";
                }

                junks[5] += "ma.ano as Anio," +
                    "ma.bastidor as VIN," +
                    "ma.modelo," +
                    "ma.modtectaller," +
                    "Ma.propanterior," +
                    "ht.recepcionista," +
                    "ht.tipoope as tipo_trabajo," +
                    "ht.fecentrada," +
                    "ht.fecent," +
                    "ht.fecentrega," +
                    "ht.fecfac" +
                    " FROM HTRACABE HT" +
                    " inner join matricul ma on ht.rfnmatricula = ma.id" +
                    " inner join CLIENTES cte on ht.rfnclientes = cte.id" +

                    " where " +
                    "TIPDOC <> 200 and " +
                    "HT.minempresas = ";

                junks[6] = " AND " +
                    "codigoht = ";
                String cad = junks[1] + junks[2] + junks[3] + junks[4] + junks[5] + min + junks[6] + data;
                return cad;
            }

            return "";
        }

        /*private String envolvente(String quien, String data, String marca, String lugar)
        {
            String[] pedazos = new String[12];
            String[] junks = new String[7];

            if ((marca == "VOLKSWAGEN") || (marca == "SEAT"))
            {
                pedazos[2] = "cl.mail_2,";
                pedazos[4] = "cl.telcontacto,";
                pedazos[8] = "cl.mail_2,";
                pedazos[10] = "cl.telcontacto,";
                junks[2] = "cte.mail_2,";
                junks[4] = "cte.telcontacto,";
            }
            else if (marca == "CHEVROLET")
            {
                pedazos[2] = "' ' AS mail_2,";
                pedazos[4] = "' ' AS telcontacto,";
                pedazos[8] = "";
                pedazos[10] = "";
                junks[2] = "' ' AS mail_2,";
                junks[4] = "' ' AS telcontacto,";
            }


            if (quien == "venta")
            {
                pedazos[1] = "select " +
                "vn.num_exp as Inventario," +
                "c.tomado as Seccion, " +
                "c.numero," +
                "C.SERIE," +
                "C.FECHA AS FechaFactura," +

                "vnmod.nombre as Modelo," +
                "vnver.deno_comer as DenominacionComercial," +
                "vn.ano_modelo as Ano," +
                "vncol.color as Color, " +
                "vn.bastidor as Chasis," +

                "cl.codigo as CODIGO," +
                "(cl.cf1 || ' '||cl.apellido1|| ' '||cl.apellido2)  as cliente," +
                "c.nomfis as Facturado_a," +
                "cl.mail,";

                pedazos[3] = "cl.telefono," +
                "cl.telefonooficina,";

                pedazos[5] = "cl.movil,";

                if ((marca == "CHEVROLET") && (lugar == "UNIVERSIDAD"))
                {
                    pedazos[5] += "' ' AS nextel,";
                }
                else
                {
                    pedazos[5] += "cl.nextel,";
                }

                pedazos[5] +=
                    "c.vendedor as NombreAgente" +
                    " from cabfactu c" +
                    " left join mapdocs m on c.id=m.mmid" +
                    " left join ope_vn_fc cf on m.fwid_1=cf.id_operacion" +
                    " left join vh_vn vn on m.id_vh=vn.id" +
                    " left join vh_color vncol on vn.id_color=vncol.id" +
                    " left join vh_vn_compra vnc on vn.id=vnc.id_vn" +
                    " left join vh_version vnver on vn.id_version=vnver.id" +
                    " left join vh_modelo vnmod on vnver.id_modelo=vnmod.id" +
                    " left join pedido_vn_de pde on c.jefeventas=pde.id" +
                    " left join pedido_vn_de_fin pdef on pde.id=pdef.id_pedido_vn_de" +
                    " LEFT JOIN pedido_vn_cli pcli on pde.id_pedido_vn = pcli.id_pedido_vn" +
                    " LEFT  join clientes cl on pcli.cli_id=cl.id" +

                    " where (m.fwcod_3=511 or m.fwcod_3=512)" +
                    " AND C.minempresas = ";

                pedazos[6] = " AND M.ultimo ='T'" +
                    " and vn.num_exp = ";

                pedazos[7] = " GROUP BY" +
                    " C.FECHA, c.numero, C.SERIE, c.tomado,pdef.financiera, vnmod.nombre, cl.codigo, vnver.deno_comer,vn.cvevehi, vn.num_exp, vn.bastidor, vn.ano_modelo,vncol.color,vn.nro_motor, vn.carroceria, cl.telefono, cl.mail, cl.cif, cf.fh_carta_factura, c.nomfis, c.vendedor, vnc.precio_ff, vnc.hbck_precio," +
                    "VNC.hbck_iva_imp, VNC.iva_imp,PDE.precio_neto,PDE.iva_imp,PDE.matri_imp,PDE.total_carretera," +
                    "M.FWCOD_3 ," +
                    "cl.codigo," +
                    "(cl.cf1 || ' '||cl.apellido1|| ' '||cl.apellido2)," +
                    "c.nomfis ," +
                    "cl.mail,";

                pedazos[9] = "cl.telefono," +
                    "cl.telefonooficina,";

                if ((marca == "CHEVROLET") && (lugar == "UNIVERSIDAD"))
                {
                    pedazos[11] = "cl.movil";
                    pedazos[11] += "";
                }
                else
                {
                    pedazos[11] = "cl.movil,";
                    pedazos[11] += "cl.nextel";
                }

                pedazos[11] += " ORDER BY C.FECHA, C.NUMERO";

                String cad = pedazos[0] + pedazos[1] + pedazos[2] + pedazos[3] + pedazos[4] + pedazos[5] + min + pedazos[6] + data + pedazos[7] + pedazos[8] + pedazos[9] + pedazos[10] + pedazos[11];
                return cad;
            }
            else if (quien == "postventa")
            {
                junks[1] = "select " +
                "ht.codigoht," +
                "ht.tag as tipo_orden," +
                "(cte.cf1 || ' '||cte.apellido1|| ' '||cte.apellido2)  as cliente," +
                "cte.mail,";

                junks[3] = "cte.telefono," +
                    "cte.telefonooficina,";


                junks[5] = "cte.movil,";

                if ((marca == "CHEVROLET") && (lugar == "UNIVERSIDAD"))
                {
                    junks[5] += "' ' AS nextel,";
                }
                else
                {
                    junks[5] += "cte.nextel,";
                }

                junks[5] += "ma.ano as Anio," +
                    "ma.bastidor as VIN," +
                    "ma.modelo," +
                    "ma.modtectaller,       " +
                    "Ma.propanterior," +
                    "ht.recepcionista," +
                    "ht.tipoope as tipo_trabajo," +
                    "ht.fecentrada," +
                    "ht.fecent," +
                    "ht.fecentrega," +
                    "ht.fecfac" +
                    " FROM HTRACABE HT" +
                    " left join matricul ma on ht.rfnmatricula = ma.id" +
                    " left join CLIENTES cte on ht.rfnclientes = cte.id" +

                    " where " +
                    "HT.minempresas = ";

                junks[6] = " AND " +
                    "codigoht = ";
                String cad = junks[1] + junks[2] + junks[3] + junks[4] + junks[5] + min + junks[6] + data;
                return cad;
            }

            return "";
        }*/

        private String envolvente2(String quien, String lalista, String marca, String lugar)
        {
            String[] pedazos = new String[12];
            String[] junks = new String[7];

            if ((marca == "VOLKSWAGEN") || (marca == "SEAT"))
            {
                pedazos[2] = "cl.mail_2,";
                pedazos[4] = "cl.telcontacto,";
                pedazos[8] = "cl.mail_2,";
                pedazos[10] = "cl.telcontacto,";
                junks[2] = "cte.mail_2,";
                junks[4] = "cte.telcontacto,";
            }
            else if (marca == "CHEVROLET")
            {
                pedazos[2] = "' ' AS mail_2,";
                pedazos[4] = "' ' AS telcontacto,";
                pedazos[8] = "";
                pedazos[10] = "";
                junks[2] = "' ' AS mail_2,";
                junks[4] = "' ' AS telcontacto,";
            }


            if (quien == "venta")
            {
                pedazos[1] = "select " +
                "vn.num_exp as Inventario," +
                "c.tomado as Seccion, " +
                "c.numero," +
                "C.SERIE," +
                "C.FECHA AS FechaFactura," +

                "vnmod.nombre as Modelo," +
                "vnver.deno_comer as DenominacionComercial," +
                "vn.ano_modelo as Ano," +
                "vncol.color as Color, " +
                "vn.bastidor as Chasis," +

                "cl.codigo as CODIGO," +
                "(cl.cf1 || ' '||cl.apellido1|| ' '||cl.apellido2)  as cliente," +
                "c.nomfis as Facturado_a," +
                "cl.mail,";

                pedazos[3] = "cl.telefono," +
                "cl.telefonooficina,";

                pedazos[5] = "cl.movil,";

                if ((marca == "CHEVROLET") && (lugar == "UNIVERSIDAD"))
                {
                    pedazos[5] += "' ' AS nextel,";
                }
                else
                {
                    pedazos[5] += "cl.nextel,";
                }

                pedazos[5] +=
                    "c.vendedor as NombreAgente" +
                    " from cabfactu c" +
                    " left join mapdocs m on c.id=m.mmid" +
                    " left join ope_vn_fc cf on m.fwid_1=cf.id_operacion" +
                    " left join vh_vn vn on m.id_vh=vn.id" +
                    " left join vh_color vncol on vn.id_color=vncol.id" +
                    " left join vh_vn_compra vnc on vn.id=vnc.id_vn" +
                    " left join vh_version vnver on vn.id_version=vnver.id" +
                    " left join vh_modelo vnmod on vnver.id_modelo=vnmod.id" +
                    " left join pedido_vn_de pde on c.jefeventas=pde.id" +
                    " left join pedido_vn_de_fin pdef on pde.id=pdef.id_pedido_vn_de" +
                    " LEFT JOIN pedido_vn_cli pcli on pde.id_pedido_vn = pcli.id_pedido_vn" +
                    " LEFT  join clientes cl on pcli.cli_id=cl.id" +

                    " where (m.fwcod_3=511 or m.fwcod_3=512)" +
                    " AND C.minempresas = ";

                pedazos[6] = " AND M.ultimo ='T'" +
                    " and vn.num_exp in (";

                pedazos[7] = ") GROUP BY" +
                    " C.FECHA, c.numero, C.SERIE, c.tomado,pdef.financiera, vnmod.nombre, cl.codigo, vnver.deno_comer,vn.cvevehi, vn.num_exp, vn.bastidor, vn.ano_modelo,vncol.color,vn.nro_motor, vn.carroceria, cl.telefono, cl.mail, cl.cif, cf.fh_carta_factura, c.nomfis, c.vendedor, vnc.precio_ff, vnc.hbck_precio," +
                    "VNC.hbck_iva_imp, VNC.iva_imp,PDE.precio_neto,PDE.iva_imp,PDE.matri_imp,PDE.total_carretera," +
                    "M.FWCOD_3 ," +
                    "cl.codigo," +
                    "(cl.cf1 || ' '||cl.apellido1|| ' '||cl.apellido2)," +
                    "c.nomfis ," +
                    "cl.mail,";

                pedazos[9] = "cl.telefono," +
                    "cl.telefonooficina,";

                if ((marca == "CHEVROLET") && (lugar == "UNIVERSIDAD"))
                {
                    pedazos[11] = "cl.movil";
                    pedazos[11] += "";
                }
                else
                {
                    pedazos[11] = "cl.movil,";
                    pedazos[11] += "cl.nextel";
                }

                pedazos[11] += " ORDER BY C.FECHA, C.NUMERO";

                String cad = pedazos[0] + pedazos[1] + pedazos[2] + pedazos[3] + pedazos[4] + pedazos[5] + min + pedazos[6] + lalista + pedazos[7] + pedazos[8] + pedazos[9] + pedazos[10] + pedazos[11];
                return cad;
            }
            else if (quien == "postventa")
            {
                junks[1] = "select " +
                "ht.codigoht," +
                "ht.tag as tipo_orden," +
                "(cte.cf1 || ' '||cte.apellido1|| ' '||cte.apellido2)  as cliente," +
                "cte.mail,";

                junks[3] = "cte.telefono," +
                    "cte.telefonooficina,";


                junks[5] = "cte.movil,";

                if ((marca == "CHEVROLET") && (lugar == "UNIVERSIDAD"))
                {
                    junks[5] += "' ' AS nextel,";
                }
                else
                {
                    junks[5] += "cte.nextel,";
                }

                junks[5] += "ma.ano as Anio," +
                    "ma.bastidor as VIN," +
                    "ma.modelo," +
                    "ma.modtectaller,       " +
                    "Ma.propanterior," +
                    "ht.recepcionista," +
                    "ht.tipoope as tipo_trabajo," +
                    "ht.fecentrada," +
                    "ht.fecent," +
                    "ht.fecentrega," +
                    "ht.fecfac" +
                    " FROM HTRACABE HT" +
                    " left join matricul ma on ht.rfnmatricula = ma.id" +
                    " left join CLIENTES cte on ht.rfnclientes = cte.id" +

                    " where " +
                    "HT.minempresas = ";

                junks[6] = " AND " +
                    "codigoht in (";
                String cad = junks[1] + junks[2] + junks[3] + junks[4] + junks[5] + min + junks[6] + lalista + ")";
                return cad;
            }

            return "";
        }

        private String envolvente3(String quien, String Email_vendedor, String marca, String lugar)
        {
            String[] pedazos = new String[12];
            String[] junks = new String[7];

            if ((marca == "VOLKSWAGEN") || (marca == "SEAT"))
            {
                pedazos[2] = "cl.mail_2,";
                pedazos[4] = "cl.telcontacto,";
                pedazos[8] = "cl.mail_2,";
                pedazos[10] = "cl.telcontacto,";
                junks[2] = "cte.mail_2,";
                junks[4] = "cte.telcontacto,";
            }
            else if (marca == "CHEVROLET")
            {
                pedazos[2] = "' ' AS mail_2,";
                pedazos[4] = "' ' AS telcontacto,";
                pedazos[8] = "";
                pedazos[10] = "";
                junks[2] = "' ' AS mail_2,";
                junks[4] = "' ' AS telcontacto,";
            }


            if (quien == "venta")
            {
                pedazos[1] = "select " +
                "vn.num_exp as Inventario," +
                "c.tomado as Seccion, " +
                "c.numero," +
                "C.SERIE," +
                "C.FECHA AS FechaFactura," +

                "vnmod.nombre as Modelo," +
                "vnver.deno_comer as DenominacionComercial," +
                "vn.ano_modelo as Ano," +
                "vncol.color as Color, " +
                "vn.bastidor as Chasis," +

                "cl.codigo as CODIGO," +
                "(cl.cf1 || ' '||cl.apellido1|| ' '||cl.apellido2)  as cliente," +
                "c.nomfis as Facturado_a," +
                "cl.mail,";

                pedazos[3] = "cl.telefono," +
                "cl.telefonooficina,";

                pedazos[5] = "cl.movil,";

                if ((marca == "CHEVROLET") && (lugar == "UNIVERSIDAD"))
                {
                    pedazos[5] += "' ' AS nextel,";
                }
                else
                {
                    pedazos[5] += "cl.nextel,";
                }

                pedazos[5] +=
                    "c.vendedor as NombreAgente" +
                    " from cabfactu c" +
                    " left join mapdocs m on c.id=m.mmid" +
                    " left join ope_vn_fc cf on m.fwid_1=cf.id_operacion" +
                    " left join vh_vn vn on m.id_vh=vn.id" +
                    " left join vh_color vncol on vn.id_color=vncol.id" +
                    " left join vh_vn_compra vnc on vn.id=vnc.id_vn" +
                    " left join vh_version vnver on vn.id_version=vnver.id" +
                    " left join vh_modelo vnmod on vnver.id_modelo=vnmod.id" +
                    " left join pedido_vn_de pde on c.jefeventas=pde.id" +
                    " left join pedido_vn_de_fin pdef on pde.id=pdef.id_pedido_vn_de" +
                    " LEFT JOIN pedido_vn_cli pcli on pde.id_pedido_vn = pcli.id_pedido_vn" +
                    " LEFT  join clientes cl on pcli.cli_id=cl.id" +

                    " where (m.fwcod_3=511 or m.fwcod_3=512)" +
                    " AND C.minempresas = ";

                pedazos[6] = " AND M.ultimo ='T'" +
                    " and vn.num_exp = ";

                pedazos[7] = " GROUP BY" +
                    " C.FECHA, c.numero, C.SERIE, c.tomado,pdef.financiera, vnmod.nombre, cl.codigo, vnver.deno_comer,vn.cvevehi, vn.num_exp, vn.bastidor, vn.ano_modelo,vncol.color,vn.nro_motor, vn.carroceria, cl.telefono, cl.mail, cl.cif, cf.fh_carta_factura, c.nomfis, c.vendedor, vnc.precio_ff, vnc.hbck_precio," +
                    "VNC.hbck_iva_imp, VNC.iva_imp,PDE.precio_neto,PDE.iva_imp,PDE.matri_imp,PDE.total_carretera," +
                    "M.FWCOD_3 ," +
                    "cl.codigo," +
                    "(cl.cf1 || ' '||cl.apellido1|| ' '||cl.apellido2)," +
                    "c.nomfis ," +
                    "cl.mail,";

                pedazos[9] = "cl.telefono," +
                    "cl.telefonooficina,";

                if ((marca == "CHEVROLET") && (lugar == "UNIVERSIDAD"))
                {
                    pedazos[11] = "cl.movil";
                    pedazos[11] += "";
                }
                else
                {
                    pedazos[11] = "cl.movil,";
                    pedazos[11] += "cl.nextel";
                }

                pedazos[11] += " ORDER BY C.FECHA, C.NUMERO";

                String cad = pedazos[0] + pedazos[1] + pedazos[2] + pedazos[3] + pedazos[4] + pedazos[5] + min + pedazos[6] + Email_vendedor + pedazos[7] + pedazos[8] + pedazos[9] + pedazos[10] + pedazos[11];
                return cad;
            }
            else if (quien == "postventa")
            {
                junks[1] = "select " +
                "ht.codigoht," +
                "ht.tag as tipo_orden," +
                "(cte.cf1 || ' '||cte.apellido1|| ' '||cte.apellido2)  as cliente," +
                "cte.mail,";

                junks[3] = "cte.telefono," +
                    "cte.telefonooficina,";


                junks[5] = "cte.movil,";

                if ((marca == "CHEVROLET") && (lugar == "UNIVERSIDAD"))
                {
                    junks[5] += "' ' AS nextel,";
                }
                else
                {
                    junks[5] += "cte.nextel,";
                }

                junks[5] += "ma.ano as Anio," +
                    "ma.bastidor as VIN," +
                    "ma.modelo," +
                    "ma.modtectaller,       " +
                    "Ma.propanterior," +
                    "ht.recepcionista," +
                    "ht.tipoope as tipo_trabajo," +
                    "ht.fecentrada," +
                    "ht.fecent," +
                    "ht.fecentrega," +
                    "ht.fecfac" +
                    " FROM HTRACABE HT" +
                    " left join matricul ma on ht.rfnmatricula = ma.id" +
                    " left join CLIENTES cte on ht.rfnclientes = cte.id" +

                    " where " +
                    "HT.minempresas = ";

                junks[6] = " AND " +
                    "codigoht = ";
                String cad = junks[1] + junks[2] + junks[3] + junks[4] + junks[5] + min + junks[6] + Email_vendedor;
                return cad;
            }

            return "";
        }

        /*métodos para kia,chysler,mitsu,fiat,y algunas chevrolet*/

        private bool conectar2(int ip)
        {
            try
            {
                //cns = "Data Source=192.168.11." + ip + ":1521/SISTEMAS;User Id=CARONE;password=CARONE";
                cns = "Data Source=192.168." + segmento + "." + ip + ":1521/SISTEMAS;User Id=CARONE;password=CARONE";
                cnx = new OracleConnection(cns);
                cnx.Open();
                return (cnx.State == ConnectionState.Open);
            }
            catch (Exception ex)
            {
                error = "Error: Excepción2 " + ex.ToString();
                if (cnx.State == ConnectionState.Open) cnx.Close();
                cnx.Dispose();
                cnx = null;
                return false;
            }
        }


        public string conectar3(string elmin,string eldns, string elsql)
        {
            min = elmin;
            elodbc = "dsn=" + eldns + ";";

            if (!conectar()) return "no se pudo conectar";
            try
            {
                try
                {
                    OdbcCommand sqlCmd = new OdbcCommand(elsql, conn);
                    OdbcDataAdapter sqlDa = new OdbcDataAdapter(sqlCmd);
                    dt = new DataTable();
                    sqlDa.Fill(dt);
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                    sqlCmd.Dispose();
                    sqlCmd = null;
                    sqlDa.Dispose();
                    sqlDa = null;
                    return "todo bien";
                }
                catch (Exception ex)
                {
                   return ex.ToString();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
                return ex.ToString();
            }
        }




    }
}

