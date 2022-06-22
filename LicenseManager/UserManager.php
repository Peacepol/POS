<div class="container"> 
<?php 
    require_once('postprocessor.php');
    require_once('function.php');
?>
</div> <!-- .container -->
<div class="container">
 <div id="table1">
<div class="card border-primary my-3 mx-2" style="max-width:100%">
    <div class="card-header">
        <h3>User List</h3> 
    </div>
<?php if(isset($_SESSION['message'])): ?>
<div class="alert alert-dismissible alert-<?=$_SESSION['msg_type']?> mx-2 my-2"  role="alert">
 
<?php
    echo $_SESSION['message'];
    unset($_SESSION['message']);
?>
<button type="button" class="close" data-dismiss="alert">&times;</button>
</div>
<?php endif ?>      
<div class="card-body">
    <form method="post" action="postprocessor.php">
        <input type = "button" class="btn btn-primary d-print-none" value = "Print" onClick="print_report('table1');"/>
        <button type="submit" class="btn btn-success d-print-none" name="refresh">Refresh</button>
        <button type="submit" class="btn btn-success d-print-none" name="search">Search</button>
        <input type="hidden" name="usermanager" value=1/>
        <input type="text" name="valueToSearch" placeholder="Value To Search"  class="d-print-none" style="width:500px"/>
    </form>
	<div class="table-responsive">
            <table class="table table-striped table-bordered mt-3 " style="max-width:100%">
                <thead>
                    <tr>
                        <th>User Name</th>
                        <th>Full Name</th>
                        <th>Can Add</th>
                        <th>Can Edit</th>
                        <th>Can Delete</th>
                        <th class="d-print-none">Update</th>
                        <th class="d-print-none">Delete</th>
						<th class="d-print-none">Change Password</th>
                    </tr>
                </thead>
                <tbody>
                <?php 

                if (isset($_SESSION['searchquery'])) {
                    $query = $_SESSION['searchquery'];
                    unset ($_SESSION['searchquery']);
                    unset ($_POST['user']);
                } else {
                    $query = "SELECT * FROM `user`";
                }
                $result = filterTable($query);
                
                if ($result != null && $result->num_rows > 0) {
                    while($value = mysqli_fetch_array($result)): 
                    $UserID = $value['UserId'];
                    $UserName = $value['UserName'];
                ?>
                    <tr>
                      <td><?php echo $value['UserName'];?></td>
                        <td><?php echo $value['FullName'];?></td>
                        <td><?php echo $value['CanAdd'];?></td>
                        <td><?php echo $value['CanEdit'];?></td>
                        <td><?php echo $value['CanDelete'];?></td>
                        <td class="d-print-none">
                            <button class="btn btn-success btn-block" onClick="ajaxRequest('UpdateUser.php?editUser=',<?php echo $value['UserId'];?>);" <?php echo $_SESSION['uEdit'];?> >Edit</button>
                        </td>
                        <td class="d-print-none">
                            <a href="postprocessor.php?deleteUser=<?php echo $value['UserId'];?>">
                                <button class="btn btn-danger btn-block"  <?php echo $_SESSION['uDelete'];?>>Delete</button>
                            </a>
                        </td>
						<td class="d-print-none">
                            <button class="btn btn-info btn-block" onClick="ajaxRequest('ChangePassword.php?editUser=',<?php echo $value['UserId'];?>);"  <?php echo $_SESSION['uEdit'];?>>Change Password</button>
                        </td>
                       
                    </tr>
                <?php 
                    endwhile;
                }
                ?>
                </tbody>
            </table>
        </div>
	</div>
        <br/>

        <p align = "right">
            <button type="submit" class="btn btn-primary d-print-none mx-3" onClick="ajaxRequest('AddUser.php');" <?php echo $_SESSION['uAdd'];?>>Add new user</button>
        </p>
    </div>
	</div>
</div>

  
