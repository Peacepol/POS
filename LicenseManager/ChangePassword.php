<?php 
require_once('postprocessor.php');
require_once 'function.php';
require('db.php');

if(isset($_GET['editUser'])) {
    $UserId = $_GET['editUser'];
    $update = true;
    $result  = $mysqlcon->query("SELECT * FROM user WHERE UserId = $UserId");
    $data = $result->fetch_array();
        $UserId = $data['UserId'];
        $UserName = $data['UserName'];
        $Password = $data['Password'];
        $FullName = $data['FullName'];
        $CanAdd = $data['CanAdd'];
        $CanEdit = $data['CanEdit'];
        $CanDelete =$data['CanDelete'];
}
?>

<div class="container">
 <div class="col-md-10">
    <div class="card my-3">
        <div class="card-header bg-primary" style="color: white">
            <h2>Change Password</h2>
        </div>
        <div class="card-body">
            <form class="form-horizontal" action="postprocessor.php" method="POST">          
                 <form class="form-horizontal" action="postprocessor.php" method="POST">
				 <div class="form-group">
                    <div class="col-md-9">
                        <input type="text" placeholder="Client Name" class="form-control"name="UserId" value="<?php echo $UserId; ?>" hidden  />
                    </div>
                </div>
                <div class="form-group">
                    <label for="Password" class="col-md-3 control-label">Password :</label>
                    <div class="col-md-12">
                        <input type="password" id="password" placeholder="Password" class="form-control"  name="Password" onkeyup='checkPassword();' required="required"  />
                    </div>
                </div>
                <div class="form-group">
                    <label for="confirm_password" class="col-md-3 control-label">Confirm Password :</label>
                    <div class="col-md-12">
                        <input type="password" id="confirm_password" placeholder="Confirm Password" class="form-control" name="ConfirmPassword" onkeyup='checkPassword();'  />
						<span id='message'></span>
                    </div>
					
                </div>
              
                <!-- /.form-group -->
                <div class="form-group">
                    <div class="col-md-9 col-md-offset-3">
                    </div>
                </div>
                <button type="submit" class="btn btn-primary btn-block mb-2" name="changePassword"  id="saveUser"  disabled >Change</button>                     
            </form> <!-- /form -->      
        </div> 
    </div>
 </div>
</div><!-- ./container -->
