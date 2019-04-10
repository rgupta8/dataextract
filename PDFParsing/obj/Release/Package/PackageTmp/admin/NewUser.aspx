<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Site1.Master" AutoEventWireup="true" CodeBehind="NewUser.aspx.cs" Inherits="PDFParsing.admin.NewUser" %>
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
							<li class="active">Add New User</li>
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
                                        <label class="col-sm-3  control-label no-padding-right" for="form-field-1"> UserName <span style="color:red;">*</span> </label>
										<div class="col-sm-5">
                                            <div class="input-group">
																<span class="input-group-addon">
																	<i class="ace-icon fa fa-user"></i>
																</span>
																<asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    
															</div>
										    </div>
                                        
									</div>
                                    <div class="form-group">
                                        <label class="col-sm-3  control-label no-padding-right" for="form-field-1"> Email Address <span style="color:red;">*</span> </label>
										<div class="col-sm-5">
                                            <div class="input-group">
												<span class="input-group-addon">
													<i class="ace-icon fa fa-envelope"></i>
												</span>
												<asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control"></asp:TextBox>
											</div>
										</div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3  control-label no-padding-right" for="form-field-1"> Password <span style="color:red;">*</span></label>
										<div class="col-sm-5">
                                            <div class="input-group">
												<span class="input-group-addon">
													<i class="ace-icon fa fa-lock"></i>
												</span>
												<asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"></asp:TextBox>
											</div>
										</div>
                                    </div>
                                    
                                    <div class="form-group">
                                        <label class="col-sm-3  control-label no-padding-right" for="form-field-1"> Is Active </label>
										<div class="col-sm-8">
                                            <div class="checkbox">
													<label class="block">
                                                        <asp:CheckBox ID="chkIs_Active" runat="server" CssClass="ace input-lg" />
														<span class="lbl bigger-120"></span>
													</label>
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


