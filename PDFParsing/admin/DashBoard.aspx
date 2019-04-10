<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Site1.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="PDFParsing.admin.DashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main-content-inner">
					<div class="breadcrumbs ace-save-state" id="breadcrumbs">
						<ul class="breadcrumb">
							<li>
								<i class="ace-icon fa fa-home home-icon"></i>
								<a href="#">Home</a>
							</li>
							<li class="active">Dashboard</li>
						</ul><!-- /.breadcrumb -->

						<!-- /.nav-search -->
					</div>

					<div class="page-content">
						<!-- /.ace-settings-container -->

						<div class="page-header">
							<h1>
								Dashboard
							</h1>
						</div><!-- /.page-header -->

						<div class="row">
                <div class="col-lg-3 col-md-6">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="fa fa-comments fa-5x"></i>
                                </div>
                                <div class="col-xs-9 text-right">
                                    <div class="huge" id="TotalUsers" runat="server">00</div>
                                    <div>Total Users</div>
                                </div>
                            </div>
                        </div>
                        <a href="View_Users.aspx">
                            <div class="panel-footer">
                                <span class="pull-left">View Details</span>
                                <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                <div class="clearfix"></div>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6">
                    <div class="panel panel-green">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-3">
                                    <i class="fa fa-tasks fa-5x"></i>
                                </div>
                                <div class="col-xs-9 text-right">
                                    <div class="huge" id="TotalDol" runat="server">00</div>
                                    <div>Total Documents</div>
                                </div>
                            </div>
                        </div>
                        <a href="View_Users.aspx">
                            <div class="panel-footer">
                                <span class="pull-left">View Details</span>
                                <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                <div class="clearfix"></div>
                            </div>
                        </a>
                    </div>
                </div>
                            <div class="col-lg-6 col-md-6">
                                <table class="highchart" data-graph-container-before="1" data-graph-type="pie" style="display:none" data-graph-datalabels-enabled="1">
                                    <caption>Months / Documents Uploaded</caption>
    <thead>
        <tr>                                  
            <th>Month</th>
            <th>Total Amount</th>

        </tr>
     </thead>
     <tbody>
         <asp:Repeater ID="rptReport" runat="server">
             <ItemTemplate>
                 <tr>
                    <td><%#Eval("RMONTH") %>/<%#Eval("RYEAR") %></td>
                    <td data-graph-name="<%#Eval("RMONTH") %>"><%#Eval("Document_Uploaded") %></td>
                </tr>
             </ItemTemplate>
         </asp:Repeater>
        
    </tbody>
</table>
                            </div>
            </div><!-- /.row -->
                        <div class="row">
                            
                        </div>
					</div><!-- /.page-content -->
				</div>
</asp:Content>


