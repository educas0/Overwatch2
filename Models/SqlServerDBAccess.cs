using Microsoft.Extensions.Configuration;
using Overwatch2.Models.Interfaces;
using Overwatch2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Overwatch2.Models.interfaces
{
    public class SqlServerDBAccess : IDBAccess
    {

        #region |||...PROS & CONSTRUCTOR....|||

        private readonly IConfiguration _config;
        private String _cadenaConexion;
        private string PassDB = string.Empty;


        public SqlServerDBAccess(IConfiguration config)
        {
            this._config = config;
            this._cadenaConexion = _config.GetConnectionString("SQLServerConnectionString");
        }






        #endregion

        #region ...C L I E N T E ....
      #region ...LOGIN...
        public Cliente ComprobarLogin(string Nickname, string Password)
        {

            //Conectar con BD

            SqlConnection _connectionBD = new SqlConnection(this._cadenaConexion);
            _connectionBD.Open();


            SqlCommand _selectedUser = new SqlCommand();

            _selectedUser.Connection = _connectionBD;
            _selectedUser.CommandText = "SELECT Nickname, Password FROM dbo.Clientes WHERE Nickname = @Nickname;";
            _selectedUser.Parameters.AddWithValue("@Nickname", Nickname);

            SqlDataReader _resultado = _selectedUser.ExecuteReader();
            //Comprobar que está ese Nickname en BD

            Cliente _cliente = new Cliente();

            while (_resultado.Read())
            {
                _cliente.Nickname = _resultado["Nickname"].ToString();
                PassDB = _resultado["Password"].ToString();
            }

            _connectionBD.Close();

            //Comprobar que Nickname corresponde con la PASSWORD


            //RESPUESTA DE LA BBDD
            if (Nickname.Equals(_cliente.Nickname) && Password.Equals(PassDB))     //Si OK Acceder al INDEX;           Si MAL redirigir al LOGIN
            {
                //Password es OK
                return _cliente;
            }
            else
            {
                return null;
            }
            //return _cliente;

        }




        #endregion

        #region   ...CREATE...
        public bool InsertarCliente(Cliente _logged)
        {

            string Nombre = _logged.Nombre;
            string Apellido = _logged.Apellido;
            string Nickname = _logged.Nickname;
            string Password = _logged.Password;


            try
            {
                //conectar a la BD
                SqlConnection _conexion = new SqlConnection();
                _conexion.ConnectionString = this._cadenaConexion;
                _conexion.Open();


                //Insert contra dbo.Clientes
                //SqlCommand _insertCliente = new SqlCommand();
                //_insertCliente.Connection = _conexion;
                //Query
                SqlCommand _insertCliente = new SqlCommand(
                                                            "INSERT INTO dbo.Clientes" +
                                                            "(" +
                                                            "Nombre," +
                                                            "Apellido," +
                                                            "Nickname," +
                                                            "Password" +
                                                            " ) " +
                                                            " VALUES " +
                                                            " ( " +
                                                            " @Nombre ," +
                                                            " @Apellido ," +
                                                            " @Nickname, " +
                                                            " @Password" +
                                                            " ) "
                                                            , _conexion);

                _insertCliente.Parameters.AddWithValue("@Nombre", Nombre);
                _insertCliente.Parameters.AddWithValue("@Apellido", Apellido);
                _insertCliente.Parameters.AddWithValue("@Nickname", Nickname);
                _insertCliente.Parameters.AddWithValue("@Password", Password);


                int _filasAfectadas = _insertCliente.ExecuteNonQuery();

                _conexion.Close();


                if (_filasAfectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }



            throw new NotImplementedException();
        }
        #endregion

        #region ...VER CLIENTES...

        public List<Cliente> DevolverListaClientes()
        {
            List<Cliente> _clientesADevolver = new List<Cliente>();


            //Crear conexión
            //Query con todo lo que haya en la tabla Clientes



            try
            {
                //conectar a la BD
                SqlConnection _conexion = new SqlConnection();
                _conexion.ConnectionString = this._cadenaConexion;
                _conexion.Open();


                //Insert contra dbo.Clientes
                //SqlCommand _insertCliente = new SqlCommand();
                //_insertCliente.Connection = _conexion;
                //Query
                SqlCommand _selectHeroes = new SqlCommand("SELECT * FROM dbo.Clientes ORDER BY Nickname ASC;", _conexion);

                SqlDataReader _resultado = _selectHeroes.ExecuteReader();

                while (_resultado.Read())
                {
                    _clientesADevolver.Add(new Cliente
                    {
                        Nombre = _resultado["Nombre"].ToString(),
                        Apellido = _resultado["Apellido"].ToString(),
                        Nickname = _resultado["Nickname"].ToString(),
                        Password = _resultado["Password"].ToString(),
                    });
                }
                return _clientesADevolver;
            }
            catch (Exception ex)
            {

            }
            throw new NotImplementedException();
        }

        #endregion



        #region ...ACTUALIZAR
        public Cliente DevuelveCliente_porNickname(string Nickname)
        {

            try
            {

                //conectar a la BD
                SqlConnection _conexion = new SqlConnection();
                _conexion.ConnectionString = this._cadenaConexion;
                _conexion.Open();

                //Query
                SqlCommand _selectedCliente = new SqlCommand("SELECT * FROM dbo.Clientes WHERE Nickname = @Nickname", _conexion);
                _selectedCliente.Parameters.AddWithValue("@Nickname", Nickname);


                SqlDataReader _resultado = _selectedCliente.ExecuteReader();

                Cliente _clienteActualizar = new Cliente();



                while (_resultado.Read())
                {
                    _clienteActualizar.IdCliente = Convert.ToInt32(_resultado["IdCliente"]);
                    _clienteActualizar.Nombre = (string)_resultado["Nombre"];
                    _clienteActualizar.Apellido = (string)_resultado["Apellido"];
                    _clienteActualizar.Nickname = (string)_resultado["Nickname"];
                    _clienteActualizar.Password = (string)_resultado["Password"];

                }

                _conexion.Close();

                return _clienteActualizar;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public bool ActualizarCliente(Cliente _cliente)
        {

            try {
                //conectar a la BD
                SqlConnection _conexion = new SqlConnection();
                _conexion.ConnectionString = this._cadenaConexion;
                _conexion.Open();


                //Query
                /*
             
                SqlCommand _insertCliente = new SqlCommand(
                                                            " UPDATE dbo.Clientes SET " +
                                                            " Nombre = @Nombre, "+
                                                            " Apellido = @Apellido, "+
                                                            " Nickname = @Nickname, "+
                                                            " Password = @Password "+

                                                            " WHERE Nickname = @Nickname "
                                                            , _conexion);
                
                */

                SqlCommand _insertCliente = new SqlCommand();
                _insertCliente.Connection = _conexion;

                //construccion query
                string sql = "";

                sql = "UPDATE dbo.Clientes ";
                sql += " SET ";
                sql += " Nombre = @Nombre, ";
                sql += " Apellido = @Apellido, ";
                sql += " Nickname = @Nickname, ";
                //sql += " Dano = @Dano, ";
                sql += " Password = @Password ";

                sql += " WHERE IdCliente = @IdCliente ";

                _insertCliente.CommandText = sql;

                /*
                 * UPDATE dbo.Clientes SET (Nombre, Apellido, Nickname, Password) VALUES ('Adolfo', 'Hitlerzio', 'Ubermench', '123' )
                 */
                _insertCliente.Parameters.AddWithValue("@Nombre", _cliente.Nombre);
                _insertCliente.Parameters.AddWithValue("@Apellido", _cliente.Apellido);
                _insertCliente.Parameters.AddWithValue("@Nickname", _cliente.Nickname);
                //_insertCliente.Parameters.AddWithValue("@Dano", _cliente.Dano);
                _insertCliente.Parameters.AddWithValue("@Password", _cliente.Password);


                _insertCliente.Parameters.AddWithValue("@IdCliente", _cliente.IdCliente);


                int _filasAfectadas = _insertCliente.ExecuteNonQuery();



                _conexion.Close();


                if (_filasAfectadas > 0)
                {
                    return true;
                }
                else { 
                    return false;
                }

            }catch(Exception ex){
                return false;
            }

            
        }
        #endregion


        #region ...BORRAR...

        public bool BorrarCliente(string Nickname)
        {

            try
            {
                //conectar a la BD
                SqlConnection _conexion = new SqlConnection();
                _conexion.ConnectionString = this._cadenaConexion;
                _conexion.Open();

                //Query     DELETE FROM table_name WHERE condition;
                SqlCommand _selectedCliente = new SqlCommand("DELETE FROM dbo.Clientes  WHERE Nickname = @Nickname", _conexion);
                _selectedCliente.Parameters.AddWithValue("@Nickname", Nickname);


                int _filasAfectadas = _selectedCliente.ExecuteNonQuery();



                if (_filasAfectadas != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }catch (Exception ex){
                return false;
            }
        }

        #endregion


        #region ...Recuperar Datos...


        public Cliente RecuperarCliente(String Nickname)
        {

            try
            {
            SqlConnection _conexion = new SqlConnection(this._cadenaConexion);

            _conexion.Open();


            SqlCommand _comando = new SqlCommand(
                "SELECT * FROM dbo.Clientes WHERE Nickname = @Nickname",
                _conexion);

            _comando.Parameters.AddWithValue("@Nickname", Nickname);

            SqlDataReader _resultado = _comando.ExecuteReader();

            Cliente _cliente = new Cliente();

            while (_resultado.Read())
            {
                _cliente.Nombre = _resultado["Nombre"].ToString();
                _cliente.Nombre = _resultado["Apellido"].ToString();
                _cliente.Nombre = _resultado["Nickname"].ToString();
                _cliente.Nombre = _resultado["Password"].ToString();

            }


            _conexion.Close();


            return _cliente;
            }catch(Exception ex)
            {
                return new Cliente();
            }


        }


        #endregion
        #endregion

        //-----------------------------------------------------------------------------------//

        #region ...H É R O E ....

        #region "Metodos Recuperacion datos"





        public List<Heroe> RecuperarListaHeroes()
        {
            SqlConnection __conexionBD = new SqlConnection(this._cadenaConexion);
            __conexionBD.Open();

            SqlCommand __SelectHeroes = new SqlCommand();
            __SelectHeroes.Connection = __conexionBD;
            __SelectHeroes.CommandText = "SELECT * FROM dbo.Heroe ORDER BY Nombre ASC;";

            SqlDataReader __resultado = __SelectHeroes.ExecuteReader();


            List<Heroe> __listaADevolver = new List<Heroe>();
            while (__resultado.Read())
            {
                __listaADevolver.Add(new Heroe
                {
                    IdHeroe = System.Convert.ToInt32(__resultado["IdHeroe"]),
                    Nombre = __resultado["Nombre"].ToString(),
                    Rol = __resultado["Rol"].ToString(),
                    Vida = System.Convert.ToInt32(__resultado["Vida"]),
                    Dano = System.Convert.ToInt32(__resultado["Dano"]),
                    Cura = System.Convert.ToInt32(__resultado["Cura"]),

                }) ;
            }
            __conexionBD.Close();
            return __listaADevolver;
        }

        public Heroe RecuperarHeroe(int IdHeroe)
        {
            SqlConnection __conexionBD = new SqlConnection(this._cadenaConexion);
            __conexionBD.Open();

            SqlCommand __SelectHeroe = new SqlCommand();
            __SelectHeroe.Connection = __conexionBD;
            __SelectHeroe.CommandText = "SELECT IdHeroe, Nombre, Rol, Vida, Dano, Cura FROM dbo.Heroe WHERE IdHeroe = @IdHeroe;";

            __SelectHeroe.Parameters.AddWithValue("@IdHeroe", IdHeroe);

            SqlDataReader __resultado = __SelectHeroe.ExecuteReader();

            Heroe _Heroe = new Heroe();

            while (__resultado.Read())
            {
                _Heroe.IdHeroe = System.Convert.ToInt32(__resultado["IdHeroe"]);
                _Heroe.Nombre = __resultado["Nombre"].ToString();
                _Heroe.Rol = __resultado["Rol"].ToString();
                _Heroe.Vida = System.Convert.ToInt32(__resultado["Vida"]);
                _Heroe.Cura = System.Convert.ToInt32(__resultado["Cura"]);
                _Heroe.Dano = System.Convert.ToInt32(__resultado["Dano"]);
            }


            __conexionBD.Close();
            return _Heroe;
        }

        #endregion

        #region "Metodos borrado datos"

        public bool BorrarHeroe(int idHeroe)
        {
            try
            {
                //1º conectarnos al servidor y a la BD
                SqlConnection __miconexion = new SqlConnection();
                __miconexion.ConnectionString = this._cadenaConexion;

                __miconexion.Open();

                //2º lanzar comando INSERT sobre tabla dbo.Clientes
                SqlCommand __deleteClientes = new SqlCommand();
                __deleteClientes.Connection = __miconexion;

                //construccion query
                string sql = "";

                sql = "DELETE  FROM dbo.Heroe  WHERE IdHeroe = @IdHeroe ";


                __deleteClientes.CommandText = sql;
                __deleteClientes.Parameters.AddWithValue("@IdHeroe", idHeroe);


                int __filasafectadas = __deleteClientes.ExecuteNonQuery();

                __miconexion.Close();

                if (__filasafectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        #endregion

        #region "Metodos Insercion datos"

        public bool CrearHeroe(Heroe newHero)
        {
            try
            {
                //1º conectarnos al servidor y a la BD
                SqlConnection __miconexion = new SqlConnection();
                __miconexion.ConnectionString = this._cadenaConexion;

                __miconexion.Open();

                //2º lanzar comando INSERT sobre tabla dbo.Clientes
                SqlCommand __insertClientes = new SqlCommand();
                __insertClientes.Connection = __miconexion;

                //construccion query
                string sql = "";

                sql = "INSERT INTO dbo.Heroe ";
                sql += " ( ";
                sql += " Nombre, ";
                sql += " Rol, ";
                sql += " Vida, ";
                sql += " Dano, ";
                sql += " Cura ";
                sql += " ) ";

                sql += " VALUES ";
                sql += " ( ";
                sql += " @Nombre, ";
                sql += " @Rol, ";
                sql += " @Vida, ";
                sql += " @Dano, ";
                sql += " @Cura ";
                sql += " ) ";

                __insertClientes.CommandText = sql;
                __insertClientes.Parameters.AddWithValue("@Nombre", newHero.Nombre);
                __insertClientes.Parameters.AddWithValue("@Rol", newHero.Rol);
                __insertClientes.Parameters.AddWithValue("@Vida", newHero.Vida);
                __insertClientes.Parameters.AddWithValue("@Dano", newHero.Dano);
                __insertClientes.Parameters.AddWithValue("@Cura", newHero.Cura);

                int __filasafectadas = __insertClientes.ExecuteNonQuery();

                __miconexion.Close();

                if (__filasafectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        #endregion

        #region "Metodos Actualizacion datos"

        public bool ActualizarHeroe(Heroe Heroe)
        {
            try
            {
                //1º conectarnos al servidor y a la BD
                SqlConnection __miconexion = new SqlConnection();
                __miconexion.ConnectionString = this._cadenaConexion;

                __miconexion.Open();

                //2º lanzar comando INSERT sobre tabla dbo.Clientes
                SqlCommand __ActualizarHeroe = new SqlCommand();
                __ActualizarHeroe.Connection = __miconexion;

                //construccion query
                string sql = "";

                sql = "UPDATE dbo.Heroe ";
                sql += " SET ";
                sql += " Nombre = @Nombre, ";
                sql += " Rol = @Rol, ";
                sql += " Vida = @Vida, ";
                sql += " Dano = @Dano, ";
                sql += " Cura = @Cura ";

                sql += " WHERE IdHeroe = @IdHeroe ";

                __ActualizarHeroe.CommandText = sql;
                __ActualizarHeroe.Parameters.AddWithValue("@Nombre", Heroe.Nombre);
                __ActualizarHeroe.Parameters.AddWithValue("@Rol", Heroe.Rol);
                __ActualizarHeroe.Parameters.AddWithValue("@Vida", Heroe.Vida);
                __ActualizarHeroe.Parameters.AddWithValue("@Dano", Heroe.Dano);
                __ActualizarHeroe.Parameters.AddWithValue("@Cura", Heroe.Cura);
                __ActualizarHeroe.Parameters.AddWithValue("@IdHeroe", Heroe.IdHeroe);

                int __filasafectadas = __ActualizarHeroe.ExecuteNonQuery();

                __miconexion.Close();

                if (__filasafectadas != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool ActualizarHeroe2(Heroe heroeActualizar)
        {
            try
            {
                //1º conectarnos al servidor y a la BD
                SqlConnection __miconexion = new SqlConnection();
                __miconexion.ConnectionString = this._cadenaConexion;

                __miconexion.Open();

                //2º lanzar comando INSERT sobre tabla dbo.Clientes
                SqlCommand __insertClientes = new SqlCommand();
                __insertClientes.Connection = __miconexion;

                //construccion query
                string sql = "";
                //UPDATE Customers SET ContactName = 'Alfred Schmidt', City = 'Frankfurt' WHERE CustomerID = 1;

                /*
                 * actualiza tablaheroes establece como nombre
                 */
                sql = "Update dbo.Heroe SET  ";
                sql += " ( ";
                sql += " Nombre, ";
                sql += " Rol, ";
                sql += " Vida, ";
                sql += " Dano, ";
                sql += " Cura ";
                sql += " ) ";

                sql += " VALUES ";
                sql += " ( ";
                sql += " @Nombre, ";
                sql += " @Rol, ";
                sql += " @Vida, ";
                sql += " @Dano, ";
                sql += " @Cura ";
                sql += " ) ";
                sql += "WHERE idHeroe = 1";

                __insertClientes.CommandText = sql;
                __insertClientes.Parameters.AddWithValue("@Nombre", heroeActualizar.Nombre);
                __insertClientes.Parameters.AddWithValue("@Rol", heroeActualizar.Rol);
                __insertClientes.Parameters.AddWithValue("@Vida", heroeActualizar.Vida);
                __insertClientes.Parameters.AddWithValue("@Dano", heroeActualizar.Dano);
                __insertClientes.Parameters.AddWithValue("@Cura", heroeActualizar.Cura);

                int __filasafectadas = __insertClientes.ExecuteNonQuery();

                __miconexion.Close();

                if (__filasafectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        #endregion

        #endregion



    }

}
