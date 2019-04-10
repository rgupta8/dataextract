<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ViewReport.aspx.cs" Inherits="PDFParsing.ViewReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">

.panel.with-nav-tabs .panel-heading{
    padding: 5px 5px 0 5px;
}
.panel.with-nav-tabs .nav-tabs{
	border-bottom: none;
}
.panel.with-nav-tabs .nav-justified{
	margin-bottom: -1px;
}
/********************************************************************/
/*** PANEL DEFAULT ***/
.with-nav-tabs.panel-default .nav-tabs > li > a,
.with-nav-tabs.panel-default .nav-tabs > li > a:hover,
.with-nav-tabs.panel-default .nav-tabs > li > a:focus {
    color: #777;
}
.with-nav-tabs.panel-default .nav-tabs > .open > a,
.with-nav-tabs.panel-default .nav-tabs > .open > a:hover,
.with-nav-tabs.panel-default .nav-tabs > .open > a:focus,
.with-nav-tabs.panel-default .nav-tabs > li > a:hover,
.with-nav-tabs.panel-default .nav-tabs > li > a:focus {
    color: #777;
	background-color: #ddd;
	border-color: transparent;
}
.with-nav-tabs.panel-default .nav-tabs > li.active > a,
.with-nav-tabs.panel-default .nav-tabs > li.active > a:hover,
.with-nav-tabs.panel-default .nav-tabs > li.active > a:focus {
	color: #555;
	background-color: #fff;
	border-color: #ddd;
	border-bottom-color: transparent;
}
.with-nav-tabs.panel-default .nav-tabs > li.dropdown .dropdown-menu {
    background-color: #f5f5f5;
    border-color: #ddd;
}
.with-nav-tabs.panel-default .nav-tabs > li.dropdown .dropdown-menu > li > a {
    color: #777;   
}
.with-nav-tabs.panel-default .nav-tabs > li.dropdown .dropdown-menu > li > a:hover,
.with-nav-tabs.panel-default .nav-tabs > li.dropdown .dropdown-menu > li > a:focus {
    background-color: #ddd;
}
.with-nav-tabs.panel-default .nav-tabs > li.dropdown .dropdown-menu > .active > a,
.with-nav-tabs.panel-default .nav-tabs > li.dropdown .dropdown-menu > .active > a:hover,
.with-nav-tabs.panel-default .nav-tabs > li.dropdown .dropdown-menu > .active > a:focus {
    color: #fff;
    background-color: #555;
}
/********************************************************************/
/*** PANEL PRIMARY ***/
.with-nav-tabs.panel-primary .nav-tabs > li > a,
.with-nav-tabs.panel-primary .nav-tabs > li > a:hover,
.with-nav-tabs.panel-primary .nav-tabs > li > a:focus {
    color: #fff;
}
.with-nav-tabs.panel-primary .nav-tabs > .open > a,
.with-nav-tabs.panel-primary .nav-tabs > .open > a:hover,
.with-nav-tabs.panel-primary .nav-tabs > .open > a:focus,
.with-nav-tabs.panel-primary .nav-tabs > li > a:hover,
.with-nav-tabs.panel-primary .nav-tabs > li > a:focus {
	color: #fff;
	background-color: #3071a9;
	border-color: transparent;
}
.with-nav-tabs.panel-primary .nav-tabs > li.active > a,
.with-nav-tabs.panel-primary .nav-tabs > li.active > a:hover,
.with-nav-tabs.panel-primary .nav-tabs > li.active > a:focus {
	color: #428bca;
	background-color: #fff;
	border-color: #428bca;
	border-bottom-color: transparent;
}
.with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu {
    background-color: #428bca;
    border-color: #3071a9;
}
.with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > li > a {
    color: #fff;   
}
.with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > li > a:hover,
.with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > li > a:focus {
    background-color: #3071a9;
}
.with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > .active > a,
.with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > .active > a:hover,
.with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > .active > a:focus {
    background-color: #4a9fe9;
}
/********************************************************************/
/*** PANEL SUCCESS ***/
.with-nav-tabs.panel-success .nav-tabs > li > a,
.with-nav-tabs.panel-success .nav-tabs > li > a:hover,
.with-nav-tabs.panel-success .nav-tabs > li > a:focus {
	color: #3c763d;
}
.with-nav-tabs.panel-success .nav-tabs > .open > a,
.with-nav-tabs.panel-success .nav-tabs > .open > a:hover,
.with-nav-tabs.panel-success .nav-tabs > .open > a:focus,
.with-nav-tabs.panel-success .nav-tabs > li > a:hover,
.with-nav-tabs.panel-success .nav-tabs > li > a:focus {
	color: #3c763d;
	background-color: #d6e9c6;
	border-color: transparent;
}
.with-nav-tabs.panel-success .nav-tabs > li.active > a,
.with-nav-tabs.panel-success .nav-tabs > li.active > a:hover,
.with-nav-tabs.panel-success .nav-tabs > li.active > a:focus {
	color: #3c763d;
	background-color: #fff;
	border-color: #d6e9c6;
	border-bottom-color: transparent;
}
.with-nav-tabs.panel-success .nav-tabs > li.dropdown .dropdown-menu {
    background-color: #dff0d8;
    border-color: #d6e9c6;
}
.with-nav-tabs.panel-success .nav-tabs > li.dropdown .dropdown-menu > li > a {
    color: #3c763d;   
}
.with-nav-tabs.panel-success .nav-tabs > li.dropdown .dropdown-menu > li > a:hover,
.with-nav-tabs.panel-success .nav-tabs > li.dropdown .dropdown-menu > li > a:focus {
    background-color: #d6e9c6;
}
.with-nav-tabs.panel-success .nav-tabs > li.dropdown .dropdown-menu > .active > a,
.with-nav-tabs.panel-success .nav-tabs > li.dropdown .dropdown-menu > .active > a:hover,
.with-nav-tabs.panel-success .nav-tabs > li.dropdown .dropdown-menu > .active > a:focus {
    color: #fff;
    background-color: #3c763d;
}
/********************************************************************/
/*** PANEL INFO ***/
.with-nav-tabs.panel-info .nav-tabs > li > a,
.with-nav-tabs.panel-info .nav-tabs > li > a:hover,
.with-nav-tabs.panel-info .nav-tabs > li > a:focus {
	color: #31708f;
}
.with-nav-tabs.panel-info .nav-tabs > .open > a,
.with-nav-tabs.panel-info .nav-tabs > .open > a:hover,
.with-nav-tabs.panel-info .nav-tabs > .open > a:focus,
.with-nav-tabs.panel-info .nav-tabs > li > a:hover,
.with-nav-tabs.panel-info .nav-tabs > li > a:focus {
	color: #31708f;
	background-color: #bce8f1;
	border-color: transparent;
}
.with-nav-tabs.panel-info .nav-tabs > li.active > a,
.with-nav-tabs.panel-info .nav-tabs > li.active > a:hover,
.with-nav-tabs.panel-info .nav-tabs > li.active > a:focus {
	color: #31708f;
	background-color: #fff;
	border-color: #bce8f1;
	border-bottom-color: transparent;
}
.with-nav-tabs.panel-info .nav-tabs > li.dropdown .dropdown-menu {
    background-color: #d9edf7;
    border-color: #bce8f1;
}
.with-nav-tabs.panel-info .nav-tabs > li.dropdown .dropdown-menu > li > a {
    color: #31708f;   
}
.with-nav-tabs.panel-info .nav-tabs > li.dropdown .dropdown-menu > li > a:hover,
.with-nav-tabs.panel-info .nav-tabs > li.dropdown .dropdown-menu > li > a:focus {
    background-color: #bce8f1;
}
.with-nav-tabs.panel-info .nav-tabs > li.dropdown .dropdown-menu > .active > a,
.with-nav-tabs.panel-info .nav-tabs > li.dropdown .dropdown-menu > .active > a:hover,
.with-nav-tabs.panel-info .nav-tabs > li.dropdown .dropdown-menu > .active > a:focus {
    color: #fff;
    background-color: #31708f;
}
/********************************************************************/
/*** PANEL WARNING ***/
.with-nav-tabs.panel-warning .nav-tabs > li > a,
.with-nav-tabs.panel-warning .nav-tabs > li > a:hover,
.with-nav-tabs.panel-warning .nav-tabs > li > a:focus {
	color: #8a6d3b;
}
.with-nav-tabs.panel-warning .nav-tabs > .open > a,
.with-nav-tabs.panel-warning .nav-tabs > .open > a:hover,
.with-nav-tabs.panel-warning .nav-tabs > .open > a:focus,
.with-nav-tabs.panel-warning .nav-tabs > li > a:hover,
.with-nav-tabs.panel-warning .nav-tabs > li > a:focus {
	color: #8a6d3b;
	background-color: #faebcc;
	border-color: transparent;
}
.with-nav-tabs.panel-warning .nav-tabs > li.active > a,
.with-nav-tabs.panel-warning .nav-tabs > li.active > a:hover,
.with-nav-tabs.panel-warning .nav-tabs > li.active > a:focus {
	color: #8a6d3b;
	background-color: #fff;
	border-color: #faebcc;
	border-bottom-color: transparent;
}
.with-nav-tabs.panel-warning .nav-tabs > li.dropdown .dropdown-menu {
    background-color: #fcf8e3;
    border-color: #faebcc;
}
.with-nav-tabs.panel-warning .nav-tabs > li.dropdown .dropdown-menu > li > a {
    color: #8a6d3b; 
}
.with-nav-tabs.panel-warning .nav-tabs > li.dropdown .dropdown-menu > li > a:hover,
.with-nav-tabs.panel-warning .nav-tabs > li.dropdown .dropdown-menu > li > a:focus {
    background-color: #faebcc;
}
.with-nav-tabs.panel-warning .nav-tabs > li.dropdown .dropdown-menu > .active > a,
.with-nav-tabs.panel-warning .nav-tabs > li.dropdown .dropdown-menu > .active > a:hover,
.with-nav-tabs.panel-warning .nav-tabs > li.dropdown .dropdown-menu > .active > a:focus {
    color: #fff;
    background-color: #8a6d3b;
}
/********************************************************************/
/*** PANEL DANGER ***/
.with-nav-tabs.panel-danger .nav-tabs > li > a,
.with-nav-tabs.panel-danger .nav-tabs > li > a:hover,
.with-nav-tabs.panel-danger .nav-tabs > li > a:focus {
	color: #a94442;
}
.with-nav-tabs.panel-danger .nav-tabs > .open > a,
.with-nav-tabs.panel-danger .nav-tabs > .open > a:hover,
.with-nav-tabs.panel-danger .nav-tabs > .open > a:focus,
.with-nav-tabs.panel-danger .nav-tabs > li > a:hover,
.with-nav-tabs.panel-danger .nav-tabs > li > a:focus {
	color: #a94442;
	background-color: #ebccd1;
	border-color: transparent;
}
.with-nav-tabs.panel-danger .nav-tabs > li.active > a,
.with-nav-tabs.panel-danger .nav-tabs > li.active > a:hover,
.with-nav-tabs.panel-danger .nav-tabs > li.active > a:focus {
	color: #a94442;
	background-color: #fff;
	border-color: #ebccd1;
	border-bottom-color: transparent;
}
.with-nav-tabs.panel-danger .nav-tabs > li.dropdown .dropdown-menu {
    background-color: #f2dede; /* bg color */
    border-color: #ebccd1; /* border color */
}
.with-nav-tabs.panel-danger .nav-tabs > li.dropdown .dropdown-menu > li > a {
    color: #a94442; /* normal text color */  
}
.with-nav-tabs.panel-danger .nav-tabs > li.dropdown .dropdown-menu > li > a:hover,
.with-nav-tabs.panel-danger .nav-tabs > li.dropdown .dropdown-menu > li > a:focus {
    background-color: #ebccd1; /* hover bg color */
}
.with-nav-tabs.panel-danger .nav-tabs > li.dropdown .dropdown-menu > .active > a,
.with-nav-tabs.panel-danger .nav-tabs > li.dropdown .dropdown-menu > .active > a:hover,
.with-nav-tabs.panel-danger .nav-tabs > li.dropdown .dropdown-menu > .active > a:focus {
    color: #fff; /* active text color */
    background-color: #a94442; /* active bg color */
}
.table-responsive {
        width: 100%;
        float: left;
    }
