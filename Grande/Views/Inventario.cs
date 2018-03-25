﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grande.Model;

namespace Grande.Views
{
    public partial class Inventario : Form
    {
        public Inventario()
        {
            InitializeComponent();
        }

        public void cargarTabla(string texto)
        {
            DataTable dt = DAOProductos.getAllNoDescription(texto);
            if (dt != null)
                dgProductos.DataSource = dt;
            else
                MessageBox.Show("Error al obtener el inventario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Inventario_Load(object sender, EventArgs e)
        {
            cargarTabla("");
        }

        private void dgProductos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int index = dgProductos.CurrentRow.Index;
                string clave = dgProductos[0, index].Value.ToString();
                txtDescripcion.Text = DAOProductos.getDescripcion(clave);
            }
            catch
            {
            }

        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            string a = new RegistroEscaneo().registrar();
            if (a != null)
            {
                new RegistroDatos(a, RegistroDatos.AGREGAR).ShowDialog();
                cargarTabla("");
                dgProductos.Focus();
            }
                
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgProductos.CurrentRow.Index;
            DialogResult dr = MessageBox.Show("¿Seguro deseas eliminar este producto?", "¿Seguro?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                bool a = DAOProductos.eliminarProducto(dgProductos[0, index].Value.ToString());
                if (a)
                    cargarTabla("");
                else
                    MessageBox.Show("Error al borrar el registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int index = dgProductos.CurrentRow.Index;
            new RegistroDatos(dgProductos[0, index].Value.ToString(), RegistroDatos.EDITAR).ShowDialog();
            cargarTabla("");
        }

        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {
            cargarTabla(txtBuscador.Text);
        }
    }
}
