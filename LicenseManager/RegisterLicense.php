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
    <div class="container">
        <form class="form-horizontal" action="postprocessor.php" method="POST">
            <h2>Client Registration</h2>
            <br/>
            <br/>
            <div class="form-group">
                <label for="ClientId" class="col-md-3 control-label">Client ID</label>
                <div class="col-md-9">
                    <input type="text" id="ClientId" placeholder="Client Id" class="form-control" autofocus name= "ClientId" required="required"/>
                </div>
            </div>
            <div class="form-group">
                <label for="InvoiceId" class="col-md-3 control-label">Invoice ID</label>
                <div class="col-md-9">
                    <input type="number" id="InvoiceId" placeholder="Invoice Id" class="form-control" autofocus name="InvoiceId" required="required"/>
                </div>
            </div>
            <div class="form-group">
                <label for="PurchaseDate" class="col-md-3 control-label">Registration Code</label>
                <div class="col-md-9">
                    <input type="text" id="PurchaseDate" placeholder="Purchase Date" class="form-control" name="RegistrationCode" required="required"/>
                </div>
            </div>
            <div class="form-group">
                <label for="PurchaseAmount" class="col-md-3 control-label">Purchase Amount</label>
                <div class="col-md-9">
                    <input type="text" id="PurchaseAmount" placeholder="Purchase Amount" class="form-control" name="PurchaseAmount" required="required"/>
                </div>
            </div>
            <div class="form-group">
                <label for="PaymentMode" class="col-md-3 control-label">Payment Mode</label>
                <div class="col-md-9">
                    <input type="text" id="PaymentMode" placeholder="Payment Mode" class="form-control" name="PaymentMode" required="required"/>
                </div>
            </div>
            <div class="form-group">
                <label for="LicenseStatus" class="col-md-3 control-label">LicenseStatus</label>
                <div class="col-md-9">
                    <input type="text" id="LicenseStatus" placeholder="License Status" class="form-control" name="State" required="required"/>
                </div>
            </div>
            <div class="form-group">
                <label for="PostCode" class="col-md-3 control-label">Post Code</label>
                <div class="col-md-9">
                    <input type="text" id="PostCode" placeholder="Post Code" class="form-control" name="PostCode" required="required"/>
                </div>
            </div>
            <div class="form-group">
                <label for="Country" class="col-md-3 control-label">Country</label>
                <div class="col-md-9">
                    <input type="text" id="Country" placeholder="Country" class="form-control" name="Country" required="required"/>
                </div>
            </div>
            <div class="form-group">
                <label for="RegistrationName" class="col-md-3 control-label">Registration Name</label>
                <div class="col-md-9">
                    <input type="text" id="RegistrationName" placeholder="Registration Name" class="form-control" name="RegistrationName" required="required"/>
                </div>
            </div>
            <div class="form-group">
                <label for="Phone" class="col-md-3 control-label">Phone</label>
                <div class="col-md-9">
                    <input type="phonenumber" id="Phone" placeholder="Phone" class="form-control" name="Phone" required="required"/>
                </div>
            </div>
            <div class="form-group">
                <label for="Fax" class="col-md-3 control-label">Fax</label>
                <div class="col-md-9">
                    <input type="text" id="Fax" placeholder="Fax" class="form-control" name="Fax" required="required"/>
                </div>
            </div> 
            <div class="form-group">
                <label for="Email" class="col-md-3 control-label">Email</label>
                <div class="col-md-9">
                    <input type="email" id="Email" placeholder="Email" class="form-control" name="Email" required="required"/>
                </div>
            </div>   
            <div class="form-group">
                <label for="ContactPerson" class="col-md-3 control-label">Contact Person</label>
                <div class="col-md-9">
                    <input type="text" id="ContactPerson" placeholder="Contact Person" class="form-control" name="ContactPerson" required="required"/>
                </div>
            </div>                  
            <!-- /.form-group -->
            <div class="form-group">
                <div class="col-md-9 col-md-offset-3">
                </div>
            </div>
            <button type="submit" class="btn btn-primary btn-block" name="save">Register</button>
            <button type="button" class="btn btn-primary btn-block" onClick="ajaxRequest('DataTableLicense.php');">View Table</button>            
        </form> <!-- /form -->            
    </div> <!-- ./container -->
</div> <!-- ./wrapper -->