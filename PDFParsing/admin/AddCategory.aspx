<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddCategory.aspx.cs" Inherits="PDFParsing.admin.AddCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="main-content-inner">
					<div class="breadcrumbs ace-save-state" id="breadcrumbs">
						<ul class="breadcrumb">
							<li>
								<i class="ace-icon fa fa-home home-icon"></i>
								<a href="DashBoard.aspx">Home</a>
							</li>
							<li class="active">Add New CATEGORY</li>
						</ul><!-- /.breadcrumb -->
					</div>

					<div class="page-content">
						<div class="row">
							<div class="col-xs-12">
								<!-- PAGE CONTENT BEGINS -->
								<div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                    <div class="form-group">
                                        <label class="col-sm-3  control-label no-padding-right" for="form-field-1"> Category Name <span style="color:red;">*</span> </label>
										<div class="col-sm-5">
                                            <div class="input-group">
																<span class="input-group-addon">
																	<i class="ace-icon fa fa-user"></i>
																</span>
																<asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    
															</div>
										    </div>
                                        
									</div>
                                    <div class="form-group">
                                        <label class="col-sm-3  control-label no-padding-right" for="form-field-1"> Category Type <span style="color:red;">*</span> </label>
										<div class="col-sm-5">
                                            <div class="input-group">
												<span class="input-group-addon">
													<i class="ace-icon fa fa-envelope"></i>
												</span>
												<asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="Debit" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Credit" Value="2"></asp:ListItem>
												</asp:DropDownList>
											</div>
										</div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <asp:Button ID="btnSave" runat="server" Text="Save Record" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                            </div>
                                        </div>
								</div>
						</div><!-- /.row -->
					</div><!-- /.page-content -->
				</div>
    <!-- Modal -->
        </div>
</asp:Content>

