<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CategoryMaster.aspx.cs" Inherits="PDFParsing.admin.CategoryMaster" %>
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
							<li class="active">Category Master</li>
						</ul><!-- /.breadcrumb -->
					</div>

					<div class="page-content">
						<div class="row">
							<div class="col-xs-12">
                                <a href="AddCategory.aspx" class="btn btn-primary">Add New Category</a> 
								<!-- PAGE CONTENT BEGINS -->
								<div class="row">
									<div class="col-xs-12">
										<div class="clearfix">
											<div class="pull-right tableTools-container"></div>
										</div>
										<div class="table-header">
											Categories
										</div>

										<!-- div.table-responsive -->

										<!-- div.dataTables_borderWrap -->
										<div>
											<table id="dynamic-table2" class="table table-striped table-bordered table-hover">
												<thead>
													<tr>
                                                        <th>Category Name</th>
														<th>Type</th>
														<th>Action</th>
													</tr>
												</thead>

												<tbody>
                                                    <asp:Repeater ID="rptCategory" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%#Eval("CATEGORY_NAME") %>
                                                                </td>
														        <td>
															        <%#Eval("CATEGORYTYPE") %>
														        </td>
															        <a title="Edit User" class="btn btn-xs btn-info" href="NewUser.aspx?id=<%#Eval("CATEGORY_ID") %>">
																        <i class="ace-icon fa fa-pencil bigger-120"></i>
															        </a>
															        <asp:LinkButton ToolTip="Delete User" ID="lnkDelete" CssClass="btn btn-xs btn-info" runat="server" CommandName="del" CommandArgument='<%#Eval("CATEGORY_ID") %>'  OnClientClick="return confirm('Do you want to delete this Record?');">
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

