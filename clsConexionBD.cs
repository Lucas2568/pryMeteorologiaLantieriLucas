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
using Guna.UI2.WinForms;

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
                    string sqlLocalidades = "SELECT id_localidad, id_provincia, nombre_localidad FROM Localidades ORDER BY id_provincia, nombre_localidad";
                    SqlCommand cmdLoc = new SqlCommand(sqlLocalidades, conexion);
                    SqlDataReader drLoc = cmdLoc.ExecuteReader();

                    while (drLoc.Read())
                    {
                        int idLocalidad = Convert.ToInt32(drLoc["id_localidad"]);
                        int idProvincia = Convert.ToInt32(drLoc["id_provincia"]);
                        string nombreLocalidad = drLoc["nombre_localidad"].ToString();

                        // Creamos el nodo de localidad
                        TreeNode nodoLocalidad = new TreeNode(nombreLocalidad);
                        nodoLocalidad.Tag = idLocalidad; // 🔹 Asignamos el ID de la localidad

                        // Agregamos al nodo de provincia correspondiente
                        if (dictProvincias.ContainsKey(idProvincia))
                        {
                            dictProvincias[idProvincia].Nodes.Add(nodoLocalidad);
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

        public void CargarTemperaturas(TreeView treeView, ListView listView, Guna2DateTimePicker dateTimePicker)
        {
            if (treeView == null || listView == null || dateTimePicker == null)
                return;

            // Suscribirse al evento AfterSelect solo una vez
            treeView.AfterSelect += (sender, e) =>
            {
                TreeNode nodo = e.Node;

                // Solo procesar nodos de localidad (hijos)
                if (nodo.Parent == null) return;

                listView.Items.Clear();

                // Verificamos que el nodo tenga un Tag con el id_localidad
                if (nodo.Tag == null)
                {
                    MessageBox.Show("Este nodo no tiene asociado un id_localidad en el Tag.");
                    return;
                }

                int idLocalidad = Convert.ToInt32(nodo.Tag);
                DateTime fechaSeleccionada = dateTimePicker.Value.Date; // Solo la fecha, sin la hora

                try
                {
                    using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                    {
                        conexion.Open();

                        string sql = @"
                SELECT temp_min, temp_max
                FROM Temperaturas
                WHERE id_localidad = @idLoc AND CAST(fecha AS DATE) = @fecha"; // Filtramos por fecha exacta

                        using (SqlCommand cmd = new SqlCommand(sql, conexion))
                        {
                            cmd.Parameters.AddWithValue("@idLoc", idLocalidad);
                            cmd.Parameters.AddWithValue("@fecha", fechaSeleccionada);

                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                // Configuramos el ListView
                                listView.Items.Clear();
                                listView.Columns.Clear();
                                listView.View = View.Details;
                                listView.FullRowSelect = true;
                                listView.GridLines = true;

                                // Definimos columnas
                                listView.Columns.Add("Tipo", 120);
                                listView.Columns.Add("Valor", 80);

                                if (dr.Read()) // Si hay datos
                                {
                                    string tempMin = dr["temp_min"].ToString();
                                    string tempMax = dr["temp_max"].ToString();

                                    ListViewItem itemMin = new ListViewItem("Temp. Mínima");
                                    itemMin.SubItems.Add(tempMin);
                                    listView.Items.Add(itemMin);

                                    ListViewItem itemMax = new ListViewItem("Temp. Máxima");
                                    itemMax.SubItems.Add(tempMax);
                                    listView.Items.Add(itemMax);
                                }
                                else
                                {
                                    MessageBox.Show("No hay temperaturas registradas para esta localidad en la fecha seleccionada.");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar temperaturas: " + ex.Message);
                }
            };
        }
    }
}
