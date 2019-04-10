<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Site1.Master" AutoEventWireup="true" CodeBehind="VIEW_USERS.aspx.cs" Inherits="PDFParsing.admin.VIEW_USERS" %>
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
							<li class="active">Users</li>
						</ul><!-- /.breadcrumb -->
					</div>

					<div class="page-content">
						<div class="row">
							<div class="col-xs-12">
                                <a href="NewUser.aspx" class="btn btn-primary">Add New User</a> 
								<!-- PAGE CONTENT BEGINS -->
								<div class="row">
									<div class="col-xs-12">
										<div class="clearfix">
											<div class="pull-right tableTools-container"></div>
										</div>
										<div class="table-header">
											Users
										</div>

										<!-- div.table-responsive -->

										<!-- div.dataTables_borderWrap -->
										<div>
											<table id="dynamic-table2" class="table table-striped table-bordered table-hover">
												<thead>
													<tr>
                                                        <th>Create Date</th>
														<th>User Name</th>
                                                        <th>Email Address</th>
                                                        <th>Document Uploaded</th>
                                                        <th>Is Active</th>
														<th>Action</th>
													</tr>
												</thead>

												<tbody>
                                                    <asp:Repeater ID="rptUsers" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%#Eval("CREATE_DATE") %>
                                                                </td>
														        <td>
															        <%#Eval("USERNAME") %>
														        </td>
                                                                <td>
															        <%#Eval("EMAIL_ADDRESS") %>
														        </td>
                                                                <td>
                                                                    <a title="View User Documents" class="btn btn-xs btn-info" href="UsersDocuments.aspx?USER_ID=<%#Eval("USER_ID") %>">
																        <%#Eval("DOCUMENT_UPLOADED") %>
															        </a>
															        
														        </td>
                                                                <td>
															        <%#Eval("IS_ACTIVE") %>
														        </td>
														        <td>
                                                                     <a title="View User Documents" class="btn btn-xs btn-info" href="UsersDocuments.aspx?USER_ID=<%#Eval("USER_ID") %>">
																        <i class="ace-icon fa fa-file-pdf-o  bigger-120"></i>
															        </a>
															        <a title="Edit User" class="btn btn-xs btn-info" href="NewUser.aspx?id=<%#Eval("USER_ID") %>">
																        <i class="ace-icon fa fa-pencil bigger-120"></i>
															        </a>
															        <asp:LinkButton ToolTip="Delete User" ID="lnkDelete" CssClass="btn btn-xs btn-info" runat="server" CommandName="del" CommandArgument='<%#Eval("USER_ID") %>'  OnClientClick="return confirm('Do you want to delete this Record?');">
																        <i class="ace-icon fa fa-trash-o bigger-120"></i>
															        </asp:LinkButton>
														        </div>
														        </td>
													        </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
												</tbody>
											</table>
										</div>
									</div>
								</div>
								<!-- PAGE CONTENT ENDS -->
							</div><!-- /.col -->
						</div><!-- /.row -->
					</div><!-- /.page-content -->
				</div>
</asp:Content>


