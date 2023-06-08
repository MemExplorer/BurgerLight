<?php
session_start();
include_once('../connection.php');
include_once('../Models/response.php');
include_once('../Models/AddCartResponse.php');
include_once('../utils.php');

try {

    if (!isset($_SESSION['usersessionid'])) {
        Response::CreateResponse(false, "Session not found!", null);
        return;
    }

    if ($_SERVER['REQUEST_METHOD'] === 'GET') {

        $lname = $_GET['lname'];
        $fname = $_GET['fname'];
        $email = $_GET['email'];
        $tel = $_GET['tel'];

        $street = $_GET['street'];
        $city = $_GET['city'];
        $province = $_GET['province'];
        $zip = $_GET['zip'];

        $currUserId = Utils::FixSessionObject($_SESSION['usersessionid'])['userid'];
        $query = "INSERT INTO tblcheckout (userid, prodId, quantity) SELECT userid, prodId, quantity FROM tblorderlist WHERE userid = " . $currUserId . "; DELETE FROM tblorderlist WHERE userid = " . $currUserId . ";" . " INSERT INTO tbluserinfo (`user_no`, `lastname`, `firstname`, `email`, `tel_num`, `street`, `city`, `province`, `zip_code`) VALUES ('" . $currUserId . "', '" . $lname . "', '" . $fname . "', '" . $email . "', '" . $tel . "', '" . $street . "', '" . $city . "', '" . $province . "', '" . $zip . "');";

        if (mysqli_multi_query($conn, $query)) {
            // Success
            Response::CreateResponse(true, "Success", "Success");
        } else {
            // Error
            Response::CreateResponse(false, "Failed to process order!", null);
        }
    }
} finally {
    $conn->close();
}
?>