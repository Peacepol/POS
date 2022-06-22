<div class="container"> 
<?php 
    require_once('postprocessor.php');
?>

<br/> 

<div class="card-body">
<form method="post" action="postprocessor.php">
    <input type = "button" class="btn btn-primary" value = "Print" onClick="print_report('table');"/>
    <button type="submit" class="btn btn-success" name="refreshReport">Refresh</button>
    <button type="submit" class="btn btn-success" name="searchReport">Search</button>
    <input type="hidden" name="client" value=1/>
    <input type="text" name="valueToSearch" placeholder="Value To Search" style="width:500px"/>
</form>
</div>
<body>
<div class="card-body">
<div id = "table">
<h2>Clients</h2>  
<table class="table table-striped table-bordered table-hover table-responsive" style="width:100%">
    <thead>
        <tr>
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
    
    while($value = mysqli_fetch_array($result)): ?>
        <tr>
            <td><?php echo $value['ClientName'];?></td>
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
    <?php endwhile; ?>
    </tbody>
</table>
</div>
<br/>
</div>
</div>





  