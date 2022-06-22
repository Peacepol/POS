<?php 
require_once('postprocessor.php');

if(isset($_GET['edit'])) {
    $ClientId = $_GET['edit'];
    $update = true;
    $result  = $mysqlcon->query("SELECT * FROM client WHERE ClientId = $ClientId");
    $data = $result->fetch_array();
    $ClientId = $data['ClientId'];
    $ClientName = $data['ClientName'];
    $SerialNumber = $data['SerialNumber'];
    $RegistrationCode = $data['RegistrationCode'];
    $Address = $data['Address'];
    $City = $data['City'];
    $State =$data['State'];
    $PostCode = $data['PostCode'];
    $Country = $data['Country'];
    $RegistrationName =$data['RegistrationName'];
    $Phone = $data['Phone'];
    $Fax = $data['Fax'];
    $Email = $data['Email'];  
    $ContactPerson = $data['ContactPerson'];
    $ControlCode = $data['ControlCode'];
}
?>

<div class="container">
 <div class="col-md-10">
    <div class="card">
        <div class="card-header bg-primary" style="color: white">
            <h2>Update Client</h2>
        </div>
        <div class="card-body">
            <form class="form-horizontal" action="postprocessor.php" method="POST">          
                <div class="form-group">
                    <div class="col-md-9">
                        <input type="text" placeholder="Client Name" class="form-control" autofocus name="ClientId" value="<?php echo $ClientId; ?>" hidden />
                    </div>
                </div>
                <div class="form-group">
                    <label for="ControlCode" class="col-md-3 control-label">Control Code</label>
                    <div class="col-md-12">
                        <input type="text" id="ControlCode" placeholder="Control Code" class="form-control" autofocus name="ControlCode" required="required" value="<?php echo $ControlCode; ?>"/>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ClientName" class="col-md-3 control-label">Client Name</label>
                    <div class="col-md-9">
                        <input type="text" placeholder="Client Name" class="form-control" autofocus name="ClientName" required="required" value="<?php echo $ClientName; ?>" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="SerialNumber" class="col-md-3 control-label">Serial Number</label>
                    <div class="col-md-9">
                        <input type="number" placeholder="Serial Name" class="form-control" autofocus name="SerialNumber" required="required" value="<?php echo $SerialNumber; ?>" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="RegistrationCode" class="col-md-3 control-label">Registration Code</label>
                    <div class="col-md-9">
                        <input type="text" placeholder="Registration Code" class="form-control" name="RegistrationCode" required="required" value="<?php echo $RegistrationCode; ?>" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="Address" class="col-md-3 control-label">Address</label>
                    <div class="col-md-9">
                        <input type="text" placeholder="Address" class="form-control" name="Address" required="required" value="<?php echo $Address; ?>" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="City" class="col-md-3 control-label">City</label>
                    <div class="col-md-9">
                        <input type="text" placeholder="City" class="form-control" name="City" required="required" value="<?php echo $City; ?>" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="State" class="col-md-3 control-label">State</label>
                    <div class="col-md-9">
                        <input type="text" placeholder="State" class="form-control" name="State" required="required" value="<?php echo $State; ?>" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="PostCode" class="col-md-3 control-label">Post Code</label>
                    <div class="col-md-9">
                        <input type="text" placeholder="Post Code " class="form-control" name="PostCode" required="required" value="<?php echo $PostCode; ?>" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="Country" class="col-md-3 control-label">Country</label>
                    <div class="col-md-9">
                        <input type="text" placeholder="Country" class="form-control" name="Country" required="required" value="<?php echo $Country; ?>" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="RegistrationName" class="col-md-3 control-label">Registration Name</label>
                    <div class="col-md-9">
                        <input type="text" placeholder="Registration Name" class="form-control" name="RegistrationName" required="required" value="<?php echo $RegistrationName; ?>" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="Phone" class="col-md-3 control-label">Phone</label>
                    <div class="col-md-9">
                        <input type="phonenumber" placeholder="Phone" class="form-control" name="Phone" required="required" value="<?php echo $Phone; ?>" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="Fax" class="col-md-3 control-label">Fax</label>
                    <div class="col-md-9">
                        <input type="text" placeholder="Fax" class="form-control" name="Fax" required="required" value="<?php echo $Fax; ?>" />
                    </div>
                </div> 
                <div class="form-group">
                    <label for="Email" class="col-md-3 control-label">Email</label>
                    <div class="col-md-9">
                        <input type="email" placeholder="Email" class="form-control" name="Email" required="required" value="<?php echo $Email; ?>" />
                    </div>
                </div>   
                <div class="form-group">
                    <label for="ContactPerson" class="col-md-3 control-label">Contact Person</label>
                    <div class="col-md-9">
                        <input type="text" placeholder="Contact Person" class="form-control" name="ContactPerson" required="required" value="<?php echo $ContactPerson; ?>" />
                    </div>
                </div>                  
                <!-- /.form-group -->
                <div class="form-group">
                    <div class="col-md-9 col-md-offset-3">
                    </div>
                </div>
                <button type="submit" class="btn btn-primary btn-block mb-2" name="update" <?php echo $_SESSION['uAdd'];?>>Save Data</button>
                <button type="button" class="btn btn-primary btn-block" onClick="ajaxRequest('DataTableClient.php');">View Table</button>                          
            </form> <!-- /form -->      
        </div> 
    </div>
 </div>
</div><!-- ./container -->
