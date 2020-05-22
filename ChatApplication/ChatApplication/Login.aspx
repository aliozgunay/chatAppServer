<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ChatApplication.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="files/css/bootstrap.min.css" rel="stylesheet" />
    <link href="files/css/login.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Kaushan+Script" rel="stylesheet">
    <link rel="shortcut icon" href="img/logo.png" />
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous">
    
</head>
<body>
    <div class="container">
        <div class="row">
			<div class="col-md-5 mx-auto">
			<div id="first">
				<div class="myform form ">
					 <div class="logo mb-3">
						 <div class="col-md-12 text-center">
							<h1>Login</h1>
						 </div>
					</div>
                   <form action="" method="post" name="login">
                           <div class="form-group">
                              <label for="exampleInputEmail1">Username</label>
                              <input type="text" name="username"  class="username form-control" id="email" aria-describedby="emailHelp" placeholder="Enter email">
                           </div>
                           <div class="form-group">
                              <label for="exampleInputEmail1">Password</label>
                              <input type="password" name="password" id="password"  class="password form-control" aria-describedby="emailHelp" placeholder="Enter Password">
                           </div>
                           <div class="form-group">
                              <p class="text-center">By signing up you accept our <a href="#">Terms Of Use</a></p>
                           </div>
                           <div class="col-md-12 text-center ">
                              <a href="javascript:void(0)" class=" btn btn-block mybtn btn-primary tx-tfm loginBtn">Login</a>
                               
                           </div>
                           <div class="col-md-12 ">
                              <div class="login-or">
                                 <hr class="hr-or">
                                 <span class="span-or">or</span>
                              </div>
                           </div>
                           <div class="col-md-12 mb-3">
                              <p class="text-center">
                                 <a href="javascript:void();" class="google btn mybtn"><i class="fa fa-google-plus">
                                 </i> Signup using Google
                                 </a>
                              </p>
                           </div>
                           <div class="form-group">
                              <p class="text-center">Don't have account? <a href="#" id="signup">Sign up here</a></p>
                           </div>
                        </form>
                 
				</div>
			</div>
			  <div id="second">
			      <div class="myform form ">
                        <div class="logo mb-3">
                           <div class="col-md-12 text-center">
                              <h1 >Signup</h1>
                           </div>
                        </div>
                        
                         
                           <div class="form-group">
                              <label for="exampleInputEmail1">Email address</label>
                              <input type="email" name="email"  class="emailreg form-control" id="email" aria-describedby="emailHelp" placeholder="Enter email">
                           </div>
                            <div class="form-group">
                              <label for="exampleInputEmail1">Username</label>
                              <input type="text" name="username"  class="usernamereg form-control" id="username" aria-describedby="emailHelp" placeholder="Enter username">
                           </div>
                           <div class="form-group">
                              <label for="exampleInputEmail1">Password</label>
                              <input type="password" name="password" id="password"  class="passwordreg form-control" aria-describedby="emailHelp" placeholder="Enter Password">
                           </div>
                           <div class="col-md-12 text-center mb-3">
                              <button class=" btn btn-block mybtn btn-primary tx-tfm signupBtn" name="getstarted">Get Started For Free</button>
                               
                           </div>
                           <div class="col-md-12 ">
                              <div class="form-group">
                                 <p class="text-center"><a href="" id="signin">Already have an account?</a></p>
                              </div>
                           </div>
                            </div>
                        
                     </div>
			</div>
		</div>
      </div>  
</body>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>  
    <script src="files/js/bootstrap.min.js"></script>
    <script src="https://cdn.jsdelivr.net/jquery.validation/1.15.1/jquery.validate.min.js"></script>
    <script src="files/login.js"></script>
    
</html>
