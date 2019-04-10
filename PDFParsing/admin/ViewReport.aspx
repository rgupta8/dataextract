<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Site1.Master" AutoEventWireup="true" CodeBehind="ViewReport.aspx.cs" Inherits="PDFParsing.admin.ViewReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    /*----- Tabs -----*/
.tabs {
    width:100%;
    display:inline-block;
}
 
    /*----- Tab Links -----*/
    /* Clearfix */
    .tab-links:after {
        display:block;
        clear:both;
        content:'';
    }
 
    .tab-links li {
        margin:0px 5px;
        float:left;
        list-style:none;
    }
 
        .tab-links a {
            padding: 7px 8px;
    display: inline-block;
    border-radius: 3px 3px 0px 0px;
    background: #7FB5DA;
    font-size: 12px;
    font-weight: 600;
    color: #4c4c4c;
    transition: all linear 0.15s;
        }
 
        .tab-links a:hover {
            background:#a7cce5;
            text-decoration:none;
        }
 
    li.active a, li.active a:hover {
        background:#fff;
        color:#4c4c4c;
    }
 
    /*----- Content of Tabs -----*/
    .tab-content {
        padding:15px;
        border-radius:3px;
        box-shadow:-1px 1px 1px rgba(0,0,0,0.15);
        background:#fff;
    }
 
        .tab {
            display:none;
        }
 
        .tab.active {
            display:block;
        }
        ol, ul {
    padding: 0;
    margin: 0 0 1px 10px;
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
                                <div class="tabs">
    <ul class="tab-links">
        <li class="active"><a href="#tab1">Statements</a></li>
    </ul>
 
    <div class="tab-content">
        <div id="tab2" class="tab active">
            <!-- PAGE CONTENT BEGINS -->
								<div class="row">
									
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
                                    <div class="col-xs-7" style="display:none;">

										<table class="highchart" data-graph-container-before="1" data-graph-type="column" style="display:none">
    <caption>REPORT VIEW</caption>
    <thead>
        <tr>                                  
            <th>Month</th>
            <th>Debit</th>
            <th>Credit</th>
        </tr>
     </thead>
     <tbody>
        <asp:Repeater ID="rptReport1" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("RMONTH") %> / <%#Eval("RYEAR") %></td>
                    <td><%#Eval("WITHDRAWAL_AMOUNT") %></td>
                    <td><%#Eval("DEPOSIT_AMOUNT") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>
									</div>
								</div>
                                <div class="row">
                                    <div class="col-xs-12" style="margin-top:20px;">
                                        <table id="dynamic-table2" class="table table-striped table-bordered table-hover">
												<thead>
													<tr>
                                                        <th>S.No.</th>
                                                        <th>DATE</th>
                                                        <th>NARRATION</th>
														<th>CHQ No.</th>
														<th>Value Date</th>
                                                        <th>Withdrawal Amount</th>
                                                        <th>Deposit Amount</th>
                                                        <th>Closing Balance</th>
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
														        <td><%#Eval("VALUE_DATE") %></td>
                                                                <td><%#Eval("WITHDRAWAL_AMOUNT") %></td>
                                                                <td><%#Eval("DEPOSIT_AMOUNT") %></td>
                                                                <td><%#Eval("CLOSING_BALANCE") %></td>
													        </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
												</tbody>
											</table>
										</div>
                                </div>
								<!-- PAGE CONTENT ENDS -->
        </div>
 
    </div>
</div>
								
							</div><!-- /.col -->
						</div><!-- /.row -->
					</div><!-- /.page-content -->
				</div>
</asp:Content>