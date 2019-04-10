<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" Debug="true" AutoEventWireup="true" CodeBehind="AddDocument.aspx.cs" Inherits="PDFParsing.AddDocument" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function changeinputstate() {
            var checkedValue = document.getElementById('chkPasswordProtected').checked;
            if (checkedValue) {
                document.getElementById('txtPassword').disabled = '';
            }
            else {
                document.getElementById('txtPassword').disabled = 'true';
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main-content-inner">
					<div class="breadcrumbs ace-save-state" id="breadcrumbs">
						<ul class="breadcrumb">
							<li>
								<i class="ace-icon fa fa-home home-icon"></i>
								<a href="DashBoard.aspx">Home</a>
							</li>
							<li class="active">Upload Your Document</li>
						</ul><!-- /.breadcrumb -->
					</div>

					<div class="page-content">
						<div class="row">
							<div class="col-xs-12">
                                
								<!-- PAGE CONTENT BEGINS -->
								<div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-5" for="form-field-1">

                                    <h3>Upload Your Document</h3></label>
                                        </div>

                                    <div class="form-group">
                                        <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                    <div class="form-group">
                                        <label class="col-sm-3  control-label no-padding-right" for="form-field-1"> Bank Name </label>
										<div class="col-sm-7">
                                            <div class="input-group">
																<span class="input-group-addon">
																	<i class="ace-icon fa fa-university"></i>
																</span>
																<asp:DropDownList ID="ddlBanks" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Text="HDFC Bank" Value="HDFC"></asp:ListItem>
                                                                    <asp:ListItem Text="State Bank of INDIA" Value="SBI"></asp:ListItem>
                                                                    <asp:ListItem Text="ICICI Bank" Value="ICICI"></asp:ListItem>
                                                                    <asp:ListItem Text="Kotak Mahindra Bank" Value="KMB"></asp:ListItem>
																</asp:DropDownList>
                                                                    
															</div>
										    </div>
                                        
									</div>
                                    <asp:Repeater ID="rptFiles" runat="server" OnItemCommand="rptFiles_ItemCommand">
                                        <ItemTemplate>
                                            <div class="form-group">
                                                <asp:Label ID="lblStatement" runat="server" CssClass="col-sm-3  control-label no-padding-right"></asp:Label>
										        <div class="col-sm-4">
                                                    <asp:FileUpload ID="id_input_file_1" ClientIDMode="Static" runat="server" />
										        </div>
                                                <div class="col-sm-3">
                                                    <div class="input-group">
												        <span class="input-group-addon">
													        <i class="ace-icon fa fa-lock"></i>
												        </span>
												        <asp:TextBox ID="txtPassword" placeholder="Statement Password (Optional)" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
											        </div>
										        </div>
                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="lnkAdd" runat="server" CommandName="add" CommandArgument='<%# Eval("Id") %>'><img src="Images/add.png" width="30" height="30" title="Add New Document" /></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("Id") %>'><img src="Images/minus.png" width="30" height="30" title="Delete a Document"  /></asp:LinkButton>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    
                                    <div class="form-group">
                                        <label class="col-sm-3  control-label no-padding-right" for="form-field-1"> &nbsp; </label>
                                        <div class="col-sm-5">
                                            <asp:Button ID="btnSave" runat="server" Text="Save Record" CssClass="btn btn-primary" OnClick="btnSave_Click" />

                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" style="margin-left:20px;" CssClass="btn btn-danger" OnClick="btnCancel_Click1" />
                                            </div>
                                        </div>
								</div>
						</div><!-- /.row -->
					</div><!-- /.page-content -->
				</div>
    <!-- Modal -->
        </div>
        <div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Uploaded Documents</h4>
      </div>
      <div class="modal-body">
        <p>Following Documents are uploaded. Please select which report you want to see.</p>
          <div class="row">
              <div class="col-xs-12">
                  <table id="dynamic-table22" class="table table-striped table-bordered table-hover">
												<thead>
													<tr>
                                                        <th>Document Name</th>
													</tr>
												</thead>

												<tbody>
                                                    <asp:Repeater ID="rptDOCUMENTS" runat="server" OnItemCommand="rptDOCUMENTS_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblParser_ID" runat="server" Text='<%#Eval("PARSER_ID") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblDOCUMENT_ID" runat="server" Text='<%#Eval("DOCUMENT_ID") %>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="dnld" CommandArgument='<%# Container.ItemIndex %>'>
																        <%#Eval("FileName") %>
															        </asp:LinkButton>
                                                                </td>
													        </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
												</tbody>
											</table>
              </div>
          </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
      </div>
    </div>

  </div>
</div>
    
</asp:Content>
