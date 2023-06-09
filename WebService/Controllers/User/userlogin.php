<?php

session_start();
include_once('../../connection.php');
include_once('../../Models/response.php');
include_once('../../Models/LoginResponse.php');
include_once('../../utils.php');

try {
	if ($_SERVER['REQUEST_METHOD'] === 'GET') {
		if (isset($_SESSION['usersessionid'])) {
			Response::CreateResponse(false, "User already logged In.", null);
			return;
		}

		//$username = "testuser";
		//$password = "testpassword";

		$username = $_GET['usrnm'];
		$password = $_GET['pswd'];

		// Prepare a SQL statement to select the user from the database
		$stmt = $conn->prepare("SELECT * FROM tbluserlogin WHERE username = ?");
		$stmt->bind_param("s", $username);
		$stmt->execute();

		// Get the result of the query
		$result = $stmt->get_result();

		// Check if the user exists
		if ($result->num_rows === 1) {
			// Fetch the user data from the result
			$user = $result->fetch_assoc();

			//hash password with the salt
			$saltVal = $user['salt'];
			$hashedPass = hash('sha256', $saltVal . $password);

			// Verify the password
			if ($hashedPass === $user['hashedpass']) {
				$uid = $user['userid'];

				//fetch cart total
				$totalQuery = $conn->query("SELECT SUM(quantity) as total FROM tblorderlist WHERE userid = " . $user['userid']);
				$row = $totalQuery->fetch_assoc();
				$q = $row['total'];
				if (!Utils::ValidateInt($q))
					$q = 0;
				// Successful login
				$_SESSION['usersessionid'] = new LoginResponse($uid, $username, $q);
				Response::CreateResponse(true, "Success", $_SESSION['usersessionid']);
			} else {
				// Invalid password
				Response::CreateResponse(false, "Invalid password.", null);
			}
		} else {
			// Invalid username
			Response::CreateResponse(false, "Invalid username.", null);
		}
	} else
		Response::CreateResponse(false, "Invalid Request.", null);
} finally {
	$conn->close();
}
?>