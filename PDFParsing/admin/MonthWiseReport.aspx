<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Site1.Master" AutoEventWireup="true" CodeBehind="MonthWiseReport.aspx.cs" Inherits="PDFParsing.admin.MonthWiseReport" %>
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
							<div class="col-xs-3">
                                <div class="input-group">
																	<span class="input-group-addon">
																		<i class="fa fa-calendar"></i>
																	</span>
                                    <asp:TextBox ID="txtFromDate" placeholder="From Date" runat="server" CssClass="form-control date-picker" data-date-format="dd-mm-yyyy"></asp:TextBox>
																</div>
						    </div><!-- /.row -->
                            <div class="col-xs-3">
                                <div class="input-group">
																	<span class="input-group-addon">
																		<i class="fa fa-calendar"></i>
																	</span>
                                    <asp:TextBox ID="txtToDate" placeholder="To Date" runat="server" CssClass="form-control date-picker" data-date-format="dd-mm-yyyy"></asp:TextBox>
                                    <%--<input class="form-control date-picker" id="Text1" type="text" data-date-format="dd-mm-yyyy">--%>
																</div>
						    </div>
                            <div class="col-xs-3">
                                <asp:Button ID="btnSave" runat="server" Text="Filter" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            </div>
                        </div>
                                
								<!-- PAGE CONTENT BEGINS -->
								<div class="row">
									<div class="col-xs-12">
										<div class="clearfix">
											<div class="pull-right tableTools-container"></div>
										</div>
										<div class="table-header">
											Month By Document uploaded by USER
										</div>

										<!-- div.table-responsive -->

										<!-- div.dataTables_borderWrap -->
										<div>
											<table id="dynamic-table2" class="table table-striped table-bordered table-hover">
												<thead>
													<tr>
														<th>User Name</th>
                                                        <th>Email Address</th>
                                                        <th>Document Uploaded</th>
														<th>Action</th>
													</tr>
												</thead>

												<tbody>
                                                    <asp:Repeater ID="rptUsers" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
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
                                                                     <a title="View User Documents" class="btn btn-xs btn-info" href="UsersDocuments.aspx?USER_ID=<%#Eval("USER_ID") %>">
																        <i class="ace-icon fa fa-file-pdf-o  bigger-120"></i>
															        </a>
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
							
					</div><!-- /.page-content -->
				</div>
</asp:Content>
