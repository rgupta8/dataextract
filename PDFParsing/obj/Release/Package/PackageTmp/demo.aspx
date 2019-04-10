<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="demo.aspx.cs" Inherits="PDFParsing.demo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="description" content="overview &amp; stats" />
		<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

		<!-- bootstrap & fontawesome -->
		<link rel="stylesheet" href="/assets/css/bootstrap.min.css" />
		<link rel="stylesheet" href="/assets/font-awesome/4.5.0/css/font-awesome.min.css" />

		<!-- page specific plugin styles -->
		<link rel="stylesheet" href="/assets/css/jquery-ui.custom.min.css" />
		<link rel="stylesheet" href="/assets/css/chosen.min.css" />
		<link rel="stylesheet" href="/assets/css/bootstrap-datepicker3.min.css" />
		<link rel="stylesheet" href="/assets/css/bootstrap-timepicker.min.css" />
		<link rel="stylesheet" href="/assets/css/daterangepicker.min.css" />
		<link rel="stylesheet" href="/assets/css/bootstrap-datetimepicker.min.css" />
		<link rel="stylesheet" href="/assets/css/bootstrap-colorpicker.min.css" />

		<!-- text fonts -->
		<link rel="stylesheet" href="/assets/css/fonts.googleapis.com.css" />

		<!-- ace styles -->
		<link rel="stylesheet" href="/assets/css/ace.min.css" class="ace-main-stylesheet" />

		<!--[if lte IE 9]>
			<link rel="stylesheet" href="/assets/css/ace-part2.min.css" class="ace-main-stylesheet" />
		<![endif]-->
		<link rel="stylesheet" href="/assets/css/ace-skins.min.css" />
		<link rel="stylesheet" href="/assets/css/ace-rtl.min.css" />

		<!--[if lte IE 9]>
		  <link rel="stylesheet" href="/assets/css/ace-ie.min.css" />
		<![endif]-->

		<!-- inline styles related to this page -->

		<!-- ace settings handler -->
		<script src="/assets/js/ace-extra.min.js"></script>

		<!-- HTML5shiv and Respond.js for IE8 to support HTML5 elements and media queries -->

		<!--[if lte IE 8]>
		<script src="/assets/js/html5shiv.min.js"></script>
		<script src="/assets/js/respond.min.js"></script>
		<![endif]-->
</head>
<body>
    <form id="form1" runat="server">
        <% Response.Write(".NET Framework Version: " + Environment.Version.ToString()); %>
        <div class="row">
            <div class="col-sm-4">
                <table class="highchart" data-graph-container-before="1" data-graph-type="column" style="display:none">
    <caption>Monthly Transaction Report</caption>
    <thead>
        <tr>                                  
            <th>Month</th>
            <th>Debit</th>
            <th>Credit</th>
        </tr>
     </thead>
     <tbody>
        <tr>
            <td>January</td>
            <td>50000</td>
            <td>60000</td>
        </tr>
        <tr>
            <td>February</td>
            <td>5000</td>
            <td>10000</td>
        </tr>
        <tr>
            <td>March</td>
             <td>2000</td>
            <td>12000</td>
        </tr>
         <tr>
            <td>April</td>
             <td>10000</td>
            <td>5000</td>
        </tr>
    </tbody>
</table>
            </div>
            <div class="col-sm-4">
                <table class="highchart" data-graph-container-before="1" data-graph-type="pie" style="display:none" data-graph-datalabels-enabled="1">
    <thead>
        <tr>                                  
            <th>Month</th>
            <th>Sales</th>
        </tr>
     </thead>
     <tbody>
        <tr>
            <td>January</td>
            <td data-graph-name="January" data-graph-item-highlight="1">8000</td>
        </tr>
        <tr>
            <td>February</td>
            <td data-graph-name="February">12000</td>
        </tr>
        <tr>
            <td>March</td>
            <td data-graph-name="March">18000</td>
        </tr>
    </tbody>
</table>
            </div>
            <div class="col-sm-4">
                <table class="highchart" data-graph-container-before="1" data-graph-type="column" style="display:none" data-graph-inverted="1">
    <thead>
        <tr>                                  
            <th>Month</th>
            <th>Sales</th>
        </tr>
     </thead>
     <tbody>
        <tr>
            <td>January</td>
            <td>8000</td>
        </tr>
        <tr>
            <td>February</td>
            <td>12000</td>
        </tr>
        <tr>
            <td>March</td>
            <td>18000</td>
        </tr>
    </tbody>
</table>
            </div>
        </div>

    </form>
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <script src="/scripts/highcharts.js" ></script>
    <script src="/scripts/jquery.highchartTable.min.js" ></script> 
    <script type="text/javascript">
        $(document).ready(function () {
            $('table.highchart').highchartTable();
        });
    </script>
</body>
</html>