</style>


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
                                <div class="panel panel-default">
                                    <div id="Tabs" role="tabpanel">
                                        <!-- Nav tabs -->
                                        <ul class="nav nav-tabs" role="tablist">
                                            <li class="active"><a href="#Analysis" aria-controls="personal" role="tab" data-toggle="tab">
                                                Analysis</a></li>
                                            <li><a href="#CustomerInfo" aria-controls="employment" role="tab" data-toggle="tab">Customer Info</a></li>
                                            <li><a href="#EODBalances" aria-controls="employment" role="tab" data-toggle="tab">EOD Balances</a></li>
                                            <li><a href="#FundsReceived" aria-controls="employment" role="tab" data-toggle="tab">Funds Received</a></li>
                                            <li><a href="#Remittances" aria-controls="employment" role="tab" data-toggle="tab">Funds Remittances</a></li>
                                            <li><a href="#Bounced_Xns" aria-controls="employment" role="tab" data-toggle="tab">Bounced-Penal Xns</a></li>
                                        </ul>
                                        <!-- Tab panes -->
                                        <div class="tab-content" style="padding-top: 20px">
                                            <div role="tabpanel" class="tab-pane active" id="Analysis">
                                                <div class="row" style="margin-top:20px;">
                                                    <div class="col-sm-12" style="text-align:right;">
                                                        <asp:Button ID="btnCancel" runat="server" Text="Update Category" style="margin-left:20px;" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <h4 style="text-align:center;">WithDrawal Transactions</h4>
                                                                <div id="dvChart" style="text-align:center;">
                                                                </div>
                                                                <div id="dvLegend" style="text-align:center;">
                                                                </div>
                                                        </div>
                                                    <div class="col-sm-6">
                                                        <h4 style="text-align:center;" >Deposit  Transactions</h4>
                                                                <div id="dvChart1" style="text-align:center;">
                                                                </div>
                                                                <div id="dvLegend1" style="text-align:center;">
                                                                </div>
                                                        </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 table-responsive">
                                                        <asp:GridView ID="grdAnalysis" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="true">
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                
                                            </div>
                                            <div role="tabpanel" class="tab-pane" id="CustomerInfo">
                                                <div class="row">
                                               <div class="col-xs-12" style="text-align:right;">
                                                        <asp:Button ID="btnDownload" runat="server" Text="Download Report" CssClass="btn btn-primary" Visible="false" />
                                                    </div>
                                                    <asp:Repeater ID="rptPARSER_DOCUMENT" runat="server">
                                                            <ItemTemplate>
									                        <div class="col-xs-6">
                                                        
                                                                <div class="profile-user-info profile-user-info-striped">
												                <div class="profile-info-row">
													                <div class="profile-info-name"> Name </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("PARSER_NAME") %></span>
													                </div>
												                </div>

												                <div class="profile-info-row">
													                <div class="profile-info-name"> Address 1 </div>

													                <div class="profile-info-value">
														                <i class="fa fa-map-marker light-orange bigger-110"></i>
														                <span class="editable"><%#Eval("ADDRESS1") %></span>
													                </div>
												                </div>

												                <div class="profile-info-row">
													                <div class="profile-info-name"> Address 2 </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("ADDRESS2") %></span>
													                </div>
												                </div>

												                <div class="profile-info-row">
													                <div class="profile-info-name"> Address 3 </div>

													                <div class="profile-info-value">
														                <span class="editable" ><%#Eval("ADDRESS3") %></span>
													                </div>
												                </div>

												                <div class="profile-info-row">
													                <div class="profile-info-name"> City </div>

													                <div class="profile-info-value">
														                <span class="editable" ><%#Eval("CITY") %></span>
													                </div>
												                </div>

												                <div class="profile-info-row">
													                <div class="profile-info-name"> State </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("STATE") %></span>
													                </div>
												                </div>
                                                            <div class="profile-info-row">
													                <div class="profile-info-name"> Branch Account </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("ACCOUNT_BRANCH") %></span>
													                </div>
												                </div>
                                                            <div class="profile-info-row">
													                <div class="profile-info-name"> Branch Address </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("BRANCH_ADDRESS") %></span>
													                </div>
												                </div>
                                                            <div class="profile-info-row">
													                <div class="profile-info-name"> Branch City </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("BRANCH_CITY") %></span>
													                </div>
												                </div>
                                                            <div class="profile-info-row">
													                <div class="profile-info-name"> Branch State </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("BRANCH_STATE") %></span>
													                </div>
												                </div>
									                </div>
                                                     </div>
                                                                <div class="col-xs-6">
                                                        
                                                                <div class="profile-user-info profile-user-info-striped">
												                <div class="profile-info-row">
													                <div class="profile-info-name"> Branch Phone No </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("BRANCH_PHONENO") %></span>
													                </div>
												                </div>

												                <div class="profile-info-row">
													                <div class="profile-info-name"> Currency </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("CURRENCY") %></span>
													                </div>
												                </div>

												                <div class="profile-info-row">
													                <div class="profile-info-name"> Email </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("EMAIL") %></span>
													                </div>
												                </div>

												                <div class="profile-info-row">
													                <div class="profile-info-name"> Cust ID </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("CUSTID") %></span>
													                </div>
												                </div>

												                <div class="profile-info-row">
													                <div class="profile-info-name"> Account No </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("ACCOUNTNO") %></span>
													                </div>
												                </div>

												                <div class="profile-info-row">
													                <div class="profile-info-name"> Account  Open Date </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("ACCOUNT_OPEN_DATE") %></span>
													                </div>
												                </div>
                                                            <div class="profile-info-row">
													                <div class="profile-info-name"> Account Status </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("ACCOUNT_STATUS") %></span>
													                </div>
												                </div>
                                                            <div class="profile-info-row">
													                <div class="profile-info-name"> Branch Code </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("BRANCH_CODE") %></span>
													                </div>
												                </div>
                                                            <div class="profile-info-row">
													                <div class="profile-info-name"> Product Code </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("PRODUCT_CODE") %></span>
													                </div>
												                </div>
                                                            <div class="profile-info-row">
													                <div class="profile-info-name"> Nomination </div>

													                <div class="profile-info-value">
														                <span class="editable"><%#Eval("NOMINATION") %></span>
													                </div>
												                </div>
											                </div>
                                                            
										
									                </div>
                                                    </ItemTemplate>
                                                        </asp:Repeater>
                                            </div>
                                                </div>
                                            <div role="tabpanel" class="tab-pane" id="EODBalances">
                                                 <asp:GridView ID="grdEODBalance" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="true">
                                                </asp:GridView>
                                            </div>
                                            <div role="tabpanel" class="tab-pane" id="FundsReceived">
                                              
                                                <asp:Repeater ID="rptFundsReceived" runat="server">
                                                    <ItemTemplate>
                                                        <table class="table table-striped table-bordered table-hover">
                                                        <tr>
                                                            <td colspan="2">
                                                                <strong><%#Eval("Month") %></strong>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <%#Eval("Description1") %>
                                                            </td>
                                                             <td>
                                                                <%#Eval("Amount1") %>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                <%#Eval("Description2") %>
                                                            </td>
                                                             <td>
                                                                <%#Eval("Amount2") %>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                <%#Eval("Description3") %>
                                                            </td>
                                                             <td>
                                                                <%#Eval("Amount3") %>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                <%#Eval("Description4") %>
                                                            </td>
                                                             <td>
                                                                <%#Eval("Amount4") %>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                <%#Eval("Description5") %>
                                                            </td>
                                                             <td>
                                                                <%#Eval("Amount5") %>
                                                            </td>
                                                        </tr>
                                                            </table>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                            <div role="tabpanel" class="tab-pane" id="Remittances">
                                               <asp:Repeater ID="rptFundsRemittance" runat="server">
                                                    <ItemTemplate>
                                                        <table class="table table-striped table-bordered table-hover">
                                                        <tr>
                                                            <td colspan="2">
                                                                <strong><%#Eval("Month") %></strong>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <%#Eval("Description1") %>
                                                            </td>
                                                             <td>
                                                                <%#Eval("Amount1") %>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                <%#Eval("Description2") %>
                                                            </td>
                                                             <td>
                                                                <%#Eval("Amount2") %>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                <%#Eval("Description3") %>
                                                            </td>
                                                             <td>
                                                                <%#Eval("Amount3") %>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                <%#Eval("Description4") %>
                                                            </td>
                                                             <td>
                                                                <%#Eval("Amount4") %>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                <%#Eval("Description5") %>
                                                            </td>
                                                             <td>
                                                                <%#Eval("Amount5") %>
                                                            </td>
                                                        </tr>
                                                            </table>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                            <div role="tabpanel" class="tab-pane" id="Bounced_Xns">
                                               
                                            </div>
                                            <div role="tabpanel" class="tab-pane" id="Xns">
                                        <table id="dynamic-table2" class="table table-striped table-bordered table-hover">
												<thead>
													<tr>
                                                        <th>S.No.</th>
                                                        <th>DATE</th>
                                                        <th>NARRATION</th>
														<th>CHQ No.</th>
                                                        <th>Withdrawal Amount</th>
                                                        <th>Deposit Amount</th>
                                                        <th>Closing Balance</th>
                                                        <th>Category</th>
													</tr>
												</thead>

												<tbody>
                                                    <asp:Repeater ID="rptSHEETS" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%#Eval("INDEX") %></td>
                                                                <td><%#Eval("DATE") %></td>
                                                                <td><%#Eval("NARRATION") %></td>
														        <td><%#Eval("CHQ_NO") %></td>
                                                                <td><%#Eval("WITHDRAWAL_AMOUNT") %></td>
                                                                <td><%#Eval("DEPOSIT_AMOUNT") %></td>
                                                                <td><%#Eval("CLOSING_BALANCE") %></td>
                                                                <td><asp:DropDownList ID="ddlCategory" runat="server">
                                                                    <asp:ListItem Text="Undefined" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList></td>
													        </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
												</tbody>
											</table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
							</div><!-- /.col -->
						</div><!-- /.row -->
					</div><!-- /.page-content -->
				</div>

    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="//cdn.jsdelivr.net/excanvas/r3/excanvas.js" type="text/javascript"></script>
    <script src="//cdn.jsdelivr.net/chart.js/0.2/Chart.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            LoadChart();
            LoadChart2();
        });
        function LoadChart() {
            var chartType = 1;
            $.ajax({
                type: "POST",
                url: "WebForm3.aspx/GetChart",
                data: "{country: '1'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $("#dvChart").html("");
                    $("#dvLegend").html("");
                    var data = eval(r.d);
                    var el = document.createElement('canvas');
                    $("#dvChart")[0].appendChild(el);

                    //Fix for IE 8
                    //if ($.browser.msie && $.browser.version == "8.0") {
                    //    G_vmlCanvasManager.initElement(el);
                    //}
                    var ctx = el.getContext('2d');
                    var userStrengthsChart;
                    switch (chartType) {
                        case 1:
                            userStrengthsChart = new Chart(ctx).Pie(data);
                            break;
                        case 2:
                            userStrengthsChart = new Chart(ctx).Doughnut(data);
                            break;
                    }
                    for (var i = 0; i < data.length; i++) {
                        var div = $("<div />");
                        div.css("margin-bottom", "10px");
                        div.html("<span style = 'display:inline-block;height:10px;width:10px;background-color:" + data[i].color + "'></span> " + data[i].text);
                        $("#dvLegend").append(div);
                    }
                },
                failure: function (response) {
                    alert('There was an error.');
                }
            });
        }
        function LoadChart2() {
            var chartType = 1;
            $.ajax({
                type: "POST",
                url: "WebForm3.aspx/GetChart1",
                data: "{country: '2'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $("#dvChart1").html("");
                    $("#dvLegend1").html("");
                    var data = eval(r.d);
                    var el = document.createElement('canvas');
                    $("#dvChart1")[0].appendChild(el);

                    //Fix for IE 8
                    //if ($.browser.msie && $.browser.version == "8.0") {
                    //    G_vmlCanvasManager.initElement(el);
                    //}
                    var ctx = el.getContext('2d');
                    var userStrengthsChart;
                    switch (chartType) {
                        case 1:
                            userStrengthsChart = new Chart(ctx).Pie(data);
                            break;
                        case 2:
                            userStrengthsChart = new Chart(ctx).Doughnut(data);
                            break;
                    }
                    for (var i = 0; i < data.length; i++) {
                        var div = $("<div />");
                        div.css("margin-bottom", "10px");
                        div.html("<span style = 'display:inline-block;height:10px;width:10px;background-color:" + data[i].color + "'></span> " + data[i].text);
                        $("#dvLegend1").append(div);
                    }
                },
                failure: function (response) {
                }
            });
        }
    </script>
</asp:Content>