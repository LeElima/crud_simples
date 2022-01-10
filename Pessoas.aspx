<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pessoas.aspx.cs" Inherits="Cadastro.Pessoas" %>
<%@ Register Src="~/App_Global/Componentes/ModalGenerico.ascx" TagPrefix="uc1" TagName="ModalGenerico" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <asp:Panel runat="server" ID="pnlPrincipal">
            <div class="panel">
                <div class="panel-heading">Controle de Pessoas</div>
                <div class="panel-body">
                    <div class="row form-group">
                        <div class="col-md-12">
                            <asp:LinkButton runat="server" ID="lbtnIncluir" Text="Incluir" CssClass="btn btn-primary create" OnClick="lbtnIncluir_Click"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="gvPessoas" CssClass="table table-bordered table-striped table-condesed table-hover mb-0" ShowHeader="true"
                            ShowHeaderWhenEmpty="true" EmptyDataText="<center>Nenhum registro encontrado!</center>" OnRowCommand="gvPessoas_RowCommand" OnRowDataBound="gvPessoas_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Nº">
                                <ItemTemplate>  <%# Container.DataItemIndex + 1 %></ItemTemplate>
                                <ItemStyle CssClass="text-right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nome">
                                <ItemTemplate><%#Eval("nomePessoa") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Idade">
                                <ItemTemplate><%#Eval("idadePessoa") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sexo">
                                <ItemTemplate><%#Eval("Sexo") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cargo">
                                <ItemTemplate><%#Eval("Cargo") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEditar" runat="server" CssClass="btn btn-success btn-sm fa fa-pencil editar" CommandName="editar" data-toggle="tooltip" ToolTip="Editar" CommandArgument='<%# Eval("idPessoa")%>'></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnExcluir" runat="server" CssClass="btn btn-danger btn-sm fa fa-trash" CommandName="excluir" data-toggle="tooltip" ToolTip="Excluir" CommandArgument='<%# Eval("idPessoa")%>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="text-center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    </div>
                    
                </div>
               
            </div>
            <cob:Modal id="mdlEditar" runat="server"  data-validar-panel="true" DialogCssClass="modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Cadastrar Pessoas</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hidIndex" runat="server" />
                    <div class="row">
                        <div class="form-group col-md-4">
                            <asp:Label ID="lblNome" runat="server" CssClass="col-form-label" Text="Nome:* " ClientIDMode="Static"></asp:Label>
                            <asp:TextBox ID="txtNome" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-2">
                            <asp:Label ID="lblIdade" runat="server" CssClass="col-form-label" Text="Idade:* " ClientIDMode="Static"></asp:Label>
                            <asp:TextBox ID="txtIdade" runat="server" CssClass="form-control" TextMode="Number" MaxLength="3"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-3">
                            <asp:Label ID="lblSexo" runat="server" CssClass="col-form-label" Text="Sexo:* " ClientIDMode="Static"></asp:Label>
                            <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group col-md-3">
                            <asp:Label ID="lblCargo" runat="server" CssClass="col-form-label" Text="Cargo:* " ClientIDMode="Static"></asp:Label>
                            <asp:DropDownList ID="ddlCargo" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                      <div class="row">
                          <div class="form-group col-md-5">
                              <asp:Label ID="lblObrigatorio" runat="server" CssClass="col-form-label text-danger" Text="* Dados Obrigatórios!"></asp:Label>
                          </div>
                      </div>
                  </div>
                <div class="modal-body text-right">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>            
                    <asp:LinkButton Text="Salvar" CssClass="btn btn-success" ID="btnSalvar" runat="server" OnClick="btnSalvar_Click"></asp:LinkButton>
                  </div>
                </div>
            </cob:Modal>
        </asp:Panel>
        
    </div>
</asp:Content>

