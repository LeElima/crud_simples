using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cadastro
{
    public partial class Pessoas : PageGeneric
    {
        public string strConexao = null;
        public int idPessoa { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            strConexao = "Server=DESKTOP-OFHHILJ;Database=ConfirpTeste;Trusted_Connection=True;";
            if (!IsPostBack)
            {

                CarregarDDLSexo();
                CarregarDDLCargos();
                CarregarGrid();
            }


        }

        private void CarregarGrid()
        {
            var dt = new DataTable();
            var da = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(strConexao);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pessoas_s";

                da.SelectCommand = cmd;
                da.Fill(dt);

                gvPessoas.DataSource = dt;
                gvPessoas.DataBind();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
                cmd.Parameters.Clear();
                cmd.CommandText = String.Empty;
                cmd.Dispose();
                da.Dispose();
                dt.Dispose();
            }
        }

        private void CarregarDDLCargos()
        {

            var dt = new DataTable();
            var da = new SqlDataAdapter();


            SqlConnection con = new SqlConnection(strConexao);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ddlCargos_s";

                da.SelectCommand = cmd;
                da.Fill(dt);


                ddlCargo.DataSource = dt;
                ddlCargo.DataTextField = "texto";
                ddlCargo.DataValueField = "valor";
                ddlCargo.DataBind();
                ddlCargo.Items.Add(new ListItem("Selecione", "-1"));
                ddlCargo.SelectedValue = "-1";

            }
            catch (SqlException ex)
            {

            }
            finally
            {
                con.Close();
                cmd.Parameters.Clear();
                cmd.CommandText = String.Empty;
                cmd.Dispose();
                da.Dispose();
                dt.Dispose();
            }
        }

        private void CarregarDDLSexo()
        {

            var dt = new DataTable();
            var da = new SqlDataAdapter();
            SqlConnection con = new SqlConnection(strConexao);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ddlSexo_s";

                da.SelectCommand = cmd;
                da.Fill(dt);
                ddlSexo.DataSource = dt;
                ddlSexo.DataTextField = "texto";
                ddlSexo.DataValueField = "valor";
                ddlSexo.DataBind();
                ddlSexo.Items.Add(new ListItem("Selecione", "-1"));
                ddlSexo.SelectedValue = "-1";
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
                cmd.Parameters.Clear();
                cmd.CommandText = String.Empty;
                cmd.Dispose();
                da.Dispose();
                dt.Dispose();
            }
        }


        public void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarDados())
                {
                    var dt = new DataTable();
                    var da = new SqlDataAdapter();
                    SqlConnection con = new SqlConnection("Server=DESKTOP-OFHHILJ;Database=ConfirpTeste;Trusted_Connection=True;");
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    try
                    {
                        con.Open();
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "pessoas_i";
                        cmd.Parameters.AddWithValue("@ID_PESSOA", Convert.ToInt32(hidIndex.Value));
                        cmd.Parameters.AddWithValue("@NOME_PESSOA", txtNome.Text);
                        cmd.Parameters.AddWithValue("@IDADE_PESSOA", txtIdade.Text);
                        cmd.Parameters.AddWithValue("@ID_SEXO", ddlSexo.SelectedValue);
                        cmd.Parameters.AddWithValue("@ID_CARGO", ddlCargo.SelectedValue);
                        cmd.ExecuteNonQuery();
                        mdlEditar.Fechar();
                        Notificacao.ToastDadosSalvos();
                        CarregarGrid();
                    }
                    catch (Exception ex)
                    {
                        Notificacao.ToastErro("Erro ao inserir dado!");
                    }
                    finally
                    {
                        con.Close();
                        cmd.Parameters.Clear();
                        cmd.CommandText = String.Empty;
                        cmd.Dispose();
                        da.Dispose();
                        dt.Dispose();

                    }
                }
                else
                {
                    Notificacao.ModalAtencao("Preencha corretamente todos os campos!");
                }
            }
            catch (Exception ex)
            {
                Notificacao.ToastErro("Erro ao inserir dado!");
            }


        }

        private bool ValidarDados()
        {
            var retorno = true;

            if (string.IsNullOrEmpty(txtNome.Text) || string.IsNullOrEmpty(txtIdade.Text) || ddlCargo.SelectedValue == "-1" || ddlSexo.SelectedValue == "-1")
            {

                retorno = false;

            }

            return retorno;
        }

        protected void gvPessoas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editar")
            {
                idPessoa = Convert.ToInt32(e.CommandArgument.ToString());
                var dt = new DataTable();
                var da = new SqlDataAdapter();
                SqlConnection con = new SqlConnection(strConexao);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "pessoas_s";
                    cmd.Parameters.AddWithValue("@ID_PESSOA", idPessoa);
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        var itemArray = dt.Rows[0];
                        hidIndex.Value = itemArray[0].ToString();
                        txtNome.Text = itemArray[1].ToString();
                        txtIdade.Text = itemArray[2].ToString();
                        ddlSexo.SelectedValue = itemArray[3].ToString();
                        ddlCargo.SelectedValue = itemArray[4].ToString();
                        mdlEditar.Abrir();
                    }

                }
                catch (Exception ex)
                {
                    Notificacao.ToastErro("Erro ao abrir modal de editar!");
                }
                finally
                {
                    con.Close();
                    cmd.Parameters.Clear();
                    cmd.CommandText = String.Empty;
                    cmd.Dispose();
                    da.Dispose();
                    dt.Dispose();
                }
            }
            if (e.CommandName == "excluir")
            {
                idPessoa = Convert.ToInt32(e.CommandArgument.ToString());
                var dt = new DataTable();
                var da = new SqlDataAdapter();
                SqlConnection con = new SqlConnection("Server=DESKTOP-OFHHILJ;Database=ConfirpTeste;Trusted_Connection=True;");
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "pessoas_D";
                    cmd.Parameters.AddWithValue("@ID_PESSOA", idPessoa);
                    cmd.ExecuteNonQuery();
                    Notificacao.ToastSucessoExclusao();
                    CarregarGrid();
                }
                catch (Exception ex)
                {
                    Notificacao.ToastErro("Erro ao tentar excluir dado!");
                }
                finally
                {
                    con.Close();
                    cmd.Parameters.Clear();
                    cmd.CommandText = String.Empty;
                    cmd.Dispose();
                    da.Dispose();
                    dt.Dispose();

                }

            }

        }

        protected void gvPessoas_RowDataBound(object sender, GridViewRowEventArgs e)
        {


        }

        protected void lbtnIncluir_Click(object sender, EventArgs e)
        {
            try
            {
                Limpar();
                mdlEditar.Abrir();
            }
            catch (Exception ex)
            {
                Notificacao.ToastErro("Erro ao abrir Modal");
            }
        }

        private void Limpar()
        {
            ddlCargo.SelectedValue = "-1";
            ddlSexo.SelectedValue = "-1";
            txtNome.Text = "";
            txtIdade.Text = "";
            idPessoa = 0;
            hidIndex.Value = "0";
        }
    }
}