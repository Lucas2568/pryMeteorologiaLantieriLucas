using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;
using System.Collections;

namespace pryMeteorologiaLantieriLucas
{
    internal class clsConexionBD
    {
        SqlConnection coneccionBaseDatos;
        string cadenaConexion = "Server=localhost;Database=Meteorologia;Trusted_Connection=True;";
        SqlCommand comandoBaseDatos;
        OleDbDataReader lectorDataReader;
        public string nombreBaseDeDatos;

        public void ConectarBD()
        {
            try
            {
                coneccionBaseDatos = new SqlConnection(cadenaConexion);

                nombreBaseDeDatos = coneccionBaseDatos.DataSource;

                coneccionBaseDatos.Open();

                MessageBox.Show("Conectado a " + nombreBaseDeDatos);
            }
            catch (Exception error)
            {
                MessageBox.Show("Tiene un errorcito - " + error.Message);
            }
        }

        // Método para cargar provincias y localidades en TreeView
        public void CargarTreeView(TreeView treeView)
        {
            if (treeView == null)
            {
                MessageBox.Show("El TreeView no está inicializado.");
                return;
            }

            treeView.Nodes.Clear();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    // 1️⃣ Traer todas las provincias
                    string sqlProvincias = "SELECT id_provincia, nombre_provincia FROM Provincias ORDER BY nombre_provincia";
                    SqlCommand cmdProv = new SqlCommand(sqlProvincias, conexion);
                    SqlDataReader drProv = cmdProv.ExecuteReader();

                    // Guardamos los nodos de provincia en un diccionario temporal para relacionar con las localidades
                    Dictionary<int, TreeNode> dictProvincias = new Dictionary<int, TreeNode>();

                    while (drProv.Read())
                    {
                        int idProvincia = Convert.ToInt32(drProv["id_provincia"]);
                        string nombreProvincia = drProv["nombre_provincia"].ToString();

                        TreeNode nodoProvincia = new TreeNode(nombreProvincia);
                        nodoProvincia.Tag = idProvincia;

                        treeView.Nodes.Add(nodoProvincia);
                        dictProvincias.Add(idProvincia, nodoProvincia);
                    }

                    drProv.Close();

                    // 2️⃣ Traer todas las localidades de una vez
                    string sqlLocalidades = "SELECT id_provincia, nombre_localidad FROM Localidades ORDER BY id_provincia, nombre_localidad";
                    SqlCommand cmdLoc = new SqlCommand(sqlLocalidades, conexion);
                    SqlDataReader drLoc = cmdLoc.ExecuteReader();

                    while (drLoc.Read())
                    {
                        int idProvincia = Convert.ToInt32(drLoc["id_provincia"]);
                        string nombreLocalidad = drLoc["nombre_localidad"].ToString();

                        // Agregamos la localidad al nodo de su provincia
                        if (dictProvincias.ContainsKey(idProvincia))
                        {
                            dictProvincias[idProvincia].Nodes.Add(new TreeNode(nombreLocalidad));
                        }
                    }

                    drLoc.Close();
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el TreeView: " + ex.Message);
            }
        }
        // Configurar el StatusStrip para que muestre provincia y localidad seleccionada
        public void ActualizarStatusStrip(TreeView treeView, ToolStripStatusLabel statusLabel)
        {
            if (treeView == null || statusLabel == null)
                return;

            void Actualizar(TreeNode nodo)
            {
                string provincia = "-";
                string localidad = "-";

                if (nodo != null)
                {
                    if (nodo.Parent == null)
                        provincia = nodo.Text;
                    else
                    {
                        localidad = nodo.Text;
                        provincia = nodo.Parent.Text;
                    }
                }

                statusLabel.Text = $"Provincia: {provincia} | Localidad: {localidad}";
            }

            // Suscribimos el evento AfterSelect
            treeView.AfterSelect += (sender, e) => Actualizar(e.Node);

            // Mostrar la primera selección por defecto
            if (treeView.Nodes.Count > 0)
                treeView.SelectedNode = treeView.Nodes[0]; // Esto disparará AfterSelect
        }

    }
}
