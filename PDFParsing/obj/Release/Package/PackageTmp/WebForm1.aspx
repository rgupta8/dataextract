<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="PDFParsing.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        html, body, #container {
    width: 100%;
    height: 100%;
    margin: 0;
    padding: 0;
}
    </style>
    <script src="https://cdn.anychart.com/js/8.0.1/anychart-bundle.min.js"></script>
<script src="https://cdn.anychart.com/releases/8.0.1/js/anychart-base.min.js"></script>
<script src="https://cdn.anychart.com/releases/8.0.1/js/anychart-exports.min.js"></script>
<script src="https://cdn.anychart.com/releases/8.0.1/js/anychart-ui.min.js"></script>
<link rel="stylesheet" href="https://cdn.anychart.com/releases/8.0.1/css/anychart-ui.min.css">
    <script type="text/javascript">
        anychart.onDocumentReady(function () {

            // create data
            var data = [
              { x: "John", value: 10000 },
              { x: "Jake", value: 12000 },
              { x: "x", value: 12000 },
              { x: "y", value: 12000 },
              {
                  x: "Peter", value: 13000,
                  normal: {
                      fill: "#5cd65c",
                      stroke: null,
                      label: { enabled: true }
                  },
                  hovered: {
                      fill: "#5cd65c",
                      label: { enabled: true }
                  },
                  selected: {
                      fill: "#5cd65c",
                      label: { enabled: true }
                  }
              },
              { x: "James", value: 10000 },
              { x: "Mary", value: 9000 }
            ];

            // create a chart
            chart = anychart.column();

            // create a column series and set the data
            var series = chart.column(data);

            // set the chart title
            chart.title("Column Chart: Appearance (Individual Points)");

            // set the titles of the axes
            chart.xAxis().title("Manager");
            chart.yAxis().title("Sales, $");

            // set the container id
            chart.container("container");

            // initiate drawing the chart
            chart.draw();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container"></div>
    </form>
</body>
</html>
