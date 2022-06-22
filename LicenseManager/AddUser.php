<?php 
require_once('postprocessor.php');
require_once('function.php'); 
?>
<?php if(isset($_SESSION['message'])): ?>
    <div class="alert alert-<?=$_SESSION[`msg_type`]?>">
    <?php
        echo $_SESSION['message'];
        unset($_SESSION['message']);
    ?>
    </div>
<?php endif ?>

<div class="d-flex" id="wrapper">
<!-- Page Content -->
<div class="container">
<?php $rnd = rand(1000000, 100); ?>
    <div class="card border-primary my-3 mx-2" style="max-width:100%">
        <div class="card-header bg-primary" style="color: white">
            <h2>Add User</h2>
        </div>
        <div class="card-body">
            <form class="form-horizontal" action="postprocessor.php" method="POST">
                <div class="form-group">
                    <label for="UserName" class="col-md-3 control-label">Username :</label>
                    <div class="col-md-12">
                        <input type="text" id="Username" placeholder="Username" class="form-control" autofocus name="UserName" required="required"required="required"/>
                    </div>
                </div>
                <div class="form-group">
                    <label for="Password" class="col-md-3 control-label">Password :</label>
                    <div class="col-md-12">
                        <input type="password" id="password" placeholder="Password" class="form-control" autofocus name="Password" onkeyup='checkPassword();' required="required"/>
                    </div>
                </div>
                <div class="form-group">
                    <label for="confirm_password" class="col-md-3 control-label">Confirm Password :</label>
                    <div class="col-md-12">
                        <input type="password" id="confirm_password" placeholder="Confirm Password" class="form-control" autofocus name="ConfirmPassword" required="required" onkeyup='checkPassword();' />
						<span id='message'></span>
                    </div>
					
                </div>
                <div class="form-group">
                    <label for="FullName" class="col-md-3 control-label">Full Name</label>
                    <div class="col-md-12">
                        <input type="text" id="FullName" placeholder="FullName" class="form-control" name="FullName" required="required"/>
                    </div>
                </div>
				<div class="form-group col-md-9">
					<div class="d-flex justify-content-between">
						<label for="CanAdd" class="col-md-2 control-label">Can Add :</label>
						<label for="CanEdit" class="col-md-2 control-label">Can Edit :</label>
						<label for="CanDelete" class="col-md-2 control-label">Can Delete :</label>
					</div>
					<div class="d-flex justify-content-between ">
							<div class="col-md-2">
							<input type="checkbox" id="CanAdd" class="form-control" name="canAdd" value = "1" /> 
						</div>
						
						<div class="col-md-2">
							<input type="checkbox" id="CanEdit" class="form-control" name="canEdit"  value = "1" >
						</div>
						
						<div class="col-md-2">
							<input type="checkbox" id="CanDelete" class="form-control" name="canDelete" value = "1" /> 
						</div>
					</div>
                </div>
          
                <!-- /.form-group -->
                <div class="form-group">
                    <div class="col-md-12 col-md-offset-3">
					  <button type="submit" class="btn btn-primary btn-block" name="saveUser" id="saveUser" disabled>Save</button>          
                    </div>
                </div>
               
                <br/> 
            </form> <!-- /form -->
        </div>
        <br/>
    </div>
	
</div> <!-- ./container -->
</div>
<!-- /#wrapper -->

