<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="PDFParsing.WebForm2" %>

<!DOCTYPE html>
<html>	
<head>
<title>Klasikal Login with Flat Responsive template :: w3layouts</title>
<meta name="viewport" content="width=device-width, initial-scale=1">
<script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
<meta name="keywords" content="Flat Dark Web Login Form Responsive Templates, Iphone Widget Template, Smartphone login forms,Login form, Widget Template, Responsive Templates, a Ipad 404 Templates, Flat Responsive Templates" />
<link href="/logincss/css/style.css" rel='stylesheet' type='text/css' />
<!--webfonts-->
<link href='http://fonts.googleapis.com/css?family=PT+Sans:400,700,400italic,700italic|Oswald:400,300,700' rel='stylesheet' type='text/css'>
<link href='http://fonts.googleapis.com/css?family=Exo+2' rel='stylesheet' type='text/css'>
<!--//webfonts-->
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
  <div class="main-content-inner">
					<div class="page-content">
						<div class="row">
                                                    <div class="col-sm-12">
                                                            <canvas id="bar-chart" width="50" height="10"></canvas>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <canvas id="pie-chart1" width="50" height="25"></canvas>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <canvas id="pie-chart2" width="50" height="25"></canvas>
                                                    </div>
                                                </div>
                    </div>
</div>
    </form>


    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.5.0/Chart.min.js"></script>
    <script type="text/javascript">
        var dataPoints = [];
        var dataPoints2 = [];
        $.ajax({
            type: 'GET',
            url: '/Analysis_Report.asmx/Get_PARSER_REPORT',
            data: { PARSER_ID: "300" },
            dataType: 'json',
            success: function (data) {

                $.each(data, function (index, element) {
                    dataPoints.push({ x: element.RMONTH, y: element.DEPOSIT_AMOUNT });
                });
                alert(dataPoints);
                new Chart(document.getElementById("bar-chart"), {
                    type: 'bar',
                    data: {
                        labels: ["March", "April"],
                        datasets: [
                          {
                              label: "Debit",
                              backgroundColor: ["#3e95cd", "#8e5ea2"],
                              data: dataPoints
                          }
                        ]
                    },
                    options: {
                        legend: { display: false },
                        title: {
                            display: true,
                            text: 'Account Statement Graphical Representation'
                        }
                    }
                });
            }
        });

        // Bar chart

    </script>
    <script type="text/javascript">
        // Bar chart
        new Chart(document.getElementById("pie-chart1"), {
            type: 'pie',
            data: {
                labels: ["January", "February", "March", "April", "May"],
                datasets: [
                  {
                      label: "Debit",
                      backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                      data: [2478, 5267, 734, 784, 433]
                  }
                ]
            },
            options: {
                legend: { display: false },
                title: {
                    display: true,
                    text: 'Debit Categorization'
                }
            }
        });
    </script>
    <script type="text/javascript">
        // Bar chart
        new Chart(document.getElementById("pie-chart2"), {
            type: 'pie',
            data: {
                labels: ["January", "February", "March", "April", "May"],
                datasets: [
                  {
                      label: "Credit",
                      backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
                      data: [2200, 5500, 700, 500, 1000]
                  }
                ]
            },
            options: {
                legend: { display: false },
                title: {
                    display: true,
                    text: 'Credit Categorization'
                }
            }
        });
    </script>

</body>
</html>
