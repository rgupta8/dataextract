<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="PDFParsing.Register" %>

<!DOCTYPE html>

<html>	
<head>
<title>MudraCube: Registration</title>
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
        <div class="row" style="padding-left:20px; padding-top:20px;">
            <img src="logo.png" style="width:186px;"  />
        </div>
        
 <!--SIGN UP-->
 <h1></h1>
        <div class="col-xs-6" style="padding-left:70px; position:absolute;">
            <h2 class="h2">Helping Business</h2>
            <h2 class="h2">Beyond Boundries</h2>
        </div>
        <div class="col-xs-6">
<div class="login-form">
		<div class="head-info">
			<label>Welcome to MudraCube</label>
		</div>
			<div class="clear"> </div>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
					<asp:TextBox ID="txtName" CssClass="text" runat="server" AutoCompleteType="None" placeholder="User Name"></asp:TextBox>
					<asp:TextBox ID="txtEmail" ClientIDMode="Static" CssClass="text" runat="server" placeholder="Email Address"></asp:TextBox>
					<asp:TextBox ID="txtMobile" ClientIDMode="Static" CssClass="text" runat="server" placeholder="Mobile No"></asp:TextBox>
     <div style="padding:20px;">
                        Already Sign In, <a href="/default.aspx">Click here</a>
                    </div>
	<div class="signin">
		<asp:Button ID="btnLogin2" runat="server" Text="Register" OnClick="btnLogin2_Click1" ClientIDMode="Static" CssClass="btn btn-lg btn-success btn-block" />
	</div>
</div>
            </div>
 <div class="copy-rights">
					<p>&copy; 2017 MudraCube. All rights reserved. User subject to End User Agreement.  <a href="http://w3layouts.com" target="_blank" style="display:none;">w3layouts</a> </p>
			</div>
    </form>
    <script type="text/javascript">
		    jQuery(function ($) {
		        $(document).on('click', '.toolbar a[data-target]', function (e) {
		            e.preventDefault();
		            var target = $(this).data('target');
		            $('.widget-box.visible').removeClass('visible');//hide others
		            $(target).addClass('visible');//show target
		        });
		    });



		    //you don't need this, just used for changing background
		    jQuery(function ($) {
		        $('#btn-login-dark').on('click', function (e) {
		            $('body').attr('class', 'login-layout');
		            $('#id-text2').attr('class', 'white');
		            $('#id-company-text').attr('class', 'blue');

		            e.preventDefault();
		        });
		        $('#btn-login-light').on('click', function (e) {
		            $('body').attr('class', 'login-layout light-login');
		            $('#id-text2').attr('class', 'grey');
		            $('#id-company-text').attr('class', 'blue');

		            e.preventDefault();
		        });
		        $('#btn-login-blur').on('click', function (e) {
		            $('body').attr('class', 'login-layout blur-login');
		            $('#id-text2').attr('class', 'white');
		            $('#id-company-text').attr('class', 'light-blue');

		            e.preventDefault();
		        });
		        $('#txtPassword').keypress(function (event) {
		            if (event.keyCode == 13 || event.which == 13) {
		                $('#password').focus();
		                event.preventDefault();
		            }
		        });
		        $('#txtPassword').keypress(function (e) {
		            if (e.which == 13) {//Enter key pressed
		                $('#btnLogin2').click();//Trigger search button click event
		            }
		        });
		    });

		    
		</script>
</body>
</html>