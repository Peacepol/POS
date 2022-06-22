<?php 
require_once('postprocessor.php');
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
            <h2>Client Registration</h2>
        </div>
        <div class="card-body">
            <form class="form-horizontal" action="postprocessor.php" method="POST">
                <div class="form-group">
                    <label for="ControlCode" class="col-md-3 control-label">Control Code</label>
                    <div class="col-md-12">
                        <input type="text" id="ControlCode" placeholder="Control Code" class="form-control" autofocus name="ControlCode" required="required"/>
                    </div>
                </div>
                <div class="form-group">
                    <label for="ClientName" class="col-md-3 control-label">Client Name</label>
                    <div class="col-md-12">
                        <input type="text" id="ClientName" placeholder="Client Name" class="form-control" autofocus name="ClientName" required="required"/>
                    </div>
                </div>
                <div class="form-group">
                    <label for="SerialNumber" class="col-md-3 control-label">Serial Number</label>
                    <div class="col-md-12">
                        <input type="number" id="SerialNumber" placeholder="Serial Name" class="form-control" autofocus name="SerialNumber" readonly value="<?php echo $rnd; ?>"/>
                    </div>
                </div>
                <div class="form-group">
                    <label for="RegistrationCode" class="col-md-3 control-label">Registration Code</label>
                    <div class="col-md-12">
                        <input type="text" id="RegistrationCode" placeholder="Registration Code" class="form-control" name="RegistrationCode" readonly value="<?php echo generateKey(); ?>"/>
                    </div>
                </div>
                <div class="form-group">
                    <label for="Address" class="col-md-3 control-label">Address</label>
                    <div class="col-md-12">
                        <input type="text" id="Address" placeholder="Address" class="form-control" name="Address" required="required"/>
                    </div>
                </div>
                <div class="form-group">
                    <label for="City" class="col-md-3 control-label">City</label>
                    <div class="col-md-12">
                        <input type="text" id="City" placeholder="City" class="form-control" name="City" required="required"/>
                    </div>
                </div>
                <div class="form-group">
                    <label for="State" class="col-md-3 control-label">State</label>
                    <div class="col-md-12">
                        <input type="text" id="State" placeholder="State" class="form-control" name="State" required="required"/>
                    </div>
                </div>
                <div class="form-group">
                    <label for="PostCode" class="col-md-3 control-label">Post Code</label>
                    <div class="col-md-12">
                        <input type="text" id="PostCode" placeholder="Post Code " class="form-control" name="PostCode" required="required"/>
                    </div>
                </div>
                <div class="form-group">
                    <label for="Country" class="col-md-3 control-label">Country</label>
                    <div class="col-md-12">
                        <input type="text" id="Country" placeholder="Country" class="form-control" name="Country" required="required"/>
                    </div>
                </div>
                <div class="form-group">
                    <label for="RegistrationName" class="col-md-3 control-label">Registration Name</label>
                    <div class="col-md-12">
                        <input type="text" id="RegistrationName" placeholder="Registration Name" class="form-control" name="RegistrationName" required="required"/>
                    </div>
                </div>
                <div class="form-group">
                    <label for="Phone" class="col-md-3 control-label">Phone</label>
                    <div class="col-md-12">
                        <input type="phonenumber" id="Phone" placeholder="Phone" class="form-control" name="Phone" required="required"/>
                    </div>
                </div>
                <div class="form-group">
                    <label for="Fax" class="col-md-3 control-label">Fax</label>
                    <div class="col-md-12">
                        <input type="text" id="Fax" placeholder="Fax" class="form-control" name="Fax"/>
                    </div>
                </div> 
                <div class="form-group">
                    <label for="Email" class="col-md-3 control-label">Email</label>
                    <div class="col-md-12">
                        <input type="email" id="Email" placeholder="Email" class="form-control" name="Email" required="required"/>
                    </div>
                </div>   
                <div class="form-group">
                    <label for="ContactPerson" class="col-md-3 control-label">Contact Person</label>
                    <div class="col-md-12">
                        <input type="text" id="ContactPerson" placeholder="Contact Person" class="form-control" name="ContactPerson" required="required"/>
                    </div>
                </div>             
                <!-- /.form-group -->
                <div class="form-group">
                    <div class="col-md-12 col-md-offset-3">
                    </div>
                </div>
                <button type="submit" class="btn btn-primary btn-block" name="save" <?php echo $_SESSION['uAdd'];?>>Register</button>           
                <br/> 
                <button type="button" class="btn btn-primary btn-block" onClick="ajaxRequest('DataTableClient.php');">View Table</button>                
            </form> <!-- /form -->
        </div>
        <br/>
    </div>
</div> <!-- ./container -->
</div>
<!-- /#wrapper -->

