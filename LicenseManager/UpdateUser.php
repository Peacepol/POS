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

		$checkAdd = '';
		$checkEdit= '';
		$checkDelete= '';
		if($data['CanAdd'] == 1){
			$checkAdd = 'checked';
		}
		if($data['CanEdit'] == 1){
			$checkEdit = 'checked';
		}
		if($data['CanDelete'] == 1){
			$checkDelete = 'checked';
		}
}
?>

<div class="container">
 <div class="col-md-10">
    <div class="card my-3">
        <div class="card-header bg-primary" style="color: white">
            <h2>Update User</h2>
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
                    <label for="UserName" class="col-md-3 control-label">Username :</label>
                    <div class="col-md-12">
                        <input type="text" id="Username" placeholder="Username" class="form-control" name="UserName" value="<?php echo $UserName; ?>"/>
                    </div>
                </div>

                <div class="form-group">
                    <label for="FullName" class="col-md-3 control-label">Full Name</label>
                    <div class="col-md-12">
                        <input type="text" id="FullName" placeholder="FullName" class="form-control" name="FullName" value="<?php echo $FullName; ?>"/>
                    </div>
                </div>
				<div class="form-group col-md-9">
					<div class="d-flex justify-content-between">
						<label for="CanAdd" class="col-md-2 control-label">Can Add :</label>
						<label for="CanEdit" class="col-md-2 control-label">Can Edit :</label>
						<label for="CanDelete" class="col-md-3 control-label">Can Delete :</label>
					</div>
					<div class="d-flex justify-content-between ">
						<div class="col-md-2">
							<input type="checkbox" id="CanAdd" class="form-control" name="canAdd" value = "1" <?php echo $checkAdd; ?> /> 
						</div>
						
						<div class="col-md-2">
							<input type="checkbox" id="CanEdit" class="form-control" name="canEdit"  value = "1"  <?php echo $checkEdit; ?>/> 
						</div>
						
						<div class="col-md-2">
							<input type="checkbox" id="CanDelete" class="form-control" name="canDelete" value = "1" <?php echo $checkDelete; ?> /> 
						</div>
					</div>
                </div>
          
                <!-- /.form-group -->
                <div class="form-group">
                    <div class="col-md-9 col-md-offset-3">
                    </div>
                </div>
                <button type="submit" class="btn btn-primary btn-block mb-2" name="updateUser" id="updateUser"  >Update</button>
                <button type="button" class="btn btn-primary btn-block" onClick="ajaxRequest('UserManager.php');">View Table</button>                          
            </form> <!-- /form -->      
        </div> 
    </div>
 </div>
</div><!-- ./container -->
