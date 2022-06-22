<?php
	use App\Application\Actions\User\ListUsersAction;
	use App\Application\Actions\User\ViewUserAction;
	use Psr\Http\Message\ResponseInterface as Response;
	use Psr\Http\Message\ServerRequestInterface as Request;
	use Slim\App;
	use Slim\Interfaces\RouteCollectorProxyInterface as Group;

	return function (App $app) {
		$app->get('/activate/{uuid}', function (Request $request, Response $response, $args) 
		{
			$uuid = $args['uuid'];

			if ($uuid != NULL || $uuid != "") {
				$rawuuid = base64_decode($uuid);
				list($company, $regno, $serialno) = explode(",", $rawuuid);

				$activestatus = false;
				$jsonresult;
				$strjsonresult = "{
									\"Status\": \"noops\",
									\"Message\": \"No operation\",
									\"Company\": \"\",
									\"IsActivated\": $activestatus
								  }";	
				try {
					require("./db.php");		
					$compsql = "SELECT CompanyName, RegistrationNumber, SerialNumber FROM companyregistration";
					$result = $mysqlcon->query($compsql);
					if ($result->num_rows == 1 
						&& ($record = $result->fetch_assoc()))
					{
						if ($company == $record["CompanyName"]
							&& $regno == $record["RegistrationNumber"]
							&& $serialno == $record["SerialNumber"])
						{
							$activestatus = true;
							$strjsonresult = "{
												\"Status\": \"success\",
												\"Message\": \"UUID is valid\",
												\"Company\": \"$company\", 
												\"IsActivated\": $activestatus
											  }";
						}
					}
				} catch (Exception $ex) {
					$strjsonresult = "{
										\"Status\": \"failed\",
										\"Message\": \"$ex->getMessage()\",
										\"Company\": \"\",
										\"IsActivated\": $activestatus
									  }";
				} finally {
					if ($mysqlcon != NULL)
						$mysqlcon->close();
					
					$jsonresult = json_encode($strjsonresult);
				}
					
				$response->getBody()->write($jsonresult);
			}
			
			return $response;
		});
	};
?>