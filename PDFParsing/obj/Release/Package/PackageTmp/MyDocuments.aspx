<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MyDocuments.aspx.cs" Inherits="PDFParsing.MyDocuments" %>
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
							<li class="active">My Documents</li>
						</ul><!-- /.breadcrumb -->
					</div>

					<div class="page-content">
						<div class="row">
							<div class="col-xs-12">
								<!-- PAGE CONTENT BEGINS -->
								<div class="row">
									<div class="col-xs-12">
										<div class="clearfix">
											<div class="pull-right tableTools-container"></div>
										</div>
										<div class="table-header">
											Documents
										</div>

										<!-- div.table-responsive -->

										<!-- div.dataTables_borderWrap -->
										<div>
											<table id="dynamic-table2" class="table table-striped table-bordered table-hover">
												<thead>
													<tr>
                                                        <th>Upload Date</th>
                                                        <th>Document Name</th>
                                                        <th>Bank Name</th>
                                                        <th>Account Type</th>
														<th>Action</th>
													</tr>
												</thead>

												<tbody>
                                                    <asp:Repeater ID="rptDOCUMENTS" runat="server" OnItemCommand="rptDOCUMENTS_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
															        <%#Eval("UPLOAD_DATE") %>
														        </td>
                                                                <td>
                                                                    <%#Eval("DOCUMENT_NAME") %>
                                                                </td>
                                                                <td>
															        <%#Eval("DOCUMENT_TYPE") %>
														        </td>
                                                                <td>
															        <%#Eval("ACCOUNT_TYPE") %>
														        </td>
														        <td>
                                                                    <asp:Button ID="btnDownload" CssClass="btn btn-xs btn-info" runat="server" Text="Download" CommandName="dnld" CommandArgument='<%#Eval("DOCUMENT_ID") %>' />
															        <asp:LinkButton ID="lnkDelete" CssClass="btn btn-xs btn-info" runat="server" CommandName="report" CommandArgument='<%#Eval("DOCUMENT_ID") %>'>
																        View Report
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



