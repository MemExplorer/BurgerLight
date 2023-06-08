<?php
include_once('../../connection.php');
include_once('../../Models/response.php');
include_once('../../utils.php');

try {
    if ($_SERVER['REQUEST_METHOD'] === 'GET') {
        $username = $_GET['usrnm'];
        $password = $_GET['pswd'];
        $stmt = $conn->prepare("SELECT * FROM tbluserlogin WHERE username = ?");
        $stmt->bind_param("s", $username);
        $stmt->execute();

        $result = $stmt->get_result();

        // Check if the user exists
        if ($result->num_rows > 0) {
            Response::CreateResponse(false, "Username already exists!", null);
            return;
        } else {
            $saltVal = Utils::GenerateRandomString(150);
            $hashedPass = hash('sha256', $saltVal . $password);

            //insert acc into table
            $stmt = $conn->prepare("INSERT INTO tbluserlogin VALUES (0, '" . $username . "', '" . $hashedPass . "', '" . $saltVal . "')");

            $result = $stmt->execute();
            if ($result)
                Response::CreateResponse(true, "Successfully created account!", "Successfully created account!");
            else
                Response::CreateResponse(false, "Failed to Create Account!", null);
        }
    } else
        Response::CreateResponse(false, "Invalid Request!", null);

} finally {
    $conn->close();
}


?>