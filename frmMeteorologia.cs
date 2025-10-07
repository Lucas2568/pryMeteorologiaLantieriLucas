using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryMeteorologiaLantieriLucas
{
    public partial class frmMeteorologia : Form
    {
        public frmMeteorologia()
        {
            InitializeComponent();
        }

        private void frmMeteorologia_Load(object sender, EventArgs e)
        {
            pryMeteorologiaLantieriLucas.clsConexionBD clsConexionBD = new pryMeteorologiaLantieriLucas.clsConexionBD();
            clsConexionBD.ConectarBD();
            clsConexionBD.CargarTreeView(trvUbicaciones);
            // Configurar StatusStrip para mostrar selección
            clsConexionBD.ActualizarStatusStrip(trvUbicaciones, toolStripStatusLabel1);

            // Seleccionar el primer nodo para mostrar por defecto en el StatusStrip
            if (trvUbicaciones.Nodes.Count > 0)
                trvUbicaciones.SelectedNode = trvUbicaciones.Nodes[0];
        }

        private void stsSeleccionado_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
       
        }
    }
}
