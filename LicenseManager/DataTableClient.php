<div class="container"> 
<?php 
    require_once('postprocessor.php');
?>
</div> <!-- .container -->
 <div id="table1">
<div class="card border-primary my-3 mx-2" >
    <div class="card-header">
        <h3>Clients List</h3> 
    </div>
<?php if(isset($_SESSION['message'])): ?>
<div class="alert alert-<?=$_SESSION['msg_type']?>">
<?php
    echo $_SESSION['message'];
    unset($_SESSION['message']);
?>
</div>
<?php endif ?>      
<div class="card-body ">
    <form method="post" action="postprocessor.php">
        <input type = "button" class="btn btn-primary d-print-none" value = "Print" onClick="print_report('table1');"/>
        <button type="submit" class="btn btn-success d-print-none" name="refresh">Refresh</button>
        <button type="submit" class="btn btn-success d-print-none" name="search">Search</button>
        <input type="hidden" name="client" value=1/>
        <input type="text" name="valueToSearch" placeholder="Value To Search"  class="d-print-none"style="width:500px"/>
    </form>
	<div class="table-responsive container"style="max-width:100%">
            <table class="table table-striped table-bordered mt-3" style="width:100%">
                <thead>
                    <tr>
                        <th class="d-print-none">Update</th>
                        <th class="d-print-none">Delete</th>
                        <th class="d-print-none">License</th>
                        <th>Client Name</th>
                        <th>Serial Number</th>
                        <th>RegistrationCode</th>
                        <th>Address</th>
                        <th>City date</th>
                        <th>State</th>
                        <th>PostCode</th>
                        <th>Country</th>
                        <th>Registration Name</th>
                        <th>Phone</th>
                        <th>Fax</th>
                        <th>Email</th>
                        <th>Contact Person</th>
                    </tr>
                </thead>
                <tbody>
                <?php 

                if (isset($_SESSION['searchquery'])) {
                    $query = $_SESSION['searchquery'];
                    unset ($_SESSION['searchquery']);
                    unset ($_POST['client']);
                } else {
                    $query = "SELECT * FROM `client`";
                }
                $result = filterTable($query);
                
                if ($result != null && $result->num_rows > 0) {
                    while($value = mysqli_fetch_array($result)): 
                    $ClientId = $value['ClientId'];
                    $ClientName = $value['ClientName'];
                ?>
                    <tr>
                     <td class="d-print-none">
                            <button class="btn btn-success btn-block" onClick="ajaxRequest('UpdateClient.php?edit=',<?php echo $value['ClientId'];?>);" <?php echo $_SESSION['uEdit'];?> >Edit</button>
                        </td>
                        <td class="d-print-none">
                            <a href="postprocessor.php?delete=<?php echo $value['ClientId'];?>">
                                <button class="btn btn-danger btn-block" <?php echo $_SESSION['uDelete'];?>  >Delete</button>
                            </a>
                        </td>
                        <td class="d-print-none">
                            <button class="btn btn-primary btn-block " onClick="ajaxRequest('activate.php?active=',<?php echo $value['ClientId'];?>);">License</button>
                        </td>
                        <td><?php echo "<a href=# onClick=\"ajaxRequest('viewClient.php?viewClient=', $ClientId);\">$ClientName</a>"; ?></td>
                        <td><?php echo $value['SerialNumber'];?></td>
                        <td><?php echo $value['RegistrationCode'];?></td>
                        <td><?php echo $value['Address'];?></td>
                        <td><?php echo $value['City'];?></td>
                        <td><?php echo $value['State'];?></td>
                        <td><?php echo $value['PostCode'];?></td>
                        <td><?php echo $value['Country'];?></td>
                        <td><?php echo $value['RegistrationName'];?></td>
                        <td><?php echo $value['Phone'];?></td>
                        <td><?php echo $value['Fax'];?></td>
                        <td><?php echo $value['Email'];?></td>
                        <td><?php echo $value['ContactPerson'];?></td>
                       
                    </tr>
                <?php 
                    endwhile;
                }
                ?>
                </tbody>
            </table>
        </div>

        <br/>

        <p align = "right">
            <button type="submit" class="btn btn-primary d-print-none" onClick="ajaxRequest('RegisterClient.php');"> Register a Client </button>
        </p>
    </div>
</div>

  
